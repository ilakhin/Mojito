using R3;

namespace IL.Mojito.Controls
{
    public sealed class SliderViewModel : ControlViewModel
    {
        public readonly float MinValue;
        public readonly float MaxValue;
        public readonly bool WholeNumbers;
        public readonly ReactiveProperty<float> Value;

        public SliderViewModel(float minValue, float maxValue, bool wholeNumbers, ReactiveProperty<float> value)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            WholeNumbers = wholeNumbers;
            Value = value;
        }
    }
}
