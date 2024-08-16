using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//สร้าง ScriptableObject อีกอันนึงเพื่อเอาไว้สร้าง inventory เพื่อแยก inventory เมื่อต้องการ
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObj : ScriptableObject
{
    //ใน inventory สร้าง lite ของ slot ขึ้นมา
    public List<InventorySlot> Container = new List<InventorySlot>();

    public float currentWeight;
    public float maxWeight;

    // AddItemโดย รับ Item และ จำนวน
    #region RemoveItem
    public void RemoveItem(ItemObj _item, int _amout)
    {
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item.name == _item.name)
            {
                Container[i].RemoveAmout(_amout);
                if (Container[i].amout <= 0)
                {
                    Container.Remove(Container[i]);
                    break;
                }
                break;
            }
        }
    }
    #endregion

    #region AddItem
    public void AddItem(ItemObj _item, int _amout)
    {
        //ตัวแปล bool เช็คว่ามีไอเท็มไหม
        bool hasItem = false;
        //loop เช็ค item ใน inventory ทุกครั้งที่มีไอเทม add เข้ามา
        if (currentWeight < maxWeight)
        {
            for (int i = 0; i < Container.Count; i++)
            {
                if (_item.stackAble)
                {
                    if ((Container[i].item == _item))
                    {
                        Container[i].AddAmount(_amout);
                        hasItem = true;
                        //จบการทำงาน
                        break;
                    }
                }
                else
                {
                    hasItem = false;
                }
            }
            //ถ้าไม่มี จะให้สร้าง inventorySlot ขึ้นมาและ Add ลง ใน inventory
            if (!hasItem)
            {
                //Debug.Log("New Item");
                Container.Add(new InventorySlot(_item, _amout));
            }
  
        }
    }

    public void AddItem(ItemObj _item,int _amout ,float _durability)
    {
        if (currentWeight < maxWeight)
        {
            Container.Add(new InventorySlot(_item, _amout,_durability));
        }
    }
    #endregion



}

#region InventorySlot
//สร้าง class inventorySlot เผื่อให้ slot รับค่าของไอเทมว่าใน slot มี item อะไรและจำนวนเท่าไหร่
[System.Serializable]
public class InventorySlot
{
    //item?
    public ItemObj item;
    //จำนวนเท่าไหร่
    public int amout;
    //slot รับค่า
    public float durability;
    public InventorySlot(ItemObj _item, int _amout)
    {
        item = _item;
        amout = _amout;
    }
    
    public InventorySlot(ItemObj _item, int _amout ,float _durability)
    {
        item = _item;
        amout = _amout;
        durability = _durability;
    }

    //รับเพิ่มเเท่ากับ value
    public void AddAmount(int _value)
    {
        amout += _value;
    }
    public void RemoveAmout(int _value)
    {
        amout -= _value;
    }
}
#endregion