using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using A7UINet;

namespace A7UINetTest
{
    // Главная форма с проверочными методами
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            cmbConnection.SelectedIndex = 0;
        }

        string GetCurrentCnnString()
        {
            if(cmbConnection.SelectedIndex==0)
                return "Integrated Security=True;Initial Catalog=Донецк7_рубль;Data Source=.";
            else
            {
                return @"Integrated Security=True;Initial Catalog=Донецк7_рубль;Data Source=(LocalDb)\MSSQLLocalDB";
            }
        }
        
        // Проверка отображения дерева элементов
        private void ShowTreeBrowser_Click(object sender, EventArgs e)
        {
            string key = (sender as Button).Tag.ToString();
            ElementKinds kind = A7UINet.ElementKinds.Agent;
            switch (key)
            {
                case "folder":
                    kind = ElementKinds.Folder;
                    break;
                case "product":
                    kind = ElementKinds.Product;
                    break;
                case "agent":
                    kind = ElementKinds.Agent;
                    break;
                case "misc": 
                    kind = ElementKinds.Misc;
                    break;
                case "template":
                    kind = ElementKinds.Template;
                    break;
                case "binder":
                    kind = ElementKinds.Binder;
                    break;
                case "account":
                    kind = ElementKinds.Account;
                    break;
            }

            A7UINet.ElementBrowserTree browserTree = new A7UINet.ElementBrowserTree();
            browserTree.ElementKind = kind;
            browserTree.ShowOnlyFolder = chkOnlyFoldersTreeShow.Checked;
            browserTree.ConnectionString = GetCurrentCnnString();
            DialogResult res = browserTree.ShowTree();
            if (res == DialogResult.OK)
            {
                //MessageBox.Show(res.ToString());
                MessageBox.Show($"Selected values SelectedElementId: {browserTree.SelectedElementId} SelectedElementName:{browserTree.SelectedElementName}");

                if (browserTree.SelectedObject != null)
                {
                    var val = browserTree.SelectedObject.GetType().GetProperty("Name")
                        .GetValue(browserTree.SelectedObject, null);
                    MessageBox.Show($"Selected object {val}");
                }
            }
        }
    }
}
