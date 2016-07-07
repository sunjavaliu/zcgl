Public Class Form4

    Dim sda As SQLite.SQLiteDataAdapter   ';//全局变量
    Dim G_dt As DataTable = New DataTable()

    Dim sda_BM As SQLite.SQLiteDataAdapter   ';//全局变量
    Dim dt_BM As DataTable = New DataTable()
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim sql As String

        Dim ComboBoxTreeLB As ComboBoxTreeView
        '嵌套了treeview的combobox
        Dim trv As New TreeView()
        'trv.Nodes(0).Expand()
        ComboBoxTreeLB = New ComboBoxTreeView()
        'comboTrv.SelectedIndexChanged += comboBox6_SelectedIndexChanged
        'comboTrv.Left = 92
        'comboTrv.Top = 8
        'comboTrv.Width = 221
        'comboTrv.TreeView.Nodes.Add("aaaa")
        'comboTrv.TreeView.Nodes.Add("aaafa")
        ComboBoxTreeLB.Dock = DockStyle.Fill
        Me.Panel1.Controls.Add(ComboBoxTreeLB)
        'ComboBoxTreeLB.TreeView.ExpandAll()


        'BindTreeView(0, ComboBoxTreeLB.TreeView, G_dt)
        OpreaLBDataBase("")
        CommBindTreeView(0, ComboBoxTreeLB.TreeView, G_dt, "parentlbdm", "0", "lbmc", "lbdm")
        'comboTrv.TreeView.Height = 400



        Dim ComboBoxTreeBM As ComboBoxTreeView
        ComboBoxTreeBM = New ComboBoxTreeView()
        'BindBMTreeView(0, ComboBoxTreeBM.TreeView, dt_BM)


        OpreaBMDataBase()
        CommBindTreeView(0, ComboBoxTreeBM.TreeView, dt_BM, "parentBMBH", "0", "BMMC", "BMBH")
        ComboBoxTreeBM.Dock = DockStyle.Fill
        Me.Panel2.Controls.Add(ComboBoxTreeBM)



        sql = String.Format("select * from zc")
        OpenDataBase(sda, G_dt, sql)
        GetJldw()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
    Private Sub OpreaLBDataBase(bmbh As String)
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

    End Sub


    Public Sub OpenDataBase(ByRef Sda As SQLiteDataAdapter, ByRef dt As DataTable, sql As String)
        Try

            Sda = New SQLite.SQLiteDataAdapter(sql, CONN_STR)
            Dim scb As SQLite.SQLiteCommandBuilder = New SQLite.SQLiteCommandBuilder(Sda)
            dt.Clear()
            Sda.Fill(dt)

            Dim bs As BindingSource = New BindingSource()
            bs.DataSource = dt
            Me.BindingNavigator1.BindingSource = bs

            'Me.BindingSource1 = bs

            TextBox1.DataBindings.Add("Text", bs, "zcbh", True)
        Catch ex As SQLite.SQLiteException
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'Me.BindingNavigator1.AddNewItem()
    End Sub

    Private Sub BindingNavigatorAddNewItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorAddNewItem.Click

    End Sub

    Private Sub GetJldw()
        Dim sda_jldw As SQLite.SQLiteDataAdapter   ';//全局变量
        Dim dt_jldw As DataTable = New DataTable()
        Dim str_sql_jldw As String
        str_sql_jldw = "select * from zd where item='计量单位'"
        Try

            sda_jldw = New SQLite.SQLiteDataAdapter(str_sql_jldw, CONN_STR)
            Dim scb As SQLite.SQLiteCommandBuilder = New SQLite.SQLiteCommandBuilder(sda_jldw)
            dt_jldw.Clear()
            sda_jldw.Fill(dt_jldw)

            Dim bs_jldw As BindingSource = New BindingSource()
            bs_jldw.DataSource = dt_jldw
            'ComboBox2.DataBindings.Add("Text", bs_jldw, "content", True)
            ComboBox2.DataSource = dt_jldw
            ComboBox2.DisplayMember = "content"
            'Me.BindingNavigator1.BindingSource = bs

            'Me.BindingSource1 = bs

            'TextBox1.DataBindings.Add("Text", bs, "zcbh", True)
        Catch ex As SQLite.SQLiteException
            MsgBox(ex.Message)
        End Try
    End Sub




    Private Sub OpreaBMDataBase()
        Dim conn As Data.SQLite.SQLiteConnection = New Data.SQLite.SQLiteConnection(CONN_STR)
        '打开连接
        conn.Open()
        'Dim cmd As SQLite.SQLiteCommand = New SQLite.SQLiteCommand(conn)
        Dim sql As String = "select * from bm"
        'ds = SQLite.SQLiteCommand SQLiteHelper.SQLiteCommandDataSet(DBConStr, sqlStr, Nothing)
        'Dim reader As SQLite.SQLiteDataReader = cmd.ExecuteReader()
        sda_BM = New SQLite.SQLiteDataAdapter(sql, CONN_STR)
        Dim scb As SQLite.SQLiteCommandBuilder = New SQLite.SQLiteCommandBuilder(sda_BM)

        sda_BM.Fill(dt_BM)

        'G_dt.Load(reader)
    End Sub

   
End Class