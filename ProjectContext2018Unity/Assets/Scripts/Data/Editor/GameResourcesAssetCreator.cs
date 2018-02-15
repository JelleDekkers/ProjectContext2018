using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/GameResources")]
    public static void CreateGameResourcesAssetFile()
    {
        GameResources asset = CustomAssetUtility.CreateAsset<GameResources>();
        asset.SheetName = "Game Database";
        asset.WorksheetName = "GameResources";
        EditorUtility.SetDirty(asset);        
    }
    
}