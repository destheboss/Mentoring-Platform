namespace DesktopApp.Forms
{
    partial class UsersEdit
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
            usersPanel = new Panel();
            btnClearSpecialties = new Button();
            btnAdd = new Button();
            cbxSpecialty = new ComboBox();
            lblMentorSpecs = new Label();
            lblSpecialty = new Label();
            tbxNewEmail = new TextBox();
            lblNewEmail = new Label();
            btnCancel = new Button();
            btnConfirm = new Button();
            tbxNewLastName = new TextBox();
            lblNewLastName = new Label();
            tbxNewName = new TextBox();
            tbxNewPassword = new TextBox();
            lblNewPassword = new Label();
            lblNewFirstName = new Label();
            usersPanel.SuspendLayout();
            SuspendLayout();
            // 
            // usersPanel
            // 
            usersPanel.BackColor = SystemColors.Control;
            usersPanel.Controls.Add(btnClearSpecialties);
            usersPanel.Controls.Add(btnAdd);
            usersPanel.Controls.Add(cbxSpecialty);
            usersPanel.Controls.Add(lblMentorSpecs);
            usersPanel.Controls.Add(lblSpecialty);
            usersPanel.Controls.Add(tbxNewEmail);
            usersPanel.Controls.Add(lblNewEmail);
            usersPanel.Controls.Add(btnCancel);
            usersPanel.Controls.Add(btnConfirm);
            usersPanel.Controls.Add(tbxNewLastName);
            usersPanel.Controls.Add(lblNewLastName);
            usersPanel.Controls.Add(tbxNewName);
            usersPanel.Controls.Add(tbxNewPassword);
            usersPanel.Controls.Add(lblNewPassword);
            usersPanel.Controls.Add(lblNewFirstName);
            usersPanel.Location = new Point(119, 67);
            usersPanel.Name = "usersPanel";
            usersPanel.Size = new Size(813, 430);
            usersPanel.TabIndex = 0;
            // 
            // btnClearSpecialties
            // 
            btnClearSpecialties.BackColor = Color.Crimson;
            btnClearSpecialties.ForeColor = SystemColors.ButtonFace;
            btnClearSpecialties.Location = new Point(674, 126);
            btnClearSpecialties.Name = "btnClearSpecialties";
            btnClearSpecialties.Size = new Size(100, 38);
            btnClearSpecialties.TabIndex = 18;
            btnClearSpecialties.Text = "Clear All";
            btnClearSpecialties.UseVisualStyleBackColor = false;
            btnClearSpecialties.Click += btnClearSpecialties_Click;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.DarkOliveGreen;
            btnAdd.ForeColor = SystemColors.ButtonFace;
            btnAdd.Location = new Point(568, 126);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(100, 38);
            btnAdd.TabIndex = 17;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // cbxSpecialty
            // 
            cbxSpecialty.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxSpecialty.FormattingEnabled = true;
            cbxSpecialty.Location = new Point(590, 87);
            cbxSpecialty.Name = "cbxSpecialty";
            cbxSpecialty.Size = new Size(150, 33);
            cbxSpecialty.TabIndex = 16;
            // 
            // lblMentorSpecs
            // 
            lblMentorSpecs.AutoSize = true;
            lblMentorSpecs.Location = new Point(489, 32);
            lblMentorSpecs.Name = "lblMentorSpecs";
            lblMentorSpecs.Size = new Size(137, 25);
            lblMentorSpecs.TabIndex = 15;
            lblMentorSpecs.Text = "Mentor specific:";
            // 
            // lblSpecialty
            // 
            lblSpecialty.AutoSize = true;
            lblSpecialty.Location = new Point(489, 90);
            lblSpecialty.Name = "lblSpecialty";
            lblSpecialty.Size = new Size(86, 25);
            lblSpecialty.TabIndex = 14;
            lblSpecialty.Text = "Specialty:";
            // 
            // tbxNewEmail
            // 
            tbxNewEmail.Location = new Point(202, 154);
            tbxNewEmail.Name = "tbxNewEmail";
            tbxNewEmail.Size = new Size(150, 31);
            tbxNewEmail.TabIndex = 12;
            // 
            // lblNewEmail
            // 
            lblNewEmail.AutoSize = true;
            lblNewEmail.Location = new Point(57, 154);
            lblNewEmail.Name = "lblNewEmail";
            lblNewEmail.Size = new Size(98, 25);
            lblNewEmail.TabIndex = 11;
            lblNewEmail.Text = "New Email:";
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Crimson;
            btnCancel.ForeColor = SystemColors.ButtonFace;
            btnCancel.Location = new Point(425, 353);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(145, 63);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnConfirm
            // 
            btnConfirm.BackColor = Color.DarkOliveGreen;
            btnConfirm.ForeColor = SystemColors.ButtonFace;
            btnConfirm.Location = new Point(202, 353);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(145, 63);
            btnConfirm.TabIndex = 9;
            btnConfirm.Text = "Confirm Changes";
            btnConfirm.UseVisualStyleBackColor = false;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // tbxNewLastName
            // 
            tbxNewLastName.Location = new Point(202, 90);
            tbxNewLastName.Name = "tbxNewLastName";
            tbxNewLastName.Size = new Size(150, 31);
            tbxNewLastName.TabIndex = 8;
            // 
            // lblNewLastName
            // 
            lblNewLastName.AutoSize = true;
            lblNewLastName.Location = new Point(55, 90);
            lblNewLastName.Name = "lblNewLastName";
            lblNewLastName.Size = new Size(139, 25);
            lblNewLastName.TabIndex = 7;
            lblNewLastName.Text = "New Last Name:";
            // 
            // tbxNewName
            // 
            tbxNewName.Location = new Point(202, 29);
            tbxNewName.Name = "tbxNewName";
            tbxNewName.Size = new Size(150, 31);
            tbxNewName.TabIndex = 6;
            // 
            // tbxNewPassword
            // 
            tbxNewPassword.Location = new Point(202, 220);
            tbxNewPassword.Name = "tbxNewPassword";
            tbxNewPassword.Size = new Size(150, 31);
            tbxNewPassword.TabIndex = 5;
            // 
            // lblNewPassword
            // 
            lblNewPassword.AutoSize = true;
            lblNewPassword.Location = new Point(57, 220);
            lblNewPassword.Name = "lblNewPassword";
            lblNewPassword.Size = new Size(131, 25);
            lblNewPassword.TabIndex = 1;
            lblNewPassword.Text = "New Password:";
            // 
            // lblNewFirstName
            // 
            lblNewFirstName.AutoSize = true;
            lblNewFirstName.Location = new Point(55, 32);
            lblNewFirstName.Name = "lblNewFirstName";
            lblNewFirstName.Size = new Size(141, 25);
            lblNewFirstName.TabIndex = 0;
            lblNewFirstName.Text = "New First Name:";
            // 
            // UsersEdit
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DeepSkyBlue;
            ClientSize = new Size(1041, 573);
            Controls.Add(usersPanel);
            Name = "UsersEdit";
            Text = "UsersEdit";
            Load += UsersEdit_Load;
            usersPanel.ResumeLayout(false);
            usersPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel usersPanel;
        private Label lblNewPassword;
        private Label lblNewFirstName;
        private Button btnCancel;
        private Button btnConfirm;
        private TextBox tbxNewLastName;
        private Label lblNewLastName;
        private TextBox tbxNewName;
        private TextBox tbxNewPassword;
        private Label lblNewEmail;
        private TextBox tbxNewEmail;
        private ComboBox cbxSpecialty;
        private Label lblMentorSpecs;
        private Label lblSpecialty;
        private Button btnAdd;
        private Button btnClearSpecialties;
    }
}