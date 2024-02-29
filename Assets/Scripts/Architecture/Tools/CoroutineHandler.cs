using System.Collections;
using UnityEngine;

namespace Architecture.Tools {
  public class CoroutineHandler : MonoBehaviour {
    private const string NAME = "[COROUTINE HANDLER]";
    private static CoroutineHandler _instance;

    private static CoroutineHandler Instance {
      get {
        return GetInstance();
      }
    }

    private static bool IsInitialized {
      get {
        return _instance != null;
      }
    }

    public static Coroutine StartRoutine(IEnumerator enumerator) {
      return Instance.StartCoroutine(enumerator);
    }

    public static void StopRoutine(IEnumerator routine) {
      if (routine != null) {
        Instance.StopCoroutine(routine);
      }
    }

    public static void StopRoutine(string routineName) {
      Instance.StopCoroutine(routineName);
    }

    private static CoroutineHandler GetInstance() {
      if (!IsInitialized) {
        _instance = CreateSingleton();
      }

      return _instance;
    }

    private static CoroutineHandler CreateSingleton() {
      var createdManager = new GameObject(NAME).AddComponent<CoroutineHandler>();
      createdManager.hideFlags = HideFlags.HideAndDontSave;
      DontDestroyOnLoad(createdManager.gameObject);
      return createdManager;
    }
  }
}