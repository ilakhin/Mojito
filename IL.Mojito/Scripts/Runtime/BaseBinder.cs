using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

namespace IL.Mojito
{
    [DisallowMultipleComponent]
    public abstract class BaseBinder : MonoBehaviour
    {
        private CompositeDisposable _compositeDisposable;
        private BaseViewModel _viewModel;

        public void SetViewModel(BaseViewModel viewModel)
        {
            if (_viewModel == viewModel)
            {
                return;
            }

            _viewModel = viewModel;

            if (_viewModel == null)
            {
                _compositeDisposable.Dispose();
                _compositeDisposable = null;
            }
            else
            {
                if (_compositeDisposable == null)
                {
                    _compositeDisposable = new CompositeDisposable();
                }
                else
                {
                    _compositeDisposable.Clear();
                }

                OnViewModelChanged(viewModel, _compositeDisposable);
            }
        }

        protected abstract void OnViewModelChanged(BaseViewModel viewModel, ICollection<IDisposable> disposables);
    }

    public abstract class BaseBinder<TViewModel> : BaseBinder
        where TViewModel : BaseViewModel
    {
        protected sealed override void OnViewModelChanged(BaseViewModel viewModel, ICollection<IDisposable> disposables)
        {
            OnViewModelChanged((TViewModel)viewModel, disposables);
        }

        protected abstract void OnViewModelChanged(TViewModel viewModel, ICollection<IDisposable> disposables);
    }
}
