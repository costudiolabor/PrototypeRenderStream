using PaintEditor;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class Entry : MonoBehaviour
{

    [SerializeField] private ARRaycastManager rayCastManager;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private ViewBasic viewBasicPrefab;
    [SerializeField] private ViewBasic viewBasic;

    [SerializeField] private UiController uiController;
    [SerializeField] private DrawerFigure drawerFigure;


    private InputManager _inputManager;

    private void Awake()
    {
        viewBasic = Instantiate(viewBasicPrefab,canvas);
        
        
        _inputManager = new InputManager();
        _inputManager.Init();
        _inputManager.OnEnable();
        
        drawerFigure = new DrawerFigure();
        drawerFigure.Init(Camera.main, rayCastManager);

        uiController = new UiController(viewBasic);
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