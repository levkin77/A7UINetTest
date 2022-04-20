
namespace A7UINetTest
{
    partial class FormMain
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
            this.btnShowFolders = new System.Windows.Forms.Button();
            this.btnShowProducts = new System.Windows.Forms.Button();
            this.cmbConnection = new System.Windows.Forms.ComboBox();
            this.btnShowAgents = new System.Windows.Forms.Button();
            this.btnShowMiscsTree = new System.Windows.Forms.Button();
            this.btnShowTemplates = new System.Windows.Forms.Button();
            this.btnShowBinders = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnShowFolders
            // 
            this.btnShowFolders.Location = new System.Drawing.Point(12, 54);
            this.btnShowFolders.Name = "btnShowFolders";
            this.btnShowFolders.Size = new System.Drawing.Size(75, 23);
            this.btnShowFolders.TabIndex = 0;
            this.btnShowFolders.Tag = "folder";
            this.btnShowFolders.Text = "ShowFolders";
            this.btnShowFolders.UseVisualStyleBackColor = true;
            this.btnShowFolders.Click += new System.EventHandler(this.ShowTreeBrowser_Click);
            // 
            // btnShowProducts
            // 
            this.btnShowProducts.Location = new System.Drawing.Point(12, 83);
            this.btnShowProducts.Name = "btnShowProducts";
            this.btnShowProducts.Size = new System.Drawing.Size(75, 23);
            this.btnShowProducts.TabIndex = 0;
            this.btnShowProducts.Tag = "product";
            this.btnShowProducts.Text = "ShowProducts";
            this.btnShowProducts.UseVisualStyleBackColor = true;
            this.btnShowProducts.Click += new System.EventHandler(this.ShowTreeBrowser_Click);
            // 
            // cmbConnection
            // 
            this.cmbConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConnection.FormattingEnabled = true;
            this.cmbConnection.Items.AddRange(new object[] {
            "DefaultLocal",
            "UseLocalDB"});
            this.cmbConnection.Location = new System.Drawing.Point(13, 13);
            this.cmbConnection.Name = "cmbConnection";
            this.cmbConnection.Size = new System.Drawing.Size(258, 21);
            this.cmbConnection.TabIndex = 1;
            // 
            // btnShowAgents
            // 
            this.btnShowAgents.Location = new System.Drawing.Point(12, 112);
            this.btnShowAgents.Name = "btnShowAgents";
            this.btnShowAgents.Size = new System.Drawing.Size(75, 23);
            this.btnShowAgents.TabIndex = 0;
            this.btnShowAgents.Tag = "agent";
            this.btnShowAgents.Text = "ShowAgents";
            this.btnShowAgents.UseVisualStyleBackColor = true;
            this.btnShowAgents.Click += new System.EventHandler(this.ShowTreeBrowser_Click);
            // 
            // btnShowMiscsTree
            // 
            this.btnShowMiscsTree.Location = new System.Drawing.Point(12, 141);
            this.btnShowMiscsTree.Name = "btnShowMiscsTree";
            this.btnShowMiscsTree.Size = new System.Drawing.Size(75, 23);
            this.btnShowMiscsTree.TabIndex = 0;
            this.btnShowMiscsTree.Tag = "misc";
            this.btnShowMiscsTree.Text = "ShowMiscs";
            this.btnShowMiscsTree.UseVisualStyleBackColor = true;
            this.btnShowMiscsTree.Click += new System.EventHandler(this.ShowTreeBrowser_Click);
            // 
            // btnShowTemplates
            // 
            this.btnShowTemplates.Location = new System.Drawing.Point(12, 170);
            this.btnShowTemplates.Name = "btnShowTemplates";
            this.btnShowTemplates.Size = new System.Drawing.Size(75, 23);
            this.btnShowTemplates.TabIndex = 0;
            this.btnShowTemplates.Tag = "template";
            this.btnShowTemplates.Text = "ShowTemplates";
            this.btnShowTemplates.UseVisualStyleBackColor = true;
            this.btnShowTemplates.Click += new System.EventHandler(this.ShowTreeBrowser_Click);
            // 
            // btnShowBinders
            // 
            this.btnShowBinders.Location = new System.Drawing.Point(12, 199);
            this.btnShowBinders.Name = "btnShowBinders";
            this.btnShowBinders.Size = new System.Drawing.Size(75, 23);
            this.btnShowBinders.TabIndex = 0;
            this.btnShowBinders.Tag = "binder";
            this.btnShowBinders.Text = "ShowBinders";
            this.btnShowBinders.UseVisualStyleBackColor = true;
            this.btnShowBinders.Click += new System.EventHandler(this.ShowTreeBrowser_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmbConnection);
            this.Controls.Add(this.btnShowBinders);
            this.Controls.Add(this.btnShowTemplates);
            this.Controls.Add(this.btnShowMiscsTree);
            this.Controls.Add(this.btnShowAgents);
            this.Controls.Add(this.btnShowProducts);
            this.Controls.Add(this.btnShowFolders);
            this.Name = "FormMain";
            this.Text = "Test A7UINet";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnShowFolders;
        private System.Windows.Forms.Button btnShowProducts;
        private System.Windows.Forms.ComboBox cmbConnection;
        private System.Windows.Forms.Button btnShowAgents;
        private System.Windows.Forms.Button btnShowMiscsTree;
        private System.Windows.Forms.Button btnShowTemplates;
        private System.Windows.Forms.Button btnShowBinders;
    }
}

