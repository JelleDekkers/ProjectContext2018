using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView {

    public class Building : MonoBehaviour {

        private IntVector2? size;
        public IntVector2 Size { get {
                if (!size.HasValue)
                    size = CalculateSize();
                return size.Value;
            }
        }

        public bool CanBeBought() {
            throw new System.NotImplementedException();
        }

        public IntVector2 CalculateSize() {
            IntVector2 calcSize = IntVector2.Zero;
            Renderer r = transform.GetChild(0).GetComponent<Renderer>();
            calcSize.x = (int)Mathf.Round(r.bounds.size.x);
            calcSize.z = (int)Mathf.Round(r.bounds.size.z);
            return calcSize;
        }
    }
}