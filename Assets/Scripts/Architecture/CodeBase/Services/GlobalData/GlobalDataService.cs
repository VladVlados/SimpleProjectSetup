using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Architecture.CodeBase.Services.GlobalData {
  public class GlobalDataService : IGlobalDataService {
    private DiContainer _container;
    private Dictionary<Type, ItemGlobalData> _staticDataMap;

    public GlobalDataService(DiContainer container) {
      _container = container;
      Initialize();
    }

    public bool IsInitialized { get; private set; }

    public void Initialize() {
      _staticDataMap = Resources.LoadAll<ItemGlobalData>(Constants.Constants.Paths.GLOBAL_DATA).ToDictionary(x => x.GetType(), x => x);

      foreach (ItemGlobalData staticData in _staticDataMap.Values) {
        staticData.LoadEssential();
      }

      IsInitialized = true;
    }

    public T GetGlobalData<T>() where T : ItemGlobalData {
      return _staticDataMap[typeof(T)] as T;
    }

    public void Dispose() {
      _staticDataMap.Clear();
    }
  }
}