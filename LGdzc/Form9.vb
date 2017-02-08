﻿Public Class Form9
    Private ComboBoxTreeLB As ComboBoxTreeView

    Private sda_LB As SQLite.SQLiteDataAdapter   ';//全局变量
    Private dt_LB As DataTable = New DataTable()
    Private Sub SetNewAdd()
        ComboBoxTreeLB.Text = ""
        ComboBoxTreeLB.TreeView.SelectedNode.Name = ""
        TextBox3.Text = ""
        ComboBox3.Text = ""
        TextBox4.Text = ""
        DateTimePicker1.Text = ""
        DateTimePicker2.Text = ""
        TextBox5.Text = ""
        TextBox1.Text = ""
        ComboBox2.Text = ""
        TextBox7.Text = ""
        TextBox2.Text = ""
        TextBox8.Text = ""
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        SetNewAdd()
    End Sub

    Private Sub Save()
        Dim SQLconn As New Data.SQLite.SQLiteConnection '定义数据库链接  
        Dim sqlcmd As New SQLite.SQLiteCommand '定义查询操作  
        Dim ds As New DataSet
        Dim salda As New SQLite.SQLiteDataAdapter
        Try
            SQLconn.ConnectionString = CONN_STR '链接数据库  
            SQLconn.Open()
            sqlcmd.Connection = SQLconn
            sqlcmd.CommandText = "insert into rk ('lbmc','lbid','zcmc','cgfs','ghs','gzrq','dhrq','price','cgsl','jldw','qsr','cgxmmc','pz','kucun') values('" + ComboBoxTreeLB.Text + "','" + ComboBoxTreeLB.TreeView.SelectedNode.Name + "','" + TextBox3.Text + "','" + ComboBox3.Text + "','" + TextBox4.Text + "','" + DateTimePicker1.Text + "','" + DateTimePicker2.Text + "'," + TextBox5.Text + "," + TextBox1.Text + ",'" + ComboBox2.Text + "','" + TextBox7.Text + "','" + TextBox2.Text + "','" + TextBox8.Text + "'," + TextBox1.Text + ")"
            Dim sqlreader As SQLite.SQLiteDataReader = sqlcmd.ExecuteReader
            salda = New SQLite.SQLiteDataAdapter(sqlcmd.CommandText, SQLconn)
            'SQLite.SQLiteHelper.ExecuteDataset(constr, CommandType.Text, Sql)
            'salda.Fill(ds, 0)
            'DGV1.DataSource = ds.Tables(0)
            MsgBox("添加成功！")

        Finally
            If Not (SQLconn Is Nothing) Then SQLconn.Dispose()
            SQLconn = Nothing
            If Not (salda Is Nothing) Then salda.Dispose()
            salda = Nothing
        End Try
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Save()


    End Sub

    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ComboBoxTreeLB = New ComboBoxTreeView()
        ComboBoxTreeLB.Dock = DockStyle.Fill
        Me.Panel2.Controls.Add(ComboBoxTreeLB)

        OpreaLBDataBase("")
        CommBindTreeView(0, ComboBoxTreeLB.TreeView, dt_LB, "parentlbdm", "0", "lbmc", "lbdm")
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
        sda_LB = New SQLite.SQLiteDataAdapter(sql, CONN_STR)
        Dim scb As SQLite.SQLiteCommandBuilder = New SQLite.SQLiteCommandBuilder(sda_LB)
        dt_LB.Clear()
        sda_LB.Fill(dt_LB)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Save()
        SetNewAdd()

    End Sub
End Class