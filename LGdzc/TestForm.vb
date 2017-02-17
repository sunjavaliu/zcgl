Imports MySql.Data.MySqlClient

Public Class TestForm
    Dim conn As New MySqlConnection
    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim datatable1 As New DataTable()
        Dim datatable2 As New DataTable()

        datatable1.Columns.Add("id", Type.[GetType]("System.Int32"))
        datatable1.Columns.Add("name", Type.[GetType]("System.String"))
        Dim dr As DataRow = datatable1.NewRow()
        dr("id") = "1"
        dr("name") = "aa"
        datatable1.Rows.Add(dr)
        dr = datatable1.NewRow()
        dr("id") = "2"
        dr("name") = "bb"
        datatable1.Rows.Add(dr)
        dr = datatable1.NewRow()
        dr("id") = "3"
        dr("name") = "cc"
        datatable1.Rows.Add(dr)


        datatable2.Columns.Add("name", Type.[GetType]("System.String"))
        Dim dr1 As DataRow = Nothing
        For i As Integer = 0 To datatable1.Rows.Count - 1
            dr1 = datatable2.NewRow()
            dr1("name") = datatable1.Rows(i)("name").ToString()
            datatable2.Rows.Add(dr1)
        Next
        DataGridView1.DataSource = datatable2
    End Sub

    Public Sub connect()

        Dim DatabaseName As String = "gdzc"

        Dim server As String = "10.43.18.42"

        Dim userName As String = "mysql"

        Dim password As String = "mysqlpwd"

        If Not conn Is Nothing Then conn.Close()

        conn.ConnectionString = String.Format("server={0}; user id={1}; password={2}; database={3}; pooling=false", server, userName, password, DatabaseName)

        Try

            conn.Open()

            MsgBox("数据库链接正常")

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

        conn.Close()

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Try

            conn.Open()

        Catch ex As Exception

        End Try
        Dim sql As String
        sql = String.Format("INSERT INTO `Products` (`upc` , `qty`) VALUES ('{0}' , '{1}')", "upc Value", "Quantity")
        Dim cmd As New MySqlCommand(sql)
        cmd.Connection = conn
        cmd.ExecuteNonQuery()

        conn.Close()

    End Sub

    Private Sub TestForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text, String.Format("INSERT INTO `Products` (`upc` , `qty`) VALUES ('{0}' , '{1}')", "upc Value", "222Quantity"))
        'DataGridView1.DataSource = MySqlHelper.GetDataSet(MySqlHelper.Conn, CommandType.Text, "select * from bm").Tables(0).DefaultView
    End Sub
End Class