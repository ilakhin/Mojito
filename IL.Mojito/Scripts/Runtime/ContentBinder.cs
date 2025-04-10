using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using ObservableCollections;
using R3;
using UnityEngine;

namespace IL.Mojito
{
    public sealed class ContentBinder : BaseBinder<ContentViewModel>
    {
        [SerializeField]
        private Template<WidgetBinder>[] _widgetBinderTemplates;

        private List<WidgetBinder> _widgetBinders;
        private Dictionary<string, WidgetBinder> _widgetBinderMap;

        // TODO: Добавить очистку при смене ViewModel
        protected override void OnViewModelChanged(ContentViewModel viewModel, ICollection<IDisposable> disposables)
        {
            var widgetViewModels = viewModel.WidgetViewModels;

            for (int index = 0, count = widgetViewModels.Count; index < count; index++)
            {
                OnAddWidgetViewModel(index, widgetViewModels[index]);
            }

            widgetViewModels
                .ObserveAdd(destroyCancellationToken)
                .Subscribe(this, static (addEvent, binder) => binder.OnAddWidgetViewModel(addEvent.Index, addEvent.Value));

            widgetViewModels
                .ObserveRemove(destroyCancellationToken)
                .Subscribe(this, static (removeEvent, binder) => binder.OnRemoveWidgetViewModel(removeEvent.Index));

            widgetViewModels
                .ObserveClear(destroyCancellationToken)
                .Subscribe(this, (_, binder) => binder.OnClearWidgetViewModels());
        }

        private void OnAddWidgetViewModel(int index, WidgetViewModel widgetViewModel)
        {
            var widgetBinder = CreateWidgetBinder(widgetViewModel);

            _widgetBinders.Insert(index, widgetBinder);
        }

        private void OnRemoveWidgetViewModel(int index)
        {
            var widgetBinder = _widgetBinders[index];

            _widgetBinders.RemoveAt(index);

            DestroyWidgetBinder(widgetBinder);
        }

        private void OnClearWidgetViewModels()
        {
            foreach (var widgetBinder in _widgetBinders)
            {
                DestroyWidgetBinder(widgetBinder);
            }

            _widgetBinders.Clear();
        }

        private WidgetBinder CreateWidgetBinder(WidgetViewModel widgetViewModel)
        {
            var widgetBinderTemplate = _widgetBinderMap[widgetViewModel.ViewId];
            var widgetBinder = Instantiate(widgetBinderTemplate, transform);

            widgetBinder.gameObject.SetActive(true);
            widgetBinder.SetViewModel(widgetViewModel);

            return widgetBinder;
        }

        // TODO: Добавить пулинг
        private void DestroyWidgetBinder(WidgetBinder widgetBinder)
        {
            Destroy(widgetBinder.gameObject);
        }

        [UsedImplicitly]
        private void Awake()
        {
            _widgetBinders = new List<WidgetBinder>();
            _widgetBinderMap = _widgetBinderTemplates.ToDictionary(static template => template.Key, static template => template.Binder, StringComparer.Ordinal);
        }

        [UsedImplicitly]
        private void OnDestroy()
        {
            _widgetBinders.Clear();
            _widgetBinders = null;

            _widgetBinderMap.Clear();
            _widgetBinderMap = null;
        }
    }
}
