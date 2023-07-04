using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfAppVsNet4
{
    public class Person
    {
        public int Id { get; set; }

        public string PersonName { get; set; }
        public string FirstName { get; set; }

        public Person()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                Id = 5;
                FirstName = "yang";
                PersonName = "yang zhi nong";
            }
            else
            {
                Id = 1;
                FirstName = "杨";
                PersonName = "杨志农";
            }
        }
    }
}