Imports System.Data.OleDb
Public Class Form7
    Dim TabPageIndex As Integer
    Dim strConn As String
    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TabPage2.Parent = Nothing


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Debug.Print(ListBox1.SelectedValue)
        If ListBox1.SelectedItem Is Nothing Then
            MsgBox("没有选择表格")
        End If


        Me.TabPage2.Parent = Me.TabControl1
        Me.TabPage1.Parent = Nothing
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim SheetName As String()
        OpenFileDialog1.Filter = "Excel 2003文件|*.xls|Excel 2007文件|*.xlsx|CSV|*.csv|所有文件|*.*"
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = OpenFileDialog1.FileName

            If Not System.IO.Path.GetExtension(TextBox1.Text) Like ".xls*" Then
                MsgBox("导入Excel失败!失败原因：选择的不是Excel文件")
            Else
                SheetName = GetAllSheetName(TextBox1.Text)
                For i As Integer = 0 To SheetName.Length - 1
                    If SheetName.Length > 0 Then
                        ListBox1.Items.Add(SheetName(i))
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click


    End Sub
    Public Shared Function GetAllSheetName(ByVal strFilePath As String) As String()
        Dim strConn As String = String.Empty
        If strFilePath.EndsWith("xls") Then
            strConn = "Provider=Microsoft.Jet.OLEDB.4.0; " + _
                       "Data Source=" + strFilePath + "; " + _
                       "Extended Properties='Excel 8.0;IMEX=1'"
        ElseIf strFilePath.EndsWith("xlsx") Then
            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + _
                      "Data Source=" + strFilePath + ";" + _
                      "Extended Properties='Excel 12.0;HDR=YES'"
        End If
        Dim conn As OleDbConnection = New OleDbConnection(strConn)
        conn.Open()


        Dim sheetNames(conn.GetSchema("Tables").Rows.Count - 1) As String
        For i As Integer = 0 To conn.GetSchema("Tables").Rows.Count - 1
            sheetNames(i) = conn.GetSchema("Tables").Rows(i)("TABLE_NAME").ToString
        Next
        conn.Close()
        Return sheetNames
    End Function

End Class