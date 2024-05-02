using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public GameObject backObject;
    public GameObject[] otherUI;
    bool isOpen;
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
        SceneManager.LoadScene("LoreMenu");
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
