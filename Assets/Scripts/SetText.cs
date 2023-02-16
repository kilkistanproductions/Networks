using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetText : MonoBehaviour, IInteract
{
    [SerializeField] private string text;
    private UiManager _ui;
    
    // Start is called before the first frame update
    private void Start() => _ui = UiManager.Instance;
    
    public void Interact() => _ui.SetText(text);
}
