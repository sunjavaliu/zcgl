Public Class Form8
    Dim sda_BM As LiuDataAdapter   ';//全局变量
    Dim dt_BM As DataTable = New DataTable()

    Dim ComboBoxTreeBM As ComboBoxTreeView
    Dim Rk_tab_id As String = "" '保存入库表中选择行的ID表属性
    Dim oldKucun As Integer = 0  '保存旧库数量属性
    Dim SelectedRowIndex As Integer '选择的行索引

    Private Sub DisplayBMTree()
        ComboBoxTreeBM = New ComboBoxTreeView()
        ComboBoxTreeBM.Dock = DockStyle.Fill
        Me.Panel2.Controls.Add(ComboBoxTreeBM)

        OpreaBMDataBase()
        CommBindTreeView(0, ComboBoxTreeBM.TreeView, dt_BM, "parentBMBH", "0", "BMMC", "BMBH")
    End Sub

    Private Sub OpreaBMDataBase()
        'Dim conn As Data.SQLite.SQLiteConnection = New Data.SQLite.SQLiteConnection(CONN_STR)
        '打开连接
        'conn.Open()
        'Dim cmd As SQLite.SQLiteCommand = New SQLite.SQLiteCommand(conn)
        Dim sql As String = "select * from bm"
        'ds = SQLite.SQLiteCommand SQLiteHelper.SQLiteCommandDataSet(DBConStr, sqlStr, Nothing)
        'Dim reader As SQLite.SQLiteDataReader = cmd.ExecuteReader()
        sda_BM = New LiuDataAdapter(sql, CONN_STR)


        dt_BM.Clear()
        sda_BM.Fill(dt_BM)


        'G_dt.Load(reader)
    End Sub
    Private Sub GetKuCunDevice()
        'MsgBox(System.Environment.GetEnvironmentVariable("SYSTEMROOT"))
        Dim TB As DataTable = New DataTable()
        'Dim conn As Data.SQLite.SQLiteConnection = New Data.SQLite.SQLiteConnection(CONN_STR)

        ' MsgBox(System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
        '打开连接
        'conn.Open()
        'Dim cmd As SQLite.SQLiteCommand = New SQLite.SQLiteCommand(conn)

        '库存大于0的才能进行分配，否则不显示
        Dim sql As String = "select * from rk where kucun>0 order by id desc"
        'Dim sql As String = "select * from rk  order by id desc"
        'ds = SQLite.SQLiteCommand SQLiteHelper.SQLiteCommandDataSet(DBConStr, sqlStr, Nothing)
        'Dim reader As SQLite.SQLiteDataReader = cmd.ExecuteReader()
        Dim sda As LiuDataAdapter = New LiuDataAdapter(sql, CONN_STR)


        'sda.Fill(G_dt)
        sda.Fill(TB)

        DataGridView1.DataSource = TB


        '设置DataGridView可显示隐藏列,用Form的名字保存xml文件
        SetDataGridViewHidenColumn(DataGridView1, Me.Name.ToString())
 


        DataGridView1.Columns(0).ReadOnly = True


        For i = 0 To 19
            If (DataGridView1(i, SelectedRowIndex).Visible) Then
                DataGridView1.CurrentCell = DataGridView1(i, SelectedRowIndex)
                Return
            End If
        Next

    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        FenPeiZC()
    End Sub


    Private Sub FenPeiZC()
        If DataGridView1.SelectedRows(0).IsNewRow Then Return '空行直接返回

        SelectedRowIndex = DataGridView1.SelectedRows(0).Index

        oldKucun = CInt(DataGridView1.SelectedRows(0).Cells(14).Value.ToString)
        If oldKucun < 1 Then

            MsgBox("库存小于0，不能再分配了")
            Return

        End If

        'DataGridView1.SelectedRows(0).Cells(0).Value.ToString()
        TextBox17.Text = DataGridView1.SelectedRows(0).Cells(2).Value.ToString()
        TextBox3.Text = DataGridView1.SelectedRows(0).Cells(1).Value.ToString()
        TextBox5.Text = DataGridView1.SelectedRows(0).Cells(3).Value.ToString()
        TextBox10.Text = DataGridView1.SelectedRows(0).Cells(8).Value.ToString()


        TextBox18.Text = DataGridView1.SelectedRows(0).Cells(10).Value.ToString()
        TextBox4.Text = DataGridView1.SelectedRows(0).Cells(13).Value.ToString()
        'DateTimePicker1.Text = DateTime.Parse(DataGridView1.SelectedRows(0).Cells(8).Value.ToString())
        DateTimePicker1.Value = CDate(DataGridView1.SelectedRows(0).Cells(21).Value.ToString())
        DateTimePicker3.Value = CDate(DataGridView1.SelectedRows(0).Cells(6).Value.ToString())
        TextBox6.Text = DataGridView1.SelectedRows(0).Cells(15).Value.ToString()
        TextBox7.Text = DataGridView1.SelectedRows(0).Cells(16).Value.ToString()
        TextBox8.Text = DataGridView1.SelectedRows(0).Cells(17).Value.ToString()
        TextBox12.Text = DataGridView1.SelectedRows(0).Cells(18).Value.ToString()
        TextBox13.Text = DataGridView1.SelectedRows(0).Cells(19).Value.ToString()


        Rk_tab_id = DataGridView1.SelectedRows(0).Cells(0).Value.ToString()



        'TextBox11.Text = CInt(TextBox10.Text) * CInt(TextBox9.Text)
    End Sub

    Private Sub DataGridView1_RowHeaderMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseDoubleClick
        FenPeiZC()
    End Sub

    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        DisplayBMTree()
        GetKuCunDevice()
        OnComboBoxTreeViewTextUpdate()   '激活OnComboBoxTreeViewTextUpdate事件

        '绑定常用数据字典，资产状态
        GetComboBoxDICT(ZC_STATE, ComboBox1)

        '绑定常用数据字典，设备的存放位置
        GetComboBoxDICT(CUN_FANG_WEI_ZHI, ComboBox2)



        '设置DataGridView显示风格
        SetDataGridViewStyle(DataGridView1)

        '设置DataGridView可显示隐藏列,用Form的名字保存xml文件
        SetDataGridViewHidenColumn(DataGridView1, Me.Name.ToString())

        SetColumns4RK(DataGridView1)
    End Sub

    Private Sub TextBox9_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox9.KeyPress
        IsInputNum(e)
    End Sub


    Private Sub TextBox10_TextChanged(sender As Object, e As EventArgs) Handles TextBox10.TextChanged
        JiSuanZongjia()
    End Sub


    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        JiSuanZongjia()
    End Sub

    Private Sub JiSuanZongjia()
        If TextBox9.Text <> "" And TextBox10.Text <> "" Then
            TextBox11.Text = CDbl(TextBox10.Text) * CDbl(TextBox9.Text)
        Else
            TextBox11.Text = ""
        End If
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
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
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SaveData()
        'MsgBox(GetZCBH("200232"))
        GetKuCunDevice()

    End Sub

    Private Sub SaveData()
        'Dim SQLconn As New Data.SQLite.SQLiteConnection '定义数据库链接  
        'Dim sqlcmd As New SQLite.SQLiteCommand '定义查询操作  
        'Dim ds As New DataSet
        Dim salda As LiuDataAdapter



        Dim zcbh As String  '资产编码（内部)
        Dim zcmc As String  '资产名称
        Dim lbid As String '类别ID
        Dim lbmc As String '类别名称
        Dim jldw As String '计量单位
        Dim gzrq As String '购置日期
        Dim djrq As String '登记日期
        Dim zcly As String '资产来源
        Dim zcsl As String '资产数量
        Dim zcdj As String '单价
        Dim zczj As String '总价
        Dim zczt As String '资产状态
        Dim bmbh As String '部门编号
        Dim bmmc As String '部门名称
        Dim zrr As String  '责任人
        Dim cfwz As String '存放位置
        Dim log As String   '资产流转日志，格式：bmbh,zrr@bmbh,zrr....往后递增
        Dim rkbh As String  '入库编号
        Dim zcxh As String  '资产型号
        Dim zcpp As String  '资产品牌   
        Dim pz As String    '配置
        Dim sbsn As String  '设备SN
        Dim ossn As String  '操作系统SN
        Dim memo As String  '备注
        Dim sjgzrq As String '实际购置日期
        Dim osstr As String '操作系统版本

        Dim newKucun As Integer = 0 '新库存数量

        Try
            '如果资产编号为空，部门为空，责任人为空则返回
            If Trim(TextBox6.Text) = "" Or Trim(ComboBoxTreeBM.Text) = "" Or Trim(ComboBox3.Text) = "" Or Trim(TextBox9.Text) = "" Or Trim(ComboBox1.Text) = "" Then
                MsgBox("资产入库编号、归属部门、责任人、分配数量和资产状态不能为空", MsgBoxStyle.OkOnly + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Exclamation, "警告")
                Return
            End If

            zcmc = TextBox5.Text

            lbid = TextBox3.Text

            zcbh = GetZCBH(lbid)


            lbmc = TextBox17.Text
            jldw = TextBox18.Text
            gzrq = DateTimePicker3.Text
            sjgzrq = DateTimePicker1.Text

            djrq = DateTimePicker2.Text
            zcly = TextBox7.Text
            zcsl = TextBox9.Text

            zcdj = TextBox10.Text
            zczj = TextBox11.Text
            'zczt = "在用"   '这里应该从数据库里面获取资产状态的值
            zczt = ComboBox1.Text
            bmbh = ComboBoxTreeBM.TreeView.SelectedNode.Name
            bmmc = ComboBoxTreeBM.Text
            zrr = ComboBox3.Text
            cfwz = ComboBox2.Text
            log = DateTimePicker2.Text + bmmc + zrr
            rkbh = TextBox6.Text
            zcxh = TextBox8.Text
            zcpp = TextBox12.Text
            pz = TextBox4.Text
            ossn = TextBox2.Text
            sbsn = TextBox1.Text
            osstr = osversion.Text
            memo = TextBox13.Text

            If zcsl > oldKucun Then
                MsgBox("分配数量大于库存数量", MsgBoxStyle.OkOnly + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Exclamation, "警告")
                TextBox9.Focus()
                Return
            Else
                newKucun = oldKucun - zcsl
            End If


            Dim CommandText As String

            'SQLite可以在字段上加单引号，MYsql就不行
            'CommandText = "insert into zc ('zcbh','zcmc','lbid','lbmc','jldw','gzrq','djrq','zcly','zcsl','zcdj','zczj','zczt','bmbh','bmmc','zrr','cfwz','meno','txt1','txt2','txt3','txt4','txt5','txt6','txt7','txt8','num1','num2','num3','num4','num5','num6','log','rkbh') values('" + zcbh + "','" + zcmc + "','" + lbid + "','" + lbmc + "','" + jldw + "','" + gzrq + "','" + djrq + "','" + zcly + "','" + zcsl + "','" + zcdj + "','" + zczj + "','" + zczt + "','" + bmbh + "','" + bmmc + "','" + zrr + "','" + cfwz + "','" + meno + "','" + txt1 + "','" + txt2 + "','" + txt3 + "','" + txt4 + "','" + txt5 + "','" + txt6 + "','" + txt7 + "','" + txt8 + "','" + num1 + "','" + num2 + "','" + num3 + "','" + num4 + "','" + num5 + "','" + num6 + "','" + log + "','" + rkbh + "')"

            CommandText = "insert into zc (zcbh,zcmc,lbid,lbmc,jldw,gzrq,djrq,zcly,zcsl,zcdj,zczj,zczt,bmbh,bmmc,zrr,cfwz,log,rkbh,zcxh,zcpp,pz,devicesn,ossn,memo,sjgzrq,osversion,operator) values('" + zcbh + "','" + zcmc + "','" + lbid + "','" + lbmc + "','" + jldw + "','" + gzrq + "','" + djrq + "','" + zcly + "','" + zcsl + "','" + zcdj + "','" + zczj + "','" + zczt + "','" + bmbh + "','" + bmmc + "','" + zrr + "','" + cfwz + "','" + log + "','" + rkbh + "','" + zcxh + "','" + zcpp + "','" + pz + "','" + sbsn + "','" + ossn + "','" + memo + "','" + sjgzrq + "','" + osstr + "','" + LoginUser + "')"

            'ComboBoxTreeLB.Text +"','" + ComboBoxTreeLB.TreeView.SelectedNode.Name + "','" + TextBox3.Text + "','" + ComboBox3.Text + "','" + TextBox4.Text + "','" + DateTimePicker1.Text + "','" + DateTimePicker2.Text + "'," + TextBox5.Text + "," + TextBox1.Text + ",'" + ComboBox2.Text + "','" + TextBox7.Text + "','" + TextBox2.Text + "','" + TextBox8.Text + "'," + TextBox1.Text + ")"
            'Dim sqlreader As SQLite.SQLiteDataReader = sqlcmd.ExecuteReader
            salda = New LiuDataAdapter()
            salda.ExecuteNonQuery(CommandText)
            'SQLconn.Close()


            'Dim sqlExecuteQuery As LiuDataAdapter '定义查询操作  
            'Dim SQLconn2 As New Data.SQLite.SQLiteConnection '定义数据库链接  
            'SQLconn2.ConnectionString = CONN_STR '链接数据库
            'SQLconn2.Open()
            'sqlExecuteQuery.Connection = SQLconn2

            CommandText = "update rk set kucun= " + newKucun.ToString + "  where id=" + Rk_tab_id
            salda.ExecuteNonQuery(CommandText)


            'SQLite.SQLiteHelper.ExecuteDataset(constr, CommandType.Text, Sql)
            'salda.Fill(ds, 0)
            'DGV1.DataSource = ds.Tables(0)
            MsgBox("设备分配成功！", MsgBoxStyle.OkOnly + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Information, "成功")
            'MsgBox("你确认要删除该记录吗？", MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Exclamation, "警告") = MsgBoxResult.Ok
            SetNew()
        Finally
            'If Not (SQLconn Is Nothing) Then SQLconn.Dispose()
            'SQLconn = Nothing
            'If Not (salda Is Nothing) Then salda.Dispose()
            salda = Nothing
        End Try
    End Sub


    Public Sub OnComboBoxTreeViewTextUpdate()

        If Not ComboBoxTreeBM Is Nothing Then
            'ComboBoxTreeBM.AutoPostBack = True
            AddHandler ComboBoxTreeBM.TextChanged, AddressOf ComboBoxTreeViewTextUpdate
        End If
    End Sub
    Private Sub ComboBoxTreeViewTextUpdate()


        Dim dt = New DataTable()
        'Dim conn As Data.SQLite.SQLiteConnection = New Data.SQLite.SQLiteConnection(CONN_STR)
        '打开连接
        'conn.Open()
        'Dim cmd As SQLite.SQLiteCommand = New SQLite.SQLiteCommand(conn)
        Dim sql As String = "select xm from bm_ry where bmbh='" + ComboBoxTreeBM.TreeView.SelectedNode.Name + "'"
        'ds = SQLite.SQLiteCommand SQLiteHelper.SQLiteCommandDataSet(DBConStr, sqlStr, Nothing)
        'Dim reader As SQLite.SQLiteDataReader = cmd.ExecuteReader()
        Dim sda = New LiuDataAdapter(sql, CONN_STR)
        'Dim scb As SQLite.SQLiteCommandBuilder = New SQLite.SQLiteCommandBuilder(sda)

        sda.Fill(dt)
        ComboBox3.DataSource = dt
        ComboBox3.DisplayMember = "xm"
        If dt.Rows.Count = 0 Then
            MsgBox("该部门下没有人员信息，请先添加人员信息")
            ComboBox3.Text = ""
        End If
        ComboBox3.Text = ""
    End Sub

    Private Sub SetNew()
        TextBox3.Text = ""
        TextBox17.Text = ""
        TextBox18.Text = ""
        DateTimePicker1.Text = ""
        DateTimePicker2.Text = ""
        TextBox7.Text = ""
        'TextBox9.Text = ""
        TextBox10.Text = ""
        TextBox11.Text = ""
        ComboBox3.Text = ""
        DateTimePicker2.Text = ""
        TextBox6.Text = ""
        TextBox13.Text = ""
        TextBox2.Text = ""
        TextBox1.Text = ""
        ComboBox1.Text = ""

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ExportToCSV(DataGridView1)
    End Sub
End Class