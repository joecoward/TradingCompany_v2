using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TradingCompany.DALEF.Concrete;
using TradingCompany.DALEF.interfaces;
using TradingCompany.DTO;
using TradingCompany.WPF.Core;
using TradingCompany.WPF.Models;

namespace TradingCompany.WPF.ViewModels
{
    public class ActionEditorViewModel : ViewModelBase
    {
        private readonly IActionDal _actionDal;
        private readonly IStatusDal _statusDal;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public Action CloseRequest;

        public ActionModel Action { get; set; }
        public ObservableCollection<StatusDTO> Statuses { get; set; } = new ObservableCollection<StatusDTO>();
        public ActionEditorViewModel(IActionDal actionDal, IStatusDal statusDal)
        {
            _actionDal = actionDal;
            _statusDal = statusDal;
            SaveCommand = new RelayCommand(ExecuteSave);
            CancelCommand = new RelayCommand(obj => CloseRequest?.Invoke());
            LoadStatuses();
        }

        private void ExecuteSave(object obj)
        {
            if (!Action.IsValid)
            {
                System.Windows.MessageBox.Show("Please correct the errors before saving the action.", "Validation Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return;
            }
            try
            {
                var dto = Action.ToDto();
                if(dto.ActionId==0)
                {
                    _actionDal.Create(dto);
                }
                else
                {
                    _actionDal.Update(dto);
                }
                CloseRequest?.Invoke();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"An error occurred while saving the action: {ex.Message}", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }
        public void SetActionToEdit(ActionDTO dto)
        {
            Action = new ActionModel
            {
                Id = dto.ActionId,
                Name = dto.Name,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                StatusId = dto.Status?.StatusId ?? 0
            };
            OnPropertyChanged(nameof(Action));
        }

        private void LoadStatuses()
        {
            var statuses = _statusDal.GetAll();
            foreach (var item in statuses)
            {
                Statuses.Add(item);
            }
        }
    }
}
