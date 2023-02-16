using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour, IInteract
{
    private AudioManager _audioManager;
    private AudioSource _audio;
    private UiManager _ui;

    private void Start() {
        _ui = UiManager.Instance;
        _audio = GetComponent<AudioSource>();
        _audioManager = AudioManager.Instance;
    }

    public void Interact() => StartCoroutine(_ui.FadeOut());

    public void PlayAudio(bool floor) {
        var clip = floor ? _audioManager.firstFloor : _audioManager.secondFloor;

        _audio.clip = clip;
        _audio.Play();
    }
}
