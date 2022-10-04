/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   ContentViewModel.cs
 *   Date			:   2022-10-04 14:29:26
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <p.robin@smartperform.de>
 * @Version      1.0.0
 */

using CommunityToolkit.Mvvm.ComponentModel;
using System.Resources;
using MetaDataXMLGenrator.Core.Config;

namespace MetaDataXMLGenerator.WPF.ViewModels;

[ObservableObject]
public partial class ContentViewModel
{
    #region Fields

    private IniFileReader _iniReader;
    private string _infilePath = "Resources\\Settings.ini";

    #endregion

    #region Properties

    [ObservableProperty] 
    private string _localPath;

    #endregion

    #region Constructor
    // ToDo: https://devblogs.microsoft.com/dotnet/announcing-the-dotnet-community-toolkit-800/
    public ContentViewModel()
    {
        _iniReader = new IniFileReader(_infilePath);
        LocalPath = _iniReader.Read("RootPath", "LocalPath");
    }
    #endregion

    #region Methods

    #endregion

}