Public Class TestForm

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



End Class