using System.Collections;
using UnityEngine;

public class MoveToPosition : MonoBehaviour
{
    public Transform gameObjectToMove;
    public Vector3 position;
    public float speed;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        MoveTo(position, speed);
    }
    // Method to move the gameObject to a certain position
    public void MoveTo(Vector3 targetPosition, float speed)
    {
        StartCoroutine(MoveCoroutine(targetPosition, speed));
    }

    // Coroutine to smoothly move the gameObject to the target position
    private IEnumerator MoveCoroutine(Vector3 targetPosition, float speed)
    {
        gameObjectToMove.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        while (Vector3.Distance(gameObjectToMove.position, targetPosition) > 0.01f)
        {
            // Calculate the direction towards the target position
            Vector3 direction = (targetPosition - gameObjectToMove.position).normalized;

            // Move towards the target position
            gameObjectToMove.position += direction * speed * Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the gameObject is exactly at the target position
        gameObjectToMove.position = targetPosition;
        FlipSpriteX();
        PixelCrushers.DialogueSystem.Sequencer.Message("SceneEnd");
        gameObjectToMove.gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }

    public void FlipSpriteX()
    {
        spriteRenderer.flipX = true;
    }
}