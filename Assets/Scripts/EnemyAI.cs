using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    Transform target;

    public LayerMask layerMask;

    DetectingPlayer detector;
    public float idleRadius, chaseRadius;

    public enum EnemyState
    {
        Idle,
        Chase
    }
    public EnemyState state;


    // Start is called before the first frame update
    void Start()
    {
        state = EnemyState.Idle;
        detector = GetComponentInChildren<DetectingPlayer>();

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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, target.position - transform.position, 100, layerMask);

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

}