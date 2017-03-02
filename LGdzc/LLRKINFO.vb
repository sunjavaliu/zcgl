﻿Public Class LLRKINFO
    Dim sda As LiuDataAdapter
    Dim IsEditCell As Boolean = False
    Dim TB As DataTable
    Private Sub GetKuCun()
        'MsgBox(System.Environment.GetEnvironmentVariable("SYSTEMROOT"))
        TB = New DataTable()
        'Dim conn As Data.SQLite.SQLiteConnection = New Data.SQLite.SQLiteConnection(CONN_STR)

        ' MsgBox(System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
        '打开连接
        'conn.Open()
        'Dim cmd As SQLite.SQLiteCommand = New SQLite.SQLiteCommand(conn)
        'Dim sql As String = "select * from rk where kucun>0 order by id desc"
        Dim sql As String = "select * from rk order by id desc"
        'ds = SQLite.SQLiteCommand SQLiteHelper.SQLiteCommandDataSet(DBConStr, sqlStr, Nothing)
        'Dim reader As SQLite.SQLiteDataReader = cmd.ExecuteReader()
        sda = New LiuDataAdapter(sql, CONN_STR)
        'Dim scb As SQLite.SQLiteCommandBuilder = New SQLite.SQLiteCommandBuilder(sda)

        'sda.Fill(G_dt)
        sda.Fill(TB)





        DataGridView1.DataSource = TB


        '设置DataGridView可显示隐藏列,用Form的名字保存xml文件
        SetDataGridViewHidenColumn(DataGridView1, Me.Name.ToString())


        'DataGridView1.Rows(0).Cells(0).Height = 90
        DataGridView1.ColumnHeadersHeight = 46
        DataGridView1.Columns(0).ReadOnly = True
        DataGridView1.Columns(0).HeaderText = "ID"

        DataGridView1.Columns(1).HeaderText = "资产类别编号（国标）"
        DataGridView1.Columns(2).HeaderText = "资产类别名称（国标）"
        DataGridView1.Columns(3).HeaderText = "资产名称"
        DataGridView1.Columns(4).HeaderText = "采购方式"
        DataGridView1.Columns(5).HeaderText = "供货商"
        DataGridView1.Columns(6).HeaderText = "购置日期"
        DataGridView1.Columns(7).HeaderText = "到货日期"
        DataGridView1.Columns(8).HeaderText = "单价"
        DataGridView1.Columns(9).HeaderText = "采购数量"
        DataGridView1.Columns(10).HeaderText = "计量单位"
        DataGridView1.Columns(11).HeaderText = "签收人"
        DataGridView1.Columns(12).HeaderText = "采购项目名称"
        DataGridView1.Columns(13).HeaderText = "简单配置"
        DataGridView1.Columns(14).HeaderText = "库存"
        DataGridView1.Columns(15).HeaderText = "入库编号"
        DataGridView1.Columns(16).HeaderText = "资产来源"
        DataGridView1.Columns(17).HeaderText = "设备型号"
        DataGridView1.Columns(18).HeaderText = "品牌"
        DataGridView1.Columns(19).HeaderText = "备注"

        'DataGridView1.Columns(17).DisplayIndex = 3
        'DataGridView1.Columns(14).DisplayIndex = 4
        'DataGridView1.Columns(9).DisplayIndex = 5

        'DataGridView1.Columns(3).Frozen = True
        'DataGridView1.Columns

        'dataAdapter.SelectCommand = cmd
        'dataAdapter.Fill(dst, "info")
        'dt = dst.Tables("info")
        'sqlConnection1.Close()   '关闭数据库   


        'DataGridView1.AutoGenerateColumns = True '自动创建列   
        'DataGridView1.DataSource = sda

    End Sub






    Private Sub LLRKINFO_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        GetKuCun()

        ComboBox1.Items.Add(New ComboboxItem("对所有单元格自动调整列宽", DataGridViewAutoSizeColumnsMode.AllCells))
        ComboBox1.Items.Add(New ComboboxItem("对内容单元格自动调整列宽", DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader))
        ComboBox1.Items.Add(New ComboboxItem("对标题单元格自动调整", DataGridViewAutoSizeColumnsMode.ColumnHeader))
        ComboBox1.Items.Add(New ComboboxItem("对可见单元格自动调整列宽，含标题", DataGridViewAutoSizeColumnsMode.DisplayedCells))
        ComboBox1.Items.Add(New ComboboxItem("对可见单元格自动调整列宽，不含标题", DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader))
        ComboBox1.Items.Add(New ComboboxItem("列宽正好填充控件的显示区域", DataGridViewAutoSizeColumnsMode.Fill))
        ComboBox1.Items.Add(New ComboboxItem("列宽不自动调整", DataGridViewAutoSizeColumnsMode.None))


        'DataGridView1.ContextMenuStrip = Me.ContextMenuStrip1
        DataGridView1.Rows(0).ContextMenuStrip = Me.ContextMenuStrip1

        '设置DataGridView显示风格
        SetDataGridViewStyle(DataGridView1)


    End Sub



    '#region DGV直接编辑修改数据的功能
    '    /// <summary>
    '    /// 用来存放DGV单元格修改之前值
    '    /// </summary>
    '    Object cellTempValue = null;

    '    /// <summary>
    '    /// DGV单元格开始编辑时触发的事件
    '    /// </summary>
    '    /// <param name="sender"></param>
    '    /// <param name="e"></param>
    '    private void DGVMain_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
    '    {
    '        cellTempValue = DGVMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
    '    }

    '    /// <summary>
    '    /// DGV单元格结束编辑时触发的事件
    '    /// </summary>
    '    /// <param name="sender"></param>
    '    /// <param name="e"></param>
    '    private void DGVMain_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    '    {
    '        //判断编辑前后的值是否一样（是否修改了内容）
    '        if (Object.Equals(cellTempValue, DGVMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
    '        {
    '            //如果没有修改，则返回
    '            return;
    '        }

    '        //判断用户是否确定修改
    '        if (MessageBox.Show("确定修改?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.None) != DialogResult.OK)
    '        {
    '            //如果不修改，恢复原来的值
    '            DGVMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = cellTempValue;
    '            return;
    '        }

    '        //修改数据库的数据
    '        string sql = String.Format("update  set {1}='{2}' where 商品编号='{3}'",
    '            DGVMain.Columns[0].DataPropertyName,　　　　　　　　    //所选单元格列名
    '            DGVMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value,    //所选单元格修改后的值
    '            DGVMain.Rows[e.RowIndex].Cells[0].Value                　　//所选行的商品编号
    '        );

    '        try
    '        {
    '            OleDbHelper.ExecuteNonQuery(CommandType.Text, sql);
    '        }
    '        catch (OleDbException ex)
    '        {
    '            MessageBox.Show(ex.Message);
    '        }

    '        //刷新数据
    '        LoadDGV();
    '    } 
    '    #endregion

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If IsEditCell Then
            If MsgBox("您编辑过的数据还未保存，确定不保存数据退出吗?", MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Exclamation, "警告") = MsgBoxResult.Ok Then
                sda.Dispose()
                Me.Close()
            End If
        Else
            sda.Dispose()
            Me.Close()
        End If

    End Sub

    Private Sub SplitContainer1_Panel2_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel2.Paint

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        sda.Update(TB)
        MsgBox("更新成功")
        IsEditCell = False
    End Sub

    Private Sub DataGridView1_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles DataGridView1.CellBeginEdit
        IsEditCell = True
    End Sub

    Private Sub DataGridView1_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseUp

        'if (e.Button == System.Windows.Forms.MouseButtons.Left)
        '   {
        '       this.contextMenuStrip1.Show(pictureBox1,new Point(e.X, e.Y));
        '   }

        'If e.Button = Windows.Forms.MouseButtons.Right Then
        '    Me.ContextMenuStrip1.Show(Me, e.)
        '    'Me.ContextMenuStrip1.Show(Me, DataGridView1.PointToScreen(New Point(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y)))

        '    'MsgBox(DataGridView1.PointToScreen(New Point(System.Windows.Forms.Cursor.Position.X.ToString(), System.Windows.Forms.Cursor.Position.Y.ToString)))

        'End If



        If e.Button = MouseButtons.Right Then
            If e.RowIndex >= 0 Then
                '若行已是选中状态就不再进行设置
                If DataGridView1.Rows(e.RowIndex).Selected = False Then
                    DataGridView1.ClearSelection()
                    DataGridView1.Rows(e.RowIndex).Selected = True
                End If
                '只选中一行时设置活动单元格
                If DataGridView1.SelectedRows.Count = 1 Then
                    DataGridView1.CurrentCell = DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex)
                End If
                '弹出操作菜单
                ContextMenuStrip1.Show(MousePosition.X, MousePosition.Y)
            End If
        End If

    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If (e.KeyCode = Keys.Tab And DataGridView1.CurrentCell.ColumnIndex = 1) Then

            e.Handled = True


            DataGridView1.CurrentCell = DataGridView1.Rows(0).Cells(0)
            DataGridView1.BeginEdit(True)
        End If
    End Sub

    Private Sub DataGridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DataGridView1.KeyPress
        IsEditCell = True
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ExportToCSV(DataGridView1)
    End Sub



    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim dis As Integer = DirectCast(ComboBox1.SelectedItem, ComboboxItem).Value
        DataGridView1.AutoSizeColumnsMode = dis
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim i, rowN As Integer
        Dim tmpList As New List(Of DataGridViewRow)()
        If DataGridView1.SelectedRows.Count > 0 Then

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
                sda.Update(TB)
                'MsgBox("删除成功")
            End If
        Else
            MsgBox("您未选择将要删除的记录。" & vbCrLf & "请在记录的最前面空白区选择整条记录，然后点击删除按钮！", MsgBoxStyle.OkOnly + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Exclamation, "警告")
        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Dim sql As String
        Dim sdazrr As LiuDataAdapter
        Dim tb As DataTable = New DataTable
        sql = "select * from zc where rkbh='" + DataGridView1.SelectedRows(0).Cells(15).Value.ToString() + "'"
        'MsgBox(sql)
        sdazrr = New LiuDataAdapter(sql, CONN_STR)
        sdazrr.Fill(tb)
        DataGridView2.DataSource = tb

        DataGridView2.Refresh()

    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        ContextMenuStrip1.Close()
    End Sub
End Class





