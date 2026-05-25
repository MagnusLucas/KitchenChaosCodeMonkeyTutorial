using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject {
    public KitchenObjectSO ingredient;
    public KitchenObjectSO result;
    public int cuttingProgressMax;
}
