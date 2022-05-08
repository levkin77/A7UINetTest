
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
            this.btnPriceListTree = new System.Windows.Forms.Button();
            this.btnShowAutonum = new System.Windows.Forms.Button();
            this.btnShowTreeProjectitems = new System.Windows.Forms.Button();
            this.btnShowTreeAccounts = new System.Windows.Forms.Button();
            this.groupBoxWA = new System.Windows.Forms.GroupBox();
            this.btnPricelistSelector = new System.Windows.Forms.Button();
            this.btnPriceList = new System.Windows.Forms.Button();
            this.PriceNameColl = new System.Windows.Forms.Button();
            this.btnShowPriceNameTree = new System.Windows.Forms.Button();
            this.btnShowPrlListPriceNames = new System.Windows.Forms.Button();
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
            this.groupBoxTree.Controls.Add(this.btnShowPrlListPriceNames);
            this.groupBoxTree.Controls.Add(this.btnShowPriceNameTree);
            this.groupBoxTree.Controls.Add(this.btnPriceListTree);
            this.groupBoxTree.Controls.Add(this.btnShowAutonum);
            this.groupBoxTree.Controls.Add(this.btnShowTreeProjectitems);
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
            this.groupBoxTree.Size = new System.Drawing.Size(184, 288);
            this.groupBoxTree.TabIndex = 3;
            this.groupBoxTree.TabStop = false;
            this.groupBoxTree.Text = "Выбор элемента из дерева";
            // 
            // btnPriceListTree
            // 
            this.btnPriceListTree.Location = new System.Drawing.Point(87, 187);
            this.btnPriceListTree.Name = "btnPriceListTree";
            this.btnPriceListTree.Size = new System.Drawing.Size(75, 23);
            this.btnPriceListTree.TabIndex = 4;
            this.btnPriceListTree.Tag = "binder";
            this.btnPriceListTree.Text = "ShowPriceList";
            this.btnPriceListTree.UseVisualStyleBackColor = true;
            this.btnPriceListTree.Click += new System.EventHandler(this.btnPriceListTree_Click);
            // 
            // btnShowAutonum
            // 
            this.btnShowAutonum.Location = new System.Drawing.Point(87, 158);
            this.btnShowAutonum.Name = "btnShowAutonum";
            this.btnShowAutonum.Size = new System.Drawing.Size(75, 23);
            this.btnShowAutonum.TabIndex = 4;
            this.btnShowAutonum.Tag = "binder";
            this.btnShowAutonum.Text = "ShowAutonum";
            this.btnShowAutonum.UseVisualStyleBackColor = true;
            this.btnShowAutonum.Click += new System.EventHandler(this.btnShowAutonum_Click);
            // 
            // btnShowTreeProjectitems
            // 
            this.btnShowTreeProjectitems.Location = new System.Drawing.Point(87, 129);
            this.btnShowTreeProjectitems.Name = "btnShowTreeProjectitems";
            this.btnShowTreeProjectitems.Size = new System.Drawing.Size(75, 23);
            this.btnShowTreeProjectitems.TabIndex = 3;
            this.btnShowTreeProjectitems.Tag = "binder";
            this.btnShowTreeProjectitems.Text = "ShowProjectItems";
            this.btnShowTreeProjectitems.UseVisualStyleBackColor = true;
            this.btnShowTreeProjectitems.Click += new System.EventHandler(this.btnShowTreeProjectitems_Click);
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
            this.groupBoxWA.Controls.Add(this.btnPricelistSelector);
            this.groupBoxWA.Controls.Add(this.btnPriceList);
            this.groupBoxWA.Controls.Add(this.PriceNameColl);
            this.groupBoxWA.Location = new System.Drawing.Point(203, 41);
            this.groupBoxWA.Name = "groupBoxWA";
            this.groupBoxWA.Size = new System.Drawing.Size(200, 165);
            this.groupBoxWA.TabIndex = 4;
            this.groupBoxWA.TabStop = false;
            this.groupBoxWA.Text = "Проверка коллекций";
            // 
            // btnPricelistSelector
            // 
            this.btnPricelistSelector.Location = new System.Drawing.Point(7, 78);
            this.btnPricelistSelector.Name = "btnPricelistSelector";
            this.btnPricelistSelector.Size = new System.Drawing.Size(84, 23);
            this.btnPricelistSelector.TabIndex = 1;
            this.btnPricelistSelector.Tag = "pricelistselector";
            this.btnPricelistSelector.Text = "PriceListColl";
            this.btnPricelistSelector.UseVisualStyleBackColor = true;
            this.btnPricelistSelector.Click += new System.EventHandler(this.ActionCheckColl_Click);
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
            // btnShowPriceNameTree
            // 
            this.btnShowPriceNameTree.Location = new System.Drawing.Point(87, 216);
            this.btnShowPriceNameTree.Name = "btnShowPriceNameTree";
            this.btnShowPriceNameTree.Size = new System.Drawing.Size(75, 23);
            this.btnShowPriceNameTree.TabIndex = 4;
            this.btnShowPriceNameTree.Tag = "binder";
            this.btnShowPriceNameTree.Text = "ShowPriceNames";
            this.btnShowPriceNameTree.UseVisualStyleBackColor = true;
            this.btnShowPriceNameTree.Click += new System.EventHandler(this.btnShowPriceNameTree_Click);
            // 
            // btnShowPrlListPriceNames
            // 
            this.btnShowPrlListPriceNames.Location = new System.Drawing.Point(87, 245);
            this.btnShowPrlListPriceNames.Name = "btnShowPrlListPriceNames";
            this.btnShowPrlListPriceNames.Size = new System.Drawing.Size(75, 23);
            this.btnShowPrlListPriceNames.TabIndex = 4;
            this.btnShowPrlListPriceNames.Tag = "binder";
            this.btnShowPrlListPriceNames.Text = "ShowPrlListPriceNames";
            this.btnShowPrlListPriceNames.UseVisualStyleBackColor = true;
            this.btnShowPrlListPriceNames.Click += new System.EventHandler(this.btnShowPrlListPriceNames_Click);
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
        private System.Windows.Forms.Button btnPricelistSelector;
        private System.Windows.Forms.Button btnShowTreeProjectitems;
        private System.Windows.Forms.Button btnShowAutonum;
        private System.Windows.Forms.Button btnPriceListTree;
        private System.Windows.Forms.Button btnShowPriceNameTree;
        private System.Windows.Forms.Button btnShowPrlListPriceNames;
    }
}

