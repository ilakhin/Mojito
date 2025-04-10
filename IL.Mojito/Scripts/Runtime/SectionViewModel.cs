using IL.Mojito.Controls;

namespace IL.Mojito
{
    public sealed class SectionViewModel : BaseViewModel
    {
        public readonly string ViewId;
        public readonly ButtonViewModel ButtonViewModel;
        public readonly TextViewModel TextViewModel;

        public SectionViewModel(string viewId, ButtonViewModel buttonViewModel, TextViewModel textViewModel)
        {
            ViewId = viewId;
            ButtonViewModel = buttonViewModel;
            TextViewModel = textViewModel;
        }
    }
}
