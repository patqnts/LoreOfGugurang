using PixelCrushers;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveTypeScript : MonoBehaviour
{
    public DialogueSystemSaver systemSaver;
    public AutoSaveLoad autoSaveLoad;
    public int saveType;
    public static SaveTypeScript instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        instance = this;
        systemSaver.restoreStateOnStart = true;
    }

    public void SetSaveType(int type)
    {
        saveType = type;

        if(saveType == 0)
        {
            SaveSystem.ResetGameState();
            systemSaver.restoreStateOnStart = true;
            SaveSystem.LoadScene("Chapter1");
        }
        else if (saveType == 1)
        {
            systemSaver.restoreStateOnStart = false;
            SaveSystem.LoadFromSlot(1);
        }

        Debug.Log($"Save type {saveType}");
    }
}
