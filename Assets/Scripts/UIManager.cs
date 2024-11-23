using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public Image PowerUp;
    public Sprite[] ImagesPowers;
    public Image[] almasVisuales;
    public Image[] almasApagadasVisuales;

    public Image[] monedasVisaules;
    public Slider barraDeVida;

    public TextMeshProUGUI nombreJefe;
    public TextMeshProUGUI countdownText;

    public Image[] vidasVisuales;
    public Image[] vidasApagadasVisuales;
    public Image[] PowerUp2;
    public Image opacidad;
    public Image countDown;


    public 
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        foreach (Image i in almasVisuales)
        {
            i.gameObject.SetActive(false);
        }
        foreach (Image i in monedasVisaules)
        {
            i.gameObject.SetActive(false);
        }
        foreach (Image i in vidasApagadasVisuales)
        {
            i.gameObject.SetActive(false);
        }
        barraDeVida.gameObject.SetActive(false);
        nombreJefe.gameObject.SetActive(false);
        opacidad.gameObject.SetActive(false);

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
    public void UpdateImagePower(int PowerUps)
    {
        PowerUp.sprite = ImagesPowers[PowerUps];
    }

    public void UpdateAlmas(int almas)
    {
        Debug.Log(almas + " almas" + "Voy a activar " + almas + " almasApagadas" + "Voy a desactivar " + (almasVisuales.Length-almas) + " almas");
        for (int i = 0; i < almas; i++)
        {
            almasVisuales[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < almasVisuales.Length-almas; i++)
        {
            almasApagadasVisuales[i].gameObject.SetActive(true);
        }

    }
    public void UpdateMonedas(int monedas)
    {
        Debug.Log(monedas   + " monedas");
        for (int i = 0; i < monedasVisaules.Length; i++)
        {
            monedasVisaules[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < monedas; i++)
        {
            monedasVisaules[i].gameObject.SetActive(true);
        }

    }
    public void UpdateVidas(float vidas)
    {
        Debug.Log(vidas + " vidas" + "Voy a activar " + vidas + " vidasApagadas" + "Voy a desactivar " + (vidasVisuales.Length - vidas) + " vidas");
        for (int i = 0; i < vidas; i++)
        {
            vidasVisuales[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < vidasVisuales.Length - vidas; i++)
        {
            vidasApagadasVisuales[i].gameObject.SetActive(true);
        }
    }
    public void UpdateBarraDeVida(float vida)
    {
        barraDeVida.value = vida;
    }
    public void showBossFightUI(string nombre)
    {
        barraDeVida.gameObject.SetActive(true);
        nombreJefe.gameObject.SetActive(true);
        opacidad.gameObject.SetActive(true);
        nombreJefe.text = nombre;
    }
    public void StartCountdown(bool a)
    {
        countDown.gameObject.SetActive(a);
    }
    public void UpdateCountdown(int seconds)
    {
        countdownText.text = seconds.ToString();
    }
}
