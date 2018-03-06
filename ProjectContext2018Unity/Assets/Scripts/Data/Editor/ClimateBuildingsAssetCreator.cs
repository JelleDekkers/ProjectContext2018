using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/ClimateBuildings")]
    public static void CreateClimateBuildingsAssetFile()
    {
        ClimateBuildings asset = CustomAssetUtility.CreateAsset<ClimateBuildings>();
        asset.SheetName = "Game Database";
        asset.WorksheetName = "ClimateBuildings";
        EditorUtility.SetDirty(asset);        
    }
    
}