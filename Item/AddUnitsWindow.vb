﻿Imports SalesInvoice.Utils
Imports System.Data.SqlServerCe
Public Class AddUnitsWindow
    Private Sub AddUnitsWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        refreshListBox()
        setLanguage()
        TextBox1.Focus()
    End Sub
    Sub setLanguage()
        btnAdd.Text = Globals.resManager.GetString("lbAdd")
        btnDelete.Text = Globals.resManager.GetString("lbDelete")
        btnOk.Text = Globals.resManager.GetString("lbOk")
        lbUnits.Text = Globals.resManager.GetString("lbUnit")
        Me.Text = Globals.resManager.GetString("lbAddUnits")

    End Sub
   
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If ListBox1.SelectedItem IsNot Nothing Then
            'MsgBox(ListBox1.SelectedItem & vbNewLine & ListBox1.SelectedValue)
            DatabaseHelper.cmd = New SqlCeCommand("select * from items WHERE unit = '" & ListBox1.SelectedItem.ToString & "'", DatabaseHelper.con)
            Dim d As SqlCeDataReader = DatabaseHelper.cmd.ExecuteReader
            If Not d.Read() Then
                DatabaseHelper.cmd = New SqlCeCommand("DELETE FROM units WHERE name = '" & ListBox1.SelectedItem.ToString & "'", DatabaseHelper.con)
                If DatabaseHelper.con.State = ConnectionState.Closed Then DatabaseHelper.con.Open()
                DatabaseHelper.cmd.ExecuteNonQuery()
            Else
                MsgBox(Globals.resManager.GetString("msgCategoryDelError"))

            End If
            refreshListBox()
        End If

    End Sub
    Public Sub refreshListBox()
        ListBox1.Items.Clear()
        DatabaseHelper.cmd = New SqlCeCommand("select name as c from units", DatabaseHelper.con)
        If DatabaseHelper.con.State = ConnectionState.Closed Then DatabaseHelper.con.Open()
        DatabaseHelper.cmd.ExecuteNonQuery()
        Using rd As SqlCeDataReader = DatabaseHelper.cmd.ExecuteReader
            While rd.Read()
                ListBox1.Items.Add(rd.GetValue(0))
            End While
        End Using
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If TextBox1.Text IsNot Nothing Then
            DatabaseHelper.cmd = New SqlCeCommand("SELECT name FROM  units WHERE name = '" & TextBox1.Text & "';", DatabaseHelper.con)
            If DatabaseHelper.con.State = ConnectionState.Closed Then DatabaseHelper.con.Open()
            DatabaseHelper.cmd.ExecuteNonQuery()
            Dim d As SqlCeDataReader = DatabaseHelper.cmd.ExecuteReader
            If Not d.Read() Then
                DatabaseHelper.cmd = New SqlCeCommand("INSERT Units(name) values('" & TextBox1.Text & "');", DatabaseHelper.con)
                If DatabaseHelper.con.State = ConnectionState.Closed Then DatabaseHelper.con.Open()
                DatabaseHelper.cmd.ExecuteNonQuery()
                refreshListBox()
                TextBox1.Text = ""
            End If
        End If
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        refreshListBox()
        Me.Close()
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode.ToString.ToLower = "return" Then
            btnAdd.PerformClick()
        End If
    End Sub

    Private Sub AddUnitsWindow_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
        Me.ResetText()
    End Sub
End Class