Public Class Form1

    Private TreeOperateType As String

    Dim sda As LiuDataAdapter  ';//全局变量

    Dim G_dt As DataTable = New DataTable()
 
    Dim sda_ry As LiuDataAdapter   ';//全局变量

    Dim G_dt_ry As DataTable = New DataTable()

    Dim selectRow As Integer

    Dim G_BMBH As String                '部门编号全局信息
    'Dim sda_ry As LiuDataAdapter
    Dim ComboBoxTreeBM As ComboBoxTreeView
    Dim oldName As String           ' 修改前的姓名
    Dim modifyID As String          '修改的ID信息
    ''' <summary>
    ''' 右键弹出窗口
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' 保存修改的数据
    ''' </summary>
    ''' <param name="dataNode"></param>
    ''' <param name="ParentID"></param>
    ''' <remarks></remarks>
    Private Sub SaveModiDB(dataNode As TreeNode, ParentID As Integer)
        Dim rows() As DataRow = G_dt.Select("bmbh=" + dataNode.Name)
        Dim row As DataRow
        For Each row In rows
            row("bmmc") = dataNode.Text
            row("parentBMBH") = ParentID
        Next
        sda.Update(G_dt)
        'OpreaBMDataBase()
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
            ElseIf TreeOperateType = TREE_ADD_SUB_NODE Then
                'node.Nodes.Add(TextBox1.Text & (node.GetNodeCount(False) + 1))

                TreeView1.SelectedNode.Nodes.Add(node)
                Me.SaveAddDB(node, SelectedNode.Name)
            ElseIf TreeOperateType = TREE_UPDATE_NODE Then
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
        TextBox1.Enabled = False
        TextBox2.Enabled = True
        'TreeView1.Refresh()
        TreeView1.Enabled = False
        Button1.Enabled = True
    End Sub
    Private Sub DisableWrite()
        TextBox1.Text = ""
        TextBox2.Text = ""
        Button1.Enabled = False
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
        IsInputNum(e)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            'BindTreeView(0, TreeView1, G_dt)

            OpreaRYDataBase("")

            OpreaBMDataBase()
            CommBindTreeView(0, TreeView1, G_dt, "parentBMBH", "0", "BMMC", "BMBH")

            DisplayBMTree()
            ComboBoxTreeBM.Enabled = False
            DisableWrite()
            TreeView1.ShowNodeToolTips = True
            'TreeView1.Nodes.
            TreeView1.Nodes(0).Expand()
        Catch ex As SQLite.SQLiteException
            MsgBox(ex.Message)
        End Try

    End Sub



    Private Sub OpreaBMDataBase()
        Dim conn As Data.SQLite.SQLiteConnection = New Data.SQLite.SQLiteConnection(CONN_STR)
        '打开连接
        conn.Open()
        Dim sql As String = "select * from bm"
        'ds = SQLite.SQLiteCommand SQLiteHelper.SQLiteCommandDataSet(DBConStr, sqlStr, Nothing)
        'Dim reader As SQLite.SQLiteDataReader = cmd.ExecuteReader()
        sda = New LiuDataAdapter(sql, CONN_STR)


        '#############################################################################################################
        '1.新增-1行/多行-保存-OK
        '2.新增-1行/多行-保存-修改/删除-保存-发生错误
        '3.重新启动界面保存OK()
        '对具有自增列的表进行新增并多次保存，由于第一次保存（插入）后没有获得到自增列的值，再次保存（更新）时失败。
        '解决方法，在获取数据时增加FillSchema（）操作：
        '下面这句太有用了
        '如果不调用FillSchema, 缺省情况下不会填如PrimaryKey信息
        'FillSchema是用来向DataTable中填入详细的元数据信息的，例如(column names, primary key, constraints等)，但不填入数据。
        'Fill主要是用来填入数据的，它在缺省情况下只填入少量必要的元数据信息，例如(column names, data types)。 
        '所以, 一般先用FillSchema来填入详细的元数据信息, 再用Fill来填充数据,
        sda.FillSchema(G_dt, SchemaType.Mapped)
        '#############################################################################################################


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
        G_BMBH = SelectedNode.Name
        OpreaRYDataBase(G_BMBH)
    End Sub


    Private Sub OpreaRYDataBase(bmbh As String)


        'Dim conn As Data.SQLite.SQLiteConnection = New Data.SQLite.SQLiteConnection(CONN_STR)
        '打开连接
        'conn.Open()
        'Dim cmd As SQLite.SQLiteCommand = New SQLite.SQLiteCommand(conn)
        Dim sql As String
        If bmbh = "" Or bmbh = "1" Then
            sql = "select * from bm_ry"
        Else
            sql = "select * from bm_ry where bmbh=" + bmbh
        End If

        'ds = SQLite.SQLiteCommand SQLiteHelper.SQLiteCommandDataSet(DBConStr, sqlStr, Nothing)
        'Dim reader As SQLite.SQLiteDataReader = cmd.ExecuteReader()
        sda_ry = New LiuDataAdapter(sql, CONN_STR)

        G_dt_ry.Clear()

        sda_ry.FillSchema(G_dt_ry, SchemaType.Mapped)
        sda_ry.Fill(G_dt_ry)




        DataGridView1.DataSource = G_dt_ry
        DataGridView1.Columns(0).ReadOnly = True
        DataGridView1.Columns(0).HeaderText = "内部ID(不可编辑)"
        DataGridView1.Columns(1).HeaderText = "姓名"
        DataGridView1.Columns(2).HeaderText = "性别"
        DataGridView1.Columns(3).HeaderText = "部门编号"
        DataGridView1.Columns(4).HeaderText = "固定电话"
        'DataGridView1.Rows(selectRow).Selected = True
        'DataGridView1.FirstDisplayedScrollingRowIndex = DataGridView1.Rows(selectRow).Index
        DataGridView1.Refresh()
        'G_dt.Load(reader)
    End Sub

    Private Sub SaveBMRYinfo()
        sda_ry.Update(G_dt_ry)
        UpdateDB_BMRY_ZC()
    End Sub

    Private Sub UpdateDB_BMRY_ZC()
        Dim sql As String
        Dim da As LiuDataAdapter = New LiuDataAdapter()
        Dim howUpdate As Integer = 0


        '这个更新语句是将在部门信息改变后对资产信息里面的部门及人员信息进行更新，SQLITE与mysql的语法还不一样，不能通用
        sql = "update zc set bmbh=(select  bm_ry.bmbh from bm_ry where bm_ry.xm=zc.zrr),bmmc=(select bminfo.bmmc from bminfo where bminfo.xm=zc.zrr),zrr=(select bminfo.xm from bminfo where bminfo.xm=zc.zrr)"

        'sql = ""

        howUpdate = da.ExecuteNonQuery(sql)
        Debug.Print("更新记录")
        Debug.Print(howUpdate)

        OpreaRYDataBase(G_BMBH)
        'FirstDisplayedScrollingRowIndex()
        'DataGridView1.FirstDisplayedScrollingRowIndex = DataGridView1.Rows(selectRow).Index
        'DataGridView1.CurrentRow.Index = DataGridView1.Rows(selectRow).Index

        'MsgBox("部门人员信息更新成功，同时更新了人员所属设备记录信息！")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        SaveBMRYinfo()

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
            sda_ry.Update(G_dt_ry)
            MsgBox("删除成功")
            Button3.Visible = False
        End If

    End Sub


    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        'DataGridView1.CellValueChanged()
        Dim rs As DataGridViewRow
        Debug.Print(DataGridView1.Rows.Count())
        'For Each rs In DataGridView1.Rows

        '    If rs.State = DataRowState.Added Or rs.State = DataRowState.Detached Or rs.State = DataRowState.Modified Then
        '        Debug.Print("bianji")
        '    Else
        '        Debug.Print("meiyou bianji")
        '    End If


        'Next
        If DataGridView1.IsCurrentCellInEditMode Then
            If MsgBox("放弃正在编辑的数据吗？", MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Exclamation, "警告") = MsgBoxResult.Ok Then
                Me.Dispose()
                Me.Close()
            End If
        Else
            Me.Dispose()
            Me.Close()
        End If

    End Sub

    Private Sub DataGridView1_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles DataGridView1.CellBeginEdit
        If DataGridView1.SelectedRows(0).IsNewRow Then      '如果是新行
            If TextBox1.Text = 1 Then                       '如果是根节点
                If MsgBox("您确定是在根节点下添加人员信息吗？", MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Exclamation, "警告") = MsgBoxResult.Ok Then
                    DataGridView1.SelectedRows(0).Cells(3).Value = TextBox1.Text
                Else
                    '取消cell的编辑状态
                    'DataGridView1.EndEdit()
                    e.Cancel = True
                    'DataGridView1.CurrentCell = Null
                    'SendKeys.Send("{ESCAPE} ")
                    'endKeys.Send("{ESCAPE}")
                End If
            Else
                DataGridView1.SelectedRows(0).Cells(3).Value = TextBox1.Text
            End If

        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        ExportToCSV(DataGridView1)
    End Sub

    Private Sub DisplayBMTree()
        ComboBoxTreeBM = New ComboBoxTreeView()
        ComboBoxTreeBM.Dock = DockStyle.Fill
        Me.Panel2.Controls.Add(ComboBoxTreeBM)

        'OpreaBMDataBase()
        CommBindTreeView(0, ComboBoxTreeBM.TreeView, G_dt, "parentBMBH", "0", "BMMC", "BMBH")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim sql As String
        Dim bmbh As String
        If ComboBoxTreeBM.TreeView.SelectedNode Is Nothing Or TextBox3.Text = "" Or ComboBoxTreeBM.Text = "" Then
            MsgBox("姓名和所属部门不能为空！")
            Return
        End If
        bmbh = ComboBoxTreeBM.TreeView.SelectedNode.Name

        If Button3.Text = "保存新增信息" Then
            sql = "insert into bm_ry(xm,xb,bmbh,tel) values ('" + TextBox3.Text + "','" + ComboBox1.Text + "','" + bmbh + "','" + TextBox5.Text + "')"
            sda_ry.ExecuteNonQuery(sql)
            UpdateDB_BMRY_ZC()
            '添加信息后将光标定在最后一行
            DataGridView1.CurrentCell = DataGridView1(0, DataGridView1.Rows.Count - 1)

            'MsgBox("添加成功！", MsgBoxStyle.OkOnly + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Information, "成功")

        End If
        If Button3.Text = "保存修改信息" Then
            'sql = "insert into bm_ry(xm,xb,bmbh,tel) values ('" + TextBox3.Text + "','" + ComboBox1.Text + "','" + bmbh + "','" + TextBox5.Text + "')"
            sql = "update bm_ry set xm='" + TextBox3.Text + "',xb='" + ComboBox1.Text + "',bmbh='" + ComboBoxTreeBM.TreeView.SelectedNode.Name + "',tel='" + TextBox5.Text + "' where id=" + modifyID
            sda_ry.ExecuteNonQuery(sql)
            UpdateDB_BMRY_ZC()
            modifyID = "NULL"

            '修改信息后将光标定在修改的行上
            DataGridView1.CurrentCell = DataGridView1(0, selectRow)

            'MsgBox("添加成功！", MsgBoxStyle.OkOnly + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Information, "成功")

        End If

        Button3.Text = "保存"
        Button3.Enabled = False
        SetNew()
        SetAddEditDisable()
    End Sub
    Private Sub SetNew()
        TextBox3.Text = ""
        TextBox5.Text = ""
        ComboBox1.Text = ""
        ComboBoxTreeBM.Text = ""
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

  

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Button3.Text = "保存修改信息"
        Button3.Visible = True

        SetAddEditEnable()
        selectRow = DataGridView1.SelectedRows(0).Index
        modifyID = DataGridView1.SelectedRows(0).Cells(0).Value.ToString()

        TextBox3.Text = DataGridView1.SelectedRows(0).Cells(1).Value.ToString()
        ComboBox1.Text = DataGridView1.SelectedRows(0).Cells(2).Value.ToString()
        TextBox5.Text = DataGridView1.SelectedRows(0).Cells(4).Value.ToString()

        'ComboBoxTreeBM.TreeView.SelectedNode.Name = DataGridView1.SelectedRows(0).Cells(3).Value.ToString
        'DataGridView1.Enabled = False

        Dim bmbh As String = DataGridView1.SelectedRows(0).Cells(3).Value.ToString()

        ComboBoxTreeBM.TreeView.Focus()
        For i As Integer = 0 To ComboBoxTreeBM.TreeView.Nodes.Count - 1
            For j As Integer = 0 To ComboBoxTreeBM.TreeView.Nodes(i).Nodes.Count - 1
                If ComboBoxTreeBM.TreeView.Nodes(i).Nodes(j).Name = bmbh Then
                    ComboBoxTreeBM.TreeView.SelectedNode = ComboBoxTreeBM.TreeView.Nodes(i).Nodes(j)
                    ComboBoxTreeBM.Text = ComboBoxTreeBM.TreeView.Nodes(i).Nodes(j).Text
                    '选中
                    'treeView.Nodes[i].Nodes[j].Checked = true;
                    ComboBoxTreeBM.TreeView.Nodes(i).Expand()
                    '展开父级
                    Return
                End If
            Next
        Next




    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        TextBox3.Text = ""
        TextBox5.Text = ""
        ComboBoxTreeBM.Text = ""
        ComboBox1.Text = ""
        Button3.Text = "保存新增信息"
        SetAddEditEnable()
    End Sub
    Private Sub SetAddEditEnable()
        TextBox3.Enabled = True
        TextBox5.Enabled = True
        ComboBoxTreeBM.Enabled = True
        ComboBox1.Enabled = True
        Button3.Enabled = True
    End Sub
    Private Sub SetAddEditDisable()
        TextBox3.Enabled = False
        TextBox5.Enabled = False
        ComboBoxTreeBM.Enabled = False
        ComboBox1.Enabled = False
        Button3.Enabled = False
        Button3.Text = "保存"
    End Sub
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        SetAddEditDisable()
        SetNew()
    End Sub

    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress

    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged

    End Sub
End Class



