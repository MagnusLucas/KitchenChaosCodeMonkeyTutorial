using UnityEngine;

public class ClearCounter : MonoBehaviour {

    [SerializeField] private KitchenObjectSO kitchenObject;
    [SerializeField] private Transform spawnPoint;

    public void Interact() {
        Instantiate(kitchenObject.prefab, spawnPoint);
    }
}
