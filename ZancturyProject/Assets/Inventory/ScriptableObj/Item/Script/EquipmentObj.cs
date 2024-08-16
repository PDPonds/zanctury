using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//สร้าง item ของแต่ละ type มาระบุค่าว่า type นี้ควรมี Variable อะไรบ้าง และเลือก type เป็นของตัวเอง
[CreateAssetMenu(fileName = "New Equipment Obj", menuName = "Inventory System/Items/Equipment")]
public class EquipmentObj : ItemObj
{
    private void Awake()
    {
        type = ItemType.Equipment;
    }
}
