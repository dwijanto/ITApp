Imports System.Windows.Forms

Public Class DialogITAssets

    Private drv As DataRowView
    Private ManufactureBS As BindingSource
    Private TypeBS As BindingSource
    Private DeptBS As BindingSource
    Private StatusBS As BindingSource
    Private LocationBS As BindingSource

    Public Event RefreshIntervace()

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        drv.EndEdit()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        drv.CancelEdit()
        Me.Close()
    End Sub

    Public Sub New(drv As DataRowView, ManufactureBS As BindingSource,
                                              TypeBS As BindingSource,
                                              DeptBS As BindingSource,
                                              StatusBS As BindingSource,
                                              LocationBS As BindingSource)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.drv = drv
        Me.ManufactureBS = ManufactureBS
        Me.TypeBS = TypeBS
        Me.DeptBS = DeptBS
        Me.StatusBS = StatusBS
        Me.LocationBS = LocationBS
        RemoveHandler UcitAssets1.RefreshInterface, AddressOf myRefreshInterface
        AddHandler UcitAssets1.RefreshInterface, AddressOf myRefreshInterface
    End Sub


    Private Sub DialogITAssets_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            UcitAssets1.BindingControl(drv, ManufactureBS, TypeBS,
                                            DeptBS,
                                            StatusBS,
                                            LocationBS)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        
    End Sub

    Private Sub myRefreshInterface()
        RaiseEvent RefreshIntervace()
    End Sub

End Class
