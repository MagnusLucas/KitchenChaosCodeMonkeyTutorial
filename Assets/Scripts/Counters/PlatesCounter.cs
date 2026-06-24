using System;
using UnityEngine;

public class PlatesCounter : KitchenCounter {

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private int maxPlates;
    [SerializeField] private float timeToSpawn;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;


    private int numberOfPlates = 0;
    private float timer = 0;
    private bool gameStarted = false;

    private void Start() {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
    }

    private void KitchenGameManager_OnStateChanged(object sender, EventArgs e) {
        if (KitchenGameManager.Instance.IsGamePlaying()) {
            gameStarted = true;
        }
    }

    private void Update() {
        if (gameStarted && numberOfPlates < maxPlates) {
            timer += Time.deltaTime;
            if (timer > timeToSpawn) {
                timer = 0;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
                numberOfPlates++;
            }
        }
    }

    public override void Interact(Player player) {
        if (!player.HasKitchenObject() && numberOfPlates > 0) {
            KitchenCounter.SpawnKitchenObject(plateKitchenObjectSO, player);
            OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            numberOfPlates--;
        }
    }

}
