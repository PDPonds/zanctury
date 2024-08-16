using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ZanctuaryData
{
    public int currentDay;

    public float carterHP;
    public float lukeHP;
    public float hannahHP;

    public float carterHungryScal;
    public float lukeHungryScal;
    public float hannahHungryScal;

    public string carterInjury;
    public string lukeInjury;
    public string hannahInjury;

    public string carterHungry;
    public string lukeHungry;
    public string hannahHungry;

    public float carterStamina;
    public float lukeStamina;
    public float hannahStamina;

    public int minDropAmout;
    public int maxDropAmout;

    public int minDropCount;
    public int maxDropCount;

    public int cookingStoveLevel1;
    public float restorHungry;

    public int medStationLevel;
    public float restorHP;

    public int securityLevel;
    public float removeDurability;

    public bool carterIsDead = false;
    public bool lukeIsDead = false;
    public bool hannahIsDead = false;

    public bool radioFix = false;
    public int currentQuestState;

    public ZanctuaryData()
    {
        currentDay = StateVariableController.currentDay;

        carterHP = StateVariableController.player1CurrentLife;
        lukeHP = StateVariableController.player2CurrentLife;
        hannahHP = StateVariableController.player3CurrentLife;

        carterHungryScal = StateVariableController.player1HungryScal;
        lukeHungryScal = StateVariableController.player2HungryScal;
        hannahHungryScal = StateVariableController.player3HungryScal;

        carterInjury = StateVariableController.current1Injury;
        lukeInjury = StateVariableController.current2Injury;
        hannahInjury = StateVariableController.current3Injury;

        carterHungry = StateVariableController.current1Hungry;
        lukeHungry = StateVariableController.current2Hungry;
        hannahHungry = StateVariableController.current3Hungry;

        carterStamina = StateVariableController.player1MaxStamina;
        lukeStamina = StateVariableController.player2MaxStamina;
        hannahStamina = StateVariableController.player3MaxStamina;

        minDropAmout = StateVariableController.minDropAmout;
        maxDropAmout = StateVariableController.maxDropAmout;

        minDropCount = StateVariableController.minDropCount;
        maxDropCount = StateVariableController.maxDropCount;

        cookingStoveLevel1 = StateVariableController.cookingStoveLevel;
        restorHungry = StateVariableController.restorHungry;

        medStationLevel = StateVariableController.medStationLevel;
        restorHP = StateVariableController.restorHP;

        securityLevel = StateVariableController.securityLevel;
        removeDurability = StateVariableController.removeDurability;

        carterIsDead = StateVariableController.carterIsDead;
        lukeIsDead = StateVariableController.lukeIsDead;
        hannahIsDead = StateVariableController.hannahIsDead;

        radioFix = StateVariableController.radioFixAlready;
        currentQuestState = StateVariableController.currentQuestState;
    }

}
