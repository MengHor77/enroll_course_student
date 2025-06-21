using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class StudentForm : Form
    {
        private string connectionString = @"Data source=DESKTOP-S99S529\SQLEXPRESS;Initial Catalog=YourDatabaseName;Integrated Security=true";
        private int? studentId;

        public StudentForm()
        {
            InitializeComponent();
            this.Text = "Add New Student";
        }

        public StudentForm(int id) : this()
        {
            studentId = id;
            this.Text = "Edit Student";
            LoadStudentData(id);
        }

        private void LoadStudentData(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT lastname, firstname, gender, age FROM students WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtLastName.Text = reader["lastname"].ToString();
                            txtFirstName.Text = reader["firstname"].ToString();
                            cmbGender.Text = reader["gender"].ToString();
                            numAge.Value = Convert.ToInt32(reader["age"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading student data: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please enter both first and last names", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd;

                    if (studentId.HasValue)
                    {
                        // Update existing student
                        cmd = new SqlCommand(
                            "UPDATE students SET lastname = @lastname, firstname = @firstname, " +
                            "gender = @gender, age = @age WHERE id = @id", conn);
                        cmd.Parameters.AddWithValue("@id", studentId.Value);
                    }
                    else
                    {
                        // Insert new student
                        cmd = new SqlCommand(
                            "INSERT INTO students (lastname, firstname, gender, age) " +
                            "VALUES (@lastname, @firstname, @gender, @age)", conn);
                    }

                    cmd.Parameters.AddWithValue("@lastname", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@firstname", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@gender", cmbGender.Text);
                    cmd.Parameters.AddWithValue("@age", numAge.Value);

                    cmd.ExecuteNonQuery();
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving student: {ex.Message}", "Error",
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