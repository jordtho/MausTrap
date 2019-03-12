using Assets.Scripts.Interfaces;
using System;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class MenuCursor : MonoBehaviour, IMenuCursor
    {
        IMenu menu;

        public MenuCursor(IMenu menu)
        {
            this.menu = menu;
            CurrentPosition = new int[2];
        }

        public int[] CurrentPosition { get; set; }
        public float FlickerRate { get; set; }

        public ISelectable Focus => menu[CurrentPosition[0] + CurrentPosition[1] * (menu.Count / menu.ColumnCount)];
        public void Move(int[] inputDirecitons) => CurrentPosition = new int[2] { CurrentPosition[0] + inputDirecitons[0], CurrentPosition[1] + inputDirecitons[1] };
    }
}
