using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private GameObject dontDestroy;
    private PlayerController _player;
    private UiManager _ui;

    private void Start() => _player = GameObject.Find("Player").GetComponent<PlayerController>();

    public void ChangeState(string state)
    {
        switch (state)
        {
            case "Normal":
                _player.OnEnable();
                _player.OnUiDisable();
                _player.canInspect = true;
                break;

            case "Dialogue":
                _player.OnDisable();
                _player.canInspect = false;
                break;
            
            case "Page":
                _player.OnDisable();
                _player.OnUiEnable();
                _player.canInspect = false;
                break;
                
            default:
                Debug.Log("State is not right");
                break;
        }
    }

    public void LoadLevel()
    {
        DontDestroyOnLoad(dontDestroy);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
