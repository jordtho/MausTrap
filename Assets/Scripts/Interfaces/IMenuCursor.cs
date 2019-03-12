using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IMenuCursor
    {
        float FlickerRate { get; set; }
        int[] CurrentPosition { get; set; }
        ISelectable Focus { get; }

        void Move(int[] directionalInputs);
    }
}
