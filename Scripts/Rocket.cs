using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float dThrust = 250f; //directional thrust

    [SerializeField] AudioClip mainEngine; //thruster sound
    [SerializeField] AudioClip deathSound;

    [SerializeField] ParticleSystem thrustParticles; //thruster particle System
    [SerializeField] ParticleSystem levelCompleteParticle; 
    [SerializeField] ParticleSystem deathExplosion;

    bool isTransitioning = false;
    bool canLand = false; //true to win, after collecting planet object

    Rigidbody rigidBody;
    AudioSource gameAudio;
    public GameManager gameManager;

    // Use this for initialization
    void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        gameAudio = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!isTransitioning)
        {
            Thrust();
            Rotate();
            CheckBounds();
            //print((transform.position.y.ToString("0"))); //print height to UI 
        }
        
	}

    void OnCollisionEnter (Collision collision)
    {
        if (isTransitioning) { return; }

        switch (collision.gameObject.tag)
        {
            case "Finish":
                //If canLand == true... advance
                if (canLand == true)
                { 
                    isTransitioning = true;
                    levelCompleteParticle.Play(); //level up confetti
                    gameManager.LevelUp();
                    print(Time.time.ToString("0")); //print seconds to UI
                }
                break;

            case "Friendly":
                {
                    return;
                }

            case "Planet":
                canLand = true; //Land on
                gameManager.ItemPickUp();
                Destroy (collision.gameObject); //Remove (Destroy) planet object
                //play educational infographic
                break;

            default: //death
                gameManager.Lost();
                gameAudio.Stop();
                gameAudio.mute = false;
                gameAudio.PlayOneShot(deathSound);
                isTransitioning = true;
                canLand = false;
                deathExplosion.Play(); //particle systems
                //rocket falling apart animation
                break;
        }
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)) //can thrust and turn
        {
            ApplyThrust();
        }
        else
        {
            StopApplyingThrust();
        }
    }

    private void StopApplyingThrust()
    {
        //gameAudio.Stop();
        gameAudio.mute = true;
        thrustParticles.Stop();
        return;
    }

    private void ApplyThrust()
    {
        float thrustThisFrame = mainThrust * Time.deltaTime;
        rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
        if (!gameAudio.isPlaying) //so it doesn't layer
        {
            gameAudio.PlayOneShot(mainEngine);

        }
        else
        {
            gameAudio.mute = false;
            thrustParticles.Play();
        }
    }

    private void Rotate()
    {
        rigidBody.angularVelocity = Vector3.zero; //remove rotation due to physics
        float rotationThisFrame = dThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)) //tilt left
        {
            transform.Rotate (Vector3.forward * rotationThisFrame);
        }

        else if (Input.GetKey(KeyCode.D)) //tilt right
        { 
            transform.Rotate (-Vector3.forward * rotationThisFrame);
        }
    }


    private void CheckBounds()
    {
        if (gameObject.transform.position.y < 0)
        {
            //Print to user, you have left the mission, please try again
            gameManager.Lost();
        }
    }


}
