using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement; // For reloading

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI livesText;
    public GameObject winTextObject;

    // Static lives stay at 2 or 1 even after the scene reloads
    public static int lives = 3; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        SetLivesText();
        winTextObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }      
    }

    public void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        
        if (count >= 2)
        {
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            Destroy(GameObject.FindGameObjectWithTag("Door"));
            Destroy(GameObject.FindGameObjectWithTag("Holder5"));
            Destroy(GameObject.FindGameObjectWithTag("Holder6"));
        }
        if (count >= 4)
        {
            Destroy(GameObject.FindGameObjectWithTag("Enemy2"));
            Destroy(GameObject.FindGameObjectWithTag("Door2"));
            Destroy(GameObject.FindGameObjectWithTag("Holder3"));
            Destroy(GameObject.FindGameObjectWithTag("Holder4"));
        }
        if (count >= 6)
        {
            Destroy(GameObject.FindGameObjectWithTag("Enemy3"));
            Destroy(GameObject.FindGameObjectWithTag("Door3"));
        }
        if (count >= 8)
        {
            Destroy(GameObject.FindGameObjectWithTag("Enemy4"));
            Destroy(GameObject.FindGameObjectWithTag("Door4"));
            Destroy(GameObject.FindGameObjectWithTag("Holder1"));
            Destroy(GameObject.FindGameObjectWithTag("Holder2"));
        }
        if (count >= 10)
        {
            winTextObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "YOU WIN!";
            Destroy(GameObject.FindGameObjectWithTag("Enemy5"));
            lives = 3; // Reset lives for next time
        }
    }

    void SetLivesText()
    {
        if (livesText != null)
            livesText.text = "Lives: " + lives.ToString();
    }
    void Update()
    {
        if (transform.position.y < -10)
        {
        HandleDeath();
        }
    } 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || 
            collision.gameObject.CompareTag("Enemy2") || 
            collision.gameObject.CompareTag("Enemy3") || 
            collision.gameObject.CompareTag("Enemy4") || 
            collision.gameObject.CompareTag("Enemy5"))
        {
            HandleDeath();
        }
    }
    void HandleDeath()
    {
        lives--;

        if (lives > 0)
        {
            // Reloads scene, which resets pickups and doors
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            //final death
            Destroy(gameObject);
            winTextObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "GAME OVER!";
            lives = 3; // Reset static lives for when they click Play again
        }
    }
}