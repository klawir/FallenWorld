using UnityEngine;

namespace Game.Runtime.Reward
{
    [System.Serializable]
    public struct RewardCollector
    {
        [SerializeField] private LootTable lootTable;
        [SerializeField] private int experience;

        public LootTable LootTable => lootTable;
        public int Experience => experience;
    }
}