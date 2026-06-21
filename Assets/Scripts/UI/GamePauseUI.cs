using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour {
    
    public static GamePauseUI Instance { get; private set; }

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake() {
        Instance = this;
        resumeButton.onClick.AddListener(() => { KitchenGameManager.Instance.UnpauseGame(); Hide(); });
        optionsButton.onClick.AddListener(() => { gameObject.SetActive(false); OptionsUI.Instance.Show(); }); //temp
        mainMenuButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.MAIN_MENU); Time.timeScale = 1.0f; });
    }

    private void Start() {
        KitchenGameManager.Instance.OnGamePaused += (_, _) => { Show(); };
        KitchenGameManager.Instance.OnGameUnpaused += (_, _) => { Hide(); };

        Hide();
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }


}
