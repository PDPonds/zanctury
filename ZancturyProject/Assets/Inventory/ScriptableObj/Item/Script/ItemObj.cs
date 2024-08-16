using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// สร้าง Item function ใหญ่ไว้ว่าทุกๆ item ควรมีค่าอะไรบ้างเช่น type หรือ description อธิบาย item
public enum ItemType
{ 
    Food,
    Equipment,
    Material,
    Ammo,
    KeyItem
}

//ใช้ ScriptableObje เพื่อให้ item แต่ละอันเข้าถึงได้ง่าย
public abstract class ItemObj : ScriptableObject
{
    public int itemID;
    //public GameObject prefab;
    public string itemName;
    public Sprite icon;

    public ItemType type;

    public float weight;
    public bool stackAble;

    public bool isGun;
}
