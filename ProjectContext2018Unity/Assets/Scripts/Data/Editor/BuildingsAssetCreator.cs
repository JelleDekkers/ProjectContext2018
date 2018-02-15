using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/Buildings")]
    public static void CreateBuildingsAssetFile()
    {
        Buildings asset = CustomAssetUtility.CreateAsset<Buildings>();
        asset.SheetName = "Game Database";
        asset.WorksheetName = "Buildings";
        EditorUtility.SetDirty(asset);        
    }
    
}