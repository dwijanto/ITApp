Imports System.Text


Public Class ImportITAssets
    Private ManufactureDict As New Dictionary(Of String, Long)
    Private TypeDict As New Dictionary(Of String, Long)
    Private DeptDict As New Dictionary(Of String, Long)
    Private StatusDict As New Dictionary(Of String, Long)
    Private LocationDict As New Dictionary(Of String, Long)
    Private TXSB As StringBuilder
    Private DeptSB As StringBuilder
    Private LocationSB As StringBuilder
    Private ManufactureSB As StringBuilder
    Private StatusSB As StringBuilder
    Private TypeSB As StringBuilder
    Private myController As TXController
    Private myAdapter As PostgreSQLDBAdapter = PostgreSQLDBAdapter.getInstance
    Private myForm As FormITAssets

    Public Function RunImport(ByVal MyForm As FormITAssets, ByVal filename As String, ByRef myController As TXController) As Boolean
        Dim myret As Boolean = True
        Me.myForm = MyForm
        Me.myController = myController
        'prepare helper
        PrepareHelper()

        Dim ds As DataSet = myController.GetDataset
        Dim myList As New List(Of String())
        Dim myrecord() As String

        TXSB = New StringBuilder
        DeptSB = New StringBuilder
        LocationSB = New StringBuilder
        ManufactureSB = New StringBuilder
        StatusSB = New StringBuilder
        TypeSB = New StringBuilder

        'Read Text File
        Using objTFParser = New FileIO.TextFieldParser(filename)
            With objTFParser
                .TextFieldType = FileIO.FieldType.Delimited
                .SetDelimiters(Chr(9))
                .HasFieldsEnclosedInQuotes = True
                Dim count As Long = 0
                MyForm.ProgressReport(1, "Read Data")

                Do Until .EndOfData
                    myrecord = .ReadFields
                    myList.Add(myrecord)
                Loop
            End With

            For i = 1 To myList.Count - 1
                CreateRecord(myList(i))
            Next
        End Using
        'Copy Master
        If Not copyMaster() Then
            Return False
        End If

        'Copy TX
        If Not CopyTx() Then
            Return False
        End If


        'Delete TX
        'Copy Tx

        Return myret


    End Function

    Private Sub PrepareHelper()
        Dim DS = myController.GetDataset
        'Manufacture
        For i = 0 To DS.Tables("Manufacture").Rows.Count - 1
            Dim drv As DataRow = DS.Tables("Manufacture").Rows(i)
            ManufactureDict.Add(drv.Item("manufacturename"), drv.Item("id"))
        Next
        'location
        For i = 0 To DS.Tables("Location").Rows.Count - 1
            Dim drv As DataRow = DS.Tables("Location").Rows(i)
            LocationDict.Add(drv.Item("locationname"), drv.Item("id"))
        Next
        'dept
        For i = 0 To DS.Tables("Dept").Rows.Count - 1
            Dim drv As DataRow = DS.Tables("Dept").Rows(i)
            If Not IsDBNull(drv.Item("deptname")) Then
                DeptDict.Add(drv.Item("deptname"), drv.Item("id"))
            End If

        Next
        'Status
        For i = 0 To DS.Tables("Status").Rows.Count - 1
            Dim drv As DataRow = DS.Tables("Status").Rows(i)
            StatusDict.Add(drv.Item("statusname"), drv.Item("id"))
        Next
        'Type
        For i = 0 To DS.Tables("Type").Rows.Count - 1
            Dim drv As DataRow = DS.Tables("Type").Rows(i)
            TypeDict.Add(drv.Item("typename"), drv.Item("id"))
        Next
    End Sub


    Private Function GetDeptID(ByVal p As String) As Long
        Dim DS As DataSet = myController.GetDataset
        Dim myret As Long
        If p = "" Then
            Return 0
        End If
        If DeptDict.ContainsKey(p) Then
            myret = DeptDict(p)
        Else
            'Create New Record And Dict
            Dim dr = DS.Tables("Dept").NewRow
            dr.Item("deptname") = p
            DS.Tables("Dept").Rows.Add(dr)
            myret = dr.Item("id")
            DeptSB.Append(myret & vbTab & p & vbCrLf)
            DeptDict.Add(p, myret)
        End If
        Return myret
    End Function

    Private Function GetManufactureID(ByVal p As String) As Long
        Dim DS As DataSet = myController.GetDataset
        Dim myret As Long
        If ManufactureDict.ContainsKey(p) Then
            myret = ManufactureDict(p)
        Else
            'Create New Record And Dict
            Dim dr = DS.Tables("Manufacture").NewRow
            dr.Item("manufacturename") = p
            DS.Tables("Manufacture").Rows.Add(dr)
            myret = dr.Item("id")
            ManufactureSB.Append(myret & vbTab & p & vbCrLf)
            ManufactureDict.Add(p, myret)
        End If
        Return myret
    End Function

    Private Function GetTypeID(ByVal p As String) As Long
        Dim DS As DataSet = myController.GetDataset
        Dim myret As Long
        If TypeDict.ContainsKey(p) Then
            myret = TypeDict(p)
        Else
            'Create New Record And Dict
            Dim dr = DS.Tables("Type").NewRow
            dr.Item("TypeName") = p
            DS.Tables("Type").Rows.Add(dr)
            myret = dr.Item("id")
            TypeSB.Append(myret & vbTab & p & vbCrLf)
            TypeDict.Add(p, myret)
        End If
        Return myret
    End Function

    Private Function GetLocationID(ByVal p As String) As Long
        Dim DS As DataSet = myController.GetDataset
        Dim myret As Long
        If LocationDict.ContainsKey(p) Then
            myret = LocationDict(p)
        Else
            'Create New Record And Dict
            Dim dr = DS.Tables("Location").NewRow
            dr.Item("LocationName") = p
            DS.Tables("Location").Rows.Add(dr)
            myret = dr.Item("id")
            LocationSB.Append(myret & vbTab & p & vbCrLf)
            LocationDict.Add(p, myret)
        End If
        Return myret
    End Function

    Private Function GetStatusID(ByVal p As String) As Long
        Dim DS As DataSet = myController.GetDataset
        Dim myret As Long
        If StatusDict.ContainsKey(p) Then
            myret = StatusDict(p)
        Else
            'Create New Record And Dict
            Dim dr = DS.Tables("Status").NewRow
            dr.Item("StatusName") = p
            DS.Tables("Status").Rows.Add(dr)
            myret = dr.Item("id")
            StatusSB.Append(myret & vbTab & p & vbCrLf)
            StatusDict.Add(p, myret)
        End If
        Return myret
    End Function

    Private Sub CreateRecord(ByVal value As String())
        If value(12) = "" Then
            Exit Sub
        End If

        Dim DeptId As Long = GetDeptID(value(4))
        Dim ManufatureId As Long = GetManufactureID(value(0))
        Dim TypeId As Long = GetTypeID(value(2))
        Dim LocationId As Long = GetLocationID(value(12))
        Dim StatusId As Long = GetStatusID(value(8))
        'TXSB.Append("")

        'manufactureid bigint,  typeid bigint,  deptid bigint,  statusid bigint,  locationid bigint,  model character varying,  serialno character varying,  
        'sapassetno character varying,  itassetno character varying,  codelabel character varying,  specification character varying,  purpose character varying,
        'computername character varying,  cabinet character varying,  vendor character varying,  invoicedate date,  servicestartdate date,  serviceenddate date,
        'hotline character varying,  remark character varying,  labeled boolean,  os character varying,  office character varying,  app1 character varying,
        'app2 character varying,  poweronpassword character varying,  remark2 character varying,  price numeric,  crcy character varying,

        'Manufacturer	Model	Type	Serial No	Department	SAP Asset	Asset No	Code Label	Status	Specification	Purpose	Computer_name	Location	Cabinet	Vendor	Invoice Date	Service Start Date	Service End Date	Hotline	Remarks	Labeled	OS	Office	Application 1	Application 2	Power On Password	Remark	F27	Price

        TXSB.Append(ManufatureId & vbTab & validStr(value(1)) & vbTab & TypeId & vbTab & validStr(value(3)) & vbTab & DeptId & vbTab & validStr(value(5)) & vbTab &
                    validStr(value(6)) & vbTab & validStr(value(7)) & vbTab & StatusId & vbTab & validStr(value(9)) & vbTab & validStr(value(10)) & vbTab &
                    validStr(value(11)) & vbTab & LocationId & vbTab & validStr(value(13)) & vbTab & validStr(value(14)) & vbTab & validDate(value(15)) & vbTab &
                    validDate(value(16)) & vbTab & validDate(value(17)) & vbTab & validStr(value(18)) & vbTab & validStr(value(19)) & vbTab & value(20) & vbTab &
                    validStr(value(21)) & vbTab & validStr(value(22)) & vbTab & validStr(value(23)) & vbTab & validStr(value(24)) & vbTab & validStr(value(25)) & vbTab &
                    validStr(value(26)) & vbTab & validNumeric(value(27)) & vbTab & validStr(value(28)) & vbCrLf)



    End Sub

    Private Function validStr(ByVal p As String) As String
        If p = "" Then
            Return "Null"
        End If

        Return String.Format("{0}", p.Replace(vbTab, "").Replace(Chr(13), "").Replace(Chr(10), ""))
    End Function

    Private Function validNumeric(ByVal p As String) As String
        If p = "" Then
            Return "Null"
        End If
        Return String.Format("{0}", p)
    End Function

    Private Function validDate(ByVal p As String) As String
        If p = "" Then
            Return "Null"
        End If
        Return String.Format("'{0:yyyy-mm-dd}'", p)
    End Function

    Private Function copyMaster() As Boolean
        Dim Sqlstr As String = String.Empty
        Dim myret As Boolean = True
        Dim message As String = String.Empty
        If DeptSB.Length > 0 Then
            Sqlstr = "copy it.dept(id,deptname)  from stdin with null as 'Null';"
            message = myAdapter.copy(Sqlstr, DeptSB.ToString, myret)
            If Not myret Then
                myForm.ProgressReport(1, message)
                Return myret
            End If
        End If

        If LocationSB.Length > 0 Then
            Sqlstr = "copy it.location(id,locationname)  from stdin with null as 'Null';"
            message = myAdapter.copy(Sqlstr, LocationSB.ToString, myret)
            If Not myret Then
                myForm.ProgressReport(1, message)
                Return myret
            End If
        End If

        If ManufactureSB.Length > 0 Then
            Sqlstr = "copy it.manufacture(id,manufacturename)  from stdin with null as 'Null';"
            message = myAdapter.copy(Sqlstr, ManufactureSB.ToString, myret)
            If Not myret Then
                myForm.ProgressReport(1, message)
                Return myret
            End If
        End If
        If StatusSB.Length > 0 Then
            Sqlstr = "copy it.status(id,statusname)  from stdin with null as 'Null';"
            message = myAdapter.copy(Sqlstr, StatusSB.ToString, myret)
            If Not myret Then
                myForm.ProgressReport(1, message)
                Return myret
            End If
        End If

        If TypeSB.Length > 0 Then
            Sqlstr = "copy it.type(id,typename)  from stdin with null as 'Null';"
            message = myAdapter.copy(Sqlstr, TypeSB.ToString, myret)
            If Not myret Then
                myForm.ProgressReport(1, message)
                Return myret
            End If
        End If

        Return myret
    End Function

    Private Function CopyTx() As Boolean
        Dim Sqlstr As String = String.Empty
        Dim myret As Boolean = True
        Dim message As String = String.Empty
        If TXSB.Length > 0 Then
            Sqlstr = "delete from it.inv_tx;select setval('it.inv_tx_id_seq',1,false);"
            Dim ra As Long
            If Not myAdapter.ExecuteNonQuery(Sqlstr, recordAffected:=ra, message:=message) Then
                myForm.ProgressReport(1, message)
                Return False
            End If

            Sqlstr = "copy it.inv_tx(manufactureid,model,typeid,serialno,deptid,sapassetno,itassetno,codelabel,statusid,specification,purpose,computername,locationid,cabinet,vendor,invoicedate,servicestartdate,serviceenddate,hotline,remark,labeled,os,office,app1,app2,poweronpassword,remark2,price,crcy)  from stdin with null as 'Null';"
            message = myAdapter.copy(Sqlstr, TXSB.ToString, myret)
            If Not myret Then
                myForm.ProgressReport(1, message)
                Return myret
            End If
        End If
        
        Return myret
    End Function

End Class
