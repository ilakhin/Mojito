namespace IL.Mojito
{
    public sealed class PanelViewModel : BaseViewModel
    {
        public readonly NavigationViewModel NavigationViewModel = new();
        public readonly ContentViewModel ContentViewModel = new();
    }
}
