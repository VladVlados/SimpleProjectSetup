using System.Collections.Generic;

namespace Pause {
  public sealed class GamePause : IPauseHandler {
    private readonly List<IPauseHandler> _pauseHandlers = new();

    public void SetPaused() {
      IsPaused = true;
      foreach (IPauseHandler pauseHandler in _pauseHandlers) {
        pauseHandler.SetPaused();
      }
    }

    public void Resume() {
      IsPaused = false;
      foreach (IPauseHandler pauseHandler in _pauseHandlers) {
        pauseHandler.Resume();
      }
    }

    public void Register(IPauseHandler handler) {
      if (_pauseHandlers.Contains(handler)) {
        return;
      }

      _pauseHandlers.Add(handler);
    }

    public void Unregister(IPauseHandler handler) {
      _pauseHandlers.Remove(handler);
    }

    public bool IsPaused { get; private set; }
  }
}