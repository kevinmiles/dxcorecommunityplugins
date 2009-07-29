Imports System.Xml
Imports System.Runtime.CompilerServices

Public Module XElementExt
    <Extension()> _
    Public Function ToXElement(ByVal Source As XmlNode) As XElement
        Return XElement.Parse(Source.OuterXml)
    End Function
    <Extension()> _
    Public Function ToXMLElement(ByVal Source As XElement) As XmlElement
        Dim X As New XmlDocument
        X.LoadXml(Source.ToString)
        Return X.DocumentElement
    End Function

End Module