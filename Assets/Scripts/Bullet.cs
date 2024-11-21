using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Velocidad del proyectil
    public float lifetime = 5f; // Tiempo de vida del proyectil

    void Start()
    {
        // Destruir el proyectil después de cierto tiempo
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Mover el proyectil hacia adelante
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Lógica al colisionar con algo (por ejemplo, dañar enemigos o destruir el proyectil)
        Debug.Log("Colisión con: " + collision.gameObject.name);
        Destroy(gameObject);
    }
}
