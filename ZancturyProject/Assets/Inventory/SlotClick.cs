using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SlotClick : MonoBehaviour
{
    #region Variable
    InventoryObj lootBox;
    public InventoryObj inventory;
    InventoryObj equipment;
    public InventoryObj ZInventory;
    InventoryObj melee;
    InventoryObj RemoveZanctuary;

    [SerializeField] private SliderBar durabilityBar;
    [SerializeField] private GameObject durabilityBarObj;

    public Transform equitmentSlot;

    ZanctuaryManager _zanctuaryManager;
    playerInventory _playerInventory;

    public List<ItemObj> items = new List<ItemObj>();

    public int currentItemID;
    public int currentAmout;
    public float currentDurability;

    public GameObject useMenu;

    public TextMeshProUGUI amountText;

    public TextMeshProUGUI weight;

    #endregion

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "LootMap")
        {
            _playerInventory = GameObject.Find("Player").GetComponent<playerInventory>();
        }
        if (SceneManager.GetActiveScene().name == "Zanctuary")
        {
            _zanctuaryManager = GameObject.Find("ZanctuaryManager").GetComponent<ZanctuaryManager>();
        }
    }

    private void Update()
    {

        #region Setup InventoryOBJ
        if (SceneManager.GetActiveScene().name == "LootMap")
        {
            lootBox = _playerInventory.lootBox;
            inventory = _playerInventory.inventory;
            equipment = _playerInventory.equipment;
            melee = _playerInventory.melee;
        }
        #endregion

        if (currentItemID == 0)
        {
            var thisImage = this.GetComponent<Image>();
            thisImage.sprite = null;
            var thisButton = this.GetComponent<Button>();
            thisButton.interactable = false;
            amountText.text = "";
            durabilityBarObj.SetActive(false);
            weight.text = "";
        }
        else if (currentItemID > 0)
        {
            var thisImage = this.GetComponent<Image>();
            foreach (var ID in items)
            {
                if (currentItemID == ID.itemID)
                {
                    if (ID.type == ItemType.Equipment)
                    {
                        durabilityBarObj.SetActive(true);
                    }
                    else if (ID.type != ItemType.Equipment)
                    {
                        durabilityBarObj.SetActive(false);
                    }

                    if (currentAmout == 1)
                    {
                        amountText.text = "";
                    }
                    else
                    {
                        amountText.text = currentAmout.ToString("0");
                    }
                    weight.text = ID.weight.ToString("0.0");
                    thisImage.sprite = ID.icon;
                }
            }
            var thisButton = GetComponent<Button>();
            thisButton.interactable = true;
            durabilityBar.SetMaxValue(100f, currentDurability);
        }

    }


    #region TakeItemFormLootBox
    public void TakeItemFormRootBox()
    {
        if (lootBox != null && inventory.currentWeight < inventory.maxWeight)
        {
            var thisSlot = this.GetComponent<WhatSlot>();
            foreach (var ID in items)
            {
                if (currentItemID == ID.itemID)
                {
                    if (ID.type == ItemType.Equipment)
                    {
                        inventory.Container.Add(lootBox.Container[thisSlot.whatSlot]);
                        lootBox.Container.Remove(lootBox.Container[thisSlot.whatSlot]);
                    }
                    else
                    {
                        inventory.AddItem(ID, 1);
                        lootBox.RemoveItem(ID, 1);
                    }
                }
            }
        }
    }
    #endregion

    #region TakeItemFormInventory
    public void TakeItemFormInventory()
    {
        if (SceneManager.GetActiveScene().name == "LootMap")
        {
            if (lootBox != null && lootBox.currentWeight < lootBox.maxWeight && _playerInventory.lootUI.activeSelf)
            {
                var thisSlot = this.GetComponent<WhatSlot>();
                foreach (var ID in items)
                {
                    if (currentItemID == ID.itemID)
                    {
                        if (ID.type == ItemType.Equipment)
                        {
                            lootBox.Container.Add(inventory.Container[thisSlot.whatSlot]);
                            inventory.Container.Remove(inventory.Container[thisSlot.whatSlot]);
                        }
                        else
                        {
                            lootBox.AddItem(ID, 1);
                            inventory.RemoveItem(ID, 1);

                        }
                    }
                }
            }
            else if (!_playerInventory.lootUI.activeSelf && _playerInventory.inventoryUI.activeSelf)
            {
                var thisSlot = this.GetComponent<WhatSlot>();
                foreach (var ID in items)
                {
                    if (currentItemID == ID.itemID)
                    {
                        if (ID.type == ItemType.Equipment)
                        {
                            useMenu.SetActive(true);
                        }
                    }
                }
            }
        }
        if (SceneManager.GetActiveScene().name == "Zanctuary")
        {
            if (gameObject.transform.parent.name == "InventoryItem")
            {
                var thisSlot = this.GetComponent<WhatSlot>();
                foreach (var ID in items)
                {
                    if (currentItemID == ID.itemID)
                    {
                        if (ZInventory.currentWeight < ZInventory.maxWeight)
                        {
                            if (ID.type == ItemType.Equipment)
                            {
                                ZInventory.Container.Add(inventory.Container[thisSlot.whatSlot]);
                                inventory.Container.Remove(inventory.Container[thisSlot.whatSlot]);
                            }
                            else
                            {
                                ZInventory.AddItem(ID, 1);
                                inventory.RemoveItem(ID, 1);
                            }
                        }
                    }
                }
            }
        }
    }
    #endregion

    public void UseMenu()
    {
        var thisSlot = gameObject.GetComponentInParent<WhatSlot>();
        useMenu.SetActive(false);
        foreach (var ID in items)
        {
            if (currentItemID == ID.itemID)
            {
                if (ID.type == ItemType.Equipment && SceneManager.GetActiveScene().name == "LootMap")
                {
                    if (ID.isGun)
                    {
                        if (equipment.Container.Count > 0)
                        {
                            inventory.Container.Add(equipment.Container[0]);
                            equipment.Container.Remove(equipment.Container[0]);
                            equipment.Container.Add(inventory.Container[thisSlot.whatSlot]);
                            inventory.Container.Remove(inventory.Container[thisSlot.whatSlot]);
                            useMenu.SetActive(false);
                        }
                        else if (equipment.Container.Count == 0)
                        {
                            equipment.Container.Add(inventory.Container[thisSlot.whatSlot]);
                            inventory.Container.Remove(inventory.Container[thisSlot.whatSlot]);
                            useMenu.SetActive(false);
                        }
                    }
                    else
                    {
                        if (melee.Container.Count > 0)
                        {
                            inventory.Container.Add(melee.Container[0]);
                            melee.Container.Remove(melee.Container[0]);
                            melee.Container.Add(inventory.Container[thisSlot.whatSlot]);
                            inventory.Container.Remove(inventory.Container[thisSlot.whatSlot]);
                            useMenu.SetActive(false);
                        }
                        else if (melee.Container.Count == 0)
                        {
                            melee.Container.Add(inventory.Container[thisSlot.whatSlot]);
                            inventory.Container.Remove(inventory.Container[thisSlot.whatSlot]);
                            useMenu.SetActive(false);
                        }
                    }
                }
            }
        }

    }

    public void SelectToInventory()
    {
        if (SceneManager.GetActiveScene().name == "Zanctuary")
        {
            if (gameObject.transform.parent.name == "SelectItem")
            {
                var thisSlot = this.GetComponent<WhatSlot>();
                foreach (var ID in items)
                {
                    if (currentItemID == ID.itemID)
                    {
                        if (inventory.currentWeight < inventory.maxWeight)
                        {
                            if (ID.type == ItemType.Equipment)
                            {
                                inventory.Container.Add(ZInventory.Container[thisSlot.whatSlot]);
                                ZInventory.Container.Remove(ZInventory.Container[thisSlot.whatSlot]);
                            }
                            else
                            {
                                inventory.AddItem(ID, 1);
                                ZInventory.RemoveItem(ID, 1);
                            }
                        }

                    }
                }
            }
        }
    }

    public void UnUse()
    {
        if (SceneManager.GetActiveScene().name == "LootMap")
        {
            if (gameObject.transform.parent.name == "EquipmentInInventory")
            {
                foreach (var ID in items)
                {
                    if (currentItemID == ID.itemID)
                    {
                        if (inventory.currentWeight < inventory.maxWeight)
                        {
                            if (ID.type == ItemType.Equipment)
                            {
                                if (!ID.isGun)
                                {
                                    inventory.Container.Add(melee.Container[0]);
                                    melee.Container.Remove(melee.Container[0]);
                                }
                                else if(ID.isGun)
                                {
                                    inventory.Container.Add(equipment.Container[0]);
                                    equipment.Container.Remove(equipment.Container[0]);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
