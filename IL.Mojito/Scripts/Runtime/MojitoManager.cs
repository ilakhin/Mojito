using System;
using System.Collections.Generic;
using System.Linq;
using IL.Mojito.Controls;
using JetBrains.Annotations;
using R3;
using UnityEngine;

namespace IL.Mojito
{
    using static WidgetUtility;

    [DisallowMultipleComponent]
    public sealed class MojitoManager : MonoBehaviour
    {
        [SerializeField]
        private PanelBinder _panelBinderPrefab;

        private readonly PanelViewModel _panelViewModel = new();
        private readonly List<WidgetEntry> _widgetEntries = new();

        private string _currentPathFilter = string.Empty;

        public void AddWidgetViewModel(string path, int priority, WidgetViewModel widgetViewModel)
        {
            var widgetEntry = new WidgetEntry(path, priority, widgetViewModel);

            _widgetEntries.Add(widgetEntry);
        }

        public void SetPathFilter(string pathFilter)
        {
            pathFilter ??= string.Empty;

            if (string.Equals(pathFilter, _currentPathFilter, StringComparison.Ordinal))
            {
                return;
            }

            _currentPathFilter = pathFilter;

            // TODO
            Debug.Log($"Current PathFilter: {_currentPathFilter}");

            UpdateSections(pathFilter);
            UpdateWidgets(pathFilter);
        }

        private void UpdateSections(string pathFilter)
        {
            var sectionViewModels = GetSectionViewModels(pathFilter);

            _panelViewModel.NavigationViewModel.SetSectionViewModels(sectionViewModels);
        }

        private IEnumerable<SectionViewModel> GetSectionViewModels(string pathFilter)
        {
            yield return CreateSectionViewModel("Section_Root", "Root", string.Empty);

            foreach (var (label, subFilter) in SplitPath(pathFilter))
            {
                yield return CreateSectionViewModel("Section_Default", label, subFilter);
            }
        }

        private SectionViewModel CreateSectionViewModel(string viewId, string label, string pathFilter)
        {
            var clickSubject = new Subject<ButtonViewModel>();

            clickSubject.Subscribe((Manager: this, PathFilter: pathFilter), static (_, stateTuple) =>
            {
                var (manager, pathFilter) = stateTuple;

                manager.SetPathFilter(pathFilter);
            });

            var buttonViewModel = new ButtonViewModel(clickSubject);
            var textViewModel = new TextViewModel(new ReactiveProperty<string>(label));
            var sectionViewModel = new SectionViewModel(viewId, buttonViewModel, textViewModel);

            return sectionViewModel;
        }

        private static IEnumerable<(string Name, string SubPath)> SplitPath(string path)
        {
            var subPath = string.Empty;

            foreach (var name in path.Split('/'))
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    continue;
                }

                subPath = string.IsNullOrWhiteSpace(subPath) ? name : $"{subPath}/{name}";

                yield return (name, subPath);
            }
        }

        private void UpdateWidgets(string pathFilter)
        {
            var widgetViewModels = GetWidgetViewModels(pathFilter);

            _panelViewModel.ContentViewModel.SetWidgetViewModels(widgetViewModels);
        }

        private IEnumerable<WidgetViewModel> GetWidgetViewModels(string pathFilter)
        {
            var knownNames = new HashSet<string>();
            var widgetEntries = _widgetEntries.OrderBy(static widgetEntry => widgetEntry.Priority);

            foreach (var widgetEntry in widgetEntries)
            {
                var path = widgetEntry.Path;

                if (!path.StartsWith(pathFilter))
                {
                    continue;
                }

                if (string.Equals(widgetEntry.Path, pathFilter, StringComparison.Ordinal))
                {
                    yield return widgetEntry.WidgetViewModel;
                }
                else
                {
                    var startIndex = pathFilter.Length == 0 ? 0 : pathFilter.Length + 1;
                    var endIndex = path.IndexOf('/', startIndex);
                    var lenght = endIndex == -1 ? widgetEntry.Path.Length - startIndex : endIndex - startIndex;
                    var label = path.Substring(startIndex, lenght);

                    if (!knownNames.Add(label))
                    {
                        continue;
                    }

                    var subPathFilter = endIndex == -1 ? path : path[..endIndex];
                    var clickSubject = new Subject<ButtonViewModel>();

                    clickSubject.Subscribe((Manager: this, PathFilter: subPathFilter), static (_, stateTuple) =>
                    {
                        var (manager, pathFilter) = stateTuple;

                        manager.SetPathFilter(pathFilter);
                    });

                    yield return CreateSectionWidget(clickSubject, label);
                }
            }
        }

        [UsedImplicitly]
        private void Start()
        {
            var panelBinder = Instantiate(_panelBinderPrefab, transform);

            panelBinder.name = "Panel";
            panelBinder.gameObject.SetActive(true);
            panelBinder.SetViewModel(_panelViewModel);
        }
    }
}
