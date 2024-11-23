using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public GameObject Pprojectile;
    public GameObject PpowerUp;
    public Transform shootPoint;
    public int segundosReiniciar;
    public bool IsMoving = false;
    private UIManager _uiManager;
    [SerializeField] Animator PlayerController;
    private Rigidbody rb;
    public ParticleSystem magicCircle;

    [Header("Stats")]
    public float speed = 5f;
    public float rotationSpeed = 720f;
    public float life = 6;
    private float shootCooldown = 0.3f;  
    private float lastShootTime = 0f;
    public bool IsInmortal = true;

    [Header("Layers")]
    [SerializeField] int powerUpLayer;
    [SerializeField] int AlmaLayer;
    [SerializeField] int MonedasLayer;
    [SerializeField] int Cafes;
    [SerializeField] int Salida;

    private void Start()
    {
        Debug.Log(powerUpLayer+" "+ AlmaLayer);
        _uiManager = UIManager.Instance;
        rb = GetComponent<Rigidbody>();
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
            Vector3 moveDirection = movement.normalized * speed;
            rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
            // Rotar al personaje hacia la direcci�n de movimiento
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            IsMoving = true;
            PlayerController.SetBool("IsRunning", true);
        }
        else
        {
            IsMoving = false;
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            PlayerController.SetBool("IsRunning", false);
        }
        if (Input.GetKeyDown(KeyCode.J) && Time.time > lastShootTime + shootCooldown)
        {
            Shoot();
            lastShootTime = Time.time;
        }
        void Shoot()
        {
            Instantiate(Pprojectile, shootPoint.position, shootPoint.rotation);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (GameManager.Instance.PowerUp != 0)
            {
                magicCircle.Play();
                switch (GameManager.Instance.PowerUp)
                {
                    case 1:
                        life += 2;
                        GameManager.Instance.PowerUp = 0;
                        //UIManager.Instance.Life.text = "Vidas: " + life;
                        _uiManager.UpdateVidas(life);
                        _uiManager.UpdateImagePower(GameManager.Instance.PowerUp);
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
        PlayerController.SetBool("IsRunning", false);
        if (IsMoving)
            PlayerController.SetTrigger("HitRunning");
        else
            PlayerController.SetTrigger("HitIdle");
        if (life <= 0)
            Die();
        else
        {
            if(!IsInmortal)
            {
                life -= damage;
                _uiManager.UpdateVidas(life);
            }
            //UIManager.Instance.Life.text = "Vidas: " + life;
        }
    }
    public void Die()
    {
        GameManager.Instance.ILose();
        //StartCoroutine(Countdown());
    }
    //public IEnumerator Countdown()
    //{
    //    //Prender imagen con los 10 sgs
    //    yield return new WaitForSeconds(10);
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == powerUpLayer)
        {
            Debug.Log("ASDHJASD");
            //PARA PIES DE MANTECA Y CHUPA SANGRE 8
            GameManager.Instance.PowerUp = Random.Range(1,6);
            _uiManager.UpdateImagePower(GameManager.Instance.PowerUp);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer == AlmaLayer)
        {
            GameManager.Instance.Almas += 1;
            _uiManager.UpdateAlmas(GameManager.Instance.Almas);
            //UIManager.Instance.Almas.text = "Almas Restantes: " + (3 - GameManager.Instance.Almas);
            if (GameManager.Instance.Almas >= 3)
                GameManager.Instance.IWin();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer == MonedasLayer)
        {
            GameManager.Instance.MonedasCafe += 1;
            _uiManager.UpdateMonedas(GameManager.Instance.MonedasCafe);
            //UIManager.Instance.Monedas.text = "Monedas: " + GameManager.Instance.MonedasCafe;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer == Cafes)
        {
            if (GameManager.Instance.MonedasCafe >= 1)
            {
                BuyPowerUp(collision.gameObject);
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.layer == Salida)
        {
            SceneController.Instance.LoadNextScene();
        }
        //if (collision.gameObject.layer == 18)
        //{
        //    GameManager.Instance.StartFight();
        //}
    }

    //public void CanEscape()
    //{
    //    // Hacer el escape
    //    //UIManager.Instance.Almas.text = "Almas Restantes: 3";
    //    _uiManager.UpdateAlmas(3);
    //    GameManager.Instance.Almas = 3;
    //}
    #region PowerUps
    public void BuyPowerUp(GameObject coffeMachine)
    {
        //GameManager.Instance.PowerUp = Random.Range(1, 7);
        Instantiate(PpowerUp,new Vector3(coffeMachine.transform.position.x,5f, coffeMachine.transform.position.z),Quaternion.identity);    
        GameManager.Instance.MonedasCafe -= 1;
        _uiManager.UpdateMonedas(GameManager.Instance.MonedasCafe);
    }
    public IEnumerator Inmortal()
    {
        //Aca lo que hace
        //Poner que arranque un efecto visual para saber
        //GetComponent<CapsuleCollider>().enabled = false;
        IsInmortal = true;
        GameManager.Instance.PowerUp = 0;
        _uiManager.UpdateImagePower(GameManager.Instance.PowerUp);
        _uiManager.Inmortal(true);
        yield return new WaitForSeconds(10);
        //Aca volver a la normalidad
        _uiManager.Inmortal(true);
        IsInmortal = false;
        GetComponent<CapsuleCollider>().enabled = true;
    }
    public IEnumerator Rampage()
    {
        //Aca lo que hace
        shootCooldown = 0.1f;
        GameManager.Instance.PowerUp = 0;
        _uiManager.UpdateImagePower(GameManager.Instance.PowerUp);
        yield return new WaitForSeconds(10);
        shootCooldown = 0.3f;
        //Aca volver a la normalidad
    }
    public IEnumerator Velocity()
    {
        //Aca lo que hace
        speed *= 2;
        GameManager.Instance.PowerUp = 0;
        _uiManager.UpdateImagePower(GameManager.Instance.PowerUp);
        yield return new WaitForSeconds(10);
        speed /= 2;
        //Aca volver a la normalidad
    }
    public IEnumerator BiggerSmolder()
    {
        //Aca lo que hace
        GameManager.Instance.PowerUp = 0;
        gameObject.transform.localScale /= 2;
        speed *= 1.3f;
        _uiManager.UpdateImagePower(GameManager.Instance.PowerUp);
        yield return new WaitForSeconds(10);
        gameObject.transform.localScale *= 2;
        speed /= 1.3f;
        //Aca volver a la normalidad
    }
    public IEnumerator ChupaSangre()
    {
        //Aca lo que hace
        if(!IsMoving)
        {
            //Prender efectos
            while (!IsMoving) 
            {
                yield return new WaitForSeconds(1f); 

                if (!IsMoving) 
                {
                    life -= 1;
                    _uiManager.UpdateVidas(life);
                    //UIManager.Instance.Life.text = "Vidas: " + life;
                    Debug.Log(life);

                    if (life <= 0)
                    {
                        Die();
                        yield break; 
                    }
                }
            }

            Debug.Log("El jugador comenz� a moverse. Se detiene la p�rdida de corazones.");
        }
        GameManager.Instance.PowerUp = 0;
        _uiManager.UpdateImagePower(GameManager.Instance.PowerUp);
        yield return new WaitForSeconds(10);
        //Aca volver a la normalidad
    }
    public IEnumerator PiesDeManteca()
    {
        //Aca lo que hace
        if (IsMoving)
        {
            //Prender efectos
            while (IsMoving)
            {
                yield return new WaitForSeconds(1f);

                if (IsMoving)
                {
                    if (Random.Range(4, 8) > 4)
                        speed = 0;
                }
            }

            Debug.Log("El jugador comenz� a moverse. Se detiene la p�rdida de corazones.");
        }
        GameManager.Instance.PowerUp = 0;
        _uiManager.UpdateImagePower(GameManager.Instance.PowerUp);
        yield return new WaitForSeconds(10);
        //Aca volver a la normalidad
    }
    #endregion
}
