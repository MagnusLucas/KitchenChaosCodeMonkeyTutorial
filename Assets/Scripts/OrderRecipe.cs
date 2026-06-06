using System.Collections.Generic;
using UnityEngine;

public class OrderRecipe {

    public List<KitchenObjectSO> ingredients;

    public static OrderRecipe FromIngredients(List<KitchenObjectSO> ingredients) {
        OrderRecipe recipe = new OrderRecipe();
        recipe.ingredients = ingredients;
        return recipe;
    }

    public static bool AreEqual(OrderRecipe first, OrderRecipe second) {
        if (first.ingredients.Count != second.ingredients.Count) {
            return false;
        }

        for (int i = 0; i < first.ingredients.Count; i++) {
            if (first.ingredients[i] != second.ingredients[i]) {
                return false;
            }
        }

        return true;
    }

    public static OrderRecipe Generate(IngredientFrequencySO frequency, int recipeLength) {
        OrderRecipe recipe = new OrderRecipe();

        float totalFrequencyAsFirst = 0;
        float totalFrequencyNotAsFirst = 0;

        foreach (IngredientFrequencySO.IngredientFrequency ingredient in frequency.ingredientFrequencies) {
            totalFrequencyAsFirst += ingredient.asFirst;
            totalFrequencyNotAsFirst += ingredient.later;
        }

        float randomNumber = Random.Range(0, totalFrequencyAsFirst);

        foreach (IngredientFrequencySO.IngredientFrequency ingredient in frequency.ingredientFrequencies) {
            randomNumber -= ingredient.asFirst;
            if (randomNumber < 0) {
                recipe.ingredients.Add(ingredient.ingredient);
                break;
            }
        }
        recipeLength--;

        while (recipeLength > 0) {
            randomNumber = Random.Range(0, totalFrequencyNotAsFirst);

            foreach (IngredientFrequencySO.IngredientFrequency ingredient in frequency.ingredientFrequencies) {
                randomNumber -= ingredient.later;
                if (randomNumber < 0) {
                    recipe.ingredients.Add(ingredient.ingredient);
                    break;
                }
            }

            recipeLength--;
        }
        return recipe;
    }

    private OrderRecipe() {
        ingredients = new List<KitchenObjectSO>();
    }

    public override string ToString() {
        string result = "OrderRecipe: ";
        foreach (KitchenObjectSO ingredient in ingredients) {
            result += ingredient.objectName + ", ";
        }
        result = result.Remove(result.Length - 2);
        return result;
    }

}
