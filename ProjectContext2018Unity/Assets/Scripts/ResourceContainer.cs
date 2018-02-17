using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Container class for a production result
/// </summary>
public class ResourceContainer {

    public int id;
    public float amount;

    /// <summary>
    /// Container class for a production result
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="amount">Amount of product</param>
    public ResourceContainer(int id, float amount) {
        this.id = id;
        this.amount = amount;
    }
}
