using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe_Database.Model
{
    public class Recipe : ViewModelBase
    {
        private string _name;
        private string _type;
        private string _origin;
        private string _ingredients;
        private string _procedure;
        private string _notes;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                RaisePropertyChanged();
            }
        }

        public string Origin
        {
            get { return _origin; }
            set
            {
                _origin = value;
                RaisePropertyChanged();
            }
        }

        public string Ingredients
        {
            get { return _ingredients; }
            set
            {
                _ingredients = value;
                RaisePropertyChanged();
            }
        }

        public string Procedure
        {
            get { return _procedure; }
            set
            {
                _procedure = value;
                RaisePropertyChanged();
            }
        }

        public string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                RaisePropertyChanged();
            }
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
