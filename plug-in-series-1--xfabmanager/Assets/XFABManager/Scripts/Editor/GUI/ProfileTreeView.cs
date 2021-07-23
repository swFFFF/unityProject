using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace XFABManager
{
    public class ProfileTreeView : TreeView
    {

        private string url;
        private UpdateMode updateModel;
        private LoadMode loadModel;
        private bool isUseDefault;

        private bool isContextClickItem;

        MultiColumnHeaderState m_Mchs;

        private EditorWindow window;

        internal static MultiColumnHeaderState CreateDefaultMultiColumnHeaderState()
        {
            return new MultiColumnHeaderState(GetColumns());
        }
        private static MultiColumnHeaderState.Column[] GetColumns()
        {
            var retVal = new MultiColumnHeaderState.Column[] {
                new MultiColumnHeaderState.Column(),
                new MultiColumnHeaderState.Column(),
                new MultiColumnHeaderState.Column(),
                new MultiColumnHeaderState.Column(),
                new MultiColumnHeaderState.Column(),
            };
            retVal[0].headerContent = new GUIContent("Name", "配置文件名称!");
            retVal[0].minWidth = 50;
            retVal[0].width = 100;
            retVal[0].maxWidth = 300;
            retVal[0].headerTextAlignment = TextAlignment.Left;
            retVal[0].canSort = false;
            retVal[0].autoResize = true;
 

            retVal[1].headerContent = new GUIContent("Url", "AssetBundle 更新地址,AssetBundles文件夹所在的网络路径!");
            retVal[1].minWidth = 50;
            retVal[1].width = 300;
            retVal[1].maxWidth = 1000;
            retVal[1].headerTextAlignment = TextAlignment.Left;
            retVal[1].canSort = false;
            retVal[1].autoResize = true;

            retVal[2].headerContent = new GUIContent("UpdateModel", "更新模式:Debug(不检测更新,使用本地资源!),Update(检测更新)");
            retVal[2].minWidth = 30;
            retVal[2].width = 200;
            retVal[2].maxWidth = 1000;
            retVal[2].headerTextAlignment = TextAlignment.Left;
            retVal[2].canSort = false;
            retVal[2].autoResize = true;

            retVal[3].headerContent = new GUIContent("LoadModel", "加载模式:\t\nAssets 从编辑器资源加载\t\nAssetBundle 从AssetBundle文件加载!(仅在编辑器模式下有用!正式环境只能从AssetBundle加载!)");
            retVal[3].minWidth = 30;
            retVal[3].width = 200;
            retVal[3].maxWidth = 1000;
            retVal[3].headerTextAlignment = TextAlignment.Left;
            retVal[3].canSort = false;
            retVal[3].autoResize = true;

            retVal[4].headerContent = new GUIContent("Use Default GetProjectVersion", "是否使用默认方式获取项目版本,具体用法详见:xxx!");
            retVal[4].minWidth = 30;
            retVal[4].width = 200;
            retVal[4].maxWidth = 1000;
            retVal[4].headerTextAlignment = TextAlignment.Left;
            retVal[4].canSort = false;
            retVal[4].autoResize = true;

            return retVal;
        }

        public ProfileTreeView(TreeViewState state, MultiColumnHeaderState mchs,EditorWindow window) : base(state, new MultiColumnHeader(mchs))
        {
            m_Mchs = mchs;
            showBorder = true;
            showAlternatingRowBackgrounds = true;
            this.window = window;
        }

        protected override TreeViewItem BuildRoot()
        {
            TreeViewItem root = new TreeViewItem(-1, -1);

            for (int i = 0; i < XFABManagerSettings.Settings.Profiles.Count; i++)
            {
                //Profile profileSettings = (Profile)XFABManagerSettings.Settings.Profiles.Where(x => x.name == "");
                TreeViewItem child = new TreeViewItem(i, 0, XFABManagerSettings.Settings.Profiles[i].name);
                root.AddChild(child);
            }

            return root;
        }
        protected override IList<TreeViewItem> BuildRows(TreeViewItem root)
        {
            return base.BuildRows(root);
        }
        protected override void RowGUI(RowGUIArgs args)
        {
            for (int i = 0; i < args.GetNumVisibleColumns(); ++i) {
                // 如果正在重命名 就不用绘制当前这一行了
                if (args.isRenaming) { continue; }
                CellGUI(args.GetCellRect(i), args.item, m_Mchs.visibleColumns[i], ref args);
            }
        }

        private void CellGUI(Rect cellRect, TreeViewItem item, int column, ref RowGUIArgs args)
        {
            CenterRectUsingSingleLineHeight(ref cellRect);

            Profile profile = XFABManagerSettings.Settings.Profiles.Where(a => a.name.Equals(item.displayName)).Single();

            if (profile == null)
            {
                Debug.LogFormat("profile:{0} is null!", item.displayName);
                return;
            }

            url = profile.url;
            updateModel = profile.updateModel;
            loadModel = profile.loadMode;
            isUseDefault = profile.useDefaultGetProjectVersion;

            switch (column)
            {
                case 0:         // Name
                        EditorGUI.LabelField(cellRect, item.displayName);
                    break;
                case 1:         // Url
                    profile.url = GUI.TextField(cellRect, profile.url);

                    if (!profile.url.Equals(url))
                    {
                        XFABManagerSettings.Settings.Save();
                    }

                    break;
                case 2:         // 更新模式
                    profile.updateModel = (UpdateMode)EditorGUI.EnumPopup(cellRect, profile.updateModel);
                    if (profile.updateModel != updateModel)
                    {
                        XFABManagerSettings.Settings.Save();
                    }
                    break;
                case 3:
                    //DefaultGUI.Label(cellRect, item.FileInfo.AssetPath, args.selected, args.focused);
                    profile.loadMode = (LoadMode)EditorGUI.EnumPopup(cellRect, profile.loadMode);
                    if (profile.loadMode != loadModel)
                    {
                        XFABManagerSettings.Settings.Save();
                    }
                    break;
                case 4:

                    profile.useDefaultGetProjectVersion = EditorGUI.Toggle(cellRect , profile.useDefaultGetProjectVersion);

                    if (profile.useDefaultGetProjectVersion != isUseDefault)
                    {
                        XFABManagerSettings.Settings.Save();
                    }

                    break;

            }

        }

        protected override void ContextClicked()
        {

            if (isContextClickItem)
            {
                isContextClickItem = false;
                return;
            }
            GenericMenu menu = new GenericMenu();

            //if (!AssetBundleModel.Model.DataSource.IsReadOnly())
            //{
            menu.AddItem(new GUIContent("Add new profile"), false, CreateNewProfile, null);
            menu.ShowAsContext();
        }
        protected override void ContextClickedItem(int id)
        {

            //Debug.Log(" context click item: "+id);
            isContextClickItem = true;
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Add new profile"), false, CreateNewProfile, null);
            menu.AddItem(new GUIContent("rename"), false, RenameBundle, id);
            menu.AddItem(new GUIContent("delete"), false, DeleteBundle, id);
            menu.ShowAsContext();
        }

        protected override bool CanRename(TreeViewItem item)
        {
            return item != null && item.displayName.Length > 0;
        }

        protected override void RenameEnded(RenameEndedArgs args)
        {
            base.RenameEnded(args);
            if (args.newName.Length > 0)
            {
                if (!args.newName.Equals(args.originalName))
                {
                    // 新名称已经存在
                    if (XFABManagerSettings.Settings.IsContainsProfileName(args.newName)) {
                        Debug.LogWarningFormat("重命名失败!名称{0}已经存在!", args.newName);
                        return;
                    }
                    // 默认的配置不能改名
                    if ( args.originalName.Equals("Default") ) {

                        this.window.ShowNotification(new GUIContent("Default Profile 不能改名!"));
                        return;
                    }

                    Profile profile = XFABManagerSettings.Settings.Profiles.Where(a => a.name.Equals(args.originalName)).Single();
                    // 重命名
                    profile.name = args.newName;
                    XFABManagerSettings.Settings.Save();
                    ReloadAndSelect(args.newName, false);
                }
            }
            else
            {
                args.acceptedRename = false;
            }
        }

        void CreateNewProfile(object context)
        {

            string name = GetProfileName();

            Profile profile = new Profile(name);
            XFABManagerSettings.Settings.Profiles.Add(profile);

            ReloadAndSelect(name, true);
        }

        void RenameBundle(object id)
        {
            ReloadAndSelect((int)id, true);
        }

        void DeleteBundle(object id)
        {

            TreeViewItem item = FindItem((int)id, rootItem);

            if (item.displayName.Equals("Default")) {
                this.window.ShowNotification(new GUIContent("Default Profile 不能删除!"));
                return;
            }

            if ( item != null )
            {
                XFABManagerSettings.Settings.Profiles.RemoveAll(x => x.name.Equals(item.displayName));
                Reload();
            }
        }

        private void ReloadAndSelect(string name, bool rename)
        {
            Reload();

            var selection = new List<int>();
            TreeViewItem item = FindItemByName(name);
            if (item == null) { return; }
            selection.Add(item.id);
            ReloadAndSelect(selection);
            if (rename)
            {
                BeginRename(FindItem(item.id, rootItem));
            }
        }

        private void ReloadAndSelect(int hashCode, bool rename)
        {
            var selection = new List<int>();
            selection.Add(hashCode);
            ReloadAndSelect(selection);
            if (rename)
            {
                BeginRename(FindItem(hashCode, rootItem), 0.25f);
            }
        }

        private void ReloadAndSelect(IList<int> hashCodes)
        {
            Reload();
            SetSelection(hashCodes, TreeViewSelectionOptions.RevealAndFrame);
            SelectionChanged(hashCodes);
        }
        public string GetProfileName()
        {

            int index = 0;
            string name = null;
            do
            {
                index++;
                name = string.Format("new profile{0}", index);
            } while ( XFABManagerSettings.Settings.IsContainsProfileName(name) );

            return name;
        }

        private TreeViewItem FindItemByName(string name) {

            foreach (var item in rootItem.children)
            {
                if (item.displayName.Equals(name)) {
                    return item;
                }
            }

            return null;

        }

        //protected override void SelectionChanged(IList<int> selectedIds)
        //{
        //    base.SelectionChanged(selectedIds);
        //    if (selectedIds.Count > 0) {
        //        current_rename_id = selectedIds[0];
        //    }
        //}

        

    }
}


