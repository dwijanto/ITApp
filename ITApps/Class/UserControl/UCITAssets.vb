Public Class UCITAssets
    Private drv As DataRowView
    Public Event RefreshInterface()
    Private ManufactureBS As BindingSource
    Private TypeBS As BindingSource
    Private DepartmentBS As BindingSource
    Private StatusBS As BindingSource
    Private LocationBS As BindingSource


    Public Sub BindingControl(ByRef drv As DataRowView, ByRef ManufactureBS As BindingSource,
                                                        ByRef TypeBS As BindingSource,
                                                        ByRef DepartmentBS As BindingSource,
                                                        ByRef StatusBS As BindingSource,
                                                        ByRef LocationBS As BindingSource)
        Me.drv = drv
        Me.ManufactureBS = ManufactureBS
        Me.TypeBS = TypeBS
        Me.DepartmentBS = DepartmentBS
        Me.StatusBS = StatusBS
        Me.LocationBS = LocationBS


        ComboBox1.DataBindings.Clear()
        ComboBox2.DataBindings.Clear()
        ComboBox3.DataBindings.Clear()
        ComboBox4.DataBindings.Clear()
        ComboBox5.DataBindings.Clear()

        CheckBox1.DataBindings.Clear()

        TextBox1.DataBindings.Clear()
        TextBox2.DataBindings.Clear()
        TextBox3.DataBindings.Clear()
        TextBox4.DataBindings.Clear()
        TextBox5.DataBindings.Clear()
        TextBox6.DataBindings.Clear()
        TextBox7.DataBindings.Clear()
        TextBox8.DataBindings.Clear()
        TextBox9.DataBindings.Clear()
        TextBox10.DataBindings.Clear()
        TextBox11.DataBindings.Clear()
        TextBox12.DataBindings.Clear()
        TextBox13.DataBindings.Clear()
        TextBox14.DataBindings.Clear()
        TextBox15.DataBindings.Clear()
        TextBox16.DataBindings.Clear()
        TextBox17.DataBindings.Clear()
        TextBox18.DataBindings.Clear()
        TextBox19.DataBindings.Clear()
        TextBox20.DataBindings.Clear()


        DateTimePicker1.DataBindings.Clear()
        DateTimePicker2.DataBindings.Clear()
        DateTimePicker3.DataBindings.Clear()


        ComboBox1.DataSource = ManufactureBS
        ComboBox1.DisplayMember = "manufacturename"
        ComboBox1.ValueMember = "id"

        ComboBox2.DataSource = TypeBS
        ComboBox2.DisplayMember = "typename"
        ComboBox2.ValueMember = "id"

        ComboBox3.DataSource = DepartmentBS
        ComboBox3.DisplayMember = "deptname"
        ComboBox3.ValueMember = "id"

        ComboBox4.DataSource = StatusBS
        ComboBox4.DisplayMember = "statusname"
        ComboBox4.ValueMember = "id"

        ComboBox5.DataSource = LocationBS
        ComboBox5.DisplayMember = "locationname"
        ComboBox5.ValueMember = "id"

        TextBox1.DataBindings.Add(New Binding("Text", drv, "model", True, DataSourceUpdateMode.OnPropertyChanged, ""))
        TextBox2.DataBindings.Add(New Binding("Text", drv, "serial no", True, DataSourceUpdateMode.OnPropertyChanged, ""))
        TextBox3.DataBindings.Add(New Binding("Text", drv, "sap asset no", True, DataSourceUpdateMode.OnPropertyChanged, ""))
        TextBox4.DataBindings.Add(New Binding("Text", drv, "it asset no", True, DataSourceUpdateMode.OnPropertyChanged, ""))
        TextBox5.DataBindings.Add(New Binding("Text", drv, "code label", True, DataSourceUpdateMode.OnPropertyChanged, ""))
        TextBox6.DataBindings.Add(New Binding("Text", drv, "specification", True, DataSourceUpdateMode.OnPropertyChanged, ""))
        TextBox7.DataBindings.Add(New Binding("Text", drv, "purpose", True, DataSourceUpdateMode.OnPropertyChanged, ""))
        TextBox8.DataBindings.Add(New Binding("Text", drv, "computer name", True, DataSourceUpdateMode.OnPropertyChanged, ""))

        TextBox9.DataBindings.Add(New Binding("Text", drv, "cabinet", True, DataSourceUpdateMode.OnPropertyChanged, ""))

        TextBox10.DataBindings.Add(New Binding("Text", drv, "vendor", True, DataSourceUpdateMode.OnPropertyChanged, ""))
        TextBox11.DataBindings.Add(New Binding("Text", drv, "hotline", True, DataSourceUpdateMode.OnPropertyChanged, ""))
        TextBox12.DataBindings.Add(New Binding("Text", drv, "remark", True, DataSourceUpdateMode.OnPropertyChanged, ""))
        TextBox13.DataBindings.Add(New Binding("Text", drv, "os", True, DataSourceUpdateMode.OnPropertyChanged, ""))
        TextBox14.DataBindings.Add(New Binding("Text", drv, "office", True, DataSourceUpdateMode.OnPropertyChanged, ""))
        TextBox15.DataBindings.Add(New Binding("Text", drv, "app1", True, DataSourceUpdateMode.OnPropertyChanged, ""))
        TextBox16.DataBindings.Add(New Binding("Text", drv, "app2", True, DataSourceUpdateMode.OnPropertyChanged, ""))
        TextBox17.DataBindings.Add(New Binding("Text", drv, "power on password", True, DataSourceUpdateMode.OnPropertyChanged, ""))
        TextBox18.DataBindings.Add(New Binding("Text", drv, "remark 2", True, DataSourceUpdateMode.OnPropertyChanged, ""))
        TextBox19.DataBindings.Add(New Binding("Text", drv, "price", True, DataSourceUpdateMode.OnPropertyChanged, ""))
        TextBox20.DataBindings.Add(New Binding("Text", drv, "crcy", True, DataSourceUpdateMode.OnPropertyChanged, ""))


        ComboBox1.DataBindings.Add(New Binding("SelectedValue", drv, "manufactureid", True, DataSourceUpdateMode.OnPropertyChanged, ""))
        ComboBox2.DataBindings.Add(New Binding("SelectedValue", drv, "typeid", True, DataSourceUpdateMode.OnPropertyChanged, ""))
        ComboBox3.DataBindings.Add(New Binding("SelectedValue", drv, "deptid", True, DataSourceUpdateMode.OnPropertyChanged, ""))
        ComboBox4.DataBindings.Add(New Binding("SelectedValue", drv, "statusid", True, DataSourceUpdateMode.OnPropertyChanged, ""))
        ComboBox5.DataBindings.Add(New Binding("SelectedValue", drv, "locationid", True, DataSourceUpdateMode.OnPropertyChanged, ""))

        CheckBox1.DataBindings.Add(New Binding("Checked", drv, "labeled", True, DataSourceUpdateMode.OnPropertyChanged, ""))

        DateTimePicker1.DataBindings.Add(New Binding("Text", drv, "invoice date", True, DataSourceUpdateMode.OnPropertyChanged, ""))
        DateTimePicker2.DataBindings.Add(New Binding("Text", drv, "service start date", True, DataSourceUpdateMode.OnPropertyChanged, ""))
        DateTimePicker3.DataBindings.Add(New Binding("Text", drv, "service end date", True, DataSourceUpdateMode.OnPropertyChanged, ""))
    End Sub



    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged, TextBox10.TextChanged, TextBox11.TextChanged, TextBox12.TextChanged, TextBox13.TextChanged, TextBox14.TextChanged,
        TextBox15.TextChanged, TextBox16.TextChanged, TextBox17.TextChanged, TextBox18.TextChanged, TextBox19.TextChanged, TextBox20.TextChanged, TextBox2.TextChanged, TextBox3.TextChanged, TextBox4.TextChanged, TextBox5.TextChanged, TextBox6.TextChanged,
        TextBox7.TextChanged, TextBox8.TextChanged, TextBox9.TextChanged
        RaiseEvent RefreshInterface()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click, Button2.Click, Button3.Click, Button4.Click, Button5.Click
        Dim myObj As Button = DirectCast(sender, Button)
        Dim myform As DialogMaster = Nothing
        Select Case myObj.Name
            Case "Button1"
                myform = New DialogMaster(ManufactureBS, MasterEnum.MasterManufactureEnum, drv)
            Case "Button2"
                myform = New DialogMaster(TypeBS, MasterEnum.MasterTypeEnum, drv)
            Case "Button3"
                myform = New DialogMaster(DepartmentBS, MasterEnum.MasterDeptEnum, drv)
            Case "Button4"
                myform = New DialogMaster(StatusBS, MasterEnum.MasterStatusEnum, drv)
            Case "Button5"
                myform = New DialogMaster(LocationBS, MasterEnum.MasterLocationEnum, drv)
        End Select
        myform.ShowDialog()

    End Sub


End Class
