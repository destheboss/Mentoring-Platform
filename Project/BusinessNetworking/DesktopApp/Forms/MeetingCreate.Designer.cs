namespace DesktopApp.Forms
{
    partial class MeetingCreate
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
            panel1 = new Panel();
            lblDate = new Label();
            dtpDate = new DateTimePicker();
            lblMentee = new Label();
            lblMentor = new Label();
            tbxMenteeEmail = new TextBox();
            tbxMentorEmail = new TextBox();
            btnCancel = new Button();
            btnCreate = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Gainsboro;
            panel1.Controls.Add(lblDate);
            panel1.Controls.Add(dtpDate);
            panel1.Controls.Add(lblMentee);
            panel1.Controls.Add(lblMentor);
            panel1.Controls.Add(tbxMenteeEmail);
            panel1.Controls.Add(tbxMentorEmail);
            panel1.Controls.Add(btnCancel);
            panel1.Controls.Add(btnCreate);
            panel1.Location = new Point(116, 40);
            panel1.Name = "panel1";
            panel1.Size = new Size(825, 443);
            panel1.TabIndex = 0;
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Location = new Point(242, 233);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(53, 25);
            lblDate.TabIndex = 18;
            lblDate.Text = "Date:";
            // 
            // dtpDate
            // 
            dtpDate.Location = new Point(369, 233);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(318, 31);
            dtpDate.TabIndex = 17;
            // 
            // lblMentee
            // 
            lblMentee.AutoSize = true;
            lblMentee.Location = new Point(242, 168);
            lblMentee.Name = "lblMentee";
            lblMentee.Size = new Size(122, 25);
            lblMentee.TabIndex = 16;
            lblMentee.Text = "Mentee email:";
            // 
            // lblMentor
            // 
            lblMentor.AutoSize = true;
            lblMentor.Location = new Point(242, 106);
            lblMentor.Name = "lblMentor";
            lblMentor.Size = new Size(121, 25);
            lblMentor.TabIndex = 15;
            lblMentor.Text = "Mentor email:";
            // 
            // tbxMenteeEmail
            // 
            tbxMenteeEmail.Location = new Point(369, 165);
            tbxMenteeEmail.Name = "tbxMenteeEmail";
            tbxMenteeEmail.Size = new Size(150, 31);
            tbxMenteeEmail.TabIndex = 14;
            // 
            // tbxMentorEmail
            // 
            tbxMentorEmail.Location = new Point(369, 103);
            tbxMentorEmail.Name = "tbxMentorEmail";
            tbxMentorEmail.Size = new Size(150, 31);
            tbxMentorEmail.TabIndex = 13;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Crimson;
            btnCancel.ForeColor = SystemColors.ButtonFace;
            btnCancel.Location = new Point(441, 362);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(145, 63);
            btnCancel.TabIndex = 12;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnCreate
            // 
            btnCreate.BackColor = Color.DarkOliveGreen;
            btnCreate.ForeColor = SystemColors.ButtonFace;
            btnCreate.Location = new Point(218, 362);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(145, 63);
            btnCreate.TabIndex = 11;
            btnCreate.Text = "Create";
            btnCreate.UseVisualStyleBackColor = false;
            btnCreate.Click += btnConfirm_Click;
            // 
            // MeetingCreate
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DeepSkyBlue;
            ClientSize = new Size(1041, 573);
            Controls.Add(panel1);
            Name = "MeetingCreate";
            Text = "MeetingCreate";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btnCancel;
        private Button btnCreate;
        private Label lblDate;
        private DateTimePicker dtpDate;
        private Label lblMentee;
        private Label lblMentor;
        private TextBox tbxMenteeEmail;
        private TextBox tbxMentorEmail;
    }
}