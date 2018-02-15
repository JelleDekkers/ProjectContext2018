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
  float costmoney;
  public float Costmoney { get {return costmoney; } set { costmoney = value;} }
  
  [SerializeField]
  float researchpointsneeded;
  public float Researchpointsneeded { get {return researchpointsneeded; } set { researchpointsneeded = value;} }
  
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
  float incomeresearch;
  public float Incomeresearch { get {return incomeresearch; } set { incomeresearch = value;} }
  
  [SerializeField]
  int[] incomeresources = new int[0];
  public int[] Incomeresources { get {return incomeresources; } set { incomeresources = value;} }
  
  [SerializeField]
  float[] productionamount = new float[0];
  public float[] Productionamount { get {return productionamount; } set { productionamount = value;} }
  
}