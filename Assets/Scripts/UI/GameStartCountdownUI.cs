using System;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour {

    private const string ANIMATOR_TRIGGER = "NumberPopup";

    [SerializeField] private TextMeshProUGUI countdownText;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start() {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        Hide();
    }

    private void Update() {
        string newText = Math.Ceiling(KitchenGameManager.Instance.GetTimeToStart()).ToString();
        if (!countdownText.text.Equals(newText)) {
            animator.SetTrigger(ANIMATOR_TRIGGER);
            SFXManager.Instance.PlayCountdownSound();
        }
        countdownText.text = newText;
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (KitchenGameManager.Instance.IsCountdownToStartActive()) {
            Show();
        } else {
            Hide();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
        SFXManager.Instance.PlayCountdownSound();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
