using Unity.VisualScripting;
using UnityEngine;

public class CuttingCounter : KitchenCounter {

    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;

    public void SecondaryInteract() {
        if (HasKitchenObject()) {
            GetKitchenObject().DestroySelf();
            KitchenCounter.SpawnKitchenObject(cutKitchenObjectSO).SetKitchenObjectParent(this);
        }
    }

}
