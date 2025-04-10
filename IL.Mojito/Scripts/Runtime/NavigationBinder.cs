using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using ObservableCollections;
using R3;
using UnityEngine;

namespace IL.Mojito
{
    public sealed class NavigationBinder : BaseBinder<NavigationViewModel>
    {
        [SerializeField]
        private SectionBinder[] _sectionBinderTemplates;

        private List<SectionBinder> _sectionBinders;
        private Dictionary<string, SectionBinder> _sectionBinderMap;

        // TODO: Добавить очистку при смене ViewModel
        protected override void OnViewModelChanged(NavigationViewModel viewModel, ICollection<IDisposable> disposables)
        {
            var sectionViewModels = viewModel.SectionViewModels;

            for (int index = 0, count = sectionViewModels.Count; index < count; index++)
            {
                OnAddSectionViewModel(index, sectionViewModels[index]);
            }

            sectionViewModels
                .ObserveAdd(destroyCancellationToken)
                .Subscribe(this, static (addEvent, binder) => binder.OnAddSectionViewModel(addEvent.Index, addEvent.Value));

            sectionViewModels
                .ObserveRemove(destroyCancellationToken)
                .Subscribe(this, static (removeEvent, binder) => binder.OnRemoveSectionViewModel(removeEvent.Index));

            sectionViewModels
                .ObserveClear(destroyCancellationToken)
                .Subscribe(this, (_, binder) => binder.OnClearSectionViewModels());
        }

        private void OnAddSectionViewModel(int index, SectionViewModel sectionViewModel)
        {
            var sectionBinder = CreateSectionBinder(sectionViewModel);

            _sectionBinders.Insert(index, sectionBinder);
        }

        private void OnRemoveSectionViewModel(int index)
        {
            var sectionBinder = _sectionBinders[index];

            _sectionBinders.RemoveAt(index);

            DestroySectionBinder(sectionBinder);
        }

        private void OnClearSectionViewModels()
        {
            foreach (var sectionBinder in _sectionBinders)
            {
                DestroySectionBinder(sectionBinder);
            }

            _sectionBinders.Clear();
        }

        private SectionBinder CreateSectionBinder(SectionViewModel sectionViewModel)
        {
            var sectionBinderTemplate = _sectionBinderMap[sectionViewModel.ViewId];
            var sectionBinder = Instantiate(sectionBinderTemplate, transform);

            sectionBinder.gameObject.SetActive(true);
            sectionBinder.SetViewModel(sectionViewModel);

            return sectionBinder;
        }

        // TODO: Добавить пулинг
        private void DestroySectionBinder(SectionBinder sectionBinder)
        {
            Destroy(sectionBinder.gameObject);
        }

        [UsedImplicitly]
        private void Awake()
        {
            _sectionBinders = new List<SectionBinder>();
            _sectionBinderMap = _sectionBinderTemplates.ToDictionary(static binder => binder.ViewId, static binder => binder, StringComparer.Ordinal);
        }

        [UsedImplicitly]
        private void OnDestroy()
        {
            _sectionBinders.Clear();
            _sectionBinders = null;

            _sectionBinderMap.Clear();
            _sectionBinderMap = null;
        }
    }
}
