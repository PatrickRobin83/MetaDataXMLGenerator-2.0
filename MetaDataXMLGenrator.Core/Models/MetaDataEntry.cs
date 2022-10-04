/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   MetaDataEntry.cs
 *   Date			:   2022-10-04 14:10:20
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <p.robin@smartperform.de>
 * @Version      1.0.0
 */

namespace MetaDataXMLGenrator.Core.Models;

public class MetaDataEntry
{
    #region Fields

    /// <summary>
    /// Filename without the path
    /// </summary>
    private string _fileName;

    /// <summary>
    /// Name of the SystemTag
    /// </summary>
    private string _systemTagName;

    /// <summary>
    /// Value of the systemtag
    /// </summary>
    private string _systemTagValue;

    #endregion

    #region Properties
    public string FileName => _fileName;

    public string SystemTagName => _systemTagName;

    public string SystemTagValue
    {
        get => _systemTagValue;
    }

    #endregion

    #region Constructor
    /// <summary>
    /// Default Constructor
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="systemTagValue"></param>
    public MetaDataEntry(string fileName, string systemTagValue)
    {
        _fileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
        _systemTagName = "DownloadUrl";
        _systemTagValue = systemTagValue ?? throw new ArgumentNullException(nameof(systemTagValue));
    }

    #endregion

    #region Methods
    #endregion

}