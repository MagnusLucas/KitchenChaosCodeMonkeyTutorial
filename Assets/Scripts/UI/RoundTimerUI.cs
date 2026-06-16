using UnityEngine;
using UnityEngine.UI;

public class RoundTimerUI : MonoBehaviour {

    [SerializeField] private Image image;

    private bool gameInProgress = false;

    private void Awake() {
        image.fillAmount = 0;
    }

    private void Start() {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e) {
        gameInProgress = KitchenGameManager.Instance.IsGamePlaying();
    }

    private void Update() {
        if (!gameInProgress) return;
        image.fillAmount = KitchenGameManager.Instance.GetGameProgress();
    }

}
