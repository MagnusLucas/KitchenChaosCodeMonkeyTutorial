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

    public bool TryCompleteOrder(OrderRecipe recipe) {
        foreach(OrderRecipe waitingOrder in waitingOrders) {
            if (OrderRecipe.AreEqual(waitingOrder, recipe)) {
                waitingOrders.Remove(waitingOrder);
                OnWaitingOrdersChanged?.Invoke(this, EventArgs.Empty);
                return true;
            }
        }
        return false;
    }

    private void GenerateOrder() {
        int orderSize = baseOrderSize + completedOrders / competionsToIncreaseOrderSize;
        OrderRecipe recipe = OrderRecipe.Generate(frequencySO, orderSize);
        Debug.Log(recipe);
        waitingOrders.Add(recipe);
        OnWaitingOrdersChanged?.Invoke(this, EventArgs.Empty);
    }


}
