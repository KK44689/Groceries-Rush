using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    // counter text
    public Text CounterText;

    private int Count = 0;

    // types
    public string boxType;

    // audio source
    private AudioSource audioSource;

    public AudioClip correctSound;

    // particle
    public ParticleSystem collectGroceries;

    // spawn manager script
    private SpawnManager spawnManagerScript;

    private void Start()
    {
        Count = 0;
        spawnManagerScript =
            GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        audioSource = GameObject.Find("Boxes").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // if correct type go into box increase count
        if (other.gameObject.tag == boxType)
        {
            Count += 1;
            CounterText.text = boxType + "s Count : " + Count;
            audioSource.PlayOneShot (correctSound);
            collectGroceries.Play();
            Destroy(other.gameObject);
        }
        else
        {
            // if wrong groceries type go into box then game over
            Debug.Log("GameOver");

            spawnManagerScript.gameover = true;
        }
    }
}
