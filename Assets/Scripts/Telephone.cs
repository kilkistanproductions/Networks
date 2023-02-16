using System;
using System.Collections;
using UnityEngine;

public class Telephone : MonoBehaviour, IInteract
{
    private AudioSource _audio;
    private AudioManager _manager;

    private void Awake()
    {
        _audio = gameObject.GetComponent<AudioSource>();
        _manager = AudioManager.Instance;
        PlayAudio(_manager.ringtone);
    }

    private void PlayAudio(AudioClip clip)
    {
        if (_audio.isPlaying)
            return;

        _audio.Stop();
        StartCoroutine(nameof(Timer));
        _audio.clip = clip;
        _audio.Play();
    }
    

    public void Interact()
    {
        _audio.loop = false;
        PlayAudio(_manager.nums);
    }

    private IEnumerator Timer() {
        yield return new WaitForSeconds(0.3f);
    }
}
