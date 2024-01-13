using CanvasDEV.Editor.Core;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AnimationKeyFrameReplacer: EditorWindow
{
    private SpriteSheet _newSpriteSheet;

    private BaseAnimationData _animationData;
    private AnimatorOverrideController _animatorOverrideController;

    private List<AnimationClip> _newClips = new();

    private string _firstName;

    [MenuItem(CanvasDevEditorKeys.ToolsPath + "AnimationKeyFrame Replacer")]
    public static void ShowWindow()
    {
        GetWindow(typeof(AnimationKeyFrameReplacer));
    }

    void OnGUI()
    {
        GUILayout.Label("Animation Clip Copier", EditorStyles.boldLabel);

        _newSpriteSheet = EditorGUILayout.ObjectField("New Sprite Sheet", _newSpriteSheet, typeof(SpriteSheet), false) as SpriteSheet;

        _animationData = EditorGUILayout.ObjectField("Base Animation Sheet", _animationData, typeof(BaseAnimationData), false) as BaseAnimationData;

        _firstName = EditorGUILayout.TextField("New Clip First Name", _firstName);

        EditorGUILayout.LabelField($"The clip will be created with name: {_firstName} + _lastWordOfBaseClip");

        if (GUILayout.Button("Copy Animation Clip"))
        {
            if (_animationData != null)
            {
                CopyAnimationClip(_animationData);
                EditorUtility.DisplayDialog("Success", "Animation Clip copied with new sprites!", "OK");
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "Please select a valid Animation Clip.", "OK");
            }
        }
    }

    private void CopyAnimationClip(BaseAnimationData original)
    {
        _newClips.Clear();

        Debug.Log("Copying Animation...");

        foreach(var clip in original.clips)
        {
            string path = AssetDatabase.GetAssetPath(clip);
            AnimationClip newClip = Instantiate(clip);

            string[] splittedClipName = clip.name.Split('_');
            string lastWord = splittedClipName[splittedClipName.Length - 1];
            string middleWord = splittedClipName[splittedClipName.Length - 2];

            newClip.name = _firstName + "_" + middleWord + "_" + lastWord;

            // Replace sprites in AnimationClip
            var editorCurves = AnimationUtility.GetObjectReferenceCurveBindings(newClip);
            foreach (var editorCurveBinding in editorCurves)
            {
                ObjectReferenceKeyframe[] keyframes = AnimationUtility.GetObjectReferenceCurve(newClip, editorCurveBinding);
                for (int i = 0; i < keyframes.Length; i++)
                {
                    keyframes[i].value = GetNewSprite(clip, (Sprite)keyframes[i].value);
                }
                AnimationUtility.SetObjectReferenceCurve(newClip, editorCurveBinding, keyframes);

                // Assuming the property is a SpriteRenderer's sprite property
                if (editorCurveBinding.type == typeof(Sprite))
                {
                    Debug.Log("If");
                }
            }

            string newPath = $"Assets/_Project/Art/Animations/Characters/{_firstName}/{newClip.name}.anim";

            Directory.CreateDirectory(newPath);

            AssetDatabase.CreateAsset(newClip, newPath);
            AssetDatabase.SaveAssets();
        
            _newClips.Add(newClip);
        }

        CreateAnimatorOverride();

        AssetDatabase.Refresh();
    }

    private Sprite GetNewSprite(AnimationClip original, Sprite oldSprite)
    {
        int index = _animationData.spriteSheet.GetSpriteIndexOf(oldSprite);

        Debug.Log($"Old: {index}: {oldSprite.name}");
        Debug.Log($"New: {_newSpriteSheet.GetSpriteByIndex(index).name}");

        return _newSpriteSheet.GetSpriteByIndex(index);
    }

    private void CreateAnimatorOverride()
    {
        _animatorOverrideController = new AnimatorOverrideController();
        _animatorOverrideController.runtimeAnimatorController = _animationData.animatorController;

        for (int i = 0; i < _animationData.clips.Length; i++)
        {
            AnimationClip clip = _animationData.clips[i];

            _animatorOverrideController[clip.name] = _newClips[i];
        }

        string newPath = $"Assets/_Project/Art/Animations/Characters/{_firstName}/{_firstName}_Override.controller";

        AssetDatabase.CreateAsset(_animatorOverrideController, newPath);
        AssetDatabase.SaveAssets();

        Debug.Log("Animator Override Controller Created!: " + newPath);
    }
}