using UnityEngine;

namespace Architecture.CodeBase.UI {
  public class UIElement : MonoBehaviour {
    [SerializeField]
    protected UILayerType _layer;

    public UILayerType Layer {
      get {
        return _layer;
      }
    }

    public virtual void Show() {
      gameObject.SetActive(true);
    }

    public virtual void Hide() {
      gameObject.SetActive(false);
    }

    public void HideInstantly() {
      gameObject.SetActive(false);
    }
  }
}