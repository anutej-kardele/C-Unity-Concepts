using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;
using static Codice.CM.WorkspaceServer.WorkspaceTreeDataStore;


public class SceneListWindow : EditorWindow
{
    private string sceneFilter = "";

    private bool listScene = false;
    string listSceneIcon = "d_VerticalLayoutGroup Icon";
    string displayName = "Search";
    int displayNameDistance = 45;

    [MenuItem("CustomEditorScript/Scene List")]
    private static void ShowWindow()
    {
        var window = GetWindow<SceneListWindow>();
        window.titleContent = new GUIContent("Scene List", EditorGUIUtility.IconContent("BuildSettings.SelectedIcon").image);
        window.Show();

    }

    private Vector2 scrollPosition;

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button(EditorGUIUtility.IconContent(listSceneIcon).image, GUILayout.MaxWidth(20), GUILayout.MaxHeight(20)))
        {
            listScene = !listScene;
            sceneFilter = (listScene) ? PlayerPrefs.GetString("SceneList") : "";
            listSceneIcon = (listScene) ? "d_Search Icon" : "d_VerticalLayoutGroup Icon";
            displayName = (!listScene) ? "Search" : "List";
            displayNameDistance = (!listScene) ? 45 : 25;
        }

        EditorGUILayout.LabelField(displayName, GUILayout.Width(displayNameDistance));

        sceneFilter = EditorGUILayout.TextField(sceneFilter);

        if (GUILayout.Button(EditorGUIUtility.IconContent("d_winbtn_win_close").image, GUILayout.MaxWidth(20), GUILayout.MaxHeight(20)))
        {
            sceneFilter = "";
        }

        EditorGUILayout.EndHorizontal();


        string[] scenes = Directory.GetFiles(Application.dataPath, "*.unity", SearchOption.AllDirectories);
        string[] scripts = Directory.GetFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories);

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        foreach (string scene in scenes)
        {
            string sceneName = Path.GetFileNameWithoutExtension(scene);

            if (sceneFilter != "" && listScene) PlayerPrefs.SetString("SceneList", sceneFilter);

            if (sceneFilter == "")
            {
                GUIContent content = new GUIContent(sceneName, EditorGUIUtility.IconContent("BuildSettings.SelectedIcon").image);
                if (GUILayout.Button(content))
                {
                    EditorSceneManager.OpenScene(scene);
                }
            }
            else if (listScene)
            {
                if (sceneFilter.ToLower().Contains(sceneName.ToLower()))
                {
                    GUIContent content = new GUIContent(sceneName, EditorGUIUtility.IconContent("BuildSettings.SelectedIcon").image);
                    if (GUILayout.Button(content))
                    {
                        EditorSceneManager.OpenScene(scene);
                    }
                }
            }
            else if (!listScene)
            {
                if (sceneName.ToLower().Contains(sceneFilter.ToLower()))
                {
                    GUIContent content = new GUIContent(sceneName, EditorGUIUtility.IconContent("BuildSettings.SelectedIcon").image);
                    if (GUILayout.Button(content))
                    {
                        EditorSceneManager.OpenScene(scene);
                    }
                }
            }
        }

        //foreach (string script in scripts)
        //{
        //    string scriptName = Path.GetFileNameWithoutExtension(script);
        //    GUIContent content = new GUIContent(scriptName, EditorGUIUtility.IconContent("cs Script Icon").image);
        //    GUILayout.Box(content, GUILayout.Height(20));
        //    //if (GUILayout.Button(content))
        //    //{
        //    //    EditorSceneManager.OpenScene(script);
        //    //}
        //}

        GUILayout.EndScrollView();
    }
}
