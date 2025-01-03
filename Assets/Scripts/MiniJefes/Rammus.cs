using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rammus : MonoBehaviour
{
    public Player player;
    [SerializeField] int bulletLayer;
    [SerializeField] Animator amoniteController;
    public GameObject PAlma;
    Rigidbody rb;
    [Header("Stats")]
    public float speed = 3f;
    public float detectionRange = 10f;
    public float distanceToCharge = 2f;
    public float life = 10;
    public bool isDead = false;
    public float chargeSpeed = 10f; // Velocidad del impulso hacia el jugador
    public float chargeDuration = 1f; // Duraci�n de la carga
    public float spinSpeed = 360f; // Velocidad de giro
    public float stopDistanceAfterCharge = 2f; // Distancia adicional despu�s de cargar
    float distanceToPlayer;
    bool isCharging = false;

    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        player = GameManager.Instance.player;
        rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (isDead || isCharging) return;

        // Verifica la distancia entre el minijefe y el jugador
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            amoniteController.SetBool("Walk", false);
        }
    }

    void MoveTowardsPlayer()
    {
        amoniteController.SetBool("Walk", false);
        navMeshAgent.SetDestination(player.transform.position);

        if (distanceToPlayer < distanceToCharge)
        {
            StartCoroutine(ChargeAtPlayer());
        }
    }

    private IEnumerator ChargeAtPlayer()
    {
        isCharging = true;
        navMeshAgent.isStopped = true;

        // Animaci�n de giro
        amoniteController.SetBool("Walk", false);
        amoniteController.SetBool("Spin",true);
        float spinTime = 1f; // Tiempo de giro antes de la carga
        float timer = 0f;

        while (timer < spinTime)
        {
            timer += Time.deltaTime;
            //transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime); // Gira sobre el eje Y
            yield return null;
        }

        navMeshAgent.isStopped = false;
        Vector3 targetPosition = player.transform.position; // Posici�n del jugador en el momento de iniciar la carga
        navMeshAgent.speed = chargeSpeed; // Cambiar la velocidad del NavMeshAgent para la carga
        navMeshAgent.SetDestination(targetPosition);

        yield return new WaitForSeconds(chargeDuration);

        // Detener el movimiento despu�s de la carga
        Vector3 stopPosition = transform.position + (transform.forward * stopDistanceAfterCharge);
        navMeshAgent.SetDestination(stopPosition);

        yield return new WaitForSeconds(0.5f); // Espera antes de continuar
        navMeshAgent.speed = speed;
        amoniteController.SetBool("Spin", false);
        isCharging = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (isCharging && collision.gameObject == GameManager.Instance.player)
        //{
        //    // L�gica de da�o al jugador durante la carga
        //    player.TakeDamage(1);
        //}

        if (collision.gameObject.TryGetComponent<Bullet>(out Bullet bullet))
        {
            GetHit(bullet.damage);
        }
    }

    public void GetHit(float damage)
    {
        life -= damage;
        UIManager.Instance.UpdateBarraDeVida(life);
        if (life <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (isDead) return;
        navMeshAgent.isStopped = true;
        amoniteController.SetBool("Walk", false);
        amoniteController.SetTrigger("Die");
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
        Instantiate(PAlma, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
