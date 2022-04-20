using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A7UINet
{
    public enum ElementKinds
    {
        Folder,
        Product,
        Agent,
        Misc,
        Binder,
        Template
    }
    internal class ElementValue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Kind { get; set; }
        public int Id2 { get; set; }
        public int Child { get; set; }
    }
    public class ElementBrowserFolder
    {
        public ElementKinds ElementKind { get; set; }
        public int SelectedElementId { get; set; }
        public string SelectedElementName { get; set; }
        public dynamic SelectedObject { get; private set; }

        public string ConnectionString { get; set; }
        public DialogResult ShowTree()
        {
            FormTreeFolder frm = new FormTreeFolder();

            if (ElementKind == ElementKinds.Folder)
            {
                frm.treeView.ImageList = GreateImageListTo(ElementKind);
                frm.treeView.SelectedImageIndex = 0;
            }
            else if (ElementKind == ElementKinds.Product)
            {
                frm.treeView.ImageList = GreateImageListTo(ElementKind);
                //frm.treeView.SelectedImageIndex = 0;
            }

            FillNodes(frm.treeView, null);
            frm.treeView.BeforeExpand += delegate(object sender, TreeViewCancelEventArgs eAgrExpand)
            {
                if (eAgrExpand.Node != null)
                {
                    if (eAgrExpand.Node.Nodes.Count == 1 && eAgrExpand.Node.Nodes[0].Text == "empty")
                    {
                        FillNodes(frm.treeView, eAgrExpand.Node);
                    }
                }
            };//TreeView_BeforeExpand;
            var res = frm.ShowDialog();
            SelectedElementId = 0;
            SelectedElementName = String.Empty;
            if (frm.treeView.SelectedNode != null)
            {
                var objSelected = frm.treeView.SelectedNode.Tag as ElementValue;
                SelectedElementId = objSelected.Id;
                SelectedElementName = objSelected.Name;
                SelectedObject = objSelected;
            }
            return res;
        }
        

        private void FillNodes(TreeView view, TreeNode nodeTo)
        {
            //root
            if (nodeTo == null)
            {
                view.Nodes.Clear();
                var values = GetLevel(0);
                foreach (var element in values.OrderBy(s => s.Name))
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = element.Name;
                    newNode.Tag = element;
                    view.Nodes.Add(newNode);
                    if (ElementKind == ElementKinds.Product)
                    {
                        newNode.ImageIndex = element.Kind < 1000 ? element.Kind : element.Kind - 1000;
                        newNode.SelectedImageIndex = newNode.ImageIndex;
                    }
                    if(element.Child>0)
                        newNode.Nodes.Add("empty");
                }
            }
            else
            {
                nodeTo.Nodes.Clear();
                ElementValue currentElementValue = nodeTo.Tag as ElementValue;
                var values = GetLevel(currentElementValue.Id);
                foreach (var element in values.OrderBy(s=>s.Kind).ThenBy(s=>s.Name))
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = element.Name;
                    newNode.Tag = element;
                    nodeTo.Nodes.Add(newNode);
                    if (ElementKind == ElementKinds.Product)
                    {
                        newNode.ImageIndex = element.Kind < 1000 ? element.Kind : element.Kind - 1000;
                        newNode.SelectedImageIndex = newNode.ImageIndex;
                    }
                    if (element.Child > 0)
                        newNode.Nodes.Add("empty");
                }
            }
        }
        
        private List<ElementValue> GetLevel(int parentId)
        {
            List<ElementValue> coll = new List<ElementValue>();
            using (var cnn  =new SqlConnection(ConnectionString))
            {
                using (var cmd = cnn.CreateCommand())
                {
                    if (ElementKind == ElementKinds.Folder)
                    {
                        if (parentId == 0)
                        {
                            cmd.CommandText =
                                ";with a as \n"
                                + "( \n"
                                + "select cast(v.FLD_ID as int) as Id, v.FLD_NAME as Name \n"
                                + "from dbo.FOLDERS v inner join dbo.FLD_TREE t on t.ID = v.FLD_ID where t.SHORTCUT = 0 and (t.P0 is null or t.p0 = 0)   \n"
                                + ") \n"
                                + "select a.Id,a.Name, (select count(*) from dbo.FLD_TREE where p0=a.Id) as HasChild from a order by a.name";

                        }
                        else
                        {
                            cmd.CommandText =
                                ";with a as \n"
                                + "( \n"
                                + "select cast(v.FLD_ID as int) as Id, v.FLD_NAME as Name \n"
                                + $"from dbo.FOLDERS v inner join dbo.FLD_TREE t on t.ID = v.FLD_ID where t.SHORTCUT = 0 and t.p0 = {parentId}   \n"
                                + ") \n"
                                + "select a.Id,a.Name, (select count(*) from dbo.FLD_TREE where p0=a.Id) as HasChild from a order by a.name";
                            //$"select cast(v.FLD_ID as int) as Id, v.FLD_NAME as Name  from dbo.FOLDERS v inner join dbo.FLD_TREE t on t.ID = v.FLD_ID where t.SHORTCUT = 0 and t.p0 = {parentId}  order by v.FLD_NAME;";
                        }
                    }
                    else if (ElementKind == ElementKinds.Product)
                    {
                        if (parentId == 0)
                        {
                            cmd.CommandText =
                                ";with a as \n"
                                + "( \n"
                                + "select cast(v.ENT_ID as int) as Id, v.ENT_NAME as Name, v.ENT_TYPE as Kind \n"
                                + "from dbo.ENTITIES v inner join dbo.ENT_TREE t on t.ID = v.ENT_ID where t.SHORTCUT = 0 and (t.P0 is null or t.p0 = 0)   \n"
                                + ") \n"
                                + "select a.Id,a.Name, (select count(*) from dbo.ENT_TREE where p0=a.Id) as HasChild, a.Kind from a order by a.name";

                        }
                        else
                        {
                            cmd.CommandText =
                                ";with a as \n"
                                + "( \n"
                                + "select cast(v.ENT_ID as int) as Id, v.ENT_NAME as Name, v.ENT_TYPE as Kind \n"
                                + $"from dbo.ENTITIES v inner join dbo.ENT_TREE t on t.ID = v.ENT_ID where t.SHORTCUT = 0 and t.p0 = {parentId}   \n"
                                + ") \n"
                                + "select a.Id,a.Name, (select count(*) from dbo.ENT_TREE where p0=a.Id) as HasChild, a.Kind from a order by a.name";
                        }
                    }
                    cmd.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        ElementValue v = new ElementValue();
                        v.Id = reader.GetInt32(0);
                        v.Name = reader.GetString(1);
                        v.Child = reader.GetInt32(2);
                        if (ElementKind == ElementKinds.Product)
                        {
                            v.Kind = reader.GetInt16(3);
                        }
                        coll.Add(v);
                    }
                }
            }

            return coll;
        }

        private ImageList GreateImageListTo(ElementKinds kind)
        {
            ImageList list = new ImageList();
            if (kind == ElementKinds.Folder)
            {
                list.Images.Add(Properties.Resources.FOLDERYELOW16);
            }
            else if (kind == ElementKinds.Product)
            {
                list.Images.Add(Properties.Resources.entity_icon0);
                list.Images.Add(Properties.Resources.entity_icon1);
                list.Images.Add(Properties.Resources.entity_icon1001);
                list.Images.Add(Properties.Resources.entity_icon1002);
                list.Images.Add(Properties.Resources.entity_icon1003);
                list.Images.Add(Properties.Resources.entity_icon1004);
                list.Images.Add(Properties.Resources.entity_icon1005);
                list.Images.Add(Properties.Resources.entity_icon1006);
                list.Images.Add(Properties.Resources.entity_icon1007);
                list.Images.Add(Properties.Resources.entity_icon1008);
                list.Images.Add(Properties.Resources.entity_icon1009);
                list.Images.Add(Properties.Resources.entity_icon1010);
                list.Images.Add(Properties.Resources.entity_icon1011);
                list.Images.Add(Properties.Resources.entity_icon1012);
                list.Images.Add(Properties.Resources.entity_icon1013);
                list.Images.Add(Properties.Resources.entity_icon1014);
                list.Images.Add(Properties.Resources.entity_icon1015);
                list.Images.Add(Properties.Resources.entity_icon1016);
                list.Images.Add(Properties.Resources.entity_icon1017);
                list.Images.Add(Properties.Resources.entity_icon1018);
            }
            return list;
        }
    }
}
