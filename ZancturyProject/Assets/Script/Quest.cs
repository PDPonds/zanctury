using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Quest
{
    public enum QuestProcress { NOT_AVAILABLE, ACCEPTED, COMPLETE,}
    public QuestProcress procress;

    public enum QuestType { FINDAREA, REPAIR }
    public QuestType type;

    public Sprite questIcon;
    public int questID;
    public string title;
    public string discription;

    [Header("========== Find Area ==========")]
    public string AreaName;

    [Header("========== Repair ==========")]

    public ItemObj item1;
    public int item1Amout;

    public ItemObj item2;
    public int item2Amout;

}
