Imports SalesInvoice.Utils

Imports System.Data.SqlServerCe
Imports System.IO
Imports System.IO.Compression
Imports System.Configuration
Imports System.Collections.Specialized

Imports System.Globalization
Imports System.Threading


Public Class OptionsAndDatabaseWindow

    Private Sub CreateDB(sender As Object, e As EventArgs) Handles btnCreateDb.Click
        Try
            If TextBox1.Text IsNot "" Then
                If Not Globals.fileExists(Application.StartupPath & "\databases\" & TextBox1.Text) Then
                    Dim connectionString = "Data Source=""" & Application.StartupPath & "\databases\" & TextBox1.Text & ".sdf"""

                    Dim en = New SqlCeEngine(connectionString)

                    en.CreateDatabase()
                    ' Dim conTest = New SqlCeConnection("Data Source=""" & Application.StartupPath & "\databases\" & TextBox1.Text & ".sdf""")
                    Dim conTest = New SqlCeConnection(connectionString)

                    conTest.Open()
                    If conTest.State = ConnectionState.Open Then
                        Dim cmdTest = New SqlCeCommand(Globals.DBobjects.getString("tableCategories"), conTest)
                        cmdTest.ExecuteNonQuery()
                        cmdTest = New SqlCeCommand(Globals.DBobjects.getString("tableClients"), conTest)
                        cmdTest.ExecuteNonQuery()
                        cmdTest = New SqlCeCommand(Globals.DBobjects.getString("tableItems"), conTest)
                        cmdTest.ExecuteNonQuery()
                        cmdTest = New SqlCeCommand(Globals.DBobjects.getString("tableReceipts"), conTest)
                        cmdTest.ExecuteNonQuery()
                        cmdTest = New SqlCeCommand(Globals.DBobjects.getString("tableReceipts_data"), conTest)
                        cmdTest.ExecuteNonQuery()
                        cmdTest = New SqlCeCommand(Globals.DBobjects.getString("table_units"), conTest)
                        cmdTest.ExecuteNonQuery()

                        Dim query As String() = Globals.DBobjects.getString("table_alters").Split(New Char() {";"c})

                        For Each w As String In query
                            If w.Length > 0 Then
                                cmdTest = New SqlCeCommand(w, conTest)
                                cmdTest.ExecuteNonQuery()
                            End If
                        Next
                        query = Globals.DBobjects.getString("tableUnits_data").Split(New Char() {";"c})

                        For Each w As String In query
                            If w.Length > 0 Then
                                cmdTest = New SqlCeCommand(w, conTest)
                                cmdTest.ExecuteNonQuery()
                            End If
                        Next

                        MessageBox.Show(Globals.resManager.GetString("msgDatabaseCreatedSuccess"),
                                       "Info", MessageBoxButtons.OK,
                                        MessageBoxIcon.Information)
                        updateData()
                        conTest.Close()
                    End If
                Else
                    MsgBox(Globals.resManager.GetString("msgDatabaseNameExists"))
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

    End Sub
    Sub updateData()
        Dim files() As String
        cbDbNameSelect.Items.Clear()
        cbSelectDbName2.Items.Clear()
        cbExportDB.Items.Clear()
        files = Directory.GetFiles(Application.StartupPath & "\databases\", "*.sdf", SearchOption.TopDirectoryOnly)

        For Each FileName As String In files

            cbDbNameSelect.Items.Add(Path.GetFileName(FileName))
            cbSelectDbName2.Items.Add(Path.GetFileName(FileName))
            cbExportDB.Items.Add(Path.GetFileName(FileName))
        Next
        cbDbNameSelect.SelectedIndex = 0
        cbSelectDbName2.SelectedIndex = 0
        cbExportDB.SelectedIndex = 0


        cbTemplate.Items.Clear()
        cbTemplate.Items.Add(Globals.resManager.GetString("lbChoose"))

        files = Directory.GetFiles(Application.StartupPath & "\Resources", "*.doc", SearchOption.TopDirectoryOnly)
        For Each FileName As String In files
            cbTemplate.Items.Add(Path.GetFileName(FileName))
        Next
        cbTemplate.SelectedIndex = 1
        If cbTemplate.Items.Count > 1 Then
            If Globals.appSettings.Settings("defaultPrintTemplate").Value <> "none" Then
                If cbTemplate.FindString(Globals.appSettings.Settings("defaultPrintTemplate").Value) > -1 Then
                    cbTemplate.SelectedIndex = cbTemplate.FindStringExact(Globals.appSettings.Settings("defaultPrintTemplate").Value)
                End If
            End If
        End If
    End Sub

    Private Sub FormOnKeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode.ToString = "Escape" Then
            Me.Close()
        End If
    End Sub
    Private Sub FormLoads(sender As Object, e As EventArgs) Handles MyBase.Load
        setLanguage()
        updateData()
        TabCleaning.Enabled = False
        TabCleaning.Visible = False
        TabControl1.TabPages.Remove(TabCleaning)
        cbSelectLanguage.SelectedIndex = 0
        If Globals.appSettings.Settings.Item("lang") Is "pl-PL" Then
            cbSelectLanguage.SelectedIndex = 0
        ElseIf Globals.appSettings.Settings.Item("lang") Is "en-US" Then
            cbSelectLanguage.SelectedIndex = 1
        End If

    End Sub

    Private Sub Confirm(sender As Object, e As EventArgs) Handles btnOk.Click
        DatabaseHelper.currentDatabase = cbDbNameSelect.SelectedItem
        DatabaseHelper.updateConnectionString(DatabaseHelper.currentDatabase)
        MainWindow.setLanguage()
        Me.Close()
    End Sub

    Private Sub CloseAndSave(sender As Object, e As EventArgs) Handles btnClose.Click
        If chkSetDefault.Checked Then
            Globals.appSettings.Settings.Item("autostart_database").Value = cbDbNameSelect.SelectedItem
        End If
        If cbTemplate.SelectedItem <> Globals.resManager.GetString("lbChoose") Then
            Globals.appSettings.Settings("defaultPrintTemplate").Value = cbTemplate.SelectedItem
        Else
            Globals.appSettings.Settings("defaultPrintTemplate").Value = "none"
        End If
        Globals.cAppConfig.Save(ConfigurationSaveMode.Modified)
        Me.Close()
    End Sub

    Private Sub DeleteDB(sender As Object, e As EventArgs) Handles btnDeleteDB.Click
        Dim answer As DialogResult
        answer = MessageBox.Show(Globals.resManager.GetString("msgSureToDeleteX") & cbSelectDbName2.SelectedItem.ToString, Globals.resManager.GetString("msgConfirmation"), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If answer = vbYes Then
            If DatabaseHelper.currentDatabase = cbSelectDbName2.SelectedItem.ToString Then
                MsgBox(Globals.resManager.GetString("msgDatabaseInUseError"))
            Else
                DatabaseHelper.con.Close()
                MainWindow.MainGridView.DataSource = Nothing
                MainWindow.MainGridView.Rows.Clear()
                My.Computer.FileSystem.DeleteFile(Application.StartupPath & "\databases\" & cbSelectDbName2.SelectedItem.ToString)
            End If
        End If
        updateData()
    End Sub

    Private Sub ImportDB(sender As Object, e As EventArgs) Handles btnImport.Click
        OpenFileDialog1.ShowDialog()
        Dim file = Nothing
        file = OpenFileDialog1.FileName
        If Not file.ToString.Equals("Nothing") Then
            If file.EndsWith(".zip") Then
                Try
                    ZipFile.ExtractToDirectory(file, Application.StartupPath & "\databases\")
                Catch ex As Exception
                    MsgBox(Globals.resManager.GetString("msgGeneralError") & vbNewLine &
                           Globals.resManager.GetString("msgZipError"))
                    MsgBox(ex.ToString)
                End Try
            End If

            If file.EndsWith(".sdf") Then
                Try
                    FileSystem.FileCopy(file, Application.StartupPath & "\databases\" & Globals.getName(file))
                Catch ex As Exception
                    MsgBox(Globals.resManager.GetString("msgGeneralError") & vbNewLine &
                           Globals.resManager.GetString("msgSdfError"))
                End Try
            End If

        End If
        updateData()


    End Sub

    Private Sub ClearClients(sender As Object, e As EventArgs) Handles btnClearClients.Click
        Dim result As Integer = MessageBox.Show(Globals.resManager.GetString("lbAreYouSure") & vbNewLine & Globals.resManager.GetString("lbCannotUndone"), Globals.resManager.GetString("lbCleaning"), MessageBoxButtons.YesNoCancel)
        If result = DialogResult.Yes Then
            DatabaseHelper.con = New SqlCeConnection("Data Source=""" & Application.StartupPath & "\databases\" & DatabaseHelper.currentDatabase & """")
            DatabaseHelper.cmd = New SqlCeCommand("DELETE FROM clients", DatabaseHelper.con)
            If DatabaseHelper.con.State = ConnectionState.Closed Then DatabaseHelper.con.Open()
            DatabaseHelper.cmd.ExecuteNonQuery()
        End If
    End Sub

    Private Sub ClearItems(sender As Object, e As EventArgs) Handles btnClearItems.Click
        Dim result As Integer = MessageBox.Show(Globals.resManager.GetString("lbAreYouSure") & vbNewLine & Globals.resManager.GetString("lbCannotUndone"), Globals.resManager.GetString("lbCleaning"), MessageBoxButtons.YesNoCancel)
        If result = DialogResult.Yes Then
            DatabaseHelper.con = New SqlCeConnection("Data Source=""" & Application.StartupPath & "\databases\" & DatabaseHelper.currentDatabase & """")
            DatabaseHelper.cmd = New SqlCeCommand("DELETE FROM items", DatabaseHelper.con)
            If DatabaseHelper.con.State = ConnectionState.Closed Then DatabaseHelper.con.Open()
            DatabaseHelper.cmd.ExecuteNonQuery()
        End If
    End Sub

    Private Sub ClearReceipts(sender As Object, e As EventArgs) Handles btnClearReceipts.Click
        Dim result As Integer = MessageBox.Show(Globals.resManager.GetString("lbAreYouSure") & vbNewLine & Globals.resManager.GetString("lbCannotUndone"), Globals.resManager.GetString("lbCleaning"), MessageBoxButtons.YesNoCancel)
        If result = DialogResult.Yes Then
            DatabaseHelper.con = New SqlCeConnection("Data Source=""" & Application.StartupPath & "\databases\" & DatabaseHelper.currentDatabase & """")
            DatabaseHelper.cmd = New SqlCeCommand("DELETE FROM receipts", DatabaseHelper.con)
            DatabaseHelper.cmd2 = New SqlCeCommand("DELETE FROM receipts_data", DatabaseHelper.con)
            If DatabaseHelper.con.State = ConnectionState.Closed Then DatabaseHelper.con.Open()
            DatabaseHelper.cmd.ExecuteNonQuery()
            DatabaseHelper.cmd2.ExecuteNonQuery()
        End If
    End Sub

    Private Sub SetDefaultDB(sender As Object, e As EventArgs) Handles chkSetDefault.CheckedChanged
        If chkSetDefault.Checked Then
            Globals.appSettings.Settings.Item("autostart_database").Value = cbDbNameSelect.SelectedItem
        Else
            Globals.appSettings.Settings.Item("autostart_database").Value = "false"
        End If
        Globals.cAppConfig.Save(ConfigurationSaveMode.Modified)
    End Sub
    Private Sub ExportDB(sender As Object, e As EventArgs) Handles btnExportDb.Click
        FolderBrowserDialog1.ShowDialog()
        doThings()
    End Sub
    Sub doThings()
        Try
            If Directory.Exists("temp") Then System.IO.Directory.Delete("temp", True)
            If FolderBrowserDialog1.SelectedPath IsNot Nothing Then
                Dim destination = FolderBrowserDialog1.SelectedPath

                Dim info As DirectoryInfo = New DirectoryInfo(destination)

                If (info.Exists AndAlso ((info.Attributes And FileAttributes.[ReadOnly]) = FileAttributes.[ReadOnly])) Then
                    info.Attributes = (info.Attributes Xor FileAttributes.[ReadOnly])
                End If

                FileSystem.MkDir("temp")
                FileSystem.FileCopy(Application.StartupPath & "\databases\" & cbDbNameSelect.SelectedItem.ToString, Application.StartupPath & "\temp\" & cbDbNameSelect.SelectedItem.ToString)
                ZipFile.CreateFromDirectory("temp", destination & "\" & cbDbNameSelect.SelectedItem.ToString.Substring(0, cbDbNameSelect.SelectedItem.ToString.Length - 4) & ".zip")

                If Directory.Exists("temp") Then System.IO.Directory.Delete("temp", True)
                Me.Close()
            End If
        Catch e As Exception
            MsgBox(e.Message)
        End Try
    End Sub
    Private Sub SelectLanguageChanged(sender As Object, e As EventArgs) Handles cbSelectLanguage.SelectedIndexChanged
        '' MsgBox(cbSelectLanguage.SelectedIndex)
        If cbSelectLanguage.SelectedIndex = 0 Then
            Globals.appSettings.Settings.Item("lang").Value = "pl-PL"

        ElseIf cbSelectLanguage.SelectedIndex = 1 Then
            Globals.appSettings.Settings.Item("lang").Value = "en-US"
        End If
        Globals.cAppConfig.Save(ConfigurationSaveMode.Modified)
        setLanguage()
        MainWindow.setLanguage()
    End Sub
    Sub setLanguage()
        Globals.reloadLanguage()
        btnClose.Text = Globals.resManager.GetString("lbClose")
        btnClearClients.Text = Globals.resManager.GetString("lbClients")
        btnClearItems.Text = Globals.resManager.GetString("lbItems")
        btnClearReceipts.Text = Globals.resManager.GetString("lbReceipts")
        btnCreateDb.Text = Globals.resManager.GetString("lbCreateDB")

        btnCreateTask.Text = Globals.resManager.GetString("lbCreateTask")
        btnCreateTask.AutoSize = True

        btnExportDb.Text = Globals.resManager.GetString("lbExportDB")
        btnImport.Text = Globals.resManager.GetString("lbImportDB")

        btnOk.Text = Globals.resManager.GetString("lbOk")
        btnDeleteDB.Text = Globals.resManager.GetString("lbDeleteDB")

        lbDatabase.Text = Globals.resManager.GetString("lbDatabase")
        lbDatabase.AutoSize = True
        lbDatabase.Location = New Point((cbDbNameSelect.Left - 8) - lbDatabase.Width, lbDatabase.Location.Y)

        lbDatabase1.Text = Globals.resManager.GetString("lbDatabase")
        lbDatabase1.AutoSize = True
        lbDatabase1.Location = New Point((cbExportDB.Left - 8) - lbDatabase1.Width, lbDatabase1.Location.Y)

        lbDatabase2.Text = Globals.resManager.GetString("lbDatabase")
        lbDatabase2.AutoSize = True
        lbDatabase2.Location = New Point((cbSelectDbName2.Left - 8) - lbDatabase2.Width, lbDatabase2.Location.Y)

        lbDbName.Text = Globals.resManager.GetString("lbItemName")
        lbDbName.AutoSize = True
        lbDbName.Location = New Point((TextBox1.Left - 8) - lbDbName.Width, lbDbName.Location.Y)


        lbLanguage.Text = Globals.resManager.GetString("lbLang")
        lbLanguage.AutoSize = True
        lbLanguage.Location = New Point((cbSelectLanguage.Left - 8) - lbLanguage.Width, lbLanguage.Location.Y)

        chkSetDefault.Text = Globals.resManager.GetString("lbSetDefaultDB")
        chkSetDefault.AutoSize = True


        TabSelectDb.Text = Globals.resManager.GetString("lbSelectDB")
        TabCleaning.Text = Globals.resManager.GetString("lbCleaning")
        TabDeleteDb.Text = Globals.resManager.GetString("lbDeleteDB")
        TabExportDb.Text = Globals.resManager.GetString("lbExportDB")
        TabExtras.Text = Globals.resManager.GetString("lbExtras")
        TabImportDb.Text = Globals.resManager.GetString("lbImportDB")
        TabCreateDb.Text = Globals.resManager.GetString("lbCreateDB")

        lbTemplate.Text = Globals.resManager.GetString("lbTemplate")

        Me.Text = Globals.resManager.GetString("lbSettings")


        Me.Refresh()
    End Sub
End Class