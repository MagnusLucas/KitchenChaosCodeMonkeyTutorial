using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerManager : MonoBehaviour {
    
    public static AudioMixerManager Instance { get; private set; }

    public enum MixerProperty {
        MASTER_VOLUME,
        SFX_VOLUME,
        MUSIC_VOLUME,
    }

    [SerializeField] private AudioMixer audioMixer;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        LoadPlayerPreferences(); // For some reason this gets overriden between awake and start >,<
    }

    private void LoadPlayerPreferences() {
        foreach( MixerProperty property in Enum.GetValues(typeof(MixerProperty))) {
            if (PlayerPrefs.HasKey(GetMixerPropertyName(property))) {
                AdjustAudioMixerProperty(property, PlayerPrefs.GetFloat(GetMixerPropertyName(property)));
            }
        }
    }

    private string GetMixerPropertyName (MixerProperty property) {
        switch(property) {
            case MixerProperty.MASTER_VOLUME:
                return "MasterVolume";
            case MixerProperty.SFX_VOLUME:
                return "SFXVolume";
            case MixerProperty.MUSIC_VOLUME:
                return "MusicVolume";
        }

        throw new Exception("Non existent audio mixer property!");
    }

    public void AdjustAudioMixerProperty(MixerProperty property, float new_value) {

        float valueForMixer = 20 * Mathf.Log10(new_value);

        audioMixer.SetFloat(GetMixerPropertyName(property), valueForMixer);

        PlayerPrefs.SetFloat(GetMixerPropertyName (property), new_value);
        PlayerPrefs.Save();
    }

    public float GetPlayerMixerPropertyVolumeSetting(MixerProperty property) {
        if (PlayerPrefs.HasKey(GetMixerPropertyName(property))) {
            return PlayerPrefs.GetFloat(GetMixerPropertyName(property));
        }

        return 0.5f;
    }

}
