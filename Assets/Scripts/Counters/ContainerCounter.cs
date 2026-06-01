using System;
using UnityEngine;

public class ContainerCounter : KitchenCounter {

    public event EventHandler OnKitchenObjectSpawned;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void InteractNoObjects(Player player) {
        KitchenCounter.SpawnKitchenObject(kitchenObjectSO, player);
        OnKitchenObjectSpawned?.Invoke(this, EventArgs.Empty);
    }

}
