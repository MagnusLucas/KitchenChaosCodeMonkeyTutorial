using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {

    [SerializeField] private KitchenCounter implementeeOfIProgressAction; // this is so sad
    [SerializeField] private Image barImage;

    private IProgressAction progressAction;

    private void Awake() {
        progressAction = (IProgressAction)implementeeOfIProgressAction;
    }

    private void Start() {
        progressAction.OnProgressUpdated += ProgressAction_OnProgressUpdated;
        implementeeOfIProgressAction.OnKitchenObjectReceived += ImplementeeOfIProgressAction_OnKitchenObjectReceived;
        implementeeOfIProgressAction.OnKitchenObjectRemoved += ImplementeeOfIProgressAction_OnKitchenObjectRemoved;

        Hide();
    }

    private void ImplementeeOfIProgressAction_OnKitchenObjectRemoved(object sender, System.EventArgs e) {
        Hide();
        ResetProgress();
    }

    private void ImplementeeOfIProgressAction_OnKitchenObjectReceived(object sender, System.EventArgs e) {
        ResetProgress();
        Show();
    }

    private void ProgressAction_OnProgressUpdated(object sender, ProgressEventArgs e) {
        barImage.fillAmount = e.progress;
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void ResetProgress() {
        barImage.fillAmount = 0;
    }
}
