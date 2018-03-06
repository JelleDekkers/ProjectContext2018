using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
///
[System.Serializable]
public class ClimateBuildingsData
{
  [SerializeField]
  int id;
  public int ID { get {return id; } set { id = value;} }
  
  [SerializeField]
  string name;
  public string Name { get {return name; } set { name = value;} }
  
  [SerializeField]
  float moneycost;
  public float Moneycost { get {return moneycost; } set { moneycost = value;} }
  
  [SerializeField]
  int[] resourcecost = new int[0];
  public int[] Resourcecost { get {return resourcecost; } set { resourcecost = value;} }
  
  [SerializeField]
  int[] resourcecostamount = new int[0];
  public int[] Resourcecostamount { get {return resourcecostamount; } set { resourcecostamount = value;} }
  
  [SerializeField]
  Climate climate;
  public Climate Climate { get {return climate; } set { climate = value;} }
  
}