using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//���ҧ ScriptableObject �ա�ѹ�֧�������������ҧ inventory �����¡ inventory ����͵�ͧ���
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObj : ScriptableObject
{
    //� inventory ���ҧ lite �ͧ slot �����
    public List<InventorySlot> Container = new List<InventorySlot>();

    public float currentWeight;
    public float maxWeight;

    // AddItem�� �Ѻ Item ��� �ӹǹ
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
        //����� bool ���������������
        bool hasItem = false;
        //loop �� item � inventory �ء���駷�������� add �����
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
                        //����÷ӧҹ
                        break;
                    }
                }
                else
                {
                    hasItem = false;
                }
            }
            //�������� ��������ҧ inventorySlot �������� Add ŧ � inventory
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
//���ҧ class inventorySlot ������� slot �Ѻ��Ңͧ�������� slot �� item ������Шӹǹ�������
[System.Serializable]
public class InventorySlot
{
    //item?
    public ItemObj item;
    //�ӹǹ�������
    public int amout;
    //slot �Ѻ���
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

    //�Ѻ�������ҡѺ value
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