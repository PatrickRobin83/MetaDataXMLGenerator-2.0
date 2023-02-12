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
using CommunityToolkit.Mvvm.Input;
using MetaDataXMLGenerator.Core.Config;
using MetaDataXMLGenerator.Core.Constants;
using MetaDataXMLGenerator.Core.Services;
using ReactiveUI;
using System.Windows.Forms;
using System.Windows.Threading;
using FolderBrowserDialog = FolderBrowserEx.FolderBrowserDialog;

namespace MetaDataXMLGenerator.WPF.ViewModels;

[ObservableObject]
public partial class ContentViewModel
{
    #region Fields

    private readonly IniFileReader _iniReader;
    private readonly string? _infilePath = ConstantStrings.INIFILEPATH;
    private readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer();

   


    #endregion

    #region Properties

    [ObservableProperty] 
    private string? _localPath;
    [ObservableProperty]
    private string? _url;
    [ObservableProperty] 
    private  float _numberOfFiles;
    [ObservableProperty] 
    private float _counter;
    [ObservableProperty] private float _currentProgressValue;
    private float _result;
    [ObservableProperty] 
    private bool _progressVisibility;
    [ObservableProperty]
    private string _currentXMLFullFilename;
    [ObservableProperty]
    private bool _currentXMLFullFilenameVisibility;


    #endregion

    #region Constructor
    public ContentViewModel()
    {
        _iniReader = new IniFileReader(_infilePath);
        LocalPath = _iniReader.Read("RootPath", "Local");
        Url = _iniReader.Read("RootPath", "Web");
    }

    #endregion

    #region Methods

    [RelayCommand]
    private void OpenFolder()
    {
        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        folderBrowserDialog.Title = "Lokaler Pfad";
        folderBrowserDialog.AllowMultiSelect = false;
        folderBrowserDialog.InitialFolder = @"C:\";
        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
        {
            LocalPath = folderBrowserDialog.SelectedFolder;
            _iniReader.Write("RootPath", LocalPath,"Local");
            
        }
    }
    [RelayCommand]
    private void SaveUrl()
    {
        _iniReader.Write("RootPath",Url,"Web");
    }

    [RelayCommand]
    private void GenerateFiles()
    {
        DirectoryAndFileReader dafr = new DirectoryAndFileReader(LocalPath);
        dafr.OnNumberOfFilesChanged += OnNumberOfFilesChanged;
        MetaDataFileWriter.OnFileDestinationChanged += OnFileDestinationChanged;
        ProgressVisibility = true;
        dafr.Run();
        CurrentXMLFullFilenameVisibility = true;
    }

    private void OnNumberOfFilesChanged(object sender, int e, int c)
    {
        NumberOfFiles = e;
        Counter = c;
        _result = (100 / NumberOfFiles) * Counter;
        CurrentProgressValue = _result;
        if (CurrentProgressValue >= 100)
        {
            ProgressVisibility = false;
            CurrentXMLFullFilenameVisibility= false;
        }

    }

   private void OnFileDestinationChanged(string fileFullName)
    {
        if (fileFullName.Length >= 50)
        {
            fileFullName = "...\\" + fileFullName.Substring(fileFullName.Length - 50);
        }
        CurrentXMLFullFilename = fileFullName;
    }

    #endregion

}