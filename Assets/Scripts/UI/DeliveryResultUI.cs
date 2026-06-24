using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour {

    private const string ANIMATION_TRIGGER = "DeliveredOrder";

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failColor;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failSprite;

    private Animator animator;


    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        DeliveryManager.Instance.OnCorrectOrderDelivered += DeliveryManager_OnCorrectOrderDelivered;
        DeliveryManager.Instance.OnIncorrectOrderDelivered += DeliveryManager_OnIncorrectOrderDelivered;
        Hide();
    }


    private void DeliveryManager_OnIncorrectOrderDelivered(object sender, System.EventArgs e) {
        Show();
        backgroundImage.color = failColor;
        messageText.text = "Delivery\nfailed :(";
        icon.sprite = failSprite;
        animator.SetTrigger(ANIMATION_TRIGGER);
    }

    private void DeliveryManager_OnCorrectOrderDelivered(object sender, System.EventArgs e) {
        Show();
        backgroundImage.color = successColor;
        messageText.text = "Delivery\nsuccess!";
        icon.sprite = successSprite;
        animator.SetTrigger(ANIMATION_TRIGGER);
    }

    private void Show() {
        gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject.SetActive(false);
    }

}
