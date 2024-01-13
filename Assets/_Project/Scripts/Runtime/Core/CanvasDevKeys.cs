using UnityEngine;

namespace CanvasDEV.Runtime.Core
{
    public static class CanvasDevKeys
    {
        public const string ScriptablePath = "CanvasDEV/Scriptables/";

        public static class Audio
        {
            public static readonly string MasterAudioVolumeKey = "MASTER_KEY";
            public static readonly string MusicAudioVolumeKey = "MUSIC_KEY";
            public static readonly string SfxAudioVolumeKey = "SFX_KEY";

            public static readonly string MasterAudioMuteKey = "MASTER_MUTE";
            public static readonly string MusicAudioMuteKey = "MUSIC_MUTE";
            public static readonly string SfxAudioMuteKey = "SFX_MUTE";
        }

        public static class Options
        {
            public static readonly string FOVKey = "FOV_KEY";
            public static readonly string MouseSensibilityKey = "MOUSE_SENSIBILITY_KEY";
            public static readonly string GamepadVibrationKey = "GAMEPAD_VIBRATION_KEY";
            public static readonly string VsyncKey = "VSYNC_KEY";
            public static readonly string FullscreenKey = "FULLSCREEN_KEY";
            public static readonly string ResolutionKey = "RESOLUTION_KEY";
        }

        public static class Save
        {
            public static readonly string DefaultSavePath = Application.persistentDataPath + "/Saves/save.canvasdev";
        }
    }
}