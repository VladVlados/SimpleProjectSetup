using Architecture.CodeBase.Services;

namespace Architecture.Save {
  public interface ISavedData : IService {
    Data GetSaveData();
    void SaveGame();

  }
}