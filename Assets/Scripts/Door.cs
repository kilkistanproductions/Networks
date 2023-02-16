using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteract
{
    [SerializeField] private bool isRight;
    [SerializeField] private bool canOpen;
    [SerializeField] private bool isOpen;
    private Animator _anim;
    private UiManager _ui;

    private void Start() {
        _ui = UiManager.Instance;
        _anim = gameObject.GetComponent<Animator>();
    }

    public void Interact() {
        if (canOpen)
        {
            if (isRight)
                _anim.Play(!isOpen ? "openRight" : "closeRight");
            else
                _anim.Play(!isOpen ? "openLeft" : "closeLeft");
            isOpen = !isOpen;
        }
        else
            _ui.SetText("You don't need to go there");
    }
 
}
