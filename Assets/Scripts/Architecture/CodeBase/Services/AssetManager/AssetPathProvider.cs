namespace Architecture.CodeBase.Services.AssetManager {
  public class AssetPathProvider : IAssetPath {
    public string FormPath<T>() {
      return typeof(T).Name;
    }
  }
}