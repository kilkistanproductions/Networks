using System.Collections;
using UnityEngine;

public class Blessing : MonoBehaviour, IInteract
{
    private UiManager _ui;
    private GameManager _gameManager;
    
    // Start is called before the first frame update
    void Start() {
        _ui = UiManager.Instance;
        _gameManager = GameManager.Instance;
    }

    public void Interact() => StartCoroutine(nameof(God));

    private IEnumerator God()
    {
        _gameManager.ChangeState("Dialogue");
        _ui.SetText("Take my blessing, peasant");
        yield return new WaitForSeconds(3);
        _ui.SetText("You may have a chance to pass diktia");
        yield return new WaitForSeconds(3);
        _ui.SetText("Now, begone");
        yield return new WaitForSeconds(3);
        Application.Quit();
    }
}
