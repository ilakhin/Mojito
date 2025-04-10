using R3;

namespace IL.Mojito.Controls
{
    public sealed class ButtonViewModel : ControlViewModel
    {
        public readonly Subject<ButtonViewModel> ClickSubject;

        public ButtonViewModel(Subject<ButtonViewModel> clickSubject)
        {
            ClickSubject = clickSubject;
        }
    }
}
