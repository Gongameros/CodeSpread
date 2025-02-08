using CodeSpread.Decompiler.Models;
using CodeSpread.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CodeSpread.Views;

/// <summary>
/// Interaction logic for DecompileView.xaml
/// </summary>
public partial class DecompileView : UserControl
{
    private DecompileViewModel _decompileViewModel;

    public DecompileView()
    {
        InitializeComponent();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        // TEMP
        _decompileViewModel = (DecompileViewModel) this.DataContext;

        foreach (var module in _decompileViewModel.AssemblyModules)
        {
            var moduleTreeItem = new TreeViewItem
            {
                Header = module.ModuleName,
                Tag = module
            };

            moduleTreeItem.Expanded += ModuleExpanded;

            foreach (DecompiledType type in module.DecompiledTypes)
            {
                var typeTreeItem = new TreeViewItem
                {
                    Header = type.Name,
                    Tag = type
                };

                typeTreeItem.Selected += TreeItemSelectedType;

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
                        Tag = nestedType
                    };


                    typeTreeItem.Items.Add(nestedTypeTreeItem);
                }

                AssemblyTreeView.Items.Add(typeTreeItem);
            }
        }

    }

    private void TreeItemSelectedType(object sender, RoutedEventArgs e)
    {
        if (sender is TreeViewItem treeViewItem && treeViewItem.Tag is DecompiledType decompiledType)
        {
            _decompileViewModel.SelectedCode = decompiledType.DecompiledCode;
        }
    }
}
