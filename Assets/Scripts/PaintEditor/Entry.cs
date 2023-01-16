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
        _inputManager = new InputManager();
        _inputManager.Init();
        _inputManager.OnEnable();

        view = Instantiate(viewPrefab);
        uiController = new UiController(view);
        drawerFigure = new DrawerFigure(Camera.main);

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