namespace Architecture.CodeBase.Services.GlobalData {
  public interface IGlobalDataService : IInitializedService {
    T GetStaticData<T>() where T : ItemGlobalData;
  }
}