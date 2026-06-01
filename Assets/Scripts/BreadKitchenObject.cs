using System;
using UnityEngine;

public class BreadKitchenObject : KitchenObject {

    public event EventHandler<PlateKitchenObject.IngredientAddedEventArgs> OnIngredientAdded;

    private PlateKitchenObject plate;

    public void SetPlate(PlateKitchenObject newPlate) {
        plate = newPlate;
        plate.OnIngredientAdded += Plate_OnIngredientAdded;
    }

    public void ClearPlate() {
        if (plate == null) {
            return;
        }
        plate.OnIngredientAdded -= Plate_OnIngredientAdded;
        plate = null;
    }

    private void Plate_OnIngredientAdded(object sender, PlateKitchenObject.IngredientAddedEventArgs e) {
        OnIngredientAdded?.Invoke(sender, e);
    }
}
