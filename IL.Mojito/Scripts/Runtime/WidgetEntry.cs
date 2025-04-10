namespace IL.Mojito
{
    internal sealed class WidgetEntry
    {
        public readonly string Path;
        public readonly int Priority;
        public readonly WidgetViewModel WidgetViewModel;

        public WidgetEntry(string path, int priority, WidgetViewModel widgetViewModel)
        {
            Path = path;
            Priority = priority;
            WidgetViewModel = widgetViewModel;
        }
    }
}
