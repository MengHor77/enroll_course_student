using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        private string connectionString = @"Data source=DESKTOP-S99S529\SQLEXPRESS;Initial Catalog=YourDatabaseName;Integrated Security=true";

        public MainForm()
        {
            InitializeComponent();

            // Attach event handlers
            label_student.Click += Label_student_Click;
            label_cours.Click += Label_cours_Click;
            label_Enroll.Click += Label_Enroll_Click;

            button_add_new.Click += Button_add_new_Click;
            button_edit.Click += Button_edit_Click;
            button_Delete.Click += Button_Delete_Click;

            // Initialize UI
            SetActiveSection(label_student, Color.Blue);
            LoadStudentData();
        }

        #region UI Section Management
        private void SetActiveSection(Label activeLabel, Color color)
        {
            // Reset all labels to black
            label_student.ForeColor = Color.Black;
            label_cours.ForeColor = Color.Black;
            label_Enroll.ForeColor = Color.Black;

            // Set active label color
            activeLabel.ForeColor = color;

            // Set button colors to match section
            SetButtonColors(color);
        }

        private void SetButtonColors(Color color)
        {
            button_add_new.ForeColor = color;
            button_edit.ForeColor = color;
            button_Delete.ForeColor = color;
        }

        private void Label_student_Click(object sender, EventArgs e)
        {
            SetActiveSection(label_student, Color.Blue);
            UpdateButtonTexts("Student");
            LoadStudentData();
        }

        private void Label_cours_Click(object sender, EventArgs e)
        {
            SetActiveSection(label_cours, Color.Green);
            UpdateButtonTexts("Course");
            LoadCourseData();
        }

        private void Label_Enroll_Click(object sender, EventArgs e)
        {
            SetActiveSection(label_Enroll, Color.Gold);
            UpdateButtonTexts("Enrollment");
            LoadEnrollmentData();
        }

        private void UpdateButtonTexts(string section)
        {
            button_add_new.Text = $"Add New {section}";
            button_edit.Text = $"Edit {section}";
            button_Delete.Text = $"Delete {section}";
        }
        #endregion

        #region Database Operations
        private void LoadStudentData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT id, lastname, firstname, gender, age FROM students";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;
                    // Removed: dataGridView1.Columns["id"].Visible = false;
                    // Now the ID column will be visible
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading student data: {ex.Message}", "Database Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCourseData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT id, code, name FROM courses";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;
                    // Removed: dataGridView1.Columns["id"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading course data: {ex.Message}", "Database Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadEnrollmentData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT e.id AS enrollment_id, 
                                   s.id AS student_id, 
                                   c.id AS course_id,
                                   s.lastname + ', ' + s.firstname AS student, 
                                   c.code + ' - ' + c.name AS course
                            FROM enrollings e
                            JOIN students s ON e.studentid = s.id
                            JOIN courses c ON e.courseid = c.id";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;
                    // All columns will be visible by default
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading enrollment data: {ex.Message}", "Database Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Button Click Handlers
        private void Button_add_new_Click(object sender, EventArgs e)
        {
            try
            {
                if (label_student.ForeColor == Color.Blue)
                {
                    using (var form = new StudentForm())
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                            LoadStudentData();
                    }
                }
                else if (label_cours.ForeColor == Color.Green)
                {
                    using (var form = new CourseForm())
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                            LoadCourseData();
                    }
                }
                else if (label_Enroll.ForeColor == Color.Gold)
                {
                    using (var form = new EnrollForm())
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                            LoadEnrollmentData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding new record: {ex.Message}", "Operation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_edit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record to edit", "Selection Required",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Get the correct ID column name based on current section
                string idColumnName = "id"; // default for students and courses

                if (label_Enroll.ForeColor == Color.Gold)
                {
                    idColumnName = "enrollment_id";
                }

                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[idColumnName].Value);

                if (label_student.ForeColor == Color.Blue)
                {
                    using (var form = new StudentForm(id))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                            LoadStudentData();
                    }
                }
                else if (label_cours.ForeColor == Color.Green)
                {
                    using (var form = new CourseForm(id))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                            LoadCourseData();
                    }
                }
                else if (label_Enroll.ForeColor == Color.Gold)
                {
                    using (var form = new EnrollForm(id))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                            LoadEnrollmentData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing record: {ex.Message}", "Operation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_Delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record to delete", "Selection Required",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Get the correct ID column name based on current section
                string idColumnName = "id"; // default for students and courses

                if (label_Enroll.ForeColor == Color.Gold)
                {
                    idColumnName = "enrollment_id";
                }

                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[idColumnName].Value);

                // Confirmation dialog
                if (MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete",
                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd;

                    if (label_student.ForeColor == Color.Blue)
                    {
                        // First delete all enrollments for this student
                        cmd = new SqlCommand("DELETE FROM enrollings WHERE studentid = @id", conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();

                        // Then delete the student
                        cmd = new SqlCommand("DELETE FROM students WHERE id = @id", conn);
                    }
                    else if (label_cours.ForeColor == Color.Green)
                    {
                        // First delete all enrollments for this course
                        cmd = new SqlCommand("DELETE FROM enrollings WHERE courseid = @id", conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();

                        // Then delete the course
                        cmd = new SqlCommand("DELETE FROM courses WHERE id = @id", conn);
                    }
                    else
                    {
                        // For enrollments, simple delete
                        cmd = new SqlCommand("DELETE FROM enrollings WHERE id = @id", conn);
                    }

                    cmd.Parameters.AddWithValue("@id", id);
                    int affected = cmd.ExecuteNonQuery();

                    if (affected == 0)
                    {
                        MessageBox.Show("No records were deleted", "Information",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                // Refresh current view
                if (label_student.ForeColor == Color.Blue)
                    LoadStudentData();
                else if (label_cours.ForeColor == Color.Green)
                    LoadCourseData();
                else
                    LoadEnrollmentData();

                MessageBox.Show("Record deleted successfully", "Success",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting record: {ex.Message}", "Operation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}