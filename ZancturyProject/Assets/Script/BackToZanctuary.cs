using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToZanctuary : MonoBehaviour
{
    public GameObject goBackChoice;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            goBackChoice.SetActive(true);
        }
    }

    public void ChoiceYes()
    {
        goBackChoice.SetActive(false);
        GameManager.Instance.SummaryLootMap();
        SaveZanctuary();
    }

    public void ChoiceNo()
    {
        goBackChoice.SetActive(false);
    }

    public void SaveZanctuary()
    {
        SaveSystem.SaveZanctuary();
    }
}
