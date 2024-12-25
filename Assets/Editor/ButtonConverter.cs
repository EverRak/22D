using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonPropertyCopier : EditorWindow
{
    private List<Button> sourceButtons = new List<Button>();
    private GameObject prefab;
    private Transform parentObject;

    [MenuItem("Tools/Button Property Copier")]
    public static void ShowWindow()
    {
        GetWindow<ButtonPropertyCopier>("Button Property Copier");
    }

    private void OnGUI()
    {
        GUILayout.Label("Button Property Copier", EditorStyles.boldLabel);

        // Display fields for prefab and parent object
        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);
        parentObject = (Transform)EditorGUILayout.ObjectField("Parent Object", parentObject, typeof(Transform), true);

        // Display a list of buttons
        EditorGUILayout.LabelField("Source Buttons", EditorStyles.boldLabel);
        for (int i = 0; i < sourceButtons.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            sourceButtons[i] = (Button)EditorGUILayout.ObjectField(sourceButtons[i], typeof(Button), true);
            if (GUILayout.Button("Remove", GUILayout.Width(60)))
            {
                sourceButtons.RemoveAt(i);
                i--;
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add Button"))
        {
            sourceButtons.Add(null);
        }

        // Perform the copying and instantiating
        if (GUILayout.Button("Copy Properties and Instantiate"))
        {
            CopyPropertiesAndInstantiate();
        }
    }

    private void CopyPropertiesAndInstantiate()
    {
        if (prefab == null || sourceButtons.Count == 0 || parentObject == null)
        {
            Debug.LogError("Please assign all required fields.");
            return;
        }

        foreach (Button sourceButton in sourceButtons)
        {
            if (sourceButton == null) continue;

            // Create a prefab instance
            GameObject newButtonObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab, parentObject);

            if (newButtonObject == null)
            {
                Debug.LogError($"Failed to instantiate prefab {prefab.name}");
                continue;
            }

            // Set position to match source button
            newButtonObject.transform.position = sourceButton.transform.position;
            newButtonObject.GetComponent<RectTransform>().sizeDelta = sourceButton.GetComponent<RectTransform>().sizeDelta;

            // Retrieve the Button component
            Button newButton = newButtonObject.GetComponent<Button>();
            if (newButton == null)
            {
                Debug.LogError($"Prefab {prefab.name} does not contain a Button component.");
                DestroyImmediate(newButtonObject);
                continue;
            }

            newButton.name = sourceButton.name;

            // Copy onClick listeners
            newButton.onClick = sourceButton.onClick;

            // Copy colors
            var sourceColors = sourceButton.colors;
            var newColors = newButton.colors;
            newColors.normalColor = sourceColors.normalColor;
            newColors.highlightedColor = sourceColors.highlightedColor;
            newColors.pressedColor = sourceColors.pressedColor;
            newColors.selectedColor = sourceColors.selectedColor;
            newColors.disabledColor = sourceColors.disabledColor;
            newColors.colorMultiplier = sourceColors.colorMultiplier;
            newColors.fadeDuration = sourceColors.fadeDuration;
            newButton.colors = newColors;

            Debug.Log($"Prefab instance created: {newButtonObject.name}");
        }
    }
}
