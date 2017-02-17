Imports System.Data

#Const IS_MYSQL_DB = True
'#Const IS_SQLITE_DB = True






Public Class LiuDataAdapter

    Dim SqliteStrConn As String = "Data Source=" + Application.StartupPath + "\\..\\..\\..\\db\\lgdzc.db"

    Dim MysqlStrConn As String = "Database='gdzc';Data Source='10.43.18.42';User Id='mysql';Password='mysqlpwd';charset='utf8';pooling=true"


#If IS_SQLITE_DB Then
    Private Adapter As SQLite.SQLiteDataAdapter=new SQLite.SQLiteDataAdapter()
#Else
    Private Adapter As MySql.Data.MySqlClient.MySqlDataAdapter
#End If

    Public Sub New(sql As String, CONN_STR As String)

#If IS_SQLITE_DB Then
    Adapter = New SQLite.SQLiteDataAdapter(sql, CONN_STR)
#Else
        Adapter = New MySql.Data.MySqlClient.MySqlDataAdapter(sql, CONN_STR)
#End If

    End Sub
    Public Function Update(dataTable As DataTable) As Integer
#If IS_SQLITE_DB Then
    Dim scb As SQLite.SQLiteCommandBuilder = SQLite.SQLiteCommandBuilder(Adapter)
#Else
        Dim scb As MySql.Data.MySqlClient.MySqlCommandBuilder = New MySql.Data.MySqlClient.MySqlCommandBuilder(Adapter)
#End If


        Return Adapter.Update(dataTable)
    End Function

    Public Sub Dispose()
        Adapter.Dispose()
    End Sub

    Public Function FillSchema(dataTable As DataTable, schemaType As System.Data.SchemaType) As System.Data.DataTable
        Return Adapter.FillSchema(dataTable, schemaType)
    End Function
    Public Function Fill(dataTable As DataTable) As Integer
#If IS_SQLITE_DB Then
    Dim scb As SQLite.SQLiteCommandBuilder = SQLite.SQLiteCommandBuilder(Adapter)
#Else
        Dim scb As MySql.Data.MySqlClient.MySqlCommandBuilder = New MySql.Data.MySqlClient.MySqlCommandBuilder(Adapter)
#End If
        Return Adapter.Fill(dataTable)
    End Function
    Public Function GetConn() As String

        Return ""
    End Function
End Class


Public Class LiuConnectiong

End Class

Public Class LiuCommandBuilder

End Class