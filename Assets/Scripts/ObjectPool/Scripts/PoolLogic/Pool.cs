using System;
using System.Collections;
using System.Collections.Generic;
using Architecture.Scene;
using Architecture.Tools;
using UnityEngine;
using CoroutineHandler = Architecture.CodeBase.Services.CoroutineHandler.CoroutineHandler;
using Object = UnityEngine.Object;

namespace ObjectPool.Scripts.PoolLogic {
  public sealed class Pool : SceneDataStorage{
    private readonly Dictionary<Type, ObjectPool<IPoolable>> _poolMap = new Dictionary<Type, ObjectPool<IPoolable>>();
    public override void Initialize() {
      //CoroutineHandler.StartRoutine(LoadPrefabs());
    }

    public T Get<T>() where T : IPoolable {
      Type type = typeof(T);
      return (T) _poolMap[type].Get();
    }

    public void Return<T>(T item) where T : IPoolable {
      Type type = typeof(T);
      if (_poolMap.ContainsKey(type) == false) {
        return;
      }
      _poolMap[type].Return(item);
    }

    private IEnumerator LoadPrefabs() {
      const string path = "Poolable";
      MonoBehaviour[] prefabs = Resources.LoadAll<MonoBehaviour>(path);

      for (var i = 0; i < prefabs.Length; i++) {
        CreatePool(prefabs[i]);
        yield return null;
      }

      Resources.UnloadUnusedAssets();
      PoolCompleted = true;
    }

    private void CreatePool<T>(T item) where T : MonoBehaviour {
      var pool = new ObjectPool<IPoolable>(() => (IPoolable) Object.Instantiate(item));
      _poolMap.Add(item.GetType(), pool);
    }

    public bool PoolCompleted { get; private set; }
  }
}