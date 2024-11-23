using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Gpelo : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab; 
    [SerializeField] private Transform shootPoint; 
    [SerializeField] private float shootCooldown = 2f;
    public Player player;
    [SerializeField] int bulletLayer;
    [SerializeField] Animator zombieController;
    Rigidbody rb;
    [Header("Stats")]
    public float speed = 3f;
    public float detectionRange = 10f;
    public float distanceToHit = 0.5f;
    public float life = 5;
    public bool isDead = false;
    float distanceToPlayer;

    private NavMeshAgent navMeshAgent;
}
