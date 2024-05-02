using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform target;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject player = collision.gameObject;

        if (player != null)
        {
            if (player.tag == "Player" || player.tag == "Bio")
            {
                player.transform.position = target.position;
            }           
        }
    }
}
