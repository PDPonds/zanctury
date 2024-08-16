using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateVariableController : MonoBehaviour
{
    #region Valiable
    public static int currentDay = 1;

    public static bool player1Select = false;
    public static bool player2Select = false;
    public static bool player3Select = false;

    public static float player1CurrentLife = 120;
    public static float player2CurrentLife = 100;
    public static float player3CurrentLife = 90;

    public static float player1HungryScal = 10;
    public static float player2HungryScal = 10;
    public static float player3HungryScal = 10;

    public static string current1Hungry;
    public static string current2Hungry;
    public static string current3Hungry;

    public static string current1Injury;
    public static string current2Injury;
    public static string current3Injury;

    public static float player1MaxStamina;
    public static float player2MaxStamina;
    public static float player3MaxStamina;

    public static int minDropAmout = 1;
    public static int maxDropAmout = 3;

    public static int minDropCount = 1;
    public static int maxDropCount = 3;

    public static int cookingStoveLevel = 1;
    public static float restorHungry = 1;

    public static int medStationLevel = 1;
    public static float restorHP = 10f;

    public static int securityLevel = 1;
    public static float removeDurability = 10;

    public static bool carterIsDead = false;
    public static bool lukeIsDead = false;
    public static bool hannahIsDead = false;

    //====================== Quest ============================
    public static int currentQuestState = 0;

    public static bool radioFixAlready = false;

    //=========================================================//
    public static float carterMaxLife = 120;//
    public static float carterMultipleMeleeDamage = 0.5f;//
    public static float carterMultipleWalkSpeed = 0.2f;//

    public static float lukeAngleDistance;
    public static float lukeReloadDelay = 1f;//
    public static float lukeMaxStamina = 80f;//

    public static float hannahStealthDamage = 0.2f;//
    public static float hannahMultipleNoiseArea = 0.2f;//
    public static float hannahMaxLife = 90;//
    //=========================================================//

    #endregion


    public float life1;
    public float life2;
    public float life3;

    public float hungry1;
    public float hungry2;
    public float hungry3;


    private void Update()
    {
        life1 = player1CurrentLife;
        life2 = player2CurrentLife;
        life3 = player3CurrentLife;

        hungry1 = player1HungryScal;
        hungry2 = player2HungryScal;
        hungry3 = player3HungryScal;

    }
}
