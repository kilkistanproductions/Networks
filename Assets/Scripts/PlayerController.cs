using UnityEngine;
using Cinemachine;
using UnityEngine.Serialization;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private Elevator elevator;
    [SerializeField] private GameObject plane;
    [SerializeField] private GameObject planei;
    [SerializeField] private Camera cameraMain;
    [SerializeField] private float spd;
    private CharacterController _controller;
    private AudioSource _audio;
    private Vector2 _lookPos;
    private Vector2 _motion;
    private float _raycastRange;
    private float _increment;
    private bool _canMove;
    private bool _level;
    private UiManager _ui;
    public bool canInspect;
    public Input input;

    private void Awake()
    {
        //Initialize input
        input = new Input();
        //Movement
        input.Player.Move.performed += ctx => _motion = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += ctx => _motion = Vector2.zero;
        //Disable mouse
        Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Set up default values
        spd = 2f;
        _increment = 3.5f;
        _raycastRange = 3;
        _level = false;
        _canMove = true;
        canInspect = true;
        _ui = UiManager.Instance;
        _controller = gameObject.GetComponent<CharacterController>();
        _audio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_canMove)
            Move();
        if (canInspect) 
            Raycast();
        if(input.UI.Ok.triggered)
            _ui.ReadText();
        if(input.UI.Exit.triggered)
            _ui.StopReading();
    }

    #region InputAction

    public void OnEnable() => input.Player.Enable();
    public void OnUiEnable() => input.UI.Enable();

    public void OnDisable() => input.Player.Disable();
    public void OnUiDisable() => input.UI.Disable();
    #endregion

    #region Movement
    private void Move()
    {
        var cameraTransform = cameraMain.transform;
        var forward = cameraTransform.forward;
        var right = cameraTransform.right;

        //We dont want to move on the y axis
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        //Apply movement
        var move = forward * _motion.y + right * _motion.x;
        _controller.SimpleMove(move * spd);
    }
    #endregion
    
    private void Raycast()
     {
         RaycastHit hit;
         var ray = cameraMain.ScreenPointToRay(UnityEngine.Input.mousePosition);
         var mask = LayerMask.GetMask("Inspect");
         if (Physics.Raycast(ray, out hit, _raycastRange, mask)) {
             var obj = hit.transform.gameObject;
             if(string.IsNullOrEmpty(obj.name))
                 return;
    
             _ui.ShowName(obj.name);
    
             if (input.Player.Interact.triggered && obj.GetComponent<IInteract>() != null)
                 obj.GetComponent<IInteract>().Interact();
         }else
             _ui.ClearName();
     }

    public void MoveLevel()
    {
        _canMove = false;
        var pos = Vector3.zero;
        pos.y = !_level ? _increment : -_increment;
        _level = !_level;
        plane.gameObject.SetActive(false);
        planei.gameObject.SetActive(false);
        _controller.Move(pos);
        _canMove = true;
        elevator.PlayAudio(_level);
    }

    public void SetCanInspect(bool canInspect) => this.canInspect = canInspect;

    public void ActivatePlane()
    {
        plane.SetActive(true);
        planei.SetActive(true);
    }
}
