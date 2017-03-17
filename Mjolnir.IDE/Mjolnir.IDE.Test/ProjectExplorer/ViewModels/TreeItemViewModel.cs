using Mjolnir.UI.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Mjolnir.IDE.Test.ProjectExplorer.ViewModels
{
    public class TreeItemViewModel : ValidatableBindableBase
    {
        #region Data

        private static readonly TreeItemViewModel DummyChild = new TreeItemViewModel();

        private readonly ObservableCollection<TreeItemViewModel> children;
        private readonly TreeItemViewModel parent;

        private bool isExpanded;
        private bool isSelected;
        private bool isEditable = true;
        private bool isEditing;
        private bool isEnabled = true;
        private bool isVisible = true;
        private string remarks;


        #endregion Data

        #region Constructor

        public TreeItemViewModel(TreeItemViewModel parent, bool lazyLoadChildren)
        {
            this.parent = parent;

            if (parent != null)
            {
                parent.Children.Add(this);
            }

            children = new ObservableCollection<TreeItemViewModel>();

            if (lazyLoadChildren)
                children.Add(DummyChild);
        }

        // This is used to create the DummyChild instance.
        private TreeItemViewModel()
        {
        }

        #endregion Constructor

        #region Public properties

        /// <summary>
        /// Returns the logical child items of this object.
        /// </summary>
        public ObservableCollection<TreeItemViewModel> Children
        {
            get { return children; }
        }

        /// <summary>
        /// Returns true if this object's Children have not yet been populated.
        /// </summary>
        public bool HasDummyChild
        {
            get { return Children.Count == 1 && Children[0] == DummyChild; }
        }

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is expanded.
        /// </summary>
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                if (value != isExpanded)
                {
                    SetProperty(ref isExpanded, value);

                    // Expand all the way up to the root.
                    if (isExpanded && parent != null)
                        parent.IsExpanded = true;

                    // Lazy load the child items, if necessary.
                    if (isExpanded && HasDummyChild)
                    {
                        Children.Remove(DummyChild);
                    }
                }
            }
        }

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (value != isSelected)
                {
                    SetProperty(ref isSelected, value);
                }
            }
        }

        public bool IsEditable
        {
            get { return isEditable; }
            set
            {
                if (value != isEditable)
                {
                    SetProperty(ref isEditable, value);
                }
            }
        }

        public bool IsEditing
        {
            get { return isEditing; }
            set
            {
                if (value != isEditing)
                {
                    SetProperty(ref isEditing, value);
                }
            }
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                if (value != isEnabled)
                {
                    SetProperty(ref isEnabled, value);
                }
            }
        }

        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                if (value != isVisible)
                {
                    SetProperty(ref isVisible, value);
                }
            }
        }

        public string Remarks
        {
            get { return remarks; }
            set
            {
                if (value != remarks)
                {
                    SetProperty(ref remarks, value);
                }
            }
        }

        public TreeItemViewModel Parent
        {
            get { return parent; }
        }

        public override string ToString()
        {
            return "{Node " + DisplayName + "}";
        }

        #endregion Public properties

        #region ViewModelBase

        /// <summary>
        /// Returns the user-friendly name of this object.
        /// Child classes can set this property to a new value,
        /// or override it to determine the value on-demand.
        /// </summary>
        private string displayName;
        public virtual string DisplayName
        {
            get { return displayName; }
            set
            {
                if (value != displayName)
                {
                    SetProperty(ref displayName, value);
                }
            }
        }

        private ImageSource _icon;
        public ImageSource Icon
        {
            get { return _icon; }
            set { SetProperty(ref _icon, value); }
        }

        #endregion ViewModelBase
    }

}
