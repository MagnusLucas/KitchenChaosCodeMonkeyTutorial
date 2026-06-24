using System;
using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour {

    private const float SHOW_THRESHOLD = 0.6f;
    private const float TIME_BETWEEN_WARNING_SOUNDS = 0.6f;

    [SerializeField] private StoveCounter stoveCounter;
    private float timer;

    private void Start() {
        stoveCounter.OnProgressUpdated += StoveCounter_OnProgressUpdated;
        stoveCounter.OnKitchenObjectRemoved += (_, _) => Hide();
        stoveCounter.OnFullyFried += (_, _) => Hide();
        Hide();
    }

    private void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            timer = TIME_BETWEEN_WARNING_SOUNDS;
            SFXManager.Instance.PlayWarningSound(stoveCounter.transform.position);
        }
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
        timer = 0;
        gameObject.SetActive(false);
    }
}
