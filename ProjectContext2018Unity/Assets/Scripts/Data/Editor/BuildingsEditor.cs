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
[CustomEditor(typeof(Buildings))]
public class BuildingsEditor : BaseGoogleEditor<Buildings>
{	    
    public override bool Load()
    {        
        Buildings targetData = target as Buildings;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<BuildingsData>(targetData.WorksheetName) ?? db.CreateTable<BuildingsData>(targetData.WorksheetName);
        
        List<BuildingsData> myDataList = new List<BuildingsData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            BuildingsData data = new BuildingsData();
            
            data = Cloner.DeepCopy<BuildingsData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
