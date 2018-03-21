using System;
using System.Collections.Generic;
using UnityEngine;
using CityView.Construction;
using CityView.Terrain;

namespace CityView {

    public class City : MonoBehaviour {
        private static City instance;
        public static City Instance {
            get {
                if (instance == null)
                    instance = FindObjectOfType<City>();
                return instance; }
        }

        public static Action OnGameSceneWasLoaded;

        [SerializeField] private Climate climateType;
        public Climate ClimateType { get { return climateType; } }

        [SerializeField] private CityGrid tilesGrid;
        public CityGrid TilesGrid { get { return tilesGrid; } }

        [SerializeField] private GameTerrain terrain;
        public GameTerrain Terrain { get { return terrain; } }

        private void Awake() {
            instance = this;

            if(OnGameSceneWasLoaded != null)
                OnGameSceneWasLoaded();
        }       
    }
}