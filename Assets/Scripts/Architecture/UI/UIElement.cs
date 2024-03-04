using UnityEngine;

namespace Architecture.UI {
  public class UIElement : MonoBehaviour {
    [SerializeField]
    protected UILayerType _layer;
    

    public void Show() {
      gameObject.SetActive(true);
    }
    
    public void HideInstantly() {
      gameObject.SetActive(false);
    }

    public UILayerType Layer {
      get {
        return _layer;
      }
    }
  }
}