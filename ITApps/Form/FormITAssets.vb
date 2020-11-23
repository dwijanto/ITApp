Imports System.Threading
Imports ITApps.UserInfo
Public Delegate Sub ProgressReportDelegate(ByVal id As Integer, ByVal message As String)

Public Enum TxEnum
    NewRecord = 1
    CopyRecord = 2
    UpdateRecord = 3
End Enum

Public Class FormITAssets
    Dim myThread As New System.Threading.Thread(AddressOf DoWork)
    Dim myController As TXController
    Dim UserInfo1 As UserInfo = UserInfo.getInstance
    Public Shared myForm As FormITAssets
    Dim drv As DataRowView
    Dim ImportFile As String
    Private OpenFileDialog1 As New OpenFileDialog

    Public Shared Function getInstance()
        If myForm Is Nothing Then
            myForm = New FormITAssets
        ElseIf myForm.IsDisposed Then
            myForm = New FormITAssets
        End If
        Return myForm
    End Function

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub RefreshMYInterface()
        DataGridView1.Invalidate()
    End Sub

    Sub DoWork()
        myController = New TXController
        Try
            ProgressReport(1, "Loading...Please wait.")
            If myController.loaddata() Then
                ProgressReport(4, "Init Data")
            End If
            ProgressReport(1, String.Format("Loading...Done. Records {0}", myController.BS.Count))
        Catch ex As Exception
            ProgressReport(1, ex.Message)
        End Try

    End Sub
    Public Sub ProgressReport(ByVal id As Integer, ByVal message As String)
        If Me.InvokeRequired Then
            Dim d As New ProgressReportDelegate(AddressOf ProgressReport)
            Me.Invoke(d, New Object() {id, message})
        Else
            Select Case id
                Case 1
                    ToolStripStatusLabel1.Text = message
                Case 4
                    DataGridView1.AutoGenerateColumns = False
                    DataGridView1.DataSource = myController.BS
            End Select
        End If
    End Sub

    Public Sub showTx(ByVal tx As TxEnum)

        If IsNothing(myController) Then
            MessageBox.Show("Refresh the query first.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If Not myThread.IsAlive And Not IsNothing(myController) Then
            Select Case tx
                Case TxEnum.NewRecord
                    drv = myController.GetNewRecord
                    drv.Row.Item("deptid") = 0
                    drv.Row.Item("manufactureid") = 1
                    drv.Row.Item("typeid") = 1
                    drv.Row.Item("statusid") = 1
                    drv.Row.Item("locationid") = 1


                    Me.drv.BeginEdit()

                Case TxEnum.UpdateRecord
                    drv = myController.GetCurrentRecord
                    Me.drv.BeginEdit()
                Case TxEnum.CopyRecord
                    Dim drvori = myController.GetCurrentRecord
                    drv = myController.GetNewRecord

                    For i = 1 To drv.Row.ItemArray.Length - 1
                        drv.Row.Item(i) = drvori.Row.Item(i)
                    Next
                    Me.drv.BeginEdit()
            End Select

            Dim myform = New DialogITAssets(drv, myController.Model.getManufactureBS,
                                              myController.Model.getTypeBS,
                                              myController.Model.getDeptBS,
                                              myController.Model.getStatusBS,
                                              myController.Model.getLocationBS)

            RemoveHandler myform.RefreshIntervace, AddressOf RefreshMYInterface
            AddHandler myform.RefreshIntervace, AddressOf RefreshMYInterface
            myform.ShowDialog()

        End If

    End Sub

    Private Sub AddNewToolStripButton1_Click(sender As Object, e As EventArgs) Handles AddNewToolStripButton1.Click
        showTx(TxEnum.NewRecord)
    End Sub

    Private Sub DeleteToolStripButton2_Click(sender As Object, e As EventArgs) Handles DeleteToolStripButton2.Click
        If Not IsNothing(myController.GetCurrentRecord) Then
            If MessageBox.Show("Delete this record?", "Delete Record", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
                For Each drv As DataGridViewRow In DataGridView1.SelectedRows
                    myController.RemoveAt(drv.Index)
                Next
            End If
        End If
    End Sub

    Private Sub FormITAssets_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadData()
    End Sub

    Private Sub LoadData()
        If Not myThread.IsAlive Then
            ToolStripStatusLabel1.Text = ""
            myThread = New Thread(AddressOf DoWork)
            myThread.Start()
        Else
            MessageBox.Show("Please wait until the current process is finished.")
        End If
    End Sub

    Private Sub RefreshToolStripButton4_Click(sender As Object, e As EventArgs) Handles RefreshToolStripButton4.Click
        If CheckSavedData() Then LoadData()
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click        
        Try
            myController.ApplyFilter = ToolStripTextBox1.Text
            ProgressReport(1, String.Format("Filter Done. Records {0} ", myController.BS.Count))
        Catch ex As Exception
            ProgressReport(1, String.Format("{0}", ex.Message))
        End Try

    End Sub

    Private Sub CopyToolStripButton2_Click(sender As Object, e As EventArgs) Handles CopyToolStripButton2.Click
        showTx(TxEnum.CopyRecord)
    End Sub

    Private Sub UpdateToolStripButton2_Click(sender As Object, e As EventArgs) Handles UpdateToolStripButton2.Click
        showTx(TxEnum.UpdateRecord)
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        UpdateToolStripButton2.PerformClick()
    End Sub

    Private Sub CommitToolStripButton3_Click(sender As Object, e As EventArgs) Handles CommitToolStripButton3.Click
        myController.save()
    End Sub


    Private Sub ToolStripTextBox2_TextChanged(sender As Object, e As EventArgs) Handles ToolStripTextBox2.TextChanged
        Try
            myController.ApplyFilterAll = ToolStripTextBox2.Text
            ProgressReport(1, String.Format("Filter Done. Records {0} ", myController.BS.Count))
        Catch ex As Exception
            ProgressReport(1, String.Format("{0}", ex.Message))
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        ImportData()
    End Sub

    Private Sub ImportData()
        If Not myThread.IsAlive Then
            ToolStripStatusLabel1.Text = ""

            'get filename
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                ImportFile = OpenFileDialog1.FileName
                myThread = New Thread(AddressOf DoImport)
                myThread.Start()
            Else
                ProgressReport(1, "Open file canceled.")
            End If

           
        Else
            MessageBox.Show("Please wait until the current process is finished.")
        End If
    End Sub

    Private Sub DoImport()
        Dim myImport = New ImportITAssets
        If myImport.RunImport(Me, ImportFile, myController) Then
            DoWork()
            ProgressReport(1, "Import Text File Done.")
        End If

    End Sub



    Private Sub DataGridView1_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DataGridView1.DataError

    End Sub

    Private Function CheckSavedData() As Boolean
        Dim myret As Boolean = True
        Try
            Dim DS2 = (myController.GetDataset).GetChanges
            If Not IsNothing(DS2) Then
                Dim myAnswer As Windows.Forms.DialogResult
                myAnswer = MessageBox.Show(String.Format("There is unsaved data in a row {0}Do you want to store to the database?", vbCrLf), "Unsaved data", MessageBoxButtons.YesNoCancel)
                Select Case myAnswer
                    Case Windows.Forms.DialogResult.Cancel
                        myret = False
                    Case Windows.Forms.DialogResult.Yes
                        'MessageBox.Show("Please click Commit Button.")
                        myController.save()
                        myret = True
                    Case Windows.Forms.DialogResult.No
                        myret = True
                End Select
            End If
        Catch ex As Exception

        End Try
        Return myret
    End Function

End Class