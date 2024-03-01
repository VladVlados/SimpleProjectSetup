using UnityEngine;

namespace Architecture.UI {
  public class UIElement : MonoBehaviour {
    [SerializeField]
    protected UILayerType _layer;

    public UILayerType Layer {
      get {
        return _layer;
      }
    }
  }
}