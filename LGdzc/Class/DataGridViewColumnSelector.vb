Imports System.IO

Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing

Public Class DataGridViewColumnSelector
    ' the DataGridView to which the DataGridViewColumnSelector is attached
    Private WithEvents mDataGridView As DataGridView = Nothing
    ' a CheckedListBox containing the column header text and checkboxes
    Public WithEvents mCheckedListBox As New CheckedListBox
    'Private mCheckedListBox As CheckedListBox 'original code
    ' a ToolStripDropDown object used to show the popup
    Private mPopup As ToolStripDropDown
    Private xmlFile As String = String.Empty
    ''' <summary>
    ''' The max height of the popup
    ''' </summary>
    Public MaxHeight As Integer = 800
    ''' <summary>
    ''' The width of the popup
    ''' </summary>
    Public Width As Integer = 200
    'Public Event mDataGridView_CellMouseClick()
    ''' <summary>
    ''' Gets or sets the DataGridView to which the DataGridViewColumnSelector is attached
    ''' </summary>
    Public Property DataGridView() As DataGridView
        Get
            Return mDataGridView
        End Get

        Set(ByVal value As DataGridView)
            ' If any, remove handler from current DataGridView 
            If Not (mDataGridView Is Nothing) Then
                'mDataGridView.CellMouseClick -= New DataGridViewCellMouseEventHandler(AddressOf mDataGridView_CellMouseClick)
                RemoveHandler mDataGridView.CellMouseClick, New DataGridViewCellMouseEventHandler(AddressOf mDataGridView_CellMouseClick)

                'mDataGridView.ColumnDisplayIndexChanged -= New DataGridViewColumnEventHandler(AddressOf mDataGridView_ColumnDisplayIndexChanged)
                RemoveHandler mDataGridView.ColumnDisplayIndexChanged, New DataGridViewColumnEventHandler(AddressOf mDataGridView_ColumnDisplayIndexChanged)

            End If
            ' Set the new DataGridView
            mDataGridView = value
            ' Attach CellMouseClick handler to DataGridView
            If Not (mDataGridView Is Nothing) Then
                'mDataGridView.CellMouseClick += New DataGridViewCellMouseEventHandler(AddressOf mDataGridView_CellMouseClick)
                AddHandler mDataGridView.CellMouseClick, New DataGridViewCellMouseEventHandler(AddressOf mDataGridView_CellMouseClick)

                AddHandler mDataGridView.ColumnDisplayIndexChanged, New DataGridViewColumnEventHandler(AddressOf mDataGridView_ColumnDisplayIndexChanged)

            End If
        End Set
    End Property

    ' When user right-clicks the cell origin, it clears and fill the CheckedListBox with
    ' columns header text. Then it shows the popup. 
    ' In this way the CheckedListBox items are always refreshed to reflect changes occurred in 
    ' DataGridView columns (column additions or name changes and so on).
    'Public Event mDataGridView_CellMouseClick(ByVal sender As Object, ByVal e As DataGridViewCellMouseEventArgs)

    Private Sub mDataGridView_CellMouseClick(ByVal sender As Object, ByVal e As DataGridViewCellMouseEventArgs) Handles mDataGridView.CellMouseClick 'original code
        If e.Button = MouseButtons.Right AndAlso e.RowIndex = -1 AndAlso e.ColumnIndex = -1 Then
            mCheckedListBox.Items.Clear()
            For Each c As DataGridViewColumn In mDataGridView.Columns
                mCheckedListBox.Items.Add(c.HeaderText, c.Visible)
            Next
            Dim PreferredHeight As Integer = (mCheckedListBox.Items.Count * 19) + 7
            'MsgBox(mCheckedListBox.Items.Count)
            mCheckedListBox.Height = If((PreferredHeight < MaxHeight), PreferredHeight, MaxHeight)
            mCheckedListBox.Width = Me.Width
            mPopup.Show(mDataGridView.PointToScreen(New Point(e.X, e.Y)))
        End If
    End Sub
    'end event

    ' The constructor creates an instance of CheckedListBox and ToolStripDropDown.
    ' the CheckedListBox is hosted by ToolStripControlHost, which in turn is
    ' added to ToolStripDropDown.
    Public Sub New()
        mCheckedListBox = New CheckedListBox()
        mCheckedListBox.CheckOnClick = True
        'mCheckedListBox.ItemCheck += New ItemCheckEventHandler(AddressOf mCheckedListBox_ItemCheck)
        AddHandler mCheckedListBox.ItemCheck, New ItemCheckEventHandler(AddressOf mCheckedListBox_ItemCheck)

        Dim mControlHost As New ToolStripControlHost(mCheckedListBox)
        mControlHost.Padding = Padding.Empty
        mControlHost.Margin = Padding.Empty
        mControlHost.AutoSize = False

        mPopup = New ToolStripDropDown()
        mPopup.Padding = Padding.Empty
        mPopup.Items.Add(mControlHost)
    End Sub

    Public Sub New(ByVal dgv As DataGridView)
        Me.New()
        Me.DataGridView = dgv
    End Sub
    Public Sub New(dgv As DataGridView, xmlFile As String)
        Me.New()
        Me.DataGridView = dgv
        Me.xmlFile = xmlFile
        'mDataGridView.ColumnDisplayIndexChanged -= New DataGridViewColumnEventHandler(AddressOf mDataGridView_ColumnDisplayIndexChanged)
        RemoveHandler mDataGridView.ColumnDisplayIndexChanged, New DataGridViewColumnEventHandler(AddressOf mDataGridView_ColumnDisplayIndexChanged)
        ReadFromXml()
        'mDataGridView.ColumnDisplayIndexChanged += New DataGridViewColumnEventHandler(AddressOf mDataGridView_ColumnDisplayIndexChanged)
        AddHandler mDataGridView.ColumnDisplayIndexChanged, New DataGridViewColumnEventHandler(AddressOf mDataGridView_ColumnDisplayIndexChanged)
        mDataGridView.ShowCellToolTips = True


        'RemoveHandler mDataGridView.ColumnWidthChanged, New DataGridViewColumnEventHandler(AddressOf mDataGridView_ColumnWidthChanged)

        AddHandler mDataGridView.ColumnWidthChanged, New DataGridViewColumnEventHandler(AddressOf mDataGridView_ColumnWidthChanged)


        For i As Integer = 0 To mDataGridView.Columns.Count - 1
            mDataGridView.Columns(i).ToolTipText = "拖动列可保存列排序信息。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "在表格的左上角空白区点击右键可以隐藏列信息"
        Next
    End Sub
    ' When user checks / unchecks a checkbox, the related column visibility is 
    ' switched.
    Private Sub mCheckedListBox_ItemCheck(ByVal sender As Object, ByVal e As ItemCheckEventArgs) Handles mCheckedListBox.ItemCheck
        mDataGridView.Columns(e.Index).Visible = (e.NewValue = CheckState.Checked)
        WriteToXml()
    End Sub
    ''' <summary>
    ''' 将列改变的信息写入到xml文件，打开表格的时候方便读取
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub mDataGridView_ColumnDisplayIndexChanged(ByVal sender As Object, ByVal e As DataGridViewColumnEventArgs) Handles mDataGridView.ColumnDisplayIndexChanged
        WriteToXml()
    End Sub
    Private Sub WriteToXml()
        If Not String.IsNullOrEmpty(xmlFile) Then
            Dim dt As New DataTable("DataGrid")
            dt.Columns.Add("Index", GetType(Int32))
            dt.Columns.Add("DisplayIndex", GetType(Int32))
            dt.Columns.Add("Visable", GetType(Boolean))
            dt.Columns.Add("Width", GetType(Int32))




            For Each col As DataGridViewColumn In mDataGridView.Columns
                Dim dr As DataRow = dt.NewRow()
                dr(0) = col.Index
                dr(1) = col.DisplayIndex
                dr(2) = col.Visible
                dr(3) = col.Width
                dt.Rows.Add(dr)
            Next
            dt.WriteXml(xmlFile)
        End If
    End Sub

    Private Sub ReadFromXml()

        If Not String.IsNullOrEmpty(xmlFile) AndAlso File.Exists(xmlFile) Then
            Dim ds As New DataSet()
            ds.ReadXml(xmlFile)
            For Each dr As DataRow In ds.Tables(0).Rows
                Dim index As Integer = Integer.Parse(dr(0).ToString())
                Dim displayIndex As Integer = Integer.Parse(dr(1).ToString())
                Dim visable As Boolean = Boolean.Parse(dr(2).ToString())
                Dim Width As Integer = Integer.Parse(dr(3).ToString())
                mDataGridView.Columns(index).Visible = visable
                mDataGridView.Columns(index).DisplayIndex = displayIndex
                mDataGridView.Columns(index).Width = Width

            Next
        End If
    End Sub

    Private Sub mDataGridView_ColumnWidthChanged()
        WriteToXml()
    End Sub

End Class

