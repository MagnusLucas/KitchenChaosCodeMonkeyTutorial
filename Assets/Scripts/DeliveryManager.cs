using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour {

    public event EventHandler OnWaitingOrdersChanged;
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private IngredientFrequencySO frequencySO;
    [SerializeField] private int baseOrderSize;
    [SerializeField] private int competionsToIncreaseOrderSize;

    private List<OrderRecipe> waitingOrders;
    private int completedOrders = 0;


    private void Awake() {
        waitingOrders = new List<OrderRecipe>();
        Instance = this;
        for (int i = 0; i < 3; i++) GenerateOrder();
    }

    public List<OrderRecipe> GetWaitingOrders() { return waitingOrders; }

    private void GenerateOrder() {
        int orderSize = baseOrderSize + completedOrders / competionsToIncreaseOrderSize;
        waitingOrders.Add(OrderRecipe.Generate(frequencySO, orderSize));
        OnWaitingOrdersChanged?.Invoke(this, EventArgs.Empty);
    }

}
