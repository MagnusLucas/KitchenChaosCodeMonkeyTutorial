using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashCounter : KitchenCounter {

    public static event EventHandler OnObjectTrashed;

    new public static void ResetStaticData() {
        OnObjectTrashed = null;
    }

    public override void GetKitchenObject(Player player) {
        OnObjectTrashed?.Invoke(this, EventArgs.Empty);
        if (player.GetKitchenObject() is PlateKitchenObject) {
            (player.GetKitchenObject() as PlateKitchenObject).ClearIngredients();
            return;
        }
        player.GetKitchenObject().DestroySelf();
    }

}
