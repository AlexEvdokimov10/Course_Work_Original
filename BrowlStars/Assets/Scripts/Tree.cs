using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TreeType
{
    Oak,
    AppleTree,
    Len,
}
[CreateAssetMenu (fileName ="Tree",menuName ="New Tree")]
public class Tree : ScriptableObject
{
    public string itemName;
    public GameObject itemPrefab;
    public TreeType itemType;
    public string decrption;
    
}
