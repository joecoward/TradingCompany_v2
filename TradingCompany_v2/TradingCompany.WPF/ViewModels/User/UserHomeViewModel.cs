using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TradingCompany.DALEF.interfaces;
using TradingCompany.DTO;
using TradingCompany.WPF.Core;
using TradingCompany.WPF.Views.Action;
using TradingCompany.WPF.Views.Product;


namespace TradingCompany.WPF.ViewModels
{
    public class UserHomeViewModel : ViewModelBase
    {
        private readonly IProductDal _productDal;
        private readonly IActionDal _actionDal;
        private readonly ICategoryDal _categoryDal;
        private readonly IStatusDal _statusDal;

        public event Action LogoutRequested;

        public ObservableCollection<ProductDTO> Products { get; set; } = new ObservableCollection<ProductDTO>();
        public ObservableCollection<ActionDTO> Actions { get; set; } = new ObservableCollection<ActionDTO>();

        public ICommand LogoutCommand { get; }

        public ICommand DeleteProductCommand { get; }
        public ICommand EditProductCommand { get; }
        public ICommand AddProductCommand { get; }

        public ICommand DeleteActionCommand { get; }
        public ICommand EditActionCommand { get; }
        public ICommand AddActionCommand { get; }


        public UserHomeViewModel(IProductDal productDal, IActionDal actionDal , ICategoryDal category, IStatusDal statusDal)
        {
            _productDal = productDal;
            _actionDal = actionDal;
            _categoryDal = category;
            _statusDal = statusDal;

            LogoutCommand = new RelayCommand(ExecuteLogout);

            DeleteProductCommand = new RelayCommand(ExecuteDeleteProduct);
            EditProductCommand = new RelayCommand(ExecuteEditProduct);
            AddProductCommand = new RelayCommand(ExrecuteAddProduct);

            DeleteActionCommand = new RelayCommand(ExecuteDeleteAction);
            EditActionCommand = new RelayCommand(ExecuteEditAction);
            AddActionCommand = new RelayCommand(ExecuteAddAction);

            LoadData();
        }

        private void ExecuteAddAction(object obj)
        {
            var editorVm = new ActionEditorViewModel(_actionDal, _statusDal);
            var newaction = new ActionDTO();
            editorVm.SetActionToEdit(newaction);
            var window = new ActionCreaterView();
            window.DataContext = editorVm;
            editorVm.CloseRequest += () =>
            {
                window.Close();
                LoadData();
            };
            window.ShowDialog();

        }

        private void ExrecuteAddProduct(object obj)
        {

            var editorVm = new ProductEditorViewModel(_productDal, _categoryDal);
            var newproduct = new ProductDTO();
            editorVm.SetProductToEdit(newproduct);
            var window = new ProductCreaterView();
            window.DataContext = editorVm;

            editorVm.CloseRequest += () =>
            {
                window.Close();

                LoadData();
            };

            window.ShowDialog();
        }

        private void ExecuteEditAction(object obj)
        {
            if(obj is ActionDTO action)
            {
                var editorVm = new ActionEditorViewModel(_actionDal, _statusDal);
                editorVm.SetActionToEdit(action);
                var window = new ActionCreaterView();
                window.DataContext = editorVm;
                editorVm.CloseRequest += () =>
                {
                    window.Close();
                    LoadData();
                };
                window.ShowDialog();


            }
        }

        private void ExecuteDeleteAction(object obj)
        {
            if (obj is ActionDTO action)
            {
                var result = MessageBox.Show($"Are you sure you want to delete the action: {action.Name}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _actionDal.Delete(action.ActionId);
                        Actions.Remove(action);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting action: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void ExecuteEditProduct(object obj)
        {
            if(obj is ProductDTO product)
            {
                var editorVm = new ProductEditorViewModel(_productDal, _categoryDal);
                editorVm.SetProductToEdit(product);
                var window = new ProductCreaterView();
                window.DataContext = editorVm;
                editorVm.CloseRequest += () =>
                {
                    window.Close();
                    LoadData();
                };
                window.ShowDialog();

            }
            
        }

        private void ExecuteDeleteProduct(object obj)
        {
            if (obj is ProductDTO product)
            {
                var result = MessageBox.Show($"Are you sure you want to delete the product: {product.Name}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _productDal.Delete(product.ProductId);
                        Products.Remove(product);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void LoadData()
        {
            try
            {
                var productlist = _productDal.GetAll();
                Products.Clear();
                foreach (var product in productlist)
                {
                    Products.Add(product);
                }
                var actionlist = _actionDal.GetAll();
                Actions.Clear();
                foreach (var action in actionlist)
                {
                    Actions.Add(action);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExecuteLogout(object obj)
        {
            LogoutRequested?.Invoke();
        }
    }
}
