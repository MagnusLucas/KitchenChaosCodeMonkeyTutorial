using UnityEngine;

public class ContainerCounter : KitchenCounter {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    override public void Interact(Player player) {
        if (kitchenObject == null) {
            if (player.HasKitchenObject()) {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, kitchenObjectHookPoint);
                kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
            }

        } else {
            if (!player.HasKitchenObject()) {
                kitchenObject.SetKitchenObjectParent(player);
            }
        }
    }

}
