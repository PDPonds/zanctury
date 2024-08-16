using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZancturyInventory : MonoBehaviour
{
    public InventoryObj ZInventory;
    public InventoryObj RemoveItemInZanctuary;
    public GameObject SlotObj;
    [SerializeField] private List<Transform> slotPos = new List<Transform>();

    [SerializeField] private InventoryObj inventory;
    [SerializeField] private List<Transform> slotPosInventory = new List<Transform>();
    [SerializeField] private TextMeshProUGUI textCurrentWeight;
    [SerializeField] private TextMeshProUGUI textMaxWeight;
    private void Start()
    {
        if (this.gameObject.name == "Content" || this.gameObject.name == "SelectItem")
        {
            if (ZInventory.Container.Count > 0)
            {
                for (int i = 0; i < ZInventory.Container.Count; i++)
                {
                    AddSlot();
                }
            }
        }
        if (this.gameObject.name == "RemovePanal")
        {

            for (int i = 0; i < RemoveItemInZanctuary.Container.Count; i++)
            {
                AddSlot();
            }

        }
        if (ZInventory != null && this.gameObject.name == "EquipmentInventory")
        {
            if (ZInventory.Container.Count > 0)
            {
                for (int i = 0; i < ZInventory.Container.Count; i++)
                {
                    if (ZInventory.Container[i].item.type == ItemType.Equipment)
                    {
                        AddSlot();
                    }
                }
            }
        }

        

    }

    private void Update()
    {
        if (inventory != null && this.gameObject.name == "InventoryItem")
        {
            textCurrentWeight.text = inventory.currentWeight.ToString("0.00");
            textMaxWeight.text = inventory.maxWeight.ToString("0.00");
            if (inventory.Container.Count > 0)
            {
                SlotCheckID(inventory, slotPosInventory);
                UpdateWeight(inventory);
            }
            else
            {
                inventory.currentWeight = 0;
                slotPosInventory[0].GetComponent<SlotClick>().currentItemID = 0;
            }
        }
        if (ZInventory != null && this.gameObject.name == "Content" && ZInventory.Container.Count != 0)
        {
            if (ZInventory.Container.Count > 0)
            {
                SlotCheckID(ZInventory, slotPos);
                UpdateWeight(ZInventory);
            }
            else
            {
                ZInventory.currentWeight = 0;
                slotPos[0].GetComponent<SlotClick>().currentItemID = 0;
            }
            if (slotPos.Count < ZInventory.Container.Count)
            {
                var DifferenceValue = ZInventory.Container.Count - slotPosInventory.Count;
                for (int i = 0; i < DifferenceValue; i++)
                {
                    AddSlot();
                    break;
                }

            }
        }
        if (ZInventory != null && this.gameObject.name == "SelectItem" && ZInventory.Container.Count != 0)
        {
            if (ZInventory.Container.Count > 0)
            {
                SlotCheckID(ZInventory, slotPos);
                UpdateWeight(ZInventory);
            }
            else
            {
                ZInventory.currentWeight = 0;
                slotPos[0].GetComponent<SlotClick>().currentItemID = 0;
            }
            if (slotPos.Count < ZInventory.Container.Count)
            {
                var DifferenceValue = ZInventory.Container.Count - slotPosInventory.Count;
                for (int i = 0; i < DifferenceValue; i++)
                {
                    AddSlot();
                    break;
                }

            }

        }
        else if (ZInventory.Container.Count <= 0 && slotPos.Count > 0)
        {
            for (int i = 0; i < slotPos.Count; i++)
            {
                Destroy(slotPos[i].gameObject);
            }
            slotPos.Clear();
        }

        if (RemoveItemInZanctuary != null && this.gameObject.name == "RemovePanal" && RemoveItemInZanctuary.Container.Count != 0)
        {
            if (RemoveItemInZanctuary.Container.Count > 0)
            {
                SlotCheckID(RemoveItemInZanctuary, slotPos);
                UpdateWeight(RemoveItemInZanctuary);
            }
            else
            {
                RemoveItemInZanctuary.currentWeight = 0;
                slotPos[0].GetComponent<SlotClick>().currentItemID = 0;
            }
        }
        if (ZInventory != null && this.gameObject.name == "EquipmentInventory" && ZInventory.Container.Count != 0)
        {
            if (ZInventory.Container.Count > 0)
            {
                for (int i = 0; i < ZInventory.Container.Count; i++)
                {
                    if (ZInventory.Container[i].item.type == ItemType.Equipment)
                    {
                        SlotCheckEquipmentID(ZInventory, slotPos);
                        UpdateWeight(ZInventory);
                    }
                }
            }
            else
            {
                ZInventory.currentWeight = 0;
                slotPos[0].GetComponent<SlotClick>().currentItemID = 0;
            }
        }
    }

    public void AddSlot()
    {
        GameObject obj = Instantiate(SlotObj, this.transform);
        var objWhatSlot = obj.gameObject.GetComponent<WhatSlot>();
        slotPos.Add(obj.transform);
        objWhatSlot.whatSlot = slotPos.Count - 1;
    }

    public void UpdateWeight(InventoryObj _inventoryOBJ)
    {
        var sumAmout = 0f;
        var itemWeight = 0f;
        var sumItemWeight = 0f;
        var sumWeight = 0f;

        for (int i = 0; i < _inventoryOBJ.Container.Count; i++)
        {
            sumAmout = _inventoryOBJ.Container[i].amout;
            itemWeight = _inventoryOBJ.Container[i].item.weight;
            sumItemWeight = sumAmout * itemWeight;
            sumWeight = sumWeight + sumItemWeight;

        }
        _inventoryOBJ.currentWeight = sumWeight;
    }

    public void SlotCheckID(InventoryObj _invetoryOBJ, List<Transform> slotPos)
    {
        for (int i = 0; i < _invetoryOBJ.Container.Count; i++)
        {
            for (int j = 0; j < slotPos.Count; j++)
            {
                var slotCurrentID = slotPos[j].GetComponent<SlotClick>();
                try
                {
                    slotCurrentID.currentItemID = _invetoryOBJ.Container[j].item.itemID;
                    slotCurrentID.currentAmout = _invetoryOBJ.Container[j].amout;
                    if (_invetoryOBJ.Container[j].item.type == ItemType.Equipment)
                    {
                        slotCurrentID.currentDurability = _invetoryOBJ.Container[j].durability;
                    }
                }
                catch
                {
                    slotCurrentID.currentItemID = 0;
                    break;
                }

            }
        }

    }

    public void SlotCheckEquipmentID(InventoryObj _invetoryOBJ, List<Transform> slotPos)
    {
        for (int i = 0; i < slotPos.Count; i++)
        {
            for (int j = 0; j < _invetoryOBJ.Container.Count; j++)
            {
                if (_invetoryOBJ.Container[j].item.type == ItemType.Equipment)
                {
                    var slotCurrentID = slotPos[i].GetComponent<SlotClick>();
                    try
                    {
                        slotCurrentID.currentItemID = _invetoryOBJ.Container[j].item.itemID;
                        slotCurrentID.currentAmout = _invetoryOBJ.Container[j].amout;
                        slotCurrentID.currentDurability = _invetoryOBJ.Container[j].durability;
                        i++;
                    }
                    catch
                    {
                        slotCurrentID.currentItemID = 0;
                        break;
                    }

                }

            }
        }

    }
}
