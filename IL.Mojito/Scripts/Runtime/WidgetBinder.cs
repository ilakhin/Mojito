using System;
using System.Collections.Generic;
using IL.Mojito.Controls;
using UnityEngine;

namespace IL.Mojito
{
    public sealed class WidgetBinder : BaseBinder<WidgetViewModel>
    {
        [SerializeField]
        private Template<ControlBinder>[] _controlBinderTemplates;

        protected override void OnViewModelChanged(WidgetViewModel viewModel, ICollection<IDisposable> disposables)
        {
            foreach (var template in _controlBinderTemplates)
            {
                var controlBinder = template.Binder;
                var controlViewModel = viewModel.ControlViewModels[template.Key];

                controlBinder.SetViewModel(controlViewModel);
            }
        }
    }
}
