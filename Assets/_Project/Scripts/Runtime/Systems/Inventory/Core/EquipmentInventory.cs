using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CanvasDEV.Runtime.Systems.Inventory
{
    public class EquipmentInventory : InventoryBase
    {
        public event Action OnUpdateEquipment;

        [SerializeField] private EquipableData defaultData;

        [SerializeField] private List<ActiveEquipmentLocation> activeEquipmentLocations;

        private Dictionary<EquipType, EquippableItemData> equipItemDictionary = new();

        private void Start()
        {
            LoadDefaultEquipement();
        }

        private void LoadDefaultEquipement()
        {
            foreach (var equipLocation in activeEquipmentLocations)
            {
                var foundedOverrider = defaultData.OverrideControllerArray.FirstOrDefault(x => x.equipItem.equipType == equipLocation.type);

                if (foundedOverrider == null || foundedOverrider.equipItem.overrideControler == null)
                {
                    continue;
                }

                equipLocation.animator.runtimeAnimatorController = foundedOverrider.equipItem.overrideControler;
            }
        }

        public void EquipItem(EquippableItemData equipItem)
        {
            equipItemDictionary[equipItem.equipType] = equipItem;

            FindLocation(equipItem.equipType).animator.runtimeAnimatorController = equipItem.overrideControler;

            OnUpdateEquipment?.Invoke();
        }

        public void RemoveEquip(EquippableItemData equipItem)
        {
            if (!equipItemDictionary.ContainsKey(equipItem.equipType))
                return;

            equipItemDictionary.Remove(equipItem.equipType);

            FindLocation(equipItem.equipType).animator.runtimeAnimatorController = null;

            OnUpdateEquipment?.Invoke();
        }

        private ActiveEquipmentLocation FindLocation(EquipType equipType)
        {
            return activeEquipmentLocations.FirstOrDefault(x => x.type == equipType);
        }

        public Dictionary<EquipType, EquippableItemData> GetItems()
        {
            return equipItemDictionary;
        }

        public override void Open()
        {

        }
    }
}