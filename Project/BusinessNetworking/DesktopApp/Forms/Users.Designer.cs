using System.Windows.Forms;

namespace DesktopApp.Forms
{
    partial class Users
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            mainPanel = new Panel();
            tabControl = new TabControl();
            createUserTab = new TabPage();
            btnSubmit = new Button();
            lblRoles = new Label();
            cbxRoles = new ComboBox();
            tbxPassword = new TextBox();
            tbxEmail = new TextBox();
            tbxLastName = new TextBox();
            tbxFirstName = new TextBox();
            lblPassword = new Label();
            lblEmail = new Label();
            lblLastName = new Label();
            lblFirstName = new Label();
            viewUsersTab = new TabPage();
            btnRemove = new Button();
            btnActivate = new Button();
            btnEdit = new Button();
            btnSuspendPerson = new Button();
            cbxSearch = new ComboBox();
            tbxSearch = new TextBox();
            lbxUsers = new ListBox();
            btnUpdate = new Button();
            panel.SuspendLayout();
            mainPanel.SuspendLayout();
            tabControl.SuspendLayout();
            createUserTab.SuspendLayout();
            viewUsersTab.SuspendLayout();
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
            // mainPanel
            // 
            mainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainPanel.Controls.Add(tabControl);
            mainPanel.Location = new Point(230, 12);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(799, 549);
            mainPanel.TabIndex = 0;
            // 
            // tabControl
            // 
            tabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl.Controls.Add(createUserTab);
            tabControl.Controls.Add(viewUsersTab);
            tabControl.Location = new Point(18, 19);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(767, 517);
            tabControl.TabIndex = 0;
            // 
            // createUserTab
            // 
            createUserTab.BackColor = Color.Gainsboro;
            createUserTab.Controls.Add(btnSubmit);
            createUserTab.Controls.Add(lblRoles);
            createUserTab.Controls.Add(cbxRoles);
            createUserTab.Controls.Add(tbxPassword);
            createUserTab.Controls.Add(tbxEmail);
            createUserTab.Controls.Add(tbxLastName);
            createUserTab.Controls.Add(tbxFirstName);
            createUserTab.Controls.Add(lblPassword);
            createUserTab.Controls.Add(lblEmail);
            createUserTab.Controls.Add(lblLastName);
            createUserTab.Controls.Add(lblFirstName);
            createUserTab.Location = new Point(4, 34);
            createUserTab.Name = "createUserTab";
            createUserTab.Size = new Size(759, 479);
            createUserTab.TabIndex = 0;
            createUserTab.Text = "Create User";
            // 
            // btnSubmit
            // 
            btnSubmit.BackColor = Color.DarkOliveGreen;
            btnSubmit.Font = new Font("Arial", 14F, FontStyle.Regular, GraphicsUnit.Point);
            btnSubmit.ForeColor = SystemColors.ButtonFace;
            btnSubmit.Location = new Point(233, 323);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(200, 60);
            btnSubmit.TabIndex = 10;
            btnSubmit.Text = "SUBMIT";
            btnSubmit.UseVisualStyleBackColor = false;
            btnSubmit.Click += btnSubmit_Click;
            // 
            // lblRoles
            // 
            lblRoles.AutoSize = true;
            lblRoles.Font = new Font("Arial", 16F, FontStyle.Regular, GraphicsUnit.Point);
            lblRoles.Location = new Point(120, 262);
            lblRoles.Name = "lblRoles";
            lblRoles.Size = new Size(104, 36);
            lblRoles.TabIndex = 9;
            lblRoles.Text = "Roles:";
            // 
            // cbxRoles
            // 
            cbxRoles.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxRoles.FormattingEnabled = true;
            cbxRoles.Location = new Point(292, 268);
            cbxRoles.Name = "cbxRoles";
            cbxRoles.Size = new Size(200, 33);
            cbxRoles.TabIndex = 8;
            // 
            // tbxPassword
            // 
            tbxPassword.Location = new Point(292, 211);
            tbxPassword.Name = "tbxPassword";
            tbxPassword.Size = new Size(200, 31);
            tbxPassword.TabIndex = 7;
            // 
            // tbxEmail
            // 
            tbxEmail.Location = new Point(292, 158);
            tbxEmail.Name = "tbxEmail";
            tbxEmail.Size = new Size(200, 31);
            tbxEmail.TabIndex = 5;
            // 
            // tbxLastName
            // 
            tbxLastName.Location = new Point(292, 108);
            tbxLastName.Name = "tbxLastName";
            tbxLastName.Size = new Size(200, 31);
            tbxLastName.TabIndex = 3;
            // 
            // tbxFirstName
            // 
            tbxFirstName.Location = new Point(292, 58);
            tbxFirstName.Name = "tbxFirstName";
            tbxFirstName.Size = new Size(200, 31);
            tbxFirstName.TabIndex = 1;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Arial", 16F, FontStyle.Regular, GraphicsUnit.Point);
            lblPassword.Location = new Point(120, 206);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(162, 36);
            lblPassword.TabIndex = 6;
            lblPassword.Text = "Password:";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Arial", 16F, FontStyle.Regular, GraphicsUnit.Point);
            lblEmail.Location = new Point(120, 153);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(103, 36);
            lblEmail.TabIndex = 4;
            lblEmail.Text = "Email:";
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.Font = new Font("Arial", 16F, FontStyle.Regular, GraphicsUnit.Point);
            lblLastName.Location = new Point(120, 103);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(172, 36);
            lblLastName.TabIndex = 2;
            lblLastName.Text = "Last name:";
            // 
            // lblFirstName
            // 
            lblFirstName.AutoSize = true;
            lblFirstName.Font = new Font("Arial", 16F, FontStyle.Regular, GraphicsUnit.Point);
            lblFirstName.Location = new Point(120, 53);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(175, 36);
            lblFirstName.TabIndex = 0;
            lblFirstName.Text = "First name:";
            // 
            // viewUsersTab
            // 
            viewUsersTab.BackColor = Color.DeepSkyBlue;
            viewUsersTab.Controls.Add(btnUpdate);
            viewUsersTab.Controls.Add(btnRemove);
            viewUsersTab.Controls.Add(btnActivate);
            viewUsersTab.Controls.Add(btnEdit);
            viewUsersTab.Controls.Add(btnSuspendPerson);
            viewUsersTab.Controls.Add(cbxSearch);
            viewUsersTab.Controls.Add(tbxSearch);
            viewUsersTab.Controls.Add(lbxUsers);
            viewUsersTab.Location = new Point(4, 34);
            viewUsersTab.Name = "viewUsersTab";
            viewUsersTab.Size = new Size(759, 479);
            viewUsersTab.TabIndex = 1;
            viewUsersTab.Text = "View Users";
            // 
            // btnRemove
            // 
            btnRemove.BackColor = Color.Crimson;
            btnRemove.Location = new Point(367, 412);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(112, 34);
            btnRemove.TabIndex = 5;
            btnRemove.Text = "Remove";
            btnRemove.UseVisualStyleBackColor = false;
            btnRemove.Click += btnRemove_Click;
            // 
            // btnActivate
            // 
            btnActivate.BackColor = Color.LimeGreen;
            btnActivate.Location = new Point(249, 412);
            btnActivate.Name = "btnActivate";
            btnActivate.Size = new Size(112, 34);
            btnActivate.TabIndex = 4;
            btnActivate.Text = "Activate";
            btnActivate.UseVisualStyleBackColor = false;
            btnActivate.Click += btnActivate_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.Goldenrod;
            btnEdit.Location = new Point(131, 412);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(112, 34);
            btnEdit.TabIndex = 3;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnSuspendPerson
            // 
            btnSuspendPerson.BackColor = Color.Crimson;
            btnSuspendPerson.Location = new Point(13, 412);
            btnSuspendPerson.Name = "btnSuspendPerson";
            btnSuspendPerson.Size = new Size(112, 34);
            btnSuspendPerson.TabIndex = 2;
            btnSuspendPerson.Text = "Suspend";
            btnSuspendPerson.UseVisualStyleBackColor = false;
            btnSuspendPerson.Click += btnSuspendPerson_Click;
            // 
            // cbxSearch
            // 
            cbxSearch.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxSearch.FormattingEnabled = true;
            cbxSearch.Location = new Point(13, 55);
            cbxSearch.Name = "cbxSearch";
            cbxSearch.Size = new Size(150, 33);
            cbxSearch.TabIndex = 1;
            cbxSearch.SelectedIndexChanged += cbxSearch_SelectedIndexChanged;
            // 
            // tbxSearch
            // 
            tbxSearch.Location = new Point(13, 18);
            tbxSearch.Name = "tbxSearch";
            tbxSearch.Size = new Size(150, 31);
            tbxSearch.TabIndex = 1;
            tbxSearch.TextChanged += tbxSearch_TextChanged;
            // 
            // lbxUsers
            // 
            lbxUsers.FormattingEnabled = true;
            lbxUsers.ItemHeight = 25;
            lbxUsers.Location = new Point(13, 102);
            lbxUsers.Name = "lbxUsers";
            lbxUsers.Size = new Size(734, 304);
            lbxUsers.TabIndex = 1;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.LimeGreen;
            btnUpdate.Location = new Point(485, 412);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(151, 34);
            btnUpdate.TabIndex = 6;
            btnUpdate.Text = "Update Ratings";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // Users
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(1041, 573);
            Controls.Add(mainPanel);
            Controls.Add(panel);
            Name = "Users";
            Text = "Users";
            Load += Users_Load;
            panel.ResumeLayout(false);
            mainPanel.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            createUserTab.ResumeLayout(false);
            createUserTab.PerformLayout();
            viewUsersTab.ResumeLayout(false);
            viewUsersTab.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel;
        private Button btnGoBack;
        private Panel mainPanel;
        private TabControl tabControl;
        private TabPage createUserTab;
        private Button btnSubmit;
        private Label lblRoles;
        private ComboBox cbxRoles;
        private TextBox tbxPassword;
        private TextBox tbxEmail;
        private TextBox tbxLastName;
        private TextBox tbxFirstName;
        private Label lblPassword;
        private Label lblEmail;
        private Label lblLastName;
        private Label lblFirstName;
        private TabPage viewUsersTab;
        private ComboBox cbxSearch;
        private TextBox tbxSearch;
        private ListBox lbxUsers;
        private Button btnSuspendPerson;
        private Button btnEdit;
        private Button btnActivate;
        private Button btnRemove;
        private Button btnUpdate;
    }
}