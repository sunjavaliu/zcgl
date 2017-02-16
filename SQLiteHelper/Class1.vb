
'''源码下载地址:http://download.csdn.net/detail/kehaigang29/8836171
'''dll下载地址:http://download.csdn.net/detail/kehaigang29/8837257   
''' <summary>
''' 本类为SQLite数据库帮助静态类,使用时只需直接调用即可,无需实例化
''' </summary>
Public NotInheritable Class SQLiteHelper
    Private Sub New()
    End Sub
    ''' <summary>
    ''' 数据库连接字符串
    ''' </summary>
    Public Shared connectionString As String = "Data Source=" + Application.StartupPath + "\" + System.Configuration.ConfigurationSettings.AppSettings("Contr")


#Region "执行数据库操作(新增、更新或删除)，返回影响行数"
    ''' <summary>
    ''' 执行数据库操作(新增、更新或删除)
    ''' </summary>
    ''' <param name="cmd">SqlCommand对象</param>
    ''' <returns>所受影响的行数</returns>
    Public Shared Function ExecuteNonQuery(cmd As SQLiteCommand) As Integer
        Dim result As Integer = 0
        If connectionString Is Nothing OrElse connectionString.Length = 0 Then
            Throw New ArgumentNullException("connectionString")
        End If
        Using con As New SQLiteConnection(connectionString)
            Dim trans As SQLiteTransaction = Nothing
            PrepareCommand(cmd, con, trans, True, cmd.CommandType, cmd.CommandText)
            Try
                result = cmd.ExecuteNonQuery()
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw ex
            End Try
        End Using
        Return result
    End Function

    ''' <summary>
    ''' 执行数据库操作(新增、更新或删除)
    ''' </summary>
    ''' <param name="commandText">执行语句或存储过程名</param>
    ''' <param name="commandType">执行类型(默认语句)</param>
    ''' <returns>所受影响的行数</returns>
    Public Shared Function ExecuteNonQuery(commandText As String, Optional commandType__1 As CommandType = CommandType.Text) As Integer
        Dim result As Integer = 0
        If connectionString Is Nothing OrElse connectionString.Length = 0 Then
            Throw New ArgumentNullException("connectionString")
        End If
        If commandText Is Nothing OrElse commandText.Length = 0 Then
            Throw New ArgumentNullException("commandText")
        End If
        Dim cmd As New SQLiteCommand()
        Using con As New SQLiteConnection(connectionString)
            Dim trans As SQLiteTransaction = Nothing
            PrepareCommand(cmd, con, trans, True, commandType__1, commandText)
            Try
                result = cmd.ExecuteNonQuery()
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw ex
            End Try
        End Using
        Return result
    End Function

    ''' <summary>
    ''' 执行数据库操作(新增、更新或删除)
    ''' </summary>
    ''' <param name="commandText">执行语句或存储过程名</param>
    ''' <param name="commandType">执行类型(默认语句)</param>
    ''' <param name="cmdParms">SQL参数对象</param>
    ''' <returns>所受影响的行数</returns>
	Public Shared Function ExecuteNonQuery(commandText As String, Optional commandType__1 As CommandType = CommandType.Text, ParamArray cmdParms As SQLiteParameter()) As Integer
        Dim result As Integer = 0
        If connectionString Is Nothing OrElse connectionString.Length = 0 Then
            Throw New ArgumentNullException("connectionString")
        End If
        If commandText Is Nothing OrElse commandText.Length = 0 Then
            Throw New ArgumentNullException("commandText")
        End If

        Dim cmd As New SQLiteCommand()
        Using con As New SQLiteConnection(connectionString)
            Dim trans As SQLiteTransaction = Nothing
            PrepareCommand(cmd, con, trans, True, commandType__1, commandText, _
                cmdParms)
            Try
                result = cmd.ExecuteNonQuery()
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw ex
            End Try
        End Using
        Return result
    End Function
#End Region

