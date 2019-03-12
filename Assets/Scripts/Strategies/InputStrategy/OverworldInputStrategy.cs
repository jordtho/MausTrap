using Assets.Scripts.ExtensionMethods;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Interfaces.Strategies;
using System;
using UnityEngine;

namespace Assets.Scripts.Strategies.InputStrategy
{
    public class OverworldInputStrategy : IInputStrategy
    {
        IPlayer player;

        public OverworldInputStrategy(IPlayer player)
        {
            this.player = player;
        }

        public void ProcessAButtonInput() => player.PlayerCharacter.Interact(player.PlayerCharacter.ParseInteraction());
        public void ProcessBButtonInput() => player.PlayerCharacter.Attack();
        public void ProcessDirectionalInput(Vector2 directionalInputs) => player.PlayerCharacter.Move(directionalInputs);
        public void ProcessLButtonInput() => player.PlayerCharacter.InvincibilityFrames();
        public void ProcessRButtonInput() => throw new NotImplementedException();
        public void ProcessStartButtonInput() => player.OpenMenu();
        public void ProcessXButtonInput() => player.OpenChart();
        public void ProcessYButtonInput() => player.PlayerCharacter.UseItem();
    }
}
