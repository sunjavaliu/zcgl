Public Class Form6

    Dim sda As LiuDataAdapter   ';//全局变量
    Dim G_dt As DataTable = New DataTable()
    Public Sub OpenDataBase2DataGrideView(ByRef Sda As LiuDataAdapter, ByRef dt As DataTable, ByRef dg As DataGridView, sql As String)
        Try
            'Dim conn As Data.SQLite.SQLiteConnection = New Data.SQLite.SQLiteConnection(CONN_STR)
            'conn.Open()
            Sda = New LiuDataAdapter(sql, CONN_STR)

            dt.Clear()
            Sda.Fill(dt)
            dg.DataSource = dt

            '设置DataGridView可显示隐藏列,用Form的名字保存xml文件
            SetDataGridViewHidenColumn(DataGridView1, Me.Name.ToString())
            'SetDataGridViewHidenColumn(DataGridView1, Me.Text)

            dg.Columns(0).ReadOnly = True
            dg.Columns(1).ReadOnly = True
            dg.Columns(0).HeaderText = "ID"
            dg.Columns(1).HeaderText = "项目名称"
            dg.Columns(2).HeaderText = "项目内容"
        Catch ex As SQLite.SQLiteException
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Dim sql As String
        'sql = "select * from  zd "
        'OpenDataBase2DataGrideView(sda, G_dt, DataGridView1, sql)


        TreeView1.ExpandAll()

    End Sub
    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        Dim sql, tip As String
        Dim SelectedNode As TreeNode = TreeView1.SelectedNode
        If SelectedNode.Text = "常用字典信息" Then
            sql = "select * from  zd"
        Else
            sql = "select * from  zd where item like '%" + SelectedNode.Text + "%'"
        End If

        OpenDataBase2DataGrideView(sda, G_dt, DataGridView1, sql)

        tip = "当前正在查看【" + SelectedNode.Text + "】的信息，【ID】和【项目名称】不能编辑，双击【项目内容】单元格可进行添加或修改。"
        Label1.Text = tip
    End Sub
 
 

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        'DataGridView1.Rows.Add()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

        sda.Update(G_dt)
        MsgBox("更新成功")
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellBeginEdit1(sender As Object, e As DataGridViewCellCancelEventArgs) Handles DataGridView1.CellBeginEdit
        'Dim i As Integer
        'i = DataGridView1.SelectedRows.Item
        'e.RowIndex
        DataGridView1.Rows(e.RowIndex).Cells(1).Value = TreeView1.SelectedNode.Text
        ' .Rows[index].Cells[0].Value = dr[0].ToString();
    End Sub
 

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        Dim i, rowN As Integer
        'Dim tmpList As New List(Of DataGridViewRow)()

        If MsgBox("你确认要删除该记录吗？", MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Exclamation, "警告") = MsgBoxResult.Ok Then
            '删除选中行
            rowN = DataGridView1.Rows.Count - 1
            For i = rowN To 0 Step -1
                If DataGridView1.Rows(i).Selected = True Then
                    DataGridView1.Rows.RemoveAt(DataGridView1.Rows(i).Index)
                End If
            Next
            '数据库中进行删除()

            sda.Update(G_dt)
            MsgBox("删除成功")
        End If
    End Sub
 

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ExportToCSV(DataGridView1)
    End Sub
End Class