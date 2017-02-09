Public Class LLRKZC


    Private Sub GetKuCun()
        'MsgBox(System.Environment.GetEnvironmentVariable("SYSTEMROOT"))
        Dim TB As DataTable = New DataTable()
        Dim conn As Data.SQLite.SQLiteConnection = New Data.SQLite.SQLiteConnection(CONN_STR)

        ' MsgBox(System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
        '打开连接
        conn.Open()
        'Dim cmd As SQLite.SQLiteCommand = New SQLite.SQLiteCommand(conn)
        'Dim sql As String = "select * from rk where kucun>0 order by id desc"
        Dim sql As String = "select * from rk order by id desc"
        'ds = SQLite.SQLiteCommand SQLiteHelper.SQLiteCommandDataSet(DBConStr, sqlStr, Nothing)
        'Dim reader As SQLite.SQLiteDataReader = cmd.ExecuteReader()
        Dim sda As SQLite.SQLiteDataAdapter = New SQLite.SQLiteDataAdapter(sql, CONN_STR)
        Dim scb As SQLite.SQLiteCommandBuilder = New SQLite.SQLiteCommandBuilder(sda)

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

        'DataGridView1.Columns(3).Frozen = True
        'DataGridView1.Columns

        'dataAdapter.SelectCommand = cmd
        'dataAdapter.Fill(dst, "info")
        'dt = dst.Tables("info")
        'sqlConnection1.Close()   '关闭数据库   


        'DataGridView1.AutoGenerateColumns = True '自动创建列   
        'DataGridView1.DataSource = sda

    End Sub




    Private Sub DataGridView1_RowHeaderMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs)

    End Sub

    Private Sub LLRKZC_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        GetKuCun()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub SplitContainer1_Panel2_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel2.Paint

    End Sub
End Class


