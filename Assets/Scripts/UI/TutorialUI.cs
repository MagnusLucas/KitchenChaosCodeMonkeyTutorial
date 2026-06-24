using UnityEngine;

public class TutorialUI : MonoBehaviour {

    private void Start() {
        GameInput.Instance.OnInteractAction += (_, _) => Hide();
    }

    private void Show() {
        gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject.SetActive(false);
    }
}
