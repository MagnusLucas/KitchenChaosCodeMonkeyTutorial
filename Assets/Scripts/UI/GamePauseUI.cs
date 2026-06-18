using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour {
    
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;


    private void Awake() {
        resumeButton.onClick.AddListener(() => { KitchenGameManager.Instance.UnpauseGame(); ToggleVisibility(); });
        mainMenuButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.MAIN_MENU); Time.timeScale = 1.0f; });
    }

    private void Start() {
        GameInput.Instance.OnPausePressed += (_, _) => { ToggleVisibility(); };
        gameObject.SetActive(false);
    }

    private void ToggleVisibility() {
        gameObject.SetActive(!gameObject.activeSelf);
    }

}
