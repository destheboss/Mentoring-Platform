namespace DesktopApp.Forms
{
    partial class Login
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
            loginPanel = new Panel();
            panel1 = new Panel();
            pbxImage = new PictureBox();
            lblLogin = new Label();
            btnLogin = new Button();
            tbUsername = new TextBox();
            tbPassword = new TextBox();
            loginPanel.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbxImage).BeginInit();
            SuspendLayout();
            // 
            // loginPanel
            // 
            loginPanel.BackColor = Color.DeepSkyBlue;
            loginPanel.Controls.Add(panel1);
            loginPanel.Dock = DockStyle.Fill;
            loginPanel.Location = new Point(0, 0);
            loginPanel.Margin = new Padding(5, 6, 5, 6);
            loginPanel.Name = "loginPanel";
            loginPanel.Size = new Size(1041, 573);
            loginPanel.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Gainsboro;
            panel1.Controls.Add(pbxImage);
            panel1.Controls.Add(lblLogin);
            panel1.Controls.Add(btnLogin);
            panel1.Controls.Add(tbUsername);
            panel1.Controls.Add(tbPassword);
            panel1.Location = new Point(172, 74);
            panel1.Name = "panel1";
            panel1.Size = new Size(683, 449);
            panel1.TabIndex = 5;
            // 
            // pbxImage
            // 
            pbxImage.Image = Properties.Resources.login;
            pbxImage.Location = new Point(256, 85);
            pbxImage.Name = "pbxImage";
            pbxImage.Size = new Size(200, 183);
            pbxImage.SizeMode = PictureBoxSizeMode.StretchImage;
            pbxImage.TabIndex = 5;
            pbxImage.TabStop = false;
            // 
            // lblLogin
            // 
            lblLogin.Anchor = AnchorStyles.Top;
            lblLogin.AutoSize = true;
            lblLogin.Font = new Font("Arial", 24F, FontStyle.Bold, GraphicsUnit.Point);
            lblLogin.Location = new Point(215, 11);
            lblLogin.Margin = new Padding(5, 0, 5, 0);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(281, 56);
            lblLogin.TabIndex = 0;
            lblLogin.Text = "Login Page";
            // 
            // btnLogin
            // 
            btnLogin.Anchor = AnchorStyles.Top;
            btnLogin.BackColor = Color.LightSkyBlue;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(256, 363);
            btnLogin.Margin = new Padding(5, 6, 5, 6);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(200, 40);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // tbUsername
            // 
            tbUsername.Anchor = AnchorStyles.Top;
            tbUsername.Location = new Point(256, 277);
            tbUsername.Margin = new Padding(5, 6, 5, 6);
            tbUsername.Name = "tbUsername";
            tbUsername.PlaceholderText = "Email";
            tbUsername.Size = new Size(200, 31);
            tbUsername.TabIndex = 2;
            // 
            // tbPassword
            // 
            tbPassword.Anchor = AnchorStyles.Top;
            tbPassword.Location = new Point(256, 320);
            tbPassword.Margin = new Padding(5, 6, 5, 6);
            tbPassword.Name = "tbPassword";
            tbPassword.PasswordChar = '*';
            tbPassword.PlaceholderText = "Password";
            tbPassword.Size = new Size(200, 31);
            tbPassword.TabIndex = 3;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(1041, 573);
            Controls.Add(loginPanel);
            Margin = new Padding(5, 6, 5, 6);
            Name = "Login";
            Text = "LoginForm";
            loginPanel.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbxImage).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel loginPanel;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button btnLogin;
        private Panel panel1;
        private PictureBox pbxImage;
    }
}

#endregion