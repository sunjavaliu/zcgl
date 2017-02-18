Imports System.Text.RegularExpressions

Module Comm


    Public Sub IsInputNum(e As KeyPressEventArgs)
        If (Not Char.IsNumber(e.KeyChar) And e.KeyChar <> Chr(8)) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub
    ''' <summary>
    ''' 实现textbox只能输入X.Y的小数，最大7位，Y最大5位。
    ''' </summary>
    ''' <param name="e"></param>
    ''' <param name="t"></param>
    ''' <remarks></remarks>
    Public Sub IsInputDigit(e As KeyPressEventArgs, t As TextBox)
        If e.KeyChar = Chr(8) Then Exit Sub

        If InStr(t.Text, ".") = 0 Then t.MaxLength = 12 Else t.MaxLength = 13
        If Len(t.Text) - InStr(t.Text, ".") = 5 And Len(t.Text) <> 5 Then e.KeyChar = Chr(0)
        If Len(t.Text) = 7 And InStr(t.Text, ".") = 0 Then
            e.KeyChar = "."
        Else
            e.Handled = False
        End If
        If e.KeyChar = Chr(46) And InStr(t.Text, ".") = 0 Then Exit Sub
        If e.KeyChar < Chr(48) Or e.KeyChar > Chr(57) Then e.KeyChar = Chr(0)

    End Sub

    Public Sub CommBindTreeView(ID As Long, ByRef treeview As TreeView, ByRef dt As DataTable, WhereFiled1 As String, WhereFiledArg As String, TreeNodeText As String, TreeNodeName As String)

        treeview.Nodes.Clear()

        Dim parentrow As DataRow() = dt.[Select](WhereFiled1 & "=" & WhereFiledArg)

        For i As Integer = 0 To parentrow.Length - 1
            Dim rootnode As New TreeNode()
            rootnode.Text = parentrow(i)(TreeNodeText).ToString() '+ "[" + parentrow(i)("lbdm").ToString() + "]"
            rootnode.Name = parentrow(i)(TreeNodeName).ToString()
            rootnode.StateImageIndex = 1

            treeview.Nodes.Add(rootnode)


            CommCreateTreeChildNode(rootnode, dt, WhereFiled1, "", TreeNodeText, TreeNodeName)
        Next
        treeview.Nodes(0).Expand()
    End Sub
    Public Sub CommCreateTreeChildNode(ByRef parentNode As TreeNode, ByRef datatable As DataTable, WhereFiled1 As String, WhereFiledArg As String, TreeNodeText As String, TreeNodeName As String)
        Dim rowlist As DataRow() = datatable.[Select](WhereFiled1 & "=" & Convert.ToString(parentNode.Name))
        For i As Integer = 0 To rowlist.Length - 1
            Dim node As New TreeNode()
            node.ToolTipText = "单击左键或右键进行选择操作"
            If datatable.[Select](WhereFiled1 & "=" & rowlist(i)(TreeNodeName).ToString().Trim()).Length > 0 Then
                node.Text = rowlist(i)(TreeNodeText).ToString() '+ "[" + rowlist(i)("lbdm").ToString() + "]"
                node.Name = rowlist(i)(TreeNodeName).ToString()
            Else
                node.Text = rowlist(i)(TreeNodeText).ToString() '+ "[" + rowlist(i)("lbdm").ToString() + "]"
                node.Name = rowlist(i)(TreeNodeName).ToString()
            End If
            'node.StateImageIndex = 1
            parentNode.Nodes.Add(node)
            '递归调用
            CommCreateTreeChildNode(node, datatable, WhereFiled1, "", TreeNodeText, TreeNodeName)
        Next
    End Sub

    '获取统计局内部资产编号
    Public Function GetZCBH(lbid As String)
        Dim zcbh As String
        Dim d1970 As New System.DateTime(1970, 1, 1, 0, 0, 0, 0)
        Dim iSeconds As Long

        iSeconds = (Now.Ticks - d1970.Ticks) / 10000000
        zcbh = "TJJ" + DateTime.Now.ToString("yyyyMMddHHmmss") + lbid + CStr(iSeconds)
        Return zcbh
    End Function


    '获取入库编号
    Public Function GetRKBH()
        Dim RKBH As String
        Dim d1970 As New System.DateTime(1970, 1, 1, 0, 0, 0, 0)
        Dim iSeconds As Long

        iSeconds = (Now.Ticks - d1970.Ticks) / 10000000
        RKBH = "RKBH" + DateTime.Now.ToString("yyyyMMddHHmmss") + CStr(iSeconds)
        Return RKBH
    End Function


    '获取通用字典
    Public Sub GetComboBoxDICT(item As String, combox As ComboBox)
        Dim dt = New DataTable()
        'Dim conn As Data.SQLite.SQLiteConnection = New Data.SQLite.SQLiteConnection(CONN_STR)
        '打开连接
        'conn.Open()
        'Dim cmd As SQLite.SQLiteCommand = New SQLite.SQLiteCommand(conn)
        Dim sql As String = "select content from zd where item='" + item + "'"
        'ds = SQLite.SQLiteCommand SQLiteHelper.SQLiteCommandDataSet(DBConStr, sqlStr, Nothing)
        'Dim reader As SQLite.SQLiteDataReader = cmd.ExecuteReader()
        Dim sda = New LiuDataAdapter(sql, CONN_STR)
        'Dim scb As SQLite.SQLiteCommandBuilder = New SQLite.SQLiteCommandBuilder(sda)

        sda.Fill(dt)
        combox.DataSource = dt
        combox.DisplayMember = "content"
        If dt.Rows.Count = 0 Then
            'MsgBox("该部门下没有人员信息，请先添加人员信息")
            combox.Text = ""
        End If
    End Sub

    '导出CSV数据
    Public Sub ExportToCSV(ByVal dgv As DataGridView, Optional ByVal strfilename As String = "")
        Dim saveFileDialog As New SaveFileDialog()
        'saveFileDialog.Filter = "Execl files (*.xls)|*.xls"
        saveFileDialog.Filter = "CSV文件(*.csv)|*.csv|所有文件(*.*)|*.*"
        saveFileDialog.FilterIndex = 0
        saveFileDialog.FileName = strfilename
        saveFileDialog.RestoreDirectory = True
        'saveFileDialog.CreatePrompt = True
        saveFileDialog.Title = "保存为CSV文件"
        saveFileDialog.ShowDialog()

        If saveFileDialog.FileName.IndexOf(":") < 0 Then
            Exit Sub
        End If
        '被点了"取消" 
        Dim myStream As IO.FileStream
        myStream = saveFileDialog.OpenFile()
        Dim sw As New IO.StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0))
        Dim columnTitle As String = ""
        Try
            '写入列标题 
            For i As Integer = 0 To dgv.ColumnCount - 1
                If i > 0 Then
                    columnTitle += ","
                End If
                columnTitle += dgv.Columns(i).HeaderText
            Next

            sw.WriteLine(columnTitle)

            '写入列内容 
            For j As Integer = 0 To dgv.Rows.Count - 1
                Dim columnValue As String = ""
                For k As Integer = 0 To dgv.Columns.Count - 1
                    If k > 0 Then
                        columnValue += ","
                    End If
                    If dgv.Rows(j).Cells(k).Value Is Nothing Then
                        columnValue += ","
                    Else
                        columnValue += dgv.Rows(j).Cells(k).FormattedValue.ToString.Trim()
                    End If
                Next

                '过滤去全空的行，一般为最后一行空数据
                If columnValue <> ",,,,," Then
                    sw.WriteLine(columnValue)
                End If
            Next
            sw.Close()
            myStream.Close()
        Catch e As Exception
            MessageBox.Show(e.ToString())
        Finally
            sw.Close()
            myStream.Close()
        End Try
    End Sub

    ''' <summary>
    ''' 防止SQL注入
    ''' </summary>
    ''' <param name="parms"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValiParms(parms As String) As Boolean
        If parms Is Nothing Then
            Return False
        End If

        Dim re1 As New Regex("sp_", RegexOptions.IgnoreCase)
        Dim re2 As New Regex("'", RegexOptions.IgnoreCase)
        Dim re3 As New Regex("create", RegexOptions.IgnoreCase)
        Dim re4 As New Regex("drop", RegexOptions.IgnoreCase)
        Dim re5 As New Regex("select", RegexOptions.IgnoreCase)
        Dim re6 As New Regex("""", RegexOptions.IgnoreCase)
        Dim re7 As New Regex("exec", RegexOptions.IgnoreCase)
        Dim re8 As New Regex("xp_", RegexOptions.IgnoreCase)
        Dim re9 As New Regex("insert", RegexOptions.IgnoreCase)
        Dim re10 As New Regex("update", RegexOptions.IgnoreCase)

        If re1.IsMatch(parms) Then
            Return True
        End If
        If re2.IsMatch(parms) Then
            Return True
        End If
        If re3.IsMatch(parms) Then
            Return True
        End If
        If re4.IsMatch(parms) Then
            Return True
        End If
        If re5.IsMatch(parms) Then
            Return True
        End If
        If re6.IsMatch(parms) Then
            Return True
        End If
        If re7.IsMatch(parms) Then
            Return True
        End If
        If re8.IsMatch(parms) Then
            Return True
        End If
        If re9.IsMatch(parms) Then
            Return True
        End If
        If re10.IsMatch(parms) Then
            Return True
        End If

        Return False
    End Function



End Module
