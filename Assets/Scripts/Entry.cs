using UnityEngine;

public class Entry : MonoBehaviour
{

    [SerializeField] private View _viewPrefab;
    [SerializeField] private View _view;

    [SerializeField] private UiController _uiController;
    [SerializeField] private DrawerFigure _drawerFigure;


    private InputManager _inputManager;

    private void Awake()
    {
        _inputManager = new InputManager();
        _inputManager.Init();
        _inputManager.OnEnable();

        _view = Instantiate(_viewPrefab);
        _uiController = new UiController(_view);
        _drawerFigure = new DrawerFigure(Camera.main);

        _uiController.ClickButtonDeleteEvent += _drawerFigure.DeleteFigure;
        
        _inputManager.StartTouchEvent += _drawerFigure.StartTouch;
        _inputManager.CancelTouchEvent += _drawerFigure.CancelTouch;
        _inputManager.DragTouchEvent += _drawerFigure.Drag;
    }

    private void OnDisable()
    {
        _inputManager.OnDisable();
    }
    
}