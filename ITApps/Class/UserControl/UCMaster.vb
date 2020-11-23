Public Enum MasterEnum
    MasterDeptEnum = 1
    MasterLocationEnum = 2
    MasterManufactureEnum = 3
    MasterStatusEnum = 4
    MasterTypeEnum = 5    
End Enum

Public Class UCMaster
    Private MasterEnum1 As MasterEnum
    Private drv As DataRowView
    Private Tx As DataRowView

    Public Sub initData(ByRef BS As BindingSource, ByVal MasterEnum As MasterEnum, ByRef tx As DataRowView)
        Me.MasterEnum1 = MasterEnum
        Me.Tx = tx
        drv = BS.AddNew

        TextBox1.DataBindings.Clear()

        Select Case MasterEnum1
            Case ITApps.MasterEnum.MasterDeptEnum
                Label1.Text = "Department Name"
                TextBox1.DataBindings.Add(New Binding("Text", drv, "deptname", True, DataSourceUpdateMode.OnPropertyChanged, ""))
            Case ITApps.MasterEnum.MasterLocationEnum
                Label1.Text = "Location Name"
                TextBox1.DataBindings.Add(New Binding("Text", drv, "locationname", True, DataSourceUpdateMode.OnPropertyChanged, ""))
            Case ITApps.MasterEnum.MasterManufactureEnum
                Label1.Text = "Manufacture Name"
                TextBox1.DataBindings.Add(New Binding("Text", drv, "manufacturename", True, DataSourceUpdateMode.OnPropertyChanged, ""))
            Case ITApps.MasterEnum.MasterStatusEnum
                Label1.Text = "Status Name"
                TextBox1.DataBindings.Add(New Binding("Text", drv, "statusname", True, DataSourceUpdateMode.OnPropertyChanged, ""))
            Case ITApps.MasterEnum.MasterTypeEnum
                Label1.Text = "Type Name"
                TextBox1.DataBindings.Add(New Binding("Text", drv, "typename", True, DataSourceUpdateMode.OnPropertyChanged, ""))
        End Select
    End Sub

    Public Overloads Function validate() As Boolean
        Select Case MasterEnum1
            Case ITApps.MasterEnum.MasterDeptEnum
                Tx.Item("department") = TextBox1.Text
            Case ITApps.MasterEnum.MasterLocationEnum
                Tx.Item("location") = TextBox1.Text
            Case ITApps.MasterEnum.MasterManufactureEnum
                Tx.Item("manufacture") = TextBox1.Text
            Case ITApps.MasterEnum.MasterStatusEnum
                Tx.Item("status") = TextBox1.Text
            Case ITApps.MasterEnum.MasterTypeEnum
                Tx.Item("type") = TextBox1.Text
        End Select
        Return True
    End Function

End Class
