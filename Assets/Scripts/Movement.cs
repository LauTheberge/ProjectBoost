using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rbody;
    AudioSource audioSource;
    float thrustForce = 800f;
    float rotationForce = 100f;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rbody.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else { audioSource.Stop(); }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationForce);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationForce);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rbody.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rbody.freezeRotation = false;
    }


}
