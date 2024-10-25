using UnityEngine;
using Zenject;

namespace Architecture.CodeBase {
  public class GlobalContainer : MonoBehaviour {
    public static DiContainer Container { get; private set; }

    public static void SetContainer(DiContainer container) {
      Container = container;
    }
  }
}