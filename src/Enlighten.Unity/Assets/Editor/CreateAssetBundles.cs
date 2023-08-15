using UnityEditor;
using UnityEngine;
using System.IO;

public class CreateAssetBundles
{
    [MenuItem("Assets/Build AssetBundles _F5")]
    static void BuildAllAssetBundles()
    {
        string assetBundleDirectory = "";
        if (PlayerPrefs.HasKey("bundleDir")) assetBundleDirectory = PlayerPrefs.GetString("bundleDir");
        else
        {
            assetBundleDirectory = EditorUtility.OpenFolderPanel("Select Directory", "", "");
            PlayerPrefs.SetString("bundleDir", assetBundleDirectory);
        }

        if (!Directory.Exists(Application.streamingAssetsPath))
            Directory.CreateDirectory(assetBundleDirectory);

        BuildPipeline.BuildAssetBundles(assetBundleDirectory,
        BuildAssetBundleOptions.UncompressedAssetBundle, EditorUserBuildSettings.activeBuildTarget);
    }

    [MenuItem("Assets/Clear Asset Bundle Location")]
    static void ClearAssetBundleLocation()
	{
        PlayerPrefs.DeleteKey("bundleDir");
	}
}