using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is a class in which the different city specifications and specializations such as climate are defined.
/// </summary>
public class CityType {

    public enum Climate { Island, Highland, Desert };

    public Climate climate;

    public CityType(Climate climate)
    {
        this.climate = climate;
    }

	// Use this for initialization
	private void Start () {
        
	}
	
	// Update is called once per frame
	private void Update () {
		
	}

    ///<summary>
    ///<para>Call this method to receive Debug messages related to the CityType parameters.</para>
    ///</summary>
    public void DebugCall()
    {
        Debug.Log("Your city has a(n) " + climate + " climate");
    }
}
