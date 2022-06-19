
using UnityEngine;

namespace Game.EquipmentAndInventory
{
    [CreateAssetMenu(fileName = "Rarity", menuName = "Item/RarityLevel", order = 1)]
    public class RarityData : ScriptableObject
    {
        public Rarity Rarity;
        public Sprite Sprite;
        public Material color;
    }
}