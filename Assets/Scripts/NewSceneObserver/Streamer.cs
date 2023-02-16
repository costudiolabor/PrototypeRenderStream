using System;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class Streamer : ViewOperator<StreamerView>
{
  [SerializeField] private StreamerModel prefabStreamerModel;
  private StreamerModel _streamerModel;
  
  public event Action CallUpEvent, HangUpEvent;
  public event Action<char> CharInputEvent;

  public void Initialize() {
      _streamerModel = Object.Instantiate(prefabStreamerModel);
      base.CreateView();
      view.Open();

      SubscribeEvent();
  }
  
  public void ViewOpen() {
      view.Open();
  }
  
  public void ViewClose() {
      view.ForceClose();
  }

  private void CallUp() {
      _streamerModel.CallUp();
      CallUpEvent?.Invoke();
     
  }
  
  private void HangUp() {
      _streamerModel.HangUp();
      HangUpEvent?.Invoke();
  }
  
  private void SubscribeEvent() {
      view.CallUpEvent += CallUp;
      view.HangUpEvent += HangUp;
      _streamerModel.CharInputEvent += context => CharInputEvent?.Invoke(context);
      _streamerModel.OnUpdateReceiveTextureEvent += texture => view.remoteVideoTexture = texture;
      _streamerModel.OnUpdateLocalTextureEvent += texture => view.localVideoTexture = texture;
  }
  
  public void Disable() {
      view.CallUpEvent -= _streamerModel.CallUp;
      view.HangUpEvent -= _streamerModel.HangUp;
  }
  
}
