Imports System.Data
Imports System.Text
Imports System.Windows.Forms
Public Class ComboBoxTreeView
    Inherits ComboBox
    Private Const WM_LBUTTONDOWN As Integer = &H201, WM_LBUTTONDBLCLK As Integer = &H203
    Private treeViewHost As ToolStripControlHost
    Private dropDownTree As ToolStripDropDown
    Private IsFind As Boolean
    Public Sub New()
        Dim treeView As New TreeView()
        'AddHandler Me.TextUpdate, New System.EventHandler(AddressOf ComboBoxTreeViewTextUpdate)
        AddHandler treeView.AfterSelect, New TreeViewEventHandler(AddressOf treeView_AfterSelect)

        '添加鼠标双击事件
        AddHandler treeView.NodeMouseDoubleClick, New TreeNodeMouseClickEventHandler(AddressOf treeView_NodeMouseDoubleClick)
        'AddHandler treeView, New TreeNodeMouseClickEventHandler(AddressOf treeView_NodeMouseDoubleClick)
        'treeView_AfterSelect

        treeView.BorderStyle = BorderStyle.None
        treeViewHost = New ToolStripControlHost(treeView)
        dropDownTree = New ToolStripDropDown()
        dropDownTree.Width = Me.Width
        dropDownTree.Items.Add(treeViewHost)

    End Sub

    'Public Sub TextChange(sender As Object, e As TreeNodeMouseClickEventArgs)
    '    Me.Text = TreeView.SelectedNode.Text
    '    dropDownTree.Close()
    'End Sub

    Public Sub treeView_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs)
        If (TreeView.SelectedNode Is Nothing) Then
            'MsgBox("没有选中节点！")
            Return
        End If
        Me.Text = TreeView.SelectedNode.Text
        dropDownTree.Close()
    End Sub
    Public Sub treeView_AfterSelect(sender As Object, e As TreeViewEventArgs)
        'Me.Text = TreeView.SelectedNode.Text
        'dropDown.Close()
        'MsgBox("ha")
    End Sub
    'Public Sub ComboBoxTreeViewTextUpdate(sender As Object, e As System.EventArgs)
    'Me.Text = TreeView.SelectedNode.Text
    'dropDown.Close()
    '    MsgBox("ComboBoxTreeViewTextUpdate")
    'End Sub

    Public Sub FindNode()
        IsFind = False
        If Me.Text Is Nothing Or Me.Text.Trim.Equals("") Then
            IsFind = True
            Return
        Else
            IsFind = False
            TreeView.Focus()
            TreeView.Nodes(0).Expand()
            For Each node As TreeNode In TreeView.Nodes
                If Not IsFind Then
                    FindNodeRecursion(node)
                End If
            Next
        End If
    End Sub
    Private Sub FindNodeRecursion(ByRef TreeNodes As TreeNode)
        Debug.Print(Me.Text)
        If TreeNodes Is Nothing Or Me.Text Is Nothing Or Me.Text.Trim.Equals("") Then
            IsFind = True
            Return
        End If

        If TreeNodes.Text.Equals(Me.Text) Then
            IsFind = True
            TreeView.SelectedNode = TreeNodes
            TreeNodes.Expand()
            Return
        End If

        For Each node As TreeNode In TreeNodes.Nodes
            If Not IsFind Then
                FindNodeRecursion(node)
                'node.Expand()
            End If
        Next
    End Sub
    Public ReadOnly Property TreeView() As TreeView
        Get
            Return TryCast(treeViewHost.Control, TreeView)
        End Get
    End Property
    Private Sub ShowDropDown()
        If dropDownTree IsNot Nothing Then

            '控制TreeView的高度和宽度
            treeViewHost.Size = New Size(DropDownWidth - 2, DropDownHeight * 3)
            dropDownTree.Show(Me, 0, Me.Height)

            Me.FindNode()

        End If
    End Sub


    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = WM_LBUTTONDBLCLK OrElse m.Msg = WM_LBUTTONDOWN Then
            ShowDropDown()
            Return
        End If
        MyBase.WndProc(m)
    End Sub
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            If dropDownTree IsNot Nothing Then
                dropDownTree.Dispose()
                dropDownTree = Nothing
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub



End Class


