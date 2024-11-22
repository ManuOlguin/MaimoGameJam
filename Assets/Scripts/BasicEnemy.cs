using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public Player player;
    [SerializeField] int bulletLayer;
    [SerializeField] Animator zombieController;
    [Header("Stats")]
    public float speed = 3f;    
    public float detectionRange = 10f;
    public float distanceToHit = 2f;
    public float life = 5;
    public bool isDead=false;
    float distanceToPlayer;

    private void Start()
    {
        player = GameManager.Instance.player;
    }
    private void Update()
    {
        // Verifica la distancia entre el enemigo y el jugador
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            // Mueve al enemigo hacia el jugador si está dentro del rango de detección
            MoveTowardsPlayer();
        }
        //else
        //{
        //    zombieController.SetBool("IsRunning", false);
        //}
    }

    void MoveTowardsPlayer()
    {
        // Mueve al enemigo hacia la posición del jugador
        zombieController.SetBool("IsRunning", true);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        if (distanceToPlayer < distanceToHit)
            Attack();
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
        if (isDead) return; 
        //POner animacion y hacer corrutina para que se destruya despues de la animacion
        zombieController.SetBool("IsRunning", false);
        zombieController.SetTrigger("Die");
        speed = 0;

        isDead = true; 

        GetComponent<Collider>().enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero; 
        }
        StartCoroutine(DestroyDead());
    }
    public IEnumerator DestroyDead()
    {
        yield return new WaitForSeconds(5);
        Destroy(this);
    }
    public void Attack()
    {
        zombieController.SetBool("IsRunning", false);
        zombieController.SetTrigger("Attack");
    }
}
