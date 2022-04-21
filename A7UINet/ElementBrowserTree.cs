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
        Template,
        Account
    }
    internal class ElementValue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Kind { get; set; }
        public int Child { get; set; }
    }
    public class ElementBrowserTree
    {
        #region Свойства

        public ElementKinds ElementKind { get; set; }
        public int SelectedElementId { get; set; }
        public string SelectedElementName { get; set; }
        public dynamic SelectedObject { get; private set; }

        public string ConnectionString { get; set; }
        /// <summary>
        /// Отображать только папки
        /// </summary>
        public bool ShowOnlyFolder { get; set; }

        #endregion

        public DialogResult ShowTree()
        {
            FormTreeFolder frm = new FormTreeFolder();

            frm.treeView.ImageList = GreateImageListTo(ElementKind);
            if(frm.treeView.ImageList.Images.Count==1)
                frm.treeView.SelectedImageIndex = 0;
            
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
            if(nodeTo==null)
                view.Nodes.Clear();
            else
                nodeTo.Nodes.Clear();
            
            var values = GetLevel(nodeTo == null ? 0 : (nodeTo.Tag as ElementValue).Id);
            var addTo = nodeTo == null ? view.Nodes : nodeTo.Nodes;
            
            foreach (var element in values.OrderBy(s=>
                     {
                         if (ElementKind == ElementKinds.Agent)
                         {
                             if (s.Kind == 5) return -1;
                             return s.Kind;
                         }
                         else
                         {
                             return s.Kind;
                         }
                     }).ThenBy(s => s.Name))
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = element.Name;
                newNode.Tag = element;
                addTo.Add(newNode);
                if (ElementKind == ElementKinds.Product | ElementKind==ElementKinds.Account )
                {
                    newNode.ImageKey = (element.Kind).ToString();
                    newNode.SelectedImageKey = newNode.ImageKey;
                    //newNode.ImageIndex = element.Kind < 1001 ? element.Kind : element.Kind - 1000;
                }
                else if (ElementKind == ElementKinds.Agent | ElementKind==ElementKinds.Binder
                                                           | ElementKind == ElementKinds.Template
                                                           | ElementKind== ElementKinds.Misc)
                {
                    newNode.ImageKey = element.Kind.ToString();
                    newNode.SelectedImageKey = newNode.ImageKey;
                }
                //newNode.SelectedImageIndex = newNode.ImageIndex;

                if (element.Child > 0)
                    newNode.Nodes.Add("empty");
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
                                + "select a.Id,a.Name, (select count(*) from dbo.FLD_TREE where p0=a.Id) as HasChild, cast(0 as smallint) as Kind from a order by a.name";

                        }
                        else
                        {
                            cmd.CommandText =
                                ";with a as \n"
                                + "( \n"
                                + "select cast(v.FLD_ID as int) as Id, v.FLD_NAME as Name \n"
                                + $"from dbo.FOLDERS v inner join dbo.FLD_TREE t on t.ID = v.FLD_ID where t.SHORTCUT = 0 and t.p0 = {parentId}   \n"
                                + ") \n"
                                + "select a.Id,a.Name, (select count(*) from dbo.FLD_TREE where p0=a.Id) as HasChild, cast(0 as smallint) as Kind from a order by a.name";
                        }
                    }
                    else if (ElementKind == ElementKinds.Account)
                    {
                        if (parentId == 0)
                        {
                            cmd.CommandText =
                                ";with a as \n"
                                + "( \n"
                                + "select cast(v.ACC_ID as int) as Id, concat_ws(' ', v.ACC_CODE, v.ACC_NAME) as Name, v.ACC_TYPE as Kind \n"
                                + "from dbo.ACCOUNTS v inner join dbo.ACC_TREE t on t.ID = v.ACC_ID where t.SHORTCUT = 0 and (t.P0 is null or t.p0 = 0)   \n"
                                + ") \n"
                                + "select a.Id,a.Name, (select count(*) from dbo.ACC_TREE where p0=a.Id) as HasChild, a.Kind from a order by a.name";

                        }
                        else
                        {
                            cmd.CommandText =
                                ";with a as \n"
                                + "( \n"
                                + "select cast(v.ACC_ID as int) as Id, concat_ws(' ', v.ACC_CODE, v.ACC_NAME) as Name, v.ACC_TYPE as Kind \n"
                                + $"from dbo.ACCOUNTS v inner join dbo.ACC_TREE t on t.ID = v.ACC_ID where t.SHORTCUT = 0 and t.p0 = {parentId}   \n"
                                + ") \n"
                                + "select a.Id,a.Name, (select count(*) from dbo.ACC_TREE where p0=a.Id) as HasChild, a. Kind from a order by a.name";
                        }
                    }
                    else
                    {
                        string prefix = string.Empty;
                        string TREETABLE = string.Empty;
                        string TABLENAME = string.Empty;
                        string WHEREADD = string.Empty;

                        if (ElementKind == ElementKinds.Product)
                        {
                            prefix = "ENT";
                            TABLENAME = "ENTITIES";
                            if (ShowOnlyFolder)
                            {
                                WHEREADD = " and v.ENT_TYPE in (0,1)";
                            }
                        }
                        else if (ElementKind == ElementKinds.Agent)
                        {
                            prefix = "AG";
                            TABLENAME = "AGENTS";
                            if (ShowOnlyFolder)
                            {
                                WHEREADD = " and v.AG_TYPE=0";
                            }
                        }
                        else if (ElementKind == ElementKinds.Binder)
                        {
                            prefix = "BIND";
                            TABLENAME = "BINDERS";
                        }
                        else if (ElementKind == ElementKinds.Template)
                        {
                            prefix = "TML";
                            TABLENAME = "TEMPLATES";
                        }
                        
                        TREETABLE = prefix + "_TREE";

                        if (ElementKind == ElementKinds.Misc)
                        {
                            prefix = "MSC";
                            TABLENAME = "MISC";
                            TREETABLE = "MISC_TREE";
                        }
                        if (parentId == 0)
                        {
                            cmd.CommandText = ";with a as (\n"
                                + $"select cast(v.{prefix}_ID as int) as Id, v.{prefix}_NAME as Name, v.{prefix}_TYPE as Kind \n"
                                + $"from dbo.{TABLENAME} v inner join dbo.{TREETABLE} t on t.ID = v.{prefix}_ID where t.SHORTCUT = 0 and (t.P0 is null or t.p0 = 0) {WHEREADD} \n"
                                + ") \n"
                                + $"select a.Id,a.Name, (select count(*) from dbo.{TREETABLE} where p0=a.Id) as HasChild, a.Kind from a";
                        }
                        else
                        {
                            cmd.CommandText =
                                ";with a as (\n"
                                + $"select cast(v.{prefix}_ID as int) as Id, v.{prefix}_NAME as Name, v.{prefix}_TYPE as Kind \n"
                                + $"from dbo.{TABLENAME} v inner join dbo.{TREETABLE} t on t.ID = v.{prefix}_ID where t.SHORTCUT = 0 and t.p0 = {parentId} {WHEREADD}   \n"
                                + ") \n"
                                + $"select a.Id,a.Name, (select count(*) from dbo.{TREETABLE} where p0=a.Id) as HasChild, a.Kind from a";
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
                        v.Kind = reader.GetInt16(3);
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
                list.Images.Add("0",Properties.Resources.entity_icon0);
                list.Images.Add("1",Properties.Resources.entity_icon1);
                list.Images.Add("1001",Properties.Resources.entity_icon1001);
                list.Images.Add("1002", Properties.Resources.entity_icon1002);
                list.Images.Add("1003", Properties.Resources.entity_icon1003);
                list.Images.Add("1004", Properties.Resources.entity_icon1004);
                list.Images.Add("1005", Properties.Resources.entity_icon1005);
                list.Images.Add("1006", Properties.Resources.entity_icon1006);
                list.Images.Add("1007", Properties.Resources.entity_icon1007);
                list.Images.Add("1008", Properties.Resources.entity_icon1008);
                list.Images.Add("1009", Properties.Resources.entity_icon1009);
                list.Images.Add("1010", Properties.Resources.entity_icon1010);
                list.Images.Add("1011", Properties.Resources.entity_icon1011);
                list.Images.Add("1012", Properties.Resources.entity_icon1012);
                list.Images.Add("1013", Properties.Resources.entity_icon1013);
                list.Images.Add("1014", Properties.Resources.entity_icon1014);
                list.Images.Add("1015", Properties.Resources.entity_icon1015);
                list.Images.Add("1016", Properties.Resources.entity_icon1016);
                list.Images.Add("1017", Properties.Resources.entity_icon1017);
                list.Images.Add("1018", Properties.Resources.entity_icon1018);
            }
            else if (kind == ElementKinds.Agent)
            {
                list.Images.Add("0",Properties.Resources.ag_icon0);
                list.Images.Add("1", Properties.Resources.ag_icon1);
                list.Images.Add("2", Properties.Resources.ag_icon2);
                list.Images.Add("3", Properties.Resources.ag_icon3);
                list.Images.Add("4", Properties.Resources.ag_icon4);
                list.Images.Add("5", Properties.Resources.ag_icon5);
            }
            else if (kind == ElementKinds.Misc)
            {
                list.Images.Add("-1",Properties.Resources.misc_type16v2);
                list.Images.Add("0",Properties.Resources.misc_fld16);
                list.Images.Add("1",Properties.Resources.misc_icon16);
            }
            else if (kind == ElementKinds.Binder)
            {
                list.Images.Add("0",Properties.Resources.binder_folder16);
                list.Images.Add("1",Properties.Resources.binder_icon16);
            }
            else if (kind == ElementKinds.Template)
            {
                list.Images.Add("0",Properties.Resources.template_fld16v2);
                list.Images.Add("1",Properties.Resources.template_icon16v2);
            }
            else if (kind == ElementKinds.Account)
            {
                list.Images.Add("1",Properties.Resources.acc_icon0);
                list.Images.Add("1",Properties.Resources.acc_icon1);
                list.Images.Add("1",Properties.Resources.acc_icon2);
                list.Images.Add("1",Properties.Resources.acc_icon3);
                list.Images.Add("-1",Properties.Resources.acc_iconp);
                list.Images.Add("-2",Properties.Resources.acc_iconz);
            }

            return list;
        }
    }
}
