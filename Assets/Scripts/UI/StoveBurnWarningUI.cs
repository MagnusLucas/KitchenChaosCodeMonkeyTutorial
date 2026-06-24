using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour {

    const float SHOW_THRESHOLD = 0.6f;

    [SerializeField] private StoveCounter stoveCounter;

    private void Start() {
        stoveCounter.OnProgressUpdated += StoveCounter_OnProgressUpdated;
        stoveCounter.OnKitchenObjectRemoved += (_, _) => Hide();
        stoveCounter.OnFullyFried += (_, _) => Hide();
        Hide();
    }

    private void StoveCounter_OnProgressUpdated(object sender, ProgressEventArgs e) {
        if (stoveCounter.IsBurningObject() && e.progress > SHOW_THRESHOLD) {
            Show();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject.SetActive(false);
    }
}
