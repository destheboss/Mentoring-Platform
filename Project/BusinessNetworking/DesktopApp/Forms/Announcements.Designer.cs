using System.Windows.Forms;

namespace DesktopApp.Forms
{
    partial class Announcements
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
            btnGoBack = new Button();
            meetingPanel = new Panel();
            tabControl = new TabControl();
            createAnnouncementTab = new TabPage();
            btnSubmit = new Button();
            label1 = new Label();
            cbxType = new ComboBox();
            tbMessage = new TextBox();
            tbTitle = new TextBox();
            viewAnnouncementsTab = new TabPage();
            rbOldest = new RadioButton();
            rbNewest = new RadioButton();
            btnRead = new Button();
            btnEdit = new Button();
            btnRemove = new Button();
            lbxAnnouncements = new ListBox();
            panel.SuspendLayout();
            meetingPanel.SuspendLayout();
            tabControl.SuspendLayout();
            createAnnouncementTab.SuspendLayout();
            viewAnnouncementsTab.SuspendLayout();
            SuspendLayout();
            // 
            // panel
            // 
            panel.BackColor = Color.DeepSkyBlue;
            panel.Controls.Add(btnGoBack);
            panel.Dock = DockStyle.Left;
            panel.Location = new Point(0, 0);
            panel.Name = "panel";
            panel.Size = new Size(214, 573);
            panel.TabIndex = 0;
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
            // meetingPanel
            // 
            meetingPanel.BackColor = Color.Gainsboro;
            meetingPanel.Controls.Add(tabControl);
            meetingPanel.Location = new Point(230, 12);
            meetingPanel.Name = "meetingPanel";
            meetingPanel.Size = new Size(799, 549);
            meetingPanel.TabIndex = 6;
            // 
            // tabControl
            // 
            tabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl.Controls.Add(createAnnouncementTab);
            tabControl.Controls.Add(viewAnnouncementsTab);
            tabControl.Location = new Point(16, 16);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(767, 517);
            tabControl.TabIndex = 1;
            // 
            // createAnnouncementTab
            // 
            createAnnouncementTab.BackColor = Color.Gainsboro;
            createAnnouncementTab.Controls.Add(btnSubmit);
            createAnnouncementTab.Controls.Add(label1);
            createAnnouncementTab.Controls.Add(cbxType);
            createAnnouncementTab.Controls.Add(tbMessage);
            createAnnouncementTab.Controls.Add(tbTitle);
            createAnnouncementTab.Location = new Point(4, 34);
            createAnnouncementTab.Name = "createAnnouncementTab";
            createAnnouncementTab.Size = new Size(759, 479);
            createAnnouncementTab.TabIndex = 0;
            createAnnouncementTab.Text = "Create";
            // 
            // btnSubmit
            // 
            btnSubmit.BackColor = Color.DarkOliveGreen;
            btnSubmit.Font = new Font("Arial", 14F, FontStyle.Regular, GraphicsUnit.Point);
            btnSubmit.ForeColor = SystemColors.ButtonFace;
            btnSubmit.Location = new Point(257, 401);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(200, 60);
            btnSubmit.TabIndex = 11;
            btnSubmit.Text = "CREATE";
            btnSubmit.UseVisualStyleBackColor = false;
            btnSubmit.Click += btnSubmit_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 72);
            label1.Name = "label1";
            label1.Size = new Size(53, 25);
            label1.TabIndex = 3;
            label1.Text = "Type:";
            // 
            // cbxType
            // 
            cbxType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxType.FormattingEnabled = true;
            cbxType.Location = new Point(61, 69);
            cbxType.Name = "cbxType";
            cbxType.Size = new Size(182, 33);
            cbxType.TabIndex = 2;
            // 
            // tbMessage
            // 
            tbMessage.Location = new Point(61, 113);
            tbMessage.Multiline = true;
            tbMessage.Name = "tbMessage";
            tbMessage.PlaceholderText = "Enter message here...";
            tbMessage.Size = new Size(666, 282);
            tbMessage.TabIndex = 1;
            // 
            // tbTitle
            // 
            tbTitle.Location = new Point(61, 16);
            tbTitle.Name = "tbTitle";
            tbTitle.PlaceholderText = "Title";
            tbTitle.Size = new Size(322, 31);
            tbTitle.TabIndex = 0;
            // 
            // viewAnnouncementsTab
            // 
            viewAnnouncementsTab.BackColor = Color.DeepSkyBlue;
            viewAnnouncementsTab.Controls.Add(rbOldest);
            viewAnnouncementsTab.Controls.Add(rbNewest);
            viewAnnouncementsTab.Controls.Add(btnRead);
            viewAnnouncementsTab.Controls.Add(btnEdit);
            viewAnnouncementsTab.Controls.Add(btnRemove);
            viewAnnouncementsTab.Controls.Add(lbxAnnouncements);
            viewAnnouncementsTab.Location = new Point(4, 34);
            viewAnnouncementsTab.Name = "viewAnnouncementsTab";
            viewAnnouncementsTab.Size = new Size(759, 479);
            viewAnnouncementsTab.TabIndex = 1;
            viewAnnouncementsTab.Text = "View Announcements";
            // 
            // rbOldest
            // 
            rbOldest.AutoSize = true;
            rbOldest.Location = new Point(135, 14);
            rbOldest.Name = "rbOldest";
            rbOldest.Size = new Size(89, 29);
            rbOldest.TabIndex = 10;
            rbOldest.TabStop = true;
            rbOldest.Text = "Oldest";
            rbOldest.UseVisualStyleBackColor = true;
            rbOldest.CheckedChanged += rbOldest_CheckedChanged;
            // 
            // rbNewest
            // 
            rbNewest.AutoSize = true;
            rbNewest.Location = new Point(17, 14);
            rbNewest.Name = "rbNewest";
            rbNewest.Size = new Size(95, 29);
            rbNewest.TabIndex = 9;
            rbNewest.TabStop = true;
            rbNewest.Text = "Newest";
            rbNewest.UseVisualStyleBackColor = true;
            rbNewest.CheckedChanged += rbNewest_CheckedChanged;
            // 
            // btnRead
            // 
            btnRead.BackColor = Color.LimeGreen;
            btnRead.Location = new Point(17, 437);
            btnRead.Name = "btnRead";
            btnRead.Size = new Size(112, 34);
            btnRead.TabIndex = 8;
            btnRead.Text = "Read Message";
            btnRead.UseVisualStyleBackColor = false;
            btnRead.Click += btnRead_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.Goldenrod;
            btnEdit.Location = new Point(135, 437);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(112, 34);
            btnEdit.TabIndex = 7;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnRemove
            // 
            btnRemove.BackColor = Color.Crimson;
            btnRemove.Location = new Point(253, 437);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(112, 34);
            btnRemove.TabIndex = 6;
            btnRemove.Text = "Remove";
            btnRemove.UseVisualStyleBackColor = false;
            btnRemove.Click += btnRemove_Click;
            // 
            // lbxAnnouncements
            // 
            lbxAnnouncements.FormattingEnabled = true;
            lbxAnnouncements.ItemHeight = 25;
            lbxAnnouncements.Location = new Point(17, 52);
            lbxAnnouncements.Name = "lbxAnnouncements";
            lbxAnnouncements.Size = new Size(722, 379);
            lbxAnnouncements.TabIndex = 0;
            // 
            // Announcements
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(1041, 573);
            Controls.Add(meetingPanel);
            Controls.Add(panel);
            Name = "Announcements";
            Load += Announcements_Load;
            panel.ResumeLayout(false);
            meetingPanel.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            createAnnouncementTab.ResumeLayout(false);
            createAnnouncementTab.PerformLayout();
            viewAnnouncementsTab.ResumeLayout(false);
            viewAnnouncementsTab.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel;
        private Button btnGoBack;
        private Panel meetingPanel;
        private TabControl tabControl;
        private TabPage createAnnouncementTab;
        private TextBox tbTitle;
        private TabPage viewAnnouncementsTab;
        private TextBox tbMessage;
        private Label label1;
        private ComboBox cbxType;
        private Button btnSubmit;
        private ListBox lbxAnnouncements;
        private Button btnRemove;
        private Button btnEdit;
        private Button btnRead;
        private RadioButton rbOldest;
        private RadioButton rbNewest;
    }
}