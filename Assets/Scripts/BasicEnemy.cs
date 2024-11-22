using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public Player player;
    [SerializeField] int bulletLayer;
    [Header("Stats")]
    public float speed = 3f;    
    public float detectionRange = 10f;
    public float life = 5;

    private void Start()
    {
        player = GameManager.Instance.player;
    }
    private void Update()
    {
        // Verifica la distancia entre el enemigo y el jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            // Mueve al enemigo hacia el jugador si está dentro del rango de detección
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        // Mueve al enemigo hacia la posición del jugador
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == bulletLayer)
            GetHit();
    }

    public void GetHit()
    {
        life -= 1;
        if (life <= 0)
            Die();
    }
    public void Die()
    {
        //POner animacion y hacer corrutina para que se destruya despues de la animacion
        Destroy(this);
    }
}
