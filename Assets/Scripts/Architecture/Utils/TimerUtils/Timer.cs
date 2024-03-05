using System;
using Architecture.GameCore;
using UnityEngine;

namespace Architecture.Utils.TimerUtils {
  public class Timer : MonoBehaviour {
    public Timer(TimerType type) {
      Type = type;
    }

    public Timer(TimerType type, float seconds) {
      Type = type;
      SetTime(seconds);
    }

    public void Init(TimerType type, float seconds) {
      Type = type;
      SetTime(seconds);
    }
    
    public void Init(TimerType type) {
      Type = type;
    }

    public TimerType Type { get; private set; }
    public float SecondsRemain { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsPaused { get; private set; }

    private void Start() {
      if (IsActive) {
        return;
      }

      if (Math.Abs(SecondsRemain) < Mathf.Epsilon) {
        OnTimerFinishedEvent?.Invoke();
        return;
      }

      IsActive = true;
      IsPaused = false;
      SubscribeOnTimeInvokerEvents();
      OnTimerValueChangedEvent?.Invoke(SecondsRemain);
    }

    public void Start(float seconds) {
      if (IsActive) {
        return;
      }

      SetTime(seconds);
      Start();
    }

    public event Action<float> OnTimerValueChangedEvent;
    public event Action OnTimerFinishedEvent;

    public void ReduceTime(float value) {
      SecondsRemain -= value;
      CheckFinished();
    }

    public void Pause() {
      if (IsPaused || !IsActive) {
        return;
      }

      IsPaused = true;
      IsActive = false;
      UnsubscribeFromTimeInvokerEvents();
      OnTimerValueChangedEvent?.Invoke(SecondsRemain);
    }

    public void Resume() {
      if (!IsPaused || IsActive) {
        return;
      }

      IsPaused = false;
      IsActive = true;
      SubscribeOnTimeInvokerEvents();
      OnTimerValueChangedEvent?.Invoke(SecondsRemain);
    }

    public void Stop() {
      if (!IsActive) {
        return;
      }

      UnsubscribeFromTimeInvokerEvents();
      SecondsRemain = 0;
      IsActive = false;
      IsPaused = false;

      OnTimerValueChangedEvent?.Invoke(SecondsRemain);
      OnTimerFinishedEvent?.Invoke();
    }

    public void SetTime(float seconds) {
      SecondsRemain = seconds;
      OnTimerValueChangedEvent?.Invoke(SecondsRemain);
    }

    private void SubscribeOnTimeInvokerEvents() {
      switch (Type) {
        case TimerType.UpdateTick:
          Game.TimeInvoker.OnUpdateTimeTickEvent += OnTick;
          break;
        case TimerType.UpdateUnscaledTick:
          Game.TimeInvoker.OnUpdateUnscaledTimeTickEvent += OnTick;
          break;
        case TimerType.OneSecTick:
          Game.TimeInvoker.OnOneSyncedSecondTickEvent += OnSyncedSecondTick;
          break;
        case TimerType.OneSecUnscaledTick:
          Game.TimeInvoker.OnOneSyncedSecondUnscaledTickEvent += OnSyncedSecondTick;
          break;
        default:
          Debug.LogWarning($"{Type} is wrong");
          break;
      }
    }

    private void UnsubscribeFromTimeInvokerEvents() {
      switch (Type) {
        case TimerType.UpdateTick:
          Game.TimeInvoker.OnUpdateTimeTickEvent -= OnTick;
          break;
        case TimerType.UpdateUnscaledTick:
          Game.TimeInvoker.OnUpdateUnscaledTimeTickEvent -= OnTick;
          break;
        case TimerType.OneSecTick:
          Game.TimeInvoker.OnOneSyncedSecondTickEvent -= OnSyncedSecondTick;
          break;
        case TimerType.OneSecUnscaledTick:
          Game.TimeInvoker.OnOneSyncedSecondUnscaledTickEvent -= OnSyncedSecondTick;
          break;
        default:
          Debug.LogWarning($"{Type} is wrong");
          break;
      }
    }

    private void OnTick(float deltaTime) {
      SecondsRemain -= deltaTime;
      CheckFinished();
    }

    private void OnSyncedSecondTick() {
      SecondsRemain -= 1f;
      CheckFinished();
    }

    private void CheckFinished() {
      if (SecondsRemain <= 0) {
        Stop();
      } else {
        OnTimerValueChangedEvent?.Invoke(SecondsRemain);
      }
    }
  }
}