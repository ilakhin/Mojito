using System.Collections.Generic;
using ObservableCollections;

namespace IL.Mojito
{
    public sealed class NavigationViewModel : BaseViewModel
    {
        private readonly ObservableList<SectionViewModel> _sectionViewModels = new();

        public IReadOnlyObservableList<SectionViewModel> SectionViewModels => _sectionViewModels;

        public void SetSectionViewModels(IEnumerable<SectionViewModel> sectionViewModels)
        {
            _sectionViewModels.Clear();
            _sectionViewModels.AddRange(sectionViewModels);
        }
    }
}
