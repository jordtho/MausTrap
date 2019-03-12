using System.Collections.Generic;

namespace Assets.Scripts.Interfaces
{
    public interface IMenu : IList<IMenuNode>, ISelectable
    {
        IMenuCursor Cursor { get; }
        int ColumnCount { get; set; }
    }
}
