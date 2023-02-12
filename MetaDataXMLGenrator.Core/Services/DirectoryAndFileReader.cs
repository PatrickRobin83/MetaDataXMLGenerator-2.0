/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   DirectoryAndFileReader.cs
 *   Date			:   2022-10-04 14:14:43
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <p.robin@smartperform.de>
 * @Version      1.0.0
 */

using MetaDataXMLGenerator.Core.Config;
using MetaDataXMLGenerator.Core.Constants;
using MetaDataXMLGenerator.Core.Models;

namespace MetaDataXMLGenerator.Core.Services;

public class DirectoryAndFileReader
{


    #region Fields
    /// <summary>
    /// IniFile reader instance
    /// </summary>
    private IniFileReader _iniReader = new IniFileReader(ConstantStrings.INIFILEPATH);
    private string? _rootpath;
    private List<string?>? _folderPathes;
    private List<string?> _allFolders = new List<string?>();

    #endregion

    #region Properties

    #endregion

    #region Events

    public delegate void NumberOfFilesChanged(object sender, int allFiles, int countValue);
    public event NumberOfFilesChanged? OnNumberOfFilesChanged;
    #endregion


    #region Constructor

    public DirectoryAndFileReader(string? rootPath)
    {
        _rootpath = rootPath;
    }
    #endregion

    #region Methods

    public void Run()
    {
        _allFolders.Add(_rootpath);
        if (_rootpath != null) GetFoldersFromRootPath(_rootpath);
        GetFilesInFolder(_allFolders);
    }

    private void GetFoldersFromRootPath(string rootPath)
    {
        _folderPathes = new List<string?>();
        DirectoryInfo di = new DirectoryInfo(rootPath);
        if (di.GetDirectories().Length > 0)
        {
            foreach (string? folderPath in Directory.EnumerateDirectories(rootPath))
            {
                _allFolders.Add(folderPath);
                _folderPathes.Add(folderPath);
            }
        }

        foreach (string? folder in _folderPathes)
        {
            if (folder != null) GetFoldersFromRootPath(folder);
        }
    }

    private async void GetFilesInFolder(List<string?> allFolders)
    {
        int i = allFolders.Count;
        int counter = 0;

        foreach (string? rootFolder in _allFolders)
        {
            List<MetaDataEntry> metaDataEntries = new List<MetaDataEntry>();
            if (rootFolder != null)
            {
                DirectoryInfo di = new DirectoryInfo(rootFolder);
                if (di.GetFiles().Length > 0)
                {
                    foreach (FileInfo fileInfo in di.GetFiles())
                    {
                        if (!fileInfo.Name.EndsWith(".xml") && !fileInfo.Name.EndsWith(".ffs_db"))
                        {
                            if (_rootpath != null)
                            {
                                string tmpName = fileInfo.FullName.Remove(0, _rootpath.Length);
                                string uriPath = UrlConverter.Convert(tmpName);
                                MetaDataEntry entry =
                                    new MetaDataEntry(fileInfo.Name, _iniReader.Read("RootPath", "Web") + uriPath);
                                metaDataEntries.Add(entry);
                            }
                        }
                    }
                    MetaDataFileWriter.WriteMetaDataXml(rootFolder, metaDataEntries);
                    await Task.Delay(75);
                }
            }

            counter++;
            if (OnNumberOfFilesChanged != null)
            {
                OnNumberOfFilesChanged(this, i, counter);
            }
           

            

        }

        
    }

    #endregion

}