Public Class LLRKZC
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
        'DataGridView1.Columns(3).Frozen = True
        'DataGridView1.Columns

        'dataAdapter.SelectCommand = cmd
        'dataAdapter.Fill(dst, "info")
        'dt = dst.Tables("info")
        'sqlConnection1.Close()   '关闭数据库   


        'DataGridView1.AutoGenerateColumns = True '自动创建列   
        'DataGridView1.DataSource = sda

    End Sub




  

    Private Sub LLRKZC_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        GetKuCun()
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




    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

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
End Class


