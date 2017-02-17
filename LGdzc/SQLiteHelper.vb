
Imports System.Data
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.IO
Imports System.Collections
Imports System.Data.SQLite

Namespace DBUtility.SQLite
    ''' <summary>  
    ''' SQLiteHelper is a utility class similar to "SQLHelper" in MS  
    ''' Data Access Application Block and follows similar pattern.  
    ''' </summary>  
    Public Class SQLiteHelper
        ''' <summary>  
        ''' Creates a new <see cref="SQLiteHelper"/> instance. The ctor is marked private since all members are static.  
        ''' </summary>  
        Private Sub New()
        End Sub
        ''' <summary>  
        ''' Creates the command.  
        ''' </summary>  
        ''' <param name="connection">Connection.</param>  
        ''' <param name="commandText">Command text.</param>  
        ''' <param name="commandParameters">Command parameters.</param>  
        ''' <returns>SQLite Command</returns>  
        Public Shared Function CreateCommand(connection As SQLiteConnection, commandText As String, ParamArray commandParameters As SQLiteParameter()) As SQLiteCommand
            Dim cmd As New SQLiteCommand(commandText, connection)
            If commandParameters.Length > 0 Then
                For Each parm As SQLiteParameter In commandParameters
                    cmd.Parameters.Add(parm)
                Next
            End If
            Return cmd
        End Function

        ''' <summary>  
        ''' Creates the command.  
        ''' </summary>  
        ''' <param name="connectionString">Connection string.</param>  
        ''' <param name="commandText">Command text.</param>  
        ''' <param name="commandParameters">Command parameters.</param>  
        ''' <returns>SQLite Command</returns>  
        Public Shared Function CreateCommand(connectionString As String, commandText As String, ParamArray commandParameters As SQLiteParameter()) As SQLiteCommand
            Dim cn As New SQLiteConnection(connectionString)

            Dim cmd As New SQLiteCommand(commandText, cn)

            If commandParameters.Length > 0 Then
                For Each parm As SQLiteParameter In commandParameters
                    cmd.Parameters.Add(parm)
                Next
            End If
            Return cmd
        End Function
        ''' <summary>  
        ''' Creates the parameter.  
        ''' </summary>  
        ''' <param name="parameterName">Name of the parameter.</param>  
        ''' <param name="parameterType">Parameter type.</param>  
        ''' <param name="parameterValue">Parameter value.</param>  
        ''' <returns>SQLiteParameter</returns>  
        Public Shared Function CreateParameter(parameterName As String, parameterType As System.Data.DbType, parameterValue As Object) As SQLiteParameter
            Dim parameter As New SQLiteParameter()
            parameter.DbType = parameterType
            parameter.ParameterName = parameterName
            parameter.Value = parameterValue
            Return parameter
        End Function

        ''' <summary>  
        ''' Shortcut method to execute dataset from SQL Statement and object[] arrray of parameter values  
        ''' </summary>  
        ''' <param name="connectionString">SQLite Connection string</param>  
        ''' <param name="commandText">SQL Statement with embedded "@param" style parameter names</param>  
        ''' <param name="paramList">object[] array of parameter values</param>  
        ''' <returns></returns>  
        Public Shared Function ExecuteDataSet(connectionString As String, commandText As String, paramList As Object()) As DataSet
            Dim cn As New SQLiteConnection(connectionString)
            Dim cmd As SQLiteCommand = cn.CreateCommand()


            cmd.CommandText = commandText
            If paramList IsNot Nothing Then
                AttachParameters(cmd, commandText, paramList)
            End If
            Dim ds As New DataSet()
            If cn.State = ConnectionState.Closed Then
                cn.Open()
            End If
            Dim da As New SQLiteDataAdapter(cmd)
            da.Fill(ds)
            da.Dispose()
            cmd.Dispose()
            cn.Close()
            Return ds
        End Function
        ''' <summary>  
        ''' Shortcut method to execute dataset from SQL Statement and object[] arrray of  parameter values  
        ''' </summary>  
        ''' <param name="cn">Connection.</param>  
        ''' <param name="commandText">Command text.</param>  
        ''' <param name="paramList">Param list.</param>  
        ''' <returns></returns>  
        Public Shared Function ExecuteDataSet(cn As SQLiteConnection, commandText As String, paramList As Object()) As DataSet

            Dim cmd As SQLiteCommand = cn.CreateCommand()


            cmd.CommandText = commandText
            If paramList IsNot Nothing Then
                AttachParameters(cmd, commandText, paramList)
            End If
            Dim ds As New DataSet()
            If cn.State = ConnectionState.Closed Then
                cn.Open()
            End If
            Dim da As New SQLiteDataAdapter(cmd)
            da.Fill(ds)
            da.Dispose()
            cmd.Dispose()
            cn.Close()
            Return ds
        End Function
        ''' <summary>  
        ''' Executes the dataset from a populated Command object.  
        ''' </summary>  
        ''' <param name="cmd">Fully populated SQLiteCommand</param>  
        ''' <returns>DataSet</returns>  
        Public Shared Function ExecuteDataset(cmd As SQLiteCommand) As DataSet
            If cmd.Connection.State = ConnectionState.Closed Then
                cmd.Connection.Open()
            End If
            Dim ds As New DataSet()
            Dim da As New SQLiteDataAdapter(cmd)
            da.Fill(ds)
            da.Dispose()
            cmd.Connection.Close()
            cmd.Dispose()
            Return ds
        End Function

        ''' <summary>  
        ''' Executes the dataset in a SQLite Transaction  
        ''' </summary>  
        ''' <param name="transaction">SQLiteTransaction. Transaction consists of Connection, Transaction,  /// and Command, all of which must be created prior to making this method call. </param>  
        ''' <param name="commandText">Command text.</param>  
        ''' <param name="commandParameters">Sqlite Command parameters.</param>  
        ''' <returns>DataSet</returns>  
        ''' <remarks>user must examine Transaction Object and handle transaction.connection .Close, etc.</remarks>  
        Public Shared Function ExecuteDataset(transaction As SQLiteTransaction, commandText As String, ParamArray commandParameters As SQLiteParameter()) As DataSet

            If transaction Is Nothing Then
                Throw New ArgumentNullException("transaction")
            End If
            If transaction IsNot Nothing AndAlso transaction.Connection Is Nothing Then
                Throw New ArgumentException("The transaction was rolled back or committed, please provide an open transaction.", "transaction")
            End If
            Dim cmd As IDbCommand = transaction.Connection.CreateCommand()
            cmd.CommandText = commandText
            For Each parm As SQLiteParameter In commandParameters
                cmd.Parameters.Add(parm)
            Next
            If transaction.Connection.State = ConnectionState.Closed Then
                transaction.Connection.Open()
            End If
            Dim ds As DataSet = ExecuteDataset(DirectCast(cmd, SQLiteCommand))
            Return ds
        End Function

        ''' <summary>  
        ''' Executes the dataset with Transaction and object array of parameter values.  
        ''' </summary>  
        ''' <param name="transaction">SQLiteTransaction. Transaction consists of Connection, Transaction,    /// and Command, all of which must be created prior to making this method call. </param>  
        ''' <param name="commandText">Command text.</param>  
        ''' <param name="commandParameters">object[] array of parameter values.</param>  
        ''' <returns>DataSet</returns>  
        ''' <remarks>user must examine Transaction Object and handle transaction.connection .Close, etc.</remarks>  
        Public Shared Function ExecuteDataset(transaction As SQLiteTransaction, commandText As String, commandParameters As Object()) As DataSet

            If transaction Is Nothing Then
                Throw New ArgumentNullException("transaction")
            End If
            If transaction IsNot Nothing AndAlso transaction.Connection Is Nothing Then
                Throw New ArgumentException("The transaction was rolled back or committed,                                                          please provide an open transaction.", "transaction")
            End If
            Dim cmd As IDbCommand = transaction.Connection.CreateCommand()
            cmd.CommandText = commandText
            AttachParameters(DirectCast(cmd, SQLiteCommand), cmd.CommandText, commandParameters)
            If transaction.Connection.State = ConnectionState.Closed Then
                transaction.Connection.Open()
            End If

            Dim ds As DataSet = ExecuteDataset(DirectCast(cmd, SQLiteCommand))
            Return ds
        End Function

#Region "UpdateDataset"
        ''' <summary>  
        ''' Executes the respective command for each inserted, updated, or deleted row in the DataSet.  
        ''' </summary>  
        ''' <remarks>  
        ''' e.g.:    
        '''  UpdateDataset(conn, insertCommand, deleteCommand, updateCommand, dataSet, "Order");  
        ''' </remarks>  
        ''' <param name="insertCommand">A valid SQL statement  to insert new records into the data source</param>  
        ''' <param name="deleteCommand">A valid SQL statement to delete records from the data source</param>  
        ''' <param name="updateCommand">A valid SQL statement used to update records in the data source</param>  
        ''' <param name="dataSet">The DataSet used to update the data source</param>  
        ''' <param name="tableName">The DataTable used to update the data source.</param>  
        Public Shared Sub UpdateDataset(insertCommand As SQLiteCommand, deleteCommand As SQLiteCommand, updateCommand As SQLiteCommand, dataSet As DataSet, tableName As String)
            If insertCommand Is Nothing Then
                Throw New ArgumentNullException("insertCommand")
            End If
            If deleteCommand Is Nothing Then
                Throw New ArgumentNullException("deleteCommand")
            End If
            If updateCommand Is Nothing Then
                Throw New ArgumentNullException("updateCommand")
            End If
            If tableName Is Nothing OrElse tableName.Length = 0 Then
                Throw New ArgumentNullException("tableName")
            End If

            ' Create a SQLiteDataAdapter, and dispose of it after we are done  
            Using dataAdapter As New SQLiteDataAdapter()
                ' Set the data adapter commands  
                dataAdapter.UpdateCommand = updateCommand
                dataAdapter.InsertCommand = insertCommand
                dataAdapter.DeleteCommand = deleteCommand

                ' Update the dataset changes in the data source  
                dataAdapter.Update(dataSet, tableName)

                ' Commit all the changes made to the DataSet  
                dataSet.AcceptChanges()
            End Using
        End Sub
#End Region




        ''' <summary>  
        ''' ShortCut method to return IDataReader  
        ''' NOTE: You should explicitly close the Command.connection you passed in as  
        ''' well as call Dispose on the Command  after reader is closed.  
        ''' We do this because IDataReader has no underlying Connection Property.  
        ''' </summary>  
        ''' <param name="cmd">SQLiteCommand Object</param>  
        ''' <param name="commandText">SQL Statement with optional embedded "@param" style parameters</param>  
        ''' <param name="paramList">object[] array of parameter values</param>  
        ''' <returns>IDataReader</returns>  
        Public Shared Function ExecuteReader(cmd As SQLiteCommand, commandText As String, paramList As Object()) As IDataReader
            If cmd.Connection Is Nothing Then
                Throw New ArgumentException("Command must have live connection attached.", "cmd")
            End If
            cmd.CommandText = commandText
            AttachParameters(cmd, commandText, paramList)
            If cmd.Connection.State = ConnectionState.Closed Then
                cmd.Connection.Open()
            End If
            Dim rdr As IDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return rdr
        End Function

        ''' <summary>  
        ''' Shortcut to ExecuteNonQuery with SqlStatement and object[] param values  
        ''' </summary>  
        ''' <param name="connectionString">SQLite Connection String</param>  
        ''' <param name="commandText">Sql Statement with embedded "@param" style parameters</param>  
        ''' <param name="paramList">object[] array of parameter values</param>  
        ''' <returns></returns>  
        Public Shared Function ExecuteNonQuery(connectionString As String, commandText As String, ParamArray paramList As Object()) As Integer
            Dim cn As New SQLiteConnection(connectionString)
            Dim cmd As SQLiteCommand = cn.CreateCommand()
            cmd.CommandText = commandText
            AttachParameters(cmd, commandText, paramList)
            If cn.State = ConnectionState.Closed Then
                cn.Open()
            End If
            Dim result As Integer = cmd.ExecuteNonQuery()
            cmd.Dispose()
            cn.Close()

            Return result
        End Function



        Public Shared Function ExecuteNonQuery(cn As SQLiteConnection, commandText As String, ParamArray paramList As Object()) As Integer

            Dim cmd As SQLiteCommand = cn.CreateCommand()
            cmd.CommandText = commandText
            AttachParameters(cmd, commandText, paramList)
            If cn.State = ConnectionState.Closed Then
                cn.Open()
            End If
            Dim result As Integer = cmd.ExecuteNonQuery()
            cmd.Dispose()
            cn.Close()

            Return result
        End Function

        ''' <summary>  
        ''' Executes  non-query sql Statment with Transaction  
        ''' </summary>  
        ''' <param name="transaction">SQLiteTransaction. Transaction consists of Connection, Transaction,   /// and Command, all of which must be created prior to making this method call. </param>  
        ''' <param name="commandText">Command text.</param>  
        ''' <param name="paramList">Param list.</param>  
        ''' <returns>Integer</returns>  
        ''' <remarks>user must examine Transaction Object and handle transaction.connection .Close, etc.</remarks>  
        Public Shared Function ExecuteNonQuery(transaction As SQLiteTransaction, commandText As String, ParamArray paramList As Object()) As Integer
            If transaction Is Nothing Then
                Throw New ArgumentNullException("transaction")
            End If
            If transaction IsNot Nothing AndAlso transaction.Connection Is Nothing Then
                Throw New ArgumentException("The transaction was rolled back or committed,                                                        please provide an open transaction.", "transaction")
            End If
            Dim cmd As IDbCommand = transaction.Connection.CreateCommand()
            cmd.CommandText = commandText
            AttachParameters(DirectCast(cmd, SQLiteCommand), cmd.CommandText, paramList)
            If transaction.Connection.State = ConnectionState.Closed Then
                transaction.Connection.Open()
            End If
            Dim result As Integer = cmd.ExecuteNonQuery()
            cmd.Dispose()
            Return result
        End Function


        ''' <summary>  
        ''' Executes the non query.  
        ''' </summary>  
        ''' <param name="cmd">CMD.</param>  
        ''' <returns></returns>  
        Public Shared Function ExecuteNonQuery(cmd As IDbCommand) As Integer
            If cmd.Connection.State = ConnectionState.Closed Then
                cmd.Connection.Open()
            End If
            Dim result As Integer = cmd.ExecuteNonQuery()
            cmd.Connection.Close()
            cmd.Dispose()
            Return result
        End Function

        ''' <summary>  
        ''' Shortcut to ExecuteScalar with Sql Statement embedded params and object[] param values  
        ''' </summary>  
        ''' <param name="connectionString">SQLite Connection String</param>  
        ''' <param name="commandText">SQL statment with embedded "@param" style parameters</param>  
        ''' <param name="paramList">object[] array of param values</param>  
        ''' <returns></returns>  
        Public Shared Function ExecuteScalar(connectionString As String, commandText As String, ParamArray paramList As Object()) As Object
            Dim cn As New SQLiteConnection(connectionString)
            Dim cmd As SQLiteCommand = cn.CreateCommand()
            cmd.CommandText = commandText
            AttachParameters(cmd, commandText, paramList)
            If cn.State = ConnectionState.Closed Then
                cn.Open()
            End If
            Dim result As Object = cmd.ExecuteScalar()
            cmd.Dispose()
            cn.Close()

            Return result
        End Function

        ''' <summary>  
        ''' Execute XmlReader with complete Command  
        ''' </summary>  
        ''' <param name="command">SQLite Command</param>  
        ''' <returns>XmlReader</returns>  
        Public Shared Function ExecuteXmlReader(command As IDbCommand) As XmlReader
            ' open the connection if necessary, but make sure we   
            ' know to close it when we�re done.  
            If command.Connection.State <> ConnectionState.Open Then
                command.Connection.Open()
            End If

            ' get a data adapter    
            Dim da As New SQLiteDataAdapter(DirectCast(command, SQLiteCommand))
            Dim ds As New DataSet()
            ' fill the data set, and return the schema information  
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey
            da.Fill(ds)
            ' convert our dataset to XML  
            Dim stream As New StringReader(ds.GetXml())
            command.Connection.Close()
            ' convert our stream of text to an XmlReader  
            Return New XmlTextReader(stream)
        End Function



        ''' <summary>  
        ''' Parses parameter names from SQL Statement, assigns values from object array ,   /// and returns fully populated ParameterCollection.  
        ''' </summary>  
        ''' <param name="commandText">Sql Statement with "@param" style embedded parameters</param>  
        ''' <param name="paramList">object[] array of parameter values</param>  
        ''' <returns>SQLiteParameterCollection</returns>  
        ''' <remarks>Status experimental. Regex appears to be handling most issues. Note that parameter object array must be in same ///order as parameter names appear in SQL statement.</remarks>  
        Private Shared Function AttachParameters(cmd As SQLiteCommand, commandText As String, ParamArray paramList As Object()) As SQLiteParameterCollection
            If paramList Is Nothing OrElse paramList.Length = 0 Then
                Return Nothing
            End If

            Dim coll As SQLiteParameterCollection = cmd.Parameters
            Dim parmString As String = commandText.Substring(commandText.IndexOf("@"))
            ' pre-process the string so always at least 1 space after a comma.  
            parmString = parmString.Replace(",", " ,")
            ' get the named parameters into a match collection  
            Dim pattern As String = "(@)\S*(.*?)\b"
            Dim ex As New Regex(pattern, RegexOptions.IgnoreCase)
            Dim mc As MatchCollection = ex.Matches(parmString)
            Dim paramNames As String() = New String(mc.Count - 1) {}
            Dim i As Integer = 0
            For Each m As Match In mc
                paramNames(i) = m.Value
                i += 1
            Next

            ' now let's type the parameters  
            Dim j As Integer = 0
            Dim t As Type = Nothing
            For Each o As Object In paramList
                t = o.[GetType]()

                Dim parm As New SQLiteParameter()
                Select Case t.ToString()

                    Case ("DBNull"), ("Char"), ("SByte"), ("UInt16"), ("UInt32"), ("UInt64")
                        Throw New SystemException("Invalid data type")


                    Case ("System.String")
                        parm.DbType = DbType.[String]
                        parm.ParameterName = paramNames(j)
                        parm.Value = DirectCast(paramList(j), String)
                        coll.Add(parm)
                        Exit Select

                    Case ("System.Byte[]")
                        parm.DbType = DbType.Binary
                        parm.ParameterName = paramNames(j)
                        parm.Value = DirectCast(paramList(j), Byte())
                        coll.Add(parm)
                        Exit Select

                    Case ("System.Int32")
                        parm.DbType = DbType.Int32
                        parm.ParameterName = paramNames(j)
                        parm.Value = CInt(paramList(j))
                        coll.Add(parm)
                        Exit Select

                    Case ("System.Boolean")
                        parm.DbType = DbType.[Boolean]
                        parm.ParameterName = paramNames(j)
                        parm.Value = CBool(paramList(j))
                        coll.Add(parm)
                        Exit Select

                    Case ("System.DateTime")
                        parm.DbType = DbType.DateTime
                        parm.ParameterName = paramNames(j)
                        parm.Value = Convert.ToDateTime(paramList(j))
                        coll.Add(parm)
                        Exit Select

                    Case ("System.Double")
                        parm.DbType = DbType.[Double]
                        parm.ParameterName = paramNames(j)
                        parm.Value = Convert.ToDouble(paramList(j))
                        coll.Add(parm)
                        Exit Select

                    Case ("System.Decimal")
                        parm.DbType = DbType.[Decimal]
                        parm.ParameterName = paramNames(j)
                        parm.Value = Convert.ToDecimal(paramList(j))
                        Exit Select

                    Case ("System.Guid")
                        parm.DbType = DbType.Guid
                        parm.ParameterName = paramNames(j)
                        parm.Value = DirectCast(paramList(j), System.Guid)
                        Exit Select

                    Case ("System.Object")

                        parm.DbType = DbType.[Object]
                        parm.ParameterName = paramNames(j)
                        parm.Value = paramList(j)
                        coll.Add(parm)
                        Exit Select
                    Case Else

                        Throw New SystemException("Value is of unknown data type")

                End Select
                ' end switch  
                j += 1
            Next
            Return coll
        End Function

        ''' <summary>  
        ''' Executes non query typed params from a DataRow  
        ''' </summary>  
        ''' <param name="command">Command.</param>  
        ''' <param name="dataRow">Data row.</param>  
        ''' <returns>Integer result code</returns>  
        Public Shared Function ExecuteNonQueryTypedParams(command As IDbCommand, dataRow As DataRow) As Integer
            Dim retVal As Integer = 0

            ' If the row has values, the store procedure parameters must be initialized  
            If dataRow IsNot Nothing AndAlso dataRow.ItemArray.Length > 0 Then
                ' Set the parameters values  
                AssignParameterValues(command.Parameters, dataRow)

                retVal = ExecuteNonQuery(command)
            Else
                retVal = ExecuteNonQuery(command)
            End If

            Return retVal
        End Function

        ''' <summary>  
        ''' This method assigns dataRow column values to an IDataParameterCollection  
        ''' </summary>  
        ''' <param name="commandParameters">The IDataParameterCollection to be assigned values</param>  
        ''' <param name="dataRow">The dataRow used to hold the command's parameter values</param>  
        ''' <exception cref="System.InvalidOperationException">Thrown if any of the parameter names are invalid.</exception>  
        Protected Friend Shared Sub AssignParameterValues(commandParameters As IDataParameterCollection, dataRow As DataRow)
            If commandParameters Is Nothing OrElse dataRow Is Nothing Then
                ' Do nothing if we get no data  
                Return
            End If

            Dim columns As DataColumnCollection = dataRow.Table.Columns

            Dim i As Integer = 0
            ' Set the parameters values  
            For Each commandParameter As IDataParameter In commandParameters
                ' Check the parameter name  
                If commandParameter.ParameterName Is Nothing OrElse commandParameter.ParameterName.Length <= 1 Then
                    Throw New InvalidOperationException(String.Format("Please provide a valid parameter name on the parameter #{0},                            the ParameterName property has the following value: '{1}'.", i, commandParameter.ParameterName))
                End If

                If columns.Contains(commandParameter.ParameterName) Then
                    commandParameter.Value = dataRow(commandParameter.ParameterName)
                ElseIf columns.Contains(commandParameter.ParameterName.Substring(1)) Then
                    commandParameter.Value = dataRow(commandParameter.ParameterName.Substring(1))
                End If

                i += 1
            Next
        End Sub

        ''' <summary>  
        ''' This method assigns dataRow column values to an array of IDataParameters  
        ''' </summary>  
        ''' <param name="commandParameters">Array of IDataParameters to be assigned values</param>  
        ''' <param name="dataRow">The dataRow used to hold the stored procedure's parameter values</param>  
        ''' <exception cref="System.InvalidOperationException">Thrown if any of the parameter names are invalid.</exception>  
        Protected Sub AssignParameterValues(commandParameters As IDataParameter(), dataRow As DataRow)
            If (commandParameters Is Nothing) OrElse (dataRow Is Nothing) Then
                ' Do nothing if we get no data  
                Return
            End If

            Dim columns As DataColumnCollection = dataRow.Table.Columns

            Dim i As Integer = 0
            ' Set the parameters values  
            For Each commandParameter As IDataParameter In commandParameters
                ' Check the parameter name  
                If commandParameter.ParameterName Is Nothing OrElse commandParameter.ParameterName.Length <= 1 Then
                    Throw New InvalidOperationException(String.Format("Please provide a valid parameter name on the parameter #{0}, the ParameterName property has the following value: '{1}'.", i, commandParameter.ParameterName))
                End If

                If columns.Contains(commandParameter.ParameterName) Then
                    commandParameter.Value = dataRow(commandParameter.ParameterName)
                ElseIf columns.Contains(commandParameter.ParameterName.Substring(1)) Then
                    commandParameter.Value = dataRow(commandParameter.ParameterName.Substring(1))
                End If

                i += 1
            Next
        End Sub

        ''' <summary>  
        ''' This method assigns an array of values to an array of IDataParameters  
        ''' </summary>  
        ''' <param name="commandParameters">Array of IDataParameters to be assigned values</param>  
        ''' <param name="parameterValues">Array of objects holding the values to be assigned</param>  
        ''' <exception cref="System.ArgumentException">Thrown if an incorrect number of parameters are passed.</exception>  
        Protected Sub AssignParameterValues(commandParameters As IDataParameter(), ParamArray parameterValues As Object())
            If (commandParameters Is Nothing) OrElse (parameterValues Is Nothing) Then
                ' Do nothing if we get no data  
                Return
            End If

            ' We must have the same number of values as we pave parameters to put them in  
            If commandParameters.Length <> parameterValues.Length Then
                Throw New ArgumentException("Parameter count does not match Parameter Value count.")
            End If

            ' Iterate through the IDataParameters, assigning the values from the corresponding position in the   
            ' value array  
            Dim i As Integer = 0, j As Integer = commandParameters.Length, k As Integer = 0
            While i < j
                If commandParameters(i).Direction <> ParameterDirection.ReturnValue Then
                    ' If the current array value derives from IDataParameter, then assign its Value property  
                    If TypeOf parameterValues(k) Is IDataParameter Then
                        Dim paramInstance As IDataParameter
                        paramInstance = DirectCast(parameterValues(k), IDataParameter)
                        If paramInstance.Direction = ParameterDirection.ReturnValue Then
                            paramInstance = DirectCast(parameterValues(System.Threading.Interlocked.Increment(k)), IDataParameter)
                        End If
                        If paramInstance.Value Is Nothing Then
                            commandParameters(i).Value = DBNull.Value
                        Else
                            commandParameters(i).Value = paramInstance.Value
                        End If
                    ElseIf parameterValues(k) Is Nothing Then
                        commandParameters(i).Value = DBNull.Value
                    Else
                        commandParameters(i).Value = parameterValues(k)
                    End If
                    k += 1
                End If
                i += 1
            End While
        End Sub
    End Class
End Namespace

'=======================================================
'Service provided by Telerik (www.telerik.com)
'Conversion powered by NRefactory.
'Twitter: @telerik
'Facebook: facebook.com/telerik
'=======================================================
