using UnityEngine;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour {
    [SerializeField] private Transform iconContainer;
    [SerializeField] private GameObject iconTemplate;

    private void Awake() {
        iconTemplate.SetActive(false);
    }

    public void SetRecipe(OrderRecipe recipe) {
        foreach (KitchenObjectSO kitchenObjectSO in recipe.ingredients) {
            GameObject icon = Instantiate(iconTemplate, iconContainer);
            icon.SetActive(true);
            if(icon.TryGetComponent<Image>(out Image image)) {
                image.sprite = kitchenObjectSO.sprite;
            }
        }
    }

}
