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
        // Проверка отображения папок
        private void showFolders_Click(object sender, EventArgs e)
        {
            A7UINet.ElementBrowserFolder browser = new A7UINet.ElementBrowserFolder();
            browser.ElementKind = A7UINet.ElementKinds.Folder;
            browser.ConnectionString = GetCurrentCnnString();
            DialogResult res = browser.ShowTree();
            if (res == DialogResult.OK)
            {
                //MessageBox.Show(res.ToString());
                MessageBox.Show(browser.SelectedElementId.ToString());
                MessageBox.Show(browser.SelectedElementName.ToString());
            }
        }
        // Проверка отображения товаров
        private void showProduct_Click(object sender, EventArgs e)
        {
            A7UINet.ElementBrowserFolder browser = new A7UINet.ElementBrowserFolder();
            browser.ElementKind = A7UINet.ElementKinds.Product;
            browser.ConnectionString = GetCurrentCnnString();
            DialogResult res = browser.ShowTree();
            if (res == DialogResult.OK)
            {
                //MessageBox.Show(res.ToString());
                MessageBox.Show($"Selected values SelectedElementId: {browser.SelectedElementId} SelectedElementName:{browser.SelectedElementName}");

                var val = browser.SelectedObject.GetType().GetProperty("Name").GetValue(browser.SelectedObject, null);
                MessageBox.Show($"Selected object {val}");

            }
        }

        private void showAgents_Click(object sender, EventArgs e)
        {
            A7UINet.ElementBrowserFolder browser = new A7UINet.ElementBrowserFolder();
            browser.ElementKind = A7UINet.ElementKinds.Agent;
            browser.ConnectionString = GetCurrentCnnString();
            DialogResult res = browser.ShowTree();
            if (res == DialogResult.OK)
            {
                //MessageBox.Show(res.ToString());
                MessageBox.Show($"Selected values SelectedElementId: {browser.SelectedElementId} SelectedElementName:{browser.SelectedElementName}");

                var val = browser.SelectedObject.GetType().GetProperty("Name").GetValue(browser.SelectedObject, null);
                MessageBox.Show($"Selected object {val}");

            }
        }

        private void ShowTreeBroser_Click(object sender, EventArgs e)
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
            }

            A7UINet.ElementBrowserFolder browser = new A7UINet.ElementBrowserFolder();
            browser.ElementKind = kind;
            browser.ConnectionString = GetCurrentCnnString();
            DialogResult res = browser.ShowTree();
            if (res == DialogResult.OK)
            {
                //MessageBox.Show(res.ToString());
                MessageBox.Show($"Selected values SelectedElementId: {browser.SelectedElementId} SelectedElementName:{browser.SelectedElementName}");

                if (browser.SelectedObject != null)
                {
                    var val = browser.SelectedObject.GetType().GetProperty("Name")
                        .GetValue(browser.SelectedObject, null);
                    MessageBox.Show($"Selected object {val}");
                }
            }
        }
    }
}
