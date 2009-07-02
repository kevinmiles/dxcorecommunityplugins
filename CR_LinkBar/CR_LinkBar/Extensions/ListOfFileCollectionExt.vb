Imports DevExpress.CodeRush.Menus
Imports System.Runtime.CompilerServices
Imports System.IO
Module ListOfFileCollectionExt
    <Extension()> _
    Function ToXML(ByVal Source As List(Of FileCollection)) As XElement
        Return <FileCollections>
                   <%= From FileCollection In Source _
                       Select <FileCollection Name=<%= FileCollection.Name %>>
                                  <%= From File In FileCollection _
                                      Select <File Display=<%= File.Display %> FileWithPath=<%= File.FileWithPath %>/> _
                                  %>
                              </FileCollection> _
                   %>
               </FileCollections>
    End Function
    <Extension()> _
    Public Sub LoadFromXML(ByVal Source As List(Of FileCollection), ByVal XML As XElement)
        Source.Clear()
        For Each FileCollectionXML In XML.<FileCollection>
            Dim FC As New FileCollection(FileCollectionXML.@Name)
            For Each File In FileCollectionXML.<File>
                FC.Add(New FileReference(File.@Display, File.@FileWithPath))
            Next
            Call Source.Add(FC)
        Next
    End Sub

End Module

