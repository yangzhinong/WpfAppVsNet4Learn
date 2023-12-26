using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace RxWinform
{
    public partial class Form1 : Form,IViewFor<PersonViewModel>
    {
        public Form1()
        {
            InitializeComponent();

            this.WhenActivated(a =>
            {
                this.OneWayBind(ViewModel, vm => vm.Id, v => v.textBox1.Text).DisposeWith(a);
                this.OneWayBind(ViewModel, vm => vm.Name, v => v.textBox1.Text).DisposeWith(a);
                //a(this.Bind(ViewModel, vm => vm.Id, v => v.textBox1.Text));
                //a(this.Bind(ViewModel, vm => vm.Name, v => v.textBox2.Text));
                //a(this.Bind(ViewModel, vm => vm.Age, v => v.textBox3.Text));

                //a(this.OneWayBind(ViewModel, vm=>vm.Id, v=>v.label1.Text));
                //a(this.OneWayBind(ViewModel, vm=>vm.Name, v=>v.label2.Text));
                //a(this.OneWayBind(ViewModel, vm=>vm.Age, v=>v.label3.Text));
            });

            ViewModel = new PersonViewModel();
        }

        public PersonViewModel ViewModel { get; set; }
        object IViewFor.ViewModel { get=> ViewModel; set => ViewModel= (PersonViewModel)value; }

        private void Form1_Load(object sender, EventArgs e)
        {
            var check1Changed = Observable.FromEventPattern<EventArgs>(this.checkBox1, nameof(CheckBox.CheckedChanged));
            var check2Changed = Observable.FromEventPattern<EventArgs>(this.checkBox2, nameof(CheckBox.CheckedChanged));
            var txtKeyPress = Observable.FromEventPattern<KeyPressEventArgs>(this.textBox4, nameof(TextBox.KeyPress));

            var plan1 = check1Changed.And(check2Changed).And(txtKeyPress)
                            .Then((c1, c2, txt) => "Done");

            var when = Observable.When(plan1);

            when.Subscribe(r => this.txtResult.Text = r);

        }
    }
}