#Region "执行数据库操作(新增、更新或删除)同时返回执行后查询所得的第1行第1列数据"
    ''' <summary>
    ''' 执行数据库操作(新增、更新或删除)同时返回执行后查询所得的第1行第1列数据
    ''' </summary>
    ''' <param name="cmd">SqlCommand对象</param>
    ''' <returns>查询所得的第1行第1列数据</returns>
    Public Shared Function ExecuteScalar(cmd As SQLiteCommand) As Object
        Dim result As Object = 0
        If connectionString Is Nothing OrElse connectionString.Length = 0 Then
            Throw New ArgumentNullException("connectionString")
        End If
        Using con As New SQLiteConnection(connectionString)
            Dim trans As SQLiteTransaction = Nothing
            PrepareCommand(cmd, con, trans, True, cmd.CommandType, cmd.CommandText)
            Try
                result = cmd.ExecuteScalar()
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw ex
            End Try
        End Using
        Return result
    End Function

    ''' <summary>
    ''' 执行数据库操作(新增、更新或删除)同时返回执行后查询所得的第1行第1列数据
    ''' </summary>
    ''' <param name="commandText">执行语句或存储过程名</param>
    ''' <param name="commandType">执行类型（默认语句）</param>
    ''' <returns>查询所得的第1行第1列数据</returns>
    Public Shared Function ExecuteScalar(commandText As String, Optional commandType__1 As CommandType = CommandType.Text) As Object
        Dim result As Object = 0
        If connectionString Is Nothing OrElse connectionString.Length = 0 Then
            Throw New ArgumentNullException("connectionString")
        End If
        If commandText Is Nothing OrElse commandText.Length = 0 Then
            Throw New ArgumentNullException("commandText")
        End If
        Dim cmd As New SQLiteCommand()
        Using con As New SQLiteConnection(connectionString)
            Dim trans As SQLiteTransaction = Nothing
            PrepareCommand(cmd, con, trans, True, commandType__1, commandText)
            Try
                result = cmd.ExecuteScalar()
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw ex
            End Try
        End Using
        Return result
    End Function

    ''' <summary>
    ''' 执行数据库操作(新增、更新或删除)同时返回执行后查询所得的第1行第1列数据
    ''' </summary>
    ''' <param name="commandText">执行语句或存储过程名</param>
    ''' <param name="commandType">执行类型(默认语句)</param>
    ''' <param name="cmdParms">SQL参数对象</param>
    ''' <returns>查询所得的第1行第1列数据</returns>
	Public Shared Function ExecuteScalar(commandText As String, Optional commandType__1 As CommandType = CommandType.Text, ParamArray cmdParms As SQLiteParameter()) As Object
        Dim result As Object = 0
        If connectionString Is Nothing OrElse connectionString.Length = 0 Then
            Throw New ArgumentNullException("connectionString")
        End If
        If commandText Is Nothing OrElse commandText.Length = 0 Then
            Throw New ArgumentNullException("commandText")
        End If

        Dim cmd As New SQLiteCommand()
        Using con As New SQLiteConnection(connectionString)
            Dim trans As SQLiteTransaction = Nothing
            PrepareCommand(cmd, con, trans, True, commandType__1, commandText, _
                cmdParms)
            Try
                result = cmd.ExecuteScalar()
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw ex
            End Try
        End Using
        Return result
    End Function
#End Region

#Region "执行数据库查询，返回SqlDataReader对象"
    ''' <summary>
    ''' 执行数据库查询，返回SqlDataReader对象
    ''' </summary>
    ''' <param name="cmd">SqlCommand对象</param>
    ''' <returns>SqlDataReader对象</returns>
    Public Shared Function ExecuteReader(cmd As SQLiteCommand) As DbDataReader
        Dim reader As DbDataReader = Nothing
        If connectionString Is Nothing OrElse connectionString.Length = 0 Then
            Throw New ArgumentNullException("connectionString")
        End If

        Dim con As New SQLiteConnection(connectionString)
        Dim trans As SQLiteTransaction = Nothing
        PrepareCommand(cmd, con, trans, False, cmd.CommandType, cmd.CommandText)
        Try
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Catch ex As Exception
            Throw ex
        End Try
        Return reader
    End Function

    ''' <summary>
    ''' 执行数据库查询，返回SqlDataReader对象
    ''' </summary>
    ''' <param name="commandText">执行语句或存储过程名</param>
    ''' <param name="commandType">执行类型(默认语句)</param>
    ''' <returns>SqlDataReader对象</returns>
    Public Shared Function ExecuteReader(commandText As String, Optional commandType__1 As CommandType = CommandType.Text) As DbDataReader
        Dim reader As DbDataReader = Nothing
        If connectionString Is Nothing OrElse connectionString.Length = 0 Then
            Throw New ArgumentNullException("connectionString")
        End If
        If commandText Is Nothing OrElse commandText.Length = 0 Then
            Throw New ArgumentNullException("commandText")
        End If

        Dim con As New SQLiteConnection(connectionString)
        Dim cmd As New SQLiteCommand()
        Dim trans As SQLiteTransaction = Nothing
        PrepareCommand(cmd, con, trans, False, commandType__1, commandText)
        Try
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Catch ex As Exception
            Throw ex
        End Try
        Return reader
    End Function

    ''' <summary>
    ''' 执行数据库查询，返回SqlDataReader对象
    ''' </summary>
    ''' <param name="commandText">执行语句或存储过程名</param>
    ''' <param name="commandType">执行类型（默认语句）</param>
    ''' <param name="cmdParms">SQL参数对象</param>
    ''' <returns>SqlDataReader对象</returns>
	Public Shared Function ExecuteReader(commandText As String, Optional commandType__1 As CommandType = CommandType.Text, ParamArray cmdParms As SQLiteParameter()) As DbDataReader
        Dim reader As DbDataReader = Nothing
        If connectionString Is Nothing OrElse connectionString.Length = 0 Then
            Throw New ArgumentNullException("connectionString")
        End If
        If commandText Is Nothing OrElse commandText.Length = 0 Then
            Throw New ArgumentNullException("commandText")
        End If

        Dim con As New SQLiteConnection(connectionString)
        Dim cmd As New SQLiteCommand()
        Dim trans As SQLiteTransaction = Nothing
        PrepareCommand(cmd, con, trans, False, commandType__1, commandText, _
            cmdParms)
        Try
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Catch ex As Exception
            Throw ex
        End Try
        Return reader
    End Function
