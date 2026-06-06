using UnityEngine;

public class DeliveryCounter : KitchenCounter {
    public override void GetKitchenObject(Player player) {
        if (player.GetKitchenObject() is PlateKitchenObject) {
            PlateKitchenObject plate = player.GetKitchenObject() as PlateKitchenObject;
            Debug.Log(DeliveryManager.Instance.TryCompleteOrder(plate.ToRecipe()));
            plate.DestroySelf();
        }
    }
}
