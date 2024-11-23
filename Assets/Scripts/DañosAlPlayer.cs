using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañosAlPlayer : MonoBehaviour
{
    [SerializeField] int damage = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.Instance.player.gameObject)
        {
            Debug.Log(other);
            GameManager.Instance.player.PlayerGetHit(damage);
        }
    }
}
