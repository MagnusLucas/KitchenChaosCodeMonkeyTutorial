using System;
using UnityEngine;

public class StoveCounter : KitchenCounter, IProgressAction {
    public event EventHandler<ProgressEventArgs> OnProgressUpdated;
    public override event EventHandler OnKitchenObjectReceived;
    public event EventHandler OnFullyFried;

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOs;

    private float fryTime;
    private FryingRecipeSO currentRecipe;


    private void StoveCounter_OnKitchenObjectReceived(object sender, EventArgs e) {
        throw new NotImplementedException();
    }

    private void Update() {
        if (HasKitchenObject()) {
            if (currentRecipe == null) {
                return;
            }
            fryTime += Time.deltaTime;
            OnProgressUpdated?.Invoke(this, new ProgressEventArgs { progress = fryTime / currentRecipe.fryTime });
            if (fryTime >= currentRecipe.fryTime) {
                GetKitchenObject().DestroySelf();
                KitchenCounter.SpawnKitchenObject(currentRecipe.result, this);
                fryTime = 0;
                currentRecipe = GetRecipeForObjectOrNull(currentRecipe.result);
                if (currentRecipe == null) {
                    OnFullyFried?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }

    public override void GetKitchenObject(Player player) {
        currentRecipe = GetRecipeForObjectOrNull(player.GetKitchenObject().GetKitchenObjectSO());

        if (currentRecipe == null) {
            return;
        }

        player.GetKitchenObject().SetKitchenObjectParent(this);
        OnKitchenObjectReceived?.Invoke(this, EventArgs.Empty);
    }

    public override void GiveKitchenObject(Player player) {
        base.GiveKitchenObject(player);
        fryTime = 0;
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
