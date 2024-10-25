using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Attributes.ClassReferenceAttribute;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ClassReferenceAttribute))]
public class ClassReferenceAttributeDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.PrefixLabel(position, new GUIContent(property.displayName));
        float width = Screen.width;
        position.x += width / 4;
        position.width -= width / 4;

        string classNameSelected = property.stringValue;
        var classAttribute = (ClassReferenceAttribute) attribute;
        Type type = classAttribute.type;

        Dictionary<string, string> typesMap = GetInheritedTypesMap(type);
        List<string> typeNames = typesMap.Keys.ToList();
        List<string> typeFullNames = typesMap.Values.ToList();
        int classNameSelectedIndex = typeFullNames.IndexOf(classNameSelected);

        classNameSelectedIndex = Mathf.Clamp(classNameSelectedIndex, 0, typesMap.Count - 1);
        classNameSelectedIndex = EditorGUI.Popup(position, classNameSelectedIndex, typeNames.ToArray());

        property.stringValue = typeFullNames[classNameSelectedIndex];
    }

    private Dictionary<string, string> GetInheritedTypesMap(Type baseType) {
        var sortedObjects = new SortedDictionary<string, string>();
        foreach (Type type in Assembly.GetAssembly(baseType).GetTypes().Where(myType => myType.IsAbstract == false && myType.IsClass && myType.IsSubclassOf(baseType))) {
            sortedObjects[type.Name] = type.FullName;
        }

        var objects = new Dictionary<string, string>();
        foreach (KeyValuePair<string, string> item in sortedObjects) {
            objects[item.Key] = item.Value;
        }

        return objects;
    }
}
