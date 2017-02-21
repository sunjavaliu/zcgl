
Module Defined


    '#Const IS_SQLITE_DB =TRUE
    '定义资产结构体
    Public Structure ZcInfo
        Dim id As Integer
        Dim zcbh As String
        Dim zcmc As String
        Dim lbid As String
        Dim lbmc As String
        Dim jldw As String
        Dim gzrq As String
        Dim zcly As String
        Dim zcsl As String
        Dim zcdj As String
        Dim zczj As String
        Dim zczt As String
        Dim bmbh As String
        Dim bmmc As String
        Dim zrr As String
        Dim cfwz As String
        Dim meno As String

        Sub Initialize()
            id = -99999
            zcbh = ""
        End Sub

    End Structure
    'Public zcData As ZcInfo

    Public Const TREE_NONE = 0
    Public Const TREE_ADD_SUB_NODE = 1          '添加子节点
    Public Const TREE_ADD_SIDEWAYS_NODE = 2     '添加同级节点
    Public Const TREE_UPDATE_NODE = 3           '编辑更新节点

    Public Const ZC_STATE = "资产状态"
    Public Const JILIANGDANWEI = "计量单位"
    Public Const ZC_FROM = "资产来源"
    Public Const CAIGOUFANGSHI = "采购方式"

    'Public CONN_STR As String = "Database='testgdzc';Data Source='10.43.18.42';User Id='mysql';Password='mysqlpwd';charset='utf8';pooling=true"

    Public CONN_STR As String = "Data Source=" + Application.StartupPath + "\\..\\..\\..\\db\\lgdzc.db"

End Module
