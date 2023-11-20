using System.Windows.Forms;

namespace DesktopApp.Forms
{
    partial class Meetings
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
            panel = new Panel();
            btnCreate = new Button();
            btnGoBack = new Button();
            lbxMeetings = new ListBox();
            meetingPanel = new Panel();
            rbPastMeetings = new RadioButton();
            rbUpcomingMeetings = new RadioButton();
            rbAllMeetings = new RadioButton();
            btnDelete = new Button();
            btnEdit = new Button();
            btnSearch = new Button();
            tbxEmail = new TextBox();
            lblSearch = new Label();
            panel.SuspendLayout();
            meetingPanel.SuspendLayout();
            SuspendLayout();
            // 
            // panel
            // 
            panel.BackColor = Color.DeepSkyBlue;
            panel.Controls.Add(btnCreate);
            panel.Controls.Add(btnGoBack);
            panel.Dock = DockStyle.Left;
            panel.Location = new Point(0, 0);
            panel.Name = "panel";
            panel.Size = new Size(214, 573);
            panel.TabIndex = 0;
            // 
            // btnCreate
            // 
            btnCreate.BackColor = Color.SteelBlue;
            btnCreate.BackgroundImageLayout = ImageLayout.Stretch;
            btnCreate.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            btnCreate.ForeColor = Color.White;
            btnCreate.Location = new Point(0, 66);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(214, 60);
            btnCreate.TabIndex = 2;
            btnCreate.Text = "Create";
            btnCreate.UseVisualStyleBackColor = false;
            btnCreate.Click += btnCreate_Click;
            // 
            // btnGoBack
            // 
            btnGoBack.BackColor = Color.SteelBlue;
            btnGoBack.BackgroundImage = Properties.Resources.back_arrow;
            btnGoBack.BackgroundImageLayout = ImageLayout.Stretch;
            btnGoBack.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            btnGoBack.ForeColor = Color.White;
            btnGoBack.Location = new Point(0, 0);
            btnGoBack.Name = "btnGoBack";
            btnGoBack.Size = new Size(214, 60);
            btnGoBack.TabIndex = 1;
            btnGoBack.UseVisualStyleBackColor = false;
            btnGoBack.Click += btnGoBack_Click;
            // 
            // lbxMeetings
            // 
            lbxMeetings.FormattingEnabled = true;
            lbxMeetings.ItemHeight = 25;
            lbxMeetings.Location = new Point(14, 147);
            lbxMeetings.Name = "lbxMeetings";
            lbxMeetings.Size = new Size(769, 354);
            lbxMeetings.TabIndex = 4;
            // 
            // meetingPanel
            // 
            meetingPanel.BackColor = Color.Gainsboro;
            meetingPanel.Controls.Add(rbPastMeetings);
            meetingPanel.Controls.Add(rbUpcomingMeetings);
            meetingPanel.Controls.Add(rbAllMeetings);
            meetingPanel.Controls.Add(btnDelete);
            meetingPanel.Controls.Add(btnEdit);
            meetingPanel.Controls.Add(btnSearch);
            meetingPanel.Controls.Add(tbxEmail);
            meetingPanel.Controls.Add(lblSearch);
            meetingPanel.Controls.Add(lbxMeetings);
            meetingPanel.Location = new Point(230, 12);
            meetingPanel.Name = "meetingPanel";
            meetingPanel.Size = new Size(799, 549);
            meetingPanel.TabIndex = 5;
            // 
            // rbPastMeetings
            // 
            rbPastMeetings.AutoSize = true;
            rbPastMeetings.Location = new Point(194, 75);
            rbPastMeetings.Name = "rbPastMeetings";
            rbPastMeetings.Size = new Size(69, 29);
            rbPastMeetings.TabIndex = 12;
            rbPastMeetings.TabStop = true;
            rbPastMeetings.Text = "Past";
            rbPastMeetings.UseVisualStyleBackColor = true;
            rbPastMeetings.CheckedChanged += rbPastMeetings_CheckedChanged;
            // 
            // rbUpcomingMeetings
            // 
            rbUpcomingMeetings.AutoSize = true;
            rbUpcomingMeetings.Location = new Point(77, 75);
            rbUpcomingMeetings.Name = "rbUpcomingMeetings";
            rbUpcomingMeetings.Size = new Size(120, 29);
            rbUpcomingMeetings.TabIndex = 11;
            rbUpcomingMeetings.TabStop = true;
            rbUpcomingMeetings.Text = "Upcoming";
            rbUpcomingMeetings.UseVisualStyleBackColor = true;
            rbUpcomingMeetings.CheckedChanged += rbUpcomingMeetings_CheckedChanged;
            // 
            // rbAllMeetings
            // 
            rbAllMeetings.AutoSize = true;
            rbAllMeetings.Location = new Point(14, 75);
            rbAllMeetings.Name = "rbAllMeetings";
            rbAllMeetings.Size = new Size(57, 29);
            rbAllMeetings.TabIndex = 10;
            rbAllMeetings.TabStop = true;
            rbAllMeetings.Text = "All";
            rbAllMeetings.UseVisualStyleBackColor = true;
            rbAllMeetings.CheckedChanged += rbAllMeetings_CheckedChanged;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.Crimson;
            btnDelete.Location = new Point(132, 507);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(112, 34);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.Goldenrod;
            btnEdit.Location = new Point(14, 507);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(112, 34);
            btnEdit.TabIndex = 8;
            btnEdit.Text = "Edit Info";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(14, 107);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(150, 34);
            btnSearch.TabIndex = 7;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // tbxEmail
            // 
            tbxEmail.Location = new Point(14, 38);
            tbxEmail.Name = "tbxEmail";
            tbxEmail.Size = new Size(150, 31);
            tbxEmail.TabIndex = 6;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(14, 10);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(136, 25);
            lblSearch.TabIndex = 5;
            lblSearch.Text = "Search by email";
            // 
            // Meetings
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1041, 573);
            Controls.Add(panel);
            Controls.Add(meetingPanel);
            Name = "Meetings";
            Text = "Meetings";
            Load += Sessions_Load;
            panel.ResumeLayout(false);
            meetingPanel.ResumeLayout(false);
            meetingPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel;
        private Button btnGoBack;
        private ListBox lbxMeetings;
        private Panel meetingPanel;
        private Button btnEdit;
        private Button btnSearch;
        private TextBox tbxEmail;
        private Label lblSearch;
        private Button btnCreate;
        private Button btnDelete;
        private RadioButton rbPastMeetings;
        private RadioButton rbUpcomingMeetings;
        private RadioButton rbAllMeetings;
    }
}