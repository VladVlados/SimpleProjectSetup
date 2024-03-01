using System;
using UnityEngine;

namespace Architecture.Game {
  public class GameManager : MonoBehaviour {
    private bool _stayOnGamePause;

    private void Awake() {
      DontDestroyOnLoad(gameObject);
    }

    private void Update() {
      OnApplicationUpdateEvent?.Invoke();
    }

    private void OnApplicationFocus(bool hasFocus) {
      if (Game.Initialized == false) {
        return;
      }

      if (!hasFocus) {
        return;
      }

      if (_stayOnGamePause == false) {
        ResumeGame();
      }
    }

    private void OnApplicationPause(bool pauseStatus) {
      if (Game.Initialized == false) {
        return;
      }

      SetupGamePause(pauseStatus);
    }

    private void OnApplicationQuit() { }

    private void SetupGamePause(bool pauseStatus) { }
    private void ResumeGame() { }

    public static event Action OnApplicationUpdateEvent;
  }
}