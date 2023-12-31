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

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Diagnostics.CodeAnalysis;

namespace RimuruDev.Internal.Codebase.Utilities.Editor.ScriptFinders
{
    [SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
    public sealed class ScriptFinder : EditorWindow
    {
        private const string Title = "Script Finder";
        private const string Label = "Script Search";
        private const string RootFolderFiels = "Root Folder";
        private const string FindInternalButton = "Find Scripts";
        private const string FindAllButton = "Find All Project Scripts";
        private const string SearchPattern = "*.cs";
        private const bool searchAllSubFolders = true;

        private int totalScriptsFound;
        private string[] scriptsInFolder;
        private string rootFolder = "/Internal";

        private string MyScriptPath =>
            Path.Combine(Application.dataPath, rootFolder);

        private static string ProjectScriptPath =>
            Application.dataPath;

        [MenuItem("Rimuru Dev Tools/Script Finder")]
        public static void ShowWindow()
        {
            var window = GetWindow<ScriptFinder>(Title);
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label(Label, EditorStyles.boldLabel);

            rootFolder = EditorGUILayout.TextField(RootFolderFiels, rootFolder);

            FindMyScripts();
            FindAllScripts();
        }

        private void FindAllScripts()
        {
            if (GUILayout.Button(FindAllButton) && !string.IsNullOrEmpty(ProjectScriptPath))
                FindScripts(ProjectScriptPath);
        }

        private void FindMyScripts()
        {
            if (GUILayout.Button(FindInternalButton) && !string.IsNullOrEmpty(MyScriptPath))
                FindScripts(MyScriptPath);
        }

        private void FindScripts(string scriptPath)
        {
            scriptsInFolder = Directory.GetFiles(scriptPath, SearchPattern, SearchOption.AllDirectories);

            totalScriptsFound = scriptsInFolder.Length;

            Debug.Log($"<color=magenta>Scripts Found:</color> <color=yellow>{totalScriptsFound}</color>");
        }
    }
}
#endif