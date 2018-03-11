using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using GDataDB;
using GDataDB.Linq;

using UnityQuickSheet;

///
/// !!! Machine generated code !!!
///
[CustomEditor(typeof(ClimateBuildings))]
public class ClimateBuildingsEditor : BaseGoogleEditor<ClimateBuildings>
{	    
    public override bool Load()
    {        
        ClimateBuildings targetData = target as ClimateBuildings;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<ClimateBuildingsData>(targetData.WorksheetName) ?? db.CreateTable<ClimateBuildingsData>(targetData.WorksheetName);
        
        List<ClimateBuildingsData> myDataList = new List<ClimateBuildingsData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            ClimateBuildingsData data = new ClimateBuildingsData();
            
            data = Cloner.DeepCopy<ClimateBuildingsData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
