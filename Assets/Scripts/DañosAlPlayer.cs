using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañosAlPlayer : MonoBehaviour
{
    [SerializeField] int damage = 10;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("aaaa");
        if (other == GameManager.Instance.player.gameObject)
        {
            Debug.Log("entro");
            GameManager.Instance.player.PlayerGetHit(damage);
        }
    }
}
