using System.Collections.Generic;
using R3;

namespace IL.Mojito.Controls
{
    public sealed class DropdownViewModel : ControlViewModel
    {
        public readonly IReadOnlyList<string> Options;
        public readonly ReactiveProperty<int> Value;

        public DropdownViewModel(IReadOnlyList<string> options, ReactiveProperty<int> value)
        {
            Options = options;
            Value = value;
        }
    }
}
