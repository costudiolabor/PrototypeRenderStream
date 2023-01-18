using PaintEditor;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class Entry : MonoBehaviour
{

    [SerializeField] private ARRaycastManager rayCastManager;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private View viewPrefab;
    [SerializeField] private View view;

    [SerializeField] private UiController uiController;
    [SerializeField] private DrawerFigure drawerFigure;


    private InputManager _inputManager;

    private void Awake()
    {
        view = Instantiate(viewPrefab,canvas);
        
        
        _inputManager = new InputManager();
        _inputManager.Init();
        _inputManager.OnEnable();
        
        drawerFigure = new DrawerFigure();
        drawerFigure.Init(Camera.main, rayCastManager);

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