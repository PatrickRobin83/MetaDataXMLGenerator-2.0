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

namespace MetaDataXMLGenrator.Core.Config;

public class IniFileReader
{
    #region Fields

    private readonly string _path;
    private readonly string _exe = Assembly.GetExecutingAssembly().GetName().Name;
    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);


    #endregion

    #region Properties

    #endregion

    #region Constructor

    public IniFileReader(string iniPath = null)
    {
        _path = new FileInfo(iniPath ?? _exe + ".ini").FullName.ToString();
    }

    #endregion

    #region Methods
    public string Read(string Key, string Section = null)
    {
        var RetVal = new StringBuilder(255);
        GetPrivateProfileString(Section ?? _exe, Key, "", RetVal, 255, _path);
        return RetVal.ToString();
    }

    public void Write(string Key, string Value, string Section = null)
    {
        WritePrivateProfileString(Section ?? _exe, Key, Value, _path);
    }

    public void DeleteKey(string Key, string Section = null)
    {
        Write(Key, null, Section ?? _exe);
    }

    public void DeleteSection(string Section = null)
    {
        Write(null, null, Section ?? _exe);
    }

    public bool KeyExists(string Key, string Section = null)
    {
        return Read(Key, Section).Length > 0;
    }

    #endregion
}