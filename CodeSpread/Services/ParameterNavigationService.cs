using CodeSpread.Stores;
using CodeSpread.ViewModels;

namespace CodeSpread.Services;

public class ParameterNavigationService<TParameter, TViewModel> : IParameterNavigationService<TParameter> 
    where TParameter : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    private readonly Func<TParameter, TViewModel> _createViewModel;

    public ParameterNavigationService(NavigationStore navigationStore, Func<TParameter, TViewModel> createViewModel)
    {
        _navigationStore = navigationStore;
        _createViewModel = createViewModel;
    }

    public void Navigate(TParameter parameter)
    {
        throw new NotImplementedException();
    }
}
