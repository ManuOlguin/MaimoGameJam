using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] public Player player;

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
        UIManager.Instance.UpdateAlmas(3);
        Almas = 3;
        //PonerMeta
    }
    public IEnumerator ILose()
    {
        yield return new WaitForSeconds(1);
        UIManager.Instance.StartCountdown(true);
        if(Input.GetKeyDown(KeyCode.J))
        {
            UIManager.Instance.StartCountdown(false);
            SceneController.Instance.ReloadCurrentScene();
        }
        yield return new WaitForSeconds(10);
        SceneController.Instance.LoadScene("Intro");
    }
}