#End Region

#Region "执行数据库查询，返回DataSet对象"
    ''' <summary>
    ''' 执行数据库查询，返回DataSet对象
    ''' </summary>
    ''' <param name="cmd">SqlCommand对象</param>
    ''' <returns>DataSet对象</returns>
    Public Shared Function ExecuteDataSet(cmd As SQLiteCommand) As DataSet
        Dim ds As New DataSet()
        Dim con As New SQLiteConnection(connectionString)
        Dim trans As SQLiteTransaction = Nothing
        PrepareCommand(cmd, con, trans, False, cmd.CommandType, cmd.CommandText)
        Try
            Dim sda As New SQLiteDataAdapter(cmd)
            sda.Fill(ds)
        Catch ex As Exception
            Throw ex
        Finally
            If cmd.Connection IsNot Nothing Then
                If cmd.Connection.State = ConnectionState.Open Then
                    cmd.Connection.Close()
                End If
            End If
        End Try
        Return ds
    End Function

    ''' <summary>
    ''' 执行数据库查询，返回DataSet对象
    ''' </summary>
    ''' <param name="commandText">执行语句或存储过程名</param>
    ''' <param name="commandType">执行类型(默认语句)</param>
    ''' <returns>DataSet对象</returns>
    Public Shared Function ExecuteDataSet(commandText As String, Optional commandType__1 As CommandType = CommandType.Text) As DataSet
        If connectionString Is Nothing OrElse connectionString.Length = 0 Then
            Throw New ArgumentNullException("connectionString")
        End If
        If commandText Is Nothing OrElse commandText.Length = 0 Then
            Throw New ArgumentNullException("commandText")
        End If
        Dim ds As New DataSet()
        Dim con As New SQLiteConnection(connectionString)
        Dim cmd As New SQLiteCommand()
        Dim trans As SQLiteTransaction = Nothing
        PrepareCommand(cmd, con, trans, False, commandType__1, commandText)
        Try
            Dim sda As New SQLiteDataAdapter(cmd)
            sda.Fill(ds)
        Catch ex As Exception
            Throw ex
        Finally
            If con IsNot Nothing Then
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
            End If
        End Try
        Return ds
    End Function

    ''' <summary>
    ''' 执行数据库查询，返回DataSet对象
    ''' </summary>
    ''' <param name="commandText">执行语句或存储过程名</param>
    ''' <param name="commandType">执行类型(默认语句)</param>
    ''' <param name="cmdParms">SQL参数对象</param>
    ''' <returns>DataSet对象</returns>
	Public Shared Function ExecuteDataSet(commandText As String, Optional commandType__1 As CommandType = CommandType.Text, ParamArray cmdParms As SQLiteParameter()) As DataSet
        If connectionString Is Nothing OrElse connectionString.Length = 0 Then
            Throw New ArgumentNullException("connectionString")
        End If
        If commandText Is Nothing OrElse commandText.Length = 0 Then
            Throw New ArgumentNullException("commandText")
        End If
        Dim ds As New DataSet()
        Dim con As New SQLiteConnection(connectionString)
        Dim cmd As New SQLiteCommand()
        Dim trans As SQLiteTransaction = Nothing
        PrepareCommand(cmd, con, trans, False, commandType__1, commandText, _
            cmdParms)
        Try
            Dim sda As New SQLiteDataAdapter(cmd)
            sda.Fill(ds)
        Catch ex As Exception
            Throw ex
        Finally
            If con IsNot Nothing Then
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
            End If
        End Try
        Return ds
    End Function
