using Game.Enum.Item.Definition;
using UnityEngine;

namespace Game.Runtime.Item.Definition
{
    [CreateAssetMenu(fileName = "Rarity", menuName = "Item/RarityLevel", order = 1)]
    public class RarityData : ScriptableObject
    {
        public Rarity Rarity;
        public UnityEngine.Sprite Sprite;
        public Material colorMaterial;
    }
}