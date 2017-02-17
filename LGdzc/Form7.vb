Imports System.Data.OleDb
Imports System.Data

Public Class Form7
    Dim TabPageIndex As Integer
    Dim strConn As String
    Dim pbDT As DataTable = New DataTable()
    Dim sda_imp As LiuDataAdapter   ';//全局变量
    Dim G_dt_imp As DataTable = New DataTable()
    Dim tablename As String

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TabPage2.Parent = Nothing
        Me.TabPage3.Parent = Nothing
        Me.TabPage4.Parent = Nothing
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Debug.Print(ListBox1.SelectedValue)
        If System.IO.Path.GetExtension(TextBox1.Text.ToLower) Like "*.xls*" And ListBox2.SelectedItem Is Nothing Then
            MsgBox("请选择Excel工作表")
        Else
            Me.TabPage1.Parent = Nothing
            Me.TabPage2.Parent = Me.TabControl1

        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim SheetName As String()
        OpenFileDialog1.Filter = "CSV|*.csv|Excel 2003文件|*.xls|Excel 2007文件|*.xlsx|所有文件|*.*"
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = OpenFileDialog1.FileName

            If Not System.IO.Path.GetExtension(TextBox1.Text.ToLower) Like ".xls*" And Not System.IO.Path.GetExtension(TextBox1.Text.ToLower) Like ".csv" Then
                MsgBox("导入Excel/CSV文件失败!失败原因：选择的不是Excel文件")
            ElseIf System.IO.Path.GetExtension(TextBox1.Text.ToLower) Like ".xls*" Then
                SheetName = GetAllSheetName(TextBox1.Text)
                For i As Integer = 0 To SheetName.Length - 1
                    If SheetName.Length > 0 Then
                        ListBox1.Items.Add(SheetName(i))
                    End If
                Next

            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Dispose()
        Me.Close()

    End Sub
    Public Shared Function GetAllSheetName(ByVal strFilePath As String) As String()
        Dim strConn As String = String.Empty
        If strFilePath.EndsWith("xls") Then
            strConn = "Provider=Microsoft.Jet.OLEDB.4.0; " + _
                       "Data Source=" + strFilePath + "; " + _
                       "Extended Properties='Excel 8.0;IMEX=1'"
        ElseIf strFilePath.EndsWith("xlsx") Then
            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + _
                      "Data Source=" + strFilePath + ";" + _
                      "Extended Properties='Excel 12.0;HDR=YES'"
        End If
        Dim conn As OleDbConnection = New OleDbConnection(strConn)
        conn.Open()


        Dim sheetNames(conn.GetSchema("Tables").Rows.Count - 1) As String
        For i As Integer = 0 To conn.GetSchema("Tables").Rows.Count - 1
            sheetNames(i) = conn.GetSchema("Tables").Rows(i)("TABLE_NAME").ToString
        Next
        conn.Close()
        Return sheetNames
    End Function

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Debug.Print(ListBox1.SelectedValue)



        If ListBox2.SelectedItem Is Nothing Then
            MsgBox("没有选择表")
        Else

            Me.TabPage3.Parent = Me.TabControl1
            Me.TabPage2.Parent = Nothing
            display()
        End If

    End Sub

    Private Sub display()


        Dim BM_Field_Array As String() = {"部门编号", "部门名称", "父级部门编号"}
        Dim ZD_Field_Array As String() = {"类别", "内容"}
        Dim LB_Field_Array As String() = {"资产类别代码", "资产类别名称", "父级资产类别代码"}
        Dim ZC_Field_Array As String() = {"资产编号", "资产名称", "资产类别"}

        'BM_Field_Array(0) = "d"
        'Dim str As String = (My.Computer.FileSystem.ReadAllText("C:\QD51-R24_A.csv"))

        Dim csvcls = New ExcelReader()
        Dim tb = New DataTable()
        tb = csvcls.ReadFile(TextBox1.Text)
        pbDT = tb.Copy

        Dim dcol As DataColumn
        Dim coln(tb.Columns.Count) As String

        Dim colNum As Integer = 0
        Dim dc As DataGridViewComboBoxColumn
        dc = DataGridView1.Columns(1)
        For Each dcol In tb.Columns

            colNum = colNum + 1
            coln(colNum) = dcol.ColumnName
            dc.Items.Add(coln(colNum))
            'Debug.Print(dcol.ColumnName)
        Next

        With DataGridView1

            .GridColor = Color.FromArgb(211, 222, 229)
            .BackgroundColor = Color.Wheat

            .RowsDefaultCellStyle.BackColor = Color.AliceBlue
            .RowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .RowsDefaultCellStyle.SelectionForeColor = Color.White
        End With

        Select Case ListBox2.SelectedItem.ToString
            Case "部门"
                tablename = "bm"
                For i = 0 To BM_Field_Array.Length - 1
                    Me.DataGridView1.Rows.Add()
                    Me.DataGridView1.Rows(i).Cells(0).Value = BM_Field_Array(i)
                Next
            Case "资产"
                tablename = "zc"
                For i = 0 To ZC_Field_Array.Length - 1
                    Me.DataGridView1.Rows.Add()
                    Me.DataGridView1.Rows(i).Cells(0).Value = ZC_Field_Array(i)
                Next
            Case "类别"
                tablename = "lb"
                For i = 0 To LB_Field_Array.Length - 1
                    Me.DataGridView1.Rows.Add()
                    Me.DataGridView1.Rows(i).Cells(0).Value = LB_Field_Array(i)
                Next
            Case "通用"
                tablename = "zd"
                For i = 0 To ZD_Field_Array.Length
                    Me.DataGridView1.Rows.Add()
                    Me.DataGridView1.Rows(i).Cells(0).Value = ZD_Field_Array(i)
                Next
        End Select



    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

    End Sub

    Private Function GetColumnIndex(ByVal dt As DataTable, ByVal columnName As String) As Integer
        For i As Integer = 0 To dt.Columns.Count - 1
            If dt.Columns(i).ColumnName = "columnName" Then
                Return i
                Exit Function
            End If
        Next
        Return -1

    End Function

    Private Sub AddColumnToArray(ByVal dt As DataTable, ByVal columnIndex As String, ByRef ary As ArrayList)
        For i As Integer = 0 To dt.Rows.Count - 1
            ary.Add(dt.Rows(i)(columnIndex))
        Next
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, _
               ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Debug.Print("ProcessCmdKey")
        If keyData = Keys.Enter Then

            ' ON ENTER KEY, GO TO THE NEXT CELL. 
            ' WHEN THE CURSOR REACHES THE LAST COLUMN, CARRY IT ON TO THE NEXT ROW.

            If ActiveControl.Name = "DataGridView1" Then
                With DataGridView1
                    If .CurrentCell.ColumnIndex = .ColumnCount - 1 Then             ' CHECK IF ITS THE LAST COLUMN
                        .CurrentCell = .Rows(.CurrentCell.RowIndex + 1).Cells(0)    ' GO TO THE FIRST COLUMN, NEXT ROW.
                    Else
                        .CurrentCell = .Rows(.CurrentRow.Index).Cells(.CurrentCell.ColumnIndex + 1)     ' NEXT COLUMN.
                    End If
                End With

            ElseIf TypeOf ActiveControl Is DataGridViewTextBoxEditingControl Then

                ' SHOW THE COMBOBOX WHEN FOCUS IS ON A CELL CORRESPONDING TO THE "QUALIFICATION" COLUMN.
                With DataGridView1
                    If .Columns(.CurrentCell.ColumnIndex).Name = "PresentAddress" Then
                        .CurrentCell = .Rows(.CurrentRow.Index).Cells(.CurrentCell.ColumnIndex + 1)

                        ' SHOW COMBOBOX.
                        'Show_Combobox(.CurrentRow.Index, .CurrentCell.ColumnIndex)
                        SendKeys.Send("{F4}")               ' DROP DOWN THE LIST.
                    Else
                        If .CurrentCell.ColumnIndex = .ColumnCount - 1 Then             ' CHECK IF ITS THE LAST COLUMN
                            .CurrentCell = .Rows(.CurrentCell.RowIndex + 1).Cells(0)    ' GO TO THE FIRST COLUMN, NEXT ROW.
                        Else
                            .CurrentCell = .Rows(.CurrentRow.Index).Cells(.CurrentCell.ColumnIndex + 1)     ' NEXT COLUMN.
                        End If
                    End If
                End With

            Else
                SendKeys.Send("{TAB}")
            End If
            Return True

        Else
            Return MyBase.ProcessCmdKey(msg, keyData)
        End If
        Return True
    End Function

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.TabPage3.Parent = Nothing
        Me.TabPage4.Parent = Me.TabControl1
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim ImdT As New DataTable()
        Dim databaseColName, csvColName As String
        Dim datatable2 As New DataTable()

        'For i As Integer = 0 To DataGridView1.Rows.Count - 2
        'databaseColName = DataGridView1.Rows(i).Cells(0).Value
        'csvColName = DataGridView1.Rows(i).Cells(1).Value
        'DataColumn dc = new DataColumn("column1", System.Type.GetType("System.Boolean"));
        'DataColumn dc = New DataColumn(colName, )
        'ImdT.Columns.Add(colName, pbDT.Columns(DataGridView1.Rows(i).Cells(1).Value))
        'Dim col As DataColumn = New DataColumn(colName)
        'Dim col As DataColumn = New DataColumn()
        'col.DataType = System.Type.[GetType]("System.String")
        'col = pbDT.Columns(DataGridView1.Rows(i).Cells(1).Value)
        'Dim rowmm As DataRow = New DataRow()
        'pbDT.Columns.Item(1).
        'ImdT.Columns.Add(col)

        'idColumn.DataType = System.Type.GetType("System.Int32")
        'ImdT.Rows.Add()
        'ImdT.Columns(0).DataType = System.Type.[GetType]("System.String")
        'ImdT.Columns(0).ColumnName = colName
        'Dim dr2 As System.Data.DataRow = Nothing
        'For Each dr1 As System.Data.DataRow In pbDT.Rows
        'dr2 = ImdT.NewRow()
        'dr2(0) = dr1(colName)

        'ImdT.Rows.Add(dr2.ItemArray)
        'Next




        Dim dr1 As DataRow = Nothing



        For mi As Integer = 0 To pbDT.Rows.Count - 1
            dr1 = ImdT.NewRow()
            For i As Integer = 0 To DataGridView1.Rows.Count - 2
                databaseColName = DataGridView1.Rows(i).Cells(0).Value
                csvColName = DataGridView1.Rows(i).Cells(1).Value
                If mi = 0 Then
                    ImdT.Columns.Add(databaseColName, Type.[GetType]("System.String"))
                End If
                dr1(databaseColName) = pbDT.Rows(mi)(csvColName).ToString()
                'dr1(i) = pbDT.Rows(mi)(csvColName).ToString()
            Next

            ImdT.Rows.Add(dr1)
        Next
        DataGridView2.DataSource = ImdT


        With DataGridView2

            .GridColor = Color.FromArgb(211, 222, 229)
            .BackgroundColor = Color.Wheat

            .RowsDefaultCellStyle.BackColor = Color.AliceBlue
            .RowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .RowsDefaultCellStyle.SelectionForeColor = Color.White
        End With
        '添加列数据
        'dt.Columns.Add("column0", System.Type.GetType("System.String"));
        ' //Method 2
        ' DataColumn dc = new DataColumn("column1", System.Type.GetType("System.Boolean"));
        ' dt.Columns.Add(dc); 
        Button8.Visible = True
        Button7.Visible = False
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Button8.Visible = False
        Dim sql As String
        Sql = "select * from " + tablename
        sda_imp = New LiuDataAdapter(sql, CONN_STR)

        G_dt_imp.Clear()
        sda_imp.Fill(G_dt_imp)

        'Dim SCB = New SQLite.SQLiteCommandBuilder(sda_imp)

        sda_imp.Update(G_dt_imp)
        MsgBox("更新成功")
        'OpreaRYDataBase("")
    End Sub
End Class
