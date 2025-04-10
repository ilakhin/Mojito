using System;
using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace IL.Mojito.Controls
{
    public sealed class ButtonBinder : ControlBinder<ButtonViewModel>
    {
        [SerializeField]
        private Button _button;

        protected override void OnViewModelChanged(ButtonViewModel viewModel, ICollection<IDisposable> disposables)
        {
            var call = new UnityAction(() => viewModel.ClickSubject.OnNext(viewModel));

            _button.onClick.AddListener(call);

            Disposable.Create((UnityEvent: _button.onClick, UnityAction: call), static stateTuple =>
            {
                var (unityEvent, unityAction) = stateTuple;

                unityEvent.RemoveListener(unityAction);
            }).AddTo(disposables);
        }
    }
}
