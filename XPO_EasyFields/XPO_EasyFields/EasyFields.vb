Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Public Class EasyFields

#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()

        CreateXPO_EasyFields()
        CreateXPO_EasyFields_Shortcut()
        CreateXPO_EasyFields_BatchUpdate()
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()

        MyBase.FinalizePlugIn()
    End Sub
#End Region

    Public Sub CreateXPO_EasyFields()
        Dim XPO_EasyFields As New DevExpress.CodeRush.Core.CodeProvider(components)
        CType(XPO_EasyFields, System.ComponentModel.ISupportInitialize).BeginInit()
        XPO_EasyFields.ProviderName = "XPO_EasyFields" ' Should be Unique
        XPO_EasyFields.DisplayName = "Update XPO FieldsClass"
        AddHandler XPO_EasyFields.CheckAvailability, AddressOf XPO_EasyFields_CheckAvailability
        AddHandler XPO_EasyFields.Apply, AddressOf XPO_EasyFields_Execute
        CType(XPO_EasyFields, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub XPO_EasyFields_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)

        'Check to see if we are within a class and the class is an XPO class
        If CodeRush.Source.ActiveClass IsNot Nothing AndAlso Helpers.XPO.IsPersistentClass(CodeRush.Source.ActiveClass) Then
            'Check to display all the time or only when Caret in the Class definition
            If Helpers.Settings.AvailableEntireClass OrElse TypeOf ea.CodeActive Is [Class] Then
                ea.Available = True
                Return
            End If
        End If
        ea.Available = False
    End Sub

    Public Sub CreateXPO_EasyFields_Shortcut()
        Dim XPO_EasyFields_Shortcut As New DevExpress.CodeRush.Core.Action(components)
        CType(XPO_EasyFields_Shortcut, System.ComponentModel.ISupportInitialize).BeginInit()
        XPO_EasyFields_Shortcut.ActionName = "XPO Update FieldsClass"
        XPO_EasyFields_Shortcut.ButtonText = "XPO Update FieldsClass" ' Used if button is placed on a menu.
        XPO_EasyFields_Shortcut.RegisterInCR = True
        AddHandler XPO_EasyFields_Shortcut.Execute, AddressOf XPO_EasyFields_Shortcut_Execute
        CType(XPO_EasyFields_Shortcut, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub XPO_EasyFields_Shortcut_Execute(ByVal ea As ExecuteEventArgs)
        ' This method is executed when your action is called.
        ' Remember you must bind your action to a shortcut in order to use it.
        ' Shortcuts are created\altered using the IDE\Shortcuts option page 
        Dim XPOClass As New Helpers.XPO.PersistentClassElement(CodeRush.Source.ActiveClass)
        XPOClass.UpdateFieldsClass()
        XPOClass.ApplyQueuedChanges()
    End Sub
    ' Please ensure the following line is not missing from your plugin's InitializeComponent
    ' components = New System.ComponentModel.Container()
    Public Sub CreateXPO_EasyFields_BatchUpdate()
        Dim XPO_EasyFields_BatchUpdate As New DevExpress.CodeRush.Core.Action(components)
        CType(XPO_EasyFields_BatchUpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        XPO_EasyFields_BatchUpdate.ActionName = "XPO Update FieldsClass ALL Persistent Classes"
        XPO_EasyFields_BatchUpdate.ButtonText = "XPO Update FieldsClass ALL Persistent Classes" ' Used if button is placed on a menu.
        XPO_EasyFields_BatchUpdate.RegisterInCR = True
        AddHandler XPO_EasyFields_BatchUpdate.Execute, AddressOf XPO_EasyFields_BatchUpdate_Execute
        CType(XPO_EasyFields_BatchUpdate, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub


    Private Sub XPO_EasyFields_BatchUpdate_Execute(ByVal ea As ExecuteEventArgs)
        Dim ActiveProject As ProjectElement = CodeRush.Source.Active.Project
        Dim XPOClassElements As IEnumerable(Of [Class]) = From c In Helpers.Common.Classes(ActiveProject) Where Helpers.XPO.IsPersistentClass(c)
        For Each XPOClassElement In XPOClassElements
            Dim XPOClass As New Helpers.XPO.PersistentClassElement(XPOClassElement)
            XPOClass.UpdateFieldsClass()
            XPOClass.ApplyQueuedChanges()
            If Helpers.Settings.UpdateOnSave Then
                XPOSyncSaveInProgress.Add(XPOClass.Document, True)
                XPOClass.Document.Save()
            End If
        Next
    End Sub
    Private Sub XPO_EasyFields_Execute(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        Dim XPOClass As New Helpers.XPO.PersistentClassElement(CodeRush.Source.ActiveClass)
        XPOClass.UpdateFieldsClass()
        XPOClass.ApplyQueuedChanges()
    End Sub

    Public Shared XPOSyncSaveInProgress As New Dictionary(Of TextDocument, Boolean)
    Private Sub XPO_EasyFields_DocumentSaved(ByVal ea As DevExpress.CodeRush.Core.DocumentEventArgs) Handles Me.DocumentSaved
        If Not Helpers.Settings.UpdateOnSave Then
            Return
        End If
        If TypeOf ea.Document Is TextDocument Then
            Dim doc As TextDocument = ea.Document
            If XPOSyncSaveInProgress.ContainsKey(doc) Then
                XPOSyncSaveInProgress.Remove(doc)
            Else
                Dim Classes As IEnumerable(Of [Class]) = New ElementEnumerable(CodeRush.Source.ActiveFileNode, LanguageElementType.Class, True).OfType(Of [Class])()
                For Each XPOClassElement As [Class] In Classes
                    Dim XPOClass As New Helpers.XPO.PersistentClassElement(XPOClassElement)
                    XPOClass.UpdateFieldsClass()
                    XPOClass.ApplyQueuedChanges()
                Next

                XPOSyncSaveInProgress.Add(doc, True)
                doc.Save()
            End If
        End If
    End Sub

    Private Sub XPO_EasyFields_OptionsChanged(ByVal ea As DevExpress.CodeRush.Core.OptionsChangedEventArgs) Handles Me.OptionsChanged
        'Don't have a need to reload settings as they only occur when events happen
        'If ea.OptionsPages.Contains(GetType(XPO_EasyFields_Options)) Then
        '    LoadSettings()
        'End If

    End Sub
End Class
