Option Strict On

Imports System



Public Class ChainedList(Of T)
	Inherits ReadOnlyChainedList(Of T)

	Public Sub Add(ByVal item As T)
		m_head = New ChainLink(Of T)(item, m_head)
	End Sub

	Public Sub AddAtEnd(ByVal item As T)
		Dim link As New ChainLink(Of T)(item)
		Dim last As ChainLink(Of T)
		last = m_head
		Do While last.Next IsNot Nothing
			last = last.Next
		Loop
		last.Next = link
	End Sub

End Class


Public Class ReadOnlyChainedList(Of T)
	Implements IEnumerable(Of T)

	Protected m_head As ChainLink(Of T)

	Protected Sub New()
		' for ease of inheritance
	End Sub

	Public Sub New(ByVal head As ChainLink(Of T))
		m_head = head
	End Sub


	Public ReadOnly Property HasItems() As Boolean
		Get
			Return m_head IsNot Nothing
		End Get
	End Property


#Region "IEnumerable GetEnumerator's"

	Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of T) Implements System.Collections.Generic.IEnumerable(Of T).GetEnumerator
		Return New ChainedListEnumerator(Of T)(m_head)
	End Function

	Private Function IEnumerable_GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		Return GetEnumerator()
	End Function

#End Region

#Region "enumerator"

	Private Class ChainedListEnumerator(Of Tdata)
		Implements IEnumerator(Of Tdata)

		Private m_root As New ChainLink(Of Tdata)
		Private m_current As ChainLink(Of Tdata)

		Public Sub New(ByVal root As ChainLink(Of Tdata))
			m_root.Next = root
			m_current = m_root
		End Sub

		Public ReadOnly Property Current() As Tdata Implements System.Collections.Generic.IEnumerator(Of Tdata).Current
			Get
				Return m_current.Data
			End Get
		End Property

		Private ReadOnly Property Ienumerator_Current() As Object Implements System.Collections.IEnumerator.Current
			Get
				Return m_current
			End Get
		End Property

		Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
			If m_current IsNot Nothing Then
				m_current = m_current.Next
			End If
			Return m_current IsNot Nothing
		End Function

		Public Sub Reset() Implements System.Collections.IEnumerator.Reset
			m_current = m_root
		End Sub

#Region " IDisposable Support "
		Public Sub Dispose() Implements IDisposable.Dispose
			'nothing to do here
		End Sub
#End Region

	End Class
#End Region

End Class


Public Class ChainLink(Of T)
	Public Sub New()
		'
	End Sub
	Public Sub New(ByVal data As T)
		Me.Data = data
	End Sub
	Public Sub New(ByVal data As T, ByVal [next] As ChainLink(Of T))
		Me.Data = data
		Me.Next = [next]
	End Sub

	Public Data As T
	Public [Next] As ChainLink(Of T)
End Class


