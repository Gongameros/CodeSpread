namespace CodeSpread.Services;

public interface IParameterNavigationService<TParameter>
{
    void Navigate(TParameter parameter);
}
