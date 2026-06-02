using System;
using UnityEngine;

public class BreadKitchenObject : KitchenObject {

    public event EventHandler<PlateKitchenObject.IngredientAddedEventArgs> OnIngredientAdded;

    private PlateKitchenObject plate;

    public void SetPlate(PlateKitchenObject newPlate) {
        plate = newPlate;
        plate.OnIngredientAdded += Plate_OnIngredientAdded;
        plate.OnPlateCleared += Plate_OnPlateCleared;
    }

    private void Plate_OnPlateCleared(object sender, EventArgs e) {
        ClearPlate();
    }

    private void ClearPlate() {
        plate.OnIngredientAdded -= Plate_OnIngredientAdded;
        plate.OnPlateCleared -= Plate_OnPlateCleared;
        plate = null;
    }

    private void Plate_OnIngredientAdded(object sender, PlateKitchenObject.IngredientAddedEventArgs e) {
        OnIngredientAdded?.Invoke(sender, e);
    }
}
