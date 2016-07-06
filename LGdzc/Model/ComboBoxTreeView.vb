Imports System.Data
Imports System.Text
Imports System.Windows.Forms
Public Class ComboBoxTreeView
    Inherits ComboBox
    Private Const WM_LBUTTONDOWN As Integer = &H201, WM_LBUTTONDBLCLK As Integer = &H203
    Private treeViewHost As ToolStripControlHost
    Private dropDown As ToolStripDropDown
    Public Sub New()
        Dim treeView As New TreeView()
        AddHandler treeView.AfterSelect, New TreeViewEventHandler(AddressOf treeView_AfterSelect)
        treeView.BorderStyle = BorderStyle.None
        'treeView.Height = 800
        treeViewHost = New ToolStripControlHost(treeView)
        dropDown = New ToolStripDropDown()
        dropDown.Width = Me.Width
        dropDown.Items.Add(treeViewHost)

    End Sub
    Public Sub treeView_AfterSelect(sender As Object, e As TreeViewEventArgs)
        Me.Text = TreeView.SelectedNode.Text
        dropDown.Close()
    End Sub
    Public ReadOnly Property TreeView() As TreeView
        Get
            Return TryCast(treeViewHost.Control, TreeView)
        End Get
    End Property
    Private Sub ShowDropDown()
        If dropDown IsNot Nothing Then

            '控制TreeView的高度和宽度
            treeViewHost.Size = New Size(DropDownWidth - 2, DropDownHeight * 3)
            dropDown.Show(Me, 0, Me.Height)
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
            If dropDown IsNot Nothing Then
                dropDown.Dispose()
                dropDown = Nothing
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
End Class


