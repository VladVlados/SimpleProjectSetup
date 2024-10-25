using System;
using UnityEngine;

namespace Attributes.GameObjectOfType {
  public class GameObjectOfTypeAttribute : PropertyAttribute {
    public GameObjectOfTypeAttribute(Type type) {
      Type = type;
    }

    public Type Type { get; }
  }
}