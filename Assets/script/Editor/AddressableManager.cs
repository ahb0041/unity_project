using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

public static class AddressableManager
{



    [MenuItem("Tools/Build Addressables")]
    public static void BuildAddressables()
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        var buildProfileId = settings.profileSettings.GetProfileId("Default");
        settings.activeProfileId = buildProfileId;

        AddressableAssetSettings.CleanPlayerContent();
        AddressableAssetSettings.BuildPlayerContent();
    }



    public static void UpdateAddressables()
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        var entries = settings.groups.SelectMany(group => group.entries);
        foreach (var entry in entries)
        {
            
        }



    }












    [MenuItem("Tools/Convert Prefab To Addressable")]
    public static void ConvertPrefabToAddressable()
    {
        // Addressable Asset 설정 가져오기
        AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;



        // 프리펩이 저장된 경로
        string prefabFolderPath = "Assets/project/resource/prefab";

        // 폴더내의 모든 프리펩 가져오기
        string[] prefabPaths = Directory.GetFiles(prefabFolderPath, "*.prefab", SearchOption.AllDirectories);

        var idx = 0;
        var max = prefabPaths.Length;

        foreach (string prefabPath in prefabPaths)
        {
            var path = prefabPath.Replace("\\", "/");
            // 프로그래스바 표시
            var rate = (float)idx / max;
            EditorUtility.DisplayProgressBar("Convert Prefab To Addressable", "Converting...", rate);

            // load prefab
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            // Addressable Asset 이름 설정
            string assetName = Path.GetFileNameWithoutExtension(path);

            // Addressable Asset 주소 설정
            string assetAddress = path.Replace("Assets/project/resource/", "").Replace(".prefab", "");

            // 프리펩을 Addressable Asset로 등록
            AddressableAssetEntry assetEntry = settings.CreateOrMoveEntry(AssetDatabase.AssetPathToGUID(path), settings.DefaultGroup, false, false);

            //Addressable Asset 이름, 주소 설정
            assetEntry.SetLabel("MyLabel", true, true);
            assetEntry.SetAddress(assetAddress);

            idx++;
        }

        settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, null, true);
        AssetDatabase.SaveAssets();

        EditorUtility.ClearProgressBar();
    }

}
