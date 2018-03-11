using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameProgress {
    public class ButtonChangeScene : ButtonChange {
        public override void Start() {
            base.Start();
        }
        public override void TaskOnClick() {
            SceneManager.LoadScene(SceneManager.GameScene, SceneManager.SingleLoad);
        }
    }
}
