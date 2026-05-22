using UnityEngine;

public class ClearCounter : KitchenCounter {
    override public void Interact(Player player) {
        if (!HasKitchenObject() && player.HasKitchenObject()) {
            player.GetKitchenObject().SetKitchenObjectParent(this);
            return;
        }
        if (HasKitchenObject() && !player.HasKitchenObject()) {
            GetKitchenObject().SetKitchenObjectParent(player);
        }
    }
}
