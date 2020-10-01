using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TravelRecordApp.Model;

namespace TravelRecordApp.ViewModel.Command
{
    public class SaveCommand : ICommand
    {
        public NewTravelVM ViewModel { get; set; }

        public SaveCommand(NewTravelVM viewModel)
        {
            ViewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var post =(Post) parameter;

            if (post != null)
            {
                if (string.IsNullOrEmpty(post.Experience))
                {
                    return false;
                }

                if (post.Venue != null)
                {
                    return true;
                }

                return false;

            }

            return false;
        }

        public void Execute(object parameter)
        {
            Post post = (Post)parameter;
            ViewModel.Save(post);
        }
    }
}
