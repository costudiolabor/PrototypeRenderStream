using UnityEngine;

public class Entry : MonoBehaviour
{

    [SerializeField] private View viewPrefab;
    [SerializeField] private View view;

    [SerializeField] private UiController uiController;
    [SerializeField] private DrawerFigure drawerFigure;


    private InputManager _inputManager;

    private void Awake()
    {
        view = Instantiate(viewPrefab);
        
        _inputManager = new InputManager();
        _inputManager.Init();
        _inputManager.OnEnable();
        
        drawerFigure = new DrawerFigure();
        drawerFigure.Init(Camera.main);

        uiController = new UiController(view);
        uiController.ClickButtonDeleteEvent += drawerFigure.DeleteFigure;
        _inputManager.StartTouchEvent += drawerFigure.StartTouch;
        _inputManager.CancelTouchEvent += drawerFigure.CancelTouch;
        _inputManager.DragTouchEvent += drawerFigure.Drag;
    }

    private void OnDisable()
    {
        _inputManager.OnDisable();
    }
    
}