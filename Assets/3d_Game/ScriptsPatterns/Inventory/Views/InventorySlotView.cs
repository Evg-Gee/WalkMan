using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

namespace Inventory
{
    public class InventorySlotView : MonoBehaviour
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private TMP_Text _textTitle;
        [SerializeField] private TMP_Text _textAmount;

        public Sprite ItemImage                                // Метод для установки спрайта
        {
              get {return _itemImage.sprite;}
            set
            {
                _itemImage.sprite = value;
                _itemImage.enabled = value != null;
            }                                                 // Включаем или выключаем Image в зависимости от наличия спрайта
        }

        public string Title                             //Заполняется в контроллере
        {
            get { return _textTitle.text; }
            set { _textTitle.text = value;}
        }

        public int Amount                                //Заполняется в контроллере
        {
            get => Convert.ToInt32(_textAmount.text);
            set => _textAmount.text = value ==0 ? "" : value.ToString();
        }      
    }
}