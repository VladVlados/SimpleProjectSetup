using System;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture.Settings.Global {
  [CreateAssetMenu(fileName = "GlobalSettings", menuName = "Architecture/Settings/GlobalSettings")]
  public class GlobalSettings : ScriptableObject {
    [SerializeField]
    private SettingsComponent[] _settingsComponents;

    private Dictionary<Type, SettingsComponent> _settingsMap;

    public void InitValues() {
      _settingsMap = new Dictionary<Type, SettingsComponent>();
      for (var i = 0; i < _settingsComponents.Length; i++) {
        SettingsComponent settingsComponent = _settingsComponents[i];
        Type type = settingsComponent.GetType();
        settingsComponent.InitializeSettings();
        _settingsMap.Add(type, settingsComponent);
      }
    }

    public T Values<T>() where T : SettingsComponent {
      Type type = typeof(T);
      return (T)_settingsMap[type];
    }
  }
}