using System;
using System.Collections.Generic;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace IL.Mojito.Controls
{
    public sealed class InputFieldBinder : ControlBinder<InputFieldViewModel>
    {
        [SerializeField]
        private TMP_InputField _inputField;

        protected override void OnViewModelChanged(InputFieldViewModel viewModel, ICollection<IDisposable> disposables)
        {
            viewModel.Text
                .Subscribe(_inputField, static (value, inputField) => inputField.text = value)
                .AddTo(disposables);

            var call = new UnityAction<string>(value => viewModel.Text.Value = value);

            _inputField.onValueChanged.AddListener(call);

            Disposable.Create((UnityEvent: _inputField.onValueChanged, UnityAction: call), static stateTuple =>
            {
                var (unityEvent, unityAction) = stateTuple;

                unityEvent.RemoveListener(unityAction);
            }).AddTo(disposables);
        }
    }
}
