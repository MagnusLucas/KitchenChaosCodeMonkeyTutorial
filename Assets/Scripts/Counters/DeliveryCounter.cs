using UnityEngine;

public class DeliveryCounter : KitchenCounter {
    public override void GetKitchenObject(Player player) {
        if (player.GetKitchenObject() is PlateKitchenObject) {
            player.GetKitchenObject().DestroySelf();
        }
    }
}
