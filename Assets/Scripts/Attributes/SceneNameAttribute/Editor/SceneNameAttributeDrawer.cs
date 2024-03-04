using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Attributes.SceneNameAttribute.Editor {
  [CustomPropertyDrawer(typeof(SceneNameAttribute))]
  public class SceneNameAttributeDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
      DrawPrefix(position, property);
      DrawPopup(position, property);
      DrawButtonForAddingScene(position);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
      return base.GetPropertyHeight(property, label) + EditorGUIUtility.singleLineHeight;
    }

    private void DrawPrefix(Rect position, SerializedProperty property) {
      EditorGUI.PrefixLabel(position, new GUIContent(property.displayName));
    }

    private void DrawPopup(Rect position, SerializedProperty property) {
      float width = Screen.width;
      position.x += width / 4;
      position.width -= width / 4;
      position.height = EditorGUIUtility.singleLineHeight;

      var sceneNames = new List<string>();
      foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
        string sceneName = Path.GetFileNameWithoutExtension(scene.path);
        sceneNames.Add(sceneName);
      }

      if (sceneNames.Count == 0) {
        return;
      }

      string sceneNameSelected = property.stringValue;
      int classNameSelectedIndex = sceneNames.IndexOf(sceneNameSelected);
      classNameSelectedIndex = Mathf.Clamp(classNameSelectedIndex, 0, sceneNames.Count - 1);
      classNameSelectedIndex = EditorGUI.Popup(position, classNameSelectedIndex, sceneNames.ToArray());

      property.stringValue = sceneNames[classNameSelectedIndex];
    }

    private void DrawButtonForAddingScene(Rect position) {
      position.width -= Screen.width / 6 * 5;
      position.x += Screen.width / 6 * 5;
      position.y += EditorGUIUtility.singleLineHeight + 5;
      position.height = EditorGUIUtility.singleLineHeight * 2;
      if (GUI.Button(position, "Add current")) {
        string currentScenePath = SceneManager.GetActiveScene().path;
        EditorBuildSettingsScene[] buildScenes = EditorBuildSettings.scenes;

        if (buildScenes.Any(item => item.path == currentScenePath)) {
          return;
        }

        var editorBuildSettingsScenes = new List<EditorBuildSettingsScene>(buildScenes);
        editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(currentScenePath, true));
        EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();
      }
    }
  }
}
