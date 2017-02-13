Module Comm


    Public Sub IsInputNum(e As KeyPressEventArgs)
        If (Not Char.IsNumber(e.KeyChar) And e.KeyChar <> Chr(8)) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
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
            node.ToolTipText = "单击右键进行编辑操作"
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
        zcbh = "TJJ" + lbid + CStr(iSeconds)
        Return zcbh
    End Function


    '获取入库编号
    Public Function GetRKBH()
        Dim RKBH As String
        Dim d1970 As New System.DateTime(1970, 1, 1, 0, 0, 0, 0)
        Dim iSeconds As Long

        iSeconds = (Now.Ticks - d1970.Ticks) / 10000000
        RKBH = "RKBH" + CStr(iSeconds)
        Return RKBH
    End Function


    '获取通用字典
    Public Sub GetComboBoxDICT(item As String, combox As ComboBox)
        Dim dt = New DataTable()
        Dim conn As Data.SQLite.SQLiteConnection = New Data.SQLite.SQLiteConnection(CONN_STR)
        '打开连接
        conn.Open()
        'Dim cmd As SQLite.SQLiteCommand = New SQLite.SQLiteCommand(conn)
        Dim sql As String = "select content from zd where item='" + item + "'"
        'ds = SQLite.SQLiteCommand SQLiteHelper.SQLiteCommandDataSet(DBConStr, sqlStr, Nothing)
        'Dim reader As SQLite.SQLiteDataReader = cmd.ExecuteReader()
        Dim sda = New SQLite.SQLiteDataAdapter(sql, CONN_STR)
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
                    ColumnTitle += ","
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
End Module
