using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour,IInteract
{
    [SerializeField] private DoorMain door;
    private UiManager _ui;

    private void Start() => _ui = UiManager.Instance;
    
    public void Interact()
    {
        _ui.SetText("You took the key");
        door.canOpen = true;
        Destroy(gameObject);
    }
}
