using UnityEngine;
using TMPro;
using System;

namespace Inventory
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private InventorySlotView[] _slots;
        [SerializeField] private TMP_Text _textOwner;

        public string OwnerId
        {
            get { return _textOwner.text; }
            set { _textOwner.text = value; }
        }

        public InventorySlotView GetInventorySlotView(int insex)
        {
            return _slots[insex];
        }
    }
}