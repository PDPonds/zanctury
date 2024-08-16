using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItemToPlayerUI : MonoBehaviour
{
    [SerializeField] private Button player1Food;
    [SerializeField] private Button player2Food;
    [SerializeField] private Button player3Food;

    [SerializeField] private Button player1Health;
    [SerializeField] private Button player2Health;
    [SerializeField] private Button player3Health;

    [SerializeField] Image carterSlash;
    [SerializeField] Image lukeSlash;
    [SerializeField] Image hannahSlash;

    void Update()
    {
        if (gameObject.name == "SelectPlayerToUseHealth")
        {
            if (StateVariableController.carterIsDead == false)
            {
                carterSlash.gameObject.SetActive(false);
                if (StateVariableController.player1CurrentLife >= 120f)
                {
                    player1Health.interactable = false;
                }
                else if (StateVariableController.player1CurrentLife < 120f)
                {
                    player1Health.interactable = true;
                }
            }
            else
            {
                carterSlash.gameObject.SetActive(true);
                player1Health.interactable = false;
            }

            ///////////////////////////
            if (StateVariableController.lukeIsDead == false)
            {
                lukeSlash.gameObject.SetActive(false);
                if (StateVariableController.player2CurrentLife >= 100f)
                {
                    player2Health.interactable = false;
                }
                else if (StateVariableController.player2CurrentLife < 100f)
                {
                    player2Health.interactable = true;
                }
            }
            else
            {
                lukeSlash.gameObject.SetActive(true);
                player2Health.interactable = false;
            }

            ////////////////////////////
            if (StateVariableController.hannahIsDead == false)
            {
                if (StateVariableController.player3CurrentLife >= 90f)
                {
                    player3Health.interactable = false;
                }
                else if (StateVariableController.player3CurrentLife < 90f)
                {
                    player3Health.interactable = true;
                }
            }
            else
            {
                hannahSlash.gameObject.SetActive(true);
                player3Health.interactable = false;
            }

        }
        else if (gameObject.name == "SelectPlayerToUseFood")
        {
            if (StateVariableController.carterIsDead == false)
            {
                if (StateVariableController.player1HungryScal >= 10)
                {
                    player1Food.interactable = false;
                }
                else if (StateVariableController.player1HungryScal < 10)
                {
                    player1Food.interactable = true;
                }
            }
            else
            {
                carterSlash.gameObject.SetActive(true);
                player1Food.interactable = false;
            }

            ///////////////////////////
            if (StateVariableController.lukeIsDead == false)
            {
                if (StateVariableController.player2HungryScal >= 10)
                {
                    player2Food.interactable = false;
                }
                else if (StateVariableController.player2HungryScal < 10)
                {
                    player2Food.interactable = true;
                }
            }
            else
            {
                lukeSlash.gameObject.SetActive(true);
                player2Food.interactable = false;
            }


            ////////////////////////////
            if (StateVariableController.hannahIsDead == false)
            {
                if (StateVariableController.player3HungryScal >= 10)
                {
                    player3Food.interactable = false;
                }
                else if (StateVariableController.player3HungryScal < 10)
                {
                    player3Food.interactable = true;
                }
            }
            else
            {
                hannahSlash.gameObject.SetActive(true);
                player3Food.interactable = false;
            }
        }
    }
}
