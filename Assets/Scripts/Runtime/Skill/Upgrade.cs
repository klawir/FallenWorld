using UnityEngine;

namespace Game.Runtime.Skill
{
    [System.Serializable]
    public struct Upgrade
    {
        [field: SerializeField] public Damage Damage { get; private set; }
        [field: SerializeField] public float CoolDown { get; private set; }
        [field: SerializeField] public int ManaCost { get; private set; }
        [field: SerializeField] public float RangeOfAttack { get; private set; }
    }
}