using System;
using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace IL.Mojito.Controls
{
    public sealed class SliderBinder : ControlBinder<SliderViewModel>
    {
        [SerializeField]
        private Slider _slider;

        protected override void OnViewModelChanged(SliderViewModel viewModel, ICollection<IDisposable> disposables)
        {
            _slider.minValue = viewModel.MinValue;
            _slider.maxValue = viewModel.MaxValue;
            _slider.wholeNumbers = viewModel.WholeNumbers;

            viewModel.Value
                .Subscribe(_slider, static (value, slider) => slider.value = value)
                .AddTo(disposables);

            var call = new UnityAction<float>(value => viewModel.Value.Value = value);

            _slider.onValueChanged.AddListener(call);

            Disposable.Create((UnityEvent: _slider.onValueChanged, UnityAction: call), static stateTuple =>
            {
                var (unityEvent, unityAction) = stateTuple;

                unityEvent.RemoveListener(unityAction);
            }).AddTo(disposables);
        }
    }
}
