Public Class Form8
    Dim sda_BM As LiuDataAdapter   ';//全局变量
    Dim dt_BM As DataTable = New DataTable()

    Dim ComboBoxTreeBM As ComboBoxTreeView
    Dim Rk_tab_id As String = "" '保存入库表中选择行的ID表属性
    Dim oldKucun As Integer = 0  '保存旧库数量属性

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
        'Dim sql As String = "select * from rk where kucun>0 order by id desc"
        Dim sql As String = "select * from rk order by id desc"
        'ds = SQLite.SQLiteCommand SQLiteHelper.SQLiteCommandDataSet(DBConStr, sqlStr, Nothing)
        'Dim reader As SQLite.SQLiteDataReader = cmd.ExecuteReader()
        Dim sda As LiuDataAdapter = New LiuDataAdapter(sql, CONN_STR)


        'sda.Fill(G_dt)
        sda.Fill(TB)





        DataGridView1.DataSource = TB



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
        DataGridView1.Columns(17).HeaderText = "资产型号"
        DataGridView1.Columns(18).HeaderText = "资产品牌"
        'DataGridView1.Columns(3).Frozen = True


        'dataAdapter.SelectCommand = cmd
        'dataAdapter.Fill(dst, "info")
        'dt = dst.Tables("info")
        'sqlConnection1.Close()   '关闭数据库   


        'DataGridView1.AutoGenerateColumns = True '自动创建列   
        'DataGridView1.DataSource = sda

    End Sub


    Private Sub FenPeiSheBei()
        oldKucun = CInt(DataGridView1.SelectedRows(0).Cells(14).Value.ToString)
        If oldKucun < 1 Then

            MsgBox("库存小于0，不能再分配了")
            Return

        End If

        'DataGridView1.SelectedRows(0).Cells(0).Value.ToString()
        TextBox3.Text = DataGridView1.SelectedRows(0).Cells(3).Value.ToString()
        TextBox10.Text = DataGridView1.SelectedRows(0).Cells(8).Value.ToString()
        TextBox17.Text = DataGridView1.SelectedRows(0).Cells(2).Value.ToString()
        TextBox5.Text = DataGridView1.SelectedRows(0).Cells(4).Value.ToString()
        TextBox18.Text = DataGridView1.SelectedRows(0).Cells(10).Value.ToString()
        TextBox4.Text = DataGridView1.SelectedRows(0).Cells(13).Value.ToString()
        'DateTimePicker1.Text = DateTime.Parse(DataGridView1.SelectedRows(0).Cells(8).Value.ToString())
        DateTimePicker1.Value = CDate(DataGridView1.SelectedRows(0).Cells(6).Value.ToString())
        TextBox6.Text = DataGridView1.SelectedRows(0).Cells(15).Value.ToString()
        TextBox7.Text = DataGridView1.SelectedRows(0).Cells(16).Value.ToString()
        TextBox8.Text = DataGridView1.SelectedRows(0).Cells(17).Value.ToString()
        TextBox12.Text = DataGridView1.SelectedRows(0).Cells(18).Value.ToString()

        Rk_tab_id = DataGridView1.SelectedRows(0).Cells(0).Value.ToString()

        'TextBox11.Text = CInt(TextBox10.Text) * CInt(TextBox9.Text)
    End Sub


    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        FenPeiSheBei()
    End Sub

    Private Sub DataGridView1_RowHeaderMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseDoubleClick
        FenPeiSheBei()
    End Sub

    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DisplayBMTree()
        GetKuCunDevice()
        OnComboBoxTreeViewTextUpdate()   '激活OnComboBoxTreeViewTextUpdate事件
    End Sub

    Private Sub TextBox9_KeyPress(sender As Object, e As KeyPressEventArgs)
        IsInputNum(e)
    End Sub


    Private Sub TextBox10_TextChanged(sender As Object, e As EventArgs) Handles TextBox10.TextChanged
        JiSuanZongjia()
    End Sub

    Private Sub TextBox9_TextChanged_1(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        JiSuanZongjia()
    End Sub

    Private Sub JiSuanZongjia()
        If TextBox9.Text <> "" And TextBox10.Text <> "" Then
            TextBox11.Text = CInt(TextBox10.Text) * CInt(TextBox9.Text)
        Else
            TextBox11.Text = ""
        End If
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
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
        Dim meno As String
        Dim txt1 As String
        Dim txt2 As String
        Dim txt3 As String
        Dim txt4 As String
        Dim txt5 As String
        Dim txt6 As String
        Dim txt7 As String
        Dim txt8 As String
        Dim num1 As String
        Dim num2 As String
        Dim num3 As String
        Dim num4 As String
        Dim num5 As String
        Dim num6 As String
        Dim log As String   '资产流转日志，格式：bmbh,zrr@bmbh,zrr....往后递增
        Dim rkbh As String  '入库编号


        Dim newKucun As Integer = 0 '新库存数量

        Try
            zcmc = TextBox3.Text
            lbid = ""

            zcbh = GetZCBH(lbid)


            lbmc = TextBox17.Text
            jldw = TextBox18.Text
            gzrq = DateTimePicker1.Text
            djrq = DateTimePicker2.Text
            zcly = TextBox7.Text
            zcsl = TextBox9.Text

            zcdj = TextBox10.Text
            zczj = TextBox11.Text
            zczt = "使用"
            bmbh = ComboBoxTreeBM.TreeView.SelectedNode.Name
            bmmc = ComboBoxTreeBM.Text
            zrr = ComboBox3.Text
            cfwz = ""
            meno = ""
            txt1 = ""
            txt2 = ""
            txt3 = ""
            txt4 = ""
            txt5 = ""
            txt6 = ""
            txt7 = ""
            txt8 = ""
            num1 = ""
            num2 = ""
            num3 = ""
            num4 = ""
            num5 = ""
            num6 = ""
            log = bmmc + zrr + DateTimePicker2.Text
            rkbh = TextBox6.Text

            If zcsl > oldKucun Then

                MsgBox("分配数量大于库存数量")
                Return
            Else
                newKucun = oldKucun - zcsl
            End If


            'SQLconn.ConnectionString = CONN_STR '链接数据库  
            'SQLconn.Open()
            'sqlcmd.Connection = SQLconn
            Dim CommandText As String

            'SQLite可以在字段上加单引号，MYsql就不行
            'CommandText = "insert into zc ('zcbh','zcmc','lbid','lbmc','jldw','gzrq','djrq','zcly','zcsl','zcdj','zczj','zczt','bmbh','bmmc','zrr','cfwz','meno','txt1','txt2','txt3','txt4','txt5','txt6','txt7','txt8','num1','num2','num3','num4','num5','num6','log','rkbh') values('" + zcbh + "','" + zcmc + "','" + lbid + "','" + lbmc + "','" + jldw + "','" + gzrq + "','" + djrq + "','" + zcly + "','" + zcsl + "','" + zcdj + "','" + zczj + "','" + zczt + "','" + bmbh + "','" + bmmc + "','" + zrr + "','" + cfwz + "','" + meno + "','" + txt1 + "','" + txt2 + "','" + txt3 + "','" + txt4 + "','" + txt5 + "','" + txt6 + "','" + txt7 + "','" + txt8 + "','" + num1 + "','" + num2 + "','" + num3 + "','" + num4 + "','" + num5 + "','" + num6 + "','" + log + "','" + rkbh + "')"

            CommandText = "insert into zc (zcbh,zcmc,lbid,lbmc,jldw,gzrq,djrq,zcly,zcsl,zcdj,zczj,zczt,bmbh,bmmc,zrr,cfwz,meno,txt1,txt2,txt3,txt4,txt5,txt6,txt7,txt8,num1,num2,num3,num4,num5,num6,log,rkbh) values('" + zcbh + "','" + zcmc + "','" + lbid + "','" + lbmc + "','" + jldw + "','" + gzrq + "','" + djrq + "','" + zcly + "','" + zcsl + "','" + zcdj + "','" + zczj + "','" + zczt + "','" + bmbh + "','" + bmmc + "','" + zrr + "','" + cfwz + "','" + meno + "','" + txt1 + "','" + txt2 + "','" + txt3 + "','" + txt4 + "','" + txt5 + "','" + txt6 + "','" + txt7 + "','" + txt8 + "','" + num1 + "','" + num2 + "','" + num3 + "','" + num4 + "','" + num5 + "','" + num6 + "','" + log + "','" + rkbh + "')"

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

            'sqlExecuteQuery.ExecuteNonQuery()
            'SQLconn2.Close()



            'SQLite.SQLiteHelper.ExecuteDataset(constr, CommandType.Text, Sql)
            'salda.Fill(ds, 0)
            'DGV1.DataSource = ds.Tables(0)
            MsgBox("添加成功！")

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
    End Sub
 
    Private Sub SetNew()
        TextBox3.Text = ""
        TextBox17.Text = ""
        TextBox18.Text = ""
        DateTimePicker1.Text = ""
        DateTimePicker2.Text = ""
        TextBox7.Text = ""
        TextBox9.Text = ""
        TextBox10.Text = ""
        TextBox11.Text = ""
        ComboBox3.Text = ""
        DateTimePicker2.Text = ""
        TextBox6.Text = ""
    End Sub
End Class


