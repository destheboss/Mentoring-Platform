namespace DesktopApp.Forms
{
    partial class MeetingEdit
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
            nudRating = new NumericUpDown();
            dtpDate = new DateTimePicker();
            btnCancel = new Button();
            btnConfirm = new Button();
            lblNewDate = new Label();
            lblNewRating = new Label();
            usersPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudRating).BeginInit();
            SuspendLayout();
            // 
            // usersPanel
            // 
            usersPanel.BackColor = SystemColors.Control;
            usersPanel.Controls.Add(nudRating);
            usersPanel.Controls.Add(dtpDate);
            usersPanel.Controls.Add(btnCancel);
            usersPanel.Controls.Add(btnConfirm);
            usersPanel.Controls.Add(lblNewDate);
            usersPanel.Controls.Add(lblNewRating);
            usersPanel.Location = new Point(114, 71);
            usersPanel.Name = "usersPanel";
            usersPanel.Size = new Size(813, 430);
            usersPanel.TabIndex = 1;
            // 
            // nudRating
            // 
            nudRating.Location = new Point(392, 105);
            nudRating.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            nudRating.Name = "nudRating";
            nudRating.ReadOnly = true;
            nudRating.Size = new Size(180, 31);
            nudRating.TabIndex = 12;
            // 
            // dtpDate
            // 
            dtpDate.Location = new Point(387, 163);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(300, 31);
            dtpDate.TabIndex = 11;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Crimson;
            btnCancel.ForeColor = SystemColors.ButtonFace;
            btnCancel.Location = new Point(392, 353);
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
            btnConfirm.Location = new Point(240, 353);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(145, 63);
            btnConfirm.TabIndex = 9;
            btnConfirm.Text = "Confirm Changes";
            btnConfirm.UseVisualStyleBackColor = false;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // lblNewDate
            // 
            lblNewDate.AutoSize = true;
            lblNewDate.Location = new Point(240, 169);
            lblNewDate.Name = "lblNewDate";
            lblNewDate.Size = new Size(93, 25);
            lblNewDate.TabIndex = 7;
            lblNewDate.Text = "New Date:";
            // 
            // lblNewRating
            // 
            lblNewRating.AutoSize = true;
            lblNewRating.Location = new Point(240, 111);
            lblNewRating.Name = "lblNewRating";
            lblNewRating.Size = new Size(107, 25);
            lblNewRating.TabIndex = 0;
            lblNewRating.Text = "New Rating:";
            // 
            // MeetingEdit
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DeepSkyBlue;
            ClientSize = new Size(1041, 573);
            Controls.Add(usersPanel);
            Name = "MeetingEdit";
            Text = "MeetingEdit";
            Load += MeetingEdit_Load;
            usersPanel.ResumeLayout(false);
            usersPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudRating).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel usersPanel;
        private Button btnCancel;
        private Button btnConfirm;
        private Label lblNewDate;
        private Label lblNewRating;
        private DateTimePicker dtpDate;
        private NumericUpDown nudRating;
    }
}