using System;
using System.Collections;
using Architecture.Audio;
using Architecture.Scene;
using Architecture.Settings.Global;
using Architecture.Tools;
using UnityEngine;

namespace Architecture.GameCore {
  public enum ModuleLoadingProgress {
    GlobalSettingsInitialized,
    SaveDataInitialized,
    AudioManagerInitialized,
    SceneManagerInitialized
  }

  public class Game {
    public static ScenesManager ScenesManager { get; private set; }
    public static GlobalSettings Settings { get; private set; }
    public static AudioManager AudioManager { get; private set; }
    public static SavedData SavedData { get; private set; }
    public static bool Initialized { get; private set; }

    public static void Run() {
      CoroutineHandler.StartRoutine(RunGameRoutine());
    }

    private static IEnumerator RunGameRoutine() {
      InitSaveData();
      OnModuleLoadedEvent?.Invoke(ModuleLoadingProgress.SaveDataInitialized);
      yield return null;

      InitAudioManager();
      OnModuleLoadedEvent?.Invoke(ModuleLoadingProgress.AudioManagerInitialized);
      yield return null;

      InitGlobalSettings();
      OnModuleLoadedEvent?.Invoke(ModuleLoadingProgress.GlobalSettingsInitialized);
      yield return null;

      InitScenesManager();
      OnModuleLoadedEvent?.Invoke(ModuleLoadingProgress.SceneManagerInitialized);
      yield return null;

      OnGameInitializedEvent?.Invoke();
      Initialized = true;
    }

    private static void InitAudioManager() {
      AudioManager = new AudioManager();
    }

    private static void InitGlobalSettings() {
      const string globalSettingsPath = "Settings/Global/GlobalSettings";
      Settings = Resources.Load<GlobalSettings>(globalSettingsPath);
      Settings.InitValues();
    }

    private static void InitSaveData() {
      SavedData = new SavedData();
    }
    
    private static void InitScenesManager() {
      ScenesManager = new ScenesManager();
    }

    public static event Action OnGameInitializedEvent;
    public static event Action<ModuleLoadingProgress> OnModuleLoadedEvent;
  }
}