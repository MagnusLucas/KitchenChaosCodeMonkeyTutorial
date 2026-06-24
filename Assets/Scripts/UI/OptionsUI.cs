using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {

    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button secondaryInteractButton;
    [SerializeField] private Button pauseButton;


    private Action onCloseButtonAction;

    private void Awake() {
        Instance = this;

        masterSlider.onValueChanged.AddListener((float value) => {
            AudioMixerManager.Instance.AdjustAudioMixerProperty(AudioMixerManager.MixerProperty.MASTER_VOLUME, value);
        });
        sfxSlider.onValueChanged.AddListener((float value) => {
            AudioMixerManager.Instance.AdjustAudioMixerProperty(AudioMixerManager.MixerProperty.SFX_VOLUME, value);
        });
        musicSlider.onValueChanged.AddListener((float value) => {
            AudioMixerManager.Instance.AdjustAudioMixerProperty(AudioMixerManager.MixerProperty.MUSIC_VOLUME, value);
        });

        closeButton.onClick.AddListener(() => { Hide(); onCloseButtonAction(); });

    }

    private void Start() {
        KitchenGameManager.Instance.OnGameUnpaused += (_, _) => { Hide(); };
        Hide();
    }


    private void SetCurrentSliderValues() {
        masterSlider.value = AudioMixerManager.Instance.GetMixerPropertyValue(AudioMixerManager.MixerProperty.MASTER_VOLUME);
        sfxSlider.value = AudioMixerManager.Instance.GetMixerPropertyValue(AudioMixerManager.MixerProperty.SFX_VOLUME);
        musicSlider.value = AudioMixerManager.Instance.GetMixerPropertyValue(AudioMixerManager.MixerProperty.MUSIC_VOLUME);
    }

    public void Show(Action onColseButtonAction) {
        this.onCloseButtonAction = onColseButtonAction;
        SetCurrentSliderValues();
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}
