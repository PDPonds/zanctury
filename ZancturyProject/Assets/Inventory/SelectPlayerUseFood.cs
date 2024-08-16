using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlayerUseFood : MonoBehaviour
{
    public InventoryObj ZInventory;
    ZanctuaryManager _ZanctuaryManager;

    private void Start()
    {
        _ZanctuaryManager = GameObject.Find("ZanctuaryManager").GetComponent<ZanctuaryManager>();
    }

    public void UseHealthItem(int numofPlayer)
    {
        if (numofPlayer == 1)
        {
            for (int i = 0; i < ZInventory.Container.Count; i++)
            {
                if (ZInventory.Container[i].item.type == ItemType.Food && ZInventory.Container[i].item.itemID == 3)
                {
                    if (StateVariableController.player1CurrentLife < 100)
                    {
                        StateVariableController.player1CurrentLife += StateVariableController.restorHP;
                        ZInventory.RemoveItem(ZInventory.Container[i].item, 1);
                        _ZanctuaryManager.selectPlayerToUseHealth.SetActive(false);
                    }
                    
                }
            }
        }
        else if (numofPlayer == 2)
        {
            for (int i = 0; i < ZInventory.Container.Count; i++)
            {
                if (ZInventory.Container[i].item.type == ItemType.Food && ZInventory.Container[i].item.itemID == 3)
                {
                    if (StateVariableController.player2CurrentLife < 100)
                    {
                        StateVariableController.player2CurrentLife += StateVariableController.restorHP;
                        ZInventory.RemoveItem(ZInventory.Container[i].item, 1);
                        _ZanctuaryManager.selectPlayerToUseHealth.SetActive(false);
                    }
                        
                }
            }
        }
        else if (numofPlayer == 3)
        {
            for (int i = 0; i < ZInventory.Container.Count; i++)
            {
                if (ZInventory.Container[i].item.type == ItemType.Food && ZInventory.Container[i].item.itemID == 3)
                {
                    if (StateVariableController.player3CurrentLife < 100)
                    {
                        StateVariableController.player3CurrentLife += StateVariableController.restorHP;
                        ZInventory.RemoveItem(ZInventory.Container[i].item, 1);
                        _ZanctuaryManager.selectPlayerToUseHealth.SetActive(false);
                    }
                        
                }
            }
        }

    }

    public void UseFoodItem(int numofPlayer)
    {
        if (numofPlayer == 1)
        {
            for (int i = 0; i < ZInventory.Container.Count; i++)
            {
                if (ZInventory.Container[i].item.type == ItemType.Food && ZInventory.Container[i].item.itemID == 4)
                {
                    if(StateVariableController.player1HungryScal < 10)
                    {
                        StateVariableController.player1HungryScal += StateVariableController.restorHungry;
                        ZInventory.RemoveItem(ZInventory.Container[i].item, 1);
                        _ZanctuaryManager.selectPlayerToUseFood.SetActive(false);
                    }
                    
                }
            }
        }
        else if (numofPlayer == 2)
        {
            for (int i = 0; i < ZInventory.Container.Count; i++)
            {
                if (ZInventory.Container[i].item.type == ItemType.Food && ZInventory.Container[i].item.itemID == 4)
                {
                    if (StateVariableController.player2HungryScal < 10)
                    {
                        StateVariableController.player2HungryScal += StateVariableController.restorHungry; ;
                        ZInventory.RemoveItem(ZInventory.Container[i].item, 1);
                        _ZanctuaryManager.selectPlayerToUseFood.SetActive(false);
                    }
                        
                }
            }
        }
        else if (numofPlayer == 3)
        {
            for (int i = 0; i < ZInventory.Container.Count; i++)
            {
                if (ZInventory.Container[i].item.type == ItemType.Food && ZInventory.Container[i].item.itemID == 4)
                {
                    if (StateVariableController.player3HungryScal < 10)
                    {
                        StateVariableController.player3HungryScal += StateVariableController.restorHungry; ;
                        ZInventory.RemoveItem(ZInventory.Container[i].item, 1);
                        _ZanctuaryManager.selectPlayerToUseFood.SetActive(false);
                    }
                }
            }
        }
    }
}
