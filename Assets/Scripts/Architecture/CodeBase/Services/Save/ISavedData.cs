namespace Architecture.CodeBase.Services.Save {
  public interface ISavedData : IService {
    Data GetSaveData();
    void SaveGame();

  }
}