using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] public Player player;
    [SerializeField] public GameObject salida;
    [SerializeField] public string actualFight;

    [Header("VariablesEntreNiveles")]
    public int Life;
    public int Almas;
    public int PowerUp;
    public int MonedasCafe;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        UIManager.Instance.showBossFightUI(actualFight);
    }
    public void IWin()
    {
        if (Almas>=3)
        {
            UIManager.Instance.UpdateAlmas(3);
            Almas = 3;
        }
        salida.SetActive(true);
        //PonerMeta
    }
    public void ILose()
    {
        StartCoroutine(ILoses());
    }
    public IEnumerator ILoses()
    {
        UIManager.Instance.StartCountdown(true);
        yield return new WaitForSeconds(1);
        int countdown = 10;

        while (countdown > 0)
        {
            UIManager.Instance.UpdateCountdown(countdown); // Actualiza el contador en la UI
            yield return new WaitForSeconds(1); // Espera un segundo
            countdown--;

            if (Input.GetKeyDown(KeyCode.J)) // Permite cancelar el contador si el jugador presiona "J"
            {
                UIManager.Instance.StartCountdown(false);
                SceneController.Instance.ReloadCurrentScene();
                countdown = 10;
                Life = 6;
                Almas = 3;
                MonedasCafe = 0;
                PowerUp = 0;
                UIManager.Instance.UpdateAlmas(Almas);
                UIManager.Instance.UpdateImagePower(PowerUp);
                UIManager.Instance.UpdateVidas(Life);
                UIManager.Instance.UpdateMonedas(MonedasCafe);
                yield break; // Termina la corrutina
            }
        }
        countdown = 10;
        Life = 6;
        Almas = 3;
        MonedasCafe = 0;
        PowerUp = 0;
        UIManager.Instance.UpdateAlmas(Almas);
        UIManager.Instance.UpdateImagePower(PowerUp);
        UIManager.Instance.UpdateVidas(Life);
        UIManager.Instance.UpdateMonedas(MonedasCafe);
        UIManager.Instance.StartCountdown(false);
        SceneController.Instance.LoadScene("Intro");
    }
    //public void StartFight()
    //{
    //    UIManager.Instance.showBossFightUI(actualFight);
    //    startFiht.SetActive(false);
    //}
}
