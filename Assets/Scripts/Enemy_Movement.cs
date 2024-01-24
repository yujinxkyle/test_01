using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public float speed = 5f; // Adjust this to control the speed of the enemy
    public float leftLimit = -5f; // Adjust this to set the left distance limit
    public float rightLimit = 5f; // Adjust this to set the right distance limit
    private int direction = 1; // 1 for moving right, for moving left
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
        FlipSprite();
    }

    void MoveEnemy()
    {
        // Calculate the new position based on the current direction and speed
        float newPosition = transform.position.x + direction * speed * Time.deltaTime;

        // Check if the new position is within the specified boundaries
        if (newPosition < leftLimit)
        {
            newPosition = leftLimit;
            SwitchDirection();
        }
        else if (newPosition > rightLimit)
        {
            newPosition = rightLimit;
            SwitchDirection();
        }

        // Update the enemy's position
        transform.position = new Vector2(newPosition, transform.position.y);
    }

    void SwitchDirection()
    {
        // Change the direction when hitting the boundaries
        direction *= -1;
    }

    void FlipSprite()
    {
        // Flip the sprite based on the current direction
        if (direction > 0)
        {
            spriteRenderer.flipX = true; // Facing right
        }
        else if (direction < 0)
        {
            spriteRenderer.flipX = false; // Facing left
        }
    }
}
