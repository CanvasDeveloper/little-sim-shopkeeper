using CanvasDEV.Editor.Core;
using UnityEditor;
using UnityEngine;

namespace CanvasDEV.Editor.TemplateCreator
{
    public class CreateFolderStructure : EditorWindow
    {
        private static string rootFolderName = "_Project";
        private static bool createGitPlaceholder = true;

        [MenuItem(CanvasDevEditorKeys.ToolsPath + "Create Default Folders")]
        private static void CreateWindow()
        {
            CreateFolderStructure window = CreateInstance<CreateFolderStructure>();
            window.position = new Rect(Screen.width / 2, Screen.height / 2, 400, 300);
            window.Show();
        }

        private static void CreateFolders()
        {
            const string ASSETS_FOLDER = "Assets";
            const string NO_VERSION_CONTROL_FOLDER = "__NoVersionControl";
            const string AUDIO_FOLDER = "Audio";
            const string SCRIPTS_FOLDER = "Scripts";
            const string PREFABS_FOLDER = "Prefabs";
            const string SCENES_FOLDER = "Scenes";
            const string DATA_FOLDER = "Data";
            const string PLUGINS_FOLDER = "Plugins";

            // Create the root folder which you can name
            VirtualFolder rootFolder = new(ASSETS_FOLDER, rootFolderName);
            VirtualFolder noVersion = new(ASSETS_FOLDER, NO_VERSION_CONTROL_FOLDER);

            rootFolder.AddSubFolder("Art").AddSubFolders(new string[] { "Animations", "Materials", "Models", "Shaders", "Sprites", "Textures", "Fonts" });

            // Add more top level folders.
            rootFolder.AddSubFolders(new string[] { AUDIO_FOLDER, SCRIPTS_FOLDER, PREFABS_FOLDER, SCENES_FOLDER, DATA_FOLDER, PLUGINS_FOLDER });

            rootFolder.AddSubFolder(SCENES_FOLDER).AddSubFolders(new string[] { "Game", "Prototype" });
            rootFolder.AddSubFolder("Scripts").AddSubFolders(new string[] { "Editor" });
            rootFolder.AddSubFolder("Scripts").AddSubFolder("Runtime").AddSubFolders(new string[] { "Implementations", "Systems", "Utility" });


            // By calling Realize() on the root folder, it will automatically create every folder we've added to it or its sub folders!
            rootFolder.Realize(createGitPlaceholder);
            noVersion.Realize(createGitPlaceholder);

            AssetDatabase.Refresh();
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Enter name of the root folder: ");
            rootFolderName = EditorGUILayout.TextField("Root:", rootFolderName);
            createGitPlaceholder = EditorGUILayout.ToggleLeft("Placeholders", createGitPlaceholder);
            EditorGUILayout.HelpBox("When ticked, a .gitkeep file will be created in the folder to make Git keep it. This file will not be imported by Unity and will only be visible in the explorer.", MessageType.Info);


            if (GUILayout.Button("Generate"))
            {
                CreateFolders();
                Close();
            }
            EditorGUILayout.EndVertical();
        }
    }
}