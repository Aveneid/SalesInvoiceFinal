Imports System.Configuration
Imports System.Collections.Specialized
Imports SalesInvoice.globalVars
Imports System.ComponentModel

Public Class FirstRunWizard
    Dim appClosing = False
    Private Sub FirstRunWizard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        reloadLanguage()
        setLanguage()
        cbLang.SelectedIndex = 0

        tbWizardCards.Appearance = TabAppearance.FlatButtons
        tbWizardCards.ItemSize = New Size(0, 1)
        tbWizardCards.SizeMode = TabSizeMode.Fixed
        MenuLabel0.Font = New Font(MenuLabel0.Font, FontStyle.Bold)
        Me.Text = rm.GetString("lbFisrtRun") & " - " & tbWizardCards.SelectedTab.Text
    End Sub
    Sub setLanguage()
        btnBack.Text = rm.GetString("lbBack")
        btnCancel.Text = rm.GetString("lbCancel")
        btnNext.Text = rm.GetString("lbNext")

        MenuLabel0.Text = rm.GetString("lbWelcome")
        MenuLabel1.Text = rm.GetString("lbIdData")
        MenuLabel2.Text = rm.GetString("lbHeadInfo")
        MenuLabel3.Text = rm.GetString("lbBankInfo")
        MenuLabel4.Text = rm.GetString("lbWizardEnd")

        TabPage1.Text = rm.GetString("lbWelcome")
        TabPage2.Text = rm.GetString("lbIdData")
        TabPage3.Text = rm.GetString("lbHeadInfo")
        TabPage4.Text = rm.GetString("lbBankInfo")
        TabPage5.Text = rm.GetString("lbWizardEnd")

        lbHi.Text = rm.GetString("lbHi")
        lbWelcomeInfo.Text = rm.GetString("lbWelcomeInfo")
        lbIdInfo.Text = rm.GetString("lbIdInfo")

        lbName.Text = rm.GetString("lbItemName")


        lbAddress.Text = rm.GetString("lbAddress")
        lbAddressNo.Text = rm.GetString("lbAddressNo")

        lbCity.Text = rm.GetString("lbCity")

        lbPostal.Text = rm.GetString("lbPostal")
        lbPhone.Text = rm.GetString("lbPhone")

        lbPesel.Text = rm.GetString("lbPESEL") & " (*)"


        lbHeadInfo.Text = rm.GetString("lbHeadInfoWizard")

        lbHeaderText.Text = rm.GetString("lbHeaderText")

        lbFooterText.Text = rm.GetString("lbFooterText")


        lbBankInfo.Text = rm.GetString("lbBankInfoWizard")
        lbAccountNo.Text = rm.GetString("lbAccountNO")
        lbBankAddress.Text = rm.GetString("lbBankAddress")

        lbEndingInfo.Text = rm.GetString("lbWizardEnding")

        lbLang.Text = rm.GetString("lbLang")


        Me.Text = rm.GetString("lbFirstRun")
        Me.Text = Me.Text & " "
        Me.Refresh()
        Me.Update()
    End Sub

    Private Sub btnNext_click(sender As Object, e As EventArgs) Handles btnNext.Click
        If btnNext.Text = rm.GetString("lbNext") Then
            Select Case tbWizardCards.SelectedIndex
                Case 1
                    If SellerName.Text.Length = 0 Or Address_1.Text.Length = 0 Or
                        Address_2.Text.Length = 0 Or Address_3.Text.Length = 0 Or
                        Address_4.Text.Length = 0 Or Phone.Text.Length = 0 Then
                        MsgBox(rm.GetString("lbEnterAllData"))
                        Return
                    End If
                Case 2
                    If Headline_info.Text.Length = 0 Then
                        MsgBox(rm.GetString("lbEnterAllData"))
                        Return
                    End If
            End Select

            tbWizardCards.SelectedIndex = tbWizardCards.SelectedIndex + 1



        ElseIf btnNext.Text = rm.GetString("lbEnd") Then

            asSettings.Settings.Item("sellerName").Value = SellerName.Text
            asSettings.Settings.Item("address").Value = Address_1.Text
            asSettings.Settings.Item("buildingNo").Value = Address_2.Text
            asSettings.Settings.Item("city").Value = Address_3.Text
            asSettings.Settings.Item("postalCode").Value = Address_4.Text
            asSettings.Settings.Item("phone").Value = Phone.Text
            asSettings.Settings.Item("pesel").Value = Pesel.Text


            asSettings.Settings.Item("headlineInfo").Value = Headline_info.Text
            asSettings.Settings.Item("footerText").Value = rtbFooterText.Text

            'asSettings.Settings.Item("headlineInfoAligment").Value = ali

            If AccountNo.Text IsNot Nothing And BankAddress.Text IsNot Nothing Then
                asSettings.Settings.Item("accountNo").Value = AccountNo.Text
                asSettings.Settings.Item("bankAddress").Value = BankAddress.Text
            End If
            asSettings.Settings.Item("firsttimerun").Value = "false"
            cAppConfig.Save(ConfigurationSaveMode.Modified)
            appClosing = True
            Me.Close()
        End If
        Me.Refresh()
        Me.Update()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        tbWizardCards.SelectedIndex = tbWizardCards.SelectedIndex - 1
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tbWizardCards.SelectedIndexChanged
        If tbWizardCards.SelectedIndex = 0 Then
            btnBack.Enabled = False
        Else
            btnBack.Enabled = True
        End If
        If tbWizardCards.SelectedIndex = tbWizardCards.TabCount - 1 Then
            btnNext.Text = rm.GetString("lbEnd")
        Else
            btnNext.Text = rm.GetString("lbNext")
        End If
        MenuLabel0.Font = New Font(MenuLabel0.Font, FontStyle.Regular)
        MenuLabel1.Font = New Font(MenuLabel1.Font, FontStyle.Regular)
        MenuLabel2.Font = New Font(MenuLabel2.Font, FontStyle.Regular)
        MenuLabel3.Font = New Font(MenuLabel3.Font, FontStyle.Regular)
        MenuLabel4.Font = New Font(MenuLabel4.Font, FontStyle.Regular)
        Select Case tbWizardCards.SelectedIndex()
            Case 0
                MenuLabel0.Font = New Font(MenuLabel0.Font, FontStyle.Bold)
            Case 1
                MenuLabel1.Font = New Font(MenuLabel1.Font, FontStyle.Bold)
            Case 2
                MenuLabel2.Font = New Font(MenuLabel2.Font, FontStyle.Bold)
            Case 3
                MenuLabel3.Font = New Font(MenuLabel3.Font, FontStyle.Bold)
            Case 4
                MenuLabel4.Font = New Font(MenuLabel4.Font, FontStyle.Bold)
        End Select
        Me.Text = rm.GetString("lbFisrtRun") & " - " & tbWizardCards.SelectedTab.Text
    End Sub

    Private Sub btnCancel_click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If MsgBox(rm.GetString("lbWizardExit"), MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            appClosing = True
            ChooseDBWindow.Close()
            Me.Close()
            Me.Dispose()
            Application.Exit()
            End
        Else
            Application.DoEvents()
        End If
    End Sub
    Private Sub cbLang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbLang.SelectedIndexChanged
        If cbLang.SelectedIndex = 0 Then
            asSettings.Settings.Item("lang").Value = "en-US"
        ElseIf cbLang.SelectedIndex = 1 Then
            asSettings.Settings.Item("lang").Value = "pl-PL"
        End If

        reloadLanguage()
        setLanguage()
    End Sub

    Private Sub FirstRunWizard_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not e.CloseReason = CloseReason.ApplicationExitCall And Not appClosing Then
            If MsgBox(rm.GetString("lbWizardExit"), MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Application.Exit()
            Else
                e.Cancel = True
            End If
        End If
    End Sub


    Private Sub Phone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Phone.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then e.KeyChar = ""
    End Sub

    Private Sub FirstRunWizard_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F8 Then
            If asSettings.Settings.Item("lang").Value = "en-US" Then
                asSettings.Settings.Item("lang").Value = "pl-PL"
            Else
                asSettings.Settings.Item("lang").Value = "en-US"
            End If
            reloadLanguage()
            setLanguage()
        End If
    End Sub
End Class