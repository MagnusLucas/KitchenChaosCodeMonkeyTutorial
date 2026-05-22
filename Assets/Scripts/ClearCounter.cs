using UnityEngine;

public class ClearCounter : KitchenCounter {
    override public void Interact(Player player) {
        if (kitchenObject == null && player.HasKitchenObject()) {
            player.GetKitchenObject().SetKitchenObjectParent(this);
            return;
        }
        if (kitchenObject != null && !player.HasKitchenObject()) {
            kitchenObject.SetKitchenObjectParent(player);
        }
    }
}
