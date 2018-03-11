using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameProgress {
    public class ButtonChange : MonoBehaviour {
        public Button yourButton;

        public virtual void Start() {
            Begin();
        }

        public virtual void Begin() {
            Button btn = yourButton.GetComponent<Button>();
            btn.onClick.AddListener(TaskOnClick);
        }

        public virtual void TaskOnClick() { }
    }
}
