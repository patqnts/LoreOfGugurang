using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsuangScript : MonoBehaviour
{
    private Transform player;
    public SpriteRenderer sprite;
    public Animator animator;
    public Transform[] movePositions;
    public GameObject fireball;
    private bool isMoving = false;
    private bool isOnCooldown = false;

    public float moveDuration = 3f; // Duration of each move
    public float idleDuration = 1f;
    public float movesetDuration = 10f; // Duration of the entire moveset
    public float cooldownDuration = 5f; // Cooldown duration between movesets

    public static AsuangScript instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        // Find the player object by tag
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

    public void AswangAttackStart()
    {
        StartCoroutine(CompleteMoveset());
    }
    public void FireBall()
    {
        Instantiate(fireball, transform.position, transform.rotation);
    }

    IEnumerator CompleteMoveset()
    {
        isOnCooldown = true;

        // Execute moveset for the specified duration
        yield return StartCoroutine(MovesetCoroutine());

        // Start cooldown after moveset duration
        yield return new WaitForSeconds(cooldownDuration);
        isOnCooldown = false;
    }

    IEnumerator MovesetCoroutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < movesetDuration)
        {
            // Select a random position from the array
            Vector3 targetPosition = movePositions[Random.Range(0, movePositions.Length)].position;

            // Play attack animation
            
            yield return new WaitForSeconds(.35f);
            // Move to the selected position
            StartCoroutine(MoveToPosition(targetPosition));
          
            // Wait for the move duration
            yield return new WaitForSeconds(moveDuration);

            // Play idle animation
            animator.Play("Idle");

            // Wait for the idle duration
            yield return new WaitForSeconds(idleDuration);

            elapsedTime += moveDuration + idleDuration;
            Debug.Log(elapsedTime);
        }
        DialogueManager.StartConversation("Liwanag");
    }

    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        isMoving = true;
        float elapsedTime = 0f;
        Vector3 startingPosition = transform.position;

        while (elapsedTime < moveDuration)
        {
            // Move towards the target position smoothly
            animator.Play("Run");
            transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure exact position at the end of movement
        transform.position = targetPosition;
        animator.Play("Attack");
        FireBall();
        isMoving = false;
    }

    public void DisableSelf()
    {
        gameObject.SetActive(false);
    }

    public void ToggleSpriteRenderer(int layer)
    {
        sprite.sortingOrder = layer;
    }
}
