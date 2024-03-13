using System.Threading.Tasks;
using UnityEngine;

namespace Architecture.CodeBase.Services.Factory {
  public interface IGameFactory : IService {
    T Create<T>() where T : FactoryPrefab;

    Task<T> CreateAsync<T>();

    Task<T> CreateAsync<T>(Vector3 at, Quaternion rotation) where T : FactoryPrefab;

    Task<T> CreateAsync<T>(Transform parent) where T : FactoryPrefab;
    
    void Init();

    void Cleanup();
  }
}