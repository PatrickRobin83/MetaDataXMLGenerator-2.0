/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   MetaDataFileWriter.cs
 *   Date			:   2022-10-04 14:15:22
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <p.robin@smartperform.de>
 * @Version      1.0.0
 */

using MetaDataXMLGenerator.Core.Config;
using MetaDataXMLGenerator.Core.Constants;
using MetaDataXMLGenerator.Core.Models;
using System.IO;
using System.Xml;
using static MetaDataXMLGenerator.Core.Services.DirectoryAndFileReader;

namespace MetaDataXMLGenerator.Core.Services;

public static class MetaDataFileWriter
{
    

    #region Fields

    private static IniFileReader _iniFileReader = new IniFileReader(ConstantStrings.INIFILEPATH);

    #endregion

    #region Properties
    public delegate void FileDestinationChanged(string FullFileName);
    public static event FileDestinationChanged? OnFileDestinationChanged;
    #endregion

    #region Constructor

    #endregion

    #region Methods

    public static void WriteMetaDataXml(string? pathForMetaDataXml, List<MetaDataEntry> entries)
    {
        if(File.Exists(@$"{pathForMetaDataXml}{ConstantStrings.METADATAXMLFILENAME}"))
        {
            File.Delete(@$"{pathForMetaDataXml}{ConstantStrings.METADATAXMLFILENAME}");
        }

        if (entries.Count > 0)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            doc.AppendChild(declaration);

            XmlNode docRoot = doc.CreateElement("Directory");
            foreach (MetaDataEntry entry in entries)
            {


                XmlNode fileElement = doc.CreateElement("File");
                XmlAttribute attribute = doc.CreateAttribute("name");
                attribute.Value = entry.FileName;
                XmlNode systemTags = doc.CreateElement("SystemTags");
                fileElement.AppendChild(systemTags);
                XmlNode systemTagNode = doc.CreateElement("SystemTag");
                systemTags.AppendChild(systemTagNode);
                XmlAttribute systemTag = doc.CreateAttribute("name");
                XmlAttribute value = doc.CreateAttribute("value");
                systemTag.Value = entry.SystemTagName;
                value.Value = entry.SystemTagValue;
                if (systemTagNode.Attributes != null)
                {
                    systemTagNode.Attributes.Append(systemTag);
                    systemTagNode.Attributes.Append(value);
                    if (fileElement.Attributes != null) fileElement.Attributes.Append(attribute);
                }

                docRoot.AppendChild(fileElement);
            }
            doc.AppendChild(docRoot);
            doc.Save(@$"{pathForMetaDataXml}{ConstantStrings.METADATAXMLFILENAME}");
            if (OnFileDestinationChanged != null)
            {
                OnFileDestinationChanged(@$"{pathForMetaDataXml}{ConstantStrings.METADATAXMLFILENAME}");
            }
        }
    }
    #endregion

}