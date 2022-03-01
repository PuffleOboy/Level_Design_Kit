using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    Transform target;

    protected Rigidbody2D rb2d;

    public DetectingPlayer detector;
    public float idleRadius, chaseRadius;

    private HealthSystem EnemyHealth;

    public enum EnemyState
    {
        Idle,
        Chase
    }
    public EnemyState state;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.isKinematic = true;
        state = EnemyState.Idle;
        EnemyHealth = new HealthSystem(7);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == EnemyState.Chase)
        {
            Vector3 direction = target.position - transform.position;
            transform.position += (direction.normalized * speed) * Time.deltaTime;
        }

        else if (state == EnemyState.Idle)
        {
            if (target != null)
            {
                SightCheck();
            }
        }

    }
    void SightCheck()
    {
        

        RaycastHit2D hit = Physics2D.Raycast(transform.position, target.position - transform.position, Mathf.Infinity, LayerMask.GetMask("Player"));
        if (hit.collider.gameObject.tag == "Player")
        {
            
            state = EnemyState.Chase;
            detector.Radius(chaseRadius);
        }
    }





    public void Detected(Transform detectedTransform)
    {
        target = detectedTransform;


    }

    public void Evaded()
    {
        target = null;
        state = EnemyState.Idle;
        detector.Radius(idleRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the other thing has a specific tag. It's a good idea to limit the detection to specific things
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Destroy this gameobject?
            EnemyHealth.Damage(1);
            Debug.Log("Health: " + EnemyHealth.GetHealth());

            if (EnemyHealth.GetHealth() == 0)
            {
                Destroy(gameObject);
            }
            // Destroy the gameobject this one collided with? Uncomment this next line
            //Destroy(collision.gameObject);
        }
    }

}