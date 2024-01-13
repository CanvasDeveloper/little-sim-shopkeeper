using CanvasDEV.Editor.Core;
using UnityEditor;
using UnityEngine;

public class AnimationKeyFrameReplacer: EditorWindow
{
    private SpriteSheet oldSpriteSheet;
    private SpriteSheet newSpriteSheet;

    private AnimationClip originalClip;

    [MenuItem(CanvasDevEditorKeys.ToolsPath + "AnimationKeyFrame Replacer")]
    public static void ShowWindow()
    {
        GetWindow(typeof(AnimationKeyFrameReplacer));
    }

    void OnGUI()
    {
        GUILayout.Label("Animation Clip Copier", EditorStyles.boldLabel);

        oldSpriteSheet = EditorGUILayout.ObjectField("Old Sprite Sheet", oldSpriteSheet, typeof(SpriteSheet), false) as SpriteSheet;
        newSpriteSheet = EditorGUILayout.ObjectField("New Sprite Sheet", newSpriteSheet, typeof(SpriteSheet), false) as SpriteSheet;

        originalClip = EditorGUILayout.ObjectField("Original Animation Clip", originalClip, typeof(AnimationClip), false) as AnimationClip;

        if (GUILayout.Button("Copy Animation Clip"))
        {
            if (originalClip != null)
            {
                CopyAnimationClip(originalClip);
                EditorUtility.DisplayDialog("Success", "Animation Clip copied with new sprites!", "OK");
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "Please select a valid Animation Clip.", "OK");
            }
        }
    }

    private void CopyAnimationClip(AnimationClip original)
    {
        Debug.Log("Copying Animation...");

        string path = AssetDatabase.GetAssetPath(original);
        AnimationClip newClip = Instantiate(original);
        newClip.name = original.name + "_Copy";

        // Replace sprites in AnimationClip
        var editorCurves = AnimationUtility.GetObjectReferenceCurveBindings(newClip);
        foreach (var editorCurveBinding in editorCurves)
        {
            ObjectReferenceKeyframe[] keyframes = AnimationUtility.GetObjectReferenceCurve(newClip, editorCurveBinding);
            for (int i = 0; i < keyframes.Length; i++)
            {
                keyframes[i].value = GetNewSprite(original, (Sprite)keyframes[i].value);
            }
            AnimationUtility.SetObjectReferenceCurve(newClip, editorCurveBinding, keyframes);

            // Assuming the property is a SpriteRenderer's sprite property
            if (editorCurveBinding.type == typeof(Sprite))
            {
                Debug.Log("If");
            }
        }

        AssetDatabase.CreateAsset(newClip, path.Replace(".anim", "_Copy.anim"));
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private Sprite GetNewSprite(AnimationClip original, Sprite oldSprite)
    {
        Debug.Log("Get new Sprite");

        int index = oldSpriteSheet.GetSpriteIndexOf(oldSprite);

        Debug.Log($"{index}: {oldSprite.name}");
        Debug.Log($"New: {newSpriteSheet.GetSpriteByIndex(index).name}");

        return newSpriteSheet.GetSpriteByIndex(index);
    }
}