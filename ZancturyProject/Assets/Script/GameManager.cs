using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    CharacterMovement _playerMovement;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private GameObject summaryLootMapUI;

    [SerializeField] private TextMeshProUGUI playerInjury;
    [SerializeField] private TextMeshProUGUI playerHungryState;
    [SerializeField] private Image currentPlayerImage;
    [SerializeField] private Sprite carterImage;
    [SerializeField] private Sprite lukeImage;
    [SerializeField] private Sprite hannahImage;

    public InventoryObj playerInventory;
    [SerializeField] private InventoryObj equipment;
    [SerializeField] private InventoryObj melee;
    public InventoryObj ZInventory;

    public Sound[] sound;

    [SerializeField] TextMeshProUGUI currentArea;
    public string area;

    public GameObject LoadingScene;
    public Slider slider;

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
        Play("BGAudio");
        dayText.text = StateVariableController.currentDay.ToString();
        _playerMovement = GameObject.Find("Player").GetComponent<CharacterMovement>();
    }
    private void Update()
    {
        currentArea.text = area.ToString();

        if(summaryLootMapUI.activeSelf)
        {
            if(StateVariableController.player1Select)
            {
                playerInjury.text = StateVariableController.current1Injury;
                playerHungryState.text = StateVariableController.current1Hungry;
                currentPlayerImage.sprite = carterImage;
            }
            else if (StateVariableController.player2Select)
            {
                playerInjury.text = StateVariableController.current2Injury;
                playerHungryState.text = StateVariableController.current2Hungry;
                currentPlayerImage.sprite = lukeImage;
            }
            else if (StateVariableController.player3Select)
            {
                playerInjury.text = StateVariableController.current3Injury;
                playerHungryState.text = StateVariableController.current3Hungry;
                currentPlayerImage.sprite = hannahImage;
            }
        }
    }
    public void  SummaryLootMap()
    {
        #region calculateExp

        if (StateVariableController.player1Select == true)
        {
            StateVariableController.player1CurrentLife = _playerMovement.playerLife;
            StateVariableController.player1HungryScal -= 2;
            StateVariableController.player2HungryScal -= 1;
            StateVariableController.player3HungryScal -= 1;
 
        }
        else if (StateVariableController.player2Select == true)
        {
            StateVariableController.player2CurrentLife = _playerMovement.playerLife;
            StateVariableController.player2HungryScal -= 2;
            StateVariableController.player1HungryScal -= 1;
            StateVariableController.player3HungryScal -= 1;

        }
        else if (StateVariableController.player3Select == true)
        {
            StateVariableController.player3CurrentLife = _playerMovement.playerLife;
            StateVariableController.player3HungryScal -= 2;
            StateVariableController.player2HungryScal -= 1;
            StateVariableController.player1HungryScal -= 1;

        }
        #endregion
        summaryLootMapUI.SetActive(true);

    }

    #region LoadScene And Loading
    public void GoBack(int sceneIndex)
    {
        if (StateVariableController.player1Select == true)
        {
            StateVariableController.player1Select = false;
        }
        else if (StateVariableController.player2Select == true)
        {
            StateVariableController.player2Select = false;
        }
        else if (StateVariableController.player3Select == true)
        {
            StateVariableController.player3Select = false;
        }

        takeItemToZactuary();
        //SceneManager.LoadScene(_nextscene);
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

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

    #endregion

    private void takeItemToZactuary()
    {
        for (int i = 0; i < playerInventory.Container.Count; i++)
        {
            if (playerInventory.Container[i].item.type == ItemType.Equipment)
            {
                ZInventory.Container.Add(playerInventory.Container[i]);
            }
            else
            {
                ZInventory.AddItem(playerInventory.Container[i].item, playerInventory.Container[i].amout);
            }
        }
        if (equipment.Container.Count > 0)
        {
            ZInventory.Container.Add(equipment.Container[0]);
            equipment.Container.Clear();
        }
        if(melee.Container.Count > 0)
        {
            ZInventory.Container.Add(melee.Container[0]);
            melee.Container.Clear();
        }

        playerInventory.Container.Clear();
        playerInventory.currentWeight = 0;
    }

    private void OnApplicationQuit()
    {
        if (SceneManager.GetActiveScene().name == "LootMap")
        {
            playerInventory.Container.Clear();
            melee.Container.Clear();
            equipment.Container.Clear();
        }
    }

    public void ReturnToMainMenu(int sceneIndex)
    {
        playerInventory.Container.Clear();
        melee.Container.Clear();
        equipment.Container.Clear();
        //SceneManager.LoadScene("MainMenu");
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
