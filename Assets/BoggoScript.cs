using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BoggoScript : MonoBehaviour
{
    private Transform player;
    public SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is on the left
        if (player.position.x < transform.position.x)
        {
            // Flip the sprite horizontally
            sprite.flipX = true;
        }
        // Check if player is on the right
        else if (player.position.x > transform.position.x)
        {
            // Unflip the sprite horizontally
            sprite.flipX = false;
        }
    }
}
