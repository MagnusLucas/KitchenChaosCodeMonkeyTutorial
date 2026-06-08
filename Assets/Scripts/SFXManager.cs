using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class SFXManager : MonoBehaviour {

    [SerializeField] private List<AudioClip> correctOrderDeliveredSFXs;
    [SerializeField] private List<AudioClip> incorrectOrderDeliveredSFXs;
    [SerializeField] private List<AudioClip> cutPerformedSFXs;
    [SerializeField] private List<AudioClip> objectPickedUpSFXs;
    [SerializeField] private List<AudioClip> objectPlacedOnCounterSFXs;
    [SerializeField] private List<AudioClip> objectTrashedSFXs;


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
        SimpleSound(PickRandom(objectTrashedSFXs), counter.transform.position);
    }

    private void KitchenCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e) {
        KitchenCounter counter = sender as KitchenCounter;
        SimpleSound(PickRandom(objectPlacedOnCounterSFXs), counter.transform.position);
    }

    private void Player_OnPickedUpObject(object sender, System.EventArgs e) {
        SimpleSound(PickRandom(objectPickedUpSFXs), Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCounterCutPerformed(object sender, System.EventArgs e) {
        CuttingCounter counter = sender as CuttingCounter;
        SimpleSound(PickRandom(cutPerformedSFXs), counter.transform.position);
    }

    private void DeliveryManager_OnIncorrectOrderDelivered(object sender, System.EventArgs e) {
        SimpleSound(PickRandom(incorrectOrderDeliveredSFXs), DeliveryCounter.Instance.transform.position);
    }

    private void DeliveryManager_OnCorrectOrderDelivered(object sender, System.EventArgs e) {
        SimpleSound(PickRandom(correctOrderDeliveredSFXs), DeliveryCounter.Instance.transform.position);
    }

    private AudioClip PickRandom(List<AudioClip> audioClips) {
        var length = audioClips.Count;
        return audioClips[Random.Range(0, length)];
    }

    private void SimpleSound(AudioClip audioClip, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

}
