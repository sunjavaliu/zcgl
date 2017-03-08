Public Class GLRKXX
    Dim sda As LiuDataAdapter
    Dim IsEditCell As Boolean = False
    Dim TB As DataTable
    Dim czzc As zcglStruct

    Private Sub GetKuCun()
        'MsgBox(System.Environment.GetEnvironmentVariable("SYSTEMROOT"))
        TB = New DataTable()
        'Dim conn As Data.SQLite.SQLiteConnection = New Data.SQLite.SQLiteConnection(CONN_STR)

        ' MsgBox(System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
        '打开连接
        'conn.Open()
        'Dim cmd As SQLite.SQLiteCommand = New SQLite.SQLiteCommand(conn)
        'Dim sql As String = "select * from rk where kucun>0 order by id desc"
        Dim sql As String = "select * from zc where   czbh is null or czbh='' order by  zcdj desc "
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
        SetColumnsTitle4ZC(DataGridView1)

        'DataGridView1.Columns(0).HeaderText = "ID"

        'DataGridView1.Columns(1).HeaderText = "资产类别编号（国标）"
        'DataGridView1.Columns(2).HeaderText = "资产类别名称（国标）"
        'DataGridView1.Columns(3).HeaderText = "资产名称"
        'DataGridView1.Columns(4).HeaderText = "采购方式"
        'DataGridView1.Columns(5).HeaderText = "供货商"
        'DataGridView1.Columns(6).HeaderText = "购置日期"
        'DataGridView1.Columns(7).HeaderText = "到货日期"
        'DataGridView1.Columns(8).HeaderText = "单价"
        'DataGridView1.Columns(9).HeaderText = "采购数量"
        'DataGridView1.Columns(10).HeaderText = "计量单位"
        'DataGridView1.Columns(11).HeaderText = "签收人"
        'DataGridView1.Columns(12).HeaderText = "采购项目名称"
        'DataGridView1.Columns(13).HeaderText = "简单配置"
        'DataGridView1.Columns(14).HeaderText = "库存"
        'DataGridView1.Columns(15).HeaderText = "入库编号"
        'DataGridView1.Columns(16).HeaderText = "资产来源"
        'DataGridView1.Columns(17).HeaderText = "设备型号"
        'DataGridView1.Columns(18).HeaderText = "品牌"
        'DataGridView1.Columns(19).HeaderText = "备注"



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
        GetCZInfo()

        ComboBox1.Items.Add(New ComboboxItem("对所有单元格自动调整列宽", DataGridViewAutoSizeColumnsMode.AllCells))
        ComboBox1.Items.Add(New ComboboxItem("对内容单元格自动调整列宽", DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader))
        ComboBox1.Items.Add(New ComboboxItem("对标题单元格自动调整", DataGridViewAutoSizeColumnsMode.ColumnHeader))
        ComboBox1.Items.Add(New ComboboxItem("对可见单元格自动调整列宽，含标题", DataGridViewAutoSizeColumnsMode.DisplayedCells))
        ComboBox1.Items.Add(New ComboboxItem("对可见单元格自动调整列宽，不含标题", DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader))
        ComboBox1.Items.Add(New ComboboxItem("列宽正好填充控件的显示区域", DataGridViewAutoSizeColumnsMode.Fill))
        ComboBox1.Items.Add(New ComboboxItem("列宽不自动调整", DataGridViewAutoSizeColumnsMode.None))


        GetComboBoxDICT("xh", "xh", ComboBox4)

        GetComboBoxDICT("lbmc", "lbmc", ComboBox2)

        GetComboBoxDICT("zcmc", "zcmc", ComboBox3)

        ComboBox1.Text = ""
        ComboBox2.Text = ""
        ComboBox3.Text = ""


        'DataGridView1.ContextMenuStrip = Me.ContextMenuStrip1
        DataGridView1.Rows(0).ContextMenuStrip = Me.ContextMenuStrip1

        '设置DataGridView显示风格
        SetDataGridViewStyle(DataGridView1)
        SetDataGridViewStyle(DataGridView2)
        SetDataGridViewHidenColumn(DataGridView1, Me.Name.ToString() + "DataGridView1")
        SetDataGridViewHidenColumn(DataGridView2, Me.Name.ToString() + "DataGridView2")
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
        sda.Update(TB)

        'sql = "UPDATE zc ,rk set  zc.jldw= rk.jldw ,zc.lbid=rk.lbid,zc.lbmc=rk.lbmc,zc.pz=rk.pz,zc.zcdj=rk.price,zc.zcmc=rk.zcmc,zc.zcxh=rk.zcxh,zc.zcpp=rk.zcpp,zc.gzrq=rk.gzrq where rk.rkbh=zc.rkbh"
        'upSDA.ExecuteNonQuery(sql)

        MsgBox("入库记录已经更新成功，并将领用记录中的计量单位，类别ID等信息也同步更新！", MsgBoxStyle.OkOnly + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Information, "提示")

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
        sql = "select * from zc   "
        If Trim(ComboBox4.Text) <> "" Then sqlxh = "zcxh='" + Trim(ComboBox4.Text) + "'"
        If Trim(ComboBox2.Text) <> "" Then sqllb = "lbmc='" + Trim(ComboBox2.Text) + "'"
        If Trim(ComboBox3.Text) <> "" Then sqlzcmc = "zcmc='" + Trim(ComboBox3.Text) + "'"
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

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        GetCZInfo()
    End Sub

    Private Sub GetCZInfo()
        Dim zcxxTb As DataTable = New DataTable
        Dim sql As String
        sql = "select * from czzcinfo where isconnect is null or isconnect ='' or isconnect=false  order by dj desc "
        Dim zcxx As LiuDataAdapter = New LiuDataAdapter(sql, CONN_STR)
        'Dim dt As DataTable = New DataTable()
        'MsgBox(sql)
        zcxx = New LiuDataAdapter(sql, CONN_STR)

        zcxxTb.Clear()
        zcxx.Fill(zcxxTb)

        DataGridView2.DataSource = zcxxTb


        SetColumnsTitle4CZ(DataGridView2)

    End Sub

    Public Sub SetColumnsTitle4CZ(DataGridView1 As DataGridView)
        DataGridView1.Columns(0).HeaderText = "资产名称"
        DataGridView1.Columns(1).HeaderText = "资产代码"
        DataGridView1.Columns(2).HeaderText = "类别代码（国标）"
        DataGridView1.Columns(3).HeaderText = "类别名称（国标）"
        DataGridView1.Columns(4).HeaderText = "单价"
        DataGridView1.Columns(5).HeaderText = "数量"
        DataGridView1.Columns(6).HeaderText = "总价"
        DataGridView1.Columns(7).HeaderText = "购置日期"
        DataGridView1.Columns(8).HeaderText = "登记日期"
        DataGridView1.Columns(9).HeaderText = "资产信息"
        DataGridView1.Columns(10).HeaderText = "是否关联"

    End Sub

    ''' <summary>
    ''' 以下三个是实现
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub DataGridView2_MouseDown(sender As Object, e As MouseEventArgs) Handles DataGridView2.MouseDown
    '    Dim a As New List(Of String)()
    '    Dim info As DataGridView.HitTestInfo = DataGridView2.HitTest(e.X, e.Y)
    '    a.Add(DataGridView2.Rows(info.RowIndex).Cells(0).Value.ToString())
    '    a.Add(DataGridView2.Rows(info.RowIndex).Cells(1).Value.ToString())
    '    a.Add(DataGridView2.Rows(info.RowIndex).Cells(2).Value.ToString())

    '    DataGridView2.DoDragDrop(a, DragDropEffects.Move)
    '    DataGridView2.Rows.RemoveAt(info.RowIndex)
    'End Sub

    'Private Sub DataGridView1_DragDrop(sender As Object, e As DragEventArgs) Handles DataGridView1.DragDrop
    '    'MsgBox("bb")
    '    Dim robj As List(Of String) = DirectCast(e.Data.GetData(GetType(List(Of String))), List(Of String))
    '    Dim b As Object() = {robj(0), robj(1), robj(2)}
    '    DataGridView1.Rows.Add(b)

    'End Sub

    'Private Sub DataGridView1_DragEnter(sender As Object, e As DragEventArgs) Handles DataGridView1.DragEnter
    '    'MsgBox("aa")
    '    Dim bIsList As Boolean = (e.Data.GetDataPresent(GetType(List(Of String))) = True)
    '    If bIsList Then
    '        e.Effect = DragDropEffects.Move
    '    End If
    'End Sub

    'Dim str1 As String = ""
    ''全局变量，存放拖动单元格value      
    'Dim nRow As Integer
    ''记录被拖动单元格行标
    'Dim nColumn As Integer
    ''记录被拖动单元格列标
    'Private Sub DataGridView2_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseDown



    '    nRow = e.RowIndex
    '    nColumn = e.ColumnIndex
    '    If Me.DataGridView2.Rows(e.RowIndex).Cells(e.ColumnIndex).Value IsNot Nothing Then
    '        str1 = Me.DataGridView2.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString()
    '    End If

    'End Sub

    'Private Sub DataGridView2_CellMouseMove(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseMove

    '    If e.Button = MouseButtons.Left Then
    '        If str1 <> "" Then
    '            DataGridView2.DoDragDrop(str1, DragDropEffects.Move)
    '        End If
    '    End If
    'End Sub



    'Private Function GetRowFromPoint(x As Integer, y As Integer) As Point
    '    Dim nX As Integer = -1
    '    Dim nY As Integer = -1
    '    For i As Integer = 0 To DataGridView1.RowCount - 1
    '        Dim rec As Rectangle = DataGridView1.GetRowDisplayRectangle(i, False)

    '        If DataGridView1.RectangleToScreen(rec).Contains(x, y) Then
    '            nX = i
    '        End If
    '    Next
    '    For nI As Integer = 0 To DataGridView1.Columns.Count - 1
    '        Dim rec As Rectangle = DataGridView1.GetColumnDisplayRectangle(nI, False)
    '        If DataGridView1.RectangleToScreen(rec).Contains(x, y) Then
    '            nY = nI
    '        End If
    '    Next
    '    Return New Point(nX, nY)
    'End Function

    'Private Sub DataGridView1_DragOver(sender As Object, e As DragEventArgs) Handles DataGridView1.DragOver

    '    If e.Data.GetDataPresent(GetType(String)) Then
    '        e.Effect = DragDropEffects.Move
    '    End If

    'End Sub


    'Private Sub DataGridView1_DragDrop(sender As Object, e As DragEventArgs) Handles DataGridView1.DragDrop

    '    Dim point As Point = GetRowFromPoint(e.X, e.Y)
    '    'Me.DataGridView1.Rows(nRow).Cells(nColumn).Value = Nothing
    '    Me.DataGridView1.Rows(point.X).Cells(point.Y).Value = str1

    'End Sub



    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim cz As zcglStruct = New zcglStruct
        Dim total As Double = 0.0
        'Dim FenPeiZiChan As List(Of zcglStruct) = New List(Of zcglStruct)

        Dim tmp As zcglStruct
        Dim sql, zcbhStr, rkbhStr As String
        zcbhStr = ""
        rkbhStr = ""
        'For i = 0 To DataGridView2.RowCount - 1
        'cz.dj = DataGridView2.SelectedRows(0).Cells()
        'Next

        If DataGridView2.SelectedRows.Count > 0 Then

            cz.czbh = DataGridView2.SelectedRows(0).Cells(1).Value
            cz.dj = DataGridView2.SelectedRows(0).Cells(4).Value
            cz.gzrq = DataGridView2.SelectedRows(0).Cells(7).Value
            cz.sl = DataGridView2.SelectedRows(0).Cells(5).Value

            If DataGridView1.SelectedRows.Count > 0 Then
                For i = 0 To DataGridView1.RowCount - 1
                    If DataGridView1.Rows(i).Selected Then
                        tmp = New zcglStruct
                        tmp.gzrq = DataGridView1.Rows(i).Cells(6).Value
                        tmp.dj = DataGridView1.Rows(i).Cells(10).Value
                        tmp.zcbh = DataGridView1.Rows(i).Cells(1).Value
                        tmp.rkbh = DataGridView1.Rows(i).Cells(18).Value
                        zcbhStr = zcbhStr + "'" + tmp.zcbh + "',"
                        rkbhStr = rkbhStr + "'" + tmp.rkbh + "',"
                        Dim rq As DateTime
                        'rq = Format(cz.gzrq, "yyyy年mm月dd日")
                        'rq = Date.ToString("yyyy年MM月", DateTimeFormatInfo.InvariantInfo)
                        rq = Convert.ToDateTime(cz.gzrq)

                        If rq.ToLongDateString().ToString() = tmp.gzrq Then

                            'FenPeiZiChan.Add(tmp)
                            total = total + CDbl(tmp.dj)

                        Else
                            MsgBox("购置日期不一致")

                        End If
                    End If
                Next


                If total = cz.dj * cz.sl Then
                    sql = "update zc set czbh='" + cz.czbh + "' where zcbh in (" + zcbhStr + "'')"
                    Dim sd As LiuDataAdapter = New LiuDataAdapter
                    sd.ExecuteNonQuery(sql)
                    'Debug.Print(sql)
                    'MsgBox(sql)
                    'End If

                    sql = "update czzcinfo set isconnect=true where zcdm='" + cz.czbh + "'"
                    Dim czSDA As LiuDataAdapter = New LiuDataAdapter
                    czSDA.ExecuteNonQuery(sql)

                    'sql = "UPDATE RK SET czbh='" + cz.czbh + "' where rkbh in (" + rkbhStr + "'')"
                    'Dim rkSDA As LiuDataAdapter
                    'rkSDA.ExecuteNonQuery(sql)

                    MsgBox("关联成功")

                    GetKuCun()
                    GetCZInfo()



                Else
                    MsgBox("总价不一致啊")
                End If
            End If



            'For Each s In DataGridView1.SelectedRows
            '    'DataGridView1.SelectedRows(i).Cells()
            '    Debug.Print(s)
            'Next
        End If
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub
End Class






