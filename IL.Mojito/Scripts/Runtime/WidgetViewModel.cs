using System.Collections.Generic;
using IL.Mojito.Controls;

namespace IL.Mojito
{
    public sealed class WidgetViewModel : BaseViewModel
    {
        public readonly string ViewId;
        public readonly IReadOnlyDictionary<string, ControlViewModel> ControlViewModels;

        public WidgetViewModel(string viewId, IReadOnlyDictionary<string, ControlViewModel> controlViewModels)
        {
            ViewId = viewId;
            ControlViewModels = controlViewModels;
        }
    }
}
