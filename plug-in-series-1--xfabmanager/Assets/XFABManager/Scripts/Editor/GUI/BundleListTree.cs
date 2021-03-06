using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using XFABManager;

public class BundleListTree : TreeView
{

    #region 变量

    //private XFABProject project;
    private XFAssetBundleProjectMain mainWindow;
    private bool isContextClickItem = false;

    private AssetBundlesPanel bundlesPanel;

    #endregion


    #region 重写方法

    protected override TreeViewItem BuildRoot()
    {
        return CreateView();
    }
    protected override IList<TreeViewItem> BuildRows(TreeViewItem root)
    {
        return base.BuildRows(root);
    }

    protected override void ContextClicked()
    {
       
        if ( isContextClickItem ) {
            isContextClickItem = false;
            return;
        }
        GenericMenu menu = new GenericMenu();

        //if (!AssetBundleModel.Model.DataSource.IsReadOnly())
        //{
        menu.AddItem(new GUIContent("Add new bundle"), false, CreateNewBundle, null);
        menu.ShowAsContext();
    }
    protected override void ContextClickedItem(int id) {

        //Debug.Log(" context click item: "+id);
        isContextClickItem = true;
        GenericMenu menu = new GenericMenu();
        menu.AddItem(new GUIContent("Add new bundle"), false, CreateNewBundle, null);
        menu.AddItem(new GUIContent("rename"), false, RenameBundle, id);
        menu.AddItem(new GUIContent("delete"), false, DeleteBundle, id);
        menu.ShowAsContext();
    }

    protected override void RenameEnded(RenameEndedArgs args)
    {
        base.RenameEnded(args);
        if (args.newName.Length > 0)
        {

            //Debug.Log( string.Format( " newName: {0} originalName:{1} ",args.newName ,args.originalName ) );

            if ( !args.newName.ToLower().Equals( args.originalName )  ) {

                // 重命名
                if (mainWindow.Project.RenameAssetBundle(args.newName.ToLower(), args.originalName)) { 
                    ReloadAndSelect(args.newName.ToLower().GetHashCode(),false);
                }
            }
        }
        else
        {
            args.acceptedRename = false;
        }
    }

    protected override bool CanRename(TreeViewItem item)
    {
        return item != null && item.displayName.Length > 0;
    }

    protected override bool CanMultiSelect(TreeViewItem item)
    {
        return false;
    }

    protected override void SelectionChanged(IList<int> selectedIds) {

        string bundle_name = null;

        if (selectedIds != null && selectedIds.Count != 0)
        {

                var item = FindItem(selectedIds[0], rootItem);
                if (item != null )
                {
                    bundle_name = item.displayName;
                }
        }

        //Debug.Log( string.Format( " select bundle name {0} ",bundle_name));
        bundlesPanel.UpdateSelectBundle(bundle_name);
        //m_Controller.UpdateSelectedBundles(selectedBundles);

    }

    public override void OnGUI(Rect rect)
    {
        base.OnGUI(rect);

        if (Event.current.type == EventType.MouseDown && Event.current.button == 0 && rect.Contains(Event.current.mousePosition))
        {
            SetSelection(new int[0], TreeViewSelectionOptions.FireSelectionChanged);
        }
 
        if ( this.rootItem.children != null && mainWindow.Project.assetBundles.Count != this.rootItem.children.Count) {
            Reload();
        }
 

    }

    #endregion

    #region 方法

    public BundleListTree(TreeViewState state, XFAssetBundleProjectMain mainWindow, AssetBundlesPanel bundlesPanel) : base(state)
    {
        showBorder = true;
        //this.project = mainWindow.Project;
        this.mainWindow = mainWindow;
        this.bundlesPanel = bundlesPanel;
    }

    public TreeViewItem CreateView()
    {
        TreeViewItem root = new TreeViewItem(0, -1);

        if (mainWindow.Project != null)
        {

            for (int i = 0; i < mainWindow.Project.assetBundles.Count; i++)
            {
                TreeViewItem child = new TreeViewItem(mainWindow.Project.assetBundles[i].bundle_name.GetHashCode(), 0, mainWindow.Project.assetBundles[i].bundle_name);
                root.AddChild(child);
            }

        }

        return root;
    }
    public string GetBundleName()
    {

        int index = 0;
        string name = null;
        do
        {
            index++;
            name = string.Format("new bundle{0}", index);
        } while (mainWindow.Project.IsContainAssetBundleName(name));

        return name;
    }

    void CreateNewBundle(object context)
    {

        string name = GetBundleName();

        Debug.Log(" CreateNewBundle : " + name);

        XFABAssetBundle assetBundle = new XFABAssetBundle(name);
        mainWindow.Project.AddAssetBundle(assetBundle);
        ReloadAndSelect(name.GetHashCode(), true);
    }

    void RenameBundle(object id) {
        ReloadAndSelect((int)id, true);
    }

    void DeleteBundle(object id) {
        mainWindow.Project.RemoveAssetBundle((int)id);
        Reload();
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
    
    #endregion
}
