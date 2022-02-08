using EquipmentAndInventory;
using System;

public interface IController
{
    Action<IItem> onItemHovered { get; set; }
    Action<IItem> onItemPickedUp { get; set; }
    Action<IItem> onItemAdded { get; set; }
    Action<IItem> onItemSwapped { get; set; }
    Action<IItem> onItemReturned { get; set; }
    Action<IItem> onItemDropped { get; set; }
}