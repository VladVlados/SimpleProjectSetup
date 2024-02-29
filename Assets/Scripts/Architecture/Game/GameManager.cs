using System;
using UnityEngine;

namespace Architecture.Game {
  public class GameManager : MonoBehaviour {
    private void Awake() {
      DontDestroyOnLoad(gameObject);
    }

    private void Update() {
      OnApplicationUpdateEvent?.Invoke();
    }

    private void OnApplicationPause(bool pauseStatus) { }
    private void OnApplicationQuit() { }

    public static event Action OnApplicationUpdateEvent;
  }
}