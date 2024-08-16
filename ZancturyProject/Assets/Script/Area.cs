using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Area : MonoBehaviour
{
    public string area;
    QuestManager quest;
    private void Start()
    {
        quest = GameObject.Find("GameManager").GetComponent<QuestManager>();
    }

    private void Update()
    {
        if (quest.currentQuest.Count > 0)
        {
            if (quest.currentQuest[0].type == Quest.QuestType.FINDAREA)
            {
                if (quest.currentQuest[0].AreaName == GameManager.Instance.area)
                {
                    quest.currentQuest[0].procress = Quest.QuestProcress.COMPLETE;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.area = area;
            if (gameObject.name == "BunkerArea" && StateVariableController.currentQuestState > 9)
            {
                SceneManager.LoadScene("Zanctuary");
            }

        }
    }

}
