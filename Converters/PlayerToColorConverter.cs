using GK.Logic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace GK;

public class PlayerToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var converted = value as Player?;

        return new SolidColorBrush(converted switch
        {
            Player.Player1 => Consts.FirstPlayerColor,
            Player.Player2 => Consts.SecondPlayerColor,
            _ => Colors.Transparent,
        });
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
