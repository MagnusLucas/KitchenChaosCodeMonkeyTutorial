using UnityEngine;

abstract public class KitchenCounter : MonoBehaviour, IKitchenObjectParent {

    [SerializeField] private Transform kitchenObjectHookPoint;

    private KitchenObject kitchenObject;

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent parent) {
        Transform spawnedObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject spawnedObject = spawnedObjectTransform.GetComponent<KitchenObject>();
        spawnedObject.SetKitchenObjectParent(parent);
        return spawnedObject;
    }

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
