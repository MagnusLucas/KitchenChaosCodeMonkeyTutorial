using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {

    public class IngredientAddedEventArgs {
        public KitchenObjectSO ingredient;
    }

    public event EventHandler<IngredientAddedEventArgs> OnIngredientAdded;

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
        ingredientVisual.transform.localPosition += Vector3.up * GetHeight();
        if (ingredientVisual.TryGetComponent<BreadKitchenObject>(out BreadKitchenObject breadKitchenObject)) {
            breadKitchenObject.SetPlate(this);
        }
        OnIngredientAdded?.Invoke(this, new IngredientAddedEventArgs { ingredient = ingredient });
    }

    public void ClearIngredients() {
        ingredients.Clear();
        foreach (GameObject ingredient in ingredientPrefabs) {
            Destroy(ingredient);
        }
        ingredientPrefabs.Clear();
    }

    private float GetHeight() {
        float sum = 0;
        foreach (KitchenObjectSO ingredient in ingredients) {
            sum += ingredient.height;
        }
        return sum;
    }

}
