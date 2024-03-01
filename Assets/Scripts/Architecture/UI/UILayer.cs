using UnityEngine;

namespace Architecture.UI {
  public enum UILayerType {
    Screen,
    FXUnderPopups,
    Popup,
    FXOverPopups
  }

  public class UILayer : UIElement {
    private void Start() {
      ApplySortingLayer();
    }

    private void ApplySortingLayer() {
      var canvas = GetComponent<Canvas>();
      canvas.sortingLayerName = _layer.ToString();
    }
  }
}