using Unity.VisualScripting;
using UnityEngine;

public class CuttingCounter : KitchenCounter {

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOs;

    public void SecondaryInteract() {
        if (HasKitchenObject()) {

            CuttingRecipeSO recipe = GetRecipeForObjectOrNull(GetKitchenObject().GetKitchenObjectSO());

            if (recipe != null) {
                GetKitchenObject().DestroySelf();
                KitchenCounter.SpawnKitchenObject(recipe.result, this);
            }

        }
    }

    private CuttingRecipeSO GetRecipeForObjectOrNull(KitchenObjectSO kitchenObjectSO) {
        foreach (CuttingRecipeSO recipe in cuttingRecipeSOs) {
            if (recipe.ingredient == kitchenObjectSO) {
                return recipe;
            }
        }
        return null;
    }

}
