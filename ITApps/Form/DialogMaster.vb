Imports System.Windows.Forms

Public Class DialogMaster

    Private BS As BindingSource
    Private Position As Integer
    Public Event DialogMasterEvent As EventHandler

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        UcMaster1.validate()

        BS.EndEdit()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        BS.CancelEdit()
        BS.Position = Position
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Public Sub New(ByRef BS As BindingSource, ByVal MasterTypeEnum As MasterEnum, ByRef TX As DataRowView)

        ' This call is required by the designer.
        InitializeComponent()
        Me.BS = BS
        ' Add any initialization after the InitializeComponent() call.
        Position = BS.Position
        UcMaster1.initData(BS, MasterTypeEnum, TX)


    End Sub


End Class
