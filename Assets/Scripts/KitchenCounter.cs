using UnityEngine;

abstract public class KitchenCounter : MonoBehaviour, IKitchenObjectParent {

    [SerializeField] private Transform kitchenObjectHookPoint;

    private KitchenObject kitchenObject;

    abstract public void Interact(Player player);


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
