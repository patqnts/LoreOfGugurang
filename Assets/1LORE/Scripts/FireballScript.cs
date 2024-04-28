using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    public float speed = 5f; // Speed of the fireball
    public float lifetime = 5f; // Lifetime of the fireball
    public Animator animator; // Reference to the animator component

    private Transform player; // Reference to the player's transform
    private bool isHit = false; // Flag to track if the fireball has hit the player
    public AudioSource audiosource;
    void Start()
    {
        // Find the player object by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Move the fireball in the direction of the player
        Vector3 direction = (player.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * speed;

        // Start the timer for the fireball's lifetime
        Invoke("DestroyFireball", lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            // Set the flag to indicate that the fireball has hit the player
            Vector3 direction = (player.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity = direction * 0f;
            isHit = true;
            SinagScript.instance.TakeDamage(1);

            // Play hit animation
             
        }
        animator.Play("Hit");
    }
    public void DestroyFireball()
    {
       
            // Destroy the fireball
            Destroy(gameObject);
        
    }
}
