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
    public class ProductModel : ViewModelBase, IDataErrorInfo
    {
        private int _id;
        private string _name;
        private decimal _price;
        private int _categoryId;
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
        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }
        public int CategoryId
        {
            get => _categoryId;
            set
            {
                _categoryId = value;
                OnPropertyChanged();
            }
        }
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Name))
                {
                    if (string.IsNullOrWhiteSpace(Name))
                    {
                        return "Name cannot be empty.";
                    }
                }
                if (columnName == nameof(Price))
                {
                    if (Price <= 0)
                    {
                        return "Price must be greater than zero.";
                    }
                }
                if (columnName == nameof(CategoryId) && CategoryId <= 0)
                {

                   return "Category must be selected.";

                }
                return null;
            }
        }
        public ProductModel()
        {
        }

        public ProductModel(ProductDTO product)
        {
            Id = product.ProductId;
            Name = product.Name;
            Price = product.Price;
            CategoryId = product.Category.CategoryId;

        }

        public ProductDTO ToDTO()
        {
            return new ProductDTO
            {
                ProductId = this.Id,
                Name = this.Name,
                Price = this.Price,
                Category = new CategoryDTO { CategoryId = this.CategoryId }
            };
        }
        public bool IsValid => string.IsNullOrWhiteSpace(this[nameof(Name)]) &&
                               string.IsNullOrWhiteSpace(this[nameof(Price)]) &&
                               string.IsNullOrWhiteSpace(this[nameof(CategoryId)]);
    }
}
