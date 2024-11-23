using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] public Player player;
    [SerializeField] public GameObject salida;

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
    public IEnumerator ILose()
    {
        yield return new WaitForSeconds(1);
        UIManager.Instance.StartCountdown(true);
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
                yield break; // Termina la corrutina
            }
        }
        SceneController.Instance.LoadScene("Intro");
    }
}
