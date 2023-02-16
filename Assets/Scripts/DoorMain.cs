using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DoorMain : MonoBehaviour, IInteract
{
    [SerializeField] private Animator door;
    public bool canOpen;
    private UiManager _ui;
    private Animator _anim;
    
    // Start is called before the first frame update
    void Start() {
        _ui = UiManager.Instance;
        _anim = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (!canOpen) {
            _ui.SetText("The door is locked");
            return;
        }
        _anim.Play("openRight");
        door.Play("openLeft");
    }
}
