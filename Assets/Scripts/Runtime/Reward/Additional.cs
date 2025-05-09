using Game.Runtime.Math;
using UnityEngine;

namespace Game.Runtime.Reward
{
    [System.Serializable]
    public struct Additional
    {
        [SerializeField] private float chanceToDrop;

        internal bool Luck { get { return Chance.Luck(chanceToDrop); } }
        [field: SerializeField] internal float DropLimit { get; private set; }
        [field: SerializeField] internal Range Value { get; private set; }
        [field: SerializeField] internal Item.Loot.Loot Prefab { get; private set; }
    }
}
