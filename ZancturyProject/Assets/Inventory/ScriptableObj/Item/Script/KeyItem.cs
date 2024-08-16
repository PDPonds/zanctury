using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Obj", menuName = "Inventory System/Items/KeyItem")]
public class KeyItem : ItemObj
{
    private void Awake()
    {
        type = ItemType.KeyItem;
    }
}
