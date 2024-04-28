using PixelCrushers;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTypeScript : MonoBehaviour
{
    public static SaveTypeScript instance;
    public DialogueSystemSaver systemSaver;
    public AutoSaveLoad autoSaveLoad;
    public int saveType;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetSaveType(int type)
    {
        saveType = type;

        if(saveType == 0)
        {
            systemSaver.restoreStateOnStart = true;
        }
        else if (saveType == 1)
        {
            SaveSystem.LoadFromSlot(1);
        }
    }
}
