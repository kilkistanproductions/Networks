using System;
using System.Collections;
using Json;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UiManager : MonoSingleton<UiManager>
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private JsonReader json;
    private GameObject _textBackground;
    private GameObject _firstSelected;
    private TextMeshProUGUI _codeText;
    private GameManager _gameManager;
    private PlayerController _player;
    private TextMeshProUGUI _paper;
    private TextMeshProUGUI _name;
    private TextMeshProUGUI _text;
    private GameObject _buttons;
    private bool _madeInHeaven;
    private RawImage _black;
    private int _page = 0;


    private void Start(){
        //Initialize var 
        _gameManager = GameManager.Instance;
        _player = GameObject.Find("Player").GetComponent<PlayerController>();
        _name = canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _black = canvas.transform.GetChild(2).GetComponent<RawImage>();
        _buttons = canvas.transform.GetChild(3).gameObject;
        _firstSelected = _buttons.transform.GetChild(0).gameObject;
        _codeText = canvas.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        _text = canvas.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
        _textBackground = canvas.transform.GetChild(6).gameObject;
        _paper = canvas.transform.GetChild(7).GetComponent<TextMeshProUGUI>();
    }
    
    private void Update()
    {
        if(_madeInHeaven && _player.input.Player.Heaven.triggered)
            Heaven();
    }

    public void ShowName(string objName) {
        _name.transform.gameObject.SetActive(true);
        _name.text = objName;
    }

    public void ClearName() => _name.transform.gameObject.SetActive(false);

    public void SetText(string t)
    {
        _text.text = t;
        StartCoroutine(SetText());
    }
    
    // This is just for those who don't want to use the coroutine
    public void Text(string t) => _text.text = t;


    private IEnumerator SetText() {
        yield return new WaitForSeconds(3);
        SetText(string.Empty);
    }

    #region Networks
    public IEnumerator FadeOut() {
        _player.SetCanInspect(false);
        var alpha = Color.black;
        for (float i = 0; i <= 1; i += Time.deltaTime){
            alpha.a = i;
            _black.color = alpha;
            yield return null;
        }

        StartCoroutine(Timer());
        yield return null;
    }

    private IEnumerator FadeIn() {
        var alpha = Color.black;
        for (float i = 1; i >= 0; i -= Time.deltaTime){
            alpha.a -= Time.deltaTime;
            _black.color = alpha;
            yield return null;
        }
        alpha.a = 0;
        _black.color = alpha;
        _player.SetCanInspect(true);
        yield return null;
    }

    private void Heaven()
    {
        _gameManager.LoadLevel();
    }

    private IEnumerator Timer()
    {
        _madeInHeaven = true;
        yield return new WaitForSeconds(3);
        _player.MoveLevel();
        _player.ActivatePlane();
        yield return new WaitForSeconds(2);
        StartCoroutine(FadeIn());
        _madeInHeaven = false;
    }
    #endregion

    #region Safe

    public void EnableCode() {
        _text.text = string.Empty;
        _buttons.SetActive(true);
        _codeText.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_firstSelected);
        _gameManager.ChangeState("Dialogue");
    }

    public void DisableCode() {
        _buttons.SetActive(false);
        _codeText.gameObject.SetActive(false);
        _gameManager.ChangeState("Normal");
    }

    public void SetButtonText(string t) => _codeText.text = t;
    #endregion
    
    #region Json

    public void ReadText()
    {
        var t = json.text.text;
        
        if (_page >= t.Length) {
            StopReading();
            return;
        }
            
        _gameManager.ChangeState("Page");
        _textBackground.SetActive(true);
        //Add text
        _paper.text = t[_page].page;
        _page++;
    }

    public void StopReading()
    {
        _page = 0;
        //Clear text
        _textBackground.SetActive(false);
        _paper.text = string.Empty;
        //Enable input
        _gameManager.ChangeState("Normal");
    }
    
    #endregion
    
}
