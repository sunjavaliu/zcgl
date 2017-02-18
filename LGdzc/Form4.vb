Public Class Form4

    Dim sda_zc As LiuDataAdapter   ';//全局变量
    Dim dt_zc As DataTable = New DataTable()

    Dim sda_BM As LiuDataAdapter   ';//全局变量
    Dim dt_BM As DataTable = New DataTable()

    Dim sda_LB As LiuDataAdapter   ';//全局变量
    Dim dt_LB As DataTable = New DataTable()

    Dim ComboBoxTreeLB As ComboBoxTreeView
    Dim ComboBoxTreeBM As ComboBoxTreeView

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim sql As String


        Dim trv As New TreeView()
        ComboBoxTreeLB = New ComboBoxTreeView()
        ComboBoxTreeLB.Dock = DockStyle.Fill
        Me.Panel1.Controls.Add(ComboBoxTreeLB)

        OpreaLBDataBase("")
        CommBindTreeView(0, ComboBoxTreeLB.TreeView, dt_LB, "parentlbdm", "0", "lbmc", "lbdm")
        'comboTrv.TreeView.Height = 400


        ComboBoxTreeBM = New ComboBoxTreeView()
        OpreaBMDataBase()
        CommBindTreeView(0, ComboBoxTreeBM.TreeView, dt_BM, "parentBMBH", "0", "BMMC", "BMBH")
        ComboBoxTreeBM.Dock = DockStyle.Fill
        Me.Panel2.Controls.Add(ComboBoxTreeBM)

        sql = String.Format("select * from zc")
        OpenDataBase(sda_zc, dt_zc, sql)
        GetJldw()
        Me.BindingText(dt_zc)
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Me.Dispose()
        Me.Close()
    End Sub
    Private Sub OpreaLBDataBase(bmbh As String)
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
        sda_LB = New LiuDataAdapter(sql, CONN_STR)

        dt_LB.Clear()
        sda_LB.Fill(dt_LB)

    End Sub


    Public Sub OpenDataBase(ByRef SQLda As LiuDataAdapter, ByRef dt As DataTable, sql As String)
        Try

            SQLda = New LiuDataAdapter(sql, CONN_STR)

            dt.Clear()
            SQLda.Fill(dt)


        Catch ex As SQLite.SQLiteException
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub BindingText(ByRef dt As DataTable)
        Dim bs As BindingSource = New BindingSource()
        bs.DataSource = dt
        Me.BindingNavigator1.BindingSource = bs

        'Me.BindingSource1 = bs

        TextBox1.DataBindings.Add("Text", bs, "zcbh", True)
        TextBox2.DataBindings.Add("Text", bs, "zcmc", True)
        TextBox3.DataBindings.Add("Text", bs, "lbid", True)
        ComboBoxTreeLB.DataBindings.Add("Text", bs, "lbmc", True)
        DateTimePicker1.DataBindings.Add("Text", bs, "gzrq", True)
        'TextBox4.DataBindings.Add("Text", bs, "lbmc", True)
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

    End Sub

    Private Sub BindingNavigatorAddNewItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorAddNewItem.Click

    End Sub

    Private Sub GetJldw()
        Dim sda_jldw As LiuDataAdapter
        Dim dt_jldw As DataTable = New DataTable()
        Dim str_sql_jldw As String
        str_sql_jldw = "select * from zd where item='计量单位'"
        Try

            sda_jldw = New LiuDataAdapter(str_sql_jldw, CONN_STR)

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
        sda_BM = New LiuDataAdapter(sql, CONN_STR)

        sda_BM.Fill(dt_BM)

        'G_dt.Load(reader)
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Dim SCB = New SQLite.SQLiteCommandBuilder(sda_zc)
        Dim dr As DataRow = dt_zc.NewRow()

        sda_zc.Update(dt_zc)

        MsgBox("更新成功")
    End Sub

    ''' <summary>
    ''' 
    ''' 通过ZC结构体传递参数
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetZcData() As ZcInfo
        Dim zcData As ZcInfo
        zcData = New ZcInfo()
        zcData.id = TextBox1.Text
        zcData.bmbh = TextBox2.Text

        GetZcData = zcData
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub
End Class


