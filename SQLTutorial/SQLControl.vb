Imports System.Data.SqlClient
Public Class SQLControl
    Private DBCon As New SqlConnection("Server=(localdb)\MSSQLLocalDB;Database=SQLTutorial;Trusted_Connection=True;")

    Private DBCmd As SqlCommand 'Container for our command. will be regenerated every time we execute the query.

    'Db Data - passing the command to the database - 
    Public DBDA As SqlDataAdapter 'also gets regen every time we run a query

    'storage container for the data we pull out of database - usually db table, can be db set
    Public DBDT As DataTable

    'query params - container for params - gets generated once and then rebuilt each time query was ran
    Public Params As New List(Of SqlParameter)

    'query statistics to keep track on (can get from datatable so probably not needed)
    Public RecordCount As Integer

    'store any error messages along the way
    Public Exception As String

    Public Sub New()
    End Sub

    'allow connection override
    Public Sub New(ConnectionString As String)
        DBCon = New SqlConnection(ConnectionString)
    End Sub

    'Execute query sub
    Public Sub ExecuteQuery(Query As String, Optional ReturnIdentity As Boolean = False)
        'reset query stats
        RecordCount = 0
        Exception = ""

        Try
            DBCon.Open()

            'create db command (passing query and db connection, ie what to do and where)
            DBCmd = New SqlCommand(Query, DBCon)

            'load params into db command
            Params.ForEach(Sub(p) DBCmd.Parameters.Add(p))

            'clear param list (after query has been ran)
            Params.Clear()

            'execute the command and return data (fill dataset); creating dbtable
            DBDT = New DataTable
            DBDA = New SqlDataAdapter(DBCmd) 'executing command - consumes command (DBCmd)
            RecordCount = DBDA.Fill(DBDT) 'returns number of records that it queried. delete/update return nil. fills our 'bucket' with results

            If ReturnIdentity = True Then
                Dim ReturnQuery As String = "SELECT @@IDENTITY As LastID;"
                '@@IDENTITY - session
                'SCOPE_IDENTITY() - session and scope
                'IDENT_CURRENT(tablename) - last ident in table , any scope or session

                'as command is consumed above in DBDA execution, we need to create new one.
                DBCmd = New SqlCommand(ReturnQuery, DBCon)

                DBDT = New DataTable
                DBDA = New SqlDataAdapter(DBCmd)
                'filling out the data table with the new output
                RecordCount = DBDA.Fill(DBDT)
            End If
        Catch ex As Exception
            'capture error
            Exception = "ExecQuery Error: " & vbNewLine & ex.Message

        Finally
            'close connection
            If DBCon.State = ConnectionState.Open Then DBCon.Close()

        End Try

    End Sub

    'add params
    Public Sub AddParam(Name As String, Value As Object)
        Dim NewParam As New SqlParameter(Name, Value)
        Params.Add(NewParam)
    End Sub

    'error checking
    Public Function HasException(Optional Report As Boolean = False)
        If String.IsNullOrEmpty(Exception) Then Return False
        If Report = True Then MsgBox(Exception, MsgBoxStyle.Critical, "Exception")
        Return True
    End Function

End Class
