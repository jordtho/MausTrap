using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Interfaces.Managers
{
    public interface IMenuManager
    {
        void GetGameObject();
        void OpenMenu(IMenu menu);
        IMenu CloseMenu();
        void ClearMenuStack();
    }
}
