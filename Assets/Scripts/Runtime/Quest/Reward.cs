using UnityEngine;

namespace Game.Runtime.Quest
{
    [System.Serializable]
    public struct Reward
    {
        [field: SerializeField] public int GetExperience { get; private set; }
        [field: SerializeField] public uint GetGold { get; private set; }
    }
}