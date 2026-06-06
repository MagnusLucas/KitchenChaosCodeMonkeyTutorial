using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {

    public class IngredientAddedEventArgs {
        public KitchenObjectSO ingredient;
    }

    public event EventHandler<IngredientAddedEventArgs> OnIngredientAdded;
    public event EventHandler OnPlateCleared;

    [SerializeField] private Transform kitchenObjecHookPoint;

    private List<KitchenObjectSO> ingredients;
    private List<GameObject> ingredientPrefabs;


    public OrderRecipe ToRecipe() {
        return OrderRecipe.FromIngredients(ingredients);
    }

    private void Awake() {
        ingredients = new List<KitchenObjectSO>();
        ingredientPrefabs = new List<GameObject>();
    }

    public void AddIngredient(KitchenObjectSO ingredient) {
        GameObject ingredientVisual = Instantiate(ingredient.prefab, kitchenObjecHookPoint).gameObject;
        if (ingredients.Count == 0) {
            if (ingredientVisual.TryGetComponent<BreadKitchenObject>(out BreadKitchenObject breadKitchenObject)) {
                breadKitchenObject.SetPlate(this);
            }
        }
        ingredientVisual.transform.localPosition += Vector3.up * GetHeightForIngredientSpawn();
        ingredients.Add(ingredient);
        ingredientPrefabs.Add(ingredientVisual);
        OnIngredientAdded?.Invoke(this, new IngredientAddedEventArgs { ingredient = ingredient });
    }

    public void ClearIngredients() {
        ingredients.Clear();
        OnPlateCleared?.Invoke(this, EventArgs.Empty);
        foreach (GameObject ingredient in ingredientPrefabs) {
            Destroy(ingredient);
        }
        ingredientPrefabs.Clear();
    }

    private float GetHeightForIngredientSpawn() {
        const string BREAD = "bread";
        const float BREAD_TOP_HEIGHT = 0.3f;

        if (ingredients.Count < 1) {
            return 0;
        }

        float sum = 0;

        foreach (KitchenObjectSO ingredient in ingredients) {
            sum += ingredient.height;
        }

        bool startsWithBread = ingredients.First().objectName.Equals(BREAD);


        if (startsWithBread) {

            sum -= BREAD_TOP_HEIGHT;
        }

        return sum;
    }

}
