using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    public float speed;
    public float rotationSpeed;
    float timer;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;

        movementDirection.Normalize();

        characterController.SimpleMove(movementDirection * magnitude);
        
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // if keypress space then shoot
        if (Input.GetKey(KeyCode.Space)) {
            Shoot();
        }

    } 

    void Shoot()
    {
        timer += Time.deltaTime;
        if (timer > 3) {
            GameObject clone = Instantiate(bullet, transform.forward, Quaternion.identity) as GameObject;
            Rigidbody r = clone.GetComponent<Rigidbody>() as Rigidbody;
            r.AddForce(transform.forward * 1000);
            timer = 0;
        }
    }

}
