using Game.Enum.Item.Definition;
using UnityEngine;

namespace Game.Runtime.Item.ScriptableItemDefinition
{
    [CreateAssetMenu(fileName = "Rarity", menuName = "Item/RarityLevel", order = 1)]
    public class RarityData : ScriptableObject
    {
        public Rarity Rarity;
        public Sprite Sprite;
        /// <summary>
        /// Standard trying to assign a default color to labels and tooltip are buged in the engine. This variable fixing problem with displaing the correct color for label.
        /// </summary>
        public Material colorMaterial;
    }
}