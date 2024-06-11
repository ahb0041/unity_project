using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ResourceManager
{
    public static void InstantiateAsync(string address)
    {
        
    }


    public static void DownloadDependenciesAsync()
    {
        //Addressables.DownloadDependenciesAsync()
    }
        
    

    public static void LoadAssetAsync<T>(string address)
    {
        Addressables.LoadAssetAsync<T>(address);
    }
}
