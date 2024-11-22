using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public TextMeshProUGUI Monedas;
    public TextMeshProUGUI Life;
    public TextMeshProUGUI Almas;
    public Image PowerUp;
    public Image[] ImagesPowers;
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
    public void UpdateImagePower(int PowerUps)
    {
        PowerUp = ImagesPowers[PowerUps];
    }
}
