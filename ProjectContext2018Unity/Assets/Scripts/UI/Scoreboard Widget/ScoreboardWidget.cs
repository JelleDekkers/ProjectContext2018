using System.Linq;
using UnityEngine;

namespace UI {

    public class ScoreboardWidget : MonoBehaviour {

        [SerializeField] private ScoreboardWidgetItem itemPrefab;
        [SerializeField] private Transform grid;

        private ScoreboardWidgetItem[] items;

        public void OnEnable() {
            FillScoreboard();
        }

        private void FillScoreboard() {
            grid.RemoveChildren();

            items = new ScoreboardWidgetItem[PlayerList.Instance.Players.Count];
            int index = 0;
            foreach(Player player in PlayerList.Instance.Players) {
                ScoreboardWidgetItem item = Instantiate(itemPrefab, grid);
                item.Init(player, player.ScoreManager, this);
                items[index] = item;
                index++;
            }
        }

        public void UpdateListOrder() {
            items.OrderBy(go => go.Player.ScoreManager.Score).ToArray();
            //for(int i = 0; i < items.Length; i++) {
            //    items[i].transform.SetSiblingIndex(i);
            //}
        }
    }
}