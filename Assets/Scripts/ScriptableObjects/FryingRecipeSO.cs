using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject {
    public KitchenObjectSO ingredient;
    public KitchenObjectSO result;
    public float fryTime;
}
