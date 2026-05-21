using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ClearCounter clearCounter;

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    public void SetClearCounter(ClearCounter newClearCounter) {
        if (clearCounter != null) {
            clearCounter.ClearKitchenObject();
        }
        clearCounter = newClearCounter;

        if (clearCounter.HasKitchenObject()) {
            Debug.LogError("Counter already has a set kitchen object!");
        }

        clearCounter.SetKitchenObject(this);
        transform.parent = clearCounter.GetKitchenObjectSpawnPoint();
    }

    public ClearCounter GetClearCounter() {
        return clearCounter;
    }

}
