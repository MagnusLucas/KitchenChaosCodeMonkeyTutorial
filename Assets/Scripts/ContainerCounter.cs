using System;
using UnityEngine;

public class ContainerCounter : KitchenCounter {

    public event EventHandler OnKitchenObjectSpawned;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    override public void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                KitchenCounter.SpawnKitchenObject(kitchenObjectSO).SetKitchenObjectParent(player);
                OnKitchenObjectSpawned?.Invoke(this, EventArgs.Empty);
            }

        } else {
            if (!player.HasKitchenObject()) {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}
