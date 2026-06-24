using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class SFXManager : MonoBehaviour {

    public static SFXManager Instance { get; private set;  }

    [SerializeField] private List<AudioClip> correctOrderDeliveredSFXs;
    [SerializeField] private List<AudioClip> incorrectOrderDeliveredSFXs;
    [SerializeField] private List<AudioClip> cutPerformedSFXs;
    [SerializeField] private List<AudioClip> objectPickedUpSFXs;
    [SerializeField] private List<AudioClip> objectPlacedOnCounterSFXs;
    [SerializeField] private List<AudioClip> objectTrashedSFXs;
    [SerializeField] private AudioClip countdownSound;

    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        Instance = this;
    }


    private void Start() {
        DeliveryManager.Instance.OnCorrectOrderDelivered += DeliveryManager_OnCorrectOrderDelivered;
        DeliveryManager.Instance.OnIncorrectOrderDelivered += DeliveryManager_OnIncorrectOrderDelivered;
        CuttingCounter.OnAnyCounterCutPerformed += CuttingCounter_OnAnyCounterCutPerformed;
        Player.Instance.OnPickedUpObject += Player_OnPickedUpObject;
        KitchenCounter.OnAnyObjectPlacedHere += KitchenCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnObjectTrashed += TrashCounter_OnObjectTrashed;
    }

    private void TrashCounter_OnObjectTrashed(object sender, System.EventArgs e) {
        TrashCounter counter = sender as TrashCounter;
        PlaySound(PickRandom(objectTrashedSFXs), counter.transform.position);
    }

    private void KitchenCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e) {
        KitchenCounter counter = sender as KitchenCounter;
        PlaySound(PickRandom(objectPlacedOnCounterSFXs), counter.transform.position);
    }

    private void Player_OnPickedUpObject(object sender, System.EventArgs e) {
        PlaySound(PickRandom(objectPickedUpSFXs), Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCounterCutPerformed(object sender, System.EventArgs e) {
        CuttingCounter counter = sender as CuttingCounter;
        PlaySound(PickRandom(cutPerformedSFXs), counter.transform.position);
    }

    private void DeliveryManager_OnIncorrectOrderDelivered(object sender, System.EventArgs e) {
        PlaySound(PickRandom(incorrectOrderDeliveredSFXs), DeliveryCounter.Instance.transform.position);
    }

    private void DeliveryManager_OnCorrectOrderDelivered(object sender, System.EventArgs e) {
        PlaySound(PickRandom(correctOrderDeliveredSFXs), DeliveryCounter.Instance.transform.position);
    }

    private AudioClip PickRandom(List<AudioClip> audioClips) {
        var length = audioClips.Count;
        return audioClips[Random.Range(0, length)];
    }


    // Naive implementation - trusts there is only one sfx at a time. True for now.
    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f) {
        transform.position = position;
        audioSource.generator = audioClip;
        audioSource.Play();
        
    }

    public void PlayCountdownSound() {
        PlaySound(countdownSound, Player.Instance.transform.position);
    }

}
