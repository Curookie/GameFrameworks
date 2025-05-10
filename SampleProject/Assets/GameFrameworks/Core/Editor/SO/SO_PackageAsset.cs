using UnityEngine;

namespace GameFrameworks.Editors
{
    public enum PackageSourceType
    {
        Git,
        UnityRegistry,
        AssetStore
    }

    [CreateAssetMenu(fileName = "NewPackageInfo", menuName = "SO/PackageInfo")]
    public class SO_PackageAsset : ScriptableObject
    {
        [Header("공통 정보")]
        public string displayName;
        public string version;
        public string asmdefGuid;
        public PackageSourceType sourceType;

        [Header("Git 패키지용")]
        public string gitUrl;

        [Header("Unity Registry 패키지용")]
        public string unityPackageName; // 예: com.unity.inputsystem

        [Header("Asset Store 에셋용")]
        public string assetStoreId; // 추후 Unity Asset Store API 연동용

        /// <summary>
        /// 현재 소스에 따른 패키지 이름
        /// </summary>
        public string packageName
        {
            get
            {
                return sourceType switch
                {
                    PackageSourceType.Git => gitUrl,
                    PackageSourceType.UnityRegistry => unityPackageName,
                    PackageSourceType.AssetStore => assetStoreId,
                    _ => null,
                };
            }
        }

        /// <summary>
        /// UPM 설치 키 (Client.Add 시 사용)
        /// </summary>
        public string InstallKey
        {
            get
            {
                return sourceType switch
                {
                    PackageSourceType.Git => gitUrl,
                    PackageSourceType.UnityRegistry => $"{unityPackageName}@{version}",
                    PackageSourceType.AssetStore => null,
                    _ => null,
                };
            }
        }

        /// <summary>
        /// 설치 가능한 패키지 여부
        /// </summary>
        public bool IsInstallable
        {
            get
            {
                return sourceType switch
                {
                    PackageSourceType.Git => !string.IsNullOrEmpty(gitUrl),
                    PackageSourceType.UnityRegistry => !string.IsNullOrEmpty(unityPackageName),
                    PackageSourceType.AssetStore => false,
                    _ => false,
                };
            }
        }
    }
}