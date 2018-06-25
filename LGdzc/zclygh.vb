Public Class zclygh

    Private TreeOperateType As String

    'Dim sda As LiuDataAdapter   ';//全局变量
    'Dim G_dt As DataTable = New DataTable()

    Dim sda_zc As LiuDataAdapter   ';//全局变量
    Dim G_dt_zc As DataTable = New DataTable()

    Dim ComboBoxTreeBM As ComboBoxTreeView

    Dim sda_BM As LiuDataAdapter   ';//全局变量
    Dim dt_BM As DataTable = New DataTable()

    Dim sda_BMRY As LiuDataAdapter   ';//全局变量
    Dim dt_BMRY As DataTable = New DataTable()

    Dim bmbh As String = ""   '部门编号
    Dim selectRowNum As Integer = -1 '选中的行数

    Dim operationRecord As Integer  '操作选项记录，如果选择的是部门就是1，如果执行的是查询就是2，刷新的时候按上一次的记录进行刷新
    Private Sub DisplayBMTree()
        ComboBoxTreeBM = New ComboBoxTreeView()
        ComboBoxTreeBM.Dock = DockStyle.Fill
        Me.Panel2.Controls.Add(ComboBoxTreeBM)

        'OpreaBMDataBase()
        CommBindTreeView(0, ComboBoxTreeBM.TreeView, dt_BM, "parentBMBH", "0", "BMMC", "BMBH")
    End Sub
    Private Sub zclygh_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try


            DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            'BindTreeView(0, TreeView1, G_dt)
            OpreaZCDataBase("")
            GetDepartmentAndPersonnelTree()
            GetDepartmentTree()
            CommBindTreeView(0, TreeView1, dt_BMRY, "parentBMBH", "0", "BMMC", "BMBH")
            TreeView1.ShowNodeToolTips = True
            DataGridView1.ShowCellToolTips = True
            DataGridView1.Columns(0).ToolTipText = "双击单元格进行编辑操作"
            'TreeView1.Nodes.
            TreeView1.Nodes(0).Expand()
            DisplayBMTree()
            OnComboBoxTreeViewTextUpdate()
            GetComboBoxDICT(ZC_STATE, ComboBox1)
            '设置DataGridView显示风格
            SetDataGridViewStyle(DataGridView1)

            GetComboBoxDICT("xh", "xh", ComboBox5)

            GetComboBoxDICT("lbmc", "lbmc", ComboBox4)

            GetComboBoxDICT("zcmc", "zcmc", ComboBox2)

            ComboBox5.Text = ""
            ComboBox2.Text = ""
            ComboBox4.Text = ""
            TextBox14.Text = ""

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' 
    ''' 获取部门和人员信息树
    ''' </summary>
    ''' <remarks></remarks>

    Private Sub GetDepartmentAndPersonnelTree()

        'Dim sql As String = "select * from bm"
        Dim sql As String = "select * from v_bm"
        'ds = SQLite.SQLiteCommand SQLiteHelper.SQLiteCommandDataSet(DBConStr, sqlStr, Nothing)
        'Dim reader As SQLite.SQLiteDataReader = cmd.ExecuteReader()
        sda_BMRY = New LiuDataAdapter(sql, CONN_STR)
        'Dim scb As SQLite.SQLiteCommandBuilder = New SQLite.SQLiteCommandBuilder(sda)

        sda_BMRY.Fill(dt_BMRY)

        'G_dt.Load(reader)
    End Sub

    Private Sub GetDepartmentTree()

        Dim sql As String = "select * from bm"
        'ds = SQLite.SQLiteCommand SQLiteHelper.SQLiteCommandDataSet(DBConStr, sqlStr, Nothing)
        'Dim reader As SQLite.SQLiteDataReader = cmd.ExecuteReader()
        sda_BM = New LiuDataAdapter(sql, CONN_STR)
        'Dim scb As SQLite.SQLiteCommandBuilder = New SQLite.SQLiteCommandBuilder(sda)

        sda_BM.Fill(dt_BM)

        'G_dt.Load(reader)
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        Dim SelectedNode As TreeNode = TreeView1.SelectedNode
        Dim selectBMMC As String
        'sda_ry = SQLite.SQLiteDataAdapter("select * from bm_ry where bmbh=" + SelectedNode.Name, CONN_STR)
        'Dim scb As SQLite.SQLiteCommandBuilder = New SQLite.SQLiteCommandBuilder(sda_ry)
        'sda_ry.SelectCommand.ExecuteReader("select * from bm_ry where bmbh=" + SelectedNode.Name)
        'sda_ry.Fill(G_dt_ry)
        'DataGridView1.DataSource = G_dt_ry
        'DataGridView1.Refresh()
        bmbh = SelectedNode.Name
        selectBMMC = SelectedNode.Text

        '如果下级子节点的个数为0表示就是末级节点
        If SelectedNode.Nodes.Count = 0 Then
            '获取个人拥有的设备
            GetZcInfo4Name(selectBMMC)
        Else
            '获取部门拥有的设备
            OpreaZCDataBase(bmbh)
        End If

        operationRecord = 1

    End Sub
    Private Sub GetZcInfo4Name(name As String)

        Dim sql As String
        sql = "select * from zc where zrr='" + name + "'"
        'commSQL = sql
        sda_zc = New LiuDataAdapter(sql, CONN_STR)
        G_dt_zc.Clear()
        sda_zc.Fill(G_dt_zc)
        DataGridView1.DataSource = G_dt_zc

    End Sub

    Private Sub OpreaZCDataBase(bmbh As String)


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

        'ds = SQLite.SQLiteCommand SQLiteHelper.SQLiteCommandDataSet(DBConStr, sqlStr, Nothing)
        'Dim reader As SQLite.SQLiteDataReader = cmd.ExecuteReader()
        sda_zc = New LiuDataAdapter(sql, CONN_STR)
        ' Dim scb As SQLite.SQLiteCommandBuilder = New SQLite.SQLiteCommandBuilder(sda_ry)

        G_dt_zc.Clear()
        sda_zc.FillSchema(G_dt_zc, SchemaType.Mapped)
        sda_zc.Fill(G_dt_zc)

        DataGridView1.DataSource = G_dt_zc

        '设置DataGridView可显示隐藏列,用Form的名字保存xml文件
        SetDataGridViewHidenColumn(DataGridView1, Me.Name.ToString())

        DataGridView1.Columns(0).ReadOnly = True
        '设置资产表的列标题
        SetColumnsTitle4ZC(DataGridView1)

        '更改显示序号
        'DataGridView1.Columns(0).DisplayIndex = 0
        'DataGridView1.Columns(14).DisplayIndex = 1
        'DataGridView1.Columns(15).DisplayIndex = 2
        'DataGridView1.Columns(19).DisplayIndex = 3
        'DataGridView1.Columns(12).DisplayIndex = 4
        'DataGridView1.Columns(3).Frozen = True
        'G_dt.Load(reader)
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        FenPeiZC()
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

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click

        Dim sql As String
        Dim log As String
        Dim zcbh As String
        Dim pz As String
        Dim memo As String

        zcbh = TextBox3.Text
        pz = TextBox8.Text
        If zcbh = "" Then Return
        memo = TextBox13.Text
        log = DataGridView1.SelectedRows(0).Cells(17).Value.ToString() + "->" + DateTimePicker2.Text + ComboBoxTreeBM.Text + ComboBox3.Text
        sql = "update zc set bmbh='" + ComboBoxTreeBM.TreeView.SelectedNode.Name + "',bmmc='" + ComboBoxTreeBM.Text + "', log='" + log + "', zrr='" + ComboBox3.Text + "' ,zczt='" + ComboBox1.Text + "' ,operator='" + LoginUser + "',pz='" + pz + "',memo='" + memo + "'  where zcbh='" + zcbh + "'"
        sda_zc.ExecuteNonQuery(sql)

        'If selectRowNum = -1 Then Return
        ''log = DataGridView1.SelectedRows(0).Cells(17).Value.ToString() + "->" + ComboBoxTreeBM.Text + ComboBox3.Text
        'DataGridView1.Rows(selectRowNum).Cells(7).Value = DateTimePicker2.Text     '登记日期
        'DataGridView1.Rows(selectRowNum).Cells(12).Value = ComboBox1.Text        '"资产状态"
        'DataGridView1.Rows(selectRowNum).Cells(13).Value = ComboBoxTreeBM.TreeView.SelectedNode.Name '部门编号
        'DataGridView1.Rows(selectRowNum).Cells(14).Value = ComboBoxTreeBM.Text   '部门名称
        'DataGridView1.Rows(selectRowNum).Cells(17).Value = DataGridView1.Rows(selectRowNum).Cells(17).Value.ToString() + "->" + DateTimePicker2.Text + ComboBoxTreeBM.Text + ComboBox3.Text    '流转记录
        'DataGridView1.Rows(selectRowNum).Cells(15).Value = ComboBox3.Text   '部门名称

        'sda_zc.Update(G_dt_zc)
        'DataGridView1.Update()
        'selectRowNum = -1

        MsgBox("设备由 【" + TextBox2.Text + "】 调拨给 【" + ComboBox3.Text + "】")
        SetNew()
        If operationRecord = 1 Then OpreaZCDataBase(bmbh)
        If operationRecord = 2 Then QueryData()

    End Sub

    Private Sub DataGridView1_RowHeaderMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseDoubleClick
        FenPeiZC()
    End Sub
    Private Sub FenPeiZC()
        selectRowNum = DataGridView1.SelectedRows(0).Index

        TextBox6.Text = DataGridView1.SelectedRows(0).Cells(18).Value.ToString()
        TextBox7.Text = DataGridView1.SelectedRows(0).Cells(8).Value.ToString()
        TextBox17.Text = DataGridView1.SelectedRows(0).Cells(3).Value.ToString()
        TextBox12.Text = DataGridView1.SelectedRows(0).Cells(4).Value.ToString()
        TextBox11.Text = DataGridView1.SelectedRows(0).Cells(11).Value.ToString()

        TextBox10.Text = DataGridView1.SelectedRows(0).Cells(11).Value.ToString()
        'DateTimePicker1.Text = DateTime.Parse(DataGridView1.SelectedRows(0).Cells(8).Value.ToString())
        DateTimePicker1.Value = CDate(DataGridView1.SelectedRows(0).Cells(6).Value.ToString())
        'TextBox6.Text = DataGridView1.SelectedRows(0).Cells(15).Value.ToString()
        TextBox18.Text = DataGridView1.SelectedRows(0).Cells(5).Value.ToString()
        'Rk_tab_id = DataGridView1.SelectedRows(0).Cells(0).Value.ToString()
        TextBox1.Text = DataGridView1.SelectedRows(0).Cells(14).Value.ToString()
        TextBox2.Text = DataGridView1.SelectedRows(0).Cells(15).Value.ToString()
        TextBox3.Text = DataGridView1.SelectedRows(0).Cells(1).Value.ToString()         '资产编号
        TextBox8.Text = DataGridView1.SelectedRows(0).Cells(21).Value.ToString()       '配置信息
        TextBox9.Text = DataGridView1.SelectedRows(0).Cells(9).Value.ToString()            '领用数量
        TextBox4.Text = DataGridView1.SelectedRows(0).Cells(22).Value.ToString()    '设备序列号
        TextBox5.Text = DataGridView1.SelectedRows(0).Cells(20).Value.ToString()        '资产品牌
        TextBox13.Text = DataGridView1.SelectedRows(0).Cells(24).Value.ToString()        '备注
    End Sub

    Private Sub SetNew()
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox17.Text = ""
        TextBox12.Text = ""
        TextBox11.Text = ""

        TextBox10.Text = ""


        TextBox18.Text = ""

        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        ComboBox1.Text = ""
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ExportToCSV(DataGridView1)
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
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        operationRecord = 2
        QueryData()
    End Sub

    Private Sub QueryData()
        Dim sql As String = ""
        Dim sqlwhere As String = ""
        Dim sqlxh As String = ""
        Dim sqllb As String = ""
        Dim sqlzrr As String = ""
        Dim sqlzcmc As String = ""
        sql = "select * from zc   "
        If Trim(ComboBox5.Text) <> "" Then sqlxh = "zcxh like '%" + Trim(ComboBox5.Text) + "%'"
        If Trim(ComboBox4.Text) <> "" Then sqllb = "lbmc like '" + Trim(ComboBox4.Text) + "%'"
        If Trim(ComboBox2.Text) <> "" Then sqlzcmc = "zcmc like '%" + Trim(ComboBox2.Text) + "%'"
        If Trim(TextBox14.Text) <> "" Then sqlzrr = "zrr like '%" + Trim(TextBox14.Text) + "%'"

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


        sda_zc = New LiuDataAdapter(sql, CONN_STR)
        G_dt_zc.Clear()
        sda_zc.Fill(G_dt_zc)
        DataGridView1.DataSource = G_dt_zc
    End Sub
End Class



