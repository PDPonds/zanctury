using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeadHandle : MonoBehaviour
{
    [Header("============ SelectToLootMap ==========")]
    [SerializeField] Button selectButton1;
    [SerializeField] Button selectButton2;
    [SerializeField] Button selectButton3;
    [Header("============ SelectToShowState ==========")]
    [SerializeField] Button stateButton1;
    [SerializeField] Button stateButton2;
    [SerializeField] Button stateButton3;
    [Header("============ SelectToHeal ==========")]
    [SerializeField] Button healButton1;
    [SerializeField] Button healButton2;
    [SerializeField] Button healButton3;
    [Header("============ SelectToHungry ==========")]
    [SerializeField] Button hungryButton1;
    [SerializeField] Button hungryButton2;
    [SerializeField] Button hungryButton3;


    void Update()
    {
        if(StateVariableController.carterIsDead == true)
        {
            selectButton1.interactable = false;
            stateButton1.interactable = false;
            healButton1.interactable = false;
            hungryButton1.interactable = false;
        }
        else
        {
            selectButton1.interactable = true;
            stateButton1.interactable = true;
            healButton1.interactable = true;
            hungryButton1.interactable = true;


        }

        if (StateVariableController.lukeIsDead == true)
        {
            selectButton2.interactable = false;
            stateButton2.interactable = false;
            healButton2.interactable = false;
            hungryButton2.interactable = false;
        }
        else
        {
            selectButton2.interactable = true;
            stateButton2.interactable = true;
            healButton2.interactable = true;
            hungryButton2.interactable = true;

        }

        if (StateVariableController.hannahIsDead == true)
        {
            selectButton3.interactable = false;
            stateButton3.interactable = false;
            healButton3.interactable = false;
            hungryButton3.interactable = false;
        }
        else
        {
            selectButton3.interactable = true;
            stateButton3.interactable = true;
            healButton3.interactable = true;
            hungryButton3.interactable = true;

        }
    }
}
