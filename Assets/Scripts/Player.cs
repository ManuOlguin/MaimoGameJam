using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 720f;
    public GameObject Pprojectile;
    public Transform shootPoint; // El punto desde donde se disparará
    public float projectileSpeed = 10f;

    void Update()
    {
        // Obtener entrada de movimiento
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Crear un vector de movimiento
        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        if (movement.magnitude > 0.1f)
        {
            // Mover al personaje
            transform.position += movement.normalized * speed * Time.deltaTime;

            // Rotar al personaje hacia la dirección de movimiento
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Shoot();
        }
        void Shoot()
        {
            // Instanciar el proyectil en el punto de disparo
            GameObject projectile = Instantiate(Pprojectile, shootPoint.position, shootPoint.rotation);

            // Agregar velocidad al proyectil
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = shootPoint.forward * projectileSpeed;
            }
        }
    }
}
