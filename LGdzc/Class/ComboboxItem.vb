Public Class ComboboxItem

    Public Text As String = ""

    Public Value As String = ""
    Public Sub New(_Text As String, _Value As String)
        Text = _Text
        Value = _Value
    End Sub

    Public Overrides Function ToString() As String
        Return Text
    End Function
End Class