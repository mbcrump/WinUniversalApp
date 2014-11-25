using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace TipAndShare
{
    public class TipAndShareViewModel : INotifyPropertyChanged
    {

        private double _totalMealCost;

        public double TotalMealCost
        {
            get { return _totalMealCost; }
            set
            {
                _totalMealCost = value;
                CalculateSummaryInformation();
                this.RaisePropertyChanged("TotalMealCost");
            }
        }

        private int _numberofDiners;

        public int NumberOfDiners
        {
            get { return _numberofDiners; }
            set
            {
                _numberofDiners = value;
                CalculateSummaryInformation();
                this.RaisePropertyChanged("NumberOfDiners");
            }
        }

        private int _tipPercentage;

        public int TipPercentage
        {
            get { return _tipPercentage; }
            set
            {
                _tipPercentage = value;
                CalculateSummaryInformation();
                this.RaisePropertyChanged("TipPercentage");
            }
        }

        private string _summaryInformation;

        public string SummaryInformation
        {
            get { return _summaryInformation; }
            set
            {
                _summaryInformation = value;
                this.RaisePropertyChanged("SummaryInformation");
            }
        }

        public ICommand ChangeTipCommand { get; set; }


        public TipAndShareViewModel()
        {
            this._totalMealCost = 15.00;
            this._tipPercentage = 15;
            this._numberofDiners = 1;
            this.ChangeTipCommand = new DelegateCommand(ChangeTipPercentage);
            this.CalculateSummaryInformation();
        }

        protected void ChangeTipPercentage(object parameter)
        {
            if (parameter == null)
                return;
            int percentage = Convert.ToInt32(parameter);
            this.TipPercentage = percentage;
        }

        protected void CalculateSummaryInformation()
        {
            double tipAmount = _totalMealCost * _tipPercentage / 100;
            double dinerPay = (_totalMealCost + tipAmount) / _numberofDiners;

            StringBuilder summary = new StringBuilder();
            summary.Append("At ")
                    .Append(_tipPercentage)
                    .Append("% the tip is ")
                    .Append(tipAmount.ToString("C2"))
                    .Append(" Each diner should pay ")
                    .Append(dinerPay.ToString("C2"));

            this.SummaryInformation = summary.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
