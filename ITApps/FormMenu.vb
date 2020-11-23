Imports System.Reflection

Public Class FormMenu

    Private userinfo1 As UserInfo
    Private dbAdapter1 As PostgreSQLDBAdapter
    Dim HasError As Boolean = True

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        Try
            userinfo1 = UserInfo.getInstance
            userinfo1.Userid = Environment.UserDomainName & "\" & Environment.UserName
            userinfo1.computerName = My.Computer.Name
            userinfo1.ApplicationName = "IT Team Apps"
            userinfo1.Username = "N/A"
            userinfo1.isAuthenticate = False
            userinfo1.Role = 0

            dbAdapter1 = PostgreSQLDBAdapter.getInstance
            dbAdapter1.UserInfo = userinfo1
            dbAdapter1.UserInfo.isAdmin = dbAdapter1.IsAdmin(userinfo1.Userid)

            HasError = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub FormMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If HasError Then
            Me.Close()
            Exit Sub
        End If

        Try
            dbAdapter1 = PostgreSQLDBAdapter.getInstance
            loglogin(userinfo1.Userid)
            Me.Text = GetMenuDesc()
            Me.Location = New Point(300, 10)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Me.Close()
        End Try

        'Menu Handles
        MenuHandles()
    End Sub

    Private Sub MenuHandles()
        AddHandler AssetsToolStripMenuItem.Click, AddressOf ToolStripMenuItem_Click
        AddHandler ManufactureToolStripMenuItem.Click, AddressOf ToolStripMenuItem_Click
        AddHandler DepartmentToolStripMenuItem.Click, AddressOf ToolStripMenuItem_Click
        AddHandler LocationToolStripMenuItem.Click, AddressOf ToolStripMenuItem_Click
        AddHandler StatusToolStripMenuItem.Click, AddressOf ToolStripMenuItem_Click
        AddHandler TypeToolStripMenuItem.Click, AddressOf ToolStripMenuItem_Click
        'Admin Function
        MasterToolStripMenuItem.Visible = userinfo1.isAdmin
    End Sub

    Private Sub loglogin(ByVal userid As String)
        Dim applicationname As String = "IT Team Apps"
        Dim username As String = Environment.UserDomainName & "\" & Environment.UserName
        Dim computername As String = My.Computer.Name
        Dim time_stamp As DateTime = Now
        dbAdapter1.loglogin(applicationname, userid, username, computername, time_stamp)
    End Sub

    Public Function GetMenuDesc() As String
        Return "App.Version: " & My.Application.Info.Version.ToString & " :: Server: " & dbAdapter1.HOST & ", Database: " & dbAdapter1.Database & ", Userid: " & dbAdapter1.UserInfo.Userid 'HelperClass1.UserId
    End Function

    Private Sub ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ctrl As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim assembly1 As Assembly = Assembly.GetAssembly(GetType(FormMenu))
        Dim frm As Object = CType(assembly1.CreateInstance(assembly1.GetName.Name.ToString & "." & ctrl.Tag.ToString, True), Form)
        Dim myform = frm.GetInstance        
        myform.show()
        myform.windowstate = FormWindowState.Normal
        myform.activate()
    End Sub

    Private Sub TestToolStripMenuItem_Click(sender As Object, e As EventArgs)
        'Test Parameters Purpose
        MessageBox.Show(dbAdapter1.isAdminParam(userinfo1.Userid))
    End Sub
End Class
