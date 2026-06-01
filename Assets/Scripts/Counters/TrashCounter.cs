using UnityEngine;

public class TrashCounter : KitchenCounter {

    public override void GetKitchenObject(Player player) {
        if (player.GetKitchenObject() is PlateKitchenObject) {
            (player.GetKitchenObject() as PlateKitchenObject).ClearIngredients();
            return;
        }
        player.GetKitchenObject().DestroySelf();
    }

}
