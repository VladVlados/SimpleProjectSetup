using UnityEngine;

namespace Architecture.CodeBase.UI {
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