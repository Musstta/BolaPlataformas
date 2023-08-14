using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform ballTransform;
    public float yOffset = 2.0f; // posicion de la camara ligeramente por encima de la pelota

    void Update()
    {
        // movimiento vertical de la camara
        if (ballTransform != null && ballTransform.position.y + yOffset > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, ballTransform.position.y + yOffset, transform.position.z);
        }
    }

    public void SetBallTransform(Transform newBallTransform)
    {
        ballTransform = newBallTransform;
    }
}
