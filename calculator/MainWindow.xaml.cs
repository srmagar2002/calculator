using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;

namespace calculator
{
    public enum Operators
    {
        PLUS,
        MINUS,
        DIV,
        MUL
    }
    public partial class MainWindow : Window
    {
        private Operators _operators { get; set; }
        private bool operatorON { get; set; } = false;

        private List<char> fstOperand { get; set; }

        private float fstOperandflt { get; set; } = 0;
        private float sndOperandflt { get; set; } = 0;
        private float result { get; set; }
        private bool forcedOperation { get; set; } = false;

        private bool isFloat { get; set; } = false;

        private bool enterCalc { get; set; } = false;

        static History history = new History();

        static Operations opt;

        public MainWindow()
        {
            InitializeComponent();

            fstOperand = new List<char>();

            this.DataContext = history;
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            string input = button.Content.ToString();

            if (int.TryParse(input, out _))
            {

                if (operatorON)
                {
                    fstOperand.Clear();
                    inputtext.Text = string.Empty;
                    operatorON = false;
                    forcedOperation = true;
                }

                if (enterCalc)
                {
                    optsign.Text = string.Empty;
                    fstOperand.Clear();
                    sndOperandflt = 0;
                    resulttext.Text = string.Empty;
                    inputtext.Text = string.Empty;
                    enterCalc = false;

                }

            }

            HandleInput(input);
        }

        private void clear()
        {
            fstOperand = new List<char>();
            fstOperandflt = 0;
            sndOperandflt = 0;
            resulttext.Text = string.Empty;
            inputtext.Text = string.Empty;
            optsign.Text = string.Empty;
            operatorON = false;
            forcedOperation = false;
            enterCalc = false;
            isFloat = false;
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            clickeffect(e);

            if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                if (operatorON)
                {
                    fstOperand.Clear();
                    inputtext.Text = string.Empty;
                    operatorON = false;
                    forcedOperation = true;
                }

                if (enterCalc)
                {
                    optsign.Text = string.Empty;
                    fstOperand.Clear();
                    sndOperandflt = 0;
                    resulttext.Text = string.Empty;
                    inputtext.Text = string.Empty;
                    enterCalc = false;

                }

                HandleInput((e.Key - Key.NumPad0).ToString());
                e.Handled = true;
            }

            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                if (operatorON)
                {
                    fstOperand.Clear();
                    inputtext.Text = string.Empty;
                    operatorON = false;
                    forcedOperation = true;
                }

                if (enterCalc)
                {
                    optsign.Text = string.Empty;
                    fstOperand.Clear();
                    sndOperandflt = 0;
                    resulttext.Text = string.Empty;
                    inputtext.Text = string.Empty;
                    enterCalc = false;

                }
                HandleInput((e.Key - Key.D0).ToString()); e.Handled = true;

            }

            if (e.Key == Key.Decimal || e.Key == Key.OemPeriod)
            {
                if (operatorON)
                {
                    fstOperand.Clear();
                    inputtext.Text = string.Empty;
                    operatorON = false;
                    forcedOperation = true;
                }

                if (enterCalc)
                {
                    optsign.Text = string.Empty;
                    fstOperand.Clear();
                    sndOperandflt = 0;
                    resulttext.Text = string.Empty;
                    inputtext.Text = string.Empty;
                    enterCalc = false;

                }
                HandleInput("."); e.Handled = true;
            }

            if (e.Key == Key.Add)
            {
                HandleInput("+"); e.Handled = true;
            }
            else if (e.Key == Key.Subtract)
            {
                HandleInput("-"); e.Handled = true;
            }
            else if (e.Key == Key.Multiply)
            {
                HandleInput("*"); e.Handled = true;
            }
            else if (e.Key == Key.Divide)
            {
                HandleInput("/"); e.Handled = true;
            }

            if (e.Key == Key.Enter)
            {
                HandleInput("=");
                e.Handled = true;

            }

