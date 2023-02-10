using System;
using UnityEngine;

[Serializable]
public class Streamer
{
  [SerializeField] private StreamerView streamerView;
  [SerializeField] private StreamerModel streamerModel;


  public void Initialize()
  {
      streamerView.CallUpEvent += streamerModel.CallUp;
      streamerView.HangUpEvent += streamerModel.HangUp;
      streamerModel.OnUpdateReceiveTextureEvent += texture => streamerView.RemoteVideoTexture = texture;
      streamerModel.OnUpdateLocalTextureEvent += texture => streamerView.LocalVideoTexture = texture;
  }

  public void Disable()
  {
      streamerView.CallUpEvent -= streamerModel.CallUp;
      streamerView.HangUpEvent -= streamerModel.HangUp;
  }
  
}
