using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public TextMeshProUGUI Life;
    public TextMeshProUGUI Monedas;
    public TextMeshProUGUI Almas;
    public Image PowerUp;
    public Sprite[] ImagesPowers;
    public Image[] almasVisuales;
    public Image[] almasApagadasVisuales;

    public Image[] monedasVisaules;
    public Slider barraDeVida;

    public TextMeshProUGUI nombreJefe;

    public Image[] vidasVisuales;
    public Image[] vidasApagadasVisuales;
    public Image[] PowerUp2;
    public Image opacidad;


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
}
