Module Comm


    Public Sub IsInputNum(e As KeyPressEventArgs)
        If (Not Char.IsNumber(e.KeyChar) And e.KeyChar <> Chr(8)) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
        Dim a As ZcInfo
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
End Module
