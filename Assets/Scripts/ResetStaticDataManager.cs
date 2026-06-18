using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour {

    private void Awake() {
        CuttingCounter.ResetStaticData();
        KitchenCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
    }

}
