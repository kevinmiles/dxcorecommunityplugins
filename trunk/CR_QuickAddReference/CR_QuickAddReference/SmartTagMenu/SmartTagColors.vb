Option Strict On
Imports System.Drawing
Imports DevExpress.CodeRush.Core

Public MustInherit Class SmartTagColors
    Inherits SmartTagPopupMenuColors
    MustOverride ReadOnly Property Dark() As Color
    MustOverride ReadOnly Property Light() As Color

#Region "Title"
#Region "InActive"
    Public Overrides ReadOnly Property TitleBackgroundColor() As Color
        Get
            Return Light ' Whatever is set here seems to bleed out left. Go figure.
        End Get
    End Property

    Public Overrides ReadOnly Property TitleTextColor() As Color
        Get
            Return Dark
        End Get
    End Property
#End Region

#Region "Active"
    Public Overrides ReadOnly Property TitleActiveBackgroundColor() As Color
        Get
            Return Light ' Whatever is set here seems to bleed out left. Go figure.
        End Get
    End Property

    Public Overrides ReadOnly Property TitleActiveTextColor() As Color
        Get
            Return Color.White
        End Get
    End Property
#End Region

#End Region
#Region "Base"
    Public Overrides ReadOnly Property TextColor() As Color
        Get
            Return Color.Black
        End Get
    End Property
    Public Overrides ReadOnly Property BackgroundColor() As Color
        Get
            Return Color.White

        End Get
    End Property
#End Region
#Region "Selected"
    Public Overrides ReadOnly Property SelectedBackgroundColor() As Color
        Get
            Return Light
        End Get
    End Property

    Public Overrides ReadOnly Property SelectedBorderColor() As Color
        Get
            Return Color.Gray
        End Get
    End Property

    Public Overrides ReadOnly Property SelectedTextColor() As Color
        Get
            Return Color.White
        End Get
    End Property
#End Region
#Region "Borders"
    Public Overrides ReadOnly Property BorderBottomColor() As Color
        Get
            Return Light
        End Get
    End Property

    Public Overrides ReadOnly Property BorderLeftInnerColor() As Color
        Get
            Return Light
        End Get
    End Property

    Public Overrides ReadOnly Property BorderLeftOuterColor() As Color
        Get
            Return Light
        End Get
    End Property

    Public Overrides ReadOnly Property BorderRightInnerColor() As Color
        Get
            Return Dark
        End Get
    End Property

    Public Overrides ReadOnly Property BorderRightOuterColor() As Color
        Get
            Return Light
        End Get
    End Property

    Public Overrides ReadOnly Property BorderTopColor() As Color
        Get
            Return Dark
        End Get
    End Property
#End Region
End Class