using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Architecture.CodeBase.Services.Factory {
  public interface IGameFactory : IService {
    T Create<T>() where T : FactoryPrefab;

    Task<T> CreateAsync<T>();

    public Task<IList<T>> LoadAssetGroup<T>(string label) where T : class;

    Task<T> CreateAsync<T>(Vector3 at, Quaternion rotation) where T : FactoryPrefab;

    Task<T> CreateAsync<T>(RuleTile.TilingRuleOutput.Transform parent) where T : FactoryPrefab;

    Task<T> CreateAsync<T>(string assetPath) where T : class;

    Task<T> CreateAddressableObjectAsync<T>(string assetPath, Vector3 at, Quaternion rotation) where T : FactoryPrefab;

    Task<T> CreateProgressRegisteredAsync<T>() where T : FactoryPrefab;

    Task<T> CreateProgressRegisteredAsync<T>(Vector3 at, Quaternion rotation) where T : FactoryPrefab;

    void WarmUp();

    void Cleanup();
  }
}