using PixelCrushers;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public DialogueSystemSaver systemSaver;
    public GameObject backObject;
    public GameObject[] otherUI;
    bool isOpen;

    private void Start()
    {
        systemSaver = FindObjectOfType<DialogueSystemSaver>();
        DialogueManager.StopAllConversations();
    }  
   
    private void Update()
    {
        int index = 0;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (GameObject go in otherUI)
            {
                if (go.activeSelf)
                {
                    index++;
                }
            }

            if(index < 1)
            {
                isOpen = !isOpen;
                backObject.SetActive(isOpen);
            }           
        }
  
    }
    public void GoToMenu()
    {
        DialogueManager.StopAllConversations();
        //systemSaver.restoreStateOnStart = true;
        Debug.Log("Restore state menu");
        PixelCrushers.SaveSystem.SaveToSlotImmediate(1);      
        SaveSystem.LoadScene("LoreMenu");
        SaveSystem.ResetGameState();

    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
