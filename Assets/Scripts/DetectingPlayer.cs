using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectingPlayer : MonoBehaviour
{
    public EnemyAI ai;
    CircleCollider2D collider;


    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ai.Detected(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ai.Evaded();
        }
    }


    // Update is called once per frame
    public void Radius(float radius)
    {
        collider.radius = radius;
    }
}
