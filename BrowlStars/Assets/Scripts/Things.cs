using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ThingsType
{
    Default,
    Food,
    Gun,
    Tool
}
public class Things : ScriptableObject
{
   
    public string itemName;
    public int maxAmount;
    public GameObject itemPrefab;
    public ThingsType itemType;
    public string itemDecription;
    public string inHandName;
    public Sprite icon;
    public bool isConsumeable;
    public float changeHealth;
    public float attackRange;
    public float damage;
}

