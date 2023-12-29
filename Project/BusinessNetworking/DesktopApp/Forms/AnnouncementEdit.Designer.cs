namespace DesktopApp.Forms
{
    partial class AnnouncementEdit
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
            lblCreatedBy = new Label();
            lblTitle = new Label();
            lblType = new Label();
            tbMessage = new TextBox();
            btnCancel = new Button();
            btnConfirm = new Button();
            usersPanel.SuspendLayout();
            SuspendLayout();
            // 
            // usersPanel
            // 
            usersPanel.BackColor = SystemColors.Control;
            usersPanel.Controls.Add(lblCreatedBy);
            usersPanel.Controls.Add(lblTitle);
            usersPanel.Controls.Add(lblType);
            usersPanel.Controls.Add(tbMessage);
            usersPanel.Controls.Add(btnCancel);
            usersPanel.Controls.Add(btnConfirm);
            usersPanel.Location = new Point(48, 39);
            usersPanel.Name = "usersPanel";
            usersPanel.Size = new Size(937, 513);
            usersPanel.TabIndex = 2;
            // 
            // lblCreatedBy
            // 
            lblCreatedBy.AutoSize = true;
            lblCreatedBy.Location = new Point(78, 46);
            lblCreatedBy.Name = "lblCreatedBy";
            lblCreatedBy.Size = new Size(102, 25);
            lblCreatedBy.TabIndex = 17;
            lblCreatedBy.Text = "Created by:";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(78, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(48, 25);
            lblTitle.TabIndex = 16;
            lblTitle.Text = "Title:";
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Location = new Point(78, 86);
            lblType.Name = "lblType";
            lblType.Size = new Size(53, 25);
            lblType.TabIndex = 15;
            lblType.Text = "Type:";
            // 
            // tbMessage
            // 
            tbMessage.Location = new Point(78, 137);
            tbMessage.Multiline = true;
            tbMessage.Name = "tbMessage";
            tbMessage.PlaceholderText = "Enter message here...";
            tbMessage.Size = new Size(666, 282);
            tbMessage.TabIndex = 13;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Crimson;
            btnCancel.ForeColor = SystemColors.ButtonFace;
            btnCancel.Location = new Point(435, 434);
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
            btnConfirm.Location = new Point(236, 434);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(145, 63);
            btnConfirm.TabIndex = 9;
            btnConfirm.Text = "Confirm Changes";
            btnConfirm.UseVisualStyleBackColor = false;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // AnnouncementEdit
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DeepSkyBlue;
            ClientSize = new Size(1041, 573);
            Controls.Add(usersPanel);
            Name = "AnnouncementEdit";
            Text = "AnnouncementEdit";
            Load += AnnouncementEdit_Load;
            usersPanel.ResumeLayout(false);
            usersPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel usersPanel;
        private Button btnCancel;
        private Button btnConfirm;
        private Label lblType;
        private TextBox tbMessage;
        private Label lblTitle;
        private Label lblCreatedBy;
    }
}