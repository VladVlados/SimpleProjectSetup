using System.Threading.Tasks;
using UnityEngine;

namespace Architecture.CodeBase.Services.AssetManager {
  public interface IAsset : IInitializedService {
    Task<GameObject> Load<T>(string address);
    Task<GameObject> Load<T>();
  }
}