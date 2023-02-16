using System;

[Serializable]
public class ScreenShotHandler : ViewOperator<ScreenShotView>
{
    public event Action PointerDownEvent;
    public void Initialize(){
        base.CreateView();
        view.Open();

        view.PointerDownEvent += PointerDown;
    }

    private void PointerDown() {
        PointerDownEvent?.Invoke();
    }
    
}
