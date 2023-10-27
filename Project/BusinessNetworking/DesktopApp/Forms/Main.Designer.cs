using System.Windows.Forms;

namespace DesktopApp.Forms
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel = new Panel();
            btnAnnouncements = new Button();
            btnMeetings = new Button();
            btnUsers = new Button();
            lblInfo = new Label();
            lbxAnnouncements = new ListBox();
            panel.SuspendLayout();
            SuspendLayout();
            // 
            // panel
            // 
            panel.BackColor = Color.DeepSkyBlue;
            panel.Controls.Add(btnAnnouncements);
            panel.Controls.Add(btnMeetings);
            panel.Controls.Add(btnUsers);
            panel.Dock = DockStyle.Left;
            panel.Location = new Point(0, 0);
            panel.Name = "panel";
            panel.Size = new Size(214, 573);
            panel.TabIndex = 0;
            // 
            // btnAnnouncements
            // 
            btnAnnouncements.BackColor = Color.SteelBlue;
            btnAnnouncements.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            btnAnnouncements.ForeColor = Color.White;
            btnAnnouncements.Location = new Point(0, 156);
            btnAnnouncements.Name = "btnAnnouncements";
            btnAnnouncements.Size = new Size(214, 56);
            btnAnnouncements.TabIndex = 3;
            btnAnnouncements.Text = "Announcements";
            btnAnnouncements.UseVisualStyleBackColor = false;
            btnAnnouncements.Click += btnAnnouncements_Click;
            // 
            // btnMeetings
            // 
            btnMeetings.BackColor = Color.SteelBlue;
            btnMeetings.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            btnMeetings.ForeColor = Color.White;
            btnMeetings.Location = new Point(0, 0);
            btnMeetings.Name = "btnMeetings";
            btnMeetings.Size = new Size(214, 60);
            btnMeetings.TabIndex = 1;
            btnMeetings.Text = "Meetings";
            btnMeetings.UseVisualStyleBackColor = false;
            btnMeetings.Click += btnMeetings_Click;
            // 
            // btnUsers
            // 
            btnUsers.BackColor = Color.SteelBlue;
            btnUsers.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            btnUsers.ForeColor = Color.White;
            btnUsers.Location = new Point(0, 66);
            btnUsers.Name = "btnUsers";
            btnUsers.Size = new Size(214, 84);
            btnUsers.TabIndex = 2;
            btnUsers.Text = "User Management";
            btnUsers.UseVisualStyleBackColor = false;
            btnUsers.Click += btnUsers_Click;
            // 
            // lblInfo
            // 
            lblInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblInfo.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            lblInfo.Location = new Point(220, 20);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(801, 60);
            lblInfo.TabIndex = 4;
            lblInfo.Text = "Welcome to Admin Panel";
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbxAnnouncements
            // 
            lbxAnnouncements.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lbxAnnouncements.ItemHeight = 25;
            lbxAnnouncements.Location = new Point(220, 100);
            lbxAnnouncements.Name = "lbxAnnouncements";
            lbxAnnouncements.Size = new Size(801, 379);
            lbxAnnouncements.TabIndex = 3;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(1041, 573);
            Controls.Add(panel);
            Controls.Add(lblInfo);
            Controls.Add(lbxAnnouncements);
            Name = "Main";
            Text = "MainForm";
            Load += Main_Load;
            panel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel;
        private Button btnMeetings;
        private Button btnUsers;
        private Label lblInfo;
        private Button btnAnnouncements;
        private ListBox lbxAnnouncements;
    }
}