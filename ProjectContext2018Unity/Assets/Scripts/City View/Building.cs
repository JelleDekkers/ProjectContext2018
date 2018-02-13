using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    public IntVector2 Size { get; private set; }

    private void Start() {
        Size = CalculateSize(this);
    }

    public bool CanBeBought() {
        throw new System.NotImplementedException();
    }

    public static IntVector2 CalculateSize(Building b) {
        IntVector2 calcSize = IntVector2.Zero;
        Renderer r = b.GetComponent<Renderer>();
        calcSize.x = (int)r.bounds.size.x;
        calcSize.z = (int)r.bounds.size.z;
        return calcSize;
    }
}
