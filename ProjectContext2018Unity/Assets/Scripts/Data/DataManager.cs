using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data Manager", menuName = "Tools/Data Manager", order = 0)]
public class DataManager : ScriptableObjectSingleton<DataManager> {

    public GameResources gameResources;
}
