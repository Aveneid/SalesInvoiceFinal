Imports SalesInvoice.globalVars
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
            cmd = New SqlCeCommand("UPDATE clients set phone = '" & ClientPhone.Text & "', address = '" & _
                                   ClientAddress.Text & "', identificator = '" & ClientId.Text & "' where id = " & cID, con)
            If con.State = ConnectionState.Closed Then con.Open()
            If cmd.ExecuteNonQuery() Then
                MsgBox(rm.GetString("msgEditOk"))
            Else
                MsgBox(rm.GetString("msgGeneralError"))
            End If
        End If
        Me.Close()
    End Sub
    Sub updateLang()
        ClientNameLabel.Text = rm.GetString("lbItemName")
        ClientPhoneLabel.Text = rm.GetString("lbPhone")
        ClientIdLabel.Text = rm.GetString("lbID")
        ClientAddressLabel.Text = rm.GetString("lbAddress")
        Me.Text = rm.GetString("lbClientDetails")
        Button1.Text = rm.GetString("lbClose")
    End Sub

    Private Sub ClientDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        updateLang()
        If con.State = ConnectionState.Closed Then con.Open()
        cmd = New SqlCeCommand("SELECT  name,phone,address,identificator from Clients where id = "& cID, con)
        cmd.ExecuteNonQuery()
        Using rd As SqlCeDataReader = cmd.ExecuteReader()
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