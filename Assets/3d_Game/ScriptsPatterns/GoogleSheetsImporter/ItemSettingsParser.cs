using System;
using System.Collections.Generic;
using UnityEngine;

namespace GoogleSheetsImporter
{
    public class ItemSettingsParser : IGoogleSheetParser
    {
        private readonly GameSettings _gameSettings;
        private ItemSettings _currentItemSettings;

        public ItemSettingsParser(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            _gameSettings.ItemSettings = new List<ItemSettings>();
        }

        public void Parse(string header, string token)
        {
            switch (header)
            {
                case "ID":
                    _currentItemSettings = new ItemSettings
                    {
                        ID = token
                    };
                    _gameSettings.ItemSettings.Add(_currentItemSettings);
                   break;

                case "CellCapacity":
                    _currentItemSettings.CellCapacity = Convert.ToInt32(token);
                    break;

                case "Titlie":
                    _currentItemSettings.Titlie = token;
                    break;

                case "Description":
                    _currentItemSettings.Description = token;
                    break;

                case "Price":
                    _currentItemSettings.Price = Convert.ToInt32(token); 
                    break;

                case "Sprite":                                                     // Добавляем загрузку спрайта
                    _currentItemSettings.Sprite = Resources.Load<Sprite>(token);   // Загружаем спрайт из Resources
                    break;

                default:
                    throw new Exception($"Invalid header: {header}");
            }
        }
    }
}