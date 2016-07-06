Public Class Form4

    Dim sda As SQLite.SQLiteDataAdapter   ';//全局变量
    Dim G_dt As DataTable = New DataTable()
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim sql As String
        sql = String.Format("select * from zc")
        OpenDataBase(sda, G_dt, sql)
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub

    Public Sub OpenDataBase(ByRef Sda As SQLiteDataAdapter, ByRef dt As DataTable, sql As String)
        Try
            'Dim conn As Data.SQLite.SQLiteConnection = New Data.SQLite.SQLiteConnection(CONN_STR)
            'conn.Open()
            Sda = New SQLite.SQLiteDataAdapter(sql, CONN_STR)
            Dim scb As SQLite.SQLiteCommandBuilder = New SQLite.SQLiteCommandBuilder(Sda)
            dt.Clear()
            Sda.Fill(dt)

            Dim bs As BindingSource = New BindingSource()
            bs.DataSource = dt
            Me.BindingNavigator1.BindingSource = bs

            'Me.BindingSource1 = bs

            TextBox1.DataBindings.Add("Text", bs, "zcbh", True)
        Catch ex As SQLite.SQLiteException
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'Me.BindingNavigator1.AddNewItem()
    End Sub

    Private Sub BindingNavigatorAddNewItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorAddNewItem.Click

    End Sub

    Private Sub 保存SToolStripButton_Click(sender As Object, e As EventArgs)

    End Sub
End Class