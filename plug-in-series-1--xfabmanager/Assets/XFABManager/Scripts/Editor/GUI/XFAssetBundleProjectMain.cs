using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace XFABManager
{

    internal enum Mode
    {
        AssetBundles,
        Build,
        Info,
    }

    public class XFAssetBundleProjectMain : EditorWindow
    {
        #region 常量

        const float toolbarPadding = 15;
        const float menubarPadding = 32;

        #endregion

        #region 变量

        string[] toolbar_labels = new string[3] { "AssetBundles", "Build", "Info" };
        Mode currentMode;
        AssetBundlesPanel assetBundlesPanel;
        AssetBundleBuildPanel buildPanel;
        InfoPanel infoPanel;

        XFABProject project;            // 当前打开的 AB 项目


        #endregion


        #region 属性


        public XFABProject Project
        {

            get
            {
                return project;
            }
        }

        #endregion


        #region OnGUI

        private void OnGUI()
        {
            ModeToggle();

            switch (currentMode)
            {
                case Mode.AssetBundles:

                    if ( assetBundlesPanel == null ) {
                        assetBundlesPanel = new AssetBundlesPanel();
                    }
                    assetBundlesPanel.OnGUI(GetSubWindowArea(),this);
                    break;
                case Mode.Build:
                    if (buildPanel == null) {
                        buildPanel = new AssetBundleBuildPanel(Project,this);
                    }
                    buildPanel.OnGUI();

                    break;
                case Mode.Info:
                    if (infoPanel == null) {
                        infoPanel = new InfoPanel(this, Project);
                    }
                    infoPanel.OnGUI();
                    break;
                default:
                    break;
            }

        }

        void ModeToggle()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(toolbarPadding);

            float toolbarWidth = position.width - toolbarPadding * 2;

            currentMode = (Mode)GUILayout.Toolbar((int)currentMode, toolbar_labels, "LargeButton", GUILayout.Width(toolbarWidth));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            //AssetDatabase.AssetPathToGUID()
            //AssetDatabase.GUIDToAssetPath()

        }

        private Rect GetSubWindowArea()
        {
 
            Rect subPos = new Rect(0, menubarPadding, position.width, position.height - menubarPadding);
            return subPos;
        }
        #endregion

        #region 方法

        public void InitProject(XFABProject project) {

            this.project = project;
            this.titleContent.text = string.IsNullOrEmpty( this.project.displayName ) ? "弦风课堂" : this.project.displayName;

            
        }

        #endregion
    }
}


