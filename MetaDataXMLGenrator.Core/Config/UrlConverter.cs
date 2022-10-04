/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   UrlConverter.cs
 *   Date			:   2022-10-04 14:05:03
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <p.robin@smartperform.de>
 * @Version      1.0.0
 */

using System.Net;

namespace MetaDataXMLGenrator.Core.Config;

public static class UrlConverter
{
    /// <summary>
    /// Converts the given string into the right url format.
    /// </summary>
    /// <param name="stringToConvert"></param>
    /// <returns></returns>
    public static string convert(string stringToConvert)
    {
        if (stringToConvert == null) throw new ArgumentNullException(nameof(stringToConvert));
        string url = stringToConvert;

        url = url.Replace("\\", "/");
        url = WebUtility.UrlEncode(stringToConvert);
        //string url = Uri.EscapeDataString(stringToConvert);

        // url = url.Replace("%2F", "/");
        url = url.Replace("+", "%20");
        url = url.Replace("%5C", "/");

        return url;
    }

    #region Fields

    #endregion

    #region Properties

    #endregion

    #region Constructor

    #endregion

    #region Methods

    #endregion

}