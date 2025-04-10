using R3;

namespace IL.Mojito.Controls
{
    public sealed class InputFieldViewModel : ControlViewModel
    {
        public readonly ReactiveProperty<string> Text;

        public InputFieldViewModel(ReactiveProperty<string> text)
        {
            Text = text;
        }
    }
}
