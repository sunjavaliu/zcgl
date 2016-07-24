
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.OleDb


Public Class ExcelReader
    Private filePath As String
    Private fileName As String
    Private conn As OleDbConnection
    Private readDataTable As DataTable
    Private connString As String
    Private Enum FileTypeS
        noset
        xls
        xlsx
        csv
    End Enum

    Private fileType As FileTypeS = FileTypeS.noset


    Public Sub New()
    End Sub

    Private Sub SetFileInfo(path As String)
        filePath = path

        fileName = Me.filePath.Remove(0, Me.filePath.LastIndexOf("\") + 1)
        Select Case fileName.Split("."c)(1)
            Case "xls"
                connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & filePath & ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'"
                fileType = fileType.xls
                Exit Select
            Case "xlsx"
                connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & filePath & ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'"
                fileType = fileType.xlsx
                Exit Select
            Case "csv"
                connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & filePath.Remove(filePath.LastIndexOf("\") + 1) & ";Extended Properties='Text;FMT=Delimited;HDR=YES;'"
                fileType = fileType.csv
                Exit Select
        End Select
    End Sub


    Public Function ReadFile(path As String) As DataTable
        If System.IO.File.Exists(path) Then
            SetFileInfo(path)
            Dim myCommand As OleDbDataAdapter = Nothing
            Dim ds As DataSet = Nothing

            Using conn = New OleDbConnection(connString)
                conn.Open()

                Dim schemaTable As DataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)

                Dim tableName As String = If(fileType = fileType.csv, fileName, schemaTable.Rows(0)(2).ToString().Trim())

                Dim strExcel As String = String.Empty

                strExcel = "Select   *   From   [" & tableName & "]"
                myCommand = New OleDbDataAdapter(strExcel, conn)

                ds = New DataSet()

                myCommand.Fill(ds, tableName)


                readDataTable = ds.Tables(0)
            End Using
        End If
        Return readDataTable
    End Function



End Class

