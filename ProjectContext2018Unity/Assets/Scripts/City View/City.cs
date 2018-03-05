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

        private CityType type;
        public CityType Type {
            get {
                return type;
            }

            private set {
                type = value;
            }
        }

        [SerializeField] private CityGrid tilesGrid;
        public CityGrid TilesGrid { get { return tilesGrid; } }

        [SerializeField] private GameTerrain terrain;
        public GameTerrain Terrain { get { return terrain; } }

        private void Awake() {
            instance = this;

            // Climate type is still randomly assigned, it still needs to check whether certain "Climates" have already been claimed by other players.
            Type = new CityType((CityType.Climate)UnityEngine.Random.Range(0, (Enum.GetNames(typeof(CityType.Climate)).Length)));
            //Type.DebugCall();
        }       
    }
}