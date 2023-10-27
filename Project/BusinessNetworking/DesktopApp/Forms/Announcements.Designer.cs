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
            dgvAnnouncements = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            Title = new DataGridViewTextBoxColumn();
            CreatedBy = new DataGridViewTextBoxColumn();
            CreatedAt = new DataGridViewTextBoxColumn();
            UpdatedAt = new DataGridViewTextBoxColumn();
            Type = new DataGridViewComboBoxColumn();
            panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAnnouncements).BeginInit();
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
            // dgvAnnouncements
            // 
            dgvAnnouncements.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvAnnouncements.BackgroundColor = Color.Gainsboro;
            dgvAnnouncements.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAnnouncements.Columns.AddRange(new DataGridViewColumn[] { Id, Title, CreatedBy, CreatedAt, UpdatedAt, Type });
            dgvAnnouncements.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dgvAnnouncements.Location = new Point(228, 30);
            dgvAnnouncements.Name = "dgvAnnouncements";
            dgvAnnouncements.RowHeadersWidth = 62;
            dgvAnnouncements.RowTemplate.Height = 33;
            dgvAnnouncements.Size = new Size(964, 508);
            dgvAnnouncements.TabIndex = 5;
            // 
            // Id
            // 
            Id.HeaderText = "ID";
            Id.MinimumWidth = 8;
            Id.Name = "Id";
            Id.ReadOnly = true;
            Id.Width = 150;
            // 
            // Title
            // 
            Title.HeaderText = "Title";
            Title.MinimumWidth = 8;
            Title.Name = "Title";
            Title.ReadOnly = true;
            Title.Width = 150;
            // 
            // CreatedBy
            // 
            CreatedBy.HeaderText = "Created By";
            CreatedBy.MinimumWidth = 8;
            CreatedBy.Name = "CreatedBy";
            CreatedBy.ReadOnly = true;
            CreatedBy.Width = 150;
            // 
            // CreatedAt
            // 
            CreatedAt.HeaderText = "Created At";
            CreatedAt.MinimumWidth = 8;
            CreatedAt.Name = "CreatedAt";
            CreatedAt.ReadOnly = true;
            CreatedAt.Width = 150;
            // 
            // UpdatedAt
            // 
            UpdatedAt.HeaderText = "Updated At";
            UpdatedAt.MinimumWidth = 8;
            UpdatedAt.Name = "UpdatedAt";
            UpdatedAt.ReadOnly = true;
            UpdatedAt.Width = 150;
            // 
            // Type
            // 
            Type.HeaderText = "Type";
            Type.Items.AddRange(new object[] { "Maintenance", "Event", "General" });
            Type.MinimumWidth = 8;
            Type.Name = "Type";
            Type.ReadOnly = true;
            Type.Resizable = DataGridViewTriState.True;
            Type.SortMode = DataGridViewColumnSortMode.Automatic;
            Type.Width = 150;
            // 
            // Announcements
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(1204, 573);
            Controls.Add(dgvAnnouncements);
            Controls.Add(panel);
            Name = "Announcements";
            Load += Announcements_Load;
            panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAnnouncements).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel;
        private Button btnGoBack;
        private DataGridView dgvAnnouncements;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn Title;
        private DataGridViewTextBoxColumn CreatedBy;
        private DataGridViewTextBoxColumn CreatedAt;
        private DataGridViewTextBoxColumn UpdatedAt;
        private DataGridViewComboBoxColumn Type;
    }
}