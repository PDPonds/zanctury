using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LootBoxData
{
    public InventoryObj lootBox;
    public List<ItemObj> items;
    public List<ItemObj> guns;
    public int allDropCout;
    public int gunDropCout;
    public int whatDrop;
    public int itemDropAmout;
    public int dropGunRate;
    public bool dropGun;
    public float gunDurability;
    public void LootBoxDatas()
    {
        dropGunRate = Random.Range(1, 11);
        if (dropGunRate <= 2)
        {
            dropGun = true;
        }
        else dropGun = false;
        if (dropGun)
        {
            gunDropCout = Random.Range(0, guns.Count);
            gunDurability = Random.Range(0, 100);
            lootBox.AddItem(guns[gunDropCout], 1, gunDurability);
        }
        allDropCout = Random.Range(1, 6);
        for (int i = 0; i < allDropCout; i++)
        {
            var whatDrop = Random.Range(0, items.Count);
            itemDropAmout = Random.Range(1, 6);
            lootBox.AddItem(items[whatDrop], itemDropAmout);
        }
    }
}
