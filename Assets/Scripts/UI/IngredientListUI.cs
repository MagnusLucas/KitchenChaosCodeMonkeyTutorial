using System.Collections.Generic;
using UnityEngine;

public class IngredientListUI : MonoBehaviour {

    [SerializeField] private int columns = 3;
    [SerializeField] private IngredientImageUI ingredientImageUIPrefab;
    [SerializeField] private PlateKitchenObject plateKitchenObject;

    private float imageSize;
    private Vector3 imageOffset;
    private List<IngredientImageUI> ingredientImages;
    private Vector3 resetPosition;

    private void Awake() {
        ingredientImages = new List<IngredientImageUI>();
        RectTransform rectTransform = GetComponent<RectTransform>();
        imageSize = rectTransform.sizeDelta.x / columns;
        imageOffset = new Vector3(0, imageSize, 0);
        resetPosition = rectTransform.localPosition;
    }

    private void Start() {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        plateKitchenObject.OnPlateCleared += PlateKitchenObject_OnPlateCleared;
    }

    private void PlateKitchenObject_OnPlateCleared(object sender, System.EventArgs e) {
        ResetList();
    }

    public void ResetList() {
        foreach (var item in ingredientImages) {
            Destroy(item.gameObject);
        }
        ingredientImages.Clear();
        ResetPosition();
    }

    private void ResetPosition() {
        transform.localPosition = resetPosition;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.IngredientAddedEventArgs e) {
        AddIngredient(e.ingredient);
    }

    private void MoveRowUp() {
        transform.localPosition += Vector3.up * imageSize;
    }

    private void KeepOverBurger(float height) {
        transform.localPosition += Vector3.up * height;
    }

    private void AddIngredient(KitchenObjectSO ingredient) {
        IngredientImageUI newImage = Instantiate(ingredientImageUIPrefab, transform);
        ingredientImages.Add(newImage);

        int numberInList = ingredientImages.Count - 1;

        if (numberInList > 0 && numberInList % columns == 0) { MoveRowUp(); }
        KeepOverBurger(ingredient.height);

        newImage.transform.localPosition += new Vector3(numberInList % columns, -numberInList / columns, 0) * imageSize + imageOffset;
        newImage.GetComponent<RectTransform>().sizeDelta = new Vector3(1, 1, 0) * imageSize;
        newImage.SetImage(ingredient);
    }

}
