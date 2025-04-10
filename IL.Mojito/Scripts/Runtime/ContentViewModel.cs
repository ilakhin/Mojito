using System.Collections.Generic;
using ObservableCollections;

namespace IL.Mojito
{
    public sealed class ContentViewModel : BaseViewModel
    {
        private readonly ObservableList<WidgetViewModel> _widgetViewModels = new();

        public IReadOnlyObservableList<WidgetViewModel> WidgetViewModels => _widgetViewModels;

        public void SetWidgetViewModels(IEnumerable<WidgetViewModel> widgetViewModels)
        {
            _widgetViewModels.Clear();
            _widgetViewModels.AddRange(widgetViewModels);
        }
    }
}
