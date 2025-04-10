using System;
using System.Collections.Generic;
using IL.Mojito.Controls;
using UnityEngine;

namespace IL.Mojito
{
    public sealed class SectionBinder : BaseBinder<SectionViewModel>
    {
        [SerializeField]
        private ButtonBinder _buttonBinder;

        [SerializeField]
        private TextBinder _textBinder;

        public string ViewId => name;

        protected override void OnViewModelChanged(SectionViewModel viewModel, ICollection<IDisposable> disposables)
        {
            _buttonBinder.SetViewModel(viewModel.ButtonViewModel);
            _textBinder.SetViewModel(viewModel.TextViewModel);
        }
    }
}
