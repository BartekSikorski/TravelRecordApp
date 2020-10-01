using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TravelRecordApp.Model;

namespace TravelRecordApp.ViewModel.Command
{
    public class RegisterCommand : ICommand
    {
        private RegisterVM viewModel;
        public event EventHandler CanExecuteChanged;

        public RegisterCommand(RegisterVM viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            var user = (User)parameter;
            if (user != null)
            {
                if (user.ConfirmPassword == user.Password)
                {
                    if (string.IsNullOrEmpty(user.Email) || string.IsNullOrWhiteSpace(user.Password))
                    {
                        return false;
                    }
                    return true;
                }
                return false;
            }

            return false;
        }

        public void Execute(object parameter)
        {
            var user = (User)parameter;

            viewModel.Register(user);
        }
    }
}
