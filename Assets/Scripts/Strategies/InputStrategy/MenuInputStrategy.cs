using Assets.Scripts.Interfaces;
using Assets.Scripts.Interfaces.Strategies;
using System;
using UnityEngine;

namespace Assets.Scripts.Strategies.InputStrategy
{
    public class MenuInputStrategy : IInputStrategy
    {
        IMenu menu;

        public MenuInputStrategy(IMenu menu)
        {
            this.menu = menu;
        }

        public void ProcessAButtonInput() => menu.Cursor.Focus.Select();
        public void ProcessBButtonInput() => menu.Cursor.Focus.Select();
        public void ProcessDirectionalInput(Vector2 directionalInputs) => menu.Cursor.Move( new[] { (int)directionalInputs.x, (int)directionalInputs.y } );
        public void ProcessLButtonInput() => throw new NotImplementedException();
        public void ProcessRButtonInput() => throw new NotImplementedException();
        public void ProcessStartButtonInput() => menu.Cursor.Focus.Select();
        public void ProcessXButtonInput() => throw new NotImplementedException();
        public void ProcessYButtonInput() => throw new NotImplementedException();
    }
}
