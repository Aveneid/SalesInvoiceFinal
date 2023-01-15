Imports System.Configuration
Imports System.Collections.Specialized
Imports SalesInvoice.Utils
Public Class Informations
    Sub setLanguage()

        'header
        tbHeader.Text = Globals.resManager.GetString("lbHeaderText")
        lbHeaderText.Text = Globals.resManager.GetString("lbHeaderText")
        lbHeaderText.Location = New Point(Me.Width / 2 - lbHeaderText.Width / 2, lbHeaderText.Location.Y)

        'footer
        tbFooter.Text = Globals.resManager.GetString("lbFooterText")
        lbFooterText.Text = Globals.resManager.GetString("lbFooterText")
        lbFooterText.Location = New Point(Me.Width / 2 - lbFooterText.Width / 2, lbFooterText.Location.Y)


        'seller
        tbSeller.Text = Globals.resManager.GetString("lbIdData")
        lbName.Text = Globals.resManager.GetString("lbItemName")
        lbAddress.Text = Globals.resManager.GetString("lbAddress")
        lbAddressNo.Text = Globals.resManager.GetString("lbAddressNo")
        lbCity.Text = Globals.resManager.GetString("lbCity")
        lbPostal.Text = Globals.resManager.GetString("lbPostal")
        lbPhone.Text = Globals.resManager.GetString("lbPhone")
        lbPesel.Text = Globals.resManager.GetString("lbPESEL") & " (*)"

        'bank
        tbBank.Text = Globals.resManager.GetString("lbBankDetails")
        lbAccountNo.Text = Globals.resManager.GetString("lbAccountNO")
        lbBankAddress.Text = Globals.resManager.GetString("lbBankAddress")


        rtbSellerName.Text = Globals.appSettings.Settings("sellerName").Value
        Address_1.Text = Globals.appSettings.Settings("address").Value
        Address_2.Text = Globals.appSettings.Settings("buildingNo").Value
        Address_3.Text = Globals.appSettings.Settings("city").Value
        Address_4.Text = Globals.appSettings.Settings("postalCode").Value
        Phone.Text = Globals.appSettings.Settings("phone").Value
        Pesel.Text = Globals.appSettings.Settings("pesel").Value

        AccountNo.Text = Globals.appSettings.Settings("accountNo").Value
        BankAddress.Text = Globals.appSettings.Settings("bankAddress").Value

        rtbFooterText.Text = Globals.appSettings.Settings("footerText").Value
        Headline_info.Text = Globals.appSettings.Settings("headlineInfo").Value


        Me.Text = Globals.resManager.GetString("lbInfo")
        btnSave.Text = Globals.resManager.GetString("lbSave")
        Me.Refresh()

    End Sub


    Private Sub SaveSettings(sender As Object, e As EventArgs) Handles btnSave.Click

        'Header info

        Globals.appSettings.Settings.Item("headlineInfo").Value = Headline_info.Text

        'footerInfo
        Globals.appSettings.Settings.Item("footerText").Value = rtbFooterText.Text

        'Seller info
        Globals.appSettings.Settings.Item("sellerName").Value = rtbSellerName.Text
        Globals.appSettings.Settings.Item("address").Value = Address_1.Text
        Globals.appSettings.Settings.Item("buildingNo").Value = Address_2.Text
        Globals.appSettings.Settings.Item("city").Value = Address_3.Text
        Globals.appSettings.Settings.Item("postalCode").Value = Address_4.Text
        Globals.appSettings.Settings.Item("phone").Value = Phone.Text
        Globals.appSettings.Settings.Item("pesel").Value = Pesel.Text

        'Bank info

        If AccountNo.Text IsNot Nothing And BankAddress.Text IsNot Nothing Then
            Globals.appSettings.Settings.Item("accountNo").Value = AccountNo.Text
            Globals.appSettings.Settings.Item("bankAddress").Value = BankAddress.Text
        End If
        If rtbFooterText.Text <> "" Then
            Globals.appSettings.Settings("footerText").Value = rtbFooterText.Text
        Else
            Globals.appSettings.Settings("footerText").Value = ""
        End If
        If Headline_info.Text <> "" Then
            Globals.appSettings.Settings("headlineInfo").Value = Headline_info.Text
        Else
            Globals.appSettings.Settings("headlineInfo").Value = ""
        End If
        Globals.cAppConfig.Save(ConfigurationSaveMode.Modified)
        MsgBox("Information updated")
        Me.Close()
    End Sub

    Private Sub FormLoads(sender As Object, e As EventArgs) Handles MyBase.Load
        setLanguage()


        ToolTip1.ToolTipTitle = Globals.resManager.GetString("lbWarning")
        ToolTip1.SetToolTip(lbPesel, Globals.resManager.GetString("msgNotRequired"))
        ToolTip1.SetToolTip(Pesel, Globals.resManager.GetString("msgNotRequired"))

        Headline_info.Text = Globals.appSettings.Settings.Item("headlineInfo").Value

        rtbFooterText.Text = Globals.appSettings.Settings.Item("footerText").Value

        rtbSellerName.Text = Globals.appSettings.Settings.Item("sellerName").Value
        Address_1.Text = Globals.appSettings.Settings.Item("address").Value
        Address_2.Text = Globals.appSettings.Settings.Item("buildingNo").Value
        Address_3.Text = Globals.appSettings.Settings.Item("city").Value
        Address_4.Text = Globals.appSettings.Settings.Item("postalCode").Value
        Phone.Text = Globals.appSettings.Settings.Item("phone").Value
        Pesel.Text = Globals.appSettings.Settings.Item("pesel").Value

        AccountNo.Text = Globals.appSettings.Settings.Item("accountNo").Value
        BankAddress.Text = Globals.appSettings.Settings.Item("bankAddress").Value
    End Sub
End Class