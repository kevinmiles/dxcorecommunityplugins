Imports DevExpress.CodeRush.StructuralParser

Public Module MiscDX
    Private Sub FillVarsForClass(ByVal theClass As [Class], Optional ByVal isParent As Boolean = False)

        Dim ienum As IEnumerator = theClass.AllVariables.GetEnumerator
        While ienum.MoveNext
            Dim var As Variable = DirectCast(ienum.Current, Variable)
            If var.Visibility = MemberVisibility.Local Then
                Continue While
            End If
            If var.Visibility = MemberVisibility.Param Then
                Continue While
            End If
            If var.Visibility = MemberVisibility.Private Then
                Continue While
            End If
            If isParent Then
                Continue While
            End If
            If var.GetClass Is Nothing Then
                Continue While
            End If
            If Not var.InsideStruct OrElse var.GetStruct.Name = _parentClassOrStructName Then
                CheckAndAddVar(var, False)
            End If
        End While
        If Not theClass.PrimaryAncestorType Is Nothing Then
            If TypeOf theClass.PrimaryAncestorType.GetDeclaration Is [Class] Then
                Dim parentclass As [Class] = DirectCast(theClass.PrimaryAncestorType.GetDeclaration, [Class])
                FillVarsForClass(parentclass, True)
            End If
        End If
    End Sub
    Private Sub FillVarsForClass(ByVal theClass As [Class], Optional ByVal isParent As Boolean = False)
        Dim ienum As IEnumerator = theClass.AllVariables.GetEnumerator
        While ienum.MoveNext
            Dim var As Variable = DirectCast(ienum.Current, Variable)
            If (Not var.Visibility = MemberVisibility.Local AndAlso Not var.Visibility = MemberVisibility.Param) Then
                If Not isParent OrElse Not var.Visibility = MemberVisibility.Private Then
                    If Not var.GetClass Is Nothing AndAlso ((var.InsideStruct AndAlso var.GetStruct.Name = _parentClassOrStructName) OrElse Not var.InsideStruct) Then
                        CheckAndAddVar(var, False)
                    End If
                End If

            End If
            Dim tst As String
        End While
        If Not theClass.PrimaryAncestorType Is Nothing Then
            If TypeOf theClass.PrimaryAncestorType.GetDeclaration Is [Class] Then
                Dim parentclass As [Class] = DirectCast(theClass.PrimaryAncestorType.GetDeclaration, [Class])
                FillVarsForClass(parentclass, True)
            End If
        End If
    End Sub

    Enum Colors
        Red
        Blue
        Green
        Yellow
    End Enum
    Private Sub MethodName()
        Dim Color As Colors
        Select Case Color
            Case Colors.Red
            Case Colors.Blue
            Case Colors.Green
            Case Else
                Throw New Exception("Bad Color")
        End Select

    End Sub

End Module
