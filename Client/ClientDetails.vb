Imports SalesInvoice.Utils
Imports System.Data.SqlServerCe
Public Class ClientDetails
    Dim cID As String
    Dim clientData(3) As String

    Public Sub New(ByVal id As String)
        InitializeComponent()
        cID = id
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ClientPhone.Text <> clientData(0) Or ClientId.Text <> clientData(1) Or ClientAddress.Text <> clientData(2) Then
            DatabaseHelper.cmd = New SqlCeCommand("UPDATE clients set phone = '" & ClientPhone.Text & "', address = '" &
                                   ClientAddress.Text & "', identificator = '" & ClientId.Text & "' where id = " & cID, DatabaseHelper.con)
            If DatabaseHelper.con.State = ConnectionState.Closed Then DatabaseHelper.con.Open()
            If DatabaseHelper.cmd.ExecuteNonQuery() Then
                MsgBox(Globals.resManager.GetString("msgEditOk"))
            Else
                MsgBox(Globals.resManager.GetString("msgGeneralError"))
            End If
        End If
        Me.Close()
    End Sub
    Sub updateLang()
        ClientNameLabel.Text = Globals.resManager.GetString("lbItemName")
        ClientPhoneLabel.Text = Globals.resManager.GetString("lbPhone")
        ClientIdLabel.Text = Globals.resManager.GetString("lbID")
        ClientAddressLabel.Text = Globals.resManager.GetString("lbAddress")
        Me.Text = Globals.resManager.GetString("lbClientDetails")
        Button1.Text = Globals.resManager.GetString("lbClose")
    End Sub

    Private Sub ClientDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        updateLang()
        If DatabaseHelper.con.State = ConnectionState.Closed Then DatabaseHelper.con.Open()
        DatabaseHelper.cmd = New SqlCeCommand("SELECT  name,phone,address,identificator from Clients where id = " & cID, DatabaseHelper.con)
        DatabaseHelper.cmd.ExecuteNonQuery()
        Using rd As SqlCeDataReader = DatabaseHelper.cmd.ExecuteReader()
            While rd.Read()
                ClientName.Text = rd.GetValue(0)
                ClientPhone.Text = rd.GetValue(1)
                ClientId.Text = rd.GetValue(3)
                ClientAddress.Text = rd.GetValue(2)
                clientData(0) = rd.GetValue(1)
                clientData(1) = rd.GetValue(3)
                clientData(2) = rd.GetValue(2)
            End While
        End Using
    End Sub
End Class