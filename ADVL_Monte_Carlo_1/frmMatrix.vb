Public Class frmMatrix
    'This form displays the data in a matrix.

#Region " Variable Declarations - All the variables used in this form and this application." '=================================================================================================

    'Public myParent As Form 'Used to access parent form methods.

#End Region 'Variable Declarations ------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Properties - All the properties used in this form and this application" '============================================================================================================

    'Private _formNo As Integer = -1 'Multiple instances of this form can be displayed. FormNo is the index number of the form in MatrixList.
    ''If the form is included in Main.MatrixOps.MatrixList() then FormNo will be > -1 --> when exiting set Main.MatrixOps.ClosedFormNo and call Main.MatrixOps.ChartClosed(). 
    'Public Property FormNo As Integer
    '    Get
    '        Return _formNo
    '    End Get
    '    Set(ByVal value As Integer)
    '        _formNo = value
    '    End Set
    'End Property

    Private _hostFormNo As Integer = -1 'The Form Number of the Host Form of this Matrix window.
    Property HostFormNo As Integer
        Get
            Return _hostFormNo
        End Get
        Set(value As Integer)
            _hostFormNo = value
        End Set
    End Property

    Private _itemName As String = "" 'The name of the node containing the matrix data.
    Property ItemName As String
        Get
            Return _itemName
        End Get
        Set(value As String)
            _itemName = value
        End Set
    End Property

    Private _dataName As String = "" 'The name of the matrix data contained in the node.
    Property DataName As String
        Get
            Return _dataName
        End Get
        Set(value As String)
            _dataName = value
        End Set
    End Property

#End Region 'Properties -----------------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Process XML files - Read and write XML files." '=====================================================================================================================================

    Private Sub SaveFormSettings()
        'Save the form settings in an XML document.
        Dim settingsData = <?xml version="1.0" encoding="utf-8"?>
                           <!---->
                           <FormSettings>
                               <Left><%= Me.Left %></Left>
                               <Top><%= Me.Top %></Top>
                               <Width><%= Me.Width %></Width>
                               <Height><%= Me.Height %></Height>
                               <!---->
                           </FormSettings>

        'Add code to include other settings to save after the comment line <!---->

        Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"
        Main.Project.SaveXmlSettings(SettingsFileName, settingsData)
    End Sub

    Private Sub RestoreFormSettings()
        'Read the form settings from an XML document.

        Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"

        If Main.Project.SettingsFileExists(SettingsFileName) Then
            Dim Settings As System.Xml.Linq.XDocument
            Main.Project.ReadXmlSettings(SettingsFileName, Settings)

            If IsNothing(Settings) Then 'There is no Settings XML data.
                Exit Sub
            End If

            'Restore form position and size:
            If Settings.<FormSettings>.<Left>.Value <> Nothing Then Me.Left = Settings.<FormSettings>.<Left>.Value
            If Settings.<FormSettings>.<Top>.Value <> Nothing Then Me.Top = Settings.<FormSettings>.<Top>.Value
            If Settings.<FormSettings>.<Height>.Value <> Nothing Then Me.Height = Settings.<FormSettings>.<Height>.Value
            If Settings.<FormSettings>.<Width>.Value <> Nothing Then Me.Width = Settings.<FormSettings>.<Width>.Value

            'Add code to read other saved setting here:

            CheckFormPos()
        End If
    End Sub

    Private Sub CheckFormPos()
        'Check that the form can be seen on a screen.

        Dim MinWidthVisible As Integer = 192 'Minimum number of X pixels visible. The form will be moved if this many form pixels are not visible.
        Dim MinHeightVisible As Integer = 64 'Minimum number of Y pixels visible. The form will be moved if this many form pixels are not visible.

        Dim FormRect As New Rectangle(Me.Left, Me.Top, Me.Width, Me.Height)
        Dim WARect As Rectangle = Screen.GetWorkingArea(FormRect) 'The Working Area rectangle - the usable area of the screen containing the form.

        'Check if the top of the form is above the top of the Working Area:
        If Me.Top < WARect.Top Then
            Me.Top = WARect.Top
        End If

        'Check if the top of the form is too close to the bottom of the Working Area:
        If (Me.Top + MinHeightVisible) > (WARect.Top + WARect.Height) Then
            Me.Top = WARect.Top + WARect.Height - MinHeightVisible
        End If

        'Check if the left edge of the form is too close to the right edge of the Working Area:
        If (Me.Left + MinWidthVisible) > (WARect.Left + WARect.Width) Then
            Me.Left = WARect.Left + WARect.Width - MinWidthVisible
        End If

        'Check if the right edge of the form is too close to the left edge of the Working Area:
        If (Me.Left + Me.Width - MinWidthVisible) < WARect.Left Then
            Me.Left = WARect.Left - Me.Width + MinWidthVisible
        End If

    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message) 'Save the form settings before the form is minimised:
        If m.Msg = &H112 Then 'SysCommand
            If m.WParam.ToInt32 = &HF020 Then 'Form is being minimised
                'SaveFormSettings()
            End If
        End If
        MyBase.WndProc(m)
    End Sub

#End Region 'Process XML Files ----------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Display Methods - Code used to display this form." '============================================================================================================================

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load
        'The following code is not needed - the form settings are specified using ShowNodeInfo
        'RestoreFormSettings()   'Restore the form settings
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs)
        'Exit the Form
        Me.Close() 'Close the form
    End Sub

    Private Sub Form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If WindowState = FormWindowState.Normal Then
            'The following code is not needed - the form settings are saved using  Main.MatrixOpsList(HostFormNo).SaveOpWindowSettings
            'SaveFormSettings()
        Else
            'Dont save settings if the form is minimised.
        End If
    End Sub


#End Region 'Form Display Methods -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Open and Close Forms - Code used to open and close other forms." '===================================================================================================================

#End Region 'Open and Close Forms -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Methods - The main actions performed by this form." '===========================================================================================================================

    Private Sub btnFormatHelp_Click(sender As Object, e As EventArgs)

    End Sub

    Public Sub ShowNodeInfo(ByRef NodeInfo As frmMatrixOps.MatrixOperationInfo)
        'Display the Node information.

        Me.Top = NodeInfo.Top
        Me.Left = NodeInfo.Left
        Me.Height = NodeInfo.Height
        Me.Width = NodeInfo.Width

        CheckFormPos() 'This ensures the form is visible

        TabControl1.SelectedIndex = NodeInfo.SelectedTab

        txtItemName.Text = ItemName
        txtItemType.Text = NodeInfo.Type
        txtItemStatus.Text = NodeInfo.Status
        txtItemDescription.Text = NodeInfo.Description

    End Sub

    Public Sub ShowMatrixInfo(ByRef DataInfo As MatrixInfo)
        'Display the Matrix information.

        txtMatrixItemName.Text = DataInfo.Name
        txtMatrixItemNRows.Text = DataInfo.NRows
        txtMatrixItemNCols.Text = DataInfo.NCols
        'txtMatrixItemFormat.Text = DataInfo.Format
        txtMatrixItemDescr.Text = DataInfo.Description

        Dim RowNo As Integer
        Dim ColNo As Integer
        dgvMatrixItem.Rows.Clear()
        dgvMatrixItem.RowCount = DataInfo.NRows
        dgvMatrixItem.ColumnCount = DataInfo.NCols
        For RowNo = 0 To DataInfo.NRows - 1
            For ColNo = 0 To DataInfo.NCols - 1
                dgvMatrixItem.Rows(RowNo).Cells(ColNo).Value = DataInfo.Data(RowNo, ColNo)
            Next
        Next
        dgvMatrixItem.DefaultCellStyle.Format = DataInfo.Format
        dgvMatrixItem.AutoResizeColumns()
        dgvMatrixItem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

    End Sub


    Private Sub frmMatrix_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

        Main.MatrixOpsList(HostFormNo).SaveOpWindowSettings(ItemName, Me.Left, Me.Top, Me.Height, Me.Width, TabControl1.SelectedIndex)
        Main.MatrixOpsList(HostFormNo).MatrixWindowClosed(ItemName)

    End Sub

    'Private Sub btnSaveSettings_Click(sender As Object, e As EventArgs) Handles btnSaveSettings.Click
    '    Main.MatrixOpsList(HostFormNo).SaveOpWindowSettings(ItemName, Me.Left, Me.Top, Me.Height, Me.Width, TabControl1.SelectedIndex)
    'End Sub

    'Private Sub btnSaveSettings2_Click(sender As Object, e As EventArgs) Handles btnSaveSettings2.Click
    '    Main.MatrixOpsList(HostFormNo).SaveOpWindowSettings(ItemName, Me.Left, Me.Top, Me.Height, Me.Width, TabControl1.SelectedIndex)
    'End Sub

    'Private Sub frmMatrix_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
    '    Main.MatrixOpsList(HostFormNo).SaveOpWindowSettings(ItemName, Me.Left, Me.Top, Me.Height, Me.Width, TabControl1.SelectedIndex)
    'End Sub

    'Private Sub frmMatrix_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
    '    Main.MatrixOpsList(HostFormNo).SaveOpWindowSettings(ItemName, Me.Left, Me.Top, Me.Height, Me.Width, TabControl1.SelectedIndex)
    'End Sub

    Private Sub frmMatrix_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Main.MatrixOpsList(HostFormNo).SaveOpWindowSettings(ItemName, Me.Left, Me.Top, Me.Height, Me.Width, TabControl1.SelectedIndex)
    End Sub

#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Events - Events that can be triggered by this form." '==========================================================================================================================

#End Region 'Form Events ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
End Class