using UnityEngine;

namespace Architecture.Settings.Global {
  public abstract class SettingsComponent : ScriptableObject {
    public abstract void InitializeSettings();
  }
}