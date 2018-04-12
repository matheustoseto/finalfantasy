using System;
using UnityEngine;

[Serializable]
public class CharacterAttribute  {

    [Header("Status")]
    [SerializeField] private string name = "";
    [SerializeField] private float totalValue = 100;
    [SerializeField] private float valueAttribute = 100;

    [Header("Automatic Decrease")]
    [SerializeField] private bool  isDecreaseValue = false;
    [SerializeField] private float speedDecrease = 1;

    public float Value { get { return valueAttribute; } }
    public bool IsDecreaseValue { get { return isDecreaseValue; } set { isDecreaseValue = value; } }

    public void Start () {
        valueAttribute = totalValue;
	}

    public void Update()
    {
        AutomaticDecreaseValue();
    }


    private void AutomaticDecreaseValue()
    {
        if (isDecreaseValue)
        {
            valueAttribute -= speedDecrease * Time.deltaTime;
            if (valueAttribute <= 0) {
                /* Aciona o evento */
            }
        }
    }
	
    public void Increase(float value)
    {
        if (valueAttribute + value >= totalValue){
            valueAttribute = totalValue;
        }else{
            valueAttribute += value;
        }
    }

    public void Decrease(float value)
    {
        if (valueAttribute - value < 0){
            valueAttribute  = 0;
            /* Aciona o evento */
        }else {
            valueAttribute -= value;
        }
    }

    public float GetPercent()
    {
        return valueAttribute / totalValue;
    }
}
