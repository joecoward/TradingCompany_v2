using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DTO;
using TradingCompany.WPF.Core;

namespace TradingCompany.WPF.Models
{
    public class ActionModel: ViewModelBase, IDataErrorInfo
    {
        private int _id;
        private string _name;
        private string _description;
        private DateTime _startDate;
        private DateTime _endDate;
        private int _statusId;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }
        public int StatusId
        {
            get => _statusId;
            set
            {
                _statusId = value;
                OnPropertyChanged();
            }
        }
        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                switch (columnName)
                {
                    case nameof(Name):
                        if (string.IsNullOrWhiteSpace(Name))
                        {
                            result = "Name cannot be empty.";
                        }
                        break;
                    case nameof(StartDate):
                        if (StartDate > EndDate)
                        {
                            result = "Start Date cannot be later than End Date.";
                        }
                        break;
                    case nameof(EndDate):
                        if (EndDate < StartDate)
                        {
                            result = "End Date cannot be earlier than Start Date.";
                        }
                        break;
                    case nameof(StatusId):
                        if (StatusId <= 0)
                        {
                            result = "Status must be selected.";
                        }
                        break;
                }
                return result;
            }
        }

        public ActionDTO ToDto()
        {
            return new ActionDTO
            {
                ActionId = this.Id,
                Name = this.Name,
                Description = this.Description,
                StartDate = this.StartDate,
                EndDate = this.EndDate,
                Status = new StatusDTO { StatusId = this.StatusId }
            };
        }
        public bool IsValid => string.IsNullOrEmpty(this[nameof(Name)]) &&
                       string.IsNullOrEmpty(this[nameof(StartDate)]) &&
                       string.IsNullOrEmpty(this[nameof(EndDate)]) &&
                       string.IsNullOrEmpty(this[nameof(StatusId)]);
    }
}
