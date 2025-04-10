using System;
using System.Collections.Generic;
using System.Linq;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace IL.Mojito.Controls
{
    public sealed class DropdownBinder : ControlBinder<DropdownViewModel>
    {
        [SerializeField]
        private TMP_Dropdown _dropdown;

        protected override void OnViewModelChanged(DropdownViewModel viewModel, ICollection<IDisposable> disposables)
        {
            _dropdown.options = viewModel.Options
                .Select(static option => new TMP_Dropdown.OptionData(option))
                .ToList();

            viewModel.Value
                .Subscribe(_dropdown, static (value, dropdown) => dropdown.value = value)
                .AddTo(disposables);

            var call = new UnityAction<int>(value => viewModel.Value.Value = value);

            _dropdown.onValueChanged.AddListener(call);

            Disposable.Create((UnityEvent: _dropdown.onValueChanged, UnityAction: call), static stateTuple =>
            {
                var (unityEvent, unityAction) = stateTuple;

                unityEvent.RemoveListener(unityAction);
            }).AddTo(disposables);
        }
    }
}
