using System;
using UnityEditor;
using UnityEngine;

public class StoveCounter : KitchenCounter, IProgressAction {
    public event EventHandler<ProgressEventArgs> OnProgressUpdated;
    public override event EventHandler OnKitchenObjectReceived;
    public event EventHandler OnFullyFried;

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOs;

    private float fryTime;
    private FryingRecipeSO currentRecipe;

    private void Start() {
        OnKitchenObjectRemoved += StoveCounter_OnKitchenObjectRemoved;
    }

    private void StoveCounter_OnKitchenObjectRemoved(object sender, EventArgs e) {
        fryTime = 0;
    }

    private void Update() {
        if (HasKitchenObject()) {
            if (currentRecipe == null) {
                return;
            }
            fryTime += Time.deltaTime;
            OnProgressUpdated?.Invoke(this, new ProgressEventArgs { progress = GetProgress() });
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

    private FryingRecipeSO GetRecipeForObjectOrNull(KitchenObjectSO kitchenObjectSO) {
        foreach (FryingRecipeSO recipe in fryingRecipeSOs) {
            if (recipe.ingredient == kitchenObjectSO) {
                return recipe;
            }
        }
        return null;
    }

    public float GetProgress() {
        return fryTime / currentRecipe.fryTime;
    }

    public bool IsBurningObject() {
        return GetRecipeForObjectOrNull(currentRecipe.result) == null;
    }
}
