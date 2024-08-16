using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject makeSureToNewGame;
    [SerializeField] InventoryObj ZInventory;
    [SerializeField] InventoryObj RemoveZInventory;
    [SerializeField] InventoryObj playerInventory;
    [SerializeField] GameObject LoadingScene;
    [SerializeField] Slider slider;


    #region NewGameButton
    public void NewGame()
    {
        makeSureToNewGame.SetActive(true);
    }
    #endregion
    #region Yes
    public void NewGameYes(int sceneIndex)
    {
        StateVariableController.currentDay = 1;

        StateVariableController.player1CurrentLife = 120;
        StateVariableController.player2CurrentLife = 100;
        StateVariableController.player3CurrentLife = 90;

        StateVariableController.player1HungryScal = 10;
        StateVariableController.player2HungryScal = 10;
        StateVariableController.player3HungryScal = 10;

        StateVariableController.player1MaxStamina = 100;
        StateVariableController.player2MaxStamina = 100;
        StateVariableController.player3MaxStamina = 90;

        StateVariableController.minDropAmout = 1;
        StateVariableController.maxDropAmout = 3;

        StateVariableController.minDropCount = 1;
        StateVariableController.maxDropCount = 3;

        StateVariableController.cookingStoveLevel = 1;
        StateVariableController.restorHungry = 1;

        StateVariableController.medStationLevel = 1;
        StateVariableController.restorHP = 10;

        StateVariableController.securityLevel = 1;
        StateVariableController.removeDurability = 10;

        StateVariableController.carterIsDead = false;
        StateVariableController.lukeIsDead = false;
        StateVariableController.hannahIsDead = false;

        StateVariableController.radioFixAlready = false;
        StateVariableController.currentQuestState = 0;

        SaveSystem.SaveZanctuary();
        ZInventory.Container.Clear();
        playerInventory.Container.Clear();
        ZInventory.currentWeight = 0;
        RemoveZInventory.currentWeight = 0;
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    #endregion
    #region No
    public void NewGameNo()
    {
        makeSureToNewGame.SetActive(false);
    }
    #endregion

    #region LoadGame
    public void LoadZanctuary(int sceneIndex)
    {
        ZanctuaryData data = SaveSystem.LoadZanctuary();

        StateVariableController.currentDay = data.currentDay;

        StateVariableController.player1CurrentLife = data.carterHP;
        StateVariableController.player2CurrentLife = data.lukeHP;
        StateVariableController.player3CurrentLife = data.hannahHP;

        StateVariableController.player1HungryScal = data.carterHungryScal;
        StateVariableController.player2HungryScal = data.lukeHungryScal;
        StateVariableController.player3HungryScal = data.hannahHungryScal;

        StateVariableController.current1Injury = data.carterInjury;
        StateVariableController.current2Injury = data.lukeInjury;
        StateVariableController.current3Injury = data.hannahInjury;

        StateVariableController.current1Hungry = data.carterHungry;
        StateVariableController.current2Hungry = data.lukeHungry;
        StateVariableController.current3Hungry = data.hannahHungry;

        StateVariableController.player1MaxStamina = data.carterStamina;
        StateVariableController.player2MaxStamina = data.lukeStamina;
        StateVariableController.player3MaxStamina = data.hannahStamina;

        StateVariableController.minDropAmout = data.minDropAmout;
        StateVariableController.maxDropAmout = data.maxDropAmout;

        StateVariableController.minDropCount = data.minDropCount;
        StateVariableController.maxDropCount = data.maxDropCount;

        StateVariableController.cookingStoveLevel = data.cookingStoveLevel1;
        StateVariableController.restorHungry = data.restorHungry;

        StateVariableController.medStationLevel = data.medStationLevel;
        StateVariableController.restorHP = data.restorHP;

        StateVariableController.securityLevel = data.securityLevel;
        StateVariableController.removeDurability = data.removeDurability;

        StateVariableController.carterIsDead = data.carterIsDead;
        StateVariableController.lukeIsDead = data.lukeIsDead;
        StateVariableController.hannahIsDead = data.hannahIsDead;

        StateVariableController.radioFixAlready = data.radioFix;
        StateVariableController.currentQuestState = data.currentQuestState;

        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    #endregion

    #region QuitGame
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        LoadingScene.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;

            yield return null;
        }
    }

    public Sound[] sound;

    private void Awake()
    {
        foreach (Sound s in sound)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("BGAudio");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sound, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " no found");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sound, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " no found");
            return;
        }
        s.source.Pause();
    }
}
