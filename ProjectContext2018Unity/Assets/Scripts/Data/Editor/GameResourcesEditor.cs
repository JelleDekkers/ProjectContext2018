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
[CustomEditor(typeof(GameResources))]
public class GameResourcesEditor : BaseGoogleEditor<GameResources>
{	    
    public override bool Load()
    {        
        GameResources targetData = target as GameResources;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<GameResourcesData>(targetData.WorksheetName) ?? db.CreateTable<GameResourcesData>(targetData.WorksheetName);
        
        List<GameResourcesData> myDataList = new List<GameResourcesData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            GameResourcesData data = new GameResourcesData();
            
            data = Cloner.DeepCopy<GameResourcesData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
