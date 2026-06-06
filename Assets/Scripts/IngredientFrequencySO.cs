using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientFrequencySO", menuName = "Scriptable Objects/IngredientFrequencySO")]
public class IngredientFrequencySO : ScriptableObject
{
    [Serializable]
    public struct IngredientFrequency {
        public KitchenObjectSO ingredient;
        public float asFirst;
        public float later;
    }

    [SerializeField]
    public List<IngredientFrequency> ingredientFrequencies;

}
