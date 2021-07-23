using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;


namespace XFABManager
{

    public class XFAssetBundleManagerProjects : EditorWindow
    {

        #region 常量
        public const float GRID_WIDTH = 150;        // 菜单界面 格子的宽
        public const float GRID_HEIGHT = 180;       // 菜单界面 格子的高
        public const float SPACE_X = 20;           // 菜单界面 格子X的间距
        public const float SPACE_Y = 20;            // 菜单界面 格子Y的间距
        public const float MARGIN_TOP = 20;         // 菜单界面 格子上边的间距
        public const float MARGIN_LEFT = 20;        // 菜单界面 格子左边的间距

        #endregion

        #region 变量

        private int row_grid_count = 1;     // 每行显示个子的数量 默认是1

        private Vector2 scrollPosition;         // Project Grid 滚动的位置

        private GUIContent buttonContent;
        private GUIStyle buttonStyle;

        private Texture refreshTexture;

        private XFAssetBundleProjectMain mainWindow;

        #endregion


        #region 生命周期

        void Awake()
        {
            refreshTexture = EditorGUIUtility.FindTexture("Refresh");
        }
        void OnGUI()
        {

            if ( XFABManagerSettings.Settings == null ) {

                GUILayout.Space(50);
                if (GUILayout.Button("Create XFABManager Settings"))
                {
                    //Debug.Log("Create XFABManager Settings");
                    XFABManagerSettings settings = CreateInstance<XFABManagerSettings>();
                    Profile profile = new Profile();
                    settings.Profiles.Add(profile);
                    settings.SelectIndex = 0;

                    string path = "Assets/XFABManager/Scripts/Editor/Settings/XFABManagerSettings.asset";
                    if ( !AssetDatabase.IsValidFolder( Path.GetDirectoryName(path)) ) {
                        Directory.CreateDirectory(Path.GetDirectoryName(path));
                    }

                    AssetDatabase.CreateAsset(settings, "Assets/XFABManager/Scripts/Editor/Settings/XFABManagerSettings.asset");
                }
                GUILayout.Space(20);
                GUILayout.Label("点击 按钮 \"Create XFABManager Settings\" 创建插件所需配置!");
                return;
            }

            if (buttonStyle == null)
            {
                ConfigStyle();
            }
            //GUILayout.Space(10);
            GUILayout.BeginHorizontal();

            //if (XFABProjectManager.Instance.IsRefreshing)
            //{
            //    GUILayout.Label(string.Format("正在刷新...{0}%", (int)(XFABProjectManager.Instance.RefreshProgress * 100)));
            //}
            //else {
            GUIContent profileButton = new GUIContent( string.Format( "Profile:{0}",XFABManagerSettings.Settings.CurrentProfile.name));
            Rect r = GUILayoutUtility.GetRect(profileButton, EditorStyles.toolbarDropDown,GUILayout.Width(150));

            if ( EditorGUI.DropdownButton(r,profileButton, FocusType.Passive, EditorStyles.toolbarDropDown ) ) {
                GenericMenu menu = new GenericMenu();
                string[] nameList = XFABManagerSettings.Settings.getProfileNames;
                for (int i = 0; i < nameList.Length; i++)
                {
                    menu.AddItem(new GUIContent(nameList[i]), i == XFABManagerSettings.Settings.SelectIndex,(index)=> {
                        XFABManagerSettings.Settings.SelectIndex = (int)index ;
                    }, i);
                }

                menu.AddSeparator(string.Empty);
                menu.AddItem(new GUIContent("Manage Profiles"), false, ()=> {
                    GetWindow<ProfileWindow>("Profiles").Show();
                });
                menu.DropDown(r);
            }

            var guiMode = new GUIContent("Tools");
            Rect rMode = GUILayoutUtility.GetRect(guiMode, EditorStyles.toolbarDropDown,GUILayout.Width(80));
            if (EditorGUI.DropdownButton(rMode, guiMode, FocusType.Passive, EditorStyles.toolbarDropDown))
            {
                var menu = new GenericMenu();

                menu.AddItem(new GUIContent("Refresh"), false, () => {
                    XFABProjectManager.Instance.RefreshProjects();
                    this.ShowNotification(new GUIContent("刷新成功!"));
                });
                menu.AddItem(new GUIContent("Package All"), false, () => {
                    //Debug.Log("一键打包 TODO ");
                    BuildAll();
                });
                //menu.AddItem(new GUIContent("优化资源"), false, () => {
                //    Debug.Log("优化资源 TODO ");
                //});
                menu.DropDown(rMode);
            }

            GUILayout.EndHorizontal();
            CaculateRowGridCount();
            DrawProjects();

        }

