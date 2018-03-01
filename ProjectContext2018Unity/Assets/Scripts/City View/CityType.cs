using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView
{
    /// <summary>
    /// This is a class in which the different city specifications and specializations such as climate are defined.
    /// </summary>
    public class CityType
    {

        public enum Climate { Island, Highland, Desert };

        public Climate climate;

        public CityType(Climate climate)
        {
            this.climate = climate;
        }

        // Use this for initialization
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {

        }

        ///<summary>
        ///<para>Call this method to receive Debug messages related to the CityType parameters.</para>
        ///</summary>
        public void DebugCall()
        {
            Debug.Log("Your city has a(n) " + climate + " climate");
        }

        public Color GetClimateCameraFillColor()
        {
            Color color = Color.green;                                                  // Default Color
            if (climate == Climate.Island) color = new Color(0, 155, 198) / 255;   // Blue - Sea Color
            else if (climate == Climate.Desert) color = new Color(193, 154, 107) / 255; // Yellow - Brown -ish Color
            else if (climate == Climate.Highland) color = new Color(104, 161, 129) / 255; // Green, Cold Color
            return color;
        }

        public Color GetClimateBaseMaterialColor()
        {
            Color color = Color.white;                                                          // Default Color
            if (climate == Climate.Island) color = new Color(240, 230, 140) / 255;       // Shore / Sand Color
            else if (climate == Climate.Desert) color = new Color(253, 227, 141) / 255;       // Bright Sand Color
            else if (climate == Climate.Highland) color = new Color(116, 88, 62) / 255;         // Brown Cold Dirt Color
            return color;
        }

    }
}
