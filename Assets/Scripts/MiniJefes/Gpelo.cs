using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Gpelo : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab; 
    [SerializeField] private Transform shootPoint; 
    [SerializeField] private float shootCooldown = 2f;
    private float lastShootTime;

    public Player player;
    [SerializeField] int bulletLayer;
    [SerializeField] Animator GpeloController;
    Rigidbody rb;
    [Header("Stats")]
    public float speed = 3f;
    public float detectionRange = 10f;
    public float distanceToHit = 0.5f;
    public float life = 5;
    public bool isDead = false;
    float distanceToPlayer;

    private NavMeshAgent navMeshAgent;
    private void Start()
    {
        player = GameManager.Instance.player;
        rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange && !isDead)
        {
            // Mueve al enemigo hacia el jugador si está dentro del rango de detección
            MoveTowardsPlayer();
        }
        else
        {
            GpeloController.SetBool("IsRunning", false);
            //navMeshAgent.isStopped = true;
        }

        if (distanceToPlayer <= detectionRange && !isDead)
        {
            LookAtPlayer();

            if (Time.time >= lastShootTime + shootCooldown)
            {
                ShootProjectile();
                lastShootTime = Time.time;
            }
        }
    }
    void MoveTowardsPlayer()
    {
        GpeloController.SetBool("IsRunning", true);
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
        GpeloController.SetBool("IsRunning", false);
        GpeloController.SetTrigger("Die");
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
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
    public void Attack()
    {
        rb.velocity = Vector3.zero;
        speed = 0;
        navMeshAgent.isStopped = true;
        GpeloController.SetBool("IsRunning", false);
        GpeloController.SetTrigger("Attack");

    }
    public void FinishAttack()
    {
        speed = 3;
        navMeshAgent.isStopped = false;
    }

    private void LookAtPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void ShootProjectile()
    {
        if (projectilePrefab != null && shootPoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (player.transform.position - shootPoint.position).normalized;
                rb.velocity = direction * 10f; // Ajusta la velocidad según lo necesites
            }
            BouncyBall bouncyBallScript = projectile.GetComponent<BouncyBall>();
            if (bouncyBallScript != null)
            {
                bouncyBallScript.myBoller = this.gameObject; // Establece quién lanzó el proyectil
            }
        }
    }
}
