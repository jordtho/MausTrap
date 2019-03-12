using Assets.Scripts.ExtensionMethods;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Interfaces.Strategies;
using System;
using UnityEngine;

namespace Assets.Scripts.Strategies.InputStrategy
{
    public class DialogInputStrategy : IInputStrategy
    {
        IDialogNode dialogNode;

        public DialogInputStrategy(IDialogNode dialogNode)
        {
            this.dialogNode = dialogNode;
        }

        public void ProcessAButtonInput() => dialogNode.Acknowledge();
        public void ProcessBButtonInput() => dialogNode.Acknowledge();
        public void ProcessDirectionalInput(Vector2 directionalInputs) => throw new NotImplementedException();
        public void ProcessLButtonInput() => throw new NotImplementedException();
        public void ProcessRButtonInput() => throw new NotImplementedException();
        public void ProcessStartButtonInput() => dialogNode.Acknowledge();
        public void ProcessXButtonInput() => throw new NotImplementedException();
        public void ProcessYButtonInput() => throw new NotImplementedException();
    }
}
