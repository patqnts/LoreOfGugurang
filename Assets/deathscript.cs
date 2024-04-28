using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathscript : MonoBehaviour
{
    public GameObject ch4trigger;
    public GameObject ch4blocker;
    public void ConfirmButton()
    {
        SinagScript.instance.Respawn();       
        PlayerController.player.moveSpeed = 3;
        ch4trigger.SetActive(true);
        ch4blocker.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
