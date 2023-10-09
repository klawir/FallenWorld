
using Game.Enum.Item.Definition;
using UnityEngine;

namespace Game.Item.Definition
{
    [CreateAssetMenu(fileName = "Rarity", menuName = "Item/RarityLevel", order = 1)]
    public class RarityData : ScriptableObject
    {
        public Rarity Rarity;
        public Sprite Sprite;
        public Material colorMaterial;
    }
}