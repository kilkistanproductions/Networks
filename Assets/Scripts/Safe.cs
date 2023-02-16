using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Safe : MonoBehaviour, IInteract
{
    [SerializeField]private GameObject key;
    private const string Code = "2326";
    private string _currentCode = "";
    private GameObject _player;
    private int _state = 0;
    private Animator _anim;
    private UiManager _ui;
    private bool _inserting;
    private bool _canOpen;

    private void Start()
    {
        _ui = UiManager.Instance;
        _anim = transform.GetChild(0).GetComponent<Animator>();
    }

    public void EnterCode(int code)
    {
        _currentCode += code;
        _state++;
        CheckCode();
    }

    private void CheckCode() {
        switch (_state) {
            case 0:
                _currentCode = string.Empty;
                _ui.SetButtonText("First digit");
                break;
            case 1:
                _ui.SetButtonText("Second digit");
                break;
            case 2:
                _ui.SetButtonText("Third digit");
                break;
            case 3:
                _ui.SetButtonText("Fourth digit");
                break;
            case 4:
                _ui.DisableCode();
                if (Code == _currentCode) {
                    _ui.SetText("Code is correct");
                    StartCoroutine(nameof(EraseText), true);
                }
                else {
                    _ui.SetText("Code is incorrect try again");
                    _inserting = false;
                    StartCoroutine(nameof(EraseText), false);
                }
                break;
        }
    }

    public void Interact() {
        if(_inserting)
            return;

        if (_canOpen) 
            return;
        
        _state = 0;
        _currentCode = String.Empty;
        _inserting = true;
        _ui.EnableCode();
        CheckCode();
    }

    private void Open() {
        key.SetActive(true);
        GetComponent<BoxCollider>().enabled = !GetComponent<BoxCollider>().enabled;
        _anim.Play("safeOpen");
    }

    private IEnumerator EraseText(bool open)
    {
        yield return new WaitForSeconds(3);
        _ui.SetText(string.Empty);
        if(open)
            Open();
    }
}
