Public Class Form3

    Private TreeOperateType As String



    Dim sda As LiuDataAdapter   ';//全局变量
    Dim G_dt As DataTable = New DataTable()

    Dim sda_ry As LiuDataAdapter   ';//全局变量
    Dim G_dt_ry As DataTable = New DataTable()

    Dim commSQL As String   '全局SQL

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

            GetComboBoxDICT("xh", "xh", ComboBox1)

            GetComboBoxDICT("lbmc", "lbmc", ComboBox2)

            GetComboBoxDICT("zcmc", "zcmc", ComboBox3)

            ComboBox1.Text = ""
            ComboBox2.Text = ""
            ComboBox3.Text = ""
            TextBox1.Text = ""
            '设置DataGridView显示风格
            SetDataGridViewStyle(DataGridView1)

        Catch ex As SQLite.SQLiteException
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs)
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
        'Dim sql As String = "select * from bm"

        Dim sql As String = "select * from v_bm"

        'ds = SQLite.SQLiteCommand SQLiteHelper.SQLiteCommandDataSet(DBConStr, sqlStr, Nothing)
        'Dim reader As SQLite.SQLiteDataReader = cmd.ExecuteReader()
        sda = New LiuDataAdapter(sql, CONN_STR)


        sda.Fill(G_dt)

        'G_dt.Load(reader)
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        Dim selectBMBH As String    '选择的部门ID
        Dim selectBMMC As String    '选择的部门名称
        Dim SelectedNode As TreeNode = TreeView1.SelectedNode

        selectBMBH = SelectedNode.Name
        selectBMMC = SelectedNode.Text

        '如果下级子节点的个数为0表示就是末级节点
        If SelectedNode.Nodes.Count = 0 Then
            '获取个人拥有的设备
            GetZcInfo4Name(selectBMMC)
        Else
            '获取部门拥有的设备
            OpreaRYDataBase(selectBMBH)
        End If

    End Sub


    Private Sub OpreaRYDataBase(bmbh As String)


        'Dim conn As Data.SQLite.SQLiteConnection = New Data.SQLite.SQLiteConnection(CONN_STR)
        '打开连接
        'conn.Open()
        'Dim cmd As SQLite.SQLiteCommand = New SQLite.SQLiteCommand(conn)
        Dim sql As String
        If bmbh = "" Or bmbh = "1" Then
            sql = "select * from zc"
        Else
            sql = "select * from zc where bmbh=" + bmbh
        End If


        commSQL = sql


        'ds = SQLite.SQLiteCommand SQLiteHelper.SQLiteCommandDataSet(DBConStr, sqlStr, Nothing)
        'Dim reader As SQLite.SQLiteDataReader = cmd.ExecuteReader()
        sda_ry = New LiuDataAdapter(sql, CONN_STR)


        G_dt_ry.Clear()
        sda_ry.Fill(G_dt_ry)

        DataGridView1.DataSource = G_dt_ry


        '设置DataGridView可显示隐藏列,用Form的名字保存xml文件
        SetDataGridViewHidenColumn(DataGridView1, Me.Name.ToString())



        DataGridView1.Columns(0).ReadOnly = True

        '设置资产表的列标题
        SetColumnsTitle4ZC(DataGridView1)


    End Sub

    Private Sub ReOpenDB(sql As String)


        'Dim conn As Data.SQLite.SQLiteConnection = New Data.SQLite.SQLiteConnection(CONN_STR)
        '打开连接
        'conn.Open()
        'Dim cmd As SQLite.SQLiteCommand = New SQLite.SQLiteCommand(conn)
        'Dim sql As String
        'If bmbh = "" Or bmbh = "1" Then
        '    sql = "select * from zc"
        'Else
        '    sql = "select * from zc where bmbh=" + bmbh
        'End If


        'commSQL = sql


        'ds = SQLite.SQLiteCommand SQLiteHelper.SQLiteCommandDataSet(DBConStr, sqlStr, Nothing)
        'Dim reader As SQLite.SQLiteDataReader = cmd.ExecuteReader()
        sda_ry = New LiuDataAdapter(sql, CONN_STR)


        G_dt_ry.Clear()
        sda_ry.Fill(G_dt_ry)

        DataGridView1.DataSource = G_dt_ry


        '设置DataGridView可显示隐藏列,用Form的名字保存xml文件
        SetDataGridViewHidenColumn(DataGridView1, Me.Name.ToString())



        DataGridView1.Columns(0).ReadOnly = True

        '设置资产表的列标题
        SetColumnsTitle4ZC(DataGridView1)

        'DataGridView1.Columns("ossn").Width=
        'DataGridView1.Columns(3).Frozen = True
        'G_dt.Load(reader)
    End Sub



    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'Dim SCB = New SQLite.SQLiteCommandBuilder(sda_ry)
        Dim UpdateCount As Integer
        UpdateCount = sda_ry.Update(G_dt_ry)
        MsgBox("更新成功！", MsgBoxStyle.OkOnly + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Information, "提示")
        ReOpenDB(commSQL)
    End Sub

    Private Sub DelRecord()
        Dim i, rowN As Integer
        Dim tmpList As New List(Of DataGridViewRow)()
        Dim rkbh, sql As String
        Dim updateSDA As LiuDataAdapter

        If DataGridView1.SelectedRows.Count > 0 Then

            If MsgBox("你确认要删除该记录，且入库吗？", MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Exclamation, "警告") = MsgBoxResult.Ok Then
                '删除选中行
                rowN = DataGridView1.Rows.Count
                rowN = rowN - 1
                For i = rowN To 0 Step -1
                    If DataGridView1.Rows(i).Selected = True Then
                        rkbh = DataGridView1.Rows(i).Cells(18).Value.ToString
                        sql = "update rk set kucun=kucun+1 where rkbh='" + rkbh + "'"
                        updateSDA = New LiuDataAdapter
                        updateSDA.ExecuteNonQuery(sql)

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
                'MsgBox("删除成功")
            End If
        Else
            MsgBox("您未选择将要删除的记录。" & vbCrLf & "请在记录的最前面空白区选择整条记录，然后点击删除按钮！", MsgBoxStyle.OkOnly + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Exclamation, "警告")
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        DelRecord()
    End Sub


    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        'MsgBox("DataGridView1_CellContentClick标题单击")
    End Sub


    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        'MsgBox("DataGridView1_CellDoubleClick")
    End Sub
    ''' <summary>
    ''' 
    ''' 显示行号
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DataGridView1_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridView1.RowsAdded
        DisplayDataGridViewRowNumber(DataGridView1, e)
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

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        Form7.ShowDialog()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs)
        ExportToCSV(DataGridView1)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim sql As String = ""
        Dim sqlwhere As String = ""
        Dim sqlxh As String = ""
        Dim sqllb As String = ""
        Dim sqlzrr As String = ""
        Dim sqlzcmc As String = ""
        sql = "select * from zc   "
        If Trim(ComboBox1.Text) <> "" Then sqlxh = "zcxh='" + Trim(ComboBox1.Text) + "'"
        If Trim(ComboBox2.Text) <> "" Then sqllb = "lbmc='" + Trim(ComboBox2.Text) + "'"
        If Trim(ComboBox3.Text) <> "" Then sqlzcmc = "zcmc='" + Trim(ComboBox3.Text) + "'"
        If Trim(TextBox1.Text) <> "" Then sqlzrr = "zrr like '%" + Trim(TextBox1.Text) + "%'"

        If sqlxh <> "" Then sqlwhere = sqlxh

        If sqllb <> "" Then

            If sqlwhere = "" Then
                sqlwhere = sqllb
            Else
                sqlwhere = sqlwhere + " and " + sqllb
            End If

        End If

        If sqlzcmc <> "" Then

            If sqlwhere = "" Then
                sqlwhere = sqlzcmc
            Else
                sqlwhere = sqlwhere + " and " + sqlzcmc
            End If

        End If

        If sqlzrr <> "" Then

            If sqlwhere = "" Then
                sqlwhere = sqlzrr
            Else
                sqlwhere = sqlwhere + " and " + sqlzrr
            End If

        End If

        sql = sql + " where " + sqlwhere

        commSQL = sql

        Dim querysql As LiuDataAdapter = New LiuDataAdapter(sql, CONN_STR)
        Dim dt As DataTable = New DataTable()

        sda_ry = New LiuDataAdapter(sql, CONN_STR)
        G_dt_ry.Clear()
        sda_ry.Fill(G_dt_ry)
        DataGridView1.DataSource = G_dt_ry

    End Sub

    Private Sub GetZcInfo4Name(name As String)

        Dim sql As String
        sql = "select * from zc where zrr='" + name + "'"
        commSQL = sql
        sda_ry = New LiuDataAdapter(sql, CONN_STR)
        G_dt_ry.Clear()
        sda_ry.Fill(G_dt_ry)
        DataGridView1.DataSource = G_dt_ry

    End Sub



    Private Sub Button8_Click_1(sender As Object, e As EventArgs) Handles Button8.Click
        ExportToCSV(DataGridView1)
    End Sub

    ''' <summary>
    ''' 调整list的高度
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ComboBox3_DrawItem(sender As Object, e As DrawItemEventArgs) Handles ComboBox3.DrawItem
        If e.Index < 0 Then
            Return
        End If
        e.DrawBackground()
        e.DrawFocusRectangle()
        e.Graphics.DrawString(ComboBox3.Items(e.Index).ToString(), e.Font, New SolidBrush(e.ForeColor), e.Bounds.X, e.Bounds.Y + 50)

    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub
End Class



