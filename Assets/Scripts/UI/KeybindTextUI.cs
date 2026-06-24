using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class KeybindTextUI : MonoBehaviour {

    [SerializeField] private GameInput.Binding binding;

    private TextMeshProUGUI textMeshProUGUI;

    private void Start() {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();

        GameInput.Instance.OnBindingUpdated += (_, _) => UpdateKeybind();

        UpdateKeybind();
    }

    private void UpdateKeybind() {
        textMeshProUGUI.text = GameInput.Instance.GetKeyboardBinding(binding);
        if (textMeshProUGUI.text.Length > 3) {
            textMeshProUGUI.text = textMeshProUGUI.text.Substring(0, 3);
        }
    }

}
