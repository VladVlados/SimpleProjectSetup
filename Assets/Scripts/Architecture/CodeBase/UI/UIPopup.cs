using UnityEngine;
using UnityEngine.UI;

namespace Architecture.CodeBase.UI {
  public class UIPopup : UIElement, IUIPopup {
    [SerializeField]
    protected Button[] _buttonsClose;

    public Button[] ButtonsClose {
      get {
        return _buttonsClose;
      }
    }
  }
}