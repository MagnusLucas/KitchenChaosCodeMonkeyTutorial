using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour {

    public event EventHandler OnWaitingOrdersChanged;
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private IngredientFrequencySO frequencySO;
    [SerializeField] private int baseOrderSize = 3;
    [SerializeField] private int competionsToIncreaseOrderSize = 3;
    [SerializeField] private float timeToSpawn = 5;
    [SerializeField] private int maxActiveOrders = 7;

    private List<OrderRecipe> waitingOrders;
    private int completedOrders = 0;
    private float time = 0;

    private void Awake() {
        waitingOrders = new List<OrderRecipe>();
        Instance = this;
        for (int i = 0; i < 3; i++) GenerateOrder();
    }

    private void Update() {
        time += Time.deltaTime;
        if (time > timeToSpawn) {
            time = 0;
            if (waitingOrders.Count < maxActiveOrders) {
                GenerateOrder();
            }
        }
    }

    public List<OrderRecipe> GetWaitingOrders() { return waitingOrders; }

    public bool TryCompleteOrder(OrderRecipe recipe) {
        foreach(OrderRecipe waitingOrder in waitingOrders) {
            if (OrderRecipe.AreEqual(waitingOrder, recipe)) {
                Debug.Log(waitingOrders.Remove(waitingOrder));
                OnWaitingOrdersChanged?.Invoke(this, EventArgs.Empty);
                completedOrders++;
                return true;
            }
        }
        return false;
    }

    private void GenerateOrder() {
        int orderSize = baseOrderSize + completedOrders / competionsToIncreaseOrderSize;
        OrderRecipe recipe = OrderRecipe.Generate(frequencySO, orderSize);
        waitingOrders.Add(recipe);
        OnWaitingOrdersChanged?.Invoke(this, EventArgs.Empty);
    }


}
