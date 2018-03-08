using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Visuals
{
    public class CityCameraVisuals : MonoBehaviour
    {
        private City city;
        private Camera mainCam;
        // Use this for initialization
        private void Start()
        {
            mainCam = Camera.main;
            city = FindObjectOfType<City>();
            InitVisuals();
        }

        // Update is called once per frame
        private void Update()
        {

        }

        private void InitVisuals()
        {
            Color cameraFillColor;
            //cameraFillColor = city.ClimateType.GetClimateCameraFillColor();
            //mainCam.backgroundColor = cameraFillColor;
        }
    }
}
