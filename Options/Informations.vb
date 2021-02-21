Imports System.Configuration
Imports System.Collections.Specialized
Imports SalesInvoice.globalVars
Public Class Informations
    Sub setLanguage()

        'header
        tbHeader.Text = rm.GetString("lbHeaderText")
        lbHeaderText.Text = rm.GetString("lbHeaderText")
        lbHeaderText.Location = New Point(Me.Width / 2 - lbHeaderText.Width / 2, lbHeaderText.Location.Y)

        'footer
        tbFooter.Text = rm.GetString("lbFooterText")
        lbFooterText.Text = rm.GetString("lbFooterText")
        lbFooterText.Location = New Point(Me.Width / 2 - lbFooterText.Width / 2, lbFooterText.Location.Y)


        'seller
        tbSeller.Text = rm.GetString("lbIdData")
        lbName.Text = rm.GetString("lbItemName")
        lbAddress.Text = rm.GetString("lbAddress")
        lbAddressNo.Text = rm.GetString("lbAddressNo")
        lbCity.Text = rm.GetString("lbCity")
        lbPostal.Text = rm.GetString("lbPostal")
        lbPhone.Text = rm.GetString("lbPhone")
        lbPesel.Text = rm.GetString("lbPESEL") & " (*)"

        'bank
        tbBank.Text = rm.GetString("lbBankDetails")
        lbAccountNo.Text = rm.GetString("lbAccountNO")
        lbBankAddress.Text = rm.GetString("lbBankAddress")


        rtbSellerName.Text = asSettings.Settings("sellerName").Value
        Address_1.Text = asSettings.Settings("address").Value
        Address_2.Text = asSettings.Settings("buildingNo").Value
        Address_3.Text = asSettings.Settings("city").Value
        Address_4.Text = asSettings.Settings("postalCode").Value
        Phone.Text = asSettings.Settings("phone").Value
        Pesel.Text = asSettings.Settings("pesel").Value

        AccountNo.Text = asSettings.Settings("accountNo").Value
        BankAddress.Text = asSettings.Settings("bankAddress").Value

        rtbFooterText.Text = asSettings.Settings("footerText").Value
        Headline_info.Text = asSettings.Settings("headlineInfo").Value


        Me.Text = rm.GetString("lbInfo")
        btnSave.Text = rm.GetString("lbSave")
        Me.Refresh()

    End Sub


    Private Sub SaveSettings(sender As Object, e As EventArgs) Handles btnSave.Click

        'Header info

        asSettings.Settings.Item("headlineInfo").Value = Headline_info.Text

        'footerInfo
        asSettings.Settings.Item("footerText").Value = rtbFooterText.Text

        'Seller info
        asSettings.Settings.Item("sellerName").Value = rtbSellerName.Text
        asSettings.Settings.Item("address").Value = Address_1.Text
        asSettings.Settings.Item("buildingNo").Value = Address_2.Text
        asSettings.Settings.Item("city").Value = Address_3.Text
        asSettings.Settings.Item("postalCode").Value = Address_4.Text
        asSettings.Settings.Item("phone").Value = Phone.Text
        asSettings.Settings.Item("pesel").Value = Pesel.Text

        'Bank info

        If AccountNo.Text IsNot Nothing And BankAddress.Text IsNot Nothing Then
            asSettings.Settings.Item("accountNo").Value = AccountNo.Text
            asSettings.Settings.Item("bankAddress").Value = BankAddress.Text
        End If
        If rtbFooterText.Text <> "" Then
            asSettings.Settings("footerText").Value = rtbFooterText.Text
        Else
            asSettings.Settings("footerText").Value = ""
        End If
        If Headline_info.Text <> "" Then
            asSettings.Settings("headlineInfo").Value = Headline_info.Text
        Else
            asSettings.Settings("headlineInfo").Value = ""
        End If
        cAppConfig.Save(ConfigurationSaveMode.Modified)
        MsgBox("Information updated")
        Me.Close()
    End Sub

    Private Sub FormLoads(sender As Object, e As EventArgs) Handles MyBase.Load
        setLanguage()


        ToolTip1.ToolTipTitle = rm.GetString("lbWarning")
        ToolTip1.SetToolTip(lbPesel, rm.GetString("msgNotRequired"))
        ToolTip1.SetToolTip(Pesel, rm.GetString("msgNotRequired"))

        Headline_info.Text = asSettings.Settings.Item("headlineInfo").Value

        rtbFooterText.Text = asSettings.Settings.Item("footerText").Value

        rtbSellerName.Text = asSettings.Settings.Item("sellerName").Value
        Address_1.Text = asSettings.Settings.Item("address").Value
        Address_2.Text = asSettings.Settings.Item("buildingNo").Value
        Address_3.Text = asSettings.Settings.Item("city").Value
        Address_4.Text = asSettings.Settings.Item("postalCode").Value
        Phone.Text = asSettings.Settings.Item("phone").Value
        Pesel.Text = asSettings.Settings.Item("pesel").Value

        AccountNo.Text = asSettings.Settings.Item("accountNo").Value
        BankAddress.Text = asSettings.Settings.Item("bankAddress").Value
    End Sub
End Class