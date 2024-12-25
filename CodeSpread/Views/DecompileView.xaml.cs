﻿using CodeSpread.Decompiler.Models;
using CodeSpread.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CodeSpread.Views;

/// <summary>
/// Interaction logic for DecompileView.xaml
/// </summary>
public partial class DecompileView : UserControl
{
    private readonly DecompileViewModel _viewModel;

    public DecompileView()
    {
        InitializeComponent();

        _viewModel = new DecompileViewModel();
        DataContext = _viewModel;
    }

    private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        _viewModel.OnSelectedItemChanged(e.NewValue);
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        foreach(var module in _viewModel.AssemblyModules)
        {
            var moduleTreeItem = new TreeViewItem
            {
                Header = module.ModuleName,
                Tag = module.ModuleName
            };

            moduleTreeItem.Expanded += ModuleExpanded;

            foreach (DecompiledType type in module.DecompiledTypes)
            {
                var typeTreeItem = new TreeViewItem
                {
                    Header = type.Name,
                    Tag = type.Name
                };

                foreach (var property in type.Properties)
                {
                    var PropertyTreeItem = new TreeViewItem
                    {
                        Header = property,
                        Tag = property
                    };

                    typeTreeItem.Items.Add(PropertyTreeItem);
                }

                foreach (var method in type.Methods)
                {
                    var MethodTreeItem = new TreeViewItem
                    {
                        Header = method,
                        Tag = method
                    };
                    typeTreeItem.Items.Add(MethodTreeItem);
                }

                foreach (var nestedType in type.NestedTypes)
                {
                    var nestedTypeTreeItem = new TreeViewItem
                    {
                        Header = nestedType.Name,
                        Tag = nestedType.Name
                    };

                    typeTreeItem.Items.Add(nestedTypeTreeItem);
                }

                AssemblyTreeView.Items.Add(typeTreeItem);
            }
        }


    }

    private void ModuleExpanded(object sender, RoutedEventArgs e)
    {
    }
}
