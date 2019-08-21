using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    [SerializeField]float rcsThrust = 200f;
    [SerializeField] float thrustPower = 15f;

    Rigidbody rigidbody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    private void OnCollisionEnter(Collision collision) 
        {
        
        switch (collision.gameObject.tag) 
        {
            case "Friendly":
                print("OK"); //TODO make som proper behaviour
                break;
            case "Fuel":
                print("GIVE ME FUEL GIVE ME FIRE");
                break;
            default:
                print("Dead");
                break;

        }
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        { //can thrust while rotating
            rigidbody.AddRelativeForce(Vector3.up * thrustPower);

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void Rotate()
    {

        rigidbody.freezeRotation = true;
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidbody.freezeRotation = false;
    }
}
