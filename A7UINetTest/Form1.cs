using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A7UINetTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // Проверка отображения папок
        private void showFolders_Click(object sender, EventArgs e)
        {
            A7UINet.ElementBrowserFolder browser = new A7UINet.ElementBrowserFolder();
            browser.ElementKind = A7UINet.ElementKinds.Folder;
            browser.ConnectionString = "Integrated Security=True;Initial Catalog=Донецк7_рубль;Data Source=.";
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
            browser.ConnectionString = "Integrated Security=True;Initial Catalog=Донецк7_рубль;Data Source=.";
            DialogResult res = browser.ShowTree();
            if (res == DialogResult.OK)
            {
                //MessageBox.Show(res.ToString());
                MessageBox.Show($"Selected values SelectedElementId: {browser.SelectedElementId} SelectedElementName:{browser.SelectedElementName}");

                var val = browser.SelectedObject.GetType().GetProperty("Name").GetValue(browser.SelectedObject, null);
                MessageBox.Show($"Selected object {val}");

            }
        }
    }
}
