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

namespace calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
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
        private float sndOperandflt { get; set; }



        private bool isFloat { get; set; } = false;

        public MainWindow()
        {
            InitializeComponent();

            fstOperand = new List<char>();
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            HandleInput(button.Content.ToString());
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                HandleInput((e.Key - Key.NumPad0).ToString());
            }

            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                HandleInput((e.Key - Key.D0).ToString());
            }

            if (e.Key == Key.Decimal || e.Key == Key.OemPeriod)
            {
                HandleInput(".");
            }

            if (e.Key == Key.Add)
            {
                HandleInput("+");
            }
            else if (e.Key == Key.Subtract)
            {
                HandleInput("-");
            }
            else if (e.Key == Key.Multiply)
            {
                HandleInput("*");
            }
            else if (e.Key == Key.Divide)
            {
                HandleInput("/");
            }

            if (e.Key == Key.Enter)
            {
                fstOperandflt = float.Parse(string.Join("", fstOperand));
                calculate();
            }
            if (e.Key == Key.Back)
            {
                backspacefunction();
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
                fstOperandShow();
            }

            else if (input == "+" || input == "-" || input == "*" || input == "/")
            {
                switch (input)
                {
                    case "+": _operators = Operators.PLUS; break;
                    case "-": _operators = Operators.MINUS; break;
                    case "/": _operators = Operators.DIV; break;
                    case "*": _operators = Operators.MUL; break;
                    default: break;
                }
                optsign.Text = input;
                fstOperandflt = float.Parse(string.Join("", fstOperand));
                fstOperand.Clear();
                sndOperandflt = fstOperandflt;
                resulttext.Text = fstOperandflt.ToString();
                inputtext.Text = string.Empty;
                operatorON = true;
            }

            if (input == "back")
            {
                backspacefunction();
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
            if (_operators == Operators.PLUS)
            {
                resulttext.Text = sndOperandflt.ToString() + " + " + fstOperandflt.ToString();
                inputtext.Text = (fstOperandflt + sndOperandflt).ToString();
            }
            else if (_operators == Operators.MINUS)
            {
                resulttext.Text = sndOperandflt.ToString() + " - " + fstOperandflt.ToString();
                inputtext.Text = (sndOperandflt - fstOperandflt).ToString();
            }

            else if (_operators == Operators.DIV)
            {
                resulttext.Text = sndOperandflt.ToString() + " / " + fstOperandflt.ToString();
                inputtext.Text = (sndOperandflt / fstOperandflt).ToString();
            }

            else if (_operators == Operators.MUL)
            {
                resulttext.Text = sndOperandflt.ToString() + " * " + fstOperandflt.ToString();
                inputtext.Text = (fstOperandflt * sndOperandflt).ToString();
            }
            optsign.Text = "=";
        }

        private void fstOperandShow()
        {
            inputtext.Text = string.Empty;
            foreach (char digit in fstOperand)
            {
                inputtext.Text += digit.ToString();
            }
        }

    }
}