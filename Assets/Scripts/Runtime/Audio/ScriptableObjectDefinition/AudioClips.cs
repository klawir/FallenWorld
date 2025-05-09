using UnityEngine;

namespace Game.Runtime.Audio.ScriptableObjectDefinition
{
    [CreateAssetMenu(fileName = nameof(AudioClips), menuName = "Audio/Set", order = 1)]
    public class AudioClips : ScriptableObject
    {
        [field: SerializeField] public AudioClip[] Clips { get; private set; }
    }
}