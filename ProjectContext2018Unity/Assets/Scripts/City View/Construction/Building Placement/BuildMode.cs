using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Construction {

    public abstract class BuildMode : MonoBehaviour {
        public abstract Builder Builder { get; set; }
        public abstract void OnStart(Builder builder);
        public abstract void OnEnd();
        public abstract void UpdateMode();
    }
}