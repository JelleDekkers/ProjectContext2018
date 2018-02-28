using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
///
[System.Serializable]
public class BuildingsData
{
  [SerializeField]
  int id;
  public int ID { get {return id; } set { id = value;} }
  
  [SerializeField]
  string name;
  public string Name { get {return name; } set { name = value;} }
  
  [SerializeField]
  float pollution;
  public float Pollution { get {return pollution; } set { pollution = value;} }
  
  [SerializeField]
  float costmoney;
  public float Costmoney { get {return costmoney; } set { costmoney = value;} }
  
  [SerializeField]
  int[] resourcecost = new int[0];
  public int[] Resourcecost { get {return resourcecost; } set { resourcecost = value;} }
  
  [SerializeField]
  float[] resourcecostamount = new float[0];
  public float[] Resourcecostamount { get {return resourcecostamount; } set { resourcecostamount = value;} }
  
  [SerializeField]
  float incomemoney;
  public float Incomemoney { get {return incomemoney; } set { incomemoney = value;} }
  
  [SerializeField]
  int[] incomeresources = new int[0];
  public int[] Incomeresources { get {return incomeresources; } set { incomeresources = value;} }
  
  [SerializeField]
  float[] incomeresourcesamount = new float[0];
  public float[] Incomeresourcesamount { get {return incomeresourcesamount; } set { incomeresourcesamount = value;} }
  
  [SerializeField]
  float productiontime;
  public float Productiontime { get {return productiontime; } set { productiontime = value;} }
  
  [SerializeField]
  string climateexclusivity;
  public string Climateexclusivity { get {return climateexclusivity; } set { climateexclusivity = value;} }
  
}