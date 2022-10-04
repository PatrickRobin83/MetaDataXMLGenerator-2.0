/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   MainWindowViewModel.cs
 *   Date			:   2022-10-04 14:20:11
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <p.robin@smartperform.de>
 * @Version      1.0.0
 */

using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetaDataXMLGenerator.WPF.Views;

namespace MetaDataXMLGenerator.WPF.ViewModels;

[ObservableObject]
public partial class MainWindowViewModel
{


    #region Fields

    #endregion

    #region Properties
    [ObservableProperty]
    private ContentView _cV = new ContentView
    {
        DataContext = new ContentViewModel()
    };
    #endregion

    #region Constructor

    public MainWindowViewModel()
    {
        InitializeViewsAndDataContext();
    }
    #endregion

    #region Methods

    private void InitializeViewsAndDataContext()
    {
    }   
    [RelayCommand]
    private void Exit()
    {
        Environment.Exit(0);
    }

    #endregion

}