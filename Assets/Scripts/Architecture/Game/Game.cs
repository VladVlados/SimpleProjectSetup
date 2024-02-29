using System;
using System.Collections;
using Architecture.Tools;

namespace Architecture.Game {
  public enum ModuleLoadingProgress {
    AudioManagerInitialized
  }

  public class Game {
    public static AudioManager AudioManager { get; private set; }
    public static SavedData SavedData { get; private set; }
    public static bool Initialized { get; private set; }

    public static void Run() {
      CoroutineHandler.StartRoutine(RunGameRoutine());
    }

    private static IEnumerator RunGameRoutine() {
      InitAudioManager();
      OnModuleLoadedEvent?.Invoke(ModuleLoadingProgress.AudioManagerInitialized);
      yield return null;

      OnGameInitializedEvent?.Invoke();
      Initialized = true;
    }

    private static void InitAudioManager() {
      AudioManager = new AudioManager();
    }

    public static event Action OnGameInitializedEvent;
    public static event Action<ModuleLoadingProgress> OnModuleLoadedEvent;
  }
}