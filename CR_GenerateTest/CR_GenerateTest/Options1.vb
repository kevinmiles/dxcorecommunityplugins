Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core

<UserLevel(UserLevel.Advanced)> _
Public Class Options1

	'DXCore-generated code...
#Region " Initialize "
	Protected Overrides Sub Initialize()
		MyBase.Initialize()

		'TODO: Add your initialization code here.
	End Sub
#End Region

#Region " GetCategory "
	Public Shared Function GetCategory() As String
		Return "Community\Testing"
	End Function
#End Region
#Region " GetPageName "
	Public Shared Function GetPageName() As String
		Return "GenerateTest"
	End Function
#End Region

End Class
