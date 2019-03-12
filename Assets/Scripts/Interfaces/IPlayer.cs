using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Interfaces
{
    public interface IPlayer
    {
        IPlayerCharacter PlayerCharacter { get; }

        void OpenChart();
        void OpenMenu();
    }
}
