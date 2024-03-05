namespace ObjectPool.Scripts.PoolLogic {
  public interface IPoolable {
    void Get();
    void Return();
  }
}