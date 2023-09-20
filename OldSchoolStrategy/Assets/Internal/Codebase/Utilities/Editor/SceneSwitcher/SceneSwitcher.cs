// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - GitHub:   https://github.com/RimuruDev
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//          - GitHub Organizations: https://github.com/Rimuru-Dev
//
// **************************************************************** //

using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

namespace Internal.Codebase.Utilities.Editor.SceneSwitcher
{
    public sealed class SceneSwitcher : EditorWindow
    {
        private bool showAllScenes;

        [MenuItem("Rimuru Dev Tools/Scene Switcher")]
        private static void ShowWindow() =>
            GetWindow(typeof(SceneSwitcher));

        private void OnGUI()
        {
            GUILayout.Label("Scene Switcher", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();

            showAllScenes = EditorGUILayout.Toggle("Show Absolutely All Scenes", showAllScenes);

            if (EditorGUI.EndChangeCheck())
                Repaint();

            var scenePaths =
                showAllScenes
                    ? GetAllScenePaths()
                    : GetScenePathsByBuildSettings();

            foreach (var scenePath in scenePaths)
                if (GUILayout.Button(Path.GetFileNameWithoutExtension(scenePath)))
                    EditorSceneManager.OpenScene(scenePath);
        }

        private static string[] GetScenePathsByBuildSettings()
        {
            var paths = new string[EditorBuildSettings.scenes.Length];

            for (var i = 0; i < EditorBuildSettings.scenes.Length; i++)
                paths[i] = EditorBuildSettings.scenes[i].path;

            return paths;
        }

        private static string[] GetAllScenePaths()
        {
            var guids = AssetDatabase.FindAssets("t:Scene");

            var scenePaths = new string[guids.Length];

            for (var i = 0; i < scenePaths.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                scenePaths[i] = path;
            }

            return scenePaths;
        }
    }
}