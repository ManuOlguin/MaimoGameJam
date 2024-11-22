using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public GameObject Pprojectile;
    public Transform shootPoint;
    public int segundosReiniciar;

    [Header("Stats")]
    public float speed = 5f;
    public float rotationSpeed = 720f;
    public float life = 6;

    [Header("Layers")]
    [SerializeField] int powerUpLayer;
    [SerializeField] int AlmaLayer;
    [SerializeField] int MonedasLayer;
    [SerializeField] int Cafes;

    private void Start()
    {
        Debug.Log(powerUpLayer+" "+ AlmaLayer);
    }
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

            // Rotar al personaje hacia la direcci�n de movimiento
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Shoot();
        }
        void Shoot()
        {
            Instantiate(Pprojectile, shootPoint.position, shootPoint.rotation);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (GameManager.Instance.PowerUp != 0)
            {
                switch (GameManager.Instance.PowerUp)
                {
                    case 1:
                        life += 2;
                        break;
                    case 2:
                        StartCoroutine(Inmortal());
                        break;
                    case 3:
                        StartCoroutine(Rampage());
                        break;
                    case 4:
                        StartCoroutine(Velocity());
                        break;
                    case 5:
                        StartCoroutine(BiggerSmolder());
                        break;
                    case 6:
                        StartCoroutine(ChupaSangre());
                        break;
                    case 7:
                        StartCoroutine(PiesDeManteca());
                        break;
                }
            }
        }
    }

    public void PlayerGetHit(int damage)
    {
        if (life <= 0)
            Die();
        else
            life -= damage;
    }
    public void Die()
    {
        StartCoroutine(Countdown());
    }
    public IEnumerator Countdown()
    {
        //Prender imagen con los 10 sgs
        yield return new WaitForSeconds(10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == powerUpLayer)
        {

            Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer == AlmaLayer)
        {
            GameManager.Instance.Almas += 1;
            if (GameManager.Instance.Almas >= 4)
                CanEscape();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer == MonedasLayer)
        {
            GameManager.Instance.MonedasCafe += 1;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer == Cafes)
        {
            if (GameManager.Instance.MonedasCafe >= 1)
                BuyPowerUp();
            Destroy(collision.gameObject);
        }
    }

    public void CanEscape()
    {
        // Hacer el escape
    }
    public void BuyPowerUp()
    {
        GameManager.Instance.PowerUp = Random.Range(1, 7);
    }
    public IEnumerator Inmortal()
    {
        //Aca lo que hace
        yield return new WaitForSeconds(10);
        //Aca volver a la normalidad
    }
    public IEnumerator Rampage()
    {
        //Aca lo que hace
        yield return new WaitForSeconds(10);
        //Aca volver a la normalidad
    }
    public IEnumerator Velocity()
    {
        //Aca lo que hace
        yield return new WaitForSeconds(10);
        //Aca volver a la normalidad
    }
    public IEnumerator BiggerSmolder()
    {
        //Aca lo que hace
        yield return new WaitForSeconds(10);
        //Aca volver a la normalidad
    }
    public IEnumerator ChupaSangre()
    {
        //Aca lo que hace
        yield return new WaitForSeconds(10);
        //Aca volver a la normalidad
    }
    public IEnumerator PiesDeManteca()
    {
        //Aca lo que hace
        yield return new WaitForSeconds(10);
        //Aca volver a la normalidad
    }
}
