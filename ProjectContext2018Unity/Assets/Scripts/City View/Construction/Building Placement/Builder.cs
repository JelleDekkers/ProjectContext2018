using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Construction {

    public class Builder : MonoBehaviour {

        private BuildMode currentBuildMode;

        private void Awake() {
            BuildingPlaceMode.OnModeToggled += SetBuildMode;
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

        private void OnDestroy() {
            BuildingPlaceMode.OnModeToggled -= SetBuildMode;
        }
    }
}