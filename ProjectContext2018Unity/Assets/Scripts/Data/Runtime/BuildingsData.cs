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
  float moneycost;
  public float Moneycost { get {return moneycost; } set { moneycost = value;} }
  
  [SerializeField]
  int[] resourcecost = new int[0];
  public int[] Resourcecost { get {return resourcecost; } set { resourcecost = value;} }
  
  [SerializeField]
  int[] resourcecostamount = new int[0];
  public int[] Resourcecostamount { get {return resourcecostamount; } set { resourcecostamount = value;} }
  
  [SerializeField]
  float moneyinput;
  public float Moneyinput { get {return moneyinput; } set { moneyinput = value;} }
  
  [SerializeField]
  int[] resourceinput = new int[0];
  public int[] Resourceinput { get {return resourceinput; } set { resourceinput = value;} }
  
  [SerializeField]
  int[] resourceinputamount = new int[0];
  public int[] Resourceinputamount { get {return resourceinputamount; } set { resourceinputamount = value;} }
  
  [SerializeField]
  float moneyoutput;
  public float Moneyoutput { get {return moneyoutput; } set { moneyoutput = value;} }
  
  [SerializeField]
  int[] resourceoutput = new int[0];
  public int[] Resourceoutput { get {return resourceoutput; } set { resourceoutput = value;} }
  
  [SerializeField]
  int[] resourceoutputamount = new int[0];
  public int[] Resourceoutputamount { get {return resourceoutputamount; } set { resourceoutputamount = value;} }
  
  [SerializeField]
  float productiontime;
  public float Productiontime { get {return productiontime; } set { productiontime = value;} }
  
  [SerializeField]
  Climate climate;
  public Climate Climate { get {return climate; } set { climate = value;} }
  
  [SerializeField]
  BuildingType buildingtype;
  public BuildingType BuildingType { get {return buildingtype; } set { buildingtype = value;} }

    public object Clone() {
        return MemberwiseClone();
    }
}