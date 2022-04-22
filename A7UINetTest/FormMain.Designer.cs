
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
            this.btnShowTreeFolders = new System.Windows.Forms.Button();
            this.btnShowTreeProducts = new System.Windows.Forms.Button();
            this.cmbConnection = new System.Windows.Forms.ComboBox();
            this.btnShowTreeAgents = new System.Windows.Forms.Button();
            this.btnShowTreeMiscs = new System.Windows.Forms.Button();
            this.btnShowTreeTemplates = new System.Windows.Forms.Button();
            this.btnShowTreeBinders = new System.Windows.Forms.Button();
            this.chkOnlyFoldersTreeShow = new System.Windows.Forms.CheckBox();
            this.groupBoxTree = new System.Windows.Forms.GroupBox();
            this.btnShowTreeAccounts = new System.Windows.Forms.Button();
            this.groupBoxWA = new System.Windows.Forms.GroupBox();
            this.btnPriceList = new System.Windows.Forms.Button();
            this.PriceNameColl = new System.Windows.Forms.Button();
            this.groupBoxTree.SuspendLayout();
            this.groupBoxWA.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnShowTreeFolders
            // 
            this.btnShowTreeFolders.Location = new System.Drawing.Point(6, 42);
            this.btnShowTreeFolders.Name = "btnShowTreeFolders";
            this.btnShowTreeFolders.Size = new System.Drawing.Size(75, 23);
            this.btnShowTreeFolders.TabIndex = 0;
            this.btnShowTreeFolders.Tag = "folder";
            this.btnShowTreeFolders.Text = "ShowFolders";
            this.btnShowTreeFolders.UseVisualStyleBackColor = true;
            this.btnShowTreeFolders.Click += new System.EventHandler(this.ShowTreeBrowser_Click);
            // 
            // btnShowTreeProducts
            // 
            this.btnShowTreeProducts.Location = new System.Drawing.Point(87, 42);
            this.btnShowTreeProducts.Name = "btnShowTreeProducts";
            this.btnShowTreeProducts.Size = new System.Drawing.Size(75, 23);
            this.btnShowTreeProducts.TabIndex = 0;
            this.btnShowTreeProducts.Tag = "product";
            this.btnShowTreeProducts.Text = "ShowProducts";
            this.btnShowTreeProducts.UseVisualStyleBackColor = true;
            this.btnShowTreeProducts.Click += new System.EventHandler(this.ShowTreeBrowser_Click);
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
            // btnShowTreeAgents
            // 
            this.btnShowTreeAgents.Location = new System.Drawing.Point(6, 71);
            this.btnShowTreeAgents.Name = "btnShowTreeAgents";
            this.btnShowTreeAgents.Size = new System.Drawing.Size(75, 23);
            this.btnShowTreeAgents.TabIndex = 0;
            this.btnShowTreeAgents.Tag = "agent";
            this.btnShowTreeAgents.Text = "ShowAgents";
            this.btnShowTreeAgents.UseVisualStyleBackColor = true;
            this.btnShowTreeAgents.Click += new System.EventHandler(this.ShowTreeBrowser_Click);
            // 
            // btnShowTreeMiscs
            // 
            this.btnShowTreeMiscs.Location = new System.Drawing.Point(87, 71);
            this.btnShowTreeMiscs.Name = "btnShowTreeMiscs";
            this.btnShowTreeMiscs.Size = new System.Drawing.Size(75, 23);
            this.btnShowTreeMiscs.TabIndex = 0;
            this.btnShowTreeMiscs.Tag = "misc";
            this.btnShowTreeMiscs.Text = "ShowMiscs";
            this.btnShowTreeMiscs.UseVisualStyleBackColor = true;
            this.btnShowTreeMiscs.Click += new System.EventHandler(this.ShowTreeBrowser_Click);
            // 
            // btnShowTreeTemplates
            // 
            this.btnShowTreeTemplates.Location = new System.Drawing.Point(6, 100);
            this.btnShowTreeTemplates.Name = "btnShowTreeTemplates";
            this.btnShowTreeTemplates.Size = new System.Drawing.Size(75, 23);
            this.btnShowTreeTemplates.TabIndex = 0;
            this.btnShowTreeTemplates.Tag = "template";
            this.btnShowTreeTemplates.Text = "ShowTemplates";
            this.btnShowTreeTemplates.UseVisualStyleBackColor = true;
            this.btnShowTreeTemplates.Click += new System.EventHandler(this.ShowTreeBrowser_Click);
            // 
            // btnShowTreeBinders
            // 
            this.btnShowTreeBinders.Location = new System.Drawing.Point(87, 100);
            this.btnShowTreeBinders.Name = "btnShowTreeBinders";
            this.btnShowTreeBinders.Size = new System.Drawing.Size(75, 23);
            this.btnShowTreeBinders.TabIndex = 0;
            this.btnShowTreeBinders.Tag = "binder";
            this.btnShowTreeBinders.Text = "ShowBinders";
            this.btnShowTreeBinders.UseVisualStyleBackColor = true;
            this.btnShowTreeBinders.Click += new System.EventHandler(this.ShowTreeBrowser_Click);
            // 
            // chkOnlyFoldersTreeShow
            // 
            this.chkOnlyFoldersTreeShow.AutoSize = true;
            this.chkOnlyFoldersTreeShow.Location = new System.Drawing.Point(6, 19);
            this.chkOnlyFoldersTreeShow.Name = "chkOnlyFoldersTreeShow";
            this.chkOnlyFoldersTreeShow.Size = new System.Drawing.Size(160, 17);
            this.chkOnlyFoldersTreeShow.TabIndex = 2;
            this.chkOnlyFoldersTreeShow.Text = "Показывать только папки";
            this.chkOnlyFoldersTreeShow.UseVisualStyleBackColor = true;
            // 
            // groupBoxTree
            // 
            this.groupBoxTree.Controls.Add(this.chkOnlyFoldersTreeShow);
            this.groupBoxTree.Controls.Add(this.btnShowTreeFolders);
            this.groupBoxTree.Controls.Add(this.btnShowTreeBinders);
            this.groupBoxTree.Controls.Add(this.btnShowTreeProducts);
            this.groupBoxTree.Controls.Add(this.btnShowTreeAccounts);
            this.groupBoxTree.Controls.Add(this.btnShowTreeTemplates);
            this.groupBoxTree.Controls.Add(this.btnShowTreeAgents);
            this.groupBoxTree.Controls.Add(this.btnShowTreeMiscs);
            this.groupBoxTree.Location = new System.Drawing.Point(12, 40);
            this.groupBoxTree.Name = "groupBoxTree";
            this.groupBoxTree.Size = new System.Drawing.Size(184, 166);
            this.groupBoxTree.TabIndex = 3;
            this.groupBoxTree.TabStop = false;
            this.groupBoxTree.Text = "Выбор элемента из дерева";
            // 
            // btnShowTreeAccounts
            // 
            this.btnShowTreeAccounts.Location = new System.Drawing.Point(6, 129);
            this.btnShowTreeAccounts.Name = "btnShowTreeAccounts";
            this.btnShowTreeAccounts.Size = new System.Drawing.Size(75, 23);
            this.btnShowTreeAccounts.TabIndex = 0;
            this.btnShowTreeAccounts.Tag = "account";
            this.btnShowTreeAccounts.Text = "ShowAccounts";
            this.btnShowTreeAccounts.UseVisualStyleBackColor = true;
            this.btnShowTreeAccounts.Click += new System.EventHandler(this.ShowTreeBrowser_Click);
            // 
            // groupBoxWA
            // 
            this.groupBoxWA.Controls.Add(this.btnPriceList);
            this.groupBoxWA.Controls.Add(this.PriceNameColl);
            this.groupBoxWA.Location = new System.Drawing.Point(203, 41);
            this.groupBoxWA.Name = "groupBoxWA";
            this.groupBoxWA.Size = new System.Drawing.Size(200, 165);
            this.groupBoxWA.TabIndex = 4;
            this.groupBoxWA.TabStop = false;
            this.groupBoxWA.Text = "Проверка коллекций";
            // 
            // btnPriceList
            // 
            this.btnPriceList.Location = new System.Drawing.Point(7, 49);
            this.btnPriceList.Name = "btnPriceList";
            this.btnPriceList.Size = new System.Drawing.Size(84, 23);
            this.btnPriceList.TabIndex = 0;
            this.btnPriceList.Tag = "pricelist";
            this.btnPriceList.Text = "PriceListColl";
            this.btnPriceList.UseVisualStyleBackColor = true;
            this.btnPriceList.Click += new System.EventHandler(this.ActionCheckColl_Click);
            // 
            // PriceNameColl
            // 
            this.PriceNameColl.Location = new System.Drawing.Point(7, 20);
            this.PriceNameColl.Name = "PriceNameColl";
            this.PriceNameColl.Size = new System.Drawing.Size(84, 23);
            this.PriceNameColl.TabIndex = 0;
            this.PriceNameColl.Tag = "pricename";
            this.PriceNameColl.Text = "btnPriceName";
            this.PriceNameColl.UseVisualStyleBackColor = true;
            this.PriceNameColl.Click += new System.EventHandler(this.ActionCheckColl_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBoxWA);
            this.Controls.Add(this.groupBoxTree);
            this.Controls.Add(this.cmbConnection);
            this.Name = "FormMain";
            this.Text = "Test A7UINet";
            this.groupBoxTree.ResumeLayout(false);
            this.groupBoxTree.PerformLayout();
            this.groupBoxWA.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnShowTreeFolders;
        private System.Windows.Forms.Button btnShowTreeProducts;
        private System.Windows.Forms.ComboBox cmbConnection;
        private System.Windows.Forms.Button btnShowTreeAgents;
        private System.Windows.Forms.Button btnShowTreeMiscs;
        private System.Windows.Forms.Button btnShowTreeTemplates;
        private System.Windows.Forms.Button btnShowTreeBinders;
        private System.Windows.Forms.CheckBox chkOnlyFoldersTreeShow;
        private System.Windows.Forms.GroupBox groupBoxTree;
        private System.Windows.Forms.Button btnShowTreeAccounts;
        private System.Windows.Forms.GroupBox groupBoxWA;
        private System.Windows.Forms.Button btnPriceList;
        private System.Windows.Forms.Button PriceNameColl;
    }
}

