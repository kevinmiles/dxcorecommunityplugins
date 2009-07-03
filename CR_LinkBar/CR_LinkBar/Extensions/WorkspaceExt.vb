Imports System.Runtime.CompilerServices
Module WorkspaceExt
    <Extension()> _
    Function ToXML(ByVal Source As List(Of Workspace)) As XElement
        Return <Workspaces>
                   <%= From Workspace In Source _
                       Select <Workspace Name=<%= Workspace.Name %>>
                                  <%= From File In Workspace _
                                      Select <File Display=<%= File.Display %> FileWithPath=<%= File.FileWithPath %>/> _
                                  %>
                              </Workspace> _
                   %>
               </Workspaces>
    End Function
    <Extension()> _
    Public Sub LoadFromXML(ByVal Source As List(Of Workspace), ByVal XML As XElement)
        Source.Clear()
        For Each WorkspaceXML In XML.<Workspace>
            Dim FC As New Workspace(WorkspaceXML.@Name)
            For Each File In WorkspaceXML.<File>
                FC.Add(New FileReference(File.@Display, File.@FileWithPath))
            Next
            Call Source.Add(FC)
        Next
    End Sub

End Module

