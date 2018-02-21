using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Construction {

    public class Builder : MonoBehaviour {

        [SerializeField] private PlaceMode placeMode;
        [SerializeField] private DestroyMode destroyMode;

        private BuildMode currentBuildMode;


        private void Awake() {
            placeMode.enabled = false;
            destroyMode.enabled = false;
        }

        private void SetBuildMode(BuildMode mode) {
            if (currentBuildMode != null) {
                currentBuildMode.OnEnd();
            } else {
                currentBuildMode = mode;
                currentBuildMode.OnStart();
                return;
            }

            // toggle:
            if (currentBuildMode.GetType() != mode.GetType()) {
                currentBuildMode = mode;
                currentBuildMode.OnStart();
            } else {
                currentBuildMode = null;
            }
        }

        private void OnGUI() {
            if (GUI.Button(new Rect(10, 10, 150, 20), "Build Mode"))
                SetBuildMode(placeMode);
            if (GUI.Button(new Rect(10, 30, 150, 20), "Destroy Mode"))
                SetBuildMode(destroyMode);
        }
    }
}