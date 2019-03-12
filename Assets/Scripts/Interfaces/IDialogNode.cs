using System.Collections.Generic;

namespace Assets.Scripts.Interfaces
{
    public interface IDialogNode
    {
        string Dialog { get; set; }
        List<IDialogNode> ChildNodes { get; set; }

        void Acknowledge();
    }
}
