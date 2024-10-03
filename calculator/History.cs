using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace calculator
{
    public class Operations
    {
        public float _fstoperand { get; set; }
        public float _sndoperand { get; set; }

        public Operators _opt { get; set; }

        public float _result { get; set; }

    }

    public class OperationView
    {
        public string lefthandside { get; set; }
        public string righthandside { get; set; }
    }
    public class History
    {
        public List<Operations> Operation { get; set; }
        public ObservableCollection<OperationView> OperationViews { get; set; }
        public History()
        {
            Operation = new List<Operations>();
            OperationViews = new ObservableCollection<OperationView>();
        }

        public void AddOperation(Operations operation)
        {
            string lhs = $"{operation._sndoperand} {operation._opt} {operation._fstoperand}";
            string rhs = $"{operation._result}";

            OperationView view = new OperationView()
            {
                lefthandside = lhs,
                righthandside = rhs
            };

            Operation.Add(operation);
            OperationViews.Add(view);

        }


    }


}
