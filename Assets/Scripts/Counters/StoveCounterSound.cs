using UnityEngine;

public class StoveCounterSound : MonoBehaviour {

    [SerializeField] private StoveCounter stoveCounter;


    private void Start() {
        stoveCounter.OnFullyFried += StoveCounter_OnFullyFried;
        stoveCounter.OnKitchenObjectReceived += StoveCounter_OnKitchenObjectReceived;
        stoveCounter.OnKitchenObjectRemoved += StoveCounter_OnKitchenObjectRemoved;
        StopPlaying();
    }

    private void StoveCounter_OnKitchenObjectRemoved(object sender, System.EventArgs e) {
        StopPlaying();
    }

    private void StoveCounter_OnKitchenObjectReceived(object sender, System.EventArgs e) {
        StartPlaying();
    }

    private void StoveCounter_OnFullyFried(object sender, System.EventArgs e) {
        StopPlaying();
    }

    private void StartPlaying() {
        gameObject.SetActive(true);
    }

    private void StopPlaying() {
        gameObject.SetActive(false);
    }

}
