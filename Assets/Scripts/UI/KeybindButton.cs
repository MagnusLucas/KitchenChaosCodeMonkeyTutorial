using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class KeybindButton : MonoBehaviour {

    [SerializeField] private GameInput.Binding binding;

    private TextMeshProUGUI buttonText;
    private Button button;

    private bool awaitingInput = false;

    protected void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            if (!awaitingInput) {
                buttonText.text = "...";
                awaitingInput = true;
                GameInput.Instance.RebindBinding(binding);
            } else {
                UpdateKeybind();
                awaitingInput = false;
            }
        });
    }

    private void Start() {
        GameInput.Instance.OnBindingUpdated += (_, _) => UpdateKeybind();

        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        UpdateKeybind();
    }

    private void UpdateKeybind() {
        buttonText.text = GameInput.Instance.GetKeyboardBinding(binding);
        if (buttonText.text.Length > 3) {
            buttonText.text = buttonText.text.Substring(0, 3);
        }
    }

}
