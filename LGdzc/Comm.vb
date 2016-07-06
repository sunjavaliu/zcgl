Module Comm

    '定义资产结构体
    Public Structure ZcInfo
        Dim id As Integer
        Dim name As String
        Dim sex As String
        Dim age As Integer
        Dim address As String
        Dim lessons As String
    End Structure

    Public Const TREE_NONE = 0
    Public Const TREE_ADD_SUB_NODE = 1          '添加子节点
    Public Const TREE_ADD_SIDEWAYS_NODE = 2     '添加同级节点
    Public Const TREE_UPDATE_NODE = 3           '编辑更新节点

    Public Const CONN_STR = "Data Source=d:\\lgdzc.db"
    Public Sub IsInputNum(e As KeyPressEventArgs)
        If (Not Char.IsNumber(e.KeyChar) And e.KeyChar <> Chr(8)) Then
            e.Handled = True
        Else
            e.Handled = False
        End If

    End Sub




End Module
