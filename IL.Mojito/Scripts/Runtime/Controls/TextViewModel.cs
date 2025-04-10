using R3;

namespace IL.Mojito.Controls
{
    public sealed class TextViewModel : ControlViewModel
    {
        public readonly ReactiveProperty<string> Text;

        public TextViewModel(ReactiveProperty<string> text)
        {
            Text = text;
        }
    }
}
