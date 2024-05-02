using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public SaveTypeScript sts;

    private void Start()
    {
        sts = FindObjectOfType<SaveTypeScript>();
    }
    public void Continue()
    {
        sts.SetSaveType(1);
    }

    public void NewGame()
    {
        sts.SetSaveType(0);
    }
}
