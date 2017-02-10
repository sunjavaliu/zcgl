Public Class zcjh

    Private TreeOperateType As String



    Dim sda As SQLite.SQLiteDataAdapter   ';//全局变量
    Dim G_dt As DataTable = New DataTable()

    Dim sda_ry As SQLite.SQLiteDataAdapter   ';//全局变量
    Dim G_dt_ry As DataTable = New DataTable()


    Private Sub zcjh_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            'BindTreeView(0, TreeView1, G_dt)
            OpreaRYDataBase("")
            OpreaBMDataBase()
            CommBindTreeView(0, TreeView1, G_dt, "parentBMBH", "0", "BMMC", "BMBH")
            DisableWrite()
            TreeView1.ShowNodeToolTips = True
            DataGridView1.ShowCellToolTips = True
            DataGridView1.Columns(0).ToolTipText = "双击单元格进行编辑操作"
            'TreeView1.Nodes.
            TreeView1.Nodes(0).Expand()
        Catch ex As SQLite.SQLiteException
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub TreeView1_MouseUp(sender As Object, e As MouseEventArgs) Handles TreeView1.MouseUp
        'PopupMenu()
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.ContextMenuStrip1.Show(Me, e.Location)
        End If

    End Sub

    Private Sub SaveAddDB(dataNode As TreeNode, ParentID As Integer)
        Dim row As DataRow = G_dt.NewRow()
        row("BMMC") = dataNode.Text
        row("bmbh") = dataNode.Name
        row("parentBMBH") = ParentID
        G_dt.Rows.Add(row)
        sda.Update(G_dt)
    End Sub
    Private Sub SaveModiDB(dataNode As TreeNode, ParentID As Integer)
        Dim rows() As DataRow = G_dt.Select("bmbh=" + dataNode.Name)
        Dim row As DataRow
        For Each row In rows
            row("bmmc") = dataNode.Text
            row("parentBMBH") = ParentID
        Next
        sda.Update(G_dt)
    End Sub
    Private Sub SaveDelDB(dataNode As TreeNode)
        Dim rows() As DataRow = G_dt.Select("bmbh=" + dataNode.Name)
        Dim row As DataRow
        For Each row In rows
            row.Delete()
        Next
        sda.Update(G_dt)
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim SelectedNode As TreeNode = TreeView1.SelectedNode

        Dim node As New TreeNode()
        node.Name = TextBox1.Text
        node.Text = TextBox2.Text

        If Not SelectedNode Is Nothing Then

            'If TreeView1.Nodes.Find(node.Name, True) Is Nothing Then
            If TreeOperateType = TREE_ADD_SIDEWAYS_NODE Then
                'node.Nodes.Add(TextBox1.Text & (node.GetNodeCount(False) + 1))
                TreeView1.SelectedNode.Parent.Nodes.Add(node)
                Me.SaveAddDB(node, SelectedNode.Parent.Name)
            End If

            If TreeOperateType = TREE_ADD_SUB_NODE Then
                'node.Nodes.Add(TextBox1.Text & (node.GetNodeCount(False) + 1))

                TreeView1.SelectedNode.Nodes.Add(node)
                Me.SaveAddDB(node, SelectedNode.Name)
            End If
            If TreeOperateType = TREE_UPDATE_NODE Then
                TreeView1.SelectedNode.Name = node.Name
                TreeView1.SelectedNode.Text = node.Text
                TreeView1.Refresh()
                If SelectedNode.Name = 1 Then
                    Me.SaveModiDB(node, 0)
                Else
                    Me.SaveModiDB(node, SelectedNode.Parent.Name)
                End If

            End If
        Else

            MessageBox.Show("没有选中任何节点")

        End If
        DisableWrite()
        TreeOperateType = TREE_NONE
    End Sub
    Private Sub EnableWrite()
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        'TreeView1.Refresh()
        TreeView1.Enabled = False

    End Sub
    Private Sub DisableWrite()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        'TreeView1.Refresh()
        TreeView1.Enabled = True
        TreeOperateType = TREE_NONE
    End Sub
    Private Sub AddSideWaysToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddSideWaysToolStripMenuItem.Click
        TreeOperateType = TREE_ADD_SIDEWAYS_NODE
        EnableWrite()
        TextBox1.Text = TreeView1.GetNodeCount(True) + CStr(1)
        TextBox2.Text = ""
        TextBox2.Focus()
    End Sub
    Private Sub AddSubToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddSubToolStripMenuItem.Click
        TreeOperateType = TREE_ADD_SUB_NODE
        EnableWrite()
        TextBox1.Text = TreeView1.GetNodeCount(True) + CStr(1)
        TextBox2.Text = ""
        TextBox2.Focus()


    End Sub

    Private Sub DelSubToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DelSubToolStripMenuItem.Click
        Dim SelectedNode As TreeNode = TreeView1.SelectedNode
        If MsgBox("你确认要删除该记录吗？", MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Exclamation, "警告") = MsgBoxResult.Ok Then
            If SelectedNode Is Nothing Then
                MessageBox.Show("没有选中任何节点")
            ElseIf SelectedNode.Name <> 1 Then
                Me.SaveDelDB(SelectedNode)
                TreeView1.Nodes.Remove(SelectedNode)
            Else
                MsgBox("该节点为根节点或含有下级子节点，请删除所有下级节点才能删除该节点！")
            End If
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DisableWrite()
        TreeOperateType = TREE_NONE
    End Sub


    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click
        Dim SelectedNode As TreeNode = TreeView1.SelectedNode
        EnableWrite()
        TextBox1.Text = SelectedNode.Name
        TextBox2.Text = SelectedNode.Text
        TreeOperateType = TREE_UPDATE_NODE
        TextBox1.Enabled = False

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
        sda = New SQLite.SQLiteDataAdapter(sql, CONN_STR)
        Dim scb As SQLite.SQLiteCommandBuilder = New SQLite.SQLiteCommandBuilder(sda)

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
        sda_ry = New SQLite.SQLiteDataAdapter(sql, CONN_STR)
        Dim scb As SQLite.SQLiteCommandBuilder = New SQLite.SQLiteCommandBuilder(sda_ry)

        G_dt_ry.Clear()
        sda_ry.Fill(G_dt_ry)

        DataGridView1.DataSource = G_dt_ry
        DataGridView1.Columns(0).ReadOnly = True
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
        DataGridView1.Columns(17).HeaderText = "备注"
        DataGridView1.Columns(18).HeaderText = "备用1"
        DataGridView1.Columns(19).HeaderText = "备用2"
        DataGridView1.Columns(20).HeaderText = "备用3"
        DataGridView1.Columns(21).HeaderText = "备用4"
        DataGridView1.Columns(22).HeaderText = "备用5"
        DataGridView1.Columns(23).HeaderText = "备用6"
        DataGridView1.Columns(24).HeaderText = "备用7"
        DataGridView1.Columns(25).HeaderText = "备用8"
        DataGridView1.Columns(26).HeaderText = "备用N1"
        DataGridView1.Columns(27).HeaderText = "备用N2"
        DataGridView1.Columns(28).HeaderText = "备用N3"
        DataGridView1.Columns(29).HeaderText = "备用N4"
        DataGridView1.Columns(30).HeaderText = "备用N5"
        DataGridView1.Columns(31).HeaderText = "备用N6"
        DataGridView1.Columns(32).HeaderText = "流转记录"
        DataGridView1.Columns(33).HeaderText = "入库编号"


        DataGridView1.Columns(3).Frozen = True
        'G_dt.Load(reader)
    End Sub




    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim SCB = New SQLite.SQLiteCommandBuilder(sda_ry)
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
            Dim SCB = New SQLite.SQLiteCommandBuilder(sda_ry)
            sda_ry.Update(G_dt_ry)
            MsgBox("删除成功")
        End If

    End Sub


    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        MsgBox("DataGridView1_CellContentClick标题单击")
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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form7.ShowDialog()

    End Sub
End Class



