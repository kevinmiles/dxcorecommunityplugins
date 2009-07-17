Imports System.Drawing
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.Menus
Imports System.IO
Imports DevExpress.CodeRush.StructuralParser


Public Class WorkspaceCollection
    Implements IEnumerable(Of Workspace)
    Private mWorkspaceList As New List(Of Workspace)
    Private mWorkspaceDict As New Dictionary(Of String, Workspace)
#Region "List"
    Public Sub Clear()
        mWorkspaceList.Clear()
        mWorkspaceDict.Clear()
    End Sub
    Public Sub Add(ByVal Workspace As Workspace)
        mWorkspaceList.Add(Workspace)
        mWorkspaceDict.Add(Workspace.Name, Workspace)
    End Sub
    Public Function Exists(ByVal P As Predicate(Of Workspace)) As Boolean
        Return mWorkspaceList.Exists(P)
    End Function
    Public Function Item(ByVal WSName As String) As Workspace
        Return mWorkspaceDict.Item(WSName)
    End Function
    Public Sub Remove(ByVal Workspace As Workspace)
        mWorkspaceList.Remove(Workspace)
        mWorkspaceDict.Remove(Workspace.Name)
    End Sub
#End Region
#Region "IEnumerable"

    Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of Workspace) Implements System.Collections.Generic.IEnumerable(Of Workspace).GetEnumerator
        Return mWorkspaceList.GetEnumerator
    End Function

    Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return mWorkspaceList.GetEnumerator
    End Function
#End Region
    Function ToXML() As XElement
        Return <Workspaces>
                   <%= From Workspace In Me _
                       Select <Workspace Name=<%= Workspace.Name %>>
                                  <%= From File In Workspace _
                                      Select <File Display=<%= File.Display %>
                                                 FileWithPath=<%= File.FileWithPath %>
                                                 TopLine=<%= File.TopLine %>
                                                 CaretX=<%= File.CaretX %>
                                                 CaretY=<%= File.CaretY %>
                                             /> _
                                  %>
                              </Workspace> _
                   %>
               </Workspaces>
    End Function
    Public Sub LoadFromXML(ByVal XML As XElement)
        Clear()
        For Each WorkspaceXML In XML.<Workspace>
            Dim FC As New Workspace(WorkspaceXML.@Name)
            For Each File In WorkspaceXML.<File>
                FC.Add(New DocumentReference(File.@Display, _
                                             File.@FileWithPath, _
                                             CInt(File.@TopLine.DefaultTo("1")), _
                                             CInt(File.@CaretX.DefaultTo("1")), _
                                             CInt(File.@CaretY.DefaultTo("1")) _
                                            ))
            Next
            Call Add(FC)
        Next
    End Sub
End Class
