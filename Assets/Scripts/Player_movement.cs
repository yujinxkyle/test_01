using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    private int keysCollected = 0;

    private SpriteRenderer spriteRenderer;
    private AudioManager audioManager; 
    private DoorController door;
    public bool PlayerDeath = false;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
        door = FindObjectOfType<DoorController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            //face left direction when A is pressed
            SetObjectFacingDirection(true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            //face right direction when D is pressed
            SetObjectFacingDirection(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            Destroy(collision.gameObject);
            keysCollected++;

            GameObject wall = GameObject.Find("temporary_wall");
            if (keysCollected == 1 && wall != null)
            {
                Destroy(wall);
            }

            GameObject wall_1 = GameObject.Find("temporary_wall_2");
            if (keysCollected >= 2 && wall_1 != null)
            {
                door.StageComplete();
                Destroy(wall_1);
            }

        }
        //game restarts when you hit an obstacle
        else if (collision.gameObject.tag == "Obstacles")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            AudioManager.Instance.PlaySFX("Death");
            audioManager.musicSource.Stop();
            audioManager.IsDead();
            PlayerDeath = true;
        }
        //prevent player to pass through walls
        else if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);

        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);

        }
    }

    private void SetObjectFacingDirection(bool facingLeft)
    {
        // Flip the character based on facing left
        spriteRenderer.flipX = facingLeft;
    }
}

//for door

