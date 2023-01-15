﻿Imports System.Data.SqlServerCe
Imports SalesInvoice.Utils
Public Class AddClientWindow
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If NAMEDATA.Text.Length > 0 And ADDRESSDATA.Text.Length > 0 Then

            If DatabaseHelper.con.State = ConnectionState.Closed Then DatabaseHelper.con.Open()

            DatabaseHelper.cmd = New SqlCeCommand("SELECT * FROM clients WHERE identificator = '" & NIPDATA.Text.Replace("-", "") & "' or name = '" & NAMEDATA.Text & "'", DatabaseHelper.con)

            DatabaseHelper.cmd.ExecuteNonQuery()
            Dim d As SqlCeDataReader = DatabaseHelper.cmd.ExecuteReader
            If Not d.Read() Then
                Dim persondata As String
                If PESELRADIO.Checked Then persondata = PESELDATA.Text
                If NIPRADIO.Checked Then persondata = NIPDATA.Text
                If NOTHINGRADIO.Checked Then persondata = 0
                DatabaseHelper.cmd = New SqlCeCommand("INSERT clients(name,phone,identificator,address) values('" & NAMEDATA.Text & "','" & IIf(PHONEDATA.Text.Length < 1, "0", PHONEDATA.Text) & "','" & persondata & "','" & ADDRESSDATA.Text & "');", DatabaseHelper.con)
                If DatabaseHelper.con.State = ConnectionState.Closed Then DatabaseHelper.con.Open()
                DatabaseHelper.cmd.ExecuteNonQuery()
                DatabaseHelper.con.Close()
                Me.Close()
            Else
                MsgBox("Client already in database!")
            End If
        Else
            MsgBox(Globals.resManager.GetString("msgNnotEnoughData"))
        End If

    End Sub
    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode.ToString = "Escape" Then
            Me.Close()
        End If
    End Sub

    Private Sub Form2_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If DatabaseHelper.con.State = ConnectionState.Open Then DatabaseHelper.con.Close()
        Me.Dispose()
        Me.ResetText()
        Me.KeyPreview = False
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        Me.Focus()
        Me.BringToFront()
        setLanguage()
    End Sub
    Sub setLanguage()
        lbAddress.Text = Globals.resManager.GetString("lbAddress")
        lbDownloadGUS.Text = Globals.resManager.GetString("lbDataFromCEIDG")
        lbName.Text = Globals.resManager.GetString("lbItemName")
        lbPhone.Text = Globals.resManager.GetString("lbPhone")
        btnAdd.Text = Globals.resManager.GetString("lbAdd")
        Me.Text = Globals.resManager.GetString("lbAddClient")
    End Sub


    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles NIPRADIO.CheckedChanged
        NIPDATA.Enabled = True
        PESELDATA.Text = ""
        PESELDATA.Enabled = False
        lbDownloadGUS.Enabled = True
        NIPDATA.Focus()
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles PESELRADIO.CheckedChanged
        PESELDATA.Enabled = True
        NIPDATA.Text = ""
        NIPDATA.Enabled = False
        lbDownloadGUS.Enabled = False
        PESELDATA.Focus()
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles NOTHINGRADIO.CheckedChanged
        NIPDATA.Enabled = False
        PESELDATA.Enabled = False
        NIPDATA.Text = ""
        PESELDATA.Text = ""
        lbDownloadGUS.Enabled = False
    End Sub

    Private Sub DOWNLOADGUS_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbDownloadGUS.LinkClicked
        If NIPDATA.Text.Replace("-", "").Replace(" ", "") IsNot "" Then
            DownloadDataFromCEIDG.NIPToSearch = NIPDATA.Text.Replace("-", "").Replace(" ", "")
            '    MsgBox(DownloadDataFromGUS.NIPToSearch)
            DownloadDataFromCEIDG.ShowDialog()
        End If
    End Sub

    Private Sub NIPDATA_KeyDown(sender As Object, e As KeyEventArgs) Handles NIPDATA.KeyDown
        If e.KeyCode.ToString.ToLower = "return" Then
            DOWNLOADGUS_LinkClicked(lbDownloadGUS, New Windows.Forms.LinkLabelLinkClickedEventArgs(lbDownloadGUS.Links(0), Windows.Forms.MouseButtons.Left))
        End If
    End Sub
End Class