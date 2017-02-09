Imports System.Windows.Forms

Public Class MDIParent1

    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs) Handles NewToolStripButton.Click, NewWindowToolStripMenuItem.Click
        ' 创建此子窗体的一个新实例。
        Dim ChildForm As New System.Windows.Forms.Form
        ' 在显示该窗体前使其成为此 MDI 窗体的子窗体。
        ChildForm.MdiParent = Me

        m_ChildFormNumber += 1
        ChildForm.Text = "窗口 " & m_ChildFormNumber

        ChildForm.Show()
    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs) Handles OpenToolStripButton.Click
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            ' TODO:  在此处添加打开文件的代码。
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO:  在此处添加代码，将窗体的当前内容保存到一个文件中。
        End If
    End Sub


    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.Close()
    End Sub



    Private Sub ToolBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolBarToolStripMenuItem.Click
        Me.ToolStrip.Visible = Me.ToolBarToolStripMenuItem.Checked
    End Sub

    Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StatusBarToolStripMenuItem.Click
        Me.StatusStrip.Visible = Me.StatusBarToolStripMenuItem.Checked
    End Sub



    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CloseAllToolStripMenuItem.Click
        ' 关闭此父窗体的所有子窗体。
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private m_ChildFormNumber As Integer


    Private Sub BmToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BmToolStripMenuItem.Click

    End Sub

    Private Sub LbToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LbToolStripMenuItem.Click


    End Sub

    Private Sub ExploreZCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExploreZCToolStripMenuItem.Click

    End Sub

    Private Sub SplitContainer1_MouseClick(sender As Object, e As MouseEventArgs) Handles SplitContainer1.MouseClick
        SplitContainer1.Panel1Collapsed = True
    End Sub



    Private Sub HideToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HideToolStripMenuItem.Click
        SplitContainer1.Panel1Collapsed = True
        'SplitContainer1.Panel2.Controls.GetEnumerator.Current
    End Sub

    Private Sub ShowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowToolStripMenuItem.Click
        SplitContainer1.Panel1Collapsed = False
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        AboutBox1.ShowDialog()
    End Sub

    Private Sub ExitToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem1.Click
        Me.Close()
    End Sub

    Private Sub JldwToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ZdToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZdToolStripMenuItem.Click
        'Me.OpenTYXX()
    End Sub

    Private Sub ImportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportToolStripMenuItem.Click
        Dim fm As New Form7()
        Me.OpenChildWindow(fm)
    End Sub

    Private Sub CaclToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CaclToolStripMenuItem.Click
        System.Diagnostics.Process.Start("C:\WINDOWS\system32\calc.exe")
    End Sub

    Private Sub TxtToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TxtToolStripMenuItem.Click
        System.Diagnostics.Process.Start("C:\WINDOWS\system32\notepad.exe")
        'System.Environment.GetEnvironmentVariable("SYSTEMROOT")
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect

    End Sub

    Private Sub TreeView1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles TreeView1.MouseDoubleClick
        Dim SelectedNode As TreeNode = TreeView1.SelectedNode
        Dim fm As Form
        Select Case SelectedNode.Text
            Case "资产信息管理"
                fm = New Form3()
                Me.OpenChildWindow(fm)
            Case "部门及人员管理"
                fm = New Form1()
                Me.OpenChildWindow(fm)
            Case "通用信息管理"
                fm = New Form6()
                Me.OpenChildWindow(fm)
            Case "资产类别管理"
                fm = New Form2()
                Me.OpenChildWindow(fm)
            Case "数据导入"
                fm = New Form7()
                Me.OpenChildWindow(fm)
            Case "设备入库"
                fm = New Form9()
                Me.OpenChildWindow(fm)
            Case "新入库设备分配"
                fm = New Form8()
                Me.OpenChildWindow(fm)
            Case "查看入库设备"
                fm = New LLRKZC()
                Me.OpenChildWindow(fm)
            Case "归还设备"
                fm = New zcjh()
                Me.OpenChildWindow(fm)
            Case "资产调拨"
                fm = New zclygh()
                Me.OpenChildWindow(fm)
        End Select
    End Sub
    Private Sub OpenChildWindow(f As Form)
        f.TopLevel = False
        'Me.Controls.Add(f)
        'f.FormBorderStyle = Windows.Forms.FormBorderStyle.SizableToolWindow
        f.FormBorderStyle = FormBorderStyle.None
        Me.SplitContainer1.Panel2.Controls.Add(f)
        f.Show()
    End Sub

    Private Sub MDIParent1_Load(sender As Object, e As EventArgs) Handles Me.Load
        TreeView1.ExpandAll()
    End Sub

    Private Sub MenuStrip_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip.ItemClicked

    End Sub

    Private Sub ToolStrip_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolStrip.ItemClicked

    End Sub

    Private Sub ZcrkToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ZcrkToolStripMenuItem1.Click
        Dim f As New Form9()
        Me.OpenChildWindow(f)
    End Sub

    Private Sub SplitContainer1_Panel2_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel2.Paint

    End Sub
End Class
