using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace WpfAppVsNet4
{
    public class EventToCommand : TriggerAction<DependencyObject>
    {
        public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register("Command", typeof(ICommand), typeof(EventToCommand),
        null);

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(EventToCommand),
        null);

        public bool IsPassSender
        {
            get { return (bool)GetValue(IsPassSenderProperty); }
            set { SetValue(IsPassSenderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsPassSender.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsPassSenderProperty =
            DependencyProperty.Register("IsPassSender", typeof(bool), typeof(EventToCommand), new PropertyMetadata(false));

        public bool PassEventArgsToCommand
        {
            get { return (bool)GetValue(PassEventArgsToCommandProperty); }
            set { SetValue(PassEventArgsToCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PassEventArgsToCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PassEventArgsToCommandProperty =
            DependencyProperty.Register("PassEventArgsToCommand", typeof(bool), typeof(EventToCommand), new PropertyMetadata(true));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            if (this.AssociatedObject != null)
            {
                ICommand command = this.Command;
                if (command != null)
                {
                    if (this.CommandParameter != null)
                    {
                        if (command.CanExecute(this.CommandParameter))
                        {
                            command.Execute(this.CommandParameter);
                        }
                    }
                    else
                    {
                        if (PassEventArgsToCommand)
                        {
                            if (command.CanExecute(parameter))
                            {
                                if (IsPassSender)
                                {
                                    command.Execute(new Tuple<object, object>(this.AssociatedObject, parameter));
                                }
                                else
                                {
                                    command.Execute(parameter);
                                }
                            }
                        }
                        else
                        {
                            command.Execute(null);
                        }
                    }
                }
            }
        }
    }
}