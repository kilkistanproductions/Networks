using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anthem : MonoBehaviour
{
    private AudioSource _audio;

    private void Start() => _audio = GetComponent<AudioSource>();
    
    private void OnTriggerEnter(Collider other)
    {
        _audio.Play();
        GetComponent<BoxCollider>().enabled = !GetComponent<BoxCollider>().enabled;
    }
}
