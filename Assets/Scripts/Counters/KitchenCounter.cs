using System;
using UnityEngine;

abstract public class KitchenCounter : MonoBehaviour, IKitchenObjectParent {

    public static event EventHandler OnAnyObjectPlacedHere;

    virtual public event EventHandler OnKitchenObjectReceived;
    virtual public event EventHandler OnKitchenObjectRemoved;

    [SerializeField] private Transform kitchenObjectHookPoint;

    private KitchenObject kitchenObject;

    public static void ResetStaticData() {
        OnAnyObjectPlacedHere = null;
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent parent) {
        Transform spawnedObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject spawnedObject = spawnedObjectTransform.GetComponent<KitchenObject>();
        spawnedObject.SetKitchenObjectParent(parent);
        return spawnedObject;
    }

    virtual public void Interact(Player player) {
        if (player.HasKitchenObject()) {

            if (!HasKitchenObject()) {
                GetKitchenObject(player);
                return;
            }

            if (player.GetKitchenObject() is PlateKitchenObject) {
                if (GetKitchenObject() is PlateKitchenObject) {
                    return;
                }
                InteractWithPlate(player.GetKitchenObject() as PlateKitchenObject, GetKitchenObject());

                return;
            }

            if (GetKitchenObject() is PlateKitchenObject) {
                InteractWithPlate(GetKitchenObject() as PlateKitchenObject, player.GetKitchenObject());
            }

            return;
        }

        if (HasKitchenObject()) {
            GiveKitchenObject(player);
            return;
        }

        InteractNoObjects(player);
    }

    virtual public void InteractNoObjects(Player player) {
    }

    virtual public void InteractWithPlate(PlateKitchenObject plate, KitchenObject kitchenObject) {
        bool hadKitchenObject = HasKitchenObject();
        plate.AddIngredient(kitchenObject.GetKitchenObjectSO());
        kitchenObject.DestroySelf();
        if (hadKitchenObject && !HasKitchenObject()) {
            OnKitchenObjectRemoved?.Invoke(this, EventArgs.Empty);
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

        if (newKitchenObject != null) {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }

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
