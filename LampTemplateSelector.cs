using System.Windows;
using System.Windows.Controls;

namespace WpfApp10
{
    public class LampTemplateSelector : DataTemplateSelector 
    {
        public DataTemplate LanternTemplate { get; set; }
        public DataTemplate TableLampTemplate { get; set; }
        public DataTemplate ChandelierTemplate { get; set; }
        public DataTemplate FloorLampTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is LanternViewModel)
                return LanternTemplate;
            else if (item is TableLampViewModel)
                return TableLampTemplate;
            else if (item is ChandelierViewModel)
                return ChandelierTemplate;
            else if (item is FloorLampViewModel) 
                return FloorLampTemplate;


            else
                return base.SelectTemplate(item, container);
        }
    }
}
//LampTemplateSelector is responsible for choosing the correct DataTemplate to display each item in the LightingFixtures collection based on its type