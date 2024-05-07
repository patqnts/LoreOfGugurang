using UnityEngine;

public class MoveToPosition : MonoBehaviour
{
    public float speed;
    public SpriteRenderer spriteRenderer;
    public Transform targetTransform;
    public CircleCollider2D collider2d;

    public float arrivalThreshold = 1f; // Adjust this threshold as needed

    private bool isMoving = false;

    private void Start()
    {
        MoveTo(targetTransform, speed);
    }

    // Method to initiate the movement towards the target position
    public void MoveTo(Transform targetTransform, float speed)
    {
        this.targetTransform = targetTransform;
        this.speed = speed;
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            if (targetTransform != null)
            {
                // Calculate the direction towards the target position
                Vector3 direction = (targetTransform.position - transform.position).normalized;

                // Move towards the target position
                transform.position += direction * speed * Time.deltaTime;

                // If close enough, stop movement
                if (Vector3.Distance(transform.position, targetTransform.position) <= arrivalThreshold)
                {
                    transform.position = targetTransform.position;
                    isMoving = false;
                    FlipSpriteX();
                    PixelCrushers.DialogueSystem.Sequencer.Message("SceneEnd");

                    if (collider2d != null)
                        collider2d.enabled = true;
                }
            }
            else
            {
                Debug.LogWarning("Target transform is null. Movement halted.");
                isMoving = false;
            }
        }
    }

    public void FlipSpriteX()
    {
        spriteRenderer.flipX = true;
    }
}
