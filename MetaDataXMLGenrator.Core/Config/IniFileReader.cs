/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   IniFileReader.cs
 *   Date			:   2022-10-04 14:06:34
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <p.robin@smartperform.de>
 * @Version      1.0.0
 */

using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace MetaDataXMLGenerator.Core.Config;

public class IniFileReader
{
    #region Fields

    private readonly string _path;
    private readonly string? _exe = Assembly.GetExecutingAssembly().GetName().Name;
    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    static extern long WritePrivateProfileString(string? section, string? key, string? value, string filePath);

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    static extern int GetPrivateProfileString(string? section, string key, string @default, StringBuilder retVal, int size, string filePath);


    #endregion

    #region Properties

    #endregion

    #region Constructor

    public IniFileReader(string? iniPath = null)
    {
        _path = new FileInfo(iniPath ?? _exe + ".ini").FullName.ToString();
    }

    #endregion

    #region Methods
    public string? Read(string key, string? section = null)
    {
        var retVal = new StringBuilder(255);
        GetPrivateProfileString(section ?? _exe, key, "", retVal, 255, _path);
        return retVal.ToString();
    }

    public void Write(string? key, string? Value, string? section = null)
    {
        WritePrivateProfileString(section ?? _exe, key, Value, _path);
    }

    public void DeleteKey(string? key, string? section = null)
    {
        Write(key, null, section ?? _exe);
    }

    public void DeleteSection(string? section = null)
    {
        Write(null, null, section ?? _exe);
    }

    public bool KeyExists(string key, string? section = null)
    {
        return Read(key, section)!.Length > 0;
    }

    #endregion
}