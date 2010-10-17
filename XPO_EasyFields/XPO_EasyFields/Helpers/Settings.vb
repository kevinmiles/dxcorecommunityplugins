Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser

Namespace Helpers
    Namespace Settings
        Module Settings
            'Setting Locations
            Public Const sName As String = "Setting"
            Public Const sRegionName As String = "RegionName"

            'Settings
            Public Const sAvailableEntireClass As String = "AvailableEntireClass"
            Public Const sUpdateOnSave As String = "UpdateOnSave"
            Public Const sReplaceClassOnly As String = "ReplaceClassOnly"
            Public Const sUseComment As String = "UseComment"
            Public Const sCommentFormat As String = "CommentFormat"
            Public Const sIncludeNonPersistent As String = "IncludeNonPersistent"
            Public Const sUseCollectionFieldsClass As String = "UseCollectionFieldsClass"
            Public Const sIncludeInheritedMembers As String = "IncludeInheritedMembers"
            Public Const sFieldsClassVariableName As String = "FieldsClassVariableName"
            Public Const sIncludeFieldConstants As String = "IncludeFieldConstants"

            'Setting Default
            Public Const sDefaultCommentFormat As String = "Created/Updated: {currentuser} on {computername} at {dateshort} {timeshort}"
            Public Const sDefaultFieldsClassVariableName As String = "_Fields"

            Private _storage As DecoupledStorage
            Public ReadOnly Property Storage() As DecoupledStorage
                Get
                    If _storage Is Nothing Then
                        _storage = DevExpress.CodeRush.Core.CodeRush.Options.GetStorage(EasyFields_Options.GetCategory(), EasyFields_Options.GetPageName())
                    End If
                    Return _storage
                End Get
            End Property

            Public Property IncludeFieldConstants As Boolean
                Get
                    Return Storage.ReadBoolean(sName, sIncludeFieldConstants, False)
                End Get
                Set(ByVal value As Boolean)
                    Storage.WriteBoolean(sName, sIncludeFieldConstants, value)
                End Set
            End Property
            Public Property IncludeInheritedMembers() As Boolean
                Get
                    Return Storage.ReadBoolean(sName, sIncludeInheritedMembers, True)
                End Get
                Set(ByVal Value As Boolean)
                    Storage.WriteBoolean(sName, sIncludeInheritedMembers, Value)
                End Set
            End Property


            Public Property AvailableEntireClass() As Boolean
                Get
                    Return Storage.ReadBoolean(sName, sAvailableEntireClass, False)
                End Get
                Set(ByVal value As Boolean)
                    Storage.WriteBoolean(sName, sAvailableEntireClass, value)
                End Set
            End Property

            Public Property UseComment() As Boolean
                Get
                    Return Storage.ReadBoolean(sName, sUseComment, True)
                End Get
                Set(ByVal Value As Boolean)
                    Storage.WriteBoolean(sName, sUseComment, Value)
                End Set
            End Property

            Public Property IncludeNonPersistent() As Boolean
                Get
                    Return Storage.ReadBoolean(sName, sIncludeNonPersistent, False)
                End Get
                Set(ByVal Value As Boolean)
                    Storage.WriteBoolean(sName, sIncludeNonPersistent, Value)
                End Set
            End Property

            Public Property CommentFormat() As String
                Get
                    Return Storage.ReadString(sName, sCommentFormat, sDefaultCommentFormat)
                End Get
                Set(ByVal Value As String)
                    Storage.WriteString(sName, sCommentFormat, Value)
                End Set
            End Property

            Public Property FieldsClassVariableName As String
                Get
                    Return Storage.ReadString(sName, sFieldsClassVariableName, sDefaultFieldsClassVariableName)
                End Get
                Set(ByVal Value As String)
                    Storage.WriteString(sName, sFieldsClassVariableName, Value)
                End Set
            End Property

            Public Property ReplaceClassOnly() As Boolean
                Get
                    Return Storage.ReadBoolean(sName, sReplaceClassOnly, True)
                End Get
                Set(ByVal Value As Boolean)
                    Storage.WriteBoolean(sName, sReplaceClassOnly, Value)
                End Set
            End Property

            Public Property UpdateOnSave() As Boolean
                Get
                    Return Storage.ReadBoolean(sName, sUpdateOnSave, False)
                End Get
                Set(ByVal Value As Boolean)
                    Storage.WriteBoolean(sName, sUpdateOnSave, Value)
                End Set
            End Property

            Public Property UseCollectionFieldsClass() As Boolean
                Get
                    Return Storage.ReadBoolean(sName, sUseCollectionFieldsClass, False)
                End Get
                Set(ByVal value As Boolean)
                    Storage.WriteBoolean(sName, sUseCollectionFieldsClass, value)
                End Set
            End Property
        End Module
    End Namespace
End Namespace

