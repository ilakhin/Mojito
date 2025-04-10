using System;
using System.Collections.Generic;

namespace IL.Mojito.Controls
{
    public abstract class ControlBinder : BaseBinder
    {
    }

    public abstract class ControlBinder<TViewModel> : ControlBinder
        where TViewModel : ControlViewModel
    {
        protected sealed override void OnViewModelChanged(BaseViewModel viewModel, ICollection<IDisposable> disposables)
        {
            OnViewModelChanged((TViewModel)viewModel, disposables);
        }

        protected abstract void OnViewModelChanged(TViewModel viewModel, ICollection<IDisposable> disposables);
    }
}
