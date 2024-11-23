using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bajadorParedes : MonoBehaviour
{
    public GameObject[] objectsToMove; // Array of objects to move
    public float moveDistance = 1.0f; // Distance to move the objects
    private int playerLayer; // Layer index for the player

    void Start()
    {
        // Get the layer index for the player layer
        playerLayer = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            foreach (GameObject obj in objectsToMove)
            {
                Debug.Log("Moving object" + obj.name + " down" + moveDistance);
                obj.transform.position -= new Vector3(0, moveDistance, 0);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            foreach (GameObject obj in objectsToMove)
            {
                Debug.Log("Moving object" + obj.name + " down" + moveDistance);
                obj.transform.position += new Vector3(0, moveDistance, 0);
            }
        }
    }
}