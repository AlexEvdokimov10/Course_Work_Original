using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Weapon Item", menuName = "Iventory/Items/New weapon Item")]
public class WeaponItem : Things
{
    public ThingsType itemType;
    
    
    private void Start()
    {
        itemType = ThingsType.Gun;
        
    }
}