        // 每秒10帧更新
        void OnInspectorUpdate()
        {
            //开启窗口的重绘，不然窗口信息不会刷新
            Repaint();
        }

        #endregion


        #region OnGUI
        // 计算每行格子的数量
        private void CaculateRowGridCount()
        {

            // row_grid_count * width + row_grid_count  * Space_X  = width - m_left + Space_X
            row_grid_count = (int)((position.width - MARGIN_LEFT + SPACE_X) / (GRID_WIDTH + SPACE_X));

            if (row_grid_count < 1)
            {
                row_grid_count = 1;
            }

            if (row_grid_count > XFABProjectManager.Instance.Projects.Count + 1)    // 除了要画出格子之外还要画出一个 + 的按钮
            {
                row_grid_count = XFABProjectManager.Instance.Projects.Count + 1;
            }
        }

        // 画出所有的项目 和 添加按钮
        private void DrawProjects()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            GUILayout.Space(MARGIN_TOP);
            //内置图标
            for (int i = 0; i < XFABProjectManager.Instance.Projects.Count + 1; i += row_grid_count)
            {

                GUILayout.BeginHorizontal();

                for (int j = 0; j < row_grid_count; j++)
                {
                    if (j == 0)
                    {
                        GUILayout.Space(MARGIN_LEFT);
                    }

                    if (i + j < XFABProjectManager.Instance.Projects.Count + 1)
                    {
                        DrawProjectGrid(i + j);
                    }

                }
                GUILayout.EndHorizontal();
                GUILayout.Space(SPACE_Y);
            }

            GUILayout.EndScrollView();

            GUILayout.Label(row_grid_count.ToString());
        }

        // 画出具体某个格子
        private void DrawProjectGrid(int index)
        {


            if (index < XFABProjectManager.Instance.Projects.Count)
            {
                //buttonContent.tooltip = XFABProjectManager.Instance.Projects[index].displayName;
                // 显示具体模块
                buttonContent.text = string.Format("<size=18>{0}</size>\n\n{1}", XFABProjectManager.Instance.Projects[index].displayName, XFABProjectManager.Instance.Projects[index].name);
            }
            else
            {
                // 显示 添加 按钮
                buttonContent.text = "<size=40>+</size>"; //  <color=#00ffffff>+</color>  <size=40>TestTest</size>
            }

            if (GUILayout.Button(buttonContent, buttonStyle, GUILayout.Width(GRID_WIDTH), GUILayout.Height(GRID_HEIGHT)))
            {
                if (index < XFABProjectManager.Instance.Projects.Count)
                {
                    //Debug.Log(" 打开项目: " + projects[index].name);
                    OpenProject(XFABProjectManager.Instance.Projects[index]);
                }
                else
                {
                    //Debug.Log(" 添加 或 创建项目 ");
                    CreateProject();
                }
            }

            if (index <= XFABProjectManager.Instance.Projects.Count - 1)
            {
                GUILayout.Space(SPACE_X);
            }

        }
        #endregion


        #region 方法

        // 配置按钮样式
        public void ConfigStyle()
        {
            buttonContent = new GUIContent();
            buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.richText = true;
            buttonStyle.wordWrap = true;
        }

        // 打开项目 
        public void OpenProject(XFABProject project)
        {
            //Debug.Log(" 打开项目 " + project.name);

            if (mainWindow != null)
            {
                mainWindow.Close();
            }

            mainWindow = EditorWindow.GetWindow<XFAssetBundleProjectMain>("Project");
            mainWindow.Show();
            mainWindow.InitProject(project);

        }
        // 创建项目
        public void CreateProject()
        {
            // 显示创建项目的窗口
            Rect rect = new Rect(0, 0, 600, 700);
            XFAssetBundleManagerCreate window = EditorWindow.GetWindowWithRect<XFAssetBundleManagerCreate>(rect, true, "创建项目");
            window.Show();

        }

        public void BuildAll() {
            foreach (var item in XFABProjectManager.Instance.Projects)
            {
                ProjectBuild.Build(item, EditorUserBuildSettings.activeBuildTarget);
            }
        }

        #endregion


    }


}