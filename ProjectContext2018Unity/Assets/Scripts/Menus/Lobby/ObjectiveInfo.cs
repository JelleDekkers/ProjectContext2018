using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveInfo : MonoBehaviour {

    public GameTime gameTime;
    public Text text;

	void Start () {
        text.text += gameTime.MaxYear.ToString();
    }

}
