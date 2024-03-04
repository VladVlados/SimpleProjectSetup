using System;
using System.Collections.Generic;
using System.Linq;
using Architecture.Scene;
using UnityEngine;

namespace Architecture.UI {
  public class UIController : MonoBehaviour {
    [SerializeField]
    private UILayer[] _layers;
    private Dictionary<string, UIElement> _createdUIElementsMap;
    private Dictionary<string, UIElement> _popupsMap;
    private SceneConfig _sceneConfig;

    private void Awake() {
      _createdUIElementsMap = new Dictionary<string, UIElement>();
      _popupsMap = new Dictionary<string, UIElement>();
      DontDestroyOnLoad(gameObject);
    }

    public void Clear() {
      if (_createdUIElementsMap == null) {
        return;
      }

      UIElement[] allCreatedUIElements = _createdUIElementsMap.Values.ToArray();
      foreach (UIElement uiElement in allCreatedUIElements) {
        Destroy(uiElement.gameObject);
      }
      
      _createdUIElementsMap.Clear();
      _popupsMap.Clear();
    }

    public void BuildUI(SceneConfig sceneConfig) {
      _sceneConfig = sceneConfig;

      UIElement[] prefabs = _sceneConfig.GetUIPrefabs();
      foreach (UIElement uiElementPref in prefabs) {
        if (uiElementPref is UIScreen uiScreenPref && uiScreenPref.Layer == UILayerType.Screen) {
          CreateScreen(uiScreenPref);
        } else if (uiElementPref is UIPopup uiPopup && uiElementPref.Layer == UILayerType.Popup) {
          CreatePopup(uiPopup);
        }
      }
    }

    private void CreateScreen(UIScreen uiScreenPref) {
      Transform container = GetContainer(uiScreenPref.Layer);
      UIScreen createdUIScreen = Instantiate(uiScreenPref, container);
      createdUIScreen.name = uiScreenPref.name;
      _createdUIElementsMap[createdUIScreen.name] = createdUIScreen;
      if (uiScreenPref.ShowByDefault) {
        createdUIScreen.Show();
      }
    }

    private void CreatePopup(UIPopup popupPref) {
      Transform container = GetContainer(popupPref.Layer);
      UIPopup createdPopup = Instantiate(popupPref, container);
      createdPopup.name = popupPref.name;
      _popupsMap[createdPopup.name] = createdPopup;
      _createdUIElementsMap[createdPopup.name] = createdPopup;
      popupPref.HideInstantly();
    }

    private Transform GetContainer(UILayerType layer) {
      return _layers.First(layerObject => layerObject.Layer == layer).transform;
    }
  }
}