using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class EnrollForm : Form
    {
        private string connectionString = @"Data source=DESKTOP-S99S529\SQLEXPRESS;Initial Catalog=YourDatabaseName;Integrated Security=true";
        private int? enrollmentId;

        public EnrollForm()
        {
            InitializeComponent();
            this.Text = "Add New Enrollment";
            LoadComboBoxData();
        }

        public EnrollForm(int id) : this()
        {
            enrollmentId = id;
            this.Text = "Edit Enrollment";
            LoadEnrollmentData(id);
        }

        private void LoadComboBoxData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Load students
                    string studentQuery = "SELECT id, lastname + ', ' + firstname AS name FROM students";
                    SqlDataAdapter studentAdapter = new SqlDataAdapter(studentQuery, conn);
                    DataTable studentTable = new DataTable();
                    studentAdapter.Fill(studentTable);

                    cmbStudent.DisplayMember = "name";
                    cmbStudent.ValueMember = "id";
                    cmbStudent.DataSource = studentTable;

                    // Load courses
                    string courseQuery = "SELECT id, code + ' - ' + name AS name FROM courses";
                    SqlDataAdapter courseAdapter = new SqlDataAdapter(courseQuery, conn);
                    DataTable courseTable = new DataTable();
                    courseAdapter.Fill(courseTable);

                    cmbCourse.DisplayMember = "name";
                    cmbCourse.ValueMember = "id";
                    cmbCourse.DataSource = courseTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dropdown data: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadEnrollmentData(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT studentid, courseid FROM enrollings WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cmbStudent.SelectedValue = reader["studentid"];
                            cmbCourse.SelectedValue = reader["courseid"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading enrollment data: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbStudent.SelectedValue == null || cmbCourse.SelectedValue == null)
            {
                MessageBox.Show("Please select both student and course", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd;

                    if (enrollmentId.HasValue)
                    {
                        // Update existing enrollment
                        cmd = new SqlCommand(
                            "UPDATE enrollings SET studentid = @studentid, courseid = @courseid " +
                            "WHERE id = @id", conn);
                        cmd.Parameters.AddWithValue("@id", enrollmentId.Value);
                    }
                    else
                    {
                        // Check if enrollment already exists
                        SqlCommand checkCmd = new SqlCommand(
                            "SELECT COUNT(*) FROM enrollings WHERE studentid = @studentid AND courseid = @courseid",
                            conn);
                        checkCmd.Parameters.AddWithValue("@studentid", cmbStudent.SelectedValue);
                        checkCmd.Parameters.AddWithValue("@courseid", cmbCourse.SelectedValue);

                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("This student is already enrolled in this course", "Warning",
                                          MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Insert new enrollment
                        cmd = new SqlCommand(
                            "INSERT INTO enrollings (studentid, courseid) " +
                            "VALUES (@studentid, @courseid)", conn);
                    }

                    cmd.Parameters.AddWithValue("@studentid", cmbStudent.SelectedValue);
                    cmd.Parameters.AddWithValue("@courseid", cmbCourse.SelectedValue);

                    cmd.ExecuteNonQuery();
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving enrollment: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}