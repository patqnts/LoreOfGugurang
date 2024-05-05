using MoreMountains.InventoryEngine;
using PixelCrushers;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SinagScript : MonoBehaviour
{
    public int Health;
    public int MaxHealth;
    public Transform[] spawnpoints; 
    public int spawnIndex; 
    public Inventory weapon;
    public Inventory main;
    public static SinagScript instance;
    public Text healthText;
    private string savePath;
    public GameObject deathScreen;
    public AudioSource audioSource;
    public AudioClip[] clips;
    public GameObject Katmbay;
    public InventoryItem[] ITEMS;
    public VideoPlayer vp;

    // 0 - hurt
    // 1 - interact/pick
    // 2- orasyon sound effect

    private string[] keySequence = { "L", "G", "A" }; // Define your combination key sequence here
    private int currentKeyIndex = 0;

    void Update()
    {
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyCode))
            {
                if (keyCode.ToString() == keySequence[currentKeyIndex])
                {
                    currentKeyIndex++;

                    if (currentKeyIndex >= keySequence.Length)
                    {
                        CheatCode();
                        currentKeyIndex = 0;
                    }
                }
                else
                {
                    currentKeyIndex = 0;
                }
            }
        }
    }

    private void CheatCode()
    {
        PlayerController.player.moveSpeed = 8;
        MMInventoryEvent.Trigger(MMInventoryEventType.Pick, null, "RogueMainInventory", ITEMS[0], 100, 15, "Player1");
        MMInventoryEvent.Trigger(MMInventoryEventType.Pick, null, "RogueMainInventory", ITEMS[1], 100, 16, "Player1");
        MMInventoryEvent.Trigger(MMInventoryEventType.Pick, null, "RogueMainInventory", ITEMS[2], 100, 17, "Player1");       
        if(vp != null)
        {
            vp.playbackSpeed = 6;
        }
        
    }
    private void Awake()
    {
        instance = this;
        savePath = Path.Combine(Application.persistentDataPath, "playerData.json");
    }
    private void Start()
    {
        SetHealth();
    }
    public void ResetGame()
    {
        ResetInventory(weapon);
        ResetInventory(main);
        SaveSystem.ResetGameState();
        SaveSystem.LoadScene("LoreMenu");

    }
    public void SetSpawnIndex(int index)
    {
        spawnIndex = index;
        AudioHandler.instance.StateMusic();
    }
    public void PlaySound(int index)
    {
        audioSource.clip = clips[index];
        audioSource.Play();
    }

    public void SavePlayerData()
    {
        SinagData sinagData = new SinagData();
        sinagData.Health = Health;
        sinagData.spawnIndex = this.spawnIndex;
        sinagData.playerPos = this.transform.position;


        string json = JsonUtility.ToJson(sinagData);
        File.WriteAllText(savePath, json);
        Debug.Log(savePath + " " + json);
    }

    public void LoadPlayerData()
    {

        PlayerController.player.LoadInventory();
        //RESET OR CONTINUE
        SaveTypeScript sts = FindObjectOfType<SaveTypeScript>();
        if (sts != null)
        {
            if (File.Exists(savePath) && sts.saveType == 1)
            {
                Debug.Log(savePath);
                string json = File.ReadAllText(savePath);
                SinagData sinag = JsonUtility.FromJson<SinagData>(json);
                Health = sinag.Health;
                spawnIndex = sinag.spawnIndex;
                if(spawnIndex == 5)
                {
                    this.transform.position = spawnpoints[5].position;
                }
                else
                {
                    this.transform.position = sinag.playerPos;
                }               
                Katmbay.transform.position = this.transform.position;                
                Debug.Log("Continue");
            }
            else if (sts.saveType == 0) // NEW GAME
            {
                ResetInventory(weapon);
                ResetInventory(main);
                Debug.Log("New Game");              
            }

            
        }  
    }


    public void ResetInventory(Inventory inventory)
    {
        inventory.EmptyInventory();
    }


    public void TakeDamage(int damage)
    {
        if(Health > 0)
        {
            Health -= damage;
            
        }

        if(Health <= 0)
        {
            Debug.Log("Death");
            Health = 0;
            //Restart spawnpoint
            DeathMethod();
        }
        
        PlaySound(0);
        SetHealth();
    }

    public void SetHealth()
    {
        healthText.text = $"{Health}/{MaxHealth}";
    }

    public void DeathMethod() 
    {
        //Player Spawnpoint script
        PlayerController.player.moveSpeed = 0;
        AsuangScript.instance.ToggleSpriteRenderer(2);
        deathScreen.SetActive(true);
        AsuangScript.instance.StopAllCoroutines();
       
    }

    public void Respawn()
    {
        AudioHandler.instance.SetBossFight(false);
        DialogueManager.StopAllConversations();
        Health = 1;
        if(spawnIndex >= 5)
        {
            Health = MaxHealth;
        }
        SetHealth();
        this.transform.position = spawnpoints[spawnIndex].position;
    }
}
