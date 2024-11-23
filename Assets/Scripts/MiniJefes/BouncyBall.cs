using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBall : MonoBehaviour
{
    public int maxBounces = 3; // Máximo de rebotes permitidos
    private int bounceCount = 0;
    public GameObject myBoller;
    public bool bounced;
    public float returnSpeed = 10f;

    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Detecta si el proyectil colisiona con algo que no sea el jugador
        if (collision.gameObject == GameManager.Instance.player)
        {
            // Aquí puedes aplicar daño al jugadorç
            GameManager.Instance.player.PlayerGetHit(1);
            //Destroy(gameObject);
        }
        else
        {
            if(!bounced)
            {
                bounceCount++;

                // Si alcanza el máximo de rebotes, destruye el proyectil
                if (bounceCount >= maxBounces)
                {
                    //Destroy(gameObject);
                    Regreso();
                }
                StartCoroutine(NextBounce());
            }
        }
    }
    public void Regreso()
    {
        //mi lugar hacia el de MyBoller
        StartCoroutine(ReturnToBoller());
    }
    private IEnumerator ReturnToBoller()
    {
        rb.isKinematic = true; // Detenemos la física para controlar manualmente el movimiento
        while (Vector3.Distance(transform.position, myBoller.transform.position) > 0.1f)
        {
            Vector3 direction = (myBoller.transform.position - transform.position).normalized;
            transform.position += direction * returnSpeed * Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject); // Destruye la pelota al llegar al lanzador
    }
    public IEnumerator NextBounce()
    {
        bounced = true;
        yield return new WaitForSeconds(0.3f);
        bounced = false;
    }
}
