
using UnityEngine;

namespace Game.Runtime.Utility
{
    public class AudioUtility
    {
        internal static void EnableAudioListener()
        {
            AudioListener.volume = 1f;
        }

        internal static void DisableAudioListener()
        {
            AudioListener.volume = 0f;
        }
    }
}