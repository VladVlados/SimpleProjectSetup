using System;
using Architecture.GameCore;
using UnityEngine;

namespace Architecture.Utils.TimerUtils {
  public enum TimerType {
    UpdateTick,
    UpdateUnscaledTick,
    OneSecTick,
    OneSecUnscaledTick
  }
  public class TimeInvoker {
    public event Action<float> OnUpdateTimeTickEvent;
    public event Action<float> OnUpdateUnscaledTimeTickEvent;
    public event Action OnOneSyncedSecondTickEvent;
    public event Action OnOneSyncedSecondUnscaledTickEvent;

    private float _oneSecTimer;
    private float _oneSecUnscaledTimer;

    public TimeInvoker() {
      GameManager.OnApplicationUpdateEvent += Update;
    }

    ~TimeInvoker() {
      GameManager.OnApplicationUpdateEvent -= Update;
    }

    private void Update() {
      float deltaTime = Time.deltaTime;
      OnUpdateTimeTickEvent?.Invoke(deltaTime);
      _oneSecTimer += deltaTime;
      if (_oneSecTimer >= 1f) {
        _oneSecTimer -= 1;
        OnOneSyncedSecondTickEvent?.Invoke();
      }

      float unscaledDeltaTime = Time.unscaledDeltaTime;
      OnUpdateUnscaledTimeTickEvent?.Invoke(unscaledDeltaTime);
      _oneSecUnscaledTimer += unscaledDeltaTime;
      if (_oneSecUnscaledTimer >= 1f) {
        _oneSecUnscaledTimer -= 1;
        OnOneSyncedSecondUnscaledTickEvent?.Invoke();
      }
    }
  }
}