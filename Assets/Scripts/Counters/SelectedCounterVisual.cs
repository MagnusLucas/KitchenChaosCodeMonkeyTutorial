using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private KitchenCounter kitchenCounter;
    [SerializeField] private GameObject[] visualGameObjects;

    private void Start() {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
        if (e.selectedCounter == kitchenCounter) {
            Show();
        } else {
            Hide();
        }
    }

    private void Show() {
        foreach (GameObject obj in visualGameObjects) {
            obj.SetActive(true);
        }
    }

    private void Hide() {
        foreach (GameObject obj in visualGameObjects) {
            obj.SetActive(false);
        }
    }
}
