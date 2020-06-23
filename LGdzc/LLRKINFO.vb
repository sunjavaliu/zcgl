Public Class LLRKINFO
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





        'DataGridView1.Rows(0).Cells(0).Height = 90
        DataGridView1.ColumnHeadersHeight = 46
        DataGridView1.Columns(0).ReadOnly = True
        SetColumns4RK(DataGridView1)



        '设置DataGridView可显示隐藏列,用Form的名字保存xml文件
        SetDataGridViewHidenColumn(DataGridView1, Me.Name.ToString() + "DataGridView1")


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


        For i = Year(Now) To 1990 Step -1
            ComboBox5.Items.Add(i)
        Next i


        GetComboBoxDICT("xh", "xh", ComboBox4)

        GetComboBoxDICT("lbmc", "lbmc", ComboBox2)

        GetComboBoxDICT("zcmc", "zcmc", ComboBox3)

        ComboBox1.Text = ""
        ComboBox2.Text = ""
        ComboBox3.Text = ""
        ComboBox5.Text = ""


        'DataGridView1.ContextMenuStrip = Me.ContextMenuStrip1
        DataGridView1.Rows(0).ContextMenuStrip = Me.ContextMenuStrip1

        '设置DataGridView显示风格
        SetDataGridViewStyle(DataGridView1)
        SetDataGridViewStyle(DataGridView2)




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
        Dim sql As String
        Dim upSDA As LiuDataAdapter = New LiuDataAdapter
        Dim iCount As Integer
        sda.Update(TB)

        sql = "UPDATE zc ,rk set  zc.jldw= rk.jldw ,zc.lbid=rk.lbid,zc.lbmc=rk.lbmc,zc.pz=rk.pz,zc.zcdj=rk.price,zc.zcmc=rk.zcmc,zc.zcxh=rk.zcxh,zc.zcpp=rk.zcpp,zc.gzrq=rk.gzrq where rk.rkbh=zc.rkbh"
        iCount = upSDA.ExecuteNonQuery(sql)

        MsgBox("入库记录已经更新成功，并将领用记录中的计量单位，类别ID等【" + CStr(iCount) + "条】信息也同步更新！", MsgBoxStyle.OkOnly + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Information, "提示")

        IsEditCell = False
    End Sub

    Private Sub DataGridView1_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles DataGridView1.CellBeginEdit
        IsEditCell = True
    End Sub

    Private Sub DataGridView1_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseUp

        If e.Button = MouseButtons.Right Then
            If e.RowIndex >= 0 Then
                '若行已是选中状态就不再进行设置
                If DataGridView1.Rows(e.RowIndex).Selected = False Then
                    DataGridView1.ClearSelection()
                    DataGridView1.Rows(e.RowIndex).Selected = True
                End If
                '只选中一行时设置活动单元格
                If DataGridView1.SelectedRows.Count = 1 Then
                    If e.ColumnIndex >= 0 Then
                        DataGridView1.CurrentCell = DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex)
                    End If
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

        SetColumnsTitle4ZC(DataGridView2)
        SetDataGridViewHidenColumn(DataGridView2, Me.Name.ToString() + "DataGridView2")

    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        ContextMenuStrip1.Close()
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
    ''' <summary>
    ''' 
    ''' 显示行号
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DataGridView2_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridView2.RowsAdded
        DisplayDataGridViewRowNumber(DataGridView2, e)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim sql As String = ""
        Dim sqlwhere As String = ""
        Dim sqlxh As String = ""
        Dim sqllb As String = ""
        Dim sqlzrr As String = ""
        Dim sqlzcmc As String = ""
        Dim sqlgzrq As String = ""
        sql = "select * from rk   "
        If Trim(ComboBox4.Text) <> "" Then sqlxh = "zcxh like '%" + Trim(ComboBox4.Text) + "%'"
        If Trim(ComboBox2.Text) <> "" Then sqllb = "lbmc like '%" + Trim(ComboBox2.Text) + "%'"
        If Trim(ComboBox3.Text) <> "" Then sqlzcmc = "zcmc like '%" + Trim(ComboBox3.Text) + "%'"
        If Trim(ComboBox5.Text) <> "" Then sqlgzrq = "gzrq like '%" + Trim(ComboBox5.Text) + "%'"

        'If Trim(TextBox1.Text) <> "" Then sqlzrr = "zrr like '%" + Trim(TextBox1.Text) + "%'"

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

        If sqlgzrq <> "" Then

            If sqlwhere = "" Then
                sqlwhere = sqlgzrq
            Else
                sqlwhere = sqlwhere + " and " + sqlgzrq
            End If

        End If
        If sqlzrr <> "" Then

            If sqlwhere = "" Then
                sqlwhere = sqlzrr
            Else
                sqlwhere = sqlwhere + " and " + sqlzrr
            End If

        End If

        If sqlwhere <> "" Then
            sql = sql + " where " + sqlwhere
        End If

        'commSQL = sql

        Dim querysql As LiuDataAdapter = New LiuDataAdapter(sql, CONN_STR)
        'Dim dt As DataTable = New DataTable()
        'MsgBox(sql)
        querysql = New LiuDataAdapter(sql, CONN_STR)

        TB.Clear()
        querysql.Fill(TB)
        DataGridView1.DataSource = TB



    End Sub

End Class






