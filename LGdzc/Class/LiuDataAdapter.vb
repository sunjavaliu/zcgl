﻿Imports System.Data

#Const IS_SQLITE_DB = False

Public Class LiuDataAdapter
    'Dim SqliteStrConn As String = "Data Source=" + Application.StartupPath + "\\..\\..\\..\\db\\lgdzc.db"

    'Dim MysqlStrConn As String = "Database='testgdzc';Data Source='10.43.18.42';User Id='mysql';Password='mysqlpwd';charset='utf8';pooling=true"
    Dim howUpdate As Integer

#If IS_SQLITE_DB Then
    'Private Adapter As SQLite.SQLiteDataAdapter=new SQLite.SQLiteDataAdapter()
#Else
    Private Adapter As MySql.Data.MySqlClient.MySqlDataAdapter
#End If

    Public Sub New(sql As String, CONN_STR As String)

#If IS_SQLITE_DB Then
    'Adapter = New SQLite.SQLiteDataAdapter(sql, CONN_STR)
#Else
        Adapter = New MySql.Data.MySqlClient.MySqlDataAdapter(sql, CONN_STR)
#End If

        'Adapter.InsertCommand

    End Sub

    Public Sub QueryDataGridView(sql As String, CONN_STR As String)

#If IS_SQLITE_DB Then
    'Adapter = New SQLite.SQLiteDataAdapter(sql, CONN_STR)
#Else
        Adapter = New MySql.Data.MySqlClient.MySqlDataAdapter(sql, CONN_STR)
        'Adapter.
#End If

        'Adapter.InsertCommand

    End Sub

    Public Sub New()

#If IS_SQLITE_DB Then
    'Adapter = New SQLite.SQLiteDataAdapter()
#Else
        Adapter = New MySql.Data.MySqlClient.MySqlDataAdapter()
#End If

        'Adapter.InsertCommand

    End Sub
    Public Function Update(dataTable As DataTable) As Integer
#If IS_SQLITE_DB Then
        'Dim scb As SQLite.SQLiteCommandBuilder = New SQLite.SQLiteCommandBuilder(Adapter)
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
        'Dim scb As SQLite.SQLiteCommandBuilder = New SQLite.SQLiteCommandBuilder(Adapter)
#Else
        Dim scb As MySql.Data.MySqlClient.MySqlCommandBuilder = New MySql.Data.MySqlClient.MySqlCommandBuilder(Adapter)
#End If

        Return Adapter.Fill(dataTable)
    End Function

    Public Function ExecuteNonQuery(SQL As String) As Integer
#If IS_SQLITE_DB Then
        'Dim conn As SQLite.SQLiteConnection = New SQLite.SQLiteConnection()
        'Dim howUpdate As Integer
        'conn.ConnectionString = CONN_STR
        'conn.Open()

        'Dim cmd As SQLite.SQLiteCommand = New SQLite.SQLiteCommand(SQL, conn)
#Else
        Dim conn As MySql.Data.MySqlClient.MySqlConnection = New MySql.Data.MySqlClient.MySqlConnection()

        conn.ConnectionString = CONN_STR
        conn.Open()

        Dim cmd As MySql.Data.MySqlClient.MySqlCommand = New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)
#End If

        howUpdate = cmd.ExecuteNonQuery()
        'cmd.ExecuteScalar()
        Try
            conn.Close()
        Catch
            conn.Close()
            Throw
        End Try
        Return howUpdate
    End Function



    Public Function GetSelectCount(SQL As String) As Integer

#If IS_SQLITE_DB Then
        'Adapter = New SQLite.SQLiteDataAdapter(SQL, CONN_STR)
#Else
        Adapter = New MySql.Data.MySqlClient.MySqlDataAdapter(SQL, CONN_STR)
#End If

        'Adapter.SelectCommand = SQL
        Dim myDataSet As DataSet = New DataSet()
        Adapter.Fill(myDataSet)
        howUpdate = myDataSet.Tables(0).Rows.Count
        'cmd.ExecuteScalar()
        Try
            Adapter.Dispose()
        Catch
            Adapter.Dispose()
            Throw
        End Try
        Return howUpdate
    End Function
End Class

