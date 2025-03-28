using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GoogleSheetsImporter
{
    [Serializable]
    public class ItemSettings
    {
        public string ID;
        public int CellCapacity;
        public string Titlie;
        public string Description;
        public int Price;
        public Sprite Sprite; // Добавляем поле для спрайта
        /*public string Id;
        public int CellCapacity;
        public string Title;
        public string Description;
        public string IconeNAme;*/
    }
}