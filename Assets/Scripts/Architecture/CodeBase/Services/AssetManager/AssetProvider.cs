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

    public async Task<GameObject> Load<T>() {
      return await Load<GameObject>(_assetPath.FormPath<T>());
    }
    
    public async Task<GameObject> Load<T>(string address) {
      return await Load<GameObject>(address);
    }
  }
}