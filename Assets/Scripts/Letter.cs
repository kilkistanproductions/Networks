using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour, IInteract
{
    private UiManager _ui;

    private void Start() => _ui = UiManager.Instance;

    public void Interact() => _ui.ReadText();
}
