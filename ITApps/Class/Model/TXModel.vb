Imports Npgsql
Public Class TXModel
    Implements IModel

    Dim myadapter As PostgreSQLDBAdapter = PostgreSQLDBAdapter.getInstance
    Private DeptBS As BindingSource
    Private LocationBS As BindingSource
    Private ManufactureBS As BindingSource
    Private StatusBS As BindingSource
    Private TypeBS As BindingSource
    Private DeptSeq As Long
    Private DS As DataSet


    Public Function getDeptBS() As BindingSource
        Return DeptBS
    End Function
    Public Function getLocationBS() As BindingSource
        Return LocationBS
    End Function
    Public Function getManufactureBS() As BindingSource
        Return ManufactureBS
    End Function
    Public Function getStatusBS() As BindingSource
        Return StatusBS
    End Function
    Public Function getTypeBS() As BindingSource
        Return TypeBS
    End Function

    Public ReadOnly Property FilterField
        Get
            Return "{0}"
        End Get
    End Property

    Public ReadOnly Property FilterFieldAll
        Get
            Return "manufacture like '%{0}%' or type like '%{0}%'  or department like '%{0}%'   or status like '%{0}%'   or location like '%{0}%'   or model like '%{0}%' " &
                " or [serial no] like '%{0}%'  or [sap asset no] like '%{0}%'  or [it asset no] like '%{0}%'  or [code label] like '%{0}%'  or specification like '%{0}%' " &
                " or purpose like '%{0}%'  or [computer name] like '%{0}%'  or cabinet like '%{0}%'   or vendor like '%{0}%'  or hotline like '%{0}%'   or remark like '%{0}%' " &
                "  or os like '%{0}%'  or office like '%{0}%'  or [app1] like '%{0}%'   or [app2] like '%{0}%'  or [power on password] like '%{0}%'  or [remark 2] like '%{0}%' "
        End Get
    End Property


    Public Function LoadData(DS As DataSet) As Boolean Implements IModel.LoadData
        Me.DS = DS
        Dim dataAdapter As NpgsqlDataAdapter = myadapter.getDbDataAdapter
        Dim myret As Boolean = False
        Dim sqlstr As String = String.Empty
        Using conn As NpgsqlConnection = myadapter.getConnection
            conn.Open()
            sqlstr = String.Format("select u.id,manufactureid,typeid,deptid,statusid,locationid,model,serialno as ""Serial No""," &
                                   " u.sapassetno as ""SAP Asset No"",u.itassetno as ""IT Asset No"",u.codelabel as ""Code Label""," &
                                   " u.specification,u.purpose,u.computername as ""Computer Name"",u.cabinet,u.vendor,u.invoicedate as ""Invoice date""," &
                                   " u.servicestartdate as ""Service Start Date"",u.serviceenddate as ""Service End Date"",u.hotline,u.remark,u.labeled," &
                                   " u.os,u.office,u.app1,u.app2,u.poweronpassword as ""Power On Password"",u.remark2 as ""Remark 2"",u.price,u.crcy," &
                                   " d.deptname as ""Department"",l.locationname as ""Location"",m.manufacturename as ""Manufacture"",s.statusname as ""Status"",t.typename as ""Type"" from {0} u left join it.dept d on d.id = u.deptid" &
                                   " left join it.location l on l.id = u.locationid" &
                                   " left join it.manufacture m on m.id = u.manufactureid " &
                                   " left join it.status s on s.id = u.statusid " &
                                   " left join it.type t on t.id = u.typeid order by {1};" &
                                   " select * from it.dept order by id;" &
                                   " select * from it.location order by id;" &
                                   " select * from it.manufacture order by id;" &
                                   " select * from it.status order by id;" &
                                   " select * from it.type order by id;", tablename, sortField)

            dataAdapter.SelectCommand = myadapter.getCommandObject(sqlstr, conn)
            dataAdapter.SelectCommand.CommandType = CommandType.Text
            dataAdapter.Fill(DS, tablename)
            PrepareDataSet(DS)
            myret = True
        End Using
        Return myret
    End Function



    Public ReadOnly Property sortField As String Implements IModel.sortField
        Get
            Return "id"
        End Get
    End Property

    Public ReadOnly Property tablename As String Implements IModel.tablename
        Get
            Return "it.inv_tx"
        End Get
    End Property

    Private Sub PrepareDataSet(ByRef DS As DataSet)
        Dim pk(0) As DataColumn
        pk(0) = DS.Tables(0).Columns("id")
        DS.Tables(0).PrimaryKey = pk
        DS.Tables(0).Columns("id").AutoIncrement = True
        DS.Tables(0).Columns("id").AutoIncrementSeed = -1
        DS.Tables(0).Columns("id").AutoIncrementStep = -1
        DS.Tables(0).TableName = tablename

        Dim pk1(0) As DataColumn
        pk1(0) = DS.Tables(1).Columns("id")
        DS.Tables(1).PrimaryKey = pk1
        DS.Tables(1).Columns("id").AutoIncrement = True
        DS.Tables(1).Columns("id").AutoIncrementSeed = DS.Tables(1).Rows(DS.Tables(1).Rows.Count - 1).Item("id") + 1
        DS.Tables(1).Columns("id").AutoIncrementStep = 1       
        DS.Tables(1).TableName = "Dept"


        Dim pk2(0) As DataColumn
        pk2(0) = DS.Tables(2).Columns("id")
        DS.Tables(2).PrimaryKey = pk2
        DS.Tables(2).Columns("id").AutoIncrement = True
        DS.Tables(2).Columns("id").AutoIncrementSeed = DS.Tables(2).Rows(DS.Tables(2).Rows.Count - 1).Item("id") + 1
        DS.Tables(2).Columns("id").AutoIncrementStep = 1
        DS.Tables(2).TableName = "Location"

        Dim pk3(0) As DataColumn
        pk3(0) = DS.Tables(3).Columns("id")
        DS.Tables(3).PrimaryKey = pk3
        DS.Tables(3).Columns("id").AutoIncrement = True
        DS.Tables(3).Columns("id").AutoIncrementSeed = DS.Tables(3).Rows(DS.Tables(3).Rows.Count - 1).Item("id") + 1
        DS.Tables(3).Columns("id").AutoIncrementStep = 1
        DS.Tables(3).TableName = "Manufacture"

        Dim pk4(0) As DataColumn
        pk4(0) = DS.Tables(4).Columns("id")
        DS.Tables(4).PrimaryKey = pk4
        DS.Tables(4).Columns("id").AutoIncrement = True
        DS.Tables(4).Columns("id").AutoIncrementSeed = DS.Tables(4).Rows(DS.Tables(4).Rows.Count - 1).Item("id") + 1
        DS.Tables(4).Columns("id").AutoIncrementStep = 1
        DS.Tables(4).TableName = "Status"

        Dim pk5(0) As DataColumn
        pk5(0) = DS.Tables(5).Columns("id")
        DS.Tables(5).PrimaryKey = pk5
        DS.Tables(5).Columns("id").AutoIncrement = True
        DS.Tables(5).Columns("id").AutoIncrementSeed = DS.Tables(5).Rows(DS.Tables(5).Rows.Count - 1).Item("id") + 1
        DS.Tables(5).Columns("id").AutoIncrementStep = 1
        DS.Tables(5).TableName = "Type"


        'Dim TXCol As DataColumn
        Dim DeptCol As DataColumn
        Dim LocationCol As DataColumn
        Dim ManufactureCol As DataColumn
        Dim StatusCol As DataColumn
        Dim TypeCol As DataColumn


        DeptCol = DS.Tables("Dept").Columns("id")
        LocationCol = DS.Tables("Location").Columns("id")
        ManufactureCol = DS.Tables("Manufacture").Columns("id")
        StatusCol = DS.Tables("Status").Columns("id")
        TypeCol = DS.Tables("Type").Columns("id")


        'Data Relation
        Dim rel As New DataRelation("DeptRel", DeptCol, DS.Tables(0).Columns("deptid"))
        Dim rel1 As New DataRelation("LocationRel", LocationCol, DS.Tables(0).Columns("locationid"))
        Dim rel2 As New DataRelation("ManufactureRel", ManufactureCol, DS.Tables(0).Columns("manufactureid"))
        Dim rel3 As New DataRelation("StatusRel", StatusCol, DS.Tables(0).Columns("statusid"))
        Dim rel4 As New DataRelation("TypeRel", TypeCol, DS.Tables(0).Columns("typeid"))

        DS.Relations.Add(rel)
        DS.Relations.Add(rel1)
        DS.Relations.Add(rel2)
        DS.Relations.Add(rel3)
        DS.Relations.Add(rel4)

        DeptBS = New BindingSource
        DeptBS.DataSource = DS.Tables("Dept")


        LocationBS = New BindingSource
        LocationBS.DataSource = DS.Tables("Location")
        ManufactureBS = New BindingSource
        ManufactureBS.DataSource = DS.Tables("Manufacture")
        StatusBS = New BindingSource
        StatusBS.DataSource = DS.Tables("Status")
        TypeBS = New BindingSource
        TypeBS.DataSource = DS.Tables("Type")




      
    End Sub

    Public Function save(obj As Object, mye As ContentBaseEventArgs) As Boolean Implements IModel.save
        Dim dataadapter As NpgsqlDataAdapter = myadapter.getDbDataAdapter
        Dim myret As Boolean = False
        AddHandler dataadapter.RowUpdated, AddressOf myadapter.onRowInsertUpdate
        Dim mytransaction As Npgsql.NpgsqlTransaction
        Using conn As Object = myadapter.getConnection
            conn.Open()
            mytransaction = conn.BeginTransaction
            'it.inv_tx
            Dim sqlstr As String

            'Master Table

            sqlstr = "it.sp_insertdept"
            dataadapter.InsertCommand = New NpgsqlCommand(sqlstr, conn)
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "deptname").SourceVersion = DataRowVersion.Current            
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Bigint, 0, "id").Direction = ParameterDirection.InputOutput
            dataadapter.InsertCommand.CommandType = CommandType.StoredProcedure

            dataadapter.InsertCommand.Transaction = mytransaction
            mye.ra = dataadapter.Update(mye.dataset.Tables("dept"))


            sqlstr = "it.sp_insertlocation"
            dataadapter.InsertCommand = New NpgsqlCommand(sqlstr, conn)
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "locationname").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Bigint, 0, "id").Direction = ParameterDirection.InputOutput
            dataadapter.InsertCommand.CommandType = CommandType.StoredProcedure

            dataadapter.InsertCommand.Transaction = mytransaction
            mye.ra = dataadapter.Update(mye.dataset.Tables("location"))


            sqlstr = "it.sp_insertmanufacture"
            dataadapter.InsertCommand = New NpgsqlCommand(sqlstr, conn)
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "manufacturename").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Bigint, 0, "id").Direction = ParameterDirection.InputOutput
            dataadapter.InsertCommand.CommandType = CommandType.StoredProcedure

            dataadapter.InsertCommand.Transaction = mytransaction
            mye.ra = dataadapter.Update(mye.dataset.Tables("manufacture"))


            sqlstr = "it.sp_insertstatus"
            dataadapter.InsertCommand = New NpgsqlCommand(sqlstr, conn)
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "statusname").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Bigint, 0, "id").Direction = ParameterDirection.InputOutput
            dataadapter.InsertCommand.CommandType = CommandType.StoredProcedure

            dataadapter.InsertCommand.Transaction = mytransaction
            mye.ra = dataadapter.Update(mye.dataset.Tables("status"))


            sqlstr = "it.sp_inserttype"
            dataadapter.InsertCommand = New NpgsqlCommand(sqlstr, conn)
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "typename").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Bigint, 0, "id").Direction = ParameterDirection.InputOutput
            dataadapter.InsertCommand.CommandType = CommandType.StoredProcedure

            dataadapter.InsertCommand.Transaction = mytransaction
            mye.ra = dataadapter.Update(mye.dataset.Tables("type"))


            'Transaction


            sqlstr = "it.sp_deleteinvtx"
            dataadapter.DeleteCommand = New NpgsqlCommand(sqlstr, conn)
            dataadapter.DeleteCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Bigint, 0, "id").SourceVersion = DataRowVersion.Original
            dataadapter.DeleteCommand.CommandType = CommandType.StoredProcedure

            sqlstr = "it.sp_insertinvtx"
            dataadapter.InsertCommand = New NpgsqlCommand(sqlstr, conn)
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Bigint, 0, "manufactureid").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Bigint, 0, "typeid").SourceVersion = DataRowVersion.Current        
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Bigint, 0, "deptid").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Bigint, 0, "statusid").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Bigint, 0, "locationid").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "model").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "serial no").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "sap asset no").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "it asset no").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "code label").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "specification").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "purpose").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "computer name").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "cabinet").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "vendor").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Date, 0, "invoice date").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Date, 0, "service start date").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Date, 0, "service end date").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "hotline").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "remark").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Boolean, 0, "labeled").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "os").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "office").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "app 1").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "app 2").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "power on password").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "remark 2").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Numeric, 0, "price").SourceVersion = DataRowVersion.Current
            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "crcy").SourceVersion = DataRowVersion.Current

            dataadapter.InsertCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Bigint, 0, "id").Direction = ParameterDirection.InputOutput

            dataadapter.InsertCommand.CommandType = CommandType.StoredProcedure

            sqlstr = "it.sp_updateinvtx"
            dataadapter.UpdateCommand = New NpgsqlCommand(sqlstr, conn)
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Bigint, 0, "id").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Bigint, 0, "manufactureid").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Bigint, 0, "typeid").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Bigint, 0, "deptid").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Bigint, 0, "statusid").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Bigint, 0, "locationid").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "model").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "serial no").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "sap asset no").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "it asset no").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "code label").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "specification").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "purpose").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "computer name").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "cabinet").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "vendor").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Date, 0, "invoice date").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Date, 0, "service start date").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Date, 0, "service end date").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "hotline").SourceVersion = DataRowVersion.Current

            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "remark").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Boolean, 0, "labeled").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "os").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "office").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "app 1").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "app 2").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "power on password").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "remark 2").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Numeric, 0, "price").SourceVersion = DataRowVersion.Current
            dataadapter.UpdateCommand.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Varchar, 0, "crcy").SourceVersion = DataRowVersion.Current

            dataadapter.UpdateCommand.CommandType = CommandType.StoredProcedure

            dataadapter.InsertCommand.Transaction = mytransaction
            dataadapter.UpdateCommand.Transaction = mytransaction
            dataadapter.DeleteCommand.Transaction = mytransaction

            mye.ra = dataadapter.Update(mye.dataset.Tables(tablename))

            mytransaction.Commit()
            myret = True
        End Using
        Return myret
    End Function





End Class
