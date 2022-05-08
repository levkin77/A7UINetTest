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
        Account,
        ProjectItem,
        Autonum,
        PriceList,
        PriceName,
        Currency,
        Unit
    }
    public class ElementValue
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
            frm.treeView.NodeMouseDoubleClick += delegate(object sender, TreeNodeMouseClickEventArgs e)
            {
                if (e.Node.Nodes.Count == 0)
                {
                    frm.DialogResult = DialogResult.OK;
                    frm.Close();
                }
            };
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
        
        protected virtual List<ElementValue> GetLevel(int parentId)
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
                            if (ShowOnlyFolder)
                            {
                                WHEREADD = " and v.BIND_TYPE = 0";
                            }
                        }
                        else if (ElementKind == ElementKinds.Template)
                        {
                            prefix = "TML";
                            TABLENAME = "TEMPLATES";
                            if (ShowOnlyFolder)
                            {
                                WHEREADD = " and v.TML_TYPE = 0";
                            }
                        }
                        
                        TREETABLE = prefix + "_TREE";

                        if (ElementKind == ElementKinds.Misc)
                        {
                            prefix = "MSC";
                            TABLENAME = "MISC";
                            TREETABLE = "MISC_TREE";
                            if (ShowOnlyFolder)
                            {
                                WHEREADD = " and v.MSC_TYPE in(0,-1)";
                            }
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

    public class ElementBrowserTreeProjectItems
    {
        /// <summary>
        /// Идентификатор выбранного элемента
        /// </summary>
        public int SelectedElementId { get; set; }
        /// <summary>
        /// Выбранное наименование
        /// </summary>
        public string SelectedElementName { get; set; }
        /// <summary>
        /// Выбранный объект
        /// </summary>
        public dynamic SelectedObject { get; private set; }
        /// <summary>
        /// Строка соединения
        /// </summary>
        public string ConnectionString { get; set; }

        public DialogResult ShowTree()
        {
            FormTreeFolder frm = new FormTreeFolder();

            frm.treeView.ImageList = GreateImageListTo(ElementKinds.ProjectItem);
            if (frm.treeView.ImageList.Images.Count == 1)
                frm.treeView.SelectedImageIndex = 0;

            FillRootNodes(frm.treeView);            
            frm.treeView.NodeMouseDoubleClick += delegate (object sender, TreeNodeMouseClickEventArgs e)
            {
                frm.treeView.SelectedNode = e.Node;
                if (e.Node.Nodes.Count == 0)
                {
                    if (e.Node != null & (e.Node.Tag as ElementValue) != null)
                    {
                        frm.DialogResult = DialogResult.OK;
                        frm.Close();
                    }
                }
            };
            frm.FormClosing += delegate(object sender, FormClosingEventArgs e)
            {
                if (frm.DialogResult == DialogResult.Cancel) return;
                if(frm.treeView.SelectedNode==null)
                    e.Cancel = true;
                else if((frm.treeView.SelectedNode.Tag as ElementValue)==null)
                    e.Cancel=true;
            };
            var res = frm.ShowDialog();
            SelectedElementId = 0;
            SelectedElementName = string.Empty;
            if (frm.treeView.SelectedNode != null)
            {
                var objSelected = frm.treeView.SelectedNode.Tag as ElementValue;
                if (objSelected != null)
                {
                    SelectedElementId = objSelected.Id;
                    SelectedElementName = objSelected.Name;
                }
                SelectedObject = objSelected;
            }
            return res;
        }

        

        private void FillRootNodes(TreeView view)
        {
            /*
0	Форма
1	Диалог
2	Электронная таблица
3	Модуль
100	Модуль рабочей области
             */
            
            var list = GetLevel();
            if (list.Any(s => s.Kind == 0))
            {
                var node = view.Nodes.Add("Формы");
                node.ImageKey = "f";
                foreach (var element in list.Where(s => s.Kind == 0))
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = element.Name;
                    newNode.Tag = element;
                    newNode.ImageKey = "0";
                    newNode.SelectedImageKey = newNode.ImageKey;
                    node.Nodes.Add(newNode);
                }
            }

            if (list.Any(s => s.Kind == 1))
            {
                var node = view.Nodes.Add("Диалоги");
                node.ImageKey = "f";
                foreach (var element in list.Where(s => s.Kind == 1))
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = element.Name;
                    newNode.Tag = element;
                    newNode.ImageKey = "1";
                    newNode.SelectedImageKey = newNode.ImageKey;
                    node.Nodes.Add(newNode);
                }
            }
            if (list.Any(s => s.Kind == 2))
            {
                var node = view.Nodes.Add("Электронная таблица");
                node.ImageKey = "f";
                foreach (var element in list.Where(s => s.Kind == 2))
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = element.Name;
                    newNode.Tag = element;
                    newNode.ImageKey = "2";
                    newNode.SelectedImageKey = newNode.ImageKey;
                    node.Nodes.Add(newNode);
                }
            }

            if (list.Any(s => s.Kind == 3))
            {
                var node = view.Nodes.Add("Модули");
                node.ImageKey = "f";
                foreach (var element in list.Where(s => s.Kind == 3))
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = element.Name;
                    newNode.Tag = element;
                    newNode.ImageKey = "3";
                    newNode.SelectedImageKey = newNode.ImageKey;
                    node.Nodes.Add(newNode);
                }
            }

            if (list.Any(s => s.Kind == 100))
            {
                if (list.Count(s => s.Kind == 100) > 1)
                {
                    var node = view.Nodes.Add("Модули рабочей области");
                    foreach (var element in list.Where(s => s.Kind == 100))
                    {
                        TreeNode newNode = new TreeNode();
                        newNode.Text = element.Name;
                        newNode.Tag = element;
                        newNode.ImageKey = "100";
                        newNode.SelectedImageKey = newNode.ImageKey;
                        node.Nodes.Add(newNode);
                    }
                }
                else
                {
                    var element = list.First(s => s.Kind == 100);
                    TreeNode newNode = new TreeNode();
                    newNode.Text = element.Name;
                    newNode.Tag = element;
                    newNode.ImageKey = "100";
                    newNode.SelectedImageKey = newNode.ImageKey;
                    view.Nodes.Add(newNode);
                }
            }

        }
        protected virtual List<ElementValue> GetLevel()
        {
            List<ElementValue> coll = new List<ElementValue>();
            using (var cnn = new SqlConnection(ConnectionString))
            {
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "select a.FRM_ID, a.FRM_TYPE, a.FRM_NAME from dbo.FORMS a";
                    cmd.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ElementValue v = new ElementValue();
                        v.Id = reader.GetInt32(0);
                        v.Kind = reader.GetInt16(1);
                        v.Name = reader.GetString(2);                        
                        coll.Add(v);
                    }
                }
            }
            return coll;
        }
        private ImageList GreateImageListTo(ElementKinds kind)
        {
            ImageList list = new ImageList();
            if (kind == ElementKinds.ProjectItem)
            {
                list.Images.Add("f", Properties.Resources.FOLDERYELOW16);
                list.Images.Add("0", Properties.Resources.form_X16);
                list.Images.Add("1", Properties.Resources.dialog_x16);
                list.Images.Add("2", Properties.Resources.table_X16);
                list.Images.Add("3", Properties.Resources.module_x16);
                list.Images.Add("100", Properties.Resources.module_X16V2);

            }

            return list;
        }
    }

    public class ElementBrowserTreeAutonum
    {
        /// <summary>
        /// Идентификатор выбранного элемента
        /// </summary>
        public int SelectedElementId { get; set; }
        /// <summary>
        /// Выбранное наименование
        /// </summary>
        public string SelectedElementName { get; set; }
        /// <summary>
        /// Выбранный объект
        /// </summary>
        public dynamic SelectedObject { get; private set; }
        /// <summary>
        /// Строка соединения
        /// </summary>
        public string ConnectionString { get; set; }

        public DialogResult ShowTree()
        {
            FormTreeFolder frm = new FormTreeFolder();

            frm.treeView.ImageList = GreateImageListTo(ElementKinds.Autonum);
            if (frm.treeView.ImageList.Images.Count == 1)
                frm.treeView.SelectedImageIndex = 0;

            FillRootNodes(frm.treeView);
            frm.treeView.NodeMouseDoubleClick += delegate (object sender, TreeNodeMouseClickEventArgs e)
            {
                frm.treeView.SelectedNode = e.Node;
                if (e.Node.Nodes.Count == 0)
                {
                    if (e.Node != null & (e.Node.Tag as ElementValue) != null)
                    {
                        frm.DialogResult = DialogResult.OK;
                        frm.Close();
                    }
                }
            };
            frm.FormClosing += delegate (object sender, FormClosingEventArgs e)
            {
                if (frm.DialogResult == DialogResult.Cancel) return;
                if (frm.treeView.SelectedNode == null)
                    e.Cancel = true;
                else if ((frm.treeView.SelectedNode.Tag as ElementValue) == null)
                    e.Cancel = true;
            };
            var res = frm.ShowDialog();
            SelectedElementId = 0;
            SelectedElementName = string.Empty;
            if (frm.treeView.SelectedNode != null)
            {
                var objSelected = frm.treeView.SelectedNode.Tag as ElementValue;
                if (objSelected != null)
                {
                    SelectedElementId = objSelected.Id;
                    SelectedElementName = objSelected.Name;
                }
                SelectedObject = objSelected;
            }
            return res;
        }



        private void FillRootNodes(TreeView view)
        {
            var list = GetLevel();
            foreach (var element in list.OrderBy(s=>s.Name))
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = element.Name;
                newNode.Tag = element;
                newNode.ImageKey = "0";
                newNode.SelectedImageKey = newNode.ImageKey;
                view.Nodes.Add(newNode);
            }

        }
        protected virtual List<ElementValue> GetLevel()
        {
            List<ElementValue> coll = new List<ElementValue>();
            using (var cnn = new SqlConnection(ConnectionString))
            {
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "select FA_ID,FA_NAME  FROM dbo.FRM_AUTONUM";
                    cmd.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ElementValue v = new ElementValue();
                        v.Id = reader.GetInt32(0);
                        v.Name = reader.GetString(1);
                        coll.Add(v);
                    }
                }
            }
            return coll;
        }
        private ImageList GreateImageListTo(ElementKinds kind)
        {
            ImageList list = new ImageList();
            if (kind == ElementKinds.Autonum)
            {
                list.Images.Add("0", Properties.Resources.numerical_x16);
            }

            return list;
        }
    }

    public class ElementBrowserTreePriceList
    {
        public enum ViewData
        {
            PriceList,
            PriceName,
            All
        }

        #region Свойства        
        /// <summary>
        /// Идентификатор выбранного элемента
        /// </summary>
        public int SelectedElementId { get; set; }
        /// <summary>
        /// Выбранное наименование
        /// </summary>
        public string SelectedElementName { get; set; }
        /// <summary>
        /// Выбранный объект
        /// </summary>
        public dynamic SelectedObject { get; private set; }
        /// <summary>
        /// Строка соединения
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// Тип отображения, по умолчанию только наименования прайс-листов
        /// </summary>
        public ViewData ViewDataKind { get; set; } = ViewData.PriceList;
        #endregion
        public DialogResult ShowTree()
        {
            FormTreeFolder frm = new FormTreeFolder();

            frm.treeView.ImageList = GreateImageListTo(ElementKinds.PriceList);
            if (frm.treeView.ImageList.Images.Count == 1)
                frm.treeView.SelectedImageIndex = 0;

            if (ViewDataKind != ViewData.All)
                FillRootNodes(frm.treeView);
            else
                FillAllModes(frm.treeView);
            frm.treeView.NodeMouseDoubleClick += delegate (object sender, TreeNodeMouseClickEventArgs e)
            {
                frm.treeView.SelectedNode = e.Node;
                if (e.Node.Nodes.Count == 0)
                {
                    if (e.Node != null & (e.Node.Tag as SelectorModel) != null)
                    {
                        frm.DialogResult = DialogResult.OK;
                        frm.Close();
                    }
                }
            };
            frm.FormClosing += delegate (object sender, FormClosingEventArgs e)
            {
                if (frm.DialogResult == DialogResult.Cancel) return;
                if (frm.treeView.SelectedNode == null)
                    e.Cancel = true;
                else if ((frm.treeView.SelectedNode.Tag as SelectorModel) == null)
                    e.Cancel = true;
            };
            var res = frm.ShowDialog();
            SelectedElementId = 0;
            SelectedElementName = string.Empty;
            if (frm.treeView.SelectedNode != null)
            {
                var objSelected = frm.treeView.SelectedNode.Tag as SelectorModel;
                if (objSelected != null)
                {
                    SelectedElementId = objSelected.Id;
                    SelectedElementName = objSelected.Name;
                }
                SelectedObject = objSelected;
            }
            return res;
        }

        private void FillRootNodes(TreeView view)
        {
            var list = GetLevel();
            foreach (var element in list.OrderBy(s => s.Name))
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = element.Name;
                newNode.Tag = element;
                newNode.ImageKey = element.Kind.ToString();
                newNode.SelectedImageKey = newNode.ImageKey;
                view.Nodes.Add(newNode);
            }
        }
        private void FillAllModes(TreeView view)
        {
            WA wa = new WA();
            wa.ConnectionString = this.ConnectionString;
            var data = wa.GetPriceListSelector();
            foreach (var element in data.OrderBy(s => s.Name))
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = element.Name;
                newNode.Tag = element;
                newNode.ImageKey = element.Kind.ToString();
                newNode.SelectedImageKey = newNode.ImageKey;
                view.Nodes.Add(newNode);
                if(element.Elements.Count>0)
                {
                    foreach (var elChild in element.Elements)
                    {
                        TreeNode newPrcNameNode = new TreeNode();
                        newPrcNameNode.Text = elChild.Name;
                        newPrcNameNode.Tag = elChild;
                        newPrcNameNode.ImageKey = elChild.Kind.ToString();
                        newPrcNameNode.SelectedImageKey = newPrcNameNode.ImageKey;
                        newNode.Nodes.Add(newPrcNameNode);
                    }
                }
            }
        }
        protected virtual List<SelectorModel> GetLevel()
        {
            if (ViewDataKind == ViewData.PriceList)
                return GetLevelSimple(0);
            if (ViewDataKind == ViewData.PriceName)
                return GetLevelSimple(1);
            if (ViewDataKind == ViewData.PriceName)
                return GetLevelAll();
            return null;
        }
        protected virtual List<SelectorModel> GetLevelSimple(int kind=0)
        {
            List<SelectorModel> coll = new List<SelectorModel>();
            using (var cnn = new SqlConnection(ConnectionString))
            {
                using (var cmd = cnn.CreateCommand())
                {
                    if(kind==1)
                        cmd.CommandText = "select a.PRC_ID, a.PRC_NAME from dbo.PRC_NAMES a";
                    else
                        cmd.CommandText = "select PRL_ID, PRL_NAME  FROM dbo.PRL_LISTS";
                    cmd.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        SelectorModel v = new SelectorModel();
                        v.Id = reader.GetInt32(0);
                        v.Name = reader.GetString(1);
                        v.Kind = kind;
                        coll.Add(v);
                    }
                }
            }
            return coll;
        }
        protected virtual List<SelectorModel> GetLevelAll()
        {
            List<SelectorModel> coll = new List<SelectorModel>();
            using (var cnn = new SqlConnection(ConnectionString))
            {
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "select a.PRC_ID, a.PRC_NAME from dbo.PRC_NAMES a";
                    cmd.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        SelectorModel v = new SelectorModel();
                        v.Id = reader.GetInt32(0);
                        v.Name = reader.GetString(1);
                        v.Kind = 1;
                        coll.Add(v);
                    }
                }
            }
            return coll;
        }
        private ImageList GreateImageListTo(ElementKinds kind)
        {
            ImageList list = new ImageList();
            if (kind == ElementKinds.PriceList)
            {
                list.Images.Add("0", Properties.Resources.pricelist_x16);
                list.Images.Add("1", Properties.Resources.pricename_x16);
            }

            return list;
        }
    }

    /// <summary>
    /// Выбор валюты
    /// </summary>
    public class ElementBrowserTreeSimple
    {
        /// <summary>
        /// Идентификатор выбранного элемента
        /// </summary>
        public int SelectedElementId { get; set; }
        /// <summary>
        /// Выбранное наименование
        /// </summary>
        public string SelectedElementName { get; set; }
        /// <summary>
        /// Выбранный объект
        /// </summary>
        public dynamic SelectedObject { get; private set; }

        public ElementKinds ElementKind { get; set; }
        /// <summary>
        /// Строка соединения
        /// </summary>
        public string ConnectionString { get; set; }

        public DialogResult ShowTree()
        {
            FormTreeFolder frm = new FormTreeFolder();

            frm.treeView.ImageList = GreateImageListTo(ElementKind);
            if (frm.treeView.ImageList.Images.Count == 1)
                frm.treeView.SelectedImageIndex = 0;

            FillRootNodes(frm.treeView);
            frm.treeView.NodeMouseDoubleClick += delegate (object sender, TreeNodeMouseClickEventArgs e)
            {
                frm.treeView.SelectedNode = e.Node;
                if (e.Node.Nodes.Count == 0)
                {
                    if (e.Node != null & (e.Node.Tag as ElementValue) != null)
                    {
                        frm.DialogResult = DialogResult.OK;
                        frm.Close();
                    }
                }
            };
            frm.FormClosing += delegate (object sender, FormClosingEventArgs e)
            {
                if (frm.DialogResult == DialogResult.Cancel) return;
                if (frm.treeView.SelectedNode == null)
                    e.Cancel = true;
                else if ((frm.treeView.SelectedNode.Tag as ElementValue) == null)
                    e.Cancel = true;
            };
            var res = frm.ShowDialog();
            SelectedElementId = 0;
            SelectedElementName = string.Empty;
            if (frm.treeView.SelectedNode != null)
            {
                var objSelected = frm.treeView.SelectedNode.Tag as ElementValue;
                if (objSelected != null)
                {
                    SelectedElementId = objSelected.Id;
                    SelectedElementName = objSelected.Name;
                }
                SelectedObject = objSelected;
            }
            return res;
        }



        private void FillRootNodes(TreeView view)
        {
            var list = GetLevel();
            foreach (var element in list.OrderBy(s => s.Name))
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = element.Name;
                newNode.Tag = element;
                newNode.ImageKey = "0";
                newNode.SelectedImageKey = newNode.ImageKey;
                view.Nodes.Add(newNode);
            }

        }
        protected virtual List<ElementValue> GetLevel()
        {
            List<ElementValue> coll = new List<ElementValue>();
            using (var cnn = new SqlConnection(ConnectionString))
            {
                using (var cmd = cnn.CreateCommand())
                {
                    if(ElementKind==ElementKinds.Currency)
                        cmd.CommandText = "select CRC_ID,CRC_NAME FROM dbo.CURRENCIES";
                    else if (ElementKind == ElementKinds.Unit)
                        cmd.CommandText = "select UN_ID, UN_NAME FROM dbo.UNITS";
                    cmd.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ElementValue v = new ElementValue();
                        v.Id = reader.GetInt32(0);
                        v.Name = reader.GetString(1);
                        coll.Add(v);
                    }
                }
            }
            return coll;
        }
        private ImageList GreateImageListTo(ElementKinds kind)
        {
            ImageList list = new ImageList();
            if (kind == ElementKinds.Currency)
            {
                list.Images.Add("0", Properties.Resources.dollar_x16);
            }
            else if (kind == ElementKinds.Unit)
            {
                list.Images.Add("0", Properties.Resources.unit_x16);
            }

            return list;
        }
    }
}
