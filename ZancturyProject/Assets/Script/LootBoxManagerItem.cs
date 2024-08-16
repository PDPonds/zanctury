using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootBoxManagerItem : MonoBehaviour, Iinteractable
{
    public InventoryObj lootBox;

    public GameObject inventoryUI;
    public GameObject SearchInterface;

    public bool isSearch;
    playerInventory _playerInventory;

    private void Awake()
    {
        _playerInventory = GameObject.Find("Player").GetComponent<playerInventory>();
        inventoryUI = _playerInventory.inventoryUI;
        SearchInterface = _playerInventory.lootUI;
        isSearch = false;
        lootBox = this.GetComponent<LootBox>().lootBox;
    }

    public void Interact()
    {
        isSearch = !isSearch;
        if (isSearch)
        {
            _playerInventory.lootBox = lootBox;
            var item = gameObject.GetComponent<LootBox>();
            var uiDisplay = item.lootBox;
            if (item)
            {
                item.isSeach = true;
                if (item.dropGun)
                {
                    for (int i = 0; i < item.lootBoxDropGun.Count; i++)
                    {
                        lootBox.AddItem(item.lootBoxDropGun[i], 1, item.durabulityDrop[i]);
                    }
                }
                for (int i = 0; i < item.lootBoxDropItem.Count; i++)
                {
                    try
                    {
                        lootBox.AddItem(item.lootBoxDropItem[i], item.amoutDrop[i]);
                    }
                    catch
                    {
                        lootBox.Container.Clear();
                    }
                }
                GameManager.Instance.OpenUISound();
                SearchInterface.SetActive(true);
                lootBox = null;
                inventoryUI.SetActive(true);
            }
            _playerInventory.equipmentInInventory.SetActive(false);
        }
        else
        {
            _playerInventory.lootBox = null;
            var item = this.GetComponent<LootBox>();
            item.isSeach = false;
            var uiDisplay = item.lootBox;
            if (item)
            {
                GameManager.Instance.CloseUISound();
                SearchInterface.SetActive(false);
                lootBox = uiDisplay; ;
                inventoryUI.SetActive(false);
                lootBox.Container.Clear();
            }
        }

    }
}
