namespace WindowsFormsApp1
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_student = new System.Windows.Forms.Label();
            this.label_cours = new System.Windows.Forms.Label();
            this.label_Enroll = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button_add_new = new System.Windows.Forms.Button();
            this.button_edit = new System.Windows.Forms.Button();
            this.button_Delete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label_student
            // 
            this.label_student.AutoSize = true;
            this.label_student.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_student.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_student.Location = new System.Drawing.Point(48, 28);
            this.label_student.Name = "label_student";
            this.label_student.Size = new System.Drawing.Size(65, 18);
            this.label_student.TabIndex = 0;
            this.label_student.Text = "Student";
            // 
            // label_cours
            // 
            this.label_cours.AutoSize = true;
            this.label_cours.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_cours.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_cours.Location = new System.Drawing.Point(178, 28);
            this.label_cours.Name = "label_cours";
            this.label_cours.Size = new System.Drawing.Size(63, 18);
            this.label_cours.TabIndex = 1;
            this.label_cours.Text = "Course";
            // 
            // label_Enroll
            // 
            this.label_Enroll.AutoSize = true;
            this.label_Enroll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_Enroll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Enroll.Location = new System.Drawing.Point(286, 28);
            this.label_Enroll.Name = "label_Enroll";
            this.label_Enroll.Size = new System.Drawing.Size(89, 18);
            this.label_Enroll.TabIndex = 2;
            this.label_Enroll.Text = "Enrollment";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(42, 133);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(850, 228);
            this.dataGridView1.TabIndex = 3;
            // 
            // button_add_new
            // 
            this.button_add_new.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button_add_new.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_add_new.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_add_new.ForeColor = System.Drawing.Color.Black;
            this.button_add_new.Location = new System.Drawing.Point(51, 76);
            this.button_add_new.Name = "button_add_new";
            this.button_add_new.Size = new System.Drawing.Size(100, 30);
            this.button_add_new.TabIndex = 4;
            this.button_add_new.Text = "Add New";
            this.button_add_new.UseVisualStyleBackColor = false;
            // 
            // button_edit
            // 
            this.button_edit.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button_edit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_edit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_edit.Location = new System.Drawing.Point(181, 76);
            this.button_edit.Name = "button_edit";
            this.button_edit.Size = new System.Drawing.Size(100, 30);
            this.button_edit.TabIndex = 5;
            this.button_edit.Text = "Edit";
            this.button_edit.UseVisualStyleBackColor = false;
            // 
            // button_Delete
            // 
            this.button_Delete.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button_Delete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Delete.Location = new System.Drawing.Point(311, 76);
            this.button_Delete.Name = "button_Delete";
            this.button_Delete.Size = new System.Drawing.Size(100, 30);
            this.button_Delete.TabIndex = 6;
            this.button_Delete.Text = "Delete";
            this.button_Delete.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 450);
            this.Controls.Add(this.button_Delete);
            this.Controls.Add(this.button_edit);
            this.Controls.Add(this.button_add_new);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label_Enroll);
            this.Controls.Add(this.label_cours);
            this.Controls.Add(this.label_student);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Student Enrollment System";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_student;
        private System.Windows.Forms.Label label_cours;
        private System.Windows.Forms.Label label_Enroll;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_add_new;
        private System.Windows.Forms.Button button_edit;
        private System.Windows.Forms.Button button_Delete;
    }
}