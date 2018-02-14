using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Construction {

    public abstract class BuildMode : MonoBehaviour {
        public abstract void OnStart();
        public abstract void OnEnd();
        public abstract void Update();
    }
}