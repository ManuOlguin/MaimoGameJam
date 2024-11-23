using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puerta : MonoBehaviour
{
    private bool isOpen = false;
    public float rotationSpeed = 1.0f; // Speed of the rotation
    public GameObject doora;

    public AudioSource doorSound;

    public bool isPuertaBossFight = false;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !isOpen)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        Debug.Log("Opening door");
        StartCoroutine(RotateDoor());
        if (isPuertaBossFight)
        {
        
            _uiManager.showBossFightUI("Dr. Bonebreaker");
        }
        isOpen = true;
    }

    private IEnumerator RotateDoor()
    {
        doorSound.Play();
        float targetAngle = transform.eulerAngles.y - 90;
        while (Mathf.Abs(transform.eulerAngles.y - targetAngle) > 0.01f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
            yield return null;
        }
    }

    private void EliminarTuki()
    {
        Destroy(doora);
    }
}