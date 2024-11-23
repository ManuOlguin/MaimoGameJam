using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpezarPelea : MonoBehaviour
{

    private int playerLayer;
    private int minibossLayer;
    private UIManager _uiManager;

    void Start()
    {
        // Get the layer indices for the player and miniboss layers
        playerLayer = LayerMask.NameToLayer("Player");
        minibossLayer = LayerMask.NameToLayer("MiniBoss");
        _uiManager = UIManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerLayer || other.gameObject.layer == minibossLayer)
        {
            _uiManager.showBossFightUI("Amonite the destroyer");
        }
    }
}