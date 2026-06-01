using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {

    [SerializeField] private Transform kitchenObjecHookPoint;

    private List<KitchenObjectSO> ingredients;
    private List<GameObject> ingredientPrefabs;

    private void Awake() {
        ingredients = new List<KitchenObjectSO>();
        ingredientPrefabs = new List<GameObject>();
    }

    public void AddIngredient(KitchenObjectSO ingredient) {
        ingredients.Add(ingredient);
        GameObject ingredientVisual = Instantiate(ingredient.prefab, kitchenObjecHookPoint).gameObject;
        ingredientPrefabs.Add(ingredientVisual);
    }

    public void ClearIngredients() {
        ingredients.Clear();
        foreach (GameObject ingredient in ingredientPrefabs) {
            Destroy(ingredient);
        }
        ingredientPrefabs.Clear();
    }
}
