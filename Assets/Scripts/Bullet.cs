using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; 
    public float lifetime = 5f;
    public float damage = 1;
    public ParticleSystem explotion;

    void Start()
    {
        // Destruir el proyectil despu�s de cierto tiempo
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Mover el proyectil hacia adelante
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(Destroying());
        
        // L�gica al colisionar con algo (por ejemplo, da�ar enemigos o destruir el proyectil)
        //Debug.Log("Colisi�n con: " + collision.gameObject.name);
        
    }
    IEnumerator Destroying()
    {
        explotion.Play();
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
