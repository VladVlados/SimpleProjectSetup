using System;
using UnityEngine;

namespace Attributes.ClassReferenceAttribute {
    [Serializable]
    public class ClassReferenceAttribute : PropertyAttribute {
        public Type type;

        public ClassReferenceAttribute(Type type) {
            this.type = type;
        }
    }
}
