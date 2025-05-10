using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameFrameworks.Editors
{
    public enum PackageStatus {
        NotInstalled,
        Installed,
        NeedsUpdate
    }

    public class GameFrameworksManagerWindow : EditorWindow
    {
        private const string MENU_ITEM_CLEAR_EDITOR_PREFS = "Edit/Clear All EditorPrefs";
        private const string MENU_ITEM_OPEN = "GameFrameworks/Settings &#f";
        private const string MENU_TITLE = "GameFrameworks Settings";

        private const int MIN_WIDTH = 800;
        private const int MIN_HEIGHT = 600;
        
        private const int FIXED_PANEL_INDEX = 0;
        private const float FIXED_PANEL_WIDTH = 200f;

        public static Texture2D GetStatusIcon(PackageStatus status) {
            switch (status)
            {
                case PackageStatus.Installed:
                    return EditorGUIUtility.IconContent("d_winbtn_mac_max").image as Texture2D;
                case PackageStatus.NeedsUpdate:
                    return EditorGUIUtility.IconContent("d_winbtn_mac_min").image as Texture2D;
                case PackageStatus.NotInstalled:
                default:
                    return EditorGUIUtility.IconContent("d_winbtn_mac_close").image as Texture2D;
            }
        }

        // private const string USS_PATH = EditorPaths.COMMON + "Settings/Stylesheets/SettingsWindow";
        
        private const string KEY_CACHE_INDEX = "gcset:cache-index";

        // private static IIcon ICON_WINDOW;
        private static GameFrameworksManagerWindow WINDOW;

        public const int INIT_PRIORITY_HIGH = 0;
        public const int INIT_PRIORITY_DEFAULT = 1;
        public const int INIT_PRIORITY_LOW = 2;
        
        // public static readonly List<InitRunner> InitRunners = new List<InitRunner>();

        // PROPERTIES: ----------------------------------------------------------------------------

        private static int CacheIndex
        {
            get => EditorPrefs.GetInt(KEY_CACHE_INDEX, 0);
            set => EditorPrefs.SetInt(KEY_CACHE_INDEX, value);
        }
        
        private VisualElement leftPanel;
        private VisualElement rightPanel;

        // internal SettingsContentList ContentList { get; set; }
        // internal SettingsContentDetails ContentDetails { get; set; }

        public event Action<int> EventChangeSelection;

        // [InitializeOnLoadMethod]
        // private static void InitializeOnLoad()
        // {
        //     EditorApplication.delayCall += DeferredInitializeOnLoad;
        // }

        // private static void DeferredInitializeOnLoad()
        // {
        //     Type[] types = TypeUtils.GetTypesDerivedFrom(typeof(TAssetRepository));
        //     foreach (Type type in types)
        //     {
        //         string[] foundGuids = AssetDatabase.FindAssets($"t:{type}");
        //         if (foundGuids.Length > 0) continue;

        //         TAssetRepository asset = CreateInstance(type) as TAssetRepository;
        //         if (asset == null) continue;
                
        //         DirectoryUtils.RequirePath(asset.AssetPath);
        //         string assetPath = PathUtils.Combine(
        //             asset.AssetPath, 
        //             $"{asset.RepositoryID}.asset"
        //         );
                
        //         DirectoryUtils.RequireFilepath(assetPath);
        //         AssetDatabase.CreateAsset(asset, assetPath);
        //         AssetDatabase.SaveAssets();
        //     }
            
        //     InitRunners.Sort((a, b) => a.Order.CompareTo(b.Order));
        //     foreach (InitRunner initRunner in InitRunners)
        //     {
        //         if (!initRunner.CanRun()) continue;
                
        //         initRunner.Run();
        //         return;
        //     }
        // }
        
        [MenuItem(MENU_ITEM_CLEAR_EDITOR_PREFS, false, 270)]
        private static void RevealPersistentDataFolder()
        {
            bool confirmation = EditorUtility.DisplayDialog(
                "Clear All EditorPrefs",
                "Are you sure you want to clear all PlayerPrefs? This action cannot be undone.",
                "Yes", "Cancel"
            );
            
            if (confirmation) EditorPrefs.DeleteAll();
        }

        [MenuItem(MENU_ITEM_OPEN, priority = 10)]
        public static void OpenWindow()
        {
            SetupWindow();
            // WINDOW.ContentList.Index = CacheIndex;
        }

        public static void OpenWindow(string repositoryID)
        {
            SetupWindow();
            
            // int index = WINDOW.ContentList.FindIndex(repositoryID);
            // WINDOW.ContentList.Index = index >= 0 ? index : CacheIndex;
        }

        private static void SetupWindow()
        {
            if (WINDOW != null) WINDOW.Close();
            
            WINDOW = GetWindow<GameFrameworksManagerWindow>();
            WINDOW.minSize = new Vector2(MIN_WIDTH, MIN_HEIGHT);
        }

        private void OnEnable()
        {
            // ICON_WINDOW ??= new IconWindowSettings(ColorTheme.Type.TextLight);
            this.titleContent = new GUIContent(MENU_TITLE, EditorGUIUtility.IconContent("d_TerrainInspector.TerrainToolSettings").image);
            
            // StyleSheet[] styleSheetsCollection = StyleSheetUtils.Load(USS_PATH);
            // foreach (StyleSheet styleSheet in styleSheetsCollection)
            // {
            //     this.rootVisualElement.styleSheets.Add(styleSheet);
            // }

            TwoPaneSplitView splitView = new TwoPaneSplitView(
                FIXED_PANEL_INDEX,
                FIXED_PANEL_WIDTH,
                TwoPaneSplitViewOrientation.Horizontal
            );

            this.rootVisualElement.Add(splitView);

            leftPanel = new VisualElement();
            leftPanel.style.flexDirection = FlexDirection.Column;
            rightPanel = new VisualElement();
            rightPanel.style.flexGrow = 1;

            splitView.Add(leftPanel);
            splitView.Add(rightPanel);

            AddSidebarButton("General", "d_Settings", () => ShowGeneralContent());
            AddSidebarButton("Install", "d_Import", () => ShowInstallContent());

            ShowGeneralContent();
            
            // this.ContentList = new SettingsContentList(this);
            // this.ContentDetails = new SettingsContentDetails(this);

            // splitView.Add(this.ContentList);
            // splitView.Add(this.ContentDetails);
            
            // this.ContentList.OnEnable();
            // this.ContentDetails.OnEnable();
        }

        private void OnDisable()
        {
            // this.ContentList?.OnDisable();
            // this.ContentDetails?.OnDisable();
        }

        private void AddSidebarButton(string label, string iconName, Action onClick) {
            var button = new Button(onClick)
            {
                style =
                {
                    height = 40,
                    flexDirection = FlexDirection.Row,
                    alignItems = Align.Center,
                    justifyContent = Justify.FlexStart,
                    marginLeft = 0,
                    marginBottom = 0,
                    backgroundColor = new Color(0.15f, 0.15f, 0.15f),
                }
            };

            var icon = new Image
            {
                image = EditorGUIUtility.IconContent(iconName).image,
                style =
                {
                    width = 16,
                    height = 16,
                    marginLeft = 8,
                    marginRight = 8,
                }
            };

            var labelEl = new Label(label)
            {
                style =
                {
                    unityFontStyleAndWeight = FontStyle.Bold,
                    fontSize = 13,
                    color = Color.white,
                }
            };

            button.Add(icon);
            button.Add(labelEl);
            leftPanel.Add(button);
        }

        private void ShowGeneralContent() {
            rightPanel.Clear();
            var label = new Label("General Settings")
            {
                style =
                {
                    unityFontStyleAndWeight = FontStyle.Bold,
                    fontSize = 18,
                    marginTop = 10,
                    marginLeft = 10
                }
            };
            rightPanel.Add(label);
            // 추가 내용 구성 가능
        }

        private void ShowInstallContent() {
            rightPanel.Clear();
            var label = new Label("Install Settings")
            {
                style =
                {
                    unityFontStyleAndWeight = FontStyle.Bold,
                    fontSize = 18,
                    marginTop = 10,
                    marginLeft = 10
                }
            };
            rightPanel.Add(label);

            var packageAssets = LoadAllPackageAssets();

            foreach (var pkg in packageAssets)
            {
                var row = new VisualElement();
                row.style.flexDirection = FlexDirection.Row;
                row.style.marginTop = 5;
                row.style.marginLeft = 10;
                row.style.marginRight = 10;
                row.style.justifyContent = Justify.SpaceBetween;

                var status = GetPackageStatus(pkg);
                var icon = new Image { image = GetStatusIcon(status) };
                string shownVersion = pkg.version;
                icon.style.width = 16;
                icon.style.height = 16;
                icon.style.marginRight = 5;

                if (status == PackageStatus.Installed)
                {
                    string installedVersion = GetInstalledVersion(pkg);
                    if (!string.IsNullOrEmpty(installedVersion))
                        shownVersion = installedVersion;
                }

                var nameLabel = new Label($"{pkg.displayName} ({shownVersion})");
                nameLabel.style.flexGrow = 1;
                nameLabel.style.unityTextAlign = TextAnchor.MiddleLeft;

                var button = new Button(() =>
                {
                    if (status == PackageStatus.NotInstalled) InstallPackage(pkg);
                    else if (status == PackageStatus.NeedsUpdate) UpdatePackage(pkg);
                })
                {
                    text = status == PackageStatus.NotInstalled ? "Install" :
                        status == PackageStatus.NeedsUpdate ? "Update" : "Reinstall"
                };

                row.Add(icon);
                row.Add(nameLabel);
                row.Add(button);

                rightPanel.Add(row);
            }
        }

        private void UpdatePackage(SO_PackageAsset pkg)
        {
            InstallPackage(pkg);
        }

        private void InstallPackage(SO_PackageAsset pkg)
        {
            if (pkg.sourceType == PackageSourceType.Git)
            {
                UnityEditor.PackageManager.Client.Add($"{pkg.packageName}#{pkg.version}");
            }
            else if (pkg.sourceType == PackageSourceType.UnityRegistry)
            {
                UnityEditor.PackageManager.Client.Add($"{pkg.packageName}@{pkg.version}");
            }
            else
            {
                Debug.LogWarning("Asset Store 기반 패키지는 자동 설치를 지원하지 않습니다.");
            }
        }

        private string GetInstalledVersion(SO_PackageAsset pkg)
        {
        #if UNITY_2021_1_OR_NEWER
            var listRequest = UnityEditor.PackageManager.Client.List(true);
            while (!listRequest.IsCompleted) { }

            if (listRequest.Status == UnityEditor.PackageManager.StatusCode.Success)
            {
                foreach (var info in listRequest.Result)
                {
                    if (info.name == pkg.packageName)
                    {
                        return info.version;
                    }
                }
            }
        #endif
            return null;
        }

        private List<SO_PackageAsset> LoadAllPackageAssets()
        {
            string[] guids = AssetDatabase.FindAssets("t:SO_PackageAsset");
            return guids
                .Select(guid => AssetDatabase.LoadAssetAtPath<SO_PackageAsset>(AssetDatabase.GUIDToAssetPath(guid)))
                .Where(asset => asset != null)
                .ToList();
        }

        private PackageStatus GetPackageStatus(SO_PackageAsset package) {
            // string asmdefPath = AssetDatabase.GUIDToAssetPath(package.asmdefGuid);
            // if (string.IsNullOrEmpty(asmdefPath)) return PackageStatus.NotInstalled;

            string currentVersion = GetInstalledPackageVersion(package.packageName);
            if (string.IsNullOrEmpty(currentVersion)) return PackageStatus.NotInstalled;

            try
            {
                Version current = new Version(currentVersion);
                Version target = new Version(package.version);

                if (current < target)
                    return PackageStatus.NeedsUpdate;
                else
                    return PackageStatus.Installed;
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[Package] 버전 비교 중 오류 발생: {ex.Message}");
                return PackageStatus.Installed; 
            }
        }

        private string GetInstalledPackageVersion(string packageName) {
            var listRequest = UnityEditor.PackageManager.Client.List(true);
            while (!listRequest.IsCompleted) { }

            if (listRequest.Status != UnityEditor.PackageManager.StatusCode.Success)
                return null;

            var installed = listRequest.Result.FirstOrDefault(pkg => pkg.name == packageName || pkg.packageId.Contains(packageName));
            return installed?.version;
        }

        public void OnChangeSelection(int index)
        {
            CacheIndex = index;
            this.EventChangeSelection?.Invoke(index);
        }
    }
}
