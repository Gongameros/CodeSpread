﻿using CodeSpread.Base;
using CodeSpread.Services;

namespace CodeSpread.Commands;

public class NavigateCommand : CommandBase
{
    private readonly INavigationService _navigationService;

    public NavigateCommand(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public override void Execute(object parameter)
    {
        _navigationService.Navigate();
    }
}
