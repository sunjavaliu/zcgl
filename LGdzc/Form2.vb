Public Class Form2
    Private TreeOperateType As String

    Dim sda As SQLite.SQLiteDataAdapter   ';//全局变量
    Dim G_dt As DataTable = New DataTable()

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

    Private Sub TreeView1_MouseUp(sender As Object, e As MouseEventArgs) Handles TreeView1.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.ContextMenuStrip1.Show(Me, e.Location)
        End If
    End Sub

    Private Sub Form2_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        sda.Dispose()
        G_dt.Dispose()
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            BindTreeView(0, TreeView1, G_dt)
            'OpreaRYDataBase("")
            'System.Threading.Thread.Sleep(10000)
            'TreeView1.ExpandAll()

            DisableWrite()
            TreeView1.ShowNodeToolTips = True
            TreeView1.Nodes(0).Expand()
            'TreeView1.ExpandAll()
        Catch ex As SQLite.SQLiteException
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub SaveAddDB(dataNode As TreeNode, ParentID As Integer)
        Dim row As DataRow = G_dt.NewRow()
        row("lbmc") = dataNode.Text
        row("lbdm") = dataNode.Name
        row("parentlbdm") = ParentID
        G_dt.Rows.Add(row)
        sda.Update(G_dt)
    End Sub
    Private Sub SaveModiDB(dataNode As TreeNode, ParentID As Integer)
        Dim rows() As DataRow = G_dt.Select("lbdm=" + dataNode.Name)
        Dim row As DataRow
        For Each row In rows
            row("lbmc") = dataNode.Text
            row("parentlbdm") = ParentID
        Next
        sda.Update(G_dt)
    End Sub
    Private Sub SaveDelDB(dataNode As TreeNode)
        Dim rows() As DataRow = G_dt.Select("lbdm=" + dataNode.Name)
        Dim row As DataRow
        For Each row In rows
            row.Delete()
        Next
        sda.Update(G_dt)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DisableWrite()
        TreeOperateType = TREE_NONE
    End Sub
    Private Sub OpreaBMDataBase(bmbh As String)
        'Dim conn As Data.SQLite.SQLiteConnection = New Data.SQLite.SQLiteConnection(CONN_STR)
        '打开连接
        'conn.Open()
        'Dim cmd As SQLite.SQLiteCommand = New SQLite.SQLiteCommand(conn)
        Dim sql As String

        If bmbh = "" Or bmbh = "1" Then
            sql = "select * from lb"
        Else
            Dim tmpI, MAXBH As Integer
            tmpI = CInt(bmbh)
            If tmpI Mod 1 = 0 Then MAXBH = tmpI
            If tmpI Mod 10 = 0 Then MAXBH = tmpI + 9
            If tmpI Mod 100 = 0 Then MAXBH = tmpI + 99
            If tmpI Mod 1000 = 0 Then MAXBH = tmpI + 999
            If tmpI Mod 10000 = 0 Then MAXBH = tmpI + 9999
            If tmpI Mod 100000 = 0 Then MAXBH = tmpI + 99999
            If tmpI Mod 1000000 = 0 Then MAXBH = tmpI + 999999

            sql = "select * from lb  where lbdm >= " + bmbh + " and lbdm<=" + MAXBH.ToString
            Debug.Print(sql)
        End If
        'ds = SQLite.SQLiteCommand SQLiteHelper.SQLiteCommandDataSet(DBConStr, sqlStr, Nothing)
        'Dim reader As SQLite.SQLiteDataReader = cmd.ExecuteReader()
        sda = New SQLite.SQLiteDataAdapter(sql, CONN_STR)
        Dim scb As SQLite.SQLiteCommandBuilder = New SQLite.SQLiteCommandBuilder(sda)
        G_dt.Clear()
        sda.Fill(G_dt)
        DataGridView1.DataSource = G_dt
        DataGridView1.Columns(0).ReadOnly = True
        DataGridView1.Columns(0).HeaderText = "内部ID"
        DataGridView1.Columns(1).HeaderText = "资产类别代码"
        DataGridView1.Columns(2).HeaderText = "资产类别名称"
        DataGridView1.Columns(3).HeaderText = "所属上级类别代码"

    End Sub

    Private Sub BindTreeView(ID As Long, treeview As TreeView, dt As DataTable)

        OpreaBMDataBase("")

        treeview.Nodes.Clear()
        treeview.ImageList = ImageList1

        Dim parentrow As DataRow() = dt.[Select]("parentlbdm=0")

        For i As Integer = 0 To parentrow.Length - 1
            Dim rootnode As New TreeNode()
            rootnode.Text = parentrow(i)("lbmc").ToString() '+ "[" + parentrow(i)("lbdm").ToString() + "]"
            'parentrow[i][3].ToString();
            rootnode.Name = parentrow(i)("lbdm").ToString()
            'rootnode.Value = parentrow(i)("ID").ToString()
            'parentrow[i][1].ToString(); 主键
            'rootnode. = True

            'rootnode.Expanded = True
            'rootnode.Selected = False
            'rootnode.SelectAction = TreeNodeSelectAction.None
            'rootnode.SelectedImageIndex = 0
            rootnode.StateImageIndex = 1
            'rootnode.ToolTipText = "单击右键进行编辑操作"
            'rootnode.SelectedImageIndex = 0
            treeview.Nodes.Add(rootnode)
            treeview.Nodes(0).Expand()
            'treeview.ImageList = 0
            '
            CreateChildNode(rootnode, dt)
        Next
        'System.Threading.Thread.Sleep(2000)
    End Sub
    Protected Sub CreateChildNode(parentNode As TreeNode, datatable As DataTable)
        Dim rowlist As DataRow() = datatable.[Select]("parentlbdm=" & Convert.ToString(parentNode.Name))
        For i As Integer = 0 To rowlist.Length - 1
            Dim node As New TreeNode()
            node.ToolTipText = "单击右键进行编辑操作"
            If datatable.[Select]("parentlbdm=" & rowlist(i)("lbdm").ToString().Trim()).Length > 0 Then
                node.Text = rowlist(i)("lbmc").ToString() '+ "[" + rowlist(i)("lbdm").ToString() + "]"
                node.Name = rowlist(i)("lbdm").ToString()
            Else
                node.Text = rowlist(i)("lbmc").ToString() '+ "[" + rowlist(i)("lbdm").ToString() + "]"
                node.Name = rowlist(i)("lbdm").ToString()
            End If
            'node.StateImageIndex = 1
            parentNode.Nodes.Add(node)
            '递归调用
            CreateChildNode(node, datatable)
        Next
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
        OpreaBMDataBase(SelectedNode.Name)
    End Sub

    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click
        Dim SelectedNode As TreeNode = TreeView1.SelectedNode
        EnableWrite()
        TextBox1.Text = SelectedNode.Name
        TextBox2.Text = SelectedNode.Text
        TreeOperateType = TREE_UPDATE_NODE
        TextBox1.Enabled = False
    End Sub

    Private Sub DelSubToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DelSubToolStripMenuItem.Click
        Dim SelectedNode As TreeNode = TreeView1.SelectedNode
        If MsgBox("你确认要删除该记录吗？", MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Exclamation, "警告") = MsgBoxResult.Ok Then

            If SelectedNode Is Nothing Then
                MessageBox.Show("没有选中任何节点")
            ElseIf SelectedNode.Name <> 1 And SelectedNode.GetNodeCount(True) = 0 Then

                Me.SaveDelDB(SelectedNode)
                TreeView1.Nodes.Remove(SelectedNode)


            Else
                MsgBox("该节点为根节点或含有下级子节点，请删除所有下级节点才能删除该节点！")
            End If
        End If
    End Sub
    '这里还有问题？删除上级节点没有处理
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
            Dim SCB = New SQLite.SQLiteCommandBuilder(sda)
            sda.Update(G_dt)
            MsgBox("删除成功")
        End If
    End Sub


    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub ExpandAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExpandAllToolStripMenuItem.Click
        TreeView1.ExpandAll()
    End Sub

    Private Sub CollapseAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CollapseAllToolStripMenuItem.Click
        TreeView1.CollapseAll()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

    End Sub
End Class