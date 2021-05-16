using System.Collections.ObjectModel;

namespace Rentering.Common.Shared.Collections
{
    public class MyCollection<T> : Collection<T>
    {
        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
        }
    }
}
