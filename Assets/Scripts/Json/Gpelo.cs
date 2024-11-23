using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Gpelo : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab; 
    [SerializeField] private Transform shootPoint; 
    [SerializeField] private float shootCooldown = 2f;
}
