using UnityEngine;
using UnityEngine.UI;

public class IngredientImageUI : MonoBehaviour {
    
    public void SetImage(KitchenObjectSO kitchenObjectSO) {
        GetComponent<Image>().sprite = kitchenObjectSO.sprite;
    }

}
