using UnityEngine;

namespace Architecture.UI {
  public class UIScreen : UIElement {
    [SerializeField]
    protected bool _showByDefault;

    public bool ShowByDefault {
      get {
        return _showByDefault;
      }
    }
  }
}