using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour {

    [SerializeField] private GameObject recipeTemplate;
    [SerializeField] private Transform container;

    private DeliveryManager deliveryManager;

    private void Awake() {
        recipeTemplate.SetActive(false);
    }

    private void Start() {
        deliveryManager = DeliveryManager.Instance;
        deliveryManager.OnWaitingOrdersChanged += DeliveryManager_OnWaitingOrdersChanged;
        UpdateVisual();
    }

    private void DeliveryManager_OnWaitingOrdersChanged(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        foreach (Transform child in container) {
            if (child.gameObject != recipeTemplate) {
                Destroy(child.gameObject);
            }
        }
        List<OrderRecipe> waitingRecipes = deliveryManager.GetWaitingOrders();

        foreach (OrderRecipe recipe in waitingRecipes) {
            GameObject recipeGameObject = Instantiate(recipeTemplate, container);
            recipeGameObject.SetActive(true);

            if (recipeGameObject.TryGetComponent<RecipeUI>(out RecipeUI recipeUI)) {
                recipeUI.SetRecipe(recipe);
            }

        }

    }

}
