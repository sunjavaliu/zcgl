Imports System.Data.OleDb
Imports System.Data

Public Class Form7
    Dim TabPageIndex As Integer
    Dim strConn As String
    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TabPage2.Parent = Nothing
        Me.TabPage3.Parent = Nothing

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Debug.Print(ListBox1.SelectedValue)
        If System.IO.Path.GetExtension(TextBox1.Text.ToLower) Like "*.xls*" And ListBox2.SelectedItem Is Nothing Then
            MsgBox("请选择Excel工作表")
        Else
            Me.TabPage1.Parent = Nothing
            Me.TabPage2.Parent = Me.TabControl1

        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim SheetName As String()
        OpenFileDialog1.Filter = "Excel 2003文件|*.xls|Excel 2007文件|*.xlsx|CSV|*.csv|所有文件|*.*"
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = OpenFileDialog1.FileName

            If Not System.IO.Path.GetExtension(TextBox1.Text.ToLower) Like ".xls*" And Not System.IO.Path.GetExtension(TextBox1.Text.ToLower) Like ".csv" Then
                MsgBox("导入Excel/CSV文件失败!失败原因：选择的不是Excel文件")
            ElseIf System.IO.Path.GetExtension(TextBox1.Text.ToLower) Like ".xls*" Then
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
        Me.Dispose()
        Me.Close()

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

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Debug.Print(ListBox1.SelectedValue)



        If ListBox2.SelectedItem Is Nothing Then
            MsgBox("没有选择表")
        Else

            Me.TabPage3.Parent = Me.TabControl1
            Me.TabPage2.Parent = Nothing
            display()
        End If

    End Sub

    Private Sub display()
        Dim BM_Field_Array As String() = {"部门编号", "部门名称", "父级部门编号"}
        Dim ZD_Field_Array As String() = {"类别", "内容"}
        Dim LB_Field_Array As String() = {"资产类别代码", "资产类别名称", "父级资产类别代码"}
        Dim ZC_Field_Array As String() = {"资产编号", "资产名称", "资产类别"}

        'BM_Field_Array(0) = "d"
        'Dim str As String = (My.Computer.FileSystem.ReadAllText("C:\QD51-R24_A.csv"))

        Select Case ListBox2.SelectedItem.ToString
            Case "部门"
                For i = 0 To BM_Field_Array.Length - 1
                    Me.DataGridView1.Rows.Add()
                    Me.DataGridView1.Rows(i).Cells(0).Value = BM_Field_Array(i)
                Next
            Case "资产"
                For i = 0 To ZC_Field_Array.Length - 1
                    Me.DataGridView1.Rows.Add()
                    Me.DataGridView1.Rows(i).Cells(0).Value = ZC_Field_Array(i)
                Next
            Case "类别"
                For i = 0 To LB_Field_Array.Length - 1
                    Me.DataGridView1.Rows(i).Cells(0).Value = LB_Field_Array(i)
                Next
            Case "通用"
                For i = 0 To ZD_Field_Array.Length
                    Me.DataGridView1.Rows.Add()
                    Me.DataGridView1.Rows(i).Cells(0).Value = ZD_Field_Array(i)
                Next
        End Select

    End Sub


    Public adoConn As New ADODB.Connection

    Private Sub csv()
        adoConn.ConnectionString = "Driver={Microsoft Text Driver (*.txt; *.csv)};DefaultDir=C:\Documents and Settings\vinsonlu\Desktop\1400004"
        adoConn.Open()
        Dim rs As New ADODB.Recordset
        rs.Open("select * from Orders_N.csv", adoConn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        If rs.RecordCount > 0 Then
            rs.MoveFirst()
            While Not rs.EOF
                MessageBox.Show(CStr(rs.Fields(1).Value))
                rs.MoveNext()
            End While
        End If
        rs.Close()
    End Sub
End Class
