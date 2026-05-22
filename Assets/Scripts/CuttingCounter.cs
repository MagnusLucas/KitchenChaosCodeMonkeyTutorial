using Unity.VisualScripting;
using UnityEngine;

public class CuttingCounter : KitchenCounter {

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOs;

    public void SecondaryInteract() {
        if (HasKitchenObject()) {
            foreach (CuttingRecipeSO recipe in cuttingRecipeSOs) {
                if (recipe.ingredient == GetKitchenObject().GetKitchenObjectSO()) {
                    GetKitchenObject().DestroySelf();
                    KitchenCounter.SpawnKitchenObject(recipe.result, this);
                    return;
                }
            }
        }
    }

}
