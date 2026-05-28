using System;
using UnityEngine;

public class StoveCounter : KitchenCounter, IProgressAction {
    public event EventHandler<ProgressEventArgs> OnProgressUpdated;
    public override event EventHandler OnKitchenObjectReceived;

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOs;

    private float fryTime;

    private void Update() {
        if (HasKitchenObject()) {
            FryingRecipeSO recipe = GetRecipeForObjectOrNull(GetKitchenObject().GetKitchenObjectSO());
            if (recipe == null) {
                return;
            }
            fryTime += Time.deltaTime;
            OnProgressUpdated?.Invoke(this, new ProgressEventArgs { progress = fryTime / recipe.fryTime });
            if (fryTime >= recipe.fryTime) {
                GetKitchenObject().DestroySelf();
                KitchenCounter.SpawnKitchenObject(recipe.result, this);
                fryTime = 0;
            }
        }
    }

    public override void GetKitchenObject(Player player) {
        FryingRecipeSO recipe = GetRecipeForObjectOrNull(player.GetKitchenObject().GetKitchenObjectSO());

        if (recipe == null) {
            return;
        }

        player.GetKitchenObject().SetKitchenObjectParent(this);
        OnKitchenObjectReceived?.Invoke(this, EventArgs.Empty);
    }


    private FryingRecipeSO GetRecipeForObjectOrNull(KitchenObjectSO kitchenObjectSO) {
        foreach (FryingRecipeSO recipe in fryingRecipeSOs) {
            if (recipe.ingredient == kitchenObjectSO) {
                return recipe;
            }
        }
        return null;
    }

    public float GetProgress() {
        throw new NotImplementedException();
    }
}
