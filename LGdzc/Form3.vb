Public Class Form3

    Private TreeOperateType As String



    Dim sda As LiuDataAdapter   ';//全局变量
    Dim G_dt As DataTable = New DataTable()

    Dim sda_ry As LiuDataAdapter   ';//全局变量
    Dim G_dt_ry As DataTable = New DataTable()


    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            'BindTreeView(0, TreeView1, G_dt)
            OpreaRYDataBase("")
            OpreaBMDataBase()
            CommBindTreeView(0, TreeView1, G_dt, "parentBMBH", "0", "BMMC", "BMBH")
            TreeView1.ShowNodeToolTips = True
            DataGridView1.ShowCellToolTips = True
            DataGridView1.Columns(0).ToolTipText = "双击单元格进行操作"
            'TreeView1.Nodes.
            TreeView1.Nodes(0).Expand()
        Catch ex As SQLite.SQLiteException
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If (Not Char.IsNumber(e.KeyChar) And e.KeyChar <> Chr(8)) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub





    Private Sub OpreaBMDataBase()
        Dim conn As Data.SQLite.SQLiteConnection = New Data.SQLite.SQLiteConnection(CONN_STR)
        '打开连接
        conn.Open()
        'Dim cmd As SQLite.SQLiteCommand = New SQLite.SQLiteCommand(conn)
        Dim sql As String = "select * from bm"
        'ds = SQLite.SQLiteCommand SQLiteHelper.SQLiteCommandDataSet(DBConStr, sqlStr, Nothing)
        'Dim reader As SQLite.SQLiteDataReader = cmd.ExecuteReader()
        sda = New LiuDataAdapter(sql, CONN_STR)


        sda.Fill(G_dt)

        'G_dt.Load(reader)
    End Sub




    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        Dim SelectedNode As TreeNode = TreeView1.SelectedNode
        TextBox1.Text = SelectedNode.Name
        TextBox2.Text = SelectedNode.Text
        'sda_ry = SQLite.SQLiteDataAdapter("select * from bm_ry where bmbh=" + SelectedNode.Name, CONN_STR)
        'Dim scb As SQLite.SQLiteCommandBuilder = New SQLite.SQLiteCommandBuilder(sda_ry)
        'sda_ry.SelectCommand.ExecuteReader("select * from bm_ry where bmbh=" + SelectedNode.Name)
        'sda_ry.Fill(G_dt_ry)
        'DataGridView1.DataSource = G_dt_ry
        'DataGridView1.Refresh()
        OpreaRYDataBase(SelectedNode.Name)
    End Sub


    Private Sub OpreaRYDataBase(bmbh As String)


        Dim conn As Data.SQLite.SQLiteConnection = New Data.SQLite.SQLiteConnection(CONN_STR)
        '打开连接
        conn.Open()
        'Dim cmd As SQLite.SQLiteCommand = New SQLite.SQLiteCommand(conn)
        Dim sql As String
        If bmbh = "" Or bmbh = "1" Then
            sql = "select * from zc"
        Else
            sql = "select * from zc where bmbh=" + bmbh
        End If

        'ds = SQLite.SQLiteCommand SQLiteHelper.SQLiteCommandDataSet(DBConStr, sqlStr, Nothing)
        'Dim reader As SQLite.SQLiteDataReader = cmd.ExecuteReader()
        sda_ry = New LiuDataAdapter(sql, CONN_STR)


        G_dt_ry.Clear()
        sda_ry.Fill(G_dt_ry)

        DataGridView1.DataSource = G_dt_ry
        DataGridView1.Columns(0).ReadOnly = True

        'DataGridView1.Columns("ID").HeaderText = "序号"



        DataGridView1.Columns(0).HeaderText = "ID"

        DataGridView1.Columns(1).HeaderText = "资产编号"
        DataGridView1.Columns(2).HeaderText = "资产名称"
        DataGridView1.Columns(3).HeaderText = "资产类别编号（国标）"
        DataGridView1.Columns(4).HeaderText = "资产类别名称（国标）"
        DataGridView1.Columns(5).HeaderText = "计量单位"
        DataGridView1.Columns(6).HeaderText = "购置日期"
        DataGridView1.Columns(7).HeaderText = "登记日期"
        DataGridView1.Columns(8).HeaderText = "资产来源"
        DataGridView1.Columns(9).HeaderText = "购置数量"
        DataGridView1.Columns(10).HeaderText = "单价"
        DataGridView1.Columns(11).HeaderText = "总价"
        DataGridView1.Columns(12).HeaderText = "资产状态"
        DataGridView1.Columns(13).HeaderText = "部门编号"
        DataGridView1.Columns(14).HeaderText = "部门名称"
        DataGridView1.Columns(15).HeaderText = "责任人"
        DataGridView1.Columns(16).HeaderText = "存放位置"
        DataGridView1.Columns(17).HeaderText = "流转记录"
        DataGridView1.Columns(18).HeaderText = "入库编号"
        DataGridView1.Columns(19).HeaderText = "设备型号"
        DataGridView1.Columns(20).HeaderText = "设备品牌"
        DataGridView1.Columns(21).HeaderText = "配置"
        DataGridView1.Columns(22).HeaderText = "设备序列号"
        DataGridView1.Columns(23).HeaderText = "操作系统序列号"

        '更改显示序号
        DataGridView1.Columns(14).DisplayIndex = 1
        DataGridView1.Columns(15).DisplayIndex = 2
        DataGridView1.Columns(19).DisplayIndex = 3
        DataGridView1.Columns(12).DisplayIndex = 4

        'DataGridView1.Columns("zcbh").HeaderText = "资产编号"
        'DataGridView1.Columns("zcmc").HeaderText = "资产名称"
        'DataGridView1.Columns("lbid").HeaderText = "资产类别编号（国标）"
        'DataGridView1.Columns("lbmc").HeaderText = "资产类别名称（国标）"
        'DataGridView1.Columns("jldw").HeaderText = "计量单位"
        'DataGridView1.Columns("gzrq").HeaderText = "购置日期"
        'DataGridView1.Columns("djrq").HeaderText = "登记日期"
        'DataGridView1.Columns("zcly").HeaderText = "资产来源"
        'DataGridView1.Columns("zcsl").HeaderText = "购置数量"
        'DataGridView1.Columns("zcdj").HeaderText = "单价"
        'DataGridView1.Columns("zczj").HeaderText = "总价"
        'DataGridView1.Columns("zczt").HeaderText = "资产状态"
        'DataGridView1.Columns("bmbh").HeaderText = "部门编号"
        'DataGridView1.Columns("bmmc").HeaderText = "部门名称"
        'DataGridView1.Columns("zrr").HeaderText = "责任人"
        'DataGridView1.Columns("cfwz").HeaderText = "存放位置"
        'DataGridView1.Columns("log").HeaderText = "流转记录"
        'DataGridView1.Columns("rkbh").HeaderText = "入库编号"
        'DataGridView1.Columns("zcxh").HeaderText = "设备型号"
        'DataGridView1.Columns("zcpp").HeaderText = "设备品牌"
        'DataGridView1.Columns("pz").HeaderText = "配置"
        'DataGridView1.Columns("devicesn").HeaderText = "设备序列号"
        'DataGridView1.Columns("ossn").HeaderText = "操作系统序列号"


        'DataGridView1.Columns(3).Frozen = True
        'G_dt.Load(reader)
    End Sub




    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'Dim SCB = New SQLite.SQLiteCommandBuilder(sda_ry)
        sda_ry.Update(G_dt_ry)
        MsgBox("更新成功")
        OpreaRYDataBase("")
    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim i, rowN As Integer
        Dim tmpList As New List(Of DataGridViewRow)()

        If MsgBox("你确认要删除该记录吗？", MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Exclamation, "警告") = MsgBoxResult.Ok Then
            '删除选中行
            rowN = DataGridView1.Rows.Count
            rowN = rowN - 1
            For i = rowN To 0 Step -1
                If DataGridView1.Rows(i).Selected = True Then
                    DataGridView1.Rows.RemoveAt(DataGridView1.Rows(i).Index)
                    'G_dt_ry.[Delete]("id="+dataGridv_AdminIma.Rows[i].Cells[0].Value.ToString())
                    'Debug.Print(i)
                    'MsgBox(DataGridView1.Rows(i).Cells(0).Value.ToString())
                    'tmpList.Add(DataGridView1.Rows(i))
                End If



            Next
            'DataGridView1.Rows.Remove(tmpList)
            'DataGridView1.Rows.RemoveAt(DataGridView1.CurrentCell.RowIndex)
            '数据库中进行删除()
            'Dim SCB = New SQLite.SQLiteCommandBuilder(sda_ry)
            sda_ry.Update(G_dt_ry)
            MsgBox("删除成功")
        End If

    End Sub


    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        'MsgBox("DataGridView1_CellContentClick标题单击")
    End Sub

 
    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        'MsgBox("DataGridView1_CellDoubleClick")
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Form4.StartPosition = FormStartPosition.CenterScreen
        Form4.ShowDialog()  '被ShowDialog出来的窗体关闭后实际只是被隐藏了，而没有被销毁。既并没有执行Dispose。
        Form4.Close()       '在这里关闭窗口就可以防止窗口窗体销毁不刷新主窗体
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub ExpandAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExpandAllToolStripMenuItem.Click
        TreeView1.ExpandAll()

    End Sub

    Private Sub CollapseAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CollapseAllToolStripMenuItem.Click
        TreeView1.CollapseAll()
    End Sub

    Private Sub TreeView1_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseDoubleClick

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        Form7.ShowDialog()

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs)
        ExportToCSV(DataGridView1)
    End Sub
End Class



