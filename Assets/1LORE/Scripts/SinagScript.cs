using MoreMountains.InventoryEngine;
using PixelCrushers;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

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
    
    // 0 - hurt
    // 1 - interact/pick
    // 2- orasyon sound effect
    private void Awake()
    {
        instance = this;
        savePath = Path.Combine(Application.persistentDataPath, "playerData.json");
    }
    private void Start()
    {
        TakeDamage(0);
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
                this.transform.position = sinag.playerPos;
                Katmbay.transform.position = sinag.playerPos;                
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
        healthText.text = $"{Health}/{MaxHealth}";
        PlaySound(0);
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
        Health = 1;
        TakeDamage(0);
        this.transform.position = spawnpoints[spawnIndex].position;
    }
}
