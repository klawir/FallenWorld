using UnityEngine;

namespace Game.Runtime.Reward
{
    [System.Serializable]
    public struct RewardCollector
    {
        [field: SerializeField] public LootTable LootTable { get; private set; }
        [field: SerializeField] public int Experience { get; private set; }
    }
}