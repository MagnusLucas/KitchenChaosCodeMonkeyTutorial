using UnityEngine;

public class BreadTopVisual : MonoBehaviour {

    [SerializeField] private BreadKitchenObject breadKitchenObject;

    private void Start() {
        breadKitchenObject.OnIngredientAdded += BreadKitchenObject_OnIngredientAdded;
    }

    private void BreadKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.IngredientAddedEventArgs e) {
        transform.localPosition += Vector3.up * e.ingredient.height;
    }
}
