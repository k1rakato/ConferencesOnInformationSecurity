using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferencesOnInformationSecurity.Assets
{
    internal class ImageConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not string filePath) return null;

            if (!File.Exists(Path.GetFullPath(Environment.CurrentDirectory + @"\Image\" + filePath)) || string.IsNullOrEmpty(filePath))
            {
                return new Bitmap(Path.GetFullPath(Environment.CurrentDirectory + "\\Image\\заглушка.png"));
            }
            else
            {
                return new Bitmap(Path.GetFullPath(Environment.CurrentDirectory + @"\Image\" + filePath));
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
