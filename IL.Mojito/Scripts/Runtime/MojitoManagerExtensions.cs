using System.Collections.Generic;

namespace IL.Mojito
{
    public static class MojitoManagerExtensions
    {
        public static void AddWidgetViewModels(this MojitoManager mojitoManager, string path, int priority, IEnumerable<WidgetViewModel> widgetViewModels)
        {
            foreach (var widgetViewModel in widgetViewModels)
            {
                mojitoManager.AddWidgetViewModel(path, priority, widgetViewModel);
            }
        }
    }
}