            if (e.Key == Key.Back)
            {
                backspacefunction(); e.Handled = true;
            }
            if (e.Key == Key.C)
            {
                HandleInput("CLS"); e.Handled = true;
            }
        }


        private void HandleInput(string input)
        {
            int inputLength = isFloat ? 17 : 16;

            if (input == "." && isFloat == false)
            {
                isFloat = true;
                fstOperand.Add('.');
                fstOperandShow();
            }

            else if (fstOperand.Count < inputLength && int.TryParse(input, out _))
            {
                fstOperand.Add(char.Parse(input));
                fstOperandflt = float.Parse(string.Join("", fstOperand));
                fstOperandShow();

            }

            else if (input == "+" || input == "-" || input == "*" || input == "/")
            {
                enterCalc = false;

                isFloat = false;

                if (forcedOperation)
                {
                    calculate();
                }

                switch (input)
                {
                    case "+": _operators = Operators.PLUS; break;
                    case "-": _operators = Operators.MINUS; break;
                    case "/": _operators = Operators.DIV; break;
                    case "*": _operators = Operators.MUL; break;
                    default: break;
                }

                optsign.Text = input;

                if (!operatorON)
                {
                    if (fstOperand.Count != 0)
                    {
                        fstOperandflt = float.Parse(string.Join("", fstOperand));
                        sndOperandflt = fstOperandflt;
                        resulttext.Text = fstOperandflt.ToString();
                        operatorON = true;
                    }
                    else
                    {
                        calculate();
                    }
                }
            }
            if (input == "=")
            {
                calculate();
                enterCalc = true;
            }
            if (input == "back")
            {
                backspacefunction();
            }

            if (input == "CLS")
            {
                clear();
            }
        }

        private void backspacefunction()
        {
            if (fstOperand.Count > 0)
            {
                fstOperand.RemoveAt(fstOperand.Count - 1);
            }

            fstOperandShow();


        }
        private void calculate()
        {

            if (history.Operation.Count != 0)
            {
                if (history.Operation[0]._fstoperand == fstOperandflt &&
                    history.Operation[0]._sndoperand == sndOperandflt &&
                    history.Operation[0]._opt_enum == _operators)
                {

                    sndOperandflt = result;

                    main_operation();
                }
                else
                {
                    main_operation();
                }
            }
            else
            {
                main_operation();
            }

            opt = new Operations() { _fstoperand = fstOperandflt, _sndoperand = sndOperandflt, _opt_enum = _operators, _result = result };
            history.AddOperation(opt);

            //2+3=5
        }

        private void main_operation()
        {
            result = 0;
            char operat = ' ';
            if (_operators == Operators.PLUS)
            {
                resulttext.Text = sndOperandflt.ToString() + " + " + fstOperandflt.ToString();
                result = (fstOperandflt + sndOperandflt);
            }
            else if (_operators == Operators.MINUS)
            {
                resulttext.Text = sndOperandflt.ToString() + " - " + fstOperandflt.ToString();
                result = (sndOperandflt - fstOperandflt);
            }

            else if (_operators == Operators.DIV)
            {
                resulttext.Text = sndOperandflt.ToString() + " / " + fstOperandflt.ToString();
                result = (sndOperandflt / fstOperandflt);


            }

            else if (_operators == Operators.MUL)
            {
                resulttext.Text = sndOperandflt.ToString() + " * " + fstOperandflt.ToString();
                result = fstOperandflt * sndOperandflt;

            }



            inputtext.Text = result.ToString();

            fstOperand = result.ToString().ToList();

            optsign.Text = "=";
            forcedOperation = false;
            isFloat = false;
            operatorON = false;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = sender as ListView;

            var selectedItem = listView.SelectedItem;

            if (selectedItem != null)
            {

                var operation = selectedItem as Operations;

                if (operation != null)
                {
                    sndOperandflt = operation._sndoperand;
                    fstOperandflt = operation._fstoperand;
                    _operators = operation._opt_enum;
                    main_operation();
                }
            }
        }

        private void fstOperandShow()
        {
            inputtext.Text = string.Empty;
            foreach (char digit in fstOperand)
            {
                inputtext.Text += digit.ToString();
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.NumPad7:
                case Key.D7:
                    button7.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3C3C36"));
                    break;

                case Key.NumPad8:
                case Key.D8:
                    button8.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3C3C36"));
                    break;


                case Key.NumPad9:
                case Key.D9:
                    button9.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3C3C36"));
                    break;


                case Key.NumPad4:
                case Key.D4:
                    button4.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3C3C36"));
                    break;


                case Key.NumPad5:
                case Key.D5:
                    button5.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3C3C36"));
                    break;


                case Key.NumPad6:
                case Key.D6:
                    button6.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3C3C36"));
                    break;


                case Key.NumPad1:
                case Key.D1:
                    button1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3C3C36"));
                    break;


                case Key.NumPad2:
                case Key.D2:
                    button2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3C3C36"));
                    break;


                case Key.NumPad3:
                case Key.D3:
                    button3.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3C3C36"));
                    break;


                case Key.NumPad0:
                case Key.D0:
                    button0.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3C3C36"));
                    break;


                case Key.Decimal:
                case Key.OemPeriod:
                    buttonPeriod.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3C3C36"));
                    break;


                //signs and others
                case Key.Add:
                    plus.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#32332D"));
                    break;

                case Key.Subtract:
                    minus.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#32332D"));
                    break;

                case Key.Divide:
                    div.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#32332D"));
                    break;

                case Key.Multiply:
                    mul.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#32332D"));
                    break;

                case Key.Back:
                    back.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#32332D"));
                    break;

                case Key.C:
                    cls.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#32332D"));
                    break;

                case Key.Enter:
                    enter.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4CC2FF"));
                    break;
            }
        }



        private void clickeffect(KeyEventArgs e)
        {
            switch (e.Key)
            {
                //numbers

                case Key.NumPad7:
                case Key.D7:
                    button7.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#32332D"));
                    break;

                case Key.NumPad8:
                case Key.D8:
                    button8.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#32332D"));
                    break;


                case Key.NumPad9:
                case Key.D9:
                    button9.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#32332D"));
                    break;


                case Key.NumPad4:
                case Key.D4:
                    button4.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#32332D"));
                    break;


                case Key.NumPad5:
                case Key.D5:
                    button5.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#32332D"));
                    break;


                case Key.NumPad6:
                case Key.D6:
                    button6.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#32332D"));
                    break;


                case Key.NumPad1:
                case Key.D1:
                    button1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#32332D"));
                    break;


                case Key.NumPad2:
                case Key.D2:
                    button2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#32332D"));
                    break;


                case Key.NumPad3:
                case Key.D3:
                    button3.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#32332D"));
                    break;


                case Key.NumPad0:
                case Key.D0:
                    button0.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#32332D"));
                    break;


                case Key.Decimal:
                case Key.OemPeriod:
                    buttonPeriod.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#32332D"));
                    break;


                //signs and others
                case Key.Add:
                    plus.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3C3C36"));
                    break;

                case Key.Subtract:
                    minus.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3C3C36"));
                    break;

                case Key.Divide:
                    div.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3C3C36"));
                    break;

                case Key.Multiply:
                    mul.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3C3C36"));
                    break;

                case Key.Back:
                    back.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3C3C36"));
                    break;

                case Key.C:
                    cls.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3C3C36"));
                    break;

                case Key.Enter:
                    enter.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#48B1E8"));
                    break;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double window_width = e.NewSize.Width;
            double window_height = e.NewSize.Height;

            //ran.Text = $" width = {window_width}, height = {window_height}";

            var historyStyle = (Style)Resources["HistoryOperandsStyle"];
            var resultStyle = (Style)Resources["ResultOperandsStyle"];

            if (window_width >= 750 && window_height >= 600)
            {
                inputtext.FontSize = 60;
                resulttext.FontSize = 35;
                optsign.FontSize = 30;



            }
            if (window_width < 750 || window_height < 600)
            {
                inputtext.FontSize = 50;
                resulttext.FontSize = 25;
                optsign.FontSize = 20;
            }

            if (window_width >= 1000 && window_height >= 1000)
            {
                inputtext.FontSize = 80;
                resulttext.FontSize = 55;
                optsign.FontSize = 50;
                resulttext.Margin = new Thickness(2, 2, 40, 2);
            }
            if (window_width < 1000 || window_height < 1000)
            {
                inputtext.FontSize = 60;
                resulttext.FontSize = 35;
                optsign.FontSize = 30;
                resulttext.Margin = new Thickness(2, 2, 23, 2);
            }


            if (window_width < 600)
            {
                calc_grid.ColumnDefinitions[4].Width = new GridLength(0);
                calc_grid.ColumnDefinitions[5].Width = new GridLength(0);
            }

            if (window_width >= 600 && window_width < 1100)
            {
                calc_grid.ColumnDefinitions[4].Width = new GridLength(100);
                calc_grid.ColumnDefinitions[5].Width = new GridLength(100);
            }


            if (window_width > 1100)
            {
                calc_grid.ColumnDefinitions[4].Width = new GridLength(200);
                calc_grid.ColumnDefinitions[5].Width = new GridLength(200);
            }


        }

        private void ClearHistory_Click(object sender, RoutedEventArgs e)
        {
            history.Operation.Clear();
            clear();
        }
    }
}