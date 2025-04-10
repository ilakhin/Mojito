using System;
using System.Collections.Generic;
using R3;
using TMPro;
using UnityEngine;

namespace IL.Mojito.Controls
{
    public sealed class TextBinder : ControlBinder<TextViewModel>
    {
        [SerializeField]
        private TMP_Text _text;

        protected override void OnViewModelChanged(TextViewModel viewModel, ICollection<IDisposable> disposables)
        {
            viewModel.Text
                .Subscribe(_text, static (value, text) => text.text = value)
                .AddTo(disposables);
        }
    }
}
