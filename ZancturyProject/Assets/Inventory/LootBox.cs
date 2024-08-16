using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LootBox : MonoBehaviour
{
    public enum LootCategory { normalDropBox, KeyItem, PliersItem, GunDropBox, WeaponDropBox, FirstDaySpawn };
    [Header("==================== Category LootDrop ====================")]
    public LootCategory currentCategury;

    playerInventory _playerInven;
    [SerializeField] bool havekeyInven;
    [SerializeField] InventoryObj ZInven;
    [SerializeField] bool haveKeyZInven;
    [Header("==================== What Item Can Drop ====================")]
    public InventoryObj lootBox;
    public List<ItemObj> items = new List<ItemObj>();
    public List<ItemObj> guns = new List<ItemObj>();

    [Header("==================== Equipment Drop ====================")]
    public List<ItemObj> lootBoxDropItem = new List<ItemObj>();
    public List<float> durabulityDrop = new List<float>();
    [Header("==================== item Drop ====================")]
    public List<ItemObj> lootBoxDropGun = new List<ItemObj>();
    public List<int> amoutDrop = new List<int>();
    [Header("==================== Drop Count ====================")]
    public int allDropCout;
    public int gunDropCout;
    public int whatDrop;
    public int itemDropAmout;
    public int dropGunRate;
    public bool dropGun;
    public float gunDurability;
    public bool isSeach;
    [Header("================ Key item Box =====================")]
    //public bool isKeyItemBox;
    //public bool isPliersItemBox;
    public List<ItemObj> keyItem = new List<ItemObj>();
    QuestManager quest;
    private void Awake()
    {
        _playerInven = GameObject.Find("Player").GetComponent<playerInventory>();
        quest = GameObject.Find("GameManager").GetComponent<QuestManager>();
        if (currentCategury == LootCategory.KeyItem)
        {
            #region Check KeyItem In Inven
            if (_playerInven.inventory.Container.Count > 0)
            {
                for (int i = 0; i < _playerInven.inventory.Container.Count; i++)
                {
                    if (_playerInven.inventory.Container[i].item.itemID == 15)
                    {
                        havekeyInven = true;
                        break;
                    }
                    else
                    {
                        havekeyInven = false;
                    }
                }
            }
            else havekeyInven = false;
            #endregion

            #region Check KeyItem In ZInven
            if (ZInven.Container.Count > 0)
            {
                for (int i = 0; i < ZInven.Container.Count; i++)
                {
                    if (ZInven.Container[i].item.itemID == 15)
                    {
                        haveKeyZInven = true;
                        break;
                    }
                    else
                    {
                        haveKeyZInven = false;
                    }
                }
            }
            else haveKeyZInven = false;
            #endregion

            if (haveKeyZInven == false && havekeyInven == false && StateVariableController.currentQuestState <= 8)
            {
                KeyItemSetUp(15);
            }
            else if (StateVariableController.currentQuestState > 8)
            {
                currentCategury = LootCategory.normalDropBox;
            }
        }
        else if (currentCategury == LootCategory.PliersItem)
        {
            #region Check KeyItem In Inven
            if (_playerInven.inventory.Container.Count > 0)
            {
                for (int i = 0; i < _playerInven.inventory.Container.Count; i++)
                {
                    if (_playerInven.inventory.Container[i].item.itemID == 16)
                    {
                        havekeyInven = true;
                        break;
                    }
                    else
                    {
                        havekeyInven = false;
                    }
                }
            }
            else havekeyInven = false;

            #endregion

            #region Check KeyItem In ZInven
            if (ZInven.Container.Count > 0)
            {
                for (int i = 0; i < ZInven.Container.Count; i++)
                {
                    if (ZInven.Container[i].item.itemID == 16)
                    {
                        haveKeyZInven = true;
                        break;
                    }
                    else
                    {
                        haveKeyZInven = false;
                    }
                }
            }
            else haveKeyZInven = false;
            #endregion

            if (haveKeyZInven == false && havekeyInven == false && StateVariableController.currentQuestState <= 4)
            {
                KeyItemSetUp(16);
            }
            else if (StateVariableController.currentQuestState > 4)
            {
                currentCategury = LootCategory.normalDropBox;
            }

        }
        else if (currentCategury == LootCategory.normalDropBox)
        {
            DropSetup();
        }
        else if (currentCategury == LootCategory.GunDropBox)
        {
            GunDropSetUp();
        }
        else if (currentCategury == LootCategory.WeaponDropBox)
        {
            WeaponDropSetUp();
        }
        else if (currentCategury == LootCategory.FirstDaySpawn)
        {
            FirstDaySetup();
        }
    }


    private void Update()
    {

        if (isSeach)
        {
            lootBoxDropGun.Clear();
            durabulityDrop.Clear();
            lootBoxDropItem.Clear();
            amoutDrop.Clear();
            for (int i = 0; i < lootBox.Container.Count; i++)
            {
                if (lootBox.Container[i].item.type == ItemType.Equipment)
                {
                    lootBoxDropGun.Add(lootBox.Container[i].item);
                    durabulityDrop.Add(lootBox.Container[i].durability);
                }
                else
                {
                    lootBoxDropItem.Add(lootBox.Container[i].item);
                    amoutDrop.Add(lootBox.Container[i].amout);
                }
            }
        }

        var sumAmout = 0f;
        var itemWeight = 0f;
        var sumItemWeight = 0f;
        var sumWeight = 0f;

        for (int i = 0; i < lootBox.Container.Count; i++)
        {
            sumAmout = lootBox.Container[i].amout;
            itemWeight = lootBox.Container[i].item.weight;
            sumItemWeight = sumAmout * itemWeight;
            sumWeight = sumWeight + sumItemWeight;
        }
        lootBox.currentWeight = sumWeight;

    }

    public void FirstDaySetup()
    {
        dropGun = true;
        if (guns.Count > 0)
        {
            var gunDurability = Random.Range(10, 50);
            lootBoxDropGun.Add(guns[0]);
            durabulityDrop.Add(gunDurability);
            var weaponDurability = Random.Range(10, 50);
            lootBoxDropGun.Add(guns[1]);
            durabulityDrop.Add(weaponDurability);
        }
        if(items.Count > 0)
        {
            int ammoDrop = Random.Range(12, 24);
            lootBoxDropItem.Add(items[0]);
            amoutDrop.Add(ammoDrop);
        }
    }

    public void WeaponDropSetUp()
    {
        dropGun = true;
        if (guns.Count > 0)
        {
            gunDropCout = Random.Range(0, guns.Count);
            gunDurability = Random.Range(0, 100);
            lootBoxDropGun.Add(guns[gunDropCout]);
            durabulityDrop.Add(gunDurability);
        }
    }
    public void KeyItemSetUp(int itemID)
    {
        for (int i = 0; i < keyItem.Count; i++)
        {
            if (keyItem[i].itemID == itemID)
            {
                lootBoxDropItem.Add(keyItem[i]);
                amoutDrop.Add(1);
                break;
            }
        }
    }
    public void GunDropSetUp()
    {
        dropGun = true;
        if (guns.Count > 0)
        {
            gunDropCout = Random.Range(0, guns.Count);
            gunDurability = Random.Range(0, 100);
            lootBoxDropGun.Add(guns[gunDropCout]);
            durabulityDrop.Add(gunDurability);
            if (lootBoxDropGun[0].itemID == 5)
            {
                int ammoDrop = Random.Range(12, 24);
                if (items.Count > 0)
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i].itemID == 2)
                        {
                            lootBoxDropItem.Add(items[i]);
                            break;
                        }
                    }
                }
                amoutDrop.Add(ammoDrop);
            }
            else if (lootBoxDropGun[0].itemID == 8)
            {
                int ammoDrop = Random.Range(15, 30);
                if (items.Count > 0)
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i].itemID == 1)
                        {
                            lootBoxDropItem.Add(items[i]);
                            break;
                        }
                    }
                }
                amoutDrop.Add(ammoDrop);
            }
            else if (lootBoxDropGun[0].itemID == 10)
            {
                int ammoDrop = Random.Range(8, 16);
                if (items.Count > 0)
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i].itemID == 9)
                        {
                            lootBoxDropItem.Add(items[i]);
                            break;
                        }
                    }
                }
                amoutDrop.Add(ammoDrop);
            }
        }
    }
    public void DropSetup()
    {
        if (guns.Count != 0)
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
                lootBoxDropGun.Add(guns[gunDropCout]);
                durabulityDrop.Add(gunDurability);
            }
        }
        if (items.Count != 0)
        {
            allDropCout = Random.Range(1, 6);
            for (int i = 0; i < allDropCout; i++)
            {
                var whatDrop = Random.Range(0, items.Count);
                itemDropAmout = Random.Range(1, 6);
                lootBoxDropItem.Add(items[whatDrop]);
                amoutDrop.Add(itemDropAmout);
            }
        }
    }

}
