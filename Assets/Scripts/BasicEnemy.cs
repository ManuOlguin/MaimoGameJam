using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    public Player player;
    [SerializeField] int bulletLayer;
    [SerializeField] Animator zombieController;
    Rigidbody rb;
    [Header("Stats")]
    public float speed = 3f;    
    public float detectionRange = 10f;
    public float distanceToHit = 0.5f;
    public float life = 5;
    public bool isDead=false;
    float distanceToPlayer;
    public AudioSource zombieSound;
    public AudioSource zombieSound2;

    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        player = GameManager.Instance.player;
        rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        // Verifica la distancia entre el enemigo y el jugador
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange && !isDead)
        {
            // Mueve al enemigo hacia el jugador si est� dentro del rango de detecci�n
            MoveTowardsPlayer();
        }
        else
        {
            zombieController.SetBool("IsRunning", false);
            //navMeshAgent.isStopped = true;
        }
    }

    void MoveTowardsPlayer()
    {
        zombieController.SetBool("IsRunning", true);
        //Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        //Vector3 direction = (player.transform.position - transform.position).normalized;
        //Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        //navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.transform.position);
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        if (distanceToPlayer < distanceToHit)
        {
            navMeshAgent.isStopped = true;
            Attack();
        }
        if (distanceToPlayer > distanceToHit)
            FinishAttack();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Bullet>(out Bullet bullet))
            GetHit(bullet.damage);
    }

    public void GetHit(float damage)
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

        
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero; 
        }
        navMeshAgent.isStopped = true;
        StartCoroutine(DestroyDead());
    }
    public IEnumerator DestroyDead()
    {
        zombieSound.Play();
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
    public void Attack()
    {
        rb.velocity = Vector3.zero;
        speed = 0;
        navMeshAgent.isStopped = true;
        zombieController.SetBool("IsRunning", false);
        zombieController.SetTrigger("Attack");
        zombieSound2.Play();
        
    }
    public void FinishAttack()
    {
        speed = 3;
        navMeshAgent.isStopped = false;
    }
}
