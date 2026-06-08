using UnityEngine;

public class DeliveryCounter : KitchenCounter {

    public static DeliveryCounter Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public override void GetKitchenObject(Player player) {
        if (player.GetKitchenObject() is PlateKitchenObject) {
            PlateKitchenObject plate = player.GetKitchenObject() as PlateKitchenObject;
            DeliveryManager.Instance.TryCompleteOrder(plate.ToRecipe());
            plate.DestroySelf();
        }
    }
}
