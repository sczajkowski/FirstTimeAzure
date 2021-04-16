using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace xaml_api_db
{
    public class MainViewModel : INotifyPropertyChanged
    {
        string bfirstname = string.Empty;
        string blastname = string.Empty;
        string bphonenumber= string.Empty;
        public string BFirstName
        {
            get => bfirstname;
            set
            {
                if (bfirstname == value)
                    return;
                bfirstname = value;
                OnPropertyChanged(nameof(BFirstName));
                OnPropertyChanged(nameof(DisplayFirstName));
            }
        }
        public string BLastName
        {
            get => blastname;
            set
            {
                if (blastname == value)
                    return;
                blastname = value;
                OnPropertyChanged(nameof(BLastName));
                OnPropertyChanged(nameof(DisplayLastName));
            }
        }
        public string BPhoneNumber
        {
            get => bphonenumber;
            set
            {
                if (bphonenumber == value)
                    return;
                bphonenumber = value;
                OnPropertyChanged(nameof(BPhoneNumber));
                OnPropertyChanged(nameof(DisplayPhoneNumber));
            }
        }
        public string DisplayFirstName => $"Name entered:{BFirstName}";
        public string DisplayLastName => $"Surname entered:{BLastName}";
        public string DisplayPhoneNumber => $"Number entered:{BPhoneNumber}";


        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
