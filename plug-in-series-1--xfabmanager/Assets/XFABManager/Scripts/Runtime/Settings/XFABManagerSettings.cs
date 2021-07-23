#if UNITY_EDITOR 
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace XFABManager
{

    public class XFABManagerSettings : ScriptableObject
    {
        [HideInInspector]
        public int _selectIndex;
        public int SelectIndex {
            get {
                return _selectIndex;
            }
            set {
                _selectIndex = value;
                Save();
            }
        }
        [HideInInspector]
        public List<Profile> Profiles = new List<Profile>();

        private static XFABManagerSettings _settings;

        public static XFABManagerSettings Settings {
            get {
                if (_settings == null) {
                    string[] assets = AssetDatabase.FindAssets("t:XFABManagerSettings");
                    if ( assets != null && assets.Length != 0 ) {
                        _settings = AssetDatabase.LoadAssetAtPath<XFABManagerSettings>(AssetDatabase.GUIDToAssetPath(assets[0]));
                    }
                }
                return _settings;
            }
        }

        public string[] getProfileNames {
            get {
                string[] names = new string[Profiles.Count];

                for (int i = 0; i < Profiles.Count; i++)
                {
                    names[i] = Profiles[i].name;
                }

                return names;
            }
        }

        public Profile CurrentProfile
        {
            get {
                if (SelectIndex >= Profiles.Count)
                    SelectIndex = 0;
                return Profiles[SelectIndex];
            }
        }

        /// <summary>
        /// 保存修改
        /// </summary>
        public void Save() {
            EditorUtility.SetDirty(this);
        }

        public bool IsContainsProfileName(string name) {

            foreach (var item in Profiles)
            {
                if (item.name.Equals(name)) {
                    return true;
                }
            }

            return false;
        }

    }

}

#endif