using System.Collections.Generic;
using IL.Mojito.Controls;
using R3;

namespace IL.Mojito
{
    public static class WidgetUtility
    {
        public static WidgetViewModel CreateButtonWidget(TextViewModel headerTextViewModel, ButtonViewModel buttonViewModel, TextViewModel labelTextViewModel)
        {
            return CreateButtonWidget("ButtonWidget", headerTextViewModel, buttonViewModel, labelTextViewModel);
        }

        public static WidgetViewModel CreateButtonWidget(string viewId, TextViewModel headerTextViewModel, ButtonViewModel buttonViewModel, TextViewModel labelTextViewModel)
        {
            var contentViewModels = new Dictionary<string, ControlViewModel>
            {
                ["HeaderText"] = headerTextViewModel,
                ["Button"] = buttonViewModel,
                ["LabelText"] = labelTextViewModel
            };
            var widgetViewModel = new WidgetViewModel(viewId, contentViewModels);

            return widgetViewModel;
        }

        public static WidgetViewModel CreateDropdownWidget(TextViewModel headerTextViewModel, DropdownViewModel dropdownViewModel, ButtonViewModel buttonViewModel, TextViewModel labelTextViewModel)
        {
            return CreateDropdownWidget("DropdownWidget", headerTextViewModel, dropdownViewModel, buttonViewModel, labelTextViewModel);
        }

        public static WidgetViewModel CreateDropdownWidget(string viewId, TextViewModel headerTextViewModel, DropdownViewModel dropdownViewModel, ButtonViewModel buttonViewModel, TextViewModel labelTextViewModel)
        {
            var contentViewModels = new Dictionary<string, ControlViewModel>
            {
                ["HeaderText"] = headerTextViewModel,
                ["Dropdown"] = dropdownViewModel,
                ["Button"] = buttonViewModel,
                ["LabelText"] = labelTextViewModel
            };
            var widgetViewModel = new WidgetViewModel(viewId, contentViewModels);

            return widgetViewModel;
        }

        public static WidgetViewModel CreateInputFieldWidget(TextViewModel headerTextViewModel, InputFieldViewModel inputFieldViewModel, ButtonViewModel buttonViewModel, TextViewModel labelTextViewModel)
        {
            return CreateInputFieldWidget("InputFieldWidget", headerTextViewModel, inputFieldViewModel, buttonViewModel, labelTextViewModel);
        }

        public static WidgetViewModel CreateInputFieldWidget(string viewId, TextViewModel headerTextViewModel, InputFieldViewModel inputFieldViewModel, ButtonViewModel buttonViewModel, TextViewModel labelTextViewModel)
        {
            var contentViewModels = new Dictionary<string, ControlViewModel>
            {
                ["HeaderText"] = headerTextViewModel,
                ["InputField"] = inputFieldViewModel,
                ["Button"] = buttonViewModel,
                ["LabelText"] = labelTextViewModel
            };
            var widgetViewModel = new WidgetViewModel(viewId, contentViewModels);

            return widgetViewModel;
        }

        // TODO: internal
        public static WidgetViewModel CreateSectionWidget(Subject<ButtonViewModel> clickSubject, string label)
        {
            var contentViewModels = new Dictionary<string, ControlViewModel>
            {
                ["Button"] = new ButtonViewModel(clickSubject),
                ["LabelText"] = new TextViewModel(new ReactiveProperty<string>(label))
            };
            var widgetViewModel = new WidgetViewModel("SectionWidget", contentViewModels);

            return widgetViewModel;
        }

        public static WidgetViewModel CreateSliderWidget(TextViewModel headerTextViewModel, SliderViewModel sliderViewModel, ButtonViewModel buttonViewModel, TextViewModel labelTextViewModel)
        {
            return CreateSliderWidget("SliderWidget", headerTextViewModel, sliderViewModel, buttonViewModel, labelTextViewModel);
        }

        public static WidgetViewModel CreateSliderWidget(string viewId, TextViewModel headerTextViewModel, SliderViewModel sliderViewModel, ButtonViewModel buttonViewModel, TextViewModel labelTextViewModel)
        {
            var contentViewModels = new Dictionary<string, ControlViewModel>
            {
                ["HeaderText"] = headerTextViewModel,
                ["Slider"] = sliderViewModel,
                ["Button"] = buttonViewModel,
                ["LabelText"] = labelTextViewModel
            };
            var widgetViewModel = new WidgetViewModel(viewId, contentViewModels);

            return widgetViewModel;
        }
    }
}
