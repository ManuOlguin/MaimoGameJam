using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraIsometric : MonoBehaviour
{
    public Transform target; // El personaje que la c�mara seguir�
    public Vector3 offset = new Vector3(0, 10, 0); // Posici�n relativa a seguir
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }
}