#End Region

#Region "执行数据库查询，返回DataTable对象"
    ''' <summary>
    ''' 执行数据库查询，返回DataTable对象
    ''' </summary>
    ''' <param name="commandText">执行语句或存储过程名</param>
    ''' <param name="commandType">执行类型(默认语句)</param>
    ''' <returns>DataTable对象</returns>
    Public Shared Function ExecuteDataTable(commandText As String, Optional commandType__1 As CommandType = CommandType.Text) As DataTable
        If connectionString Is Nothing OrElse connectionString.Length = 0 Then
            Throw New ArgumentNullException("connectionString")
        End If
        If commandText Is Nothing OrElse commandText.Length = 0 Then
            Throw New ArgumentNullException("commandText")
        End If
        Dim dt As New DataTable()
        Dim con As New SQLiteConnection(connectionString)
        Dim cmd As New SQLiteCommand()
        Dim trans As SQLiteTransaction = Nothing
        PrepareCommand(cmd, con, trans, False, commandType__1, commandText)
        Try
            Dim sda As New SQLiteDataAdapter(cmd)
            sda.Fill(dt)
        Catch ex As Exception
            Throw ex
        Finally
            If con IsNot Nothing Then
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
            End If
        End Try
        Return dt
    End Function

#End Region

#Region "通用分页查询方法"
    ''' <summary>
    ''' 通用分页查询方法
    ''' </summary>
    ''' <param name="tableName">表名</param>
    ''' <param name="strColumns">查询字段名</param>
    ''' <param name="strWhere">where条件</param>
    ''' <param name="strOrder">排序条件</param>
    ''' <param name="pageSize">每页数据数量</param>
    ''' <param name="currentIndex">当前页数</param>
    ''' <param name="recordOut">数据总量</param>
    ''' <returns>DataTable数据表</returns>
    Public Shared Function SelectPaging(tableName As String, strColumns As String, strWhere As String, strOrder As String, pageSize As Integer, currentIndex As Integer, _
        ByRef recordOut As Integer) As DataTable
        Dim dt As New DataTable()
        recordOut = Convert.ToInt32(ExecuteScalar(Convert.ToString("select count(*) from ") & tableName, CommandType.Text))
        Dim pagingTemplate As String = "select {0} from {1} where {2} order by {3} limit {4} offset {5} "
        Dim offsetCount As Integer = (currentIndex - 1) * pageSize
        Dim commandText As String = [String].Format(pagingTemplate, strColumns, tableName, strWhere, strOrder, pageSize.ToString(), _
            offsetCount.ToString())
        Using reader As DbDataReader = ExecuteReader(commandText, CommandType.Text)
            If reader IsNot Nothing Then
                dt.Load(reader)
            End If
        End Using
        Return dt
    End Function

#End Region

#Region "预处理Command对象,数据库链接,事务,需要执行的对象,参数等的初始化"
    ''' <summary>
    ''' 预处理Command对象,数据库链接,事务,需要执行的对象,参数等的初始化
    ''' </summary>
    ''' <param name="cmd">Command对象</param>
    ''' <param name="conn">Connection对象</param>
    ''' <param name="trans">Transcation对象</param>
    ''' <param name="useTrans">是否使用事务</param>
    ''' <param name="cmdType">SQL字符串执行类型</param>
    ''' <param name="cmdText">SQL Text</param>
    ''' <param name="cmdParms">SQLiteParameters to use in the command</param>
    Private Shared Sub PrepareCommand(cmd As SQLiteCommand, conn As SQLiteConnection, ByRef trans As SQLiteTransaction, useTrans As Boolean, cmdType As CommandType, cmdText As String, _
        ParamArray cmdParms As SQLiteParameter())

        If conn.State <> ConnectionState.Open Then
            conn.Open()
        End If

        cmd.Connection = conn
        cmd.CommandText = cmdText

        If useTrans Then
            trans = conn.BeginTransaction(IsolationLevel.ReadCommitted)
            cmd.Transaction = trans
        End If


        cmd.CommandType = cmdType

        If cmdParms IsNot Nothing Then
            For Each parm As SQLiteParameter In cmdParms
                cmd.Parameters.Add(parm)
            Next
        End If
    End Sub

#End Region

End Class

'=======================================================
'Service provided by Telerik (www.telerik.com)
'Conversion powered by NRefactory.
'Twitter: @telerik
'Facebook: facebook.com/telerik
'=======================================================
