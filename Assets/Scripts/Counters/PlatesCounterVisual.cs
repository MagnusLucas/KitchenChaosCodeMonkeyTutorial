using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour {

    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform kitchenObjectHookPoint;
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private float plateOffset;

    private List<Transform> spawnedPlates;

    private void Awake() {
        spawnedPlates = new List<Transform>();
    }

    private void Start() {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e) {
        Transform last = spawnedPlates.Last();
        spawnedPlates.Remove(last);
        Destroy(last.gameObject);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e) {
        Transform plateVisual = Instantiate(plateVisualPrefab, kitchenObjectHookPoint);

        plateVisual.transform.localPosition = spawnedPlates.Count * new Vector3(0, plateOffset, 0);

        spawnedPlates.Add(plateVisual);

    }


}
