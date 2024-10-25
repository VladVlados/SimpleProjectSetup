using System;

namespace Architecture.CodeBase.Services.Events {
  public interface IMonoEventService : IService {
    event EventHandler OnUpdate;
    event EventHandler OnFixedUpdate;
    event EventHandler OnLateUpdate;
    event Action<bool> OnAppFocus;
    event Action<bool> OnAppPause;
    event Action OnAppQuit;
  }
}