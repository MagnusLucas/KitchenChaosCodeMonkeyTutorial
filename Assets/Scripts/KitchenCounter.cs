using System;
using UnityEngine;

abstract public class KitchenCounter : MonoBehaviour, IKitchenObjectParent {

    virtual public event EventHandler OnKitchenObjectReceived;
    virtual public event EventHandler OnKitchenObjectRemoved;

    [SerializeField] private Transform kitchenObjectHookPoint;

    private KitchenObject kitchenObject;

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent parent) {
        Transform spawnedObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject spawnedObject = spawnedObjectTransform.GetComponent<KitchenObject>();
        spawnedObject.SetKitchenObjectParent(parent);
        return spawnedObject;
    }

    virtual public void Interact(Player player) {
        if (!HasKitchenObject() && player.HasKitchenObject()) {
            GetKitchenObject(player);
            return;
        }
        if (HasKitchenObject() && !player.HasKitchenObject()) {
            GiveKitchenObject(player);
        }
    }

    virtual public void GetKitchenObject(Player player) {
        player.GetKitchenObject().SetKitchenObjectParent(this);
        OnKitchenObjectReceived?.Invoke(this, EventArgs.Empty);
    }

    virtual public void GiveKitchenObject(Player player) {
        kitchenObject.SetKitchenObjectParent(player);
        OnKitchenObjectRemoved?.Invoke(this, EventArgs.Empty);
    }

    public Transform GetKitchenObjectHookPoint() {
        return kitchenObjectHookPoint;
    }

    public void SetKitchenObject(KitchenObject newKitchenObject) {
        kitchenObject = newKitchenObject;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return (kitchenObject != null);
    }
}
