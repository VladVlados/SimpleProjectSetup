using System.Threading.Tasks;
using UnityEngine;

namespace Architecture.CodeBase.Services.AssetManager {
  public interface IAsset : IInitializedService {
    Task<GameObject> Load<T>();

    Task<T> Load<T>(string address) where T : class;
  }
}