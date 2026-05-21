using UnityEngine;

public class ClearCounter : MonoBehaviour {

    [SerializeField] private Transform tomatoPrefabTransform;
    [SerializeField] private Transform spawnPoint;

    public void Interact() {
        Debug.Log("Interacted with clear counter");
        Instantiate(tomatoPrefabTransform, spawnPoint);
    }
}
