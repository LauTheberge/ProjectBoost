using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustForce = 800f;
    [SerializeField] float rotationForce = 100f;
    [SerializeField] AudioClip mainThrust;


    [SerializeField] ParticleSystem boostParticle;
    [SerializeField] ParticleSystem leftThrustParticle;
    [SerializeField] ParticleSystem rightThrustParticle;

    Rigidbody rbody;
    AudioSource audioSource;
  

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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();

        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rbody.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rbody.freezeRotation = false;
    }

    private void StartThrusting()
    {
        rbody.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainThrust);
        }
        if (!boostParticle.isPlaying)
        {
            boostParticle.Play();
        }
    }
    private void StopThrusting()
    {
        audioSource.Stop();
        boostParticle.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationForce);
        if (!leftThrustParticle.isPlaying)
        {
            leftThrustParticle.Play();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationForce);
        if (!rightThrustParticle.isPlaying)
        {
            rightThrustParticle.Play();
        }
    }

    private void StopRotating()
    {
        leftThrustParticle.Stop();
        rightThrustParticle.Stop();
    }
}
