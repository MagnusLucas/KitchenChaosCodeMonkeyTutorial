using System;
using Unity.VisualScripting;
using UnityEngine;

public class CuttingCounter : KitchenCounter {

    public event EventHandler OnCutPerformed;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOs;
    private int cuttingProgress;

    public override void Interact(Player player) {
        base.Interact(player);
        cuttingProgress = 0;
    }

    public void SecondaryInteract() {
        if (HasKitchenObject()) {
            cuttingProgress++;
            OnCutPerformed?.Invoke(this, EventArgs.Empty);
            CuttingRecipeSO recipe = GetRecipeForObjectOrNull(GetKitchenObject().GetKitchenObjectSO());

            if (cuttingProgress == recipe.cuttingProgressMax) {
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
