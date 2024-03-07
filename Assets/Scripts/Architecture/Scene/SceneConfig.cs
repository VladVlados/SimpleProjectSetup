using System;
using System.Collections.Generic;
using System.Linq;
using Architecture.UI;
using Attributes.ClassReferenceAttribute;
using Attributes.GameObjectOfType;
using Attributes.SceneNameAttribute;
using UnityEngine;

namespace Architecture.Scene {
  [CreateAssetMenu(fileName = "SceneConfig", menuName = "Architecture/Scenes/New SceneConfig")]
  public class SceneConfig : ScriptableObject {
    [SerializeField, SceneName]
    private string _sceneName;
    [Header("======= CORE ARCHITECTURE ======="), SerializeField, ClassReference(typeof(SceneDataStorage))]
    private string[] _sceneDataStorages;
    [Header("======= UI STRUCTURE ======="), Space(20), SerializeField, GameObjectOfType(typeof(UIElement))]
    private List<GameObject> _uiPrefabs;

    private Dictionary<Type, SceneDataStorage> _componentsMap;

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

    public void CreateSceneDataStorage() {
      _componentsMap = CreateDataStorage<SceneDataStorage>(_sceneDataStorages);
    }

    public T GetComponent<T>() where T : SceneDataStorage {
      Type type = typeof(T);
      bool founded = _componentsMap.TryGetValue(type, out SceneDataStorage resultComponent);

      if (founded) {
        return (T)resultComponent;
      }

      return default;
    }

    public Dictionary<Type, T> CreateDataStorage<T>(string[] classReferences) where T : SceneDataStorage {
      var createdMap = new Dictionary<Type, T>();
      foreach (string reference in classReferences) {
        var type = Type.GetType(reference);
        object result = Activator.CreateInstance(type!);
        var resultComponent = (T)result;
        resultComponent.Initialize();
        createdMap[type] = resultComponent;
      }

      return createdMap;
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