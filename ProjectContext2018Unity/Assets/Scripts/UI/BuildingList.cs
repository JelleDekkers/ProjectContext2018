using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CompanyView.UI {
    public class BuildingList : MonoBehaviour {
        public GameObject buttonPrefab;
        public GameObject[] buildingPrefabs;
        
        private RectTransform myRect;
        private AudioSource audioSource;

        // Use this for initialization
        private void Start() {
            audioSource = GetComponent<AudioSource>();
            for(int i = 0 ; i < buildingPrefabs.Length ; i++) {
                // CREATE BUTTON WITH SCRIPTABLE OBJECT PROPERTIES
                GameObject obj = Instantiate<GameObject>(buttonPrefab);
                obj.transform.SetParent(gameObject.transform);
                BuildingContainer container = obj.GetComponent<BuildingContainer>();
                //container.sprite = buildingPrefabs[i].gameObject.GetComponent<Building>().thumbnail;
                //container.clickedSprite = buildingPrefabs[i].gameObject.GetComponent<Building>().thumbnailClicked;
                //container.building = buildingPrefabs[i];
                obj.GetComponent<RectTransform>().position = new Vector2(48 + (i * 100), 48);
                obj.GetComponent<Button>().onClick.AddListener(delegate () { Select(obj.GetComponent<BuildingContainer>()); });
            }
            myRect = GetComponent<RectTransform>();

        }

        // Update is called once per frame
        private void Update() {
            /*else if (Input.GetAxis("Mouse ScrollWheel") < 0 && myRect.localPosition.x > 0 - 100 * (buildingPrefabs.Length) + Screen.width) {
                myRect.position -= new Vector3(scrollSpeed, 0, 0);
            }*/
        }
        private void Select(BuildingContainer building) {
            BuildingSelector.SetBuilding(building);
            
        }
    }
}
