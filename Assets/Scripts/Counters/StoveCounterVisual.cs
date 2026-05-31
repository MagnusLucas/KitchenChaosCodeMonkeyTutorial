using System;
using System.Runtime.CompilerServices;
using UnityEngine;



public class StoveCounterVisual : MonoBehaviour {

    [SerializeField] private GameObject sizzleParticles;
    [SerializeField] private GameObject stoveHeatVisual;
    [SerializeField] private StoveCounter stoveCounter;

    private void Start() {
        stoveCounter.OnKitchenObjectReceived += StoveCounter_OnKitchenObjectReceived;
        stoveCounter.OnKitchenObjectRemoved += StoveCounter_OnKitchenObjectRemoved;
        stoveCounter.OnFullyFried += StoveCounter_OnKitchenObjectRemoved;
    }

    private void StoveCounter_OnKitchenObjectRemoved(object sender, EventArgs e) {
        TurnOff();
    }

    private void StoveCounter_OnKitchenObjectReceived(object sender, EventArgs e) {
        TurnOn();
    }

    public void TurnOn() {
        sizzleParticles.SetActive(true);
        stoveHeatVisual.SetActive(true);
    }

    public void TurnOff() {
        sizzleParticles.SetActive(false);
        stoveHeatVisual.SetActive(false);
    }

}
