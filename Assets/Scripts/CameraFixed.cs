using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFixed : MonoBehaviour
{
    public Transform PlayerTransform;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var camTransform = GetComponent<Transform>();
        camTransform.position = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, -10.5f);
    }
}

