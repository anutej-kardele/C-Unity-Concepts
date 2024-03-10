using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ToggleShadowLODObjects : EditorWindow
{
    private List<GameObject> lodObjects = new List<GameObject>();
    public int LOD_Index;
    public bool status;


    [MenuItem("CustomEditorScript/Toggle Shadow LOD Objects")]
    static void Init()
    {
        ToggleShadowLODObjects window = EditorWindow.GetWindow<ToggleShadowLODObjects>("Toggle Shadow LOD Objects");
        window.Show();
    }

    void OnGUI()
    {
        LOD_Index = EditorGUILayout.IntField("LOD_Index", LOD_Index);
        status = EditorGUILayout.Toggle("Status", status);

        if (GUILayout.Button("Find Objects and apply"))
        {
            lodObjects.Clear();

            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                LODGroup lodGroup = obj.GetComponent<LODGroup>();

                if (lodGroup != null)
                {
                    LOD[] lods = lodGroup.GetLODs();

                    if (lods.Length >= (LOD_Index + 1))
                    {
                        Renderer[] renderers = lods[LOD_Index].renderers;

                        for (int i = 0; i < renderers.Length; i++)
                        {
                            if (renderers[i] != null)
                            {
                                lodObjects.Add(renderers[i].gameObject);
                            }
                        }
                    }
                }
            }

            Debug.Log("Found " + lodObjects.Count + " objects with LODs.");

            foreach (GameObject obj in lodObjects)
            {
                Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in renderers)
                {
                    renderer.shadowCastingMode = status ? UnityEngine.Rendering.ShadowCastingMode.On : UnityEngine.Rendering.ShadowCastingMode.Off;
                }
            }
        }
    }
}
