using UnityEditor;
using UnityEngine;

public class EnableDisableMeshRenderer : EditorWindow
{
    [MenuItem("CustomEditorScript/Enable-Disable Mesh Renderer")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(EnableDisableMeshRenderer));
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Enable Mesh Renderer"))
        {
            // Get the selected game object in the hierarchy
            GameObject selectedGameObject = Selection.activeGameObject;

            // Check if the selected object is a valid game object
            if (selectedGameObject != null)
            {
                // Get all the MeshRenderer components in the selected game object and its children
                MeshRenderer[] meshRenderers = selectedGameObject.GetComponentsInChildren<MeshRenderer>(true);

                // Enable all the MeshRenderer components
                foreach (MeshRenderer meshRenderer in meshRenderers)
                {
                    meshRenderer.enabled = true;
                }

                // Mark the selected game object as dirty to save changes
                EditorUtility.SetDirty(selectedGameObject);
            }
        }

        if (GUILayout.Button("Disable Mesh Renderer"))
        {
            // Get the selected game object in the hierarchy
            GameObject selectedGameObject = Selection.activeGameObject;

            // Check if the selected object is a valid game object
            if (selectedGameObject != null)
            {
                // Get all the MeshRenderer components in the selected game object and its children
                MeshRenderer[] meshRenderers = selectedGameObject.GetComponentsInChildren<MeshRenderer>(true);

                // Enable all the MeshRenderer components
                foreach (MeshRenderer meshRenderer in meshRenderers)
                {
                    meshRenderer.enabled = false;
                }

                // Mark the selected game object as dirty to save changes
                EditorUtility.SetDirty(selectedGameObject);
            }
        }
    }
}
