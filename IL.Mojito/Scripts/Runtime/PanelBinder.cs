using System;
using System.Collections.Generic;
using UnityEngine;

namespace IL.Mojito
{
    public sealed class PanelBinder : BaseBinder<PanelViewModel>
    {
        [SerializeField]
        private NavigationBinder _navigationBinder;

        [SerializeField]
        private ContentBinder _contentBinder;

        protected override void OnViewModelChanged(PanelViewModel viewModel, ICollection<IDisposable> disposables)
        {
            _navigationBinder.SetViewModel(viewModel.NavigationViewModel);
            _contentBinder.SetViewModel(viewModel.ContentViewModel);
        }
    }
}
