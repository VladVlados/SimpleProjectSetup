using System;
using System.Collections.Generic;
using System.Linq;
using Architecture.CodeBase.UI;
using Attributes.GameObjectOfType;
using Attributes.SceneNameAttribute;
using UnityEngine;

namespace Architecture.CodeBase.Services.SceneLoader {
  [CreateAssetMenu(fileName = "SceneConfig", menuName = "Architecture/Scenes/New SceneConfig")]
  public class SceneConfig : ScriptableObject {
    [SerializeField, SceneName]
    private string _sceneName;
    [Header("======= UI STRUCTURE ======="), Space(20), SerializeField, GameObjectOfType(typeof(UIElement))]
    private List<GameObject> _uiPrefabs;

    public string SceneName {
      get {
        return _sceneName;
      }
    }

    public UIElement[] UIPrefabs {
      get {
        return GetUIPrefabs();
      }
    }

    public UIElement[] GetUIPrefabs() {
      var uiPrefabs = new List<UIElement>();
      foreach (GameObject goPrefab in _uiPrefabs) {
        var uiPrefab = goPrefab.GetComponent<UIElement>();
        uiPrefabs.Add(uiPrefab);
      }

      return uiPrefabs.ToArray();
    }

    public UIElement GetPrefab(Type type) {
      UIElement[] allPrefab = UIPrefabs;
      return allPrefab.First(pref => pref.GetType() == type);
    }
  }
}