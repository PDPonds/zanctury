using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//สร้าง item ของแต่ละ type มาระบุค่าว่า type นี้ควรมี Variable อะไรบ้าง และเลือก type เป็นของตัวเอง
[CreateAssetMenu(fileName = "New Food Obj", menuName ="Inventory System/Items/Food")]
public class FoodObj : ItemObj
{

    private void Awake()
    {
        type = ItemType.Food;
    }
}
