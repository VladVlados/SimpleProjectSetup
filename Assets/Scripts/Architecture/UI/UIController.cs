using UnityEngine;

namespace Architecture.UI {
  public class UIController : MonoBehaviour {
    private void Awake() {
      DontDestroyOnLoad(gameObject);
    }
    public void Clear() { }
    public void BuildUI() { }
  }
}