using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    public partial class CourseForm : Form
    {
        private string connectionString = @"Data source=DESKTOP-S99S529\SQLEXPRESS;Initial Catalog=YourDatabaseName;Integrated Security=true";
        private int? courseId;

        public CourseForm()
        {
            InitializeComponent();
            this.Text = "Add New Course";
        }

        public CourseForm(int id) : this()
        {
            courseId = id;
            this.Text = "Edit Course";
            LoadCourseData(id);
        }

        private void LoadCourseData(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT code, name FROM courses WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtCode.Text = reader["code"].ToString();
                            txtName.Text = reader["name"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading course data: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCode.Text) ||
                string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter both course code and name", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd;

                    if (courseId.HasValue)
                    {
                        // Update existing course
                        cmd = new SqlCommand(
                            "UPDATE courses SET code = @code, name = @name WHERE id = @id", conn);
                        cmd.Parameters.AddWithValue("@id", courseId.Value);
                    }
                    else
                    {
                        // Insert new course
                        cmd = new SqlCommand(
                            "INSERT INTO courses (code, name) VALUES (@code, @name)", conn);
                    }

                    cmd.Parameters.AddWithValue("@code", txtCode.Text);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);

                    cmd.ExecuteNonQuery();
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving course: {ex.Message}", "Error",
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