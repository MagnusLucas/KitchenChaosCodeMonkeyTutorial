using UnityEngine;

abstract public class KitchenCounter : MonoBehaviour, IKitchenObjectParent {

    [SerializeField] private Transform kitchenObjectHookPoint;

    private KitchenObject kitchenObject;

    virtual public void Interact(Player player) {
        if (kitchenObject == null && player.HasKitchenObject()) {
            player.GetKitchenObject().SetKitchenObjectParent(this);
            return;
        }
        if (kitchenObject != null && !player.HasKitchenObject()) {
            kitchenObject.SetKitchenObjectParent(player);
        }
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
