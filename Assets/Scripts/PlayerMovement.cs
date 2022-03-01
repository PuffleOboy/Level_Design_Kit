using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Variables which can be modified in the Inspector
    public float speed = 3;

    public float currentTime = -1000f;
    // Private variables used only within this script

    // THIS IS AN UPDATED SCRIPT: direction is now public so it can be accessed by other scripts
    // HideInInspector will hide the variable below it from appearing in the Inspector
    [HideInInspector]
    public Vector2 direction;
    private Rigidbody2D rb2d;

    public Camera cam;
    public Vector2 mousePos;

    public HealthSystem playerHealth;
    public float iframes = .5f;


    private void Start()
    {
        // Set this gameobject's tag to Player. You can also do this in the Inspector
        gameObject.tag = "Player";
        // Store a reference to the rigidbody2d component to use in this script
        rb2d = GetComponent<Rigidbody2D>();

        playerHealth = new HealthSystem(5);

    }

    // Update is called once per frame
    private void Update()
    {
        // Store the values of the input axes in a Vector2 to use later
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        
    }

    // FixedUpdate is for physics
    private void FixedUpdate()
    {
        // Set the rigidbody2d velocity property to 0,0 (this stops movement)
        rb2d.velocity = Vector2.zero;


        // Add an instant forces to the rigidbody2d. The forces are the input values * the speed value
        rb2d.AddForce(direction * speed, ForceMode2D.Impulse);


        Vector2 lookDir = mousePos - rb2d.position;
        float angle = Mathf.Atan2(lookDir.y,lookDir.x) * Mathf.Rad2Deg - 90f;
        rb2d.rotation = angle;

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // If the other thing has a specific tag. It's a good idea to limit the detection to specific things
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (iframes + currentTime > Time.time)
                return;
            // Destroy this gameobject?
            playerHealth.Damage(1);

            currentTime = Time.time;

            Debug.Log("Health: " + playerHealth.GetHealth());

            if (playerHealth.GetHealth() == 0)
            {
                SceneManager.LoadScene("GameOver");
            }
            // Destroy the gameobject this one collided with? Uncomment this next line
            //Destroy(collision.gameObject);
        }
    }



}