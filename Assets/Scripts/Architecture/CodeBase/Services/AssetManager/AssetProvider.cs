using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Architecture.CodeBase.Services.AssetManager {
  public class AssetProvider : IAsset {
    private readonly IAssetPath _assetPath;

    public AssetProvider(IAssetPath assetPath) {
      _assetPath = assetPath;
      Initialize();
    }

    public void Initialize() {
      IsInitialized = true;
    }

    public void Dispose() { }

    public bool IsInitialized { get; private set; }

    public Task<GameObject> Load<T>() {
      throw new NotImplementedException();
    }

    public Task<T> Load<T>(string address) where T : class {
      throw new NotImplementedException();
    }
  }
}