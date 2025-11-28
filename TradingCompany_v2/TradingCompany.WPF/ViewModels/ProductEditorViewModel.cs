using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TradingCompany.DALEF.interfaces;
using TradingCompany.DTO;
using TradingCompany.WPF.Core;
using TradingCompany.WPF.Models;

namespace TradingCompany.WPF.ViewModels
{
    public class ProductEditorViewModel : ViewModelBase
    {
        private readonly IProductDal _productDal;
        private readonly ICategoryDal _categoryDal;

        public ProductModel Product { get; set; }

        public ObservableCollection<CategoryDTO> Categories { get; set; } = new ObservableCollection<CategoryDTO>();

        public Action CloseRequest;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public ProductEditorViewModel(IProductDal productDal, ICategoryDal categoryDal)
        {
            _productDal = productDal;
            _categoryDal = categoryDal;
            SaveCommand = new RelayCommand(ExecuteSave);
            CancelCommand = new RelayCommand(obj=>CloseRequest?.Invoke());
            LoadCategories();
        }

        public void SetProductToEdit (ProductDTO Dto)
        {
            Product = new ProductModel
            {
               Id = Dto.ProductId,
                Name = Dto.Name,
                Price = Dto.Price,
                CategoryId = Dto.Category?.CategoryId ?? 0
            };
            OnPropertyChanged(nameof(Product));

        }


        private void ExecuteSave(object obj)
        {
           if(!Product.IsValid)
            {
                MessageBox.Show("Please correct the errors before saving the product.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var dto = Product.ToDTO();
                if (dto.ProductId == 0)
                {
                    _productDal.Create(dto);
                }
                else
                {
                    _productDal.Update(dto);
                }
                CloseRequest?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            }

        private void LoadCategories()
        {
            var categories = _categoryDal.GetAll();
            foreach (var category in categories) { Categories.Add(category);
            }
        }
    }
}
