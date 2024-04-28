using UnityEngine;

public class ScrollSideways : MonoBehaviour
{
    public float scrollSpeed = 1.0f;
    private float spriteWidth;
    private Vector3 startPosition;

    private void Start()
    {
        // Get the width of the sprite
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;

        // Get the starting position of the sprite
        startPosition = transform.position;
    }

    private void Update()
    {
        // Calculate how much to move based on time and speed
        float offset = Mathf.Repeat(Time.time * scrollSpeed, spriteWidth);

        // Calculate the new position based on the start position and the movement
        Vector3 newPos = startPosition + Vector3.left * offset;

        // Check if the sprite has moved beyond its width
        if (transform.position.x < startPosition.x - spriteWidth)
        {
            // If so, reset the position to the starting position
            transform.position = startPosition;
        }
        else
        {
            // Otherwise, apply the new position
            transform.position = newPos;
        }
    }
}
