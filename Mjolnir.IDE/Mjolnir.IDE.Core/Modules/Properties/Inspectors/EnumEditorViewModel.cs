using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mjolnir.IDE.Core.Modules.Properties.Inspectors
{
    public class EnumValueViewModel<TEnum>
    {
        public TEnum Value { get; set; }
        public string Text { get; set; }
    }

    public class EnumEditorViewModel<TEnum> : EditorBase<TEnum>, ILabelledInspector
    {
        private readonly IUnityContainer _container;

        private readonly List<EnumValueViewModel<TEnum>> _items;
        public IEnumerable<EnumValueViewModel<TEnum>> Items
        {
            get { return _items; }
        }

        public EnumEditorViewModel(IUnityContainer container)
            :base(container)
        {
            _container = container;

            _items = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Select(x => new EnumValueViewModel<TEnum>
            {
                Value = x,
                Text = Enum.GetName(typeof(TEnum), x)
            }).ToList();
        }
    }

    public class EnumValueViewModel
    {
        public object Value { get; set; }
        public string Text { get; set; }
    }

    public class EnumEditorViewModel : EditorBase<Enum>, ILabelledInspector
    {
        private readonly IUnityContainer _container;

        private readonly List<EnumValueViewModel> _items;
        public IEnumerable<EnumValueViewModel> Items
        {
            get { return _items; }
        }

        public EnumEditorViewModel(IUnityContainer container, Type enumType)
            :base(container)
        {
            _container = container;

            _items = Enum.GetValues(enumType).Cast<object>().Select(x => new EnumValueViewModel
            {
                Value = x,
                Text = Enum.GetName(enumType, x)
            }).ToList();
        }
    }
}