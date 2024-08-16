using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ZanctuaryManager : MonoBehaviour
{
    public static ZanctuaryManager Instance;

    [SerializeField] InventoryObj Lootbox;
    public InventoryObj ZInventory;
    public InventoryObj RemoveZAnctuary;
    [SerializeField] private TextMeshProUGUI dayText;

    [Header("====================================== Day Summarize")]
    [SerializeField] private GameObject summarizeUI;
    [SerializeField] private GameObject selecPlayerUI;
    public GameObject selectPlayerToUseFood;
    public GameObject selectPlayerToUseHealth;

    [Header("========== Select Player to loot map")]
    [SerializeField] private TextMeshProUGUI namePlayer;

    [Header("========== Current State Player Summary")]
    [SerializeField] private TextMeshProUGUI player1Injury;
    [SerializeField] private TextMeshProUGUI player2Injury;
    [SerializeField] private TextMeshProUGUI player3Injury;

    [SerializeField] private TextMeshProUGUI player1Hungry;
    [SerializeField] private TextMeshProUGUI player2Hungry;
    [SerializeField] private TextMeshProUGUI player3Hungry;

    [Header("========== Current State Player To Select")]
    [SerializeField] private TextMeshProUGUI player1InjuryToSelect;
    [SerializeField] private TextMeshProUGUI player2InjuryToSelect;
    [SerializeField] private TextMeshProUGUI player3InjuryToSelect;

    [SerializeField] private TextMeshProUGUI player1HungryToSelect;
    [SerializeField] private TextMeshProUGUI player2HungryToSelect;
    [SerializeField] private TextMeshProUGUI player3HungryToSelect;

    [SerializeField] private int minDropAmout;
    [SerializeField] private int maxDropAmout;
    [SerializeField] private int minDropCout;
    [SerializeField] private int maxDropCout;

    [Header("========== Die All Character")]
    [SerializeField] private GameObject EndGameCharacter;
    [SerializeField] GameObject carterCharacter;
    [SerializeField] GameObject lukeCharacter;
    [SerializeField] GameObject hannahCharacter;

    [SerializeField] GameObject carterSlash;
    [SerializeField] GameObject lukeSlash;
    [SerializeField] GameObject hannahSlash;

    [Header("============= LoadingScene ==========")]
    public GameObject LoadingScene;
    public Slider slider;

    [Header("============= Audio ===========")]
    public Sound[] sound;

    public void OpenUISound()
    {
        Play("OpenUI");
    }

    public void CloseUISound()
    {
        Play("CloseUI");

    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

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
        selectPlayerToUseFood.SetActive(false);
        selectPlayerToUseHealth.SetActive(false);
        summarizeUI.SetActive(false);
        selecPlayerUI.SetActive(false);
        Lootbox.Container.Clear();
        Lootbox.currentWeight = 0;
        Play("EnterZanctuary");
        Play("Campfire");
        Play("BGAudio");
        
    }
    private void Update()
    {
        minDropCout = StateVariableController.minDropCount;
        maxDropCout = StateVariableController.maxDropCount;
        minDropAmout = StateVariableController.minDropAmout;
        maxDropAmout = StateVariableController.maxDropAmout;
        dayText.text = StateVariableController.currentDay.ToString();

        #region player Hungry And HP Update State

        #region HungryState
        if (StateVariableController.player1HungryScal > 8)
        {
            StateVariableController.current1Hungry = "Be Full";
            StateVariableController.player1MaxStamina = 100;
        }
        else if (StateVariableController.player1HungryScal > 6 && StateVariableController.player1HungryScal < 9)
        {
            StateVariableController.current1Hungry = "Stomach growling";
            StateVariableController.player1MaxStamina = 80;
        }
        else if (StateVariableController.player1HungryScal > 4 && StateVariableController.player1HungryScal < 7)
        {
            StateVariableController.current1Hungry = "Hungry";
            StateVariableController.player1MaxStamina = 60;
        }
        else if (StateVariableController.player1HungryScal > 2 && StateVariableController.player1HungryScal < 5)
        {
            StateVariableController.current1Hungry = "Very Hungry";
            StateVariableController.player1MaxStamina = 40;
        }
        else if (StateVariableController.player1HungryScal < 3 && StateVariableController.player1HungryScal > 0)
        {
            StateVariableController.current1Hungry = "Starving";
            StateVariableController.player1MaxStamina = 20;
        }
        else if (StateVariableController.player1HungryScal <= 0)
        {
            StateVariableController.current1Hungry = "Can't Run";
            StateVariableController.player1MaxStamina = 10;
        }

        if (StateVariableController.player1HungryScal > 10)
        {
            StateVariableController.player1HungryScal = 10;
        }

        if (StateVariableController.player1HungryScal < 0)
        {
            StateVariableController.player1HungryScal = 0;
        }
        #endregion
        #region HPState
        if (StateVariableController.player1CurrentLife > 79f)
        {
            StateVariableController.current1Injury = "Healthy";

        }
        else if (StateVariableController.player1CurrentLife > 49f && StateVariableController.player1CurrentLife < 80f)
        {
            StateVariableController.current1Injury = "Wouded";

        }
        else if (StateVariableController.player1CurrentLife > 20f && StateVariableController.player1CurrentLife < 50f)
        {
            StateVariableController.current1Injury = "Heavy Wounded";

        }
        else if (StateVariableController.player1CurrentLife < 20f && StateVariableController.player1CurrentLife > 0f)
        {
            StateVariableController.current1Injury = "Fatal Wounded";
        }

        if (StateVariableController.player1CurrentLife <= 0f)
        {
            StateVariableController.current1Injury = "Die";
        }
        #endregion

        #region HungryState
        if (StateVariableController.player2HungryScal > 8)
        {
            StateVariableController.current2Hungry = "Be Full";
            StateVariableController.player2MaxStamina = 80;
        }
        else if (StateVariableController.player2HungryScal > 6 && StateVariableController.player2HungryScal < 9)
        {
            StateVariableController.current2Hungry = "Stomach growling";
            StateVariableController.player2MaxStamina = 80;
        }
        else if (StateVariableController.player2HungryScal > 4 && StateVariableController.player2HungryScal < 7)
        {
            StateVariableController.current2Hungry = "Hungry";
            StateVariableController.player2MaxStamina = 60;
        }
        else if (StateVariableController.player2HungryScal > 2 && StateVariableController.player2HungryScal < 5)
        {
            StateVariableController.current2Hungry = "Very Hungry";
            StateVariableController.player2MaxStamina = 40;
        }
        else if (StateVariableController.player2HungryScal < 3 && StateVariableController.player2HungryScal > 0)
        {
            StateVariableController.current2Hungry = "Starving";
            StateVariableController.player2MaxStamina = 20;
        }
        else if (StateVariableController.player2HungryScal <= 0)
        {
            StateVariableController.current2Hungry = "Can't Run";
            StateVariableController.player2MaxStamina = 10;
        }

        if (StateVariableController.player2HungryScal > 10)
        {
            StateVariableController.player2HungryScal = 10;
        }
        
        if (StateVariableController.player2HungryScal < 0)
        {
            StateVariableController.player2HungryScal = 0;
        }
        #endregion
        #region HPState
        if (StateVariableController.player2CurrentLife > 79f)
        {
            StateVariableController.current2Injury = "Healthy";

        }
        else if (StateVariableController.player2CurrentLife > 49f && StateVariableController.player2CurrentLife < 80f)
        {
            StateVariableController.current2Injury = "Wouded";

        }
        else if (StateVariableController.player2CurrentLife > 20f && StateVariableController.player2CurrentLife < 50f)
        {
            StateVariableController.current2Injury = "Heavy Wounded";

        }
        else if (StateVariableController.player2CurrentLife < 20f && StateVariableController.player2CurrentLife > 0f)
        {
            StateVariableController.current2Injury = "Fatal Wounded";
        }
        
        if (StateVariableController.player2CurrentLife <= 0f)
        {
            StateVariableController.current2Injury = "Die";
        }
        #endregion

        #region HungryState
        if (StateVariableController.player3HungryScal > 8)
        {
            StateVariableController.current3Hungry = "Be Full";
            StateVariableController.player3MaxStamina = 100;
        }
        else if (StateVariableController.player3HungryScal > 6 && StateVariableController.player3HungryScal < 9)
        {
            StateVariableController.current3Hungry = "Stomach growling";
            StateVariableController.player3MaxStamina = 80;
        }
        else if (StateVariableController.player3HungryScal > 4 && StateVariableController.player3HungryScal < 7)
        {
            StateVariableController.current3Hungry = "Hungry";
            StateVariableController.player3MaxStamina = 60;
        }
        else if (StateVariableController.player3HungryScal > 2 && StateVariableController.player3HungryScal < 5)
        {
            StateVariableController.current3Hungry = "Very Hungry";
            StateVariableController.player3MaxStamina = 40;
        }
        else if (StateVariableController.player3HungryScal < 3 && StateVariableController.player3HungryScal > 0)
        {
            StateVariableController.current3Hungry = "Starving";
            StateVariableController.player3MaxStamina = 20;
        }
        else if (StateVariableController.player3HungryScal <= 0)
        {
            StateVariableController.current3Hungry = "Can't Run";
            StateVariableController.player3MaxStamina = 10;
        }

        if (StateVariableController.player3HungryScal > 10)
        {
            StateVariableController.player3HungryScal = 10;
        }
         
        if (StateVariableController.player3HungryScal < 0)
        {
            StateVariableController.player3HungryScal = 0;
        }
        #endregion
        #region HPState
        if (StateVariableController.player3CurrentLife > 79f)
        {
            StateVariableController.current3Injury = "Healthy";

        }
        else if (StateVariableController.player3CurrentLife > 49f && StateVariableController.player3CurrentLife < 80f)
        {
            StateVariableController.current3Injury = "Wouded";

        }
        else if (StateVariableController.player3CurrentLife > 20f && StateVariableController.player3CurrentLife < 50f)
        {
            StateVariableController.current3Injury = "Heavy Wounded";

        }
        else if (StateVariableController.player3CurrentLife < 20f && StateVariableController.player3CurrentLife > 0f)
        {
            StateVariableController.current3Injury = "Fatal Wounded";
        }
        
        if (StateVariableController.player3CurrentLife <= 0f)
        {
            StateVariableController.current3Injury = "Die";
        }
        #endregion

        #endregion

        if (StateVariableController.carterIsDead == false)
        {
            player1HungryToSelect.text = StateVariableController.current1Hungry;
            player1InjuryToSelect.text = StateVariableController.current1Injury;
            carterCharacter.SetActive(true);
            carterSlash.SetActive(false);
        }
        else
        {
            player1HungryToSelect.text = "Die";
            player1InjuryToSelect.text = "Die";
            player1InjuryToSelect.color = Color.red;
            player1HungryToSelect.color = Color.red;
            carterCharacter.SetActive(false);
            carterSlash.SetActive(true);
        }

        if (StateVariableController.lukeIsDead == false)
        {
            player2HungryToSelect.text = StateVariableController.current2Hungry;
            player2InjuryToSelect.text = StateVariableController.current2Injury;
            lukeCharacter.SetActive(true);
            lukeSlash.SetActive(false);
        }
        else
        {
            player2HungryToSelect.text = "Die";
            player2InjuryToSelect.text = "Die";
            player2HungryToSelect.color = Color.red;
            player2InjuryToSelect.color = Color.red;
            lukeCharacter.SetActive(false);
            lukeSlash.SetActive(true);
        }

        if (StateVariableController.hannahIsDead == false)
        {
            player3HungryToSelect.text = StateVariableController.current3Hungry;
            player3InjuryToSelect.text = StateVariableController.current3Injury;
            hannahCharacter.SetActive(true);
            hannahSlash.SetActive(false);
        }
        else
        {
            player3HungryToSelect.text = "Die";
            player3InjuryToSelect.text = "Die";
            player3HungryToSelect.color = Color.red;
            player3InjuryToSelect.color = Color.red;
            hannahCharacter.SetActive(false);
            hannahSlash.SetActive(true);
        }

        #region cookingStove
        if (StateVariableController.cookingStoveLevel <= 0)
        {
            StateVariableController.cookingStoveLevel = 1;
        }
        else if (StateVariableController.cookingStoveLevel == 1)
        {
            StateVariableController.restorHungry = 1;
        }
        else if (StateVariableController.cookingStoveLevel == 2)
        {
            StateVariableController.restorHungry = 1.5f;
        }
        else if (StateVariableController.cookingStoveLevel == 3)
        {
            StateVariableController.restorHungry = 2;
        }
        else if (StateVariableController.cookingStoveLevel > 3)
        {
            StateVariableController.cookingStoveLevel = 3;
        }
        #endregion

        #region medStation
        if (StateVariableController.medStationLevel <= 0)
        {
            StateVariableController.medStationLevel = 1;
            StateVariableController.restorHP = 10f;
        }
        else if (StateVariableController.medStationLevel == 1)
        {
            StateVariableController.restorHP = 10f;
        }
        else if (StateVariableController.medStationLevel == 2)
        {
            StateVariableController.restorHP = 20f;
        }
        else if (StateVariableController.medStationLevel == 3)
        {
            StateVariableController.restorHP = 30f;
        }
        else if (StateVariableController.medStationLevel > 3)
        {
            StateVariableController.medStationLevel = 3;
            StateVariableController.restorHP = 30f;
        }
        #endregion

        #region security
        if (StateVariableController.securityLevel <= 0)
        {
            StateVariableController.securityLevel = 1;
            StateVariableController.removeDurability = 10f;
        }
        else if (StateVariableController.securityLevel == 1)
        {
            StateVariableController.removeDurability = 10f;
        }
        else if (StateVariableController.securityLevel == 2)
        {
            StateVariableController.removeDurability = 7f;
        }
        else if (StateVariableController.securityLevel == 3)
        {
            StateVariableController.removeDurability = 5f;
        }
        else if (StateVariableController.securityLevel > 3)
        {
            StateVariableController.securityLevel = 3;
            StateVariableController.removeDurability = 5f;
        }
        #endregion

        for (int i = 0; i < ZInventory.Container.Count; i++)
        {
            if (ZInventory.Container[i].item.type == ItemType.Equipment && ZInventory.Container[i].durability <= 0)
            {
                ZInventory.Container.Remove(ZInventory.Container[i]);
            }
        }

        if (StateVariableController.player1CurrentLife >= 120)
        {
            StateVariableController.player1CurrentLife = 120;
        }
        if (StateVariableController.player2CurrentLife >= 100)
        {
            StateVariableController.player2CurrentLife = 100;
        }
        if (StateVariableController.player3CurrentLife >= 90)
        {
            StateVariableController.player3CurrentLife = 90;
        }

        if (StateVariableController.carterIsDead == true &&
            StateVariableController.lukeIsDead == true &&
            StateVariableController.hannahIsDead == true)
        {
            EndGameCharacter.SetActive(true);
        }

    }

    public void DaySummarize()
    {


        if (StateVariableController.carterIsDead == false)
        {
            if (StateVariableController.player1HungryScal <= 0)
            {
                StateVariableController.player1CurrentLife -= 20f;
            }

            player1Injury.text = StateVariableController.current1Injury;
            player1Hungry.text = StateVariableController.current1Hungry;
        }
        else
        {
            player1Injury.text = "Die";
            player1Hungry.text = "Die";
            player1Injury.color = Color.red;
            player1Hungry.color = Color.red;
        }

        if (StateVariableController.lukeIsDead == false)
        {
            player2Injury.text = StateVariableController.current2Injury;
            player2Hungry.text = StateVariableController.current2Hungry;

            if (StateVariableController.player2HungryScal <= 0)
            {
                StateVariableController.player2CurrentLife -= 20f;
            }
        }
        else
        {
            player2Injury.text = "Die";
            player2Hungry.text = "Die";
            player2Injury.color = Color.red;
            player2Hungry.color = Color.red;
        }

        if (StateVariableController.hannahIsDead == false)
        {
         
            if (StateVariableController.player3HungryScal <= 0)
            {
                StateVariableController.player3CurrentLife -= 20f;
            }

            player3Injury.text = StateVariableController.current3Injury;
            player3Hungry.text = StateVariableController.current3Hungry;
        }
        else
        {
            player3Injury.text = "Die";
            player3Hungry.text = "Die";
            player3Injury.color = Color.red;
            player3Hungry.color = Color.red;
        }


        if (StateVariableController.currentDay % 2 == 0)
        {
            StateVariableController.minDropAmout++;
            StateVariableController.maxDropAmout++;
            StateVariableController.minDropCount++;
            StateVariableController.maxDropCount++;
        }

        if (ZInventory.Container.Count != 0)
        {
            var whatItemDropCount = UnityEngine.Random.Range(minDropCout, maxDropCout);
            if (whatItemDropCount > ZInventory.Container.Count)
            {
                whatItemDropCount = ZInventory.Container.Count;
            }
            for (int i = 0; i < whatItemDropCount; i++)
            {
                if (whatItemDropCount < ZInventory.Container.Count)
                {
                    var whatSlotDrop = UnityEngine.Random.Range(0, ZInventory.Container.Count);
                    var whatAmoutDrop = UnityEngine.Random.Range(minDropAmout, maxDropAmout);
                    if (ZInventory.Container[whatSlotDrop].item.type != ItemType.Equipment && ZInventory.Container[whatSlotDrop].item.type != ItemType.KeyItem)
                    {
                        RemoveZAnctuary.AddItem(ZInventory.Container[whatSlotDrop].item, whatAmoutDrop);
                        ZInventory.RemoveItem(ZInventory.Container[whatSlotDrop].item, whatAmoutDrop);
                    }
                }
                else if (whatItemDropCount == ZInventory.Container.Count)
                {
                    if (ZInventory.Container[i].item.type != ItemType.Equipment && ZInventory.Container[i].item.type != ItemType.KeyItem)
                    {
                        var whatAmoutDrop = UnityEngine.Random.Range(minDropAmout, maxDropAmout);
                        RemoveZAnctuary.AddItem(ZInventory.Container[i].item, whatAmoutDrop);
                        ZInventory.RemoveItem(ZInventory.Container[i].item, whatAmoutDrop);
                    }
                }

            }
        }

        List<InventorySlot> slotEq = new List<InventorySlot>();
        for (int i = 0; i < ZInventory.Container.Count; i++)
        {
            if (ZInventory.Container[i].item.type == ItemType.Equipment)
            {
                slotEq.Add(ZInventory.Container[i]);
            }
        }
        for (int i = 0; i < slotEq.Count; i++)
        {
            slotEq[i].durability -= StateVariableController.removeDurability;
        }

        summarizeUI.SetActive(true);
        SaveZanctuary();
    }

    

    public void nextToSelecPlayer()
    {
        RemoveZAnctuary.Container.Clear();
        summarizeUI.SetActive(false);
        selecPlayerUI.SetActive(true);
    }

    public void SelectPlayer1Button()
    {
        StateVariableController.player1Select = true;
        StateVariableController.player2Select = false;
        StateVariableController.player3Select = false;
        namePlayer.text = "Carter";
    }
    public void SelectPlayer2Button()
    {
        StateVariableController.player2Select = true;
        StateVariableController.player1Select = false;
        StateVariableController.player3Select = false;
        namePlayer.text = "Luke";
    }
    public void SelectPlayer3Button()
    {
        StateVariableController.player3Select = true;
        StateVariableController.player2Select = false;
        StateVariableController.player1Select = false;
        namePlayer.text = "Hannah";
    }

    #region LoadScene No LoadingScene
    //public void EndDay(string _nextscene)
    //{
    //    if (StateVariableController.player1Select == true || StateVariableController.player2Select == true || StateVariableController.player3Select == true)
    //    {
    //        StateVariableController.currentDay++;
    //        SceneManager.LoadScene(_nextscene);
    //    }
    //}
    #endregion

    #region LoadScene And Loading

    public void EndDay(int sceneIndex)
    {
        if (StateVariableController.player1Select == true || StateVariableController.player2Select == true || StateVariableController.player3Select == true)
        {
            StateVariableController.currentDay++;
            StartCoroutine(LoadAsynchronously(sceneIndex));
        }
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        LoadingScene.SetActive(true);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;

            yield return null;
        }
    }

    #endregion

    public void showUI(GameObject _UI)
    {
        OpenUISound();
        _UI.SetActive(true);
    }

    public void closeUI(GameObject _UI)
    {
        CloseUISound();
        _UI.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        SaveZanctuary();
    }

    public void SaveZanctuary()
    {
        if (SceneManager.GetActiveScene().name == "Zanctuary")
        {
            SaveSystem.SaveZanctuary();
        }
    }

    public void ExitToMainMenu(int sceneIndex)
    {
        SaveZanctuary();
        StartCoroutine(LoadAsynchronously(sceneIndex));
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