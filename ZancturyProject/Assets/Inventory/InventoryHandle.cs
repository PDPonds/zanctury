using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryHandle : MonoBehaviour
{
    #region Variable
    public InventoryObj inventory;
    public InventoryObj lootBox;
    public InventoryObj equipment;
    public InventoryObj melee;

    public List<Transform> slotPosLootBox = new List<Transform>();
    public List<Transform> slotPosInven = new List<Transform>();
    public List<Transform> slotPosEquipment = new List<Transform>();
    public List<Transform> slotPosMelee = new List<Transform>();

    public TextMeshProUGUI textCurrentWeight;
    public TextMeshProUGUI textMaxWeight;

    [SerializeField] private TextMeshProUGUI currentAmmoInStack;
    [SerializeField] private TextMeshProUGUI ammoCount;

    playerInventory _playerInventory;
    CharacterMovement _characterMovement;
    #endregion
    void Start()
    {
        _playerInventory = GameObject.Find("Player").GetComponent<playerInventory>();
        _characterMovement = GameObject.Find("Player").GetComponent<CharacterMovement>();
    }

    void Update()
    {
        lootBox = _playerInventory.lootBox;
        inventory = _playerInventory.inventory;
        equipment = _playerInventory.equipment;

        if (lootBox != null && this.gameObject.name == "LootSystem")
        {
            textCurrentWeight.text = lootBox.currentWeight.ToString("0.00");
            textMaxWeight.text = lootBox.maxWeight.ToString("0.00");
            if (lootBox.Container.Count > 0)
            {
                SlotCheckID(lootBox, slotPosLootBox);
                //UpdateWeight(lootBox);
            }
            else
            {
                lootBox.currentWeight = 0;
                if (slotPosLootBox.Count > 0)
                {
                    for (int i = 0; i < slotPosLootBox.Count; i++)
                    {
                        slotPosLootBox[i].GetComponent<SlotClick>().currentItemID = 0;
                    }
                }
                //slotPosLootBox[0].GetComponent<SlotClick>().currentItemID = 0;
            }
        }
        if (inventory != null && this.gameObject.name == "InventorySystem")
        {
            textCurrentWeight.text = inventory.currentWeight.ToString("0.00");
            textMaxWeight.text = inventory.maxWeight.ToString("0.00");
            if (inventory.Container.Count > 0)
            {
                SlotCheckID(inventory, slotPosInven);
                UpdateWeight(inventory);
            }
            else
            {
                inventory.currentWeight = 0;
                if(slotPosInven.Count > 0)
                {
                    for (int i = 0; i < slotPosInven.Count; i++)
                    {
                        slotPosInven[i].GetComponent<SlotClick>().currentItemID = 0;
                    }
                }
                
                //slotPosInven[0].GetComponent<SlotClick>().currentItemID = 0;
            }
        }
        if (this.gameObject.name == "InventoryToGet")
        {
            if (inventory.Container.Count > 0)
            {
                SlotCheckID(inventory, slotPosInven);
                UpdateWeight(inventory);
            }
            else
            {
                inventory.currentWeight = 0;
                if (slotPosInven.Count > 0)
                {
                    for (int i = 0; i < slotPosInven.Count; i++)
                    {
                        slotPosInven[i].GetComponent<SlotClick>().currentItemID = 0;
                    }
                }
                //slotPosInven[0].GetComponent<SlotClick>().currentItemID = 0;
            }
        }
        if (this.gameObject.name == "EquipmentSystem")
        {
            textCurrentWeight.text = equipment.currentWeight.ToString("0.00");
            textMaxWeight.text = equipment.maxWeight.ToString("0.00");
            if (_characterMovement.gunGlock)
            {
                currentAmmoInStack.text = _characterMovement.currentGlockAmmoInStake.ToString();
            }
            else if (_characterMovement.gunM16)
            {
                currentAmmoInStack.text = _characterMovement.currentRifleAmmoInStake.ToString();
            }
            else if (_characterMovement.gunShootGun)
            {
                currentAmmoInStack.text = _characterMovement.currentShotgunAmmoInStake.ToString();

            }
            ammoCount.text = _characterMovement.ammoCount.ToString();

            if (equipment.Container.Count > 0)
            {
                SlotCheckID(equipment, slotPosEquipment);
                UpdateWeight(equipment);
            }
            else
            {
                equipment.currentWeight = 0;
                slotPosEquipment[0].GetComponent<SlotClick>().currentItemID = 0;
            }

            if (equipment.Container.Count > 0 && equipment.Container[0].durability <= 0)
            {
                equipment.Container.Remove(equipment.Container[0]);
            }

            if (melee.Container.Count > 0)
            {
                SlotCheckID(melee, slotPosMelee);
                UpdateWeight(melee);
            }
            else
            {
                melee.currentWeight = 0;
                slotPosMelee[0].GetComponent<SlotClick>().currentItemID = 0;
            }

            if (melee.Container.Count > 0 && melee.Container[0].durability <= 0)
            {
                melee.Container.Remove(melee.Container[0]);
            }
        }

        if (this.gameObject.name == "EquipmentInInventory")
        {
            if(_characterMovement.gunGlock)
            {
                currentAmmoInStack.text = _characterMovement.currentGlockAmmoInStake.ToString();
            }
            else if (_characterMovement.gunM16)
            {
                currentAmmoInStack.text = _characterMovement.currentRifleAmmoInStake.ToString();
            }
            else if(_characterMovement.gunShootGun)
            {
                currentAmmoInStack.text = _characterMovement.currentShotgunAmmoInStake.ToString();
            }

            ammoCount.text = _characterMovement.ammoCount.ToString();

            if (equipment.Container.Count > 0)
            {
                SlotCheckID(equipment, slotPosEquipment);
                UpdateWeight(equipment);
            }
            else
            {
                equipment.currentWeight = 0;
                slotPosEquipment[0].GetComponent<SlotClick>().currentItemID = 0;
            }

            if (equipment.Container.Count > 0 && equipment.Container[0].durability <= 0)
            {
                equipment.Container.Remove(equipment.Container[0]);
            }

            if (melee.Container.Count > 0)
            {
                SlotCheckID(melee, slotPosMelee);
                UpdateWeight(melee);
            }
            else
            {
                melee.currentWeight = 0;
                slotPosMelee[0].GetComponent<SlotClick>().currentItemID = 0;
            }

            if (melee.Container.Count > 0 && melee.Container[0].durability <= 0)
            {
                melee.Container.Remove(melee.Container[0]);
            }
        }
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
                    //break;
                }

            }
        }

    }
}
