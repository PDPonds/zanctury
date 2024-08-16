using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//สร้าง item ของแต่ละ type มาระบุค่าว่า type นี้ควรมี Variable อะไรบ้าง และเลือก type เป็นของตัวเอง
[CreateAssetMenu(fileName = "New Ammo Obj", menuName = "Inventory System/Items/Ammo")]
public class AmmoObj : ItemObj
{
    private void Awake()
    {
        type = ItemType.Ammo;
    }
}
