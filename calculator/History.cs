using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace calculator
{
    public class Operations
    {
        public float _fstoperand { get; set; }
        public float _sndoperand { get; set; }

        public Operators _opt_enum { get; set; }

        public float _result { get; set; }

    }

    public class History
    {
        public ObservableCollection<Operations> Operation { get; set; }
        public History()
        {
            Operation = new ObservableCollection<Operations>();
        }

        public void AddOperation(Operations operation)
        {
            Operation.Insert(0, operation);

            if (Operation.Count >= 20)
            {
                Operation.RemoveAt(Operation.Count - 1);
            }
        }


    }

    public class OperatorToSignConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Operators op)
            {
                return op switch
                {
                    Operators.PLUS => "+",
                    Operators.MINUS => "-",
                    Operators.DIV => "/",
                    Operators.MUL => "*",
                    _ => string.Empty
                };
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
