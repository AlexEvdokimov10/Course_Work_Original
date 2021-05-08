using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Food Item",menuName ="Iventory/Items/New food Item")]
public class FoodItem : Things
{
    
    public ThingsType itemType;
    public float foodHeal = 10;
    private void Start()
    {
        itemType = ThingsType.Food;
    }
}
