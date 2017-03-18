using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Sdk.Interfaces
{
    //
    // Summary:
    //     Represents a collection that is observable.
    //
    // Type parameters:
    //   T:
    //     The type of elements contained in the collection.
    public interface IObservableCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, INotifyPropertyChanged, INotifyCollectionChanged
    {
        //
        // Summary:
        //     Adds the range.
        //
        // Parameters:
        //   items:
        //     The items.
        void AddRange(IEnumerable<T> items);
        //
        // Summary:
        //     Removes the range.
        //
        // Parameters:
        //   items:
        //     The items.
        void RemoveRange(IEnumerable<T> items);
    }
}