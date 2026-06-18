using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CuttingCounter : KitchenCounter, IProgressAction {

    public static event EventHandler OnAnyCounterCutPerformed;
    public event EventHandler OnCutPerformed;
    public event EventHandler<ProgressEventArgs> OnProgressUpdated;
    override public event EventHandler OnKitchenObjectReceived;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOs;
    private int cuttingProgress;

    new public static void ResetStaticData() {
        OnAnyCounterCutPerformed = null;
    }


    public override void GetKitchenObject(Player player) {
        cuttingProgress = 0;
        player.GetKitchenObject().SetKitchenObjectParent(this);

        if (GetRecipeForObjectOrNull(GetKitchenObject().GetKitchenObjectSO()) != null) {
            OnKitchenObjectReceived?.Invoke(this, EventArgs.Empty);
        }
    }

    public void SecondaryInteract(Player player) {
        if (player.HasKitchenObject()) {
            return;
        }
        if (HasKitchenObject()) {
            CuttingRecipeSO recipe = GetRecipeForObjectOrNull(GetKitchenObject().GetKitchenObjectSO());
            if (recipe == null) {
                return;
            }
            cuttingProgress++;
            OnProgressUpdated?.Invoke(this, new ProgressEventArgs { progress = GetProgress() });
            OnCutPerformed?.Invoke(this, EventArgs.Empty);
            OnAnyCounterCutPerformed?.Invoke(this, EventArgs.Empty);

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

    public float GetProgress() {
        CuttingRecipeSO recipe = GetRecipeForObjectOrNull(GetKitchenObject().GetKitchenObjectSO());

        if (recipe != null) {
            return cuttingProgress.ConvertTo<float>() / recipe.cuttingProgressMax;
        }

        throw new NotImplementedException();
    }
}
