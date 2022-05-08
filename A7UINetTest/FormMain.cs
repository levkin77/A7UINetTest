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

        private void ActionCheckColl_Click(object sender, EventArgs e)
        {
            string key = (sender as Button).Tag.ToString();

            A7UINet.WA wa = new A7UINet.WA();  
            wa.ConnectionString = GetCurrentCnnString();
            if (key == "pricelist")
            {
                var coll = wa.GetPriceLists();
                foreach (var item in coll)
                {
                    Console.WriteLine($"PriceList item: id {item.Id}| guid {item.Guid}| name {item.Name}| memo {item.Memo}| ismain {item.IsMain}|");
                }
            }
            else if (key == "pricename")
            {
                var coll = wa.GetPriceNames();
                foreach (var item in coll)
                {
                    Console.WriteLine($"PriceName item: id {item.Id}| guid {item.Guid}| name {item.Name}| memo {item.Memo}| formula {item.Formula}|");
                }
            }
            else if(key == "pricelistselector")
            {
                var coll = wa.GetPriceListSelector();
                foreach (var item in coll)
                {
                    Console.WriteLine($"PriceList id {item.Id} | name {item.Name}");
                    foreach (var child in item.Elements)
                    {
                        Console.WriteLine($"===PriceName id {child.Id} | name {child.Name}");
                    }
                    Console.WriteLine("-----------------------------------------");
                }
            }
        }

        private void btnShowTreeProjectitems_Click(object sender, EventArgs e)
        {
            A7UINet.ElementBrowserTreeProjectItems browserTree = new A7UINet.ElementBrowserTreeProjectItems();
            //browserTree.ShowOnlyFolder = chkOnlyFoldersTreeShow.Checked;
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

        private void btnShowAutonum_Click(object sender, EventArgs e)
        {
            A7UINet.ElementBrowserTreeAutonum browserTree = new A7UINet.ElementBrowserTreeAutonum();
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

        private void btnPriceListTree_Click(object sender, EventArgs e)
        {
            A7UINet.ElementBrowserTreePriceList browserTree = new A7UINet.ElementBrowserTreePriceList();
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

        private void btnShowPriceNameTree_Click(object sender, EventArgs e)
        {
            A7UINet.ElementBrowserTreePriceList browserTree = new A7UINet.ElementBrowserTreePriceList();
            browserTree.ViewDataKind = ElementBrowserTreePriceList.ViewData.PriceName;
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

        private void btnShowPrlListPriceNames_Click(object sender, EventArgs e)
        {
            A7UINet.ElementBrowserTreePriceList browserTree = new A7UINet.ElementBrowserTreePriceList();
            //browserTree.ViewDataKind = A7UINet.ElementBrowserTreePriceList.ViewData.PriceName;
            browserTree.ViewDataKind = A7UINet.ElementBrowserTreePriceList.ViewData.All;
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
