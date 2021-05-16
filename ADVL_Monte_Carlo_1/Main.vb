'==============================================================================================================================================================================================
'
'Copyright 2021 Signalworks Pty Ltd, ABN 26 066 681 598

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at
'
'http://www.apache.org/licenses/LICENSE-2.0
'
'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
''WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
'See the License for the specific language governing permissions and
'limitations under the License.
'
'----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Imports System.ComponentModel
Imports System.Security.Permissions
<PermissionSet(SecurityAction.Demand, Name:="FullTrust")>
<System.Runtime.InteropServices.ComVisibleAttribute(True)> 'Note: There should be no blank lines between this line and the line: Public Class Main
Public Class Main
    'The ADVL_Monte_Carlo application is used to design, run and analyse Monte Carlo simulations.




#Region " Coding Notes - Notes on the code used in this class." '==============================================================================================================================

    'ADD THE SYSTEM UTILITIES REFERENCE: ==========================================================================================
    'The following references are required by this software: 
    'ADVL_Utilities_Library_1.dll
    'To add the reference, press Project \ Add Reference... 
    '  Select the Browse option then press the Browse button
    '  Find the ADVL_Utilities_Library_1.dll file (it should be located in the directory ...\Projects\ADVL_Utilities_Library_1\ADVL_Utilities_Library_1\bin\Debug\)
    '  Press the Add button. Press the OK button.
    'The Utilities Library is used for Project Management, Archive file management, running XSequence files and running XMessage files.
    'If there are problems with a reference, try deleting it from the references list and adding it again.

    'ADD THE SERVICE REFERENCE: ===================================================================================================
    'A service reference to the Message Service must be added to the source code before this service can be used.
    'This is used to connect to the Application Network.

    'Adding the service reference to a project that includes the Message Service project: -----------------------------------------
    'Project \ Add Service Reference
    'Press the Discover button.
    'Expand the items in the Services window and select IMsgService.
    'Press OK.
    '------------------------------------------------------------------------------------------------------------------------------
    '------------------------------------------------------------------------------------------------------------------------------
    'Adding the service reference to other projects that dont include the Message Service project: -------------------------------
    'Run the ADVL_Network_1 application to start the message service.
    'In Microsoft Visual Studio select: Project \ Add Service Reference
    'Enter the address: http://localhost:8734/ADVLService
    'Press the Go button.
    'MsgService is found.
    'Press OK to add ServiceReference1 to the project.
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'ADD THE MsgServiceCallback CODE: =============================================================================================
    'This is used to connect to the Application Network.
    'In Microsoft Visual Studio select: Project \ Add Class
    'MsgServiceCallback.vb
    'Add the following code to the class:
    'Imports System.ServiceModel
    'Public Class MsgServiceCallback
    '    Implements ServiceReference1.IMsgServiceCallback
    '    Public Sub OnSendMessage(message As String) Implements ServiceReference1.IMsgServiceCallback.OnSendMessage
    '        'A message has been received.
    '        'Set the InstrReceived property value to the message (usually in XMessage format). This will also apply the instructions in the XMessage.
    '        Main.InstrReceived = message
    '    End Sub
    'End Class
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'DEBUGGING TIPS:
    '1. If an application based on the Application Template does not initially run correctly,
    '    check that the copied methods, such as Main_Load, have the correct Handles statement.
    '    For example: the Main_Load method should have the following declaration: Private Sub Main_Load(sender As Object, e As EventArgs) Handles Me.Load
    '      It will not run when the application loads, with this declaration:      Private Sub Main_Load(sender As Object, e As EventArgs)
    '    For example: the Main_FormClosing method should have the following declaration: Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
    '      It will not run when the application closes, with this declaration:     Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs)
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'ADD THE Timer1 Control to the Main Form: =====================================================================================
    'Select the Main.vb [Design] tab.
    'Press Toolbox \ Components \ Timer and add Timer1 to the Main form.
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'EDIT THE DefaultAppProperties() CODE: ========================================================================================
    'This sets the Application properties that are stored in the Application_Info_ADVL_2.xml settings file.
    'The following properties need to be updated:
    '  ApplicationInfo.Name
    '  ApplicationInfo.Description
    '  ApplicationInfo.CreationDate
    '  ApplicationInfo.Author
    '  ApplicationInfo.Copyright
    '  ApplicationInfo.Trademarks
    '  ApplicationInfo.License
    '  ApplicationInfo.SourceCode          (Optional - Preliminary implemetation coded.)
    '  ApplicationInfo.ModificationSummary (Optional - Preliminary implemetation coded.)
    '  ApplicationInfo.Libraries           (Optional - Preliminary implemetation coded.)
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'ADD THE Application Icon: ====================================================================================================
    'Double-click My Project in the Solution Explorer window to open the project tab.
    'In the Application section press the Icon box and select Browse.
    'Select an application icon.
    'This icon can also be selected for the Main form icon by editing the properties of this form.
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'EDIT THE Application Info Text: ==============================================================================================
    'The Application Info Text is used to label the application icon in the Application Network tree view.
    'This is edited in the SendApplicationInfo() method of the Main form.
    'Edit the line of code: Dim text As New XElement("Text", "Application Template").
    'Replace the default text "Application Template" with the required text.
    'Note that this text can be updated at any time and when the updated executable is run, it will update the Application Network tree view the next time it is connected.
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'Calling JavaScript from VB.NET:
    'The following Imports statement and permissions are required for the Main form:
    'Imports System.Security.Permissions
    '<PermissionSet(SecurityAction.Demand, Name:="FullTrust")> _
    '<System.Runtime.InteropServices.ComVisibleAttribute(True)> _
    'NOTE: the line continuation characters (_) will disappear form the code view after they have been typed!
    '------------------------------------------------------------------------------------------------------------------------------
    'Calling VB.NET from JavaScript
    'Add the following line to the Main.Load method:
    '  Me.WebBrowser1.ObjectForScripting = Me
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'Adding a Context Menu Strip:
    'In Visual Studio select the tab Main.vb [Design]
    'Select Toolbox \ Menus & Toolbars \ ContextMenuStrip and add it to the form. ContextMenuStrip1 appears in the panel below the form.
    'Right-click ContextMenuStrip1 and select Edit Items...
    'Press Add to add a new menu item
    '  Add item: Name: ToolStripMenuItem1_EditWorkflowTabPage         Text: Edit Workflow Tab Page (Edit the name and text on the right half of the Items Collection Editor.)
    '  Add item: Name: ToolStripMenuItem1_ShowStartPageInWorkflowTab  Text: Show Start Page In Workflow Tab
    'Select the Workflows button on the main form and select ContectMenuStrip property = ContextMenuStrip1
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'Edit the AppInfoHtmlString function to display the appropriate information about the application.
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'The ADVL_Network_1 application should be running the first time the new application is run.
    'The Network application will automatically send its executable file location to the new application.
    'This will allow the new application to start the Network when required.
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'Other code edits:
    '  Main.Load - Message.AddText("------------------- Starting Application: ADVL Application Template ----------------- " & vbCrLf, "Heading")
    '  Private Sub SendApplicationInfo() - Dim text As New XElement("Text", "Application Template")
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'To use MathNet.Numerics:
    '  Install the .nupkg file in a Visual Studio project:
    '    Right-click References in the Solution Explorer window.
    '      Select Manage NuGet Packages...
    '  In the NuGet tab, select Browse and search for MathNet
    '  Select MathNet.Numerics and press the Install button.
    '------------------------------------------------------------------------------------------------------------------------------
    '


#End Region 'Coding Notes ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Variable Declarations - All the variables and class objects used in this form and this application." '===============================================================================

    Public WithEvents ApplicationInfo As New ADVL_Utilities_Library_1.ApplicationInfo 'This object is used to store application information.
    Public WithEvents Project As New ADVL_Utilities_Library_1.Project 'This object is used to store Project information.
    Public WithEvents Message As New ADVL_Utilities_Library_1.Message 'This object is used to display messages in the Messages window.
    Public WithEvents ApplicationUsage As New ADVL_Utilities_Library_1.Usage 'This object stores application usage information.

    'Declare Forms used by the application:
    Public WithEvents TemplateForm As frmTemplate
    Public WithEvents WebPageList As frmWebPageList
    Public WithEvents ProjectArchive As frmArchive 'Form used to view the files in a Project archive
    Public WithEvents SettingsArchive As frmArchive 'Form used to view the files in a Settings archive
    Public WithEvents DataArchive As frmArchive 'Form used to view the files in a Data archive
    Public WithEvents SystemArchive As frmArchive 'Form used to view the files in a System archive

    Public WithEvents NewHtmlDisplay As frmHtmlDisplay
    Public HtmlDisplayFormList As New ArrayList 'Used for displaying multiple HtmlDisplay forms.

    Public WithEvents NewWebPage As frmWebPage
    Public WebPageFormList As New ArrayList 'Used for displaying multiple WebView forms.

    Public WithEvents Chart As frmChart
    Public ChartList As New ArrayList 'Used for displaying multiple Chart forms.

    Public WithEvents Table As frmTable
    Public TableList As New ArrayList 'Used for displaying multiple Table forms

    Public WithEvents DataInfo As frmDataInfo 'Used for displaying information about a dataset or collection of datasets.
    Public DataInfoList As New ArrayList 'Used for displaying multiple DataInfo forms.

    Public WithEvents DistribChart As frmDistribChart
    Public DistribChartFormList As New ArrayList 'Used for displaying multiple Distribution Chart forms.

    Public WithEvents MatrixOps As frmMatrixOps
    Public MatrixOpsList As New ArrayList 'Used for displaying multiple MatrixOps forms.

    Public WithEvents SeriesAnalysis As frmSeriesAnalysis
    Public SeriesAnalysisList As New ArrayList 'Used for displaying multiple Series Analysis forms.

    'Declare objects used to connect to the Message Service:
    Public client As ServiceReference1.MsgServiceClient
    Public WithEvents XMsg As New ADVL_Utilities_Library_1.XMessage
    Dim XDoc As New System.Xml.XmlDocument
    Public Status As New System.Collections.Specialized.StringCollection
    Dim ClientProNetName As String = "" 'The name of the client Project Network requesting service. 
    Dim ClientAppName As String = "" 'The name of the client requesting service
    Dim ClientConnName As String = "" 'The name of the client connection requesting service
    Dim MessageXDoc As System.Xml.Linq.XDocument
    Dim xmessage As XElement 'This will contain the message. It will be added to MessageXDoc.
    Dim xlocns As New List(Of XElement) 'A list of locations. Each location forms part of the reply message. The information in the reply message will be sent to the specified location in the client application.
    Dim MessageText As String = "" 'The text of a message sent through the Application Network.

    Public OnCompletionInstruction As String = "Stop" 'The last instruction returned on completion of the processing of an XMessage.
    Public EndInstruction As String = "Stop" 'Another method of specifying the last instruction. This is processed in the EndOfSequence section of XMsg.Instructions.

    Public ConnectionName As String = "" 'The name of the connection used to connect this application to the ComNet (Message Service).

    Public ProNetName As String = "" 'The name of the Project Network
    Public ProNetPath As String = "" 'The path of the Project Network

    Public AdvlNetworkAppPath As String = "" 'The application path of the ADVL Network application (ComNet). This is where the "Application.Lock" file will be while ComNet is running
    Public AdvlNetworkExePath As String = "" 'The executable path of the ADVL Network.

    'Variable for local processing of an XMessage:
    Public WithEvents XMsgLocal As New ADVL_Utilities_Library_1.XMessage
    Dim XDocLocal As New System.Xml.XmlDocument
    Public StatusLocal As New System.Collections.Specialized.StringCollection

    'Main.Load variables:
    Dim ProjectSelected As Boolean = False 'If True, a project has been selected using Command Arguments. Used in Main.Load.
    Dim StartupConnectionName As String = "" 'If not "" the application will be connected to the ComNet using this connection name in  Main.Load.

    'The following variables are used to run JavaScript in Web Pages loaded into the Document View: -------------------
    Public WithEvents XSeq As New ADVL_Utilities_Library_1.XSequence
    'To run an XSequence:
    '  XSeq.RunXSequence(xDoc, Status) 'ImportStatus in Import
    '    Handle events:
    '      XSeq.ErrorMsg
    '      XSeq.Instruction(Info, Locn)

    Private XStatus As New System.Collections.Specialized.StringCollection

    'Variables used to restore Item values on a web page.
    Private FormName As String
    Private ItemName As String
    Private SelectId As String

    'StartProject variables:
    Private StartProject_AppName As String  'The application name
    Private StartProject_ConnName As String 'The connection name
    Private StartProject_ProjID As String   'The project ID
    Private StartProject_ProjName As String ' The project name

    Private WithEvents bgwComCheck As New System.ComponentModel.BackgroundWorker 'Used to perform communication checks on a separate thread.

    Public WithEvents bgwSendMessage As New System.ComponentModel.BackgroundWorker 'Used to send a message through the Message Service.
    Dim SendMessageParams As New clsSendMessageParams 'This holds the Send Message parameters: .ProjectNetworkName, .ConnectionName & .Message

    'Alternative SendMessage background worker - needed to send a message while instructions are being processed.
    Public WithEvents bgwSendMessageAlt As New System.ComponentModel.BackgroundWorker 'Used to send a message through the Message Service - alternative backgound worker.
    Dim SendMessageParamsAlt As New clsSendMessageParams 'This hold the Send Message parameters: .ProjectNetworkName, .ConnectionName & .Message - for the alternative background worker.

    Public WithEvents bgwRunInstruction As New System.ComponentModel.BackgroundWorker 'Used to run a single instruction
    Dim InstructionParams As New clsInstructionParams 'This holds the Info and Locn parameters of an instruction.

    Public WithEvents MonteCarlo As New clsMonteCarlo 'Object used to store the Monte Carlo model and run a simulation.
    Dim cboCorrVariables As New DataGridViewComboBoxColumn 'Used to select Random Variables in a correlation matrix.
    Dim cboDestTable As New DataGridViewComboBoxColumn 'Used to select the Destination Table for a new Random Variable
    Dim ColList() As String 'Column list used to copy columns.

    Public MatrixClipboard As New MatrixClipboard 'Stores Matrix information and contains methods for Matrix copy and paste operations.

    'Variables used in a Calculation Sequence.
    Public CalcInfo As New Dictionary(Of String, CalcOpInfo) 'Dictionary of Matrix Operation Information
    Dim ScalarData As New Dictionary(Of String, Double) 'Dictionary of Scalar data
    Public ColumnInfo As New Dictionary(Of String, DataColumnInfo) 'Dictionary of Column Names corresponding to Input/Output Data Names. Column Name = ColumnInfo(VariableName).Name, Column Type = ColumnInfo(VariableName).Type

    'Variables used for editing nodes in the trvCalculations tree view:
    Dim CutNode As TreeNode 'Used for cutting and pasting nodes
    Dim SelNode As TreeNode 'The node selected on trvMatrixOps
    Dim SelItemName As String = "" 'The name of the item selected on trvCalculations
    Dim SelDataName As String = "" 'The name of the data corresponding to the item selected. This will be the same as the SelItemName unless it is a Scalar Copy or Matrix Copy node.



#End Region 'Variable Declarations ------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Properties - All the properties used in this form and this application" '============================================================================================================

    Private _connectionHashcode As Integer 'The Message Service connection hashcode. This is used to identify a connection in the Message Service when reconnecting.
    Property ConnectionHashcode As Integer
        Get
            Return _connectionHashcode
        End Get
        Set(value As Integer)
            _connectionHashcode = value
        End Set
    End Property

    Private _connectedToComNet As Boolean = False  'True if the application is connected to the Communication Network (Message Service).
    Property ConnectedToComNet As Boolean
        Get
            Return _connectedToComNet
        End Get
        Set(value As Boolean)
            _connectedToComNet = value
        End Set
    End Property

    Private _instrReceived As String = "" 'Contains Instructions received via the Message Service.
    Property InstrReceived As String
        Get
            Return _instrReceived
        End Get
        Set(value As String)
            If value = Nothing Then
                Message.Add("Empty message received!")
            Else
                _instrReceived = value
                ProcessInstructions(_instrReceived)
            End If
        End Set
    End Property

    Private Sub ProcessInstructions(ByVal Instructions As String)
        'Process the XMessage instructions.

        Dim MsgType As String
        If Instructions.StartsWith("<XMsg>") Then
            MsgType = "XMsg"
            If ShowXMessages Then
                'Add the message header to the XMessages window:
                Message.XAddText("Message received: " & vbCrLf, "XmlReceivedNotice")
            End If
        ElseIf Instructions.StartsWith("<XSys>") Then
            MsgType = "XSys"
            If ShowSysMessages Then
                'Add the message header to the XMessages window:
                Message.XAddText("System Message received: " & vbCrLf, "XmlReceivedNotice")
            End If
        Else
            MsgType = "Unknown"
        End If

        If MsgType = "XMsg" Or MsgType = "XSys" Then 'This is an XMessage or XSystem set of instructions.
            Try
                'Inititalise the reply message:
                ClientProNetName = ""
                ClientConnName = ""
                ClientAppName = ""
                xlocns.Clear() 'Clear the list of locations in the reply message. 
                Dim Decl As New XDeclaration("1.0", "utf-8", "yes")
                MessageXDoc = New XDocument(Decl, Nothing) 'Reply message - this will be sent to the Client App.
                xmessage = New XElement(MsgType)
                xlocns.Add(New XElement("Main")) 'Initially set the location in the Client App to Main.

                'Run the received message:
                Dim XmlHeader As String = "<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>"
                XDoc.LoadXml(XmlHeader & vbCrLf & Instructions.Replace("&", "&amp;")) 'Replace "&" with "&amp:" before loading the XML text.

                If (MsgType = "XMsg") And ShowXMessages Then
                    Message.XAddXml(XDoc)  'Add the message to the XMessages window.
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                ElseIf (MsgType = "XSys") And ShowSysMessages Then
                    Message.XAddXml(XDoc)  'Add the message to the XMessages window.
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                End If

                XMsg.Run(XDoc, Status)
            Catch ex As Exception
                Message.Add("Error running XMsg: " & ex.Message & vbCrLf)
            End Try

            'XMessage has been run.
            'Reply to this message:
            'Add the message reply to the XMessages window:
            'Complete the MessageXDoc:
            xmessage.Add(xlocns(xlocns.Count - 1)) 'Add the last location reply instructions to the message.
            MessageXDoc.Add(xmessage)
            MessageText = MessageXDoc.ToString

            If ClientConnName = "" Then
                'No client to send a message to - process the message locally.

                If (MsgType = "XMsg") And ShowXMessages Then
                    Message.XAddText("Message processed locally:" & vbCrLf, "XmlSentNotice")
                    Message.XAddXml(MessageText)
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                ElseIf (MsgType = "XSys") And ShowSysMessages Then
                    Message.XAddText("System Message processed locally:" & vbCrLf, "XmlSentNotice")
                    Message.XAddXml(MessageText)
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                End If
                ProcessLocalInstructions(MessageText)
            Else
                If (MsgType = "XMsg") And ShowXMessages Then
                    Message.XAddText("Message sent to [" & ClientProNetName & "]." & ClientConnName & ":" & vbCrLf, "XmlSentNotice")   'NOTE: There is no SendMessage code in the Message Service application!
                    Message.XAddXml(MessageText)
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                ElseIf (MsgType = "XSys") And ShowSysMessages Then
                    Message.XAddText("System Message sent to [" & ClientProNetName & "]." & ClientConnName & ":" & vbCrLf, "XmlSentNotice")   'NOTE: There is no SendMessage code in the Message Service application!
                    Message.XAddXml(MessageText)
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                End If

                'Send Message on a new thread:
                SendMessageParams.ProjectNetworkName = ClientProNetName
                SendMessageParams.ConnectionName = ClientConnName
                SendMessageParams.Message = MessageText
                If bgwSendMessage.IsBusy Then
                    Message.AddWarning("Send Message backgroundworker is busy." & vbCrLf)
                Else
                    bgwSendMessage.RunWorkerAsync(SendMessageParams)
                End If
            End If

        Else 'This is not an XMessage!
            If Instructions.StartsWith("<XMsgBlk>") Then 'This is an XMessageBlock.
                'Process the received message:
                Dim XmlHeader As String = "<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>"
                XDoc.LoadXml(XmlHeader & vbCrLf & Instructions.Replace("&", "&amp;")) 'Replace "&" with "&amp:" before loading the XML text.
                If ShowXMessages Then
                    Message.XAddXml(XDoc)   'Add the message to the XMessages window.
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                End If

                'Process the XMessageBlock:
                Dim XMsgBlkLocn As String
                XMsgBlkLocn = XDoc.GetElementsByTagName("ClientLocn")(0).InnerText
                Select Case XMsgBlkLocn
                    Case "TestLocn" 'Replace this with the required location name.
                        Dim XInfo As Xml.XmlNodeList = XDoc.GetElementsByTagName("XInfo") 'Get the XInfo node list
                        Dim InfoXDoc As New Xml.Linq.XDocument 'Create an XDocument to hold the information contained in XInfo 
                        InfoXDoc = XDocument.Parse("<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>" & vbCrLf & XInfo(0).InnerXml) 'Read the information into InfoXDoc
                        'Add processing instructions here 
                        ' The information in the InfoXDoc is usually sent to an XDocument in the application or stored as an XML file in the project.

                    Case Else
                        Message.AddWarning("Unknown XInfo Message location: " & XMsgBlkLocn & vbCrLf)
                End Select
            Else
                Message.XAddText("The message is not an XMessage or XMessageBlock: " & vbCrLf & Instructions & vbCrLf & vbCrLf, "Normal")
            End If
        End If
    End Sub

    Private Sub ProcessLocalInstructions(ByVal Instructions As String)
        'Process the XMessage instructions locally.

        If Instructions.StartsWith("<XMsg>") Or Instructions.StartsWith("<XSys>") Then 'This is an XMessage set of instructions.
            'Run the received message:
            Dim XmlHeader As String = "<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>"
            XDocLocal.LoadXml(XmlHeader & vbCrLf & Instructions)
            XMsgLocal.Run(XDocLocal, StatusLocal)
        Else 'This is not an XMessage!
            Message.XAddText("The message is not an XMessage: " & Instructions & vbCrLf, "Normal")
        End If
    End Sub

    Private _showXMessages As Boolean = True 'If True, XMessages that are sent or received will be shown in the Messages window.
    Property ShowXMessages As Boolean
        Get
            Return _showXMessages
        End Get
        Set(value As Boolean)
            _showXMessages = value
        End Set
    End Property

    Private _showSysMessages As Boolean = True 'If True, System messages that are sent or received will be shown in the messages window.
    Property ShowSysMessages As Boolean
        Get
            Return _showSysMessages
        End Get
        Set(value As Boolean)
            _showSysMessages = value
        End Set
    End Property

    Private _closedFormNo As Integer 'Temporarily holds the number of the form that is being closed. 
    Property ClosedFormNo As Integer
        Get
            Return _closedFormNo
        End Get
        Set(value As Integer)
            _closedFormNo = value
        End Set
    End Property

    Private _workflowFileName As String = "" 'The file name of the html document displayed in the Workflow tab.
    Public Property WorkflowFileName As String
        Get
            Return _workflowFileName
        End Get
        Set(value As String)
            _workflowFileName = value
        End Set
    End Property


    Private _mCTableName As String = ""
    Property MCTableName As String
        Get
            Return _mCTableName
        End Get
        Set(value As String)
            _mCTableName = value

            UpdateMCDataTableView()
            'cmbMCTableName.SelectedIndex = cmbMCTableName.FindStringExact(_mCTableName)
        End Set
    End Property

    Private _calcSeqModified As Boolean = False 'If True the Monte Carlo Calculation Sequence has been modified - save it before exiting
    Property CalcSeqModified As Boolean
        Get
            Return _calcSeqModified
        End Get
        Set(value As Boolean)
            _calcSeqModified = value
        End Set
    End Property

    Private _trialNo As Integer = 1 'The trial number in a Monte Carlo model
    Property TrialNo As Integer
        Get
            Return _trialNo
        End Get
        Set(value As Integer)
            If value < 1 Then
                _trialNo = 1
            ElseIf TrialNo > MonteCarlo.NTrials Then
                _trialNo = MonteCarlo.NTrials
            Else
                _trialNo = value
            End If
            txtTrialNo.Text = _trialNo
        End Set
    End Property




#End Region 'Properties -----------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Process XML Files - Read and write XML files." '=====================================================================================================================================

    Private Sub SaveFormSettings()
        'Save the form settings in an XML document.
        Dim settingsData = <?xml version="1.0" encoding="utf-8"?>
                           <!---->
                           <!--Form settings for Main form.-->
                           <FormSettings>
                               <Left><%= Me.Left %></Left>
                               <Top><%= Me.Top %></Top>
                               <Width><%= Me.Width %></Width>
                               <Height><%= Me.Height %></Height>
                               <AdvlNetworkAppPath><%= AdvlNetworkAppPath %></AdvlNetworkAppPath>
                               <AdvlNetworkExePath><%= AdvlNetworkExePath %></AdvlNetworkExePath>
                               <ShowXMessages><%= ShowXMessages %></ShowXMessages>
                               <ShowSysMessages><%= ShowSysMessages %></ShowSysMessages>
                               <!---->
                               <SelectedTabIndex><%= TabControl1.SelectedIndex %></SelectedTabIndex>
                               <!---->
                               <MonteCarloFileName><%= MonteCarlo.FileName %></MonteCarloFileName>
                               <SaveMonteCarloData><%= chkSaveMCData.Checked %></SaveMonteCarloData>
                           </FormSettings>

        'Add code to include other settings to save after the comment line <!---->

        Dim SettingsFileName As String = "FormSettings_" & ApplicationInfo.Name & " - Main.xml"
        Project.SaveXmlSettings(SettingsFileName, settingsData)
    End Sub

    Private Sub RestoreFormSettings()
        'Read the form settings from an XML document.

        Dim SettingsFileName As String = "FormSettings_" & ApplicationInfo.Name & " - Main.xml"

        If Project.SettingsFileExists(SettingsFileName) Then
            Dim Settings As System.Xml.Linq.XDocument
            Project.ReadXmlSettings(SettingsFileName, Settings)

            If IsNothing(Settings) Then 'There is no Settings XML data.
                Exit Sub
            End If

            'Restore form position and size:
            If Settings.<FormSettings>.<Left>.Value <> Nothing Then Me.Left = Settings.<FormSettings>.<Left>.Value
            If Settings.<FormSettings>.<Top>.Value <> Nothing Then Me.Top = Settings.<FormSettings>.<Top>.Value
            If Settings.<FormSettings>.<Height>.Value <> Nothing Then Me.Height = Settings.<FormSettings>.<Height>.Value
            If Settings.<FormSettings>.<Width>.Value <> Nothing Then Me.Width = Settings.<FormSettings>.<Width>.Value

            If Settings.<FormSettings>.<AdvlNetworkAppPath>.Value <> Nothing Then AdvlNetworkAppPath = Settings.<FormSettings>.<AdvlNetworkAppPath>.Value
            If Settings.<FormSettings>.<AdvlNetworkExePath>.Value <> Nothing Then AdvlNetworkExePath = Settings.<FormSettings>.<AdvlNetworkExePath>.Value

            If Settings.<FormSettings>.<ShowXMessages>.Value <> Nothing Then ShowXMessages = Settings.<FormSettings>.<ShowXMessages>.Value
            If Settings.<FormSettings>.<ShowSysMessages>.Value <> Nothing Then ShowSysMessages = Settings.<FormSettings>.<ShowSysMessages>.Value

            'Add code to read other saved setting here:
            If Settings.<FormSettings>.<SelectedTabIndex>.Value <> Nothing Then TabControl1.SelectedIndex = Settings.<FormSettings>.<SelectedTabIndex>.Value

            If Settings.<FormSettings>.<SaveMonteCarloData>.Value <> Nothing Then chkSaveMCData.Checked = Settings.<FormSettings>.<SaveMonteCarloData>.Value

            'Restore Monte Carlo file:
            If Settings.<FormSettings>.<MonteCarloFileName>.Value <> Nothing Then OpenMCModel(Settings.<FormSettings>.<MonteCarloFileName>.Value)



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

    Private Sub ReadApplicationInfo()
        'Read the Application Information.

        If ApplicationInfo.FileExists Then
            ApplicationInfo.ReadFile()
        Else
            'There is no Application_Info_ADVL_2.xml file.
            DefaultAppProperties() 'Create a new Application Info file with default application properties.
            ApplicationInfo.WriteFile() 'Write the file now. The file information may be used by other applications.
        End If
    End Sub

    Private Sub DefaultAppProperties()
        'These properties will be saved in the Application_Info.xml file in the application directory.
        'If this file is deleted, it will be re-created using these default application properties.

        'Change this to show your application Name, Description and Creation Date.
        ApplicationInfo.Name = "ADVL_Monte_Carlo_1"

        'ApplicationInfo.ApplicationDir is set when the application is started.
        ApplicationInfo.ExecutablePath = Application.ExecutablePath

        ApplicationInfo.Description = "The Monte Carlo application is used to design, run and analyse Monte Carlo simulations."
        ApplicationInfo.CreationDate = "20-Jan-2021 12:00:00"

        'Author -----------------------------------------------------------------------------------------------------------
        'Change this to show your Name, Description and Contact information.
        ApplicationInfo.Author.Name = "Signalworks Pty Ltd"
        ApplicationInfo.Author.Description = "Signalworks Pty Ltd" & vbCrLf &
            "Australian Proprietary Company" & vbCrLf &
            "ABN 26 066 681 598" & vbCrLf &
            "Registration Date 05/10/1994"

        ApplicationInfo.Author.Contact = "http://www.andorville.com.au/"

        'File Associations: -----------------------------------------------------------------------------------------------
        'Add any file associations here.
        'The file extension and a description of files that can be opened by this application are specified.
        'The example below specifies a coordinate system parameter file type with the file extension .ADVLCoord.
        'Dim Assn1 As New ADVL_System_Utilities.FileAssociation
        'Assn1.Extension = "ADVLCoord"
        'Assn1.Description = "Andorville™ software coordinate system parameter file"
        'ApplicationInfo.FileAssociations.Add(Assn1)

        'Version ----------------------------------------------------------------------------------------------------------
        ApplicationInfo.Version.Major = My.Application.Info.Version.Major
        ApplicationInfo.Version.Minor = My.Application.Info.Version.Minor
        ApplicationInfo.Version.Build = My.Application.Info.Version.Build
        ApplicationInfo.Version.Revision = My.Application.Info.Version.Revision

        'Copyright --------------------------------------------------------------------------------------------------------
        'Add your copyright information here.
        ApplicationInfo.Copyright.OwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        ApplicationInfo.Copyright.PublicationYear = "2021"

        'Trademarks -------------------------------------------------------------------------------------------------------
        'Add your trademark information here.
        Dim Trademark1 As New ADVL_Utilities_Library_1.Trademark
        Trademark1.OwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        Trademark1.Text = "Andorville"
        Trademark1.Registered = False
        Trademark1.GenericTerm = "software"
        ApplicationInfo.Trademarks.Add(Trademark1)
        Dim Trademark2 As New ADVL_Utilities_Library_1.Trademark
        Trademark2.OwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        Trademark2.Text = "AL-H7"
        Trademark2.Registered = False
        Trademark2.GenericTerm = "software"
        ApplicationInfo.Trademarks.Add(Trademark2)
        Dim Trademark3 As New ADVL_Utilities_Library_1.Trademark
        Trademark3.OwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        Trademark3.Text = "AL-M7"
        Trademark3.Registered = False
        Trademark3.GenericTerm = "software"
        ApplicationInfo.Trademarks.Add(Trademark3)
        Dim Trademark4 As New ADVL_Utilities_Library_1.Trademark
        Trademark4.OwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        Trademark4.Text = "AL-S7"
        Trademark4.Registered = False
        Trademark4.GenericTerm = "software"
        ApplicationInfo.Trademarks.Add(Trademark4)
        Dim Trademark5 As New ADVL_Utilities_Library_1.Trademark
        Trademark5.OwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        Trademark5.Text = "AL-Q7"
        Trademark5.Registered = False
        Trademark5.GenericTerm = "software"
        ApplicationInfo.Trademarks.Add(Trademark5)

        'License -------------------------------------------------------------------------------------------------------
        'Add your license information here.
        ApplicationInfo.License.CopyrightOwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        ApplicationInfo.License.PublicationYear = "2021"

        'License Links:
        'http://choosealicense.com/
        'http://www.apache.org/licenses/
        'http://opensource.org/

        'Apache License 2.0 ---------------------------------------------
        ApplicationInfo.License.Code = ADVL_Utilities_Library_1.License.Codes.Apache_License_2_0
        ApplicationInfo.License.Notice = ApplicationInfo.License.ApacheLicenseNotice 'Get the pre-defined Aapche license notice.
        ApplicationInfo.License.Text = ApplicationInfo.License.ApacheLicenseText     'Get the pre-defined Apache license text.

        'Code to use other pre-defined license types is shown below:

        'GNU General Public License, version 3 --------------------------
        'ApplicationInfo.License.Type = ADVL_Utilities_Library_1.License.Types.GNU_GPL_V3_0
        'ApplicationInfo.License.Notice = 'Add the License Notice to ADVL_Utilities_Library_1 License class.
        'ApplicationInfo.License.Text = 'Add the License Text to ADVL_Utilities_Library_1 License class.

        'The MIT License ------------------------------------------------
        'ApplicationInfo.License.Type = ADVL_Utilities_Library_1.License.Types.MIT_License
        'ApplicationInfo.License.Notice = ApplicationInfo.License.MITLicenseNotice
        'ApplicationInfo.License.Text = ApplicationInfo.License.MITLicenseText

        'No License Specified -------------------------------------------
        'ApplicationInfo.License.Type = ADVL_Utilities_Library_1.License.Types.None
        'ApplicationInfo.License.Notice = ""
        'ApplicationInfo.License.Text = ""

        'The Unlicense --------------------------------------------------
        'ApplicationInfo.License.Type = ADVL_Utilities_Library_1.License.Types.The_Unlicense
        'ApplicationInfo.License.Notice = ApplicationInfo.License.UnLicenseNotice
        'ApplicationInfo.License.Text = ApplicationInfo.License.UnLicenseText

        'Unknown License ------------------------------------------------
        'ApplicationInfo.License.Type = ADVL_Utilities_Library_1.License.Types.Unknown
        'ApplicationInfo.License.Notice = ""
        'ApplicationInfo.License.Text = ""

        'Source Code: --------------------------------------------------------------------------------------------------
        'Add your source code information here if required.
        'THIS SECTION WILL BE UPDATED TO ALLOW A GITHUB LINK.
        ApplicationInfo.SourceCode.Language = "Visual Basic 2015"
        ApplicationInfo.SourceCode.FileName = ""
        ApplicationInfo.SourceCode.FileSize = 0
        ApplicationInfo.SourceCode.FileHash = ""
        ApplicationInfo.SourceCode.WebLink = ""
        ApplicationInfo.SourceCode.Contact = ""
        ApplicationInfo.SourceCode.Comments = ""

        'ModificationSummary: -----------------------------------------------------------------------------------------
        'Add any source code modification here is required.
        ApplicationInfo.ModificationSummary.BaseCodeName = ""
        ApplicationInfo.ModificationSummary.BaseCodeDescription = ""
        ApplicationInfo.ModificationSummary.BaseCodeVersion.Major = 0
        ApplicationInfo.ModificationSummary.BaseCodeVersion.Minor = 0
        ApplicationInfo.ModificationSummary.BaseCodeVersion.Build = 0
        ApplicationInfo.ModificationSummary.BaseCodeVersion.Revision = 0
        ApplicationInfo.ModificationSummary.Description = "This is the first released version of the application. No earlier base code used."

        'Library List: ------------------------------------------------------------------------------------------------
        'Add the ADVL_Utilties_Library_1 library:
        Dim NewLib As New ADVL_Utilities_Library_1.LibrarySummary
        NewLib.Name = "ADVL_System_Utilities"
        NewLib.Description = "System Utility classes used in Andorville™ software development system applications"
        NewLib.CreationDate = "7-Jan-2016 12:00:00"
        NewLib.LicenseNotice = "Copyright 2016 Signalworks Pty Ltd, ABN 26 066 681 598" & vbCrLf &
                               vbCrLf &
                               "Licensed under the Apache License, Version 2.0 (the ""License"");" & vbCrLf &
                               "you may not use this file except in compliance with the License." & vbCrLf &
                               "You may obtain a copy of the License at" & vbCrLf &
                               vbCrLf &
                               "http://www.apache.org/licenses/LICENSE-2.0" & vbCrLf &
                               vbCrLf &
                               "Unless required by applicable law or agreed to in writing, software" & vbCrLf &
                               "distributed under the License is distributed on an ""AS IS"" BASIS," & vbCrLf &
                               "WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied." & vbCrLf &
                               "See the License for the specific language governing permissions and" & vbCrLf &
                               "limitations under the License." & vbCrLf

        NewLib.CopyrightNotice = "Copyright 2016 Signalworks Pty Ltd, ABN 26 066 681 598"

        NewLib.Version.Major = 1
        NewLib.Version.Minor = 0
        'NewLib.Version.Build = 1
        NewLib.Version.Build = 0
        NewLib.Version.Revision = 0

        NewLib.Author.Name = "Signalworks Pty Ltd"
        NewLib.Author.Description = "Signalworks Pty Ltd" & vbCrLf &
            "Australian Proprietary Company" & vbCrLf &
            "ABN 26 066 681 598" & vbCrLf &
            "Registration Date 05/10/1994"

        NewLib.Author.Contact = "http://www.andorville.com.au/"

        Dim NewClass1 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass1.Name = "ZipComp"
        NewClass1.Description = "The ZipComp class is used to compress files into and extract files from a zip file."
        NewLib.Classes.Add(NewClass1)
        Dim NewClass2 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass2.Name = "XSequence"
        NewClass2.Description = "The XSequence class is used to run an XML property sequence (XSequence) file. XSequence files are used to record and replay processing sequences in Andorville™ software applications."
        NewLib.Classes.Add(NewClass2)
        Dim NewClass3 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass3.Name = "XMessage"
        NewClass3.Description = "The XMessage class is used to read an XML Message (XMessage). An XMessage is a simplified XSequence used to exchange information between Andorville™ software applications."
        NewLib.Classes.Add(NewClass3)
        Dim NewClass4 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass4.Name = "Location"
        NewClass4.Description = "The Location class consists of properties and methods to store data in a location, which is either a directory or archive file."
        NewLib.Classes.Add(NewClass4)
        Dim NewClass5 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass5.Name = "Project"
        NewClass5.Description = "An Andorville™ software application can store data within one or more projects. Each project stores a set of related data files. The Project class contains properties and methods used to manage a project."
        NewLib.Classes.Add(NewClass5)
        Dim NewClass6 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass6.Name = "ProjectSummary"
        NewClass6.Description = "ProjectSummary stores a summary of a project."
        NewLib.Classes.Add(NewClass6)
        Dim NewClass7 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass7.Name = "DataFileInfo"
        NewClass7.Description = "The DataFileInfo class stores information about a data file."
        NewLib.Classes.Add(NewClass7)
        Dim NewClass8 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass8.Name = "Message"
        NewClass8.Description = "The Message class contains text properties and methods used to display messages in an Andorville™ software application."
        NewLib.Classes.Add(NewClass8)
        Dim NewClass9 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass9.Name = "ApplicationSummary"
        NewClass9.Description = "The ApplicationSummary class stores a summary of an Andorville™ software application."
        NewLib.Classes.Add(NewClass9)
        Dim NewClass10 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass10.Name = "LibrarySummary"
        NewClass10.Description = "The LibrarySummary class stores a summary of a software library used by an application."
        NewLib.Classes.Add(NewClass10)
        Dim NewClass11 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass11.Name = "ClassSummary"
        NewClass11.Description = "The ClassSummary class stores a summary of a class contained in a software library."
        NewLib.Classes.Add(NewClass11)
        Dim NewClass12 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass12.Name = "ModificationSummary"
        NewClass12.Description = "The ModificationSummary class stores a summary of any modifications made to an application or library."
        NewLib.Classes.Add(NewClass12)
        Dim NewClass13 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass13.Name = "ApplicationInfo"
        NewClass13.Description = "The ApplicationInfo class stores information about an Andorville™ software application."
        NewLib.Classes.Add(NewClass13)
        Dim NewClass14 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass14.Name = "Version"
        NewClass14.Description = "The Version class stores application, library or project version information."
        NewLib.Classes.Add(NewClass14)
        Dim NewClass15 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass15.Name = "Author"
        NewClass15.Description = "The Author class stores information about an Author."
        NewLib.Classes.Add(NewClass15)
        Dim NewClass16 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass16.Name = "FileAssociation"
        NewClass16.Description = "The FileAssociation class stores the file association extension and description. An application can open files on its file association list."
        NewLib.Classes.Add(NewClass16)
        Dim NewClass17 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass17.Name = "Copyright"
        NewClass17.Description = "The Copyright class stores copyright information."
        NewLib.Classes.Add(NewClass17)
        Dim NewClass18 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass18.Name = "License"
        NewClass18.Description = "The License class stores license information."
        NewLib.Classes.Add(NewClass18)
        Dim NewClass19 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass19.Name = "SourceCode"
        NewClass19.Description = "The SourceCode class stores information about the source code for the application."
        NewLib.Classes.Add(NewClass19)
        Dim NewClass20 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass20.Name = "Usage"
        NewClass20.Description = "The Usage class stores information about application or project usage."
        NewLib.Classes.Add(NewClass20)
        Dim NewClass21 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass21.Name = "Trademark"
        NewClass21.Description = "The Trademark class stored information about a trademark used by the author of an application or data."
        NewLib.Classes.Add(NewClass21)

        ApplicationInfo.Libraries.Add(NewLib)

        'Add other library information here: --------------------------------------------------------------------------

    End Sub

    'Save the form settings if the form is being minimised:
    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = &H112 Then 'SysCommand
            If m.WParam.ToInt32 = &HF020 Then 'Form is being minimised
                SaveFormSettings()
            End If
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub SaveProjectSettings()
        'Save the project settings in an XML file.
        'Add any Project Settings to be saved into the settingsData XDocument.
        Dim settingsData = <?xml version="1.0" encoding="utf-8"?>
                           <!---->
                           <!--Project settings for ADVL_Coordinates_1 application.-->
                           <ProjectSettings>
                           </ProjectSettings>

        Dim SettingsFileName As String = "ProjectSettings_" & ApplicationInfo.Name & "_" & ".xml"
        Project.SaveXmlSettings(SettingsFileName, settingsData)
    End Sub

    Private Sub RestoreProjectSettings()
        'Restore the project settings from an XML document.

        Dim SettingsFileName As String = "ProjectSettings_" & ApplicationInfo.Name & "_" & ".xml"

        If Project.SettingsFileExists(SettingsFileName) Then
            Dim Settings As System.Xml.Linq.XDocument
            Project.ReadXmlSettings(SettingsFileName, Settings)

            If IsNothing(Settings) Then 'There is no Settings XML data.
                Exit Sub
            End If

            'Restore a Project Setting example:
            If Settings.<ProjectSettings>.<Setting1>.Value = Nothing Then
                'Project setting not saved.
                'Setting1 = ""
            Else
                'Setting1 = Settings.<ProjectSettings>.<Setting1>.Value
            End If

            'Continue restoring saved settings.

        End If

    End Sub

#End Region 'Process XML Files ----------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Display Methods - Code used to display this form." '============================================================================================================================

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Loading the Main form.

        'Set the Application Directory path: ------------------------------------------------
        Project.ApplicationDir = My.Application.Info.DirectoryPath.ToString

        'Read the Application Information file: ---------------------------------------------
        ApplicationInfo.ApplicationDir = My.Application.Info.DirectoryPath.ToString 'Set the Application Directory property

        ''Get the Application Version Information:
        'ApplicationInfo.Version.Major = My.Application.Info.Version.Major
        'ApplicationInfo.Version.Minor = My.Application.Info.Version.Minor
        'ApplicationInfo.Version.Build = My.Application.Info.Version.Build
        'ApplicationInfo.Version.Revision = My.Application.Info.Version.Revision

        'The code above does not appear to work.
        'New code:
        ''Get the Application Version Information:
        'If System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed Then
        '    'Application is network deployed.
        '    ApplicationInfo.Version.Major = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Major
        '    ApplicationInfo.Version.Minor = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Minor
        '    ApplicationInfo.Version.Build = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Build
        '    ApplicationInfo.Version.Revision = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Revision
        'Else
        '    'Application is not network deployed.
        '    ApplicationInfo.Version.Major = My.Application.Info.Version.Major
        '    ApplicationInfo.Version.Minor = My.Application.Info.Version.Minor
        '    ApplicationInfo.Version.Build = My.Application.Info.Version.Build
        '    ApplicationInfo.Version.Revision = My.Application.Info.Version.Revision
        'End If

        'This code is now at the end of this method.


        If ApplicationInfo.ApplicationLocked Then
            MessageBox.Show("The application is locked. If the application is not already in use, remove the 'Application_Info.lock file from the application directory: " & ApplicationInfo.ApplicationDir, "Notice", MessageBoxButtons.OK)
            Dim dr As System.Windows.Forms.DialogResult
            dr = MessageBox.Show("Press 'Yes' to unlock the application", "Notice", MessageBoxButtons.YesNo)
            If dr = System.Windows.Forms.DialogResult.Yes Then
                ApplicationInfo.UnlockApplication()
            Else
                Application.Exit()
                Exit Sub
            End If
        End If

        ReadApplicationInfo()

        'Read the Application Usage information: --------------------------------------------
        ApplicationUsage.StartTime = Now
        ApplicationUsage.SaveLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Directory
        ApplicationUsage.SaveLocn.Path = Project.ApplicationDir
        ApplicationUsage.RestoreUsageInfo()

        'Restore Project information: -------------------------------------------------------
        Project.Application.Name = ApplicationInfo.Name

        'Set up Message object:
        Message.ApplicationName = ApplicationInfo.Name

        'Set up a temporary initial settings location:
        Dim TempLocn As New ADVL_Utilities_Library_1.FileLocation
        TempLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Directory
        TempLocn.Path = ApplicationInfo.ApplicationDir
        Message.SettingsLocn = TempLocn

        Me.Show() 'Show this form before showing the Message form - This will show the App icon on top in the TaskBar.

        'Start showing messages here - Message system is set up.
        'Message.AddText("------------------- Starting Application: ADVL Application Template ----------------- " & vbCrLf, "Heading")
        Message.AddText("------------------- Starting Application: ADVL Monte Carlo ------------------------------- " & vbCrLf, "Heading")
        'Message.AddText("Application usage: Total duration = " & Format(ApplicationUsage.TotalDuration.TotalHours, "#.##") & " hours" & vbCrLf, "Normal")
        Dim TotalDuration As String = ApplicationUsage.TotalDuration.Days.ToString.PadLeft(5, "0"c) & "d:" &
                           ApplicationUsage.TotalDuration.Hours.ToString.PadLeft(2, "0"c) & "h:" &
                           ApplicationUsage.TotalDuration.Minutes.ToString.PadLeft(2, "0"c) & "m:" &
                           ApplicationUsage.TotalDuration.Seconds.ToString.PadLeft(2, "0"c) & "s"
        Message.AddText("Application usage: Total duration = " & TotalDuration & vbCrLf, "Normal")

        'https://msdn.microsoft.com/en-us/library/z2d603cy(v=vs.80).aspx#Y550
        'Process any command line arguments:
        Try
            For Each s As String In My.Application.CommandLineArgs
                Message.Add("Command line argument: " & vbCrLf)
                Message.AddXml(s & vbCrLf & vbCrLf)
                InstrReceived = s
            Next
        Catch ex As Exception
            Message.AddWarning("Error processing command line arguments: " & ex.Message & vbCrLf)
        End Try

        If ProjectSelected = False Then
            'Read the Settings Location for the last project used:
            Project.ReadLastProjectInfo()
            'The Last_Project_Info.xml file contains:
            '  Project Name and Description. Settings Location Type and Settings Location Path.
            Message.Add("Last project details:" & vbCrLf)
            Message.Add("Project Type:  " & Project.Type.ToString & vbCrLf)
            Message.Add("Project Path:  " & Project.Path & vbCrLf)

            'At this point read the application start arguments, if any.
            'The selected project may be changed here.

            'Check if the project is locked:
            If Project.ProjectLocked Then
                Message.AddWarning("The project is locked: " & Project.Name & vbCrLf)
                Dim dr As System.Windows.Forms.DialogResult
                dr = MessageBox.Show("Press 'Yes' to unlock the project", "Notice", MessageBoxButtons.YesNo)
                If dr = System.Windows.Forms.DialogResult.Yes Then
                    Project.UnlockProject()
                    Message.AddWarning("The project has been unlocked: " & Project.Name & vbCrLf)
                    'Read the Project Information file: -------------------------------------------------
                    Message.Add("Reading project info." & vbCrLf)
                    Project.ReadProjectInfoFile()                 'Read the file in the SettingsLocation: ADVL_Project_Info.xml
                    Project.ReadParameters()
                    Project.ReadParentParameters()
                    If Project.ParentParameterExists("ProNetName") Then
                        Project.AddParameter("ProNetName", Project.ParentParameter("ProNetName").Value, Project.ParentParameter("ProNetName").Description) 'AddParameter will update the parameter if it already exists.
                        ProNetName = Project.Parameter("ProNetName").Value
                    Else
                        ProNetName = Project.GetParameter("ProNetName")
                    End If
                    If Project.ParentParameterExists("ProNetPath") Then 'Get the parent parameter value - it may have been updated.
                        Project.AddParameter("ProNetPath", Project.ParentParameter("ProNetPath").Value, Project.ParentParameter("ProNetPath").Description) 'AddParameter will update the parameter if it already exists.
                        ProNetPath = Project.Parameter("ProNetPath").Value
                    Else
                        ProNetPath = Project.GetParameter("ProNetPath") 'If the parameter does not exist, the value is set to ""
                    End If
                    Project.SaveParameters() 'These should be saved now - child projects look for parent parameters in the parameter file.

                    Project.LockProject() 'Lock the project while it is open in this application.
                    'Set the project start time. This is used to track project usage.
                    Project.Usage.StartTime = Now
                    ApplicationInfo.SettingsLocn = Project.SettingsLocn
                    'Set up the Message object:
                    Message.SettingsLocn = Project.SettingsLocn
                    Message.Show()
                Else
                    'Continue without any project selected.
                    Project.Name = ""
                    Project.Type = ADVL_Utilities_Library_1.Project.Types.None
                    Project.Description = ""
                    Project.SettingsLocn.Path = ""
                    Project.DataLocn.Path = ""
                End If
            Else
                'Read the Project Information file: -------------------------------------------------
                Message.Add("Reading project info." & vbCrLf)
                Project.ReadProjectInfoFile()  'Read the file in the Project Location: ADVL_Project_Info.xml
                Project.ReadParameters()
                Project.ReadParentParameters()
                If Project.ParentParameterExists("ProNetName") Then
                    Project.AddParameter("ProNetName", Project.ParentParameter("ProNetName").Value, Project.ParentParameter("ProNetName").Description) 'AddParameter will update the parameter if it already exists.
                    ProNetName = Project.Parameter("ProNetName").Value
                Else
                    ProNetName = Project.GetParameter("ProNetName")
                End If
                If Project.ParentParameterExists("ProNetPath") Then 'Get the parent parameter value - it may have been updated.
                    Project.AddParameter("ProNetPath", Project.ParentParameter("ProNetPath").Value, Project.ParentParameter("ProNetPath").Description) 'AddParameter will update the parameter if it already exists.
                    ProNetPath = Project.Parameter("ProNetPath").Value
                Else
                    ProNetPath = Project.GetParameter("ProNetPath") 'If the parameter does not exist, the value is set to ""
                End If
                Project.SaveParameters() 'These should be saved now - child projects look for parent parameters in the parameter file.

                Project.LockProject() 'Lock the project while it is open in this application.
                'Set the project start time. This is used to track project usage.
                Project.Usage.StartTime = Now
                ApplicationInfo.SettingsLocn = Project.SettingsLocn
                'Set up the Message object:
                Message.SettingsLocn = Project.SettingsLocn
                Message.Show() 'Added 18May19
            End If

        Else  'Project has been opened using Command Line arguments.
            Project.ReadParameters()
            Project.ReadParentParameters()
            If Project.ParentParameterExists("ProNetName") Then
                Project.AddParameter("ProNetName", Project.ParentParameter("ProNetName").Value, Project.ParentParameter("ProNetName").Description) 'AddParameter will update the parameter if it already exists.
                ProNetName = Project.Parameter("ProNetName").Value
            Else
                ProNetName = Project.GetParameter("ProNetName")
            End If
            If Project.ParentParameterExists("ProNetPath") Then 'Get the parent parameter value - it may have been updated.
                Project.AddParameter("ProNetPath", Project.ParentParameter("ProNetPath").Value, Project.ParentParameter("ProNetPath").Description) 'AddParameter will update the parameter if it already exists.
                ProNetPath = Project.Parameter("ProNetPath").Value
            Else
                ProNetPath = Project.GetParameter("ProNetPath") 'If the parameter does not exist, the value is set to ""
            End If
            Project.SaveParameters() 'These should be saved now - child projects look for parent parameters in the parameter file.

            Project.LockProject() 'Lock the project while it is open in this application.
            ProjectSelected = False 'Reset the Project Selected flag.

            'Set up the Message object:
            Message.SettingsLocn = Project.SettingsLocn
            Message.Show() 'Added 18May19
        End If

        'START Initialise the form: ===============================================================

        Me.WebBrowser1.ObjectForScripting = Me
        'IF THE LINE ABOVE PRODUCES AN ERROR ON STARTUP, CHECK THAT THE CODE ON THE FOLLOWING THREE LINES IS INSERTED JUST ABOVE THE Public Class Main STATEMENT.
        'Imports System.Security.Permissions
        '<PermissionSet(SecurityAction.Demand, Name:="FullTrust")>
        '<System.Runtime.InteropServices.ComVisibleAttribute(True)>

        bgwSendMessage.WorkerReportsProgress = True
        bgwSendMessage.WorkerSupportsCancellation = True

        bgwSendMessageAlt.WorkerReportsProgress = True
        bgwSendMessageAlt.WorkerSupportsCancellation = True

        bgwRunInstruction.WorkerReportsProgress = True
        bgwRunInstruction.WorkerSupportsCancellation = True


        dgvMCVariables.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvMCVariables.ColumnHeadersDefaultCellStyle.Font = New Font(dgvMCVariables.Font, FontStyle.Bold)

        'dgvMCVariables.ColumnCount = 14
        dgvMCVariables.ColumnCount = 15
        dgvMCVariables.Columns(0).HeaderText = "Name"
        dgvMCVariables.Columns(1).HeaderText = "Units"
        dgvMCVariables.Columns(2).HeaderText = "Units Abbrev"

        'dgvMCVariables.Columns(2).HeaderText = "Description"
        dgvMCVariables.Columns(3).HeaderText = "Description"

        Dim cboDataSetType As New DataGridViewComboBoxColumn 'Used for selecting the Data Set Type (or Distribution)
        cboDataSetType.FlatStyle = FlatStyle.Flat
        'Special data options:
        cboDataSetType.Items.Add("Data Table")    'A new Table to contain a collection of data columns.
        cboDataSetType.Items.Add("Trial Number")  'The Trial Number of a realization.
        cboDataSetType.Items.Add("Probability Samples")  'The Probability Samples realized using a specified Sampling.
        cboDataSetType.Items.Add("Imported Data") 'Imported data is real data that has been impolrted for analysis.
        cboDataSetType.Items.Add("Normal Scores") 'The standard normal CDF scaled to a variance of 1. Used in the Iman-Conover method of generating correlated variables.

        'Continuous distribution options:
        cboDataSetType.Items.Add("C2 - Beta")
        cboDataSetType.Items.Add("C4 - Beta Scaled")
        'cboDataSetType.Items.Add("C3 - Burr")
        'cboDataSetType.Items.Add("Categorical")
        cboDataSetType.Items.Add("C2 - Cauchy")
        cboDataSetType.Items.Add("C1 - Chi Squared")
        cboDataSetType.Items.Add("C2 - Continuous Uniform")
        cboDataSetType.Items.Add("C1 - Exponential")
        cboDataSetType.Items.Add("C2 - Fisher-Snedecor")
        cboDataSetType.Items.Add("C2 - Gamma")
        cboDataSetType.Items.Add("C2 - Inverse Gaussian")
        cboDataSetType.Items.Add("C2 - Log Normal")
        cboDataSetType.Items.Add("C2 - Normal")
        cboDataSetType.Items.Add("C2 - Pareto")
        cboDataSetType.Items.Add("C1 - Rayleigh")
        cboDataSetType.Items.Add("C4 - Skewed Generalized Error")
        cboDataSetType.Items.Add("C5 - Skewed Generalized T")
        cboDataSetType.Items.Add("C3 - Student's T")
        cboDataSetType.Items.Add("C3 - Triangular")
        cboDataSetType.Items.Add("C3 - Truncated Pareto")

        'Discrete distribution options:
        cboDataSetType.Items.Add("D1 - Bernoulli")
        cboDataSetType.Items.Add("D2 - Binomial")
        cboDataSetType.Items.Add("D1 - Categorical")
        cboDataSetType.Items.Add("D2 - Conway-Maxwell-Poisson")
        cboDataSetType.Items.Add("D2 - Discrete Uniform")
        cboDataSetType.Items.Add("D1 - Geometric")
        cboDataSetType.Items.Add("D3 - Hypergeometric")
        cboDataSetType.Items.Add("D2 - Negative Binomial")
        cboDataSetType.Items.Add("D1 - Poisson")
        cboDataSetType.Items.Add("D2 - Zipf")

        'dgvMCVariables.Columns.Insert(3, cboDataSetType)
        dgvMCVariables.Columns.Insert(4, cboDataSetType)

        'dgvMCVariables.Columns(3).HeaderText = "Data Set Type"
        'dgvMCVariables.Columns(3).Width = 150
        dgvMCVariables.Columns(4).HeaderText = "Data Set Type"
        dgvMCVariables.Columns(4).Width = 150

        Dim cboDataValueType As New DataGridViewComboBoxColumn 'Used for selecting the Data Value Type
        cboDataValueType.FlatStyle = FlatStyle.Flat
        cboDataValueType.Items.Add("Int32")
        cboDataValueType.Items.Add("Single")
        cboDataValueType.Items.Add("Double")
        'dgvMCVariables.Columns.Insert(4, cboDataValueType)
        'dgvMCVariables.Columns(4).HeaderText = "Data Value Type"
        'dgvMCVariables.Columns(4).Width = 150
        dgvMCVariables.Columns.Insert(5, cboDataValueType)
        dgvMCVariables.Columns(5).HeaderText = "Data Value Type"
        dgvMCVariables.Columns(5).Width = 150

        Dim cboDistSampling As New DataGridViewComboBoxColumn 'Used for selecting the distribution Sampling.
        cboDistSampling.FlatStyle = FlatStyle.Flat
        cboDistSampling.Items.Add("N/A") 'Sampling Not Applicable.
        cboDistSampling.Items.Add("Random") 'Random sampling
        cboDistSampling.Items.Add("Latin Hypercube") 'Latin Hypercube sampling, random order
        cboDistSampling.Items.Add("Sorted Latin Hypercube") 'Sorted Latin Hypercube sampling
        cboDistSampling.Items.Add("Median Latin Hypercube") 'Median Latin Hypercube spaced sampling, random order
        cboDistSampling.Items.Add("Sorted Median Latin Hypercube") 'Sorted Median Latin Hypercube spaced sampling

        'dgvMCVariables.Columns.Insert(5, cboDistSampling)
        'dgvMCVariables.Columns(5).HeaderText = "Sampling"
        'dgvMCVariables.Columns(5).Width = 150
        dgvMCVariables.Columns.Insert(6, cboDistSampling)
        dgvMCVariables.Columns(6).HeaderText = "Sampling"
        dgvMCVariables.Columns(6).Width = 150

        'cboDestTable is used to select the destination table for the Variable.
        cboDestTable.FlatStyle = FlatStyle.Flat
        cboDestTable.Items.Add("Calculations") 'The calculations table. Used to perform Monte Carlo calculations on the set of variables.
        cboDestTable.Items.Add("New Table") 'Creates a new table with the same name as the Variable.
        'The Destination Table list is updated when a new table is created.
        'dgvMCVariables.Columns.Insert(6, cboDestTable)
        'dgvMCVariables.Columns(6).HeaderText = "Table"
        'dgvMCVariables.Columns(6).Width = 150
        dgvMCVariables.Columns.Insert(7, cboDestTable)
        dgvMCVariables.Columns(7).HeaderText = "Table"
        dgvMCVariables.Columns(7).Width = 150

        'dgvMCVariables.Columns(7).HeaderText = "Parameter" & vbCrLf & "Name"
        'dgvMCVariables.Columns(7).Width = 60
        'dgvMCVariables.Columns(8).HeaderText = "Value"
        'dgvMCVariables.Columns(8).Width = 60
        'dgvMCVariables.Columns(9).HeaderText = "Parameter" & vbCrLf & "Name"
        'dgvMCVariables.Columns(9).Width = 60
        'dgvMCVariables.Columns(10).HeaderText = "Value"
        'dgvMCVariables.Columns(10).Width = 60
        'dgvMCVariables.Columns(11).HeaderText = "Parameter" & vbCrLf & "Name"
        'dgvMCVariables.Columns(11).Width = 60
        'dgvMCVariables.Columns(12).HeaderText = "Value"
        'dgvMCVariables.Columns(12).Width = 60
        'dgvMCVariables.Columns(13).HeaderText = "Parameter" & vbCrLf & "Name"
        'dgvMCVariables.Columns(13).Width = 60
        'dgvMCVariables.Columns(14).HeaderText = "Value"
        'dgvMCVariables.Columns(14).Width = 60
        'dgvMCVariables.Columns(15).HeaderText = "Parameter" & vbCrLf & "Name"
        'dgvMCVariables.Columns(15).Width = 60
        'dgvMCVariables.Columns(16).HeaderText = "Value"
        'dgvMCVariables.Columns(16).Width = 60
        'dgvMCVariables.Columns(17).HeaderText = "Seed"
        'dgvMCVariables.Columns(17).Width = 40
        dgvMCVariables.Columns(8).HeaderText = "Parameter" & vbCrLf & "Name"
        dgvMCVariables.Columns(8).Width = 60
        dgvMCVariables.Columns(9).HeaderText = "Value"
        dgvMCVariables.Columns(9).Width = 60
        dgvMCVariables.Columns(10).HeaderText = "Parameter" & vbCrLf & "Name"
        dgvMCVariables.Columns(10).Width = 60
        dgvMCVariables.Columns(11).HeaderText = "Value"
        dgvMCVariables.Columns(11).Width = 60
        dgvMCVariables.Columns(12).HeaderText = "Parameter" & vbCrLf & "Name"
        dgvMCVariables.Columns(12).Width = 60
        dgvMCVariables.Columns(13).HeaderText = "Value"
        dgvMCVariables.Columns(13).Width = 60
        dgvMCVariables.Columns(14).HeaderText = "Parameter" & vbCrLf & "Name"
        dgvMCVariables.Columns(14).Width = 60
        dgvMCVariables.Columns(15).HeaderText = "Value"
        dgvMCVariables.Columns(15).Width = 60
        dgvMCVariables.Columns(16).HeaderText = "Parameter" & vbCrLf & "Name"
        dgvMCVariables.Columns(16).Width = 60
        dgvMCVariables.Columns(17).HeaderText = "Value"
        dgvMCVariables.Columns(17).Width = 60
        dgvMCVariables.Columns(18).HeaderText = "Seed"
        dgvMCVariables.Columns(18).Width = 40

        Dim btn As New DataGridViewButtonColumn
        dgvMCVariables.Columns.Add(btn)
        btn.HeaderText = ""
        btn.Text = "Plot"
        btn.Name = "btn"
        btn.UseColumnTextForButtonValue = True
        'dgvMCVariables.Columns(18).Width = 40
        dgvMCVariables.Columns(19).Width = 40

        cmbRVAlignment.Items.Add("NotSet")
        cmbRVAlignment.Items.Add("TopLeft")
        cmbRVAlignment.Items.Add("TopCenter")
        cmbRVAlignment.Items.Add("TopRight")
        cmbRVAlignment.Items.Add("MiddleLeft")
        cmbRVAlignment.Items.Add("MiddleCenter")
        cmbRVAlignment.Items.Add("MiddleRight")
        cmbRVAlignment.Items.Add("BottomLeft")
        cmbRVAlignment.Items.Add("BottomCenter")
        cmbRVAlignment.Items.Add("BottomRight")

        dgvMCVariables.AllowUserToAddRows = False

        dgvResults.AllowUserToAddRows = False

        MonteCarlo.NTrials = 10000

        Dim cboAlignment As New DataGridViewComboBoxColumn 'Used for selecting the Field alignment
        cboAlignment.Items.Add("NotSet")
        cboAlignment.Items.Add("TopLeft")
        cboAlignment.Items.Add("TopCenter")
        cboAlignment.Items.Add("TopRight")
        cboAlignment.Items.Add("MiddleLeft")
        cboAlignment.Items.Add("MiddleCenter")
        cboAlignment.Items.Add("MiddleRight")
        cboAlignment.Items.Add("BottomLeft")
        cboAlignment.Items.Add("BottomCenter")
        cboAlignment.Items.Add("BottomRight")

        dgvCorrMatrix.ColumnCount = 2
        dgvCorrMatrix.Columns.Insert(0, cboCorrVariables)
        dgvCorrMatrix.Columns(0).HeaderText = "Random Variable"
        dgvCorrMatrix.Columns(1).HeaderText = "Correlated Variable 1"
        dgvCorrMatrix.Columns(2).HeaderText = "Correlated Variable 2"
        dgvCorrMatrix.Rows.Add(3)
        dgvCorrMatrix.AllowUserToAddRows = False
        dgvCorrMatrix.Rows(0).Cells(0).Style.BackColor = Color.LightGray
        dgvCorrMatrix.Rows(0).Cells(0).ReadOnly = True
        dgvCorrMatrix.Rows(1).Cells(1).Style.BackColor = Color.LightGray
        dgvCorrMatrix.Rows(1).Cells(1).Value = 1
        dgvCorrMatrix.Rows(1).Cells(1).ReadOnly = True
        dgvCorrMatrix.Rows(2).Cells(2).Style.BackColor = Color.LightGray
        dgvCorrMatrix.Rows(2).Cells(2).Value = 1
        dgvCorrMatrix.Rows(2).Cells(2).ReadOnly = True
        dgvCorrMatrix.Rows(1).Cells(2).Style.BackColor = Color.WhiteSmoke
        dgvCorrMatrix.Rows(1).Cells(2).ReadOnly = True

        dgvCholesky.AllowUserToAddRows = False
        dgvCholesky.ColumnCount = 2
        dgvCholesky.RowCount = 2

        txtNCorrVars.Text = "2"

        cmbCorrTableName.Items.Add("Calculations") 'Add the default Calculations table to the list.

        rbCopyToNewTable.Checked = True

        rbSortAscending.Checked = True





        pbIconInputVar.Image = ImageList1.Images(0)
        pbIconUserDefInputVar.Image = ImageList1.Images(2)
        pbIconOutputVal.Image = ImageList1.Images(4)
        pbIconProcess.Image = ImageList1.Images(6)
        pbIconValProcess.Image = ImageList1.Images(8)
        pbIconCollection.Image = ImageList1.Images(10)
        pbIconValueCopy.Image = ImageList1.Images(12)
        pbIconConstE.Image = ImageList1.Images(14) 'Calculation Tree does not currently support complex numbers.
        rbConstI.Enabled = False 'Calculation Tree does not currently support complex numbers.
        pbIconConstI.Image = ImageList1.Images(16)
        pbIconConstI.Enabled = False
        pbIconConstPi.Image = ImageList1.Images(18)
        pbIconConstUserDef.Image = ImageList1.Images(20)

        pbIconAdd.Image = ImageList1.Images(22)
        pbIconSubtract.Image = ImageList1.Images(24)
        pbIconMultiply.Image = ImageList1.Images(26)
        pbIconDivide.Image = ImageList1.Images(28)
        pbIconSum.Image = ImageList1.Images(30)
        pbIconProduct.Image = ImageList1.Images(32)
        pbIconSin.Image = ImageList1.Images(34)
        pbIconCos.Image = ImageList1.Images(36)
        pbIconTan.Image = ImageList1.Images(38)
        pbIconArcSin.Image = ImageList1.Images(40)
        pbIconArcCos.Image = ImageList1.Images(42)
        pbIconArcTan.Image = ImageList1.Images(44)
        pbIconDegToRad.Image = ImageList1.Images(46)
        pbIconRadToDeg.Image = ImageList1.Images(48)

        pbIconEPower.Image = ImageList1.Images(50)
        pbIconLn.Image = ImageList1.Images(52)
        pbIconTenPower.Image = ImageList1.Images(54)
        pbIconLog.Image = ImageList1.Images(56)
        pbIconSquare.Image = ImageList1.Images(58)
        pbIconSquareRoot.Image = ImageList1.Images(60)
        pbIconCube.Image = ImageList1.Images(62)
        pbIconCubeRoot.Image = ImageList1.Images(64)
        pbIconYthPower.Image = ImageList1.Images(66)
        pbIconYthRoot.Image = ImageList1.Images(68)

        pbIconEquals.Image = ImageList1.Images(84)
        pbIconNegate.Image = ImageList1.Images(70)
        pbIconInvert.Image = ImageList1.Images(72)
        pbIconAbsoluteVal.Image = ImageList1.Images(74)
        pbIconRound.Image = ImageList1.Images(76)
        pbIconRoundUp.Image = ImageList1.Images(78)
        pbIconRoundDown.Image = ImageList1.Images(80)

        pbIconIfGt.Image = ImageList1.Images(90)
        pbIconIfGtEq.Image = ImageList1.Images(92)
        pbIconIfEq.Image = ImageList1.Images(86)
        pbIconIfLtEq.Image = ImageList1.Images(94)
        pbIconIfLt.Image = ImageList1.Images(88)


        trvCalculations.ImageList = ImageList1

        rbInputVar.Checked = True
        rbProcess.Checked = True
        rbConstE.Checked = True
        rbAdd.Checked = True
        rbSin.Checked = True
        rbEPower.Checked = True
        rbNegate.Checked = True

        chkRemoveUnusedValues.Checked = True

        TabPage2.AllowDrop = True 'Allow archived project to be dropped onto the Project Information page. The Project will be extracted from the Archive and opened.

        InitialiseForm() 'Initialise the form for a new project.

        'END   Initialise the form: ---------------------------------------------------------------

        RestoreFormSettings() 'Restore the form settings
        Message.ShowXMessages = ShowXMessages
        Message.ShowSysMessages = ShowSysMessages
        RestoreProjectSettings() 'Restore the Project settings

        ShowProjectInfo() 'Show the project information.

        Message.AddText("------------------- Started OK -------------------------------------------------------------------------- " & vbCrLf & vbCrLf, "Heading")

        If StartupConnectionName = "" Then
            If Project.ConnectOnOpen Then
                ConnectToComNet() 'The Project is set to connect when it is opened.
            ElseIf ApplicationInfo.ConnectOnStartup Then
                ConnectToComNet() 'The Application is set to connect when it is started.
            Else
                'Don't connect to ComNet.
            End If
        Else
            'Connect to ComNet using the connection name StartupConnectionName.
            ConnectToComNet(StartupConnectionName)
        End If

        'If System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed Then
        '    Message.Add("Application version: " & System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString & vbCrLf)
        '    'This version information is defined on: MyProject \ Publish
        'Else
        '    Message.Add("The Application is in Debug mode. The Version information may be incorrect." & vbCrLf)
        '    Message.Add("Application version: " & My.Application.Info.Version.ToString & vbCrLf)
        '    'This version information is defined on: MyProject \ Application \ Assembly Information
        'End If

        'Get the Application Version Information:
        If System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed Then
            'Application is network deployed.
            ApplicationInfo.Version.Number = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString
            ApplicationInfo.Version.Major = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Major
            ApplicationInfo.Version.Minor = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Minor
            ApplicationInfo.Version.Build = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Build
            ApplicationInfo.Version.Revision = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Revision
            ApplicationInfo.Version.Source = "Publish"
            Message.Add("Application version: " & System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString & vbCrLf)
        Else
            'Application is not network deployed.
            ApplicationInfo.Version.Number = My.Application.Info.Version.ToString
            ApplicationInfo.Version.Major = My.Application.Info.Version.Major
            ApplicationInfo.Version.Minor = My.Application.Info.Version.Minor
            ApplicationInfo.Version.Build = My.Application.Info.Version.Build
            ApplicationInfo.Version.Revision = My.Application.Info.Version.Revision
            ApplicationInfo.Version.Source = "Assembly"
            'Message.Add("The Application is in Debug mode. The Version information may be incorrect." & vbCrLf)
            Message.Add("Application version: " & My.Application.Info.Version.ToString & vbCrLf)
        End If

    End Sub

    Private Sub InitialiseForm()
        'Initialise the form for a new project.
        OpenStartPage()
    End Sub

    Private Sub ShowProjectInfo()
        'Show the project information:

        txtParentProject.Text = Project.ParentProjectName
        txtProNetName.Text = Project.GetParameter("ProNetName")
        txtProjectName.Text = Project.Name
        txtProjectDescription.Text = Project.Description
        Select Case Project.Type
            Case ADVL_Utilities_Library_1.Project.Types.Directory
                txtProjectType.Text = "Directory"
            Case ADVL_Utilities_Library_1.Project.Types.Archive
                txtProjectType.Text = "Archive"
            Case ADVL_Utilities_Library_1.Project.Types.Hybrid
                txtProjectType.Text = "Hybrid"
            Case ADVL_Utilities_Library_1.Project.Types.None
                txtProjectType.Text = "None"
        End Select

        txtCreationDate.Text = Format(Project.Usage.FirstUsed, "d-MMM-yyyy H:mm:ss")
        txtLastUsed.Text = Format(Project.Usage.LastUsed, "d-MMM-yyyy H:mm:ss")

        txtProjectPath.Text = Project.Path

        Select Case Project.SettingsLocn.Type
            Case ADVL_Utilities_Library_1.FileLocation.Types.Directory
                txtSettingsLocationType.Text = "Directory"
            Case ADVL_Utilities_Library_1.FileLocation.Types.Archive
                txtSettingsLocationType.Text = "Archive"
        End Select
        txtSettingsPath.Text = Project.SettingsLocn.Path

        Select Case Project.DataLocn.Type
            Case ADVL_Utilities_Library_1.FileLocation.Types.Directory
                txtDataLocationType.Text = "Directory"
            Case ADVL_Utilities_Library_1.FileLocation.Types.Archive
                txtDataLocationType.Text = "Archive"
        End Select
        txtDataPath.Text = Project.DataLocn.Path

        Select Case Project.SystemLocn.Type
            Case ADVL_Utilities_Library_1.FileLocation.Types.Directory
                txtSystemLocationType.Text = "Directory"
            Case ADVL_Utilities_Library_1.FileLocation.Types.Archive
                txtSystemLocationType.Text = "Archive"
        End Select
        txtSystemPath.Text = Project.SystemLocn.Path

        If Project.ConnectOnOpen Then
            chkConnect.Checked = True
        Else
            chkConnect.Checked = False
        End If

        'txtTotalDuration.Text = Project.Usage.TotalDuration.Days.ToString.PadLeft(5, "0"c) & ":" &
        '                        Project.Usage.TotalDuration.Hours.ToString.PadLeft(2, "0"c) & ":" &
        '                        Project.Usage.TotalDuration.Minutes.ToString.PadLeft(2, "0"c) & ":" &
        '                        Project.Usage.TotalDuration.Seconds.ToString.PadLeft(2, "0"c)

        'txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & ":" &
        '                          Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & ":" &
        '                          Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & ":" &
        '                          Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c)

        txtTotalDuration.Text = Project.Usage.TotalDuration.Days.ToString.PadLeft(5, "0"c) & "d:" &
                        Project.Usage.TotalDuration.Hours.ToString.PadLeft(2, "0"c) & "h:" &
                        Project.Usage.TotalDuration.Minutes.ToString.PadLeft(2, "0"c) & "m:" &
                        Project.Usage.TotalDuration.Seconds.ToString.PadLeft(2, "0"c) & "s"

        txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & "d:" &
                                  Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & "h:" &
                                  Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & "m:" &
                                  Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c) & "s"

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Exit the Application

        DisconnectFromComNet() 'Disconnect from the Communication Network (Message Service).

        SaveProjectSettings() 'Save project settings.

        ApplicationInfo.WriteFile() 'Update the Application Information file.

        Project.SaveLastProjectInfo() 'Save information about the last project used.

        Project.SaveParameters()

        'Project.SaveProjectInfoFile() 'Update the Project Information file. This is not required unless there is a change made to the project.

        Project.Usage.SaveUsageInfo() 'Save Project usage information.

        Project.UnlockProject() 'Unlock the project.

        ApplicationUsage.SaveUsageInfo() 'Save Application usage information.
        ApplicationInfo.UnlockApplication()

        Application.Exit()

    End Sub

    Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'Save the form settings if the form state is normal. (A minimised form will have the incorrect size and location.)
        If WindowState = FormWindowState.Normal Then
            SaveFormSettings()
        End If
    End Sub


#End Region 'Form Display Methods -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Open and Close Forms - Code used to open and close other forms." '===================================================================================================================

    Private Sub btnOpenTemplateForm_Click(sender As Object, e As EventArgs)
        'Open the Template form:
        If IsNothing(TemplateForm) Then
            TemplateForm = New frmTemplate
            TemplateForm.Show()
        Else
            TemplateForm.Show()
        End If
    End Sub

    Private Sub TemplateForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles TemplateForm.FormClosed
        TemplateForm = Nothing
    End Sub

    Private Sub btnMessages_Click(sender As Object, e As EventArgs) Handles btnMessages.Click
        'Show the Messages form.
        Message.ApplicationName = ApplicationInfo.Name
        Message.SettingsLocn = Project.SettingsLocn
        Message.Show()
        Message.ShowXMessages = ShowXMessages
        Message.MessageForm.BringToFront()
    End Sub

    Private Sub btnWebPages_Click(sender As Object, e As EventArgs) Handles btnWebPages.Click
        'Open the Web Pages form.

        If IsNothing(WebPageList) Then
            WebPageList = New frmWebPageList
            WebPageList.Show()
        Else
            WebPageList.Show()
            WebPageList.BringToFront()
        End If
    End Sub

    Private Sub WebPageList_FormClosed(sender As Object, e As FormClosedEventArgs) Handles WebPageList.FormClosed
        WebPageList = Nothing
    End Sub

    Public Function OpenNewWebPage() As Integer
        'Open a new HTML Web View window, or reuse an existing one if avaiable.
        'The new forms index number in WebViewFormList is returned.

        NewWebPage = New frmWebPage
        If WebPageFormList.Count = 0 Then
            WebPageFormList.Add(NewWebPage)
            WebPageFormList(0).FormNo = 0
            WebPageFormList(0).Show
            Return 0 'The new HTML Display is at position 0 in WebViewFormList()
        Else
            Dim I As Integer
            Dim FormAdded As Boolean = False
            For I = 0 To WebPageFormList.Count - 1 'Check if there are closed forms in WebViewFormList. They can be re-used.
                If IsNothing(WebPageFormList(I)) Then
                    WebPageFormList(I) = NewWebPage
                    WebPageFormList(I).FormNo = I
                    WebPageFormList(I).Show
                    FormAdded = True
                    Return I 'The new Html Display is at position I in WebViewFormList()
                    Exit For
                End If
            Next
            If FormAdded = False Then 'Add a new form to WebViewFormList
                Dim FormNo As Integer
                WebPageFormList.Add(NewWebPage)
                FormNo = WebPageFormList.Count - 1
                WebPageFormList(FormNo).FormNo = FormNo
                WebPageFormList(FormNo).Show
                Return FormNo 'The new WebPage is at position FormNo in WebPageFormList()
            End If
        End If
    End Function

    Public Sub WebPageFormClosed()
        'This subroutine is called when the Web Page form has been closed.
        'The subroutine is usually called from the FormClosed event of the WebPage form.
        'The WebPage form may have multiple instances.
        'The ClosedFormNumber property should contain the number of the instance of the WebPage form.
        'This property should be updated by the WebPage form when it is being closed.
        'The ClosedFormNumber property value is used to determine which element in WebPageList should be set to Nothing.

        If WebPageFormList.Count < ClosedFormNo + 1 Then
            'ClosedFormNo is too large to exist in WebPageFormList
            Exit Sub
        End If

        If IsNothing(WebPageFormList(ClosedFormNo)) Then
            'The form is already set to nothing
        Else
            WebPageFormList(ClosedFormNo) = Nothing
        End If
    End Sub

    Public Function OpenNewHtmlDisplayPage() As Integer
        'Open a new HTML display window, or reuse an existing one if avaiable.
        'The new forms index number in HtmlDisplayFormList is returned.

        NewHtmlDisplay = New frmHtmlDisplay
        If HtmlDisplayFormList.Count = 0 Then
            HtmlDisplayFormList.Add(NewHtmlDisplay)
            HtmlDisplayFormList(0).FormNo = 0
            HtmlDisplayFormList(0).Show
            Return 0 'The new HTML Display is at position 0 in HtmlDisplayFormList()
        Else
            Dim I As Integer
            Dim FormAdded As Boolean = False
            For I = 0 To HtmlDisplayFormList.Count - 1 'Check if there are closed forms in HtmlDisplayFormList. They can be re-used.
                If IsNothing(HtmlDisplayFormList(I)) Then
                    HtmlDisplayFormList(I) = NewHtmlDisplay
                    HtmlDisplayFormList(I).FormNo = I
                    HtmlDisplayFormList(I).Show
                    FormAdded = True
                    Return I 'The new Html Display is at position I in HtmlDisplayFormList()
                    Exit For
                End If
            Next
            If FormAdded = False Then 'Add a new form to HtmlDisplayFormList
                Dim FormNo As Integer
                HtmlDisplayFormList.Add(NewHtmlDisplay)
                FormNo = HtmlDisplayFormList.Count - 1
                HtmlDisplayFormList(FormNo).FormNo = FormNo
                HtmlDisplayFormList(FormNo).Show
                Return FormNo 'The new HtmlDisplay is at position FormNo in HtmlDisplayFormList()
            End If
        End If
    End Function

    Public Sub HtmlDisplayFormClosed()
        'This subroutine is called when the Html Display form has been closed.
        'The subroutine is usually called from the FormClosed event of the HtmlDisplay form.
        'The HtmlDisplay form may have multiple instances.
        'The ClosedFormNumber property should contain the number of the instance of the HtmlDisplay form.
        'This property should be updated by the HtmlDisplay form when it is being closed.
        'The ClosedFormNumber property value is used to determine which element in HtmlDisplayList should be set to Nothing.

        If HtmlDisplayFormList.Count < ClosedFormNo + 1 Then
            'ClosedFormNo is too large to exist in HtmlDisplayFormList
            Exit Sub
        End If

        If IsNothing(HtmlDisplayFormList(ClosedFormNo)) Then
            'The form is already set to nothing
        Else
            HtmlDisplayFormList(ClosedFormNo) = Nothing
        End If
    End Sub

    Public Function OpenNewTable() As Integer
        'Open a new Table form, or reuse an existing one if avaialable.
        'The new forms index number in TableList is returned.

        Table = New frmTable
        If TableList.Count = 0 Then
            TableList.Add(Table)
            TableList(0).FormNo = 0
            TableList(0).Show
            Return 0 'The new Table is at position 0 in TableList()
        Else
            Dim I As Integer
            Dim TableAdded As Boolean = False
            For I = 0 To TableList.Count - 1
                If IsNothing(TableList(I)) Then
                    TableList(I) = Table
                    TableList(I).FormNo = I
                    TableList(I).Show
                    TableAdded = True
                    Return I 'The new Table is at position I in TableList()
                    Exit For
                End If
            Next
            If TableAdded = False Then 'Add a new Table to TableList
                Dim TableNo As Integer
                TableList.Add(Table)
                TableNo = TableList.Count - 1
                TableList(TableNo).FormNo = TableNo
                TableList(TableNo).Show
                Return TableNo 'The new Table is at position TableNo in TableList()
            End If
        End If
    End Function



    Private Sub btnMatrixOps2_Click(sender As Object, e As EventArgs) Handles btnMatrixOps2.Click
        Dim MatixOpsNo As Integer = OpenNewMatrixOps()
    End Sub

    Public Function OpenNewMatrixOps() As Integer
        'Open a new MatrixOps form, or reuse an existing one if available.
        'The new forms index number in MatrixOpsList is returned.

        MatrixOps = New frmMatrixOps
        If MatrixOpsList.Count = 0 Then
            MatrixOpsList.Add(MatrixOps)
            MatrixOpsList(0).FormNo = 0
            MatrixOpsList(0).Show
            Return 0
        Else
            Dim I As Integer
            Dim MatrixOpsAdded As Boolean = False
            For I = 0 To MatrixOpsList.Count - 1
                If IsNothing(MatrixOpsList(I)) Then
                    MatrixOpsList(I) = MatrixOps
                    MatrixOpsList(I).FormNo = I
                    MatrixOpsList(I).Show
                    MatrixOpsAdded = True
                    Return I 'The new MatrixOps is at position I in MatroxOpsList()
                    Exit For
                End If
            Next
            If MatrixOpsAdded = False Then 'Add a new DataInfo to MatroxOpsList()
                Dim MatrixOpsNo As Integer
                MatrixOpsList.Add(MatrixOps)
                MatrixOpsNo = MatrixOpsList.Count - 1
                MatrixOpsList(MatrixOpsNo).FormNo = MatrixOpsNo
                MatrixOpsList(MatrixOpsNo).Show
                Return MatrixOpsNo 'The new DataInfo is at position DataInfoNo in MatroxOpsList()
            End If
        End If
    End Function

    Private Sub btnShowDataTable_Click(sender As Object, e As EventArgs) Handles btnShowDataTable.Click
        'Open a new Table form:

        If cmbMCTableName.SelectedIndex = -1 Then
            Dim TableNo As Integer = OpenNewTable()
            TableList(TableNo).DataSource = MonteCarlo

            TableList(TableNo).Name = "Monte Carlo Data"
            TableList(TableNo).TableName = "Calculations"
            TableList(TableNo).UpdateTable
        Else
            Dim TableNo As Integer = OpenNewTable()
            TableList(TableNo).DataSource = MonteCarlo

            TableList(TableNo).Name = "Monte Carlo Data"
            TableList(TableNo).TableName = cmbMCTableName.SelectedItem.ToString
            TableList(TableNo).UpdateTable
        End If
    End Sub

    Public Sub TableClosed()
        'This subroutine is called when the Table form has been closed.
        'The subroutine is usually called from the FormClosed event of the Table form.
        'The Table form may have multiple instances.
        'The ClosedFormNumber property should contain the number of the instance of the Table form.
        'This property should be updated by the Table form when it is being closed.
        'The ClosedFormNumber property value is used to determine which element in TableList should be set to Nothing.

        If TableList.Count < ClosedFormNo + 1 Then
            'ClosedFormNo is too large to exist in TableList
            Exit Sub
        End If

        If IsNothing(TableList(ClosedFormNo)) Then
            'The form is already set to nothing
        Else
            TableList(ClosedFormNo) = Nothing
        End If
    End Sub

    Public Function NewDistribChartDisplay() As Integer
        'Open a New DistribChart window, or reuse an existing one if available.
        'The new forms index number in DistribChartFormList is returned.

        DistribChart = New frmDistribChart
        If DistribChartFormList.Count = 0 Then
            DistribChartFormList.Add(DistribChart)
            DistribChartFormList(0).FormNo = 0
            DistribChartFormList(0).Show
            Return 0 'The new RTF Display is at position 0 in DistribChartFormList()
        Else
            Dim I As Integer
            Dim FormAdded As Boolean = False
            For I = 0 To DistribChartFormList.Count - 1 'Check if there are closed forms in DistribChartFormList. They can be re-used.
                If IsNothing(DistribChartFormList(I)) Then
                    DistribChartFormList(I) = DistribChart
                    DistribChartFormList(I).FormNo = I
                    DistribChartFormList(I).Show
                    FormAdded = True
                    Return I 'The new RTF Display is at position I in DistribChartFormList()
                    Exit For
                End If
            Next
            If FormAdded = False Then 'Add a new form to DistribChartFormList
                Dim FormNo As Integer
                DistribChartFormList.Add(DistribChart)
                FormNo = DistribChartFormList.Count - 1
                DistribChartFormList(FormNo).FormNo = FormNo
                DistribChartFormList(FormNo).Show
                Return FormNo 'The new RTF Display is at position FormNo in DistribChartFormList()
            End If
        End If
    End Function

    Public Sub DistribChartFormClosed()
        'This subroutine is called when the Distribution Chart form has been closed.
        'The subroutine is usually called from the FormClosed event of the DistribChart form.
        'The DistribChart form may have multiple instances.
        'The ClosedFormNumber property should contains the number of the instance of the DistribChart form.
        'This property should be updated by the DistribChart form when it is being closed.
        'The ClosedFormNumber property value is used to determine which element in DistribChartFormList should be set to Nothing.

        If DistribChartFormList.Count < ClosedFormNo + 1 Then
            'ClosedFormNo is too large to exist in DistribChartFormList
            Exit Sub
        End If

        If IsNothing(DistribChartFormList(ClosedFormNo)) Then
            'The form is already set to nothing
        Else
            DistribChartFormList(ClosedFormNo) = Nothing
        End If

    End Sub

    Public Function OpenNewSeriesAnalysis() As Integer
        'Open a new SeriesAnalysis form, or reuse an existing one if available.
        'The new forms index number in SeriesAnalysisList is returned.

        SeriesAnalysis = New frmSeriesAnalysis
        If SeriesAnalysisList.Count = 0 Then
            SeriesAnalysisList.Add(SeriesAnalysis)
            SeriesAnalysisList(0).FormNo = 0
            'SeriesAnalysisList(0).Show 'NOTE: This is now opened later - after the SourceColumnName is set - this is needed to find the settings file.
            Return 0 'The new SeriesAnalysis is at position 0 in SeriesAnalysisList()
        Else
            Dim I As Integer
            Dim SeriesAnalysisAdded As Boolean = False
            For I = 0 To SeriesAnalysisList.Count - 1
                If IsNothing(SeriesAnalysisList(I)) Then
                    SeriesAnalysisList(I) = SeriesAnalysis
                    SeriesAnalysisList(I).FormNo = I
                    'SeriesAnalysisList(I).Show  'NOTE: This is now opened later - after the SourceColumnName is set - this is needed to find the settings file.
                    SeriesAnalysisAdded = True
                    Return I 'The new SeriesAnalysis is at position I in SeriesAnalysisList()
                    Exit For
                End If
            Next
            If SeriesAnalysisAdded = False Then 'Add a new SeriesAnalysis to SeriesAnalysisList()
                Dim SeriesAnalysisNo As Integer
                SeriesAnalysisList.Add(SeriesAnalysis)
                SeriesAnalysisNo = SeriesAnalysisList.Count - 1
                SeriesAnalysisList(SeriesAnalysisNo).FormNo = SeriesAnalysisNo
                'SeriesAnalysisList(SeriesAnalysisNo).Show 'NOTE: This is now opened later - after the SourceColumnName is set - this is needed to find the settings file.
                Return SeriesAnalysisNo 'The new SeriesAnalysis is at position SeriesAnalysisNo in SeriesAnalysisList()
            End If
        End If
    End Function

    Private Function SeriesAnalysisOpen(ByVal ColumnName As String) As Boolean
        'Return True if the Series Analysis for for the specified Column Name is open.

        Dim FormOpen As Boolean = False
        For Each Item In SeriesAnalysisList
            If Item Is Nothing Then
                'The corresponding form has been closed.
            Else
                If Item.SourceColumnName = ColumnName Then 'The Series Analysis form corresponding to ColumnName is open.
                    Item.BringToFront 'Bring the form to the front
                    FormOpen = True
                    Exit For
                End If
            End If
        Next
        Return FormOpen
    End Function

    Private Function SeriesAnalysisFormNo(ByVal ColumnName As String) As Integer
        'Return the form number of the Series Analysis form for ColumnName.
        'Returns -1 if the form for the ColumnName is not shown

        Dim FormNo As Integer = -1
        For Each Item In SeriesAnalysisList
            If Item Is Nothing Then
                'The corresponding form has been closed.
            Else
                If Item.SourceColumnName = ColumnName Then 'The Series Analysis form corresponding to ColumnName is open.
                    Item.BringToFront 'Bring the form to the front
                    'FormOpen = True
                    FormNo = Item.FormNo
                    Exit For
                End If
            End If
        Next
        'Return FormOpen
        Return FormNo
    End Function

    Public Sub SeriesAnalysisClosed()
        'This subroutine is called when the SeriesAnalysis form has been closed.
        'The subroutine is usually called from the FormClosed event of the SeriesAnalysis form.
        'The SeriesAnalysis form may have multiple instances.
        'The ClosedFormNumber property should contain the number of the instance of the SeriesAnalysis form.
        'This property should be updated by the SeriesAnalysis form when it is being closed.
        'The ClosedFormNumber property value is used to determine which element in SeriesAnalysisList should be set to Nothing.

        If SeriesAnalysisList.Count < ClosedFormNo + 1 Then
            'ClosedFormNo is too large to exist in SeriesAnalysisList
            Exit Sub
        End If

        If IsNothing(SeriesAnalysisList(ClosedFormNo)) Then
            'The form is already set to nothing
        Else
            SeriesAnalysisList(ClosedFormNo) = Nothing
        End If
    End Sub

    Private Sub btnNewTableWindow_Click(sender As Object, e As EventArgs) Handles btnNewTableWindow.Click
        'Open a new Table form:

        If cmbMCTableName.SelectedIndex = -1 Then
            Dim TableNo As Integer = OpenNewTable()
            TableList(TableNo).DataSource = MonteCarlo
        Else
            Dim TableNo As Integer = OpenNewTable()
            TableList(TableNo).DataSource = MonteCarlo
            TableList(TableNo).TableName = cmbMCTableName.SelectedItem.ToString
        End If
    End Sub

    Public Function OpenNewChart() As Integer
        'Open a new Chart form, or reuse an existing one if avaialable.
        'The new forms index number in ChartFormList is returned.

        Chart = New frmChart
        If ChartList.Count = 0 Then
            ChartList.Add(Chart)
            ChartList(0).FormNo = 0
            ChartList(0).Show
            Return 0 'The new Chart is at position 0 in ChartList()
        Else
            Dim I As Integer
            Dim ChartAdded As Boolean = False
            For I = 0 To ChartList.Count - 1
                If IsNothing(ChartList(I)) Then
                    ChartList(I) = Chart
                    ChartList(I).FormNo = I
                    ChartList(I).Show
                    ChartAdded = True
                    Return I 'The new Chart is at position I in ChartList()
                    Exit For
                End If
            Next
            If ChartAdded = False Then 'Add a new Chart to ChartList
                Dim ChartNo As Integer
                ChartList.Add(Chart)
                ChartNo = ChartList.Count - 1
                ChartList(ChartNo).FormNo = ChartNo
                ChartList(ChartNo).Show
                Return ChartNo 'The new Chart is at position ChartNo in ChartList()
            End If
        End If
    End Function

    Public Sub ChartClosed()
        'This subroutine is called when the Chart form has been closed.
        'The subroutine is usually called from the FormClosed event of the Chart form.
        'The Chart form may have multiple instances.
        'The ClosedFormNumber property should contain the number of the instance of the Chart form.
        'This property should be updated by the Chart form when it is being closed.
        'The ClosedFormNumber property value is used to determine which element in ChartList should be set to Nothing.

        If ChartList.Count < ClosedFormNo + 1 Then
            'ClosedFormNo is too large to exist in ChartList
            Exit Sub
        End If

        If IsNothing(ChartList(ClosedFormNo)) Then
            'The form is already set to nothing
        Else
            ChartList(ClosedFormNo) = Nothing
        End If
    End Sub

    Private Sub btnInfoMonteCarlo_Click(sender As Object, e As EventArgs) Handles btnInfoMonteCarlo.Click
        'Open a Data Info form and select the Monte Carlo data table.
        Dim DataInfoNo As Integer = OpenNewDataInfo()
        DataInfoList(DataInfoNo).Show
        DataInfoList(DataInfoNo).DataSource = MonteCarlo
        DataInfoList(DataInfoNo).DataSourceDescription = "Monte Carlo Model"
        DataInfoList(DataInfoNo).TableName = MCTableName
    End Sub

    Public Function OpenNewDataInfo() As Integer
        'Open a new DataInfo form, or reuse an existing one if avaialable.
        'The new forms index number in DataInfoList is returned.

        DataInfo = New frmDataInfo
        If DataInfoList.Count = 0 Then
            DataInfoList.Add(DataInfo)
            DataInfoList(0).FormNo = 0
            DataInfoList(0).Show
            Return 0 'The new DataInfo is at position 0 in DataInfoList()
        Else
            Dim I As Integer
            Dim DataInfoAdded As Boolean = False
            For I = 0 To DataInfoList.Count - 1
                If IsNothing(DataInfoList(I)) Then
                    DataInfoList(I) = DataInfo
                    DataInfoList(I).FormNo = I
                    DataInfoList(I).Show
                    DataInfoAdded = True
                    Return I 'The new DataInfo is at position I in DataInfoList()
                    Exit For
                End If
            Next
            If DataInfoAdded = False Then 'Add a new DataInfo to DataInfoList()
                Dim DataInfoNo As Integer
                DataInfoList.Add(DataInfo)
                DataInfoNo = DataInfoList.Count - 1
                DataInfoList(DataInfoNo).FormNo = DataInfoNo
                DataInfoList(DataInfoNo).Show
                Return DataInfoNo 'The new DataInfo is at position DataInfoNo in DataInfoList()
            End If
        End If
    End Function

    Public Sub DataInfoClosed()
        'This subroutine is called when the DataInfo form has been closed.
        'The subroutine is usually called from the FormClosed event of the DataInfo form.
        'The DataInfo form may have multiple instances.
        'The ClosedFormNumber property should contain the number of the instance of the DataInfo form.
        'This property should be updated by the DataInfo form when it is being closed.
        'The ClosedFormNumber property value is used to determine which element in DataInfoList should be set to Nothing.

        If DataInfoList.Count < ClosedFormNo + 1 Then
            'ClosedFormNo is too large to exist in DataInfoList
            Exit Sub
        End If

        If IsNothing(DataInfoList(ClosedFormNo)) Then
            'The form is already set to nothing
        Else
            DataInfoList(ClosedFormNo) = Nothing
        End If
    End Sub

    Public Sub MatrixOpsClosed()
        'This subroutine is called when the MatrixOps form has been closed.
        'The subroutine is usually called from the FormClosed event of the MatrixOps form.
        'The MatrixOps form may have multiple instances.
        'The ClosedFormNumber property should contain the number of the instance of the MatrixOps form.
        'This property should be updated by the MatrixOps form when it is being closed.
        'The ClosedFormNumber property value is used to determine which element in MatrixOpsList should be set to Nothing.

        If MatrixOpsList.Count < ClosedFormNo + 1 Then
            'ClosedFormNo is too large to exist in MatrixOpsList
            Exit Sub
        End If

        If IsNothing(MatrixOpsList(ClosedFormNo)) Then
            'The form is already set to nothing
        Else
            MatrixOpsList(ClosedFormNo) = Nothing
        End If
    End Sub

#End Region 'Open and Close Forms -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Methods - The main actions performed by this form." '===========================================================================================================================

    Public Sub CloseAppAtConnection(ByVal ProNetName As String, ByVal ConnectionName As String)
        'Close the application and project at the specified connection.

        If IsNothing(client) Then
            Message.Add("No client connection available!" & vbCrLf)
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.Add("client state is faulted. Message not sent!" & vbCrLf)
            Else
                'Create the XML instructions to close the application at the connection.
                Dim decl As New XDeclaration("1.0", "utf-8", "yes")
                Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.
                Dim xmessage As New XElement("XMsg") 'This indicates the start of the message in the XMessage class

                Dim command As New XElement("Command", "Close")
                xmessage.Add(command)
                doc.Add(xmessage)

                'Show the message sent:
                Message.XAddText("Message sent to: [" & ProNetName & "]." & ConnectionName & ":" & vbCrLf, "XmlSentNotice")
                Message.XAddXml(doc.ToString)
                Message.XAddText(vbCrLf, "Normal") 'Add extra line
                client.SendMessage(ProNetName, ConnectionName, doc.ToString)
            End If
        End If
    End Sub

    Private Sub btnProject_Click(sender As Object, e As EventArgs) Handles btnProject.Click
        Project.SelectProject()
    End Sub

    Private Sub btnParameters_Click(sender As Object, e As EventArgs) Handles btnParameters.Click
        Project.ShowParameters()
    End Sub

    Private Sub btnAppInfo_Click(sender As Object, e As EventArgs) Handles btnAppInfo.Click
        ApplicationInfo.ShowInfo()
    End Sub

    Private Sub btnAndorville_Click(sender As Object, e As EventArgs) Handles btnAndorville.Click
        ApplicationInfo.ShowInfo()
    End Sub

    Private Sub ApplicationInfo_UpdateExePath() Handles ApplicationInfo.UpdateExePath
        'Update the Executable Path.
        ApplicationInfo.ExecutablePath = Application.ExecutablePath
    End Sub

    Private Sub ApplicationInfo_RestoreDefaults() Handles ApplicationInfo.RestoreDefaults
        'Restore the default application settings.
        DefaultAppProperties()
    End Sub

    Public Sub UpdateWebPage(ByVal FileName As String)
        'Update the web page in WebPageFormList if the Web file name is FileName.

        Dim NPages As Integer = WebPageFormList.Count
        Dim I As Integer

        Try
            For I = 0 To NPages - 1
                If IsNothing(WebPageFormList(I)) Then
                    'Web page has been deleted!
                Else
                    If WebPageFormList(I).FileName = FileName Then
                        WebPageFormList(I).OpenDocument
                    End If
                End If
            Next
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub


#Region " Start Page Code" '=========================================================================================================================================

    Public Sub OpenStartPage()
        'Open the StartPage.html file and display in the Workflow tab.

        If Project.DataFileExists("StartPage.html") Then
            WorkflowFileName = "StartPage.html"
            DisplayWorkflow()
        Else
            CreateStartPage()
            WorkflowFileName = "StartPage.html"
            DisplayWorkflow()
        End If

    End Sub

    Public Sub DisplayWorkflow()
        'Display the StartPage.html file in the Start Page tab.

        If Project.DataFileExists(WorkflowFileName) Then
            Dim rtbData As New IO.MemoryStream
            Project.ReadData(WorkflowFileName, rtbData)
            rtbData.Position = 0
            Dim sr As New IO.StreamReader(rtbData)
            WebBrowser1.DocumentText = sr.ReadToEnd()
        Else
            Message.AddWarning("Web page file not found: " & WorkflowFileName & vbCrLf)
        End If
    End Sub

    Private Sub CreateStartPage()
        'Create a new default StartPage.html file.

        Dim htmData As New IO.MemoryStream
        Dim sw As New IO.StreamWriter(htmData)
        sw.Write(AppInfoHtmlString("Application Information")) 'Create a web page providing information about the application.
        sw.Flush()
        Project.SaveData("StartPage.html", htmData)
    End Sub

    Public Function AppInfoHtmlString(ByVal DocumentTitle As String) As String
        'Create an Application Information Web Page.

        'This function should be edited to provide a brief description of the Application.

        Dim sb As New System.Text.StringBuilder

        sb.Append("<!DOCTYPE html>" & vbCrLf)
        sb.Append("<html>" & vbCrLf)
        sb.Append("<head>" & vbCrLf)
        sb.Append("<title>" & DocumentTitle & "</title>" & vbCrLf)
        sb.Append("<meta name=""description"" content=""Application information."">" & vbCrLf)
        sb.Append("</head>" & vbCrLf)

        sb.Append("<body style=""font-family:arial;"">" & vbCrLf & vbCrLf)

        sb.Append("<h2>" & "Andorville&trade; Monte Carlo" & "</h2>" & vbCrLf & vbCrLf) 'Add the page title.
        sb.Append("<hr>" & vbCrLf) 'Add a horizontal divider line.
        sb.Append("<p>The Monte Carlo application is used to design, run and analyse Monte Carlo simulations.</p>" & vbCrLf) 'Add an application description.
        sb.Append("<hr>" & vbCrLf & vbCrLf) 'Add a horizontal divider line.

        sb.Append(DefaultJavaScriptString)

        sb.Append("</body>" & vbCrLf)
        sb.Append("</html>" & vbCrLf)

        Return sb.ToString

    End Function

    Public Function DefaultJavaScriptString() As String
        'Generate the default JavaScript section of an Andorville(TM) Workflow Web Page.

        Dim sb As New System.Text.StringBuilder

        'Add JavaScript section:
        sb.Append("<script>" & vbCrLf & vbCrLf)

        'START: User defined JavaScript functions ==========================================================================
        'Add functions to implement the main actions performed by this web page.
        sb.Append("//START: User defined JavaScript functions ==========================================================================" & vbCrLf)
        sb.Append("//  Add functions to implement the main actions performed by this web page." & vbCrLf & vbCrLf)

        sb.Append("//END:   User defined JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf & vbCrLf)
        'END:   User defined JavaScript functions --------------------------------------------------------------------------


        'START: User modified JavaScript functions ==========================================================================
        'Modify these function to save all required web page settings and process all expected XMessage instructions.
        sb.Append("//START: User modified JavaScript functions ==========================================================================" & vbCrLf)
        sb.Append("//  Modify these function to save all required web page settings and process all expected XMessage instructions." & vbCrLf & vbCrLf)

        'Add the Start Up code section.
        sb.Append("//Code to execute on Start Up:" & vbCrLf)
        sb.Append("function StartUpCode() {" & vbCrLf)
        sb.Append("  RestoreSettings() ;" & vbCrLf)
        sb.Append("}" & vbCrLf & vbCrLf)

        'Add the SaveSettings function - This is used to save web page settings between sessions.
        sb.Append("//Save the web page settings." & vbCrLf)
        sb.Append("function SaveSettings() {" & vbCrLf)
        sb.Append("  var xSettings = ""<Settings>"" + "" \n"" ; //String containing the web page settings in XML format." & vbCrLf)
        sb.Append("  //Add xml lines to save each setting." & vbCrLf & vbCrLf)
        sb.Append("  xSettings +=    ""</Settings>"" + ""\n"" ; //End of the Settings element." & vbCrLf)
        sb.Append(vbCrLf)
        sb.Append("  //Save the settings as an XML file in the project." & vbCrLf)
        sb.Append("  window.external.SaveHtmlSettings(xSettings) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Process a single XMsg instruction (Information:Location pair)
        sb.Append("//Process an XMessage instruction:" & vbCrLf)
        sb.Append("function XMsgInstruction(Info, Locn) {" & vbCrLf)
        sb.Append("  switch(Locn) {" & vbCrLf)
        sb.Append("  //Insert case statements here." & vbCrLf)
        sb.Append(vbCrLf)

        'sb.Append(vbCrLf)
        'sb.Append("  case ""Status"" :" & vbCrLf)
        'sb.Append("    if (Info = ""OK"") { " & vbCrLf)
        'sb.Append("      //Instruction processing completed OK:" & vbCrLf)
        'sb.Append("      } else {" & vbCrLf)
        'sb.Append("      window.external.AddWarning(""Error: Unknown Status information: "" + "" Info: "" + Info + ""\r\n"") ;" & vbCrLf)
        'sb.Append("     }" & vbCrLf)
        'sb.Append("    break ;" & vbCrLf)
        'sb.Append(vbCrLf)

        'sb.Append("  case ""OnCompletion"" :" & vbCrLf)
        sb.Append("  case ""EndInstruction"" :" & vbCrLf)
        sb.Append("    switch(Info) {" & vbCrLf)
        sb.Append("      case ""Stop"" :" & vbCrLf)
        sb.Append("        //Do nothing." & vbCrLf)
        sb.Append("        break ;" & vbCrLf)
        sb.Append(vbCrLf)
        sb.Append("      default:" & vbCrLf)
        'sb.Append("        window.external.AddWarning(""Error: Unknown OnCompletion information:  "" + "" Info: "" + Info + ""\r\n"") ;" & vbCrLf)
        sb.Append("        window.external.AddWarning(""Error: Unknown EndInstruction information:  "" + "" Info: "" + Info + ""\r\n"") ;" & vbCrLf)
        sb.Append("        break ;" & vbCrLf)
        sb.Append("    }" & vbCrLf)
        sb.Append("    break ;" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("  case ""Status"" :" & vbCrLf)
        sb.Append("    switch(Info) {" & vbCrLf)
        sb.Append("      case ""OK"" :" & vbCrLf)
        sb.Append("        //Instruction processing completed OK." & vbCrLf)
        sb.Append("        break ;" & vbCrLf)
        sb.Append(vbCrLf)
        sb.Append("      default:" & vbCrLf)
        sb.Append("        window.external.AddWarning(""Error: Unknown Status information:  "" + "" Info: "" + Info + ""\r\n"") ;" & vbCrLf)
        sb.Append("        break ;" & vbCrLf)
        sb.Append("    }" & vbCrLf)
        sb.Append("    break ;" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("  default:" & vbCrLf)
        sb.Append("    window.external.AddWarning(""Unknown location: "" + Locn + ""\r\n"") ;" & vbCrLf)
        sb.Append("  }" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("//END:   User modified JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf & vbCrLf)
        'END:   User modified JavaScript functions --------------------------------------------------------------------------

        'START: Required Document Library Web Page JavaScript functions ==========================================================================
        sb.Append("//START: Required Document Library Web Page JavaScript functions ==========================================================================" & vbCrLf & vbCrLf)

        'Add the AddText function - This sends a message to the message window using a named text type.
        sb.Append("//Add text to the Message window using a named txt type:" & vbCrLf)
        sb.Append("function AddText(Msg, TextType) {" & vbCrLf)
        sb.Append("  window.external.AddText(Msg, TextType) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the AddMessage function - This sends a message to the message window using default black text.
        sb.Append("//Add a message to the Message window using the default black text:" & vbCrLf)
        sb.Append("function AddMessage(Msg) {" & vbCrLf)
        sb.Append("  window.external.AddMessage(Msg) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the AddWarning function - This sends a red, bold warning message to the message window.
        sb.Append("//Add a warning message to the Message window using bold red text:" & vbCrLf)
        sb.Append("function AddWarning(Msg) {" & vbCrLf)
        sb.Append("  window.external.AddWarning(Msg) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the RestoreSettings function - This is used to restore web page settings.
        sb.Append("//Restore the web page settings." & vbCrLf)
        sb.Append("function RestoreSettings() {" & vbCrLf)
        sb.Append("  window.external.RestoreHtmlSettings() " & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'This line runs the RestoreSettings function when the web page is loaded.
        sb.Append("//Restore the web page settings when the page loads." & vbCrLf)
        'sb.Append("window.onload = RestoreSettings; " & vbCrLf)
        sb.Append("window.onload = StartUpCode ; " & vbCrLf)
        sb.Append(vbCrLf)

        'Restores a single setting on the web page.
        sb.Append("//Restore a web page setting." & vbCrLf)
        sb.Append("  function RestoreSetting(FormName, ItemName, ItemValue) {" & vbCrLf)
        sb.Append("  document.forms[FormName][ItemName].value = ItemValue ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the RestoreOption function - This is used to add an option to a Select list.
        sb.Append("//Restore a Select control Option." & vbCrLf)
        sb.Append("function RestoreOption(SelectId, OptionText) {" & vbCrLf)
        sb.Append("  var x = document.getElementById(SelectId) ;" & vbCrLf)
        sb.Append("  var option = document.createElement(""Option"") ;" & vbCrLf)
        sb.Append("  option.text = OptionText ;" & vbCrLf)
        sb.Append("  x.add(option) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("//END:   Required Document Library Web Page JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf)
        'END:   Required Document Library Web Page JavaScript functions --------------------------------------------------------------------------

        sb.Append("</script>" & vbCrLf & vbCrLf)

        Return sb.ToString

    End Function

    Public Function DefaultJavaScriptString_Old() As String
        'Generate the default JavaScript section of an Andorville(TM) Workflow Web Page.

        Dim sb As New System.Text.StringBuilder

        'Add JavaScript section:
        sb.Append("<script>" & vbCrLf & vbCrLf)

        'START: User defined JavaScript functions ==========================================================================
        'Add functions to implement the main actions performed by this web page.
        sb.Append("//START: User defined JavaScript functions ==========================================================================" & vbCrLf)
        sb.Append("//  Add functions to implement the main actions performed by this web page." & vbCrLf & vbCrLf)

        sb.Append("//END:   User defined JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf & vbCrLf)
        'END:   User defined JavaScript functions --------------------------------------------------------------------------


        'START: User modified JavaScript functions ==========================================================================
        'Modify these function to save all required web page settings and process all expected XMessage instructions.
        sb.Append("//START: User modified JavaScript functions ==========================================================================" & vbCrLf)
        sb.Append("//  Modify these function to save all required web page settings and process all expected XMessage instructions." & vbCrLf & vbCrLf)

        'Add the Start Up code section.
        sb.Append("//Code to execute on Start Up:" & vbCrLf)
        sb.Append("function StartUpCode() {" & vbCrLf)
        sb.Append("  RestoreSettings() ;" & vbCrLf)
        'sb.Append("  GetCalcsDbPath() ;" & vbCrLf)
        sb.Append("}" & vbCrLf & vbCrLf)

        'Add the SaveSettings function - This is used to save web page settings between sessions.
        sb.Append("//Save the web page settings." & vbCrLf)
        sb.Append("function SaveSettings() {" & vbCrLf)
        sb.Append("  var xSettings = ""<Settings>"" + "" \n"" ; //String containing the web page settings in XML format." & vbCrLf)
        sb.Append("  //Add xml lines to save each setting." & vbCrLf & vbCrLf)
        sb.Append("  xSettings +=    ""</Settings>"" + ""\n"" ; //End of the Settings element." & vbCrLf)
        sb.Append(vbCrLf)
        sb.Append("  //Save the settings as an XML file in the project." & vbCrLf)
        sb.Append("  window.external.SaveHtmlSettings(xSettings) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Process a single XMsg instruction (Information:Location pair)
        sb.Append("//Process an XMessage instruction:" & vbCrLf)
        sb.Append("function XMsgInstruction(Info, Locn) {" & vbCrLf)
        sb.Append("  switch(Locn) {" & vbCrLf)
        sb.Append("  //Insert case statements here." & vbCrLf)
        sb.Append("  case ""Status"" :" & vbCrLf)
        sb.Append("    if (Info = ""OK"") { " & vbCrLf)
        sb.Append("      //Instruction processing completed OK:" & vbCrLf)
        sb.Append("      } else {" & vbCrLf)
        sb.Append("      window.external.AddWarning(""Error: Unknown Status information: "" + "" Info: "" + Info + ""\r\n"") ;" & vbCrLf)
        sb.Append("     }" & vbCrLf)
        sb.Append("    break ;" & vbCrLf)
        sb.Append(vbCrLf)
        sb.Append("  default:" & vbCrLf)
        sb.Append("    window.external.AddWarning(""Unknown location: "" + Locn + ""\r\n"") ;" & vbCrLf)
        sb.Append("  }" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("//END:   User modified JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf & vbCrLf)
        'END:   User modified JavaScript functions --------------------------------------------------------------------------

        'START: Required Document Library Web Page JavaScript functions ==========================================================================
        sb.Append("//START: Required Document Library Web Page JavaScript functions ==========================================================================" & vbCrLf & vbCrLf)

        'Add the AddText function - This sends a message to the message window using a named text type.
        sb.Append("//Add text to the Message window using a named txt type:" & vbCrLf)
        sb.Append("function AddText(Msg, TextType) {" & vbCrLf)
        sb.Append("  window.external.AddText(Msg, TextType) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the AddMessage function - This sends a message to the message window using default black text.
        sb.Append("//Add a message to the Message window using the default black text:" & vbCrLf)
        sb.Append("function AddMessage(Msg) {" & vbCrLf)
        sb.Append("  window.external.AddMessage(Msg) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the AddWarning function - This sends a red, bold warning message to the message window.
        sb.Append("//Add a warning message to the Message window using bold red text:" & vbCrLf)
        sb.Append("function AddWarning(Msg) {" & vbCrLf)
        sb.Append("  window.external.AddWarning(Msg) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the RestoreSettings function - This is used to restore web page settings.
        sb.Append("//Restore the web page settings." & vbCrLf)
        sb.Append("function RestoreSettings() {" & vbCrLf)
        sb.Append("  window.external.RestoreHtmlSettings() " & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'This line runs the RestoreSettings function when the web page is loaded.
        sb.Append("//Restore the web page settings when the page loads." & vbCrLf)
        'sb.Append("window.onload = RestoreSettings; " & vbCrLf)
        sb.Append("window.onload = StartUpCode ; " & vbCrLf)
        sb.Append(vbCrLf)

        'Restores a single setting on the web page.
        sb.Append("//Restore a web page setting." & vbCrLf)
        sb.Append("  function RestoreSetting(FormName, ItemName, ItemValue) {" & vbCrLf)
        sb.Append("  document.forms[FormName][ItemName].value = ItemValue ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the RestoreOption function - This is used to add an option to a Select list.
        sb.Append("//Restore a Select control Option." & vbCrLf)
        sb.Append("function RestoreOption(SelectId, OptionText) {" & vbCrLf)
        sb.Append("  var x = document.getElementById(SelectId) ;" & vbCrLf)
        sb.Append("  var option = document.createElement(""Option"") ;" & vbCrLf)
        sb.Append("  option.text = OptionText ;" & vbCrLf)
        sb.Append("  x.add(option) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("//END:   Required Document Library Web Page JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf)
        'END:   Required Document Library Web Page JavaScript functions --------------------------------------------------------------------------

        sb.Append("</script>" & vbCrLf & vbCrLf)

        Return sb.ToString

    End Function

    Public Function DefaultHtmlString(ByVal DocumentTitle As String) As String
        'Create a blank HTML Web Page.

        Dim sb As New System.Text.StringBuilder

        sb.Append("<!DOCTYPE html>" & vbCrLf)
        sb.Append("<html>" & vbCrLf)
        sb.Append("<!-- Andorville(TM) Workflow File -->" & vbCrLf)
        sb.Append("<!-- Application Name:    " & ApplicationInfo.Name & " -->" & vbCrLf)
        sb.Append("<!-- Application Version: " & My.Application.Info.Version.ToString & " -->" & vbCrLf)
        sb.Append("<!-- Creation Date:          " & Format(Now, "dd MMMM yyyy") & " -->" & vbCrLf)
        sb.Append("<head>" & vbCrLf)
        sb.Append("<title>" & DocumentTitle & "</title>" & vbCrLf)
        sb.Append("<meta name=""description"" content=""Workflow description."">" & vbCrLf)
        sb.Append("</head>" & vbCrLf)

        sb.Append("<body style=""font-family:arial;"">" & vbCrLf & vbCrLf)

        sb.Append("<h2>" & DocumentTitle & "</h2>" & vbCrLf & vbCrLf)

        sb.Append(DefaultJavaScriptString)

        sb.Append("</body>" & vbCrLf)
        sb.Append("</html>" & vbCrLf)

        Return sb.ToString

    End Function

    Public Function DefaultHtmlString_Old(ByVal DocumentTitle As String) As String
        'Create a blank HTML Web Page.

        Dim sb As New System.Text.StringBuilder

        sb.Append("<!DOCTYPE html>" & vbCrLf)
        sb.Append("<html>" & vbCrLf & "<head>" & vbCrLf & "<title>" & DocumentTitle & "</title>" & vbCrLf)
        sb.Append("</head>" & vbCrLf & "<body>" & vbCrLf & vbCrLf)
        sb.Append("<h1>" & DocumentTitle & "</h1>" & vbCrLf & vbCrLf)

        'Add JavaScript section:
        sb.Append("<script>" & vbCrLf & vbCrLf)

        'START: User defined JavaScript functions ==========================================================================
        'Add functions to implement the main actions performed by this web page.
        sb.Append("//START: User defined JavaScript functions ==========================================================================" & vbCrLf)
        sb.Append("//  Add functions to implement the main actions performed by this web page." & vbCrLf & vbCrLf)

        sb.Append("//END:   User defined JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf & vbCrLf)
        'END:   User defined JavaScript functions --------------------------------------------------------------------------


        'START: User modified JavaScript functions ==========================================================================
        'Modify these function to save all required web page settings and process all expected XMessage instructions.
        sb.Append("//START: User modified JavaScript functions ==========================================================================" & vbCrLf)
        sb.Append("//  Modify these function to save all required web page settings and process all expected XMessage instructions." & vbCrLf & vbCrLf)

        'Add the SaveSettings function - This is used to save web page settings between sessions.
        sb.Append("//Save the web page settings." & vbCrLf)
        sb.Append("function SaveSettings() {" & vbCrLf)
        sb.Append("  var xSettings = ""<Settings>"" + "" \n"" ; //String containing the web page settings in XML format." & vbCrLf)
        sb.Append("  //Add xml lines to save each setting." & vbCrLf & vbCrLf)
        sb.Append("  xSettings +=    ""</Settings>"" + ""\n"" ; //End of the Settings element." & vbCrLf)
        sb.Append(vbCrLf)
        sb.Append("  //Save the settings as an XML file in the project." & vbCrLf)
        sb.Append("  window.external.SaveHtmlSettings(xSettings) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Process a single XMsg instruction (Information:Location pair)
        sb.Append("//Process an XMessage instruction:" & vbCrLf)
        sb.Append("function XMsgInstruction(Info, Locn) {" & vbCrLf)
        sb.Append("  switch(Locn) {" & vbCrLf)
        sb.Append("  //Insert case statements here." & vbCrLf)
        sb.Append("  default:" & vbCrLf)
        sb.Append("    window.external.AddWarning(""Unknown location: "" + Locn + ""\r\n"") ;" & vbCrLf)
        sb.Append("  }" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("//END:   User modified JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf & vbCrLf)
        'END:   User modified JavaScript functions --------------------------------------------------------------------------

        'START: Required Document Library Web Page JavaScript functions ==========================================================================
        sb.Append("//START: Required Document Library Web Page JavaScript functions ==========================================================================" & vbCrLf & vbCrLf)

        'Add the AddText function - This sends a message to the message window using a named text type.
        sb.Append("//Add text to the Message window using a named txt type:" & vbCrLf)
        sb.Append("function AddText(Msg, TextType) {" & vbCrLf)
        sb.Append("  window.external.AddText(Msg, TextType) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the AddMessage function - This sends a message to the message window using default black text.
        sb.Append("//Add a message to the Message window using the default black text:" & vbCrLf)
        sb.Append("function AddMessage(Msg) {" & vbCrLf)
        sb.Append("  window.external.AddMessage(Msg) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the AddWarning function - This sends a red, bold warning message to the message window.
        sb.Append("//Add a warning message to the Message window using bold red text:" & vbCrLf)
        sb.Append("function AddWarning(Msg) {" & vbCrLf)
        sb.Append("  window.external.AddWarning(Msg) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the RestoreSettings function - This is used to restore web page settings.
        sb.Append("//Restore the web page settings." & vbCrLf)
        sb.Append("function RestoreSettings() {" & vbCrLf)
        sb.Append("  window.external.RestoreHtmlSettings() " & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'This line runs the RestoreSettings function when the web page is loaded.
        sb.Append("//Restore the web page settings when the page loads." & vbCrLf)
        sb.Append("window.onload = RestoreSettings; " & vbCrLf)
        sb.Append(vbCrLf)

        'Restores a single setting on the web page.
        sb.Append("//Restore a web page setting." & vbCrLf)
        sb.Append("  function RestoreSetting(FormName, ItemName, ItemValue) {" & vbCrLf)
        sb.Append("  document.forms[FormName][ItemName].value = ItemValue ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the RestoreOption function - This is used to add an option to a Select list.
        sb.Append("//Restore a Select control Option." & vbCrLf)
        sb.Append("function RestoreOption(SelectId, OptionText) {" & vbCrLf)
        sb.Append("  var x = document.getElementById(SelectId) ;" & vbCrLf)
        sb.Append("  var option = document.createElement(""Option"") ;" & vbCrLf)
        sb.Append("  option.text = OptionText ;" & vbCrLf)
        sb.Append("  x.add(option) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("//END:   Required Document Library Web Page JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf)
        'END:   Required Document Library Web Page JavaScript functions --------------------------------------------------------------------------

        sb.Append("</script>" & vbCrLf & vbCrLf)

        sb.Append("</body>" & vbCrLf & "</html>" & vbCrLf)

        Return sb.ToString

    End Function

#End Region 'Start Page Code ------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Methods Called by JavaScript - A collection of methods that can be called by JavaScript in a web page shown in WebBrowser1" '==================================
    'These methods are used to display HTML pages in the Workflow tab.
    'The same methods can be found in the WebView form, which displays web pages on seprate forms.


    'Display Messages ==============================================================================================

    Public Sub AddMessage(ByVal Msg As String)
        'Add a normal text message to the Message window.
        Message.Add(Msg)
    End Sub

    Public Sub AddWarning(ByVal Msg As String)
        'Add a warning text message to the Message window.
        Message.AddWarning(Msg)
    End Sub

    Public Sub AddTextTypeMessage(ByVal Msg As String, ByVal TextType As String)
        'Add a message with the specified Text Type to the Message window.
        Message.AddText(Msg, TextType)
    End Sub

    Public Sub AddXmlMessage(ByVal XmlText As String)
        'Add an Xml message to the Message window.
        Message.AddXml(XmlText)
    End Sub

    'END Display Messages ------------------------------------------------------------------------------------------


    'Run an XSequence ==============================================================================================

    Public Sub RunClipboardXSeq()
        'Run the XSequence instructions in the clipboard.

        Dim XDocSeq As System.Xml.Linq.XDocument
        Try
            XDocSeq = XDocument.Parse(My.Computer.Clipboard.GetText)
        Catch ex As Exception
            Message.AddWarning("Error reading Clipboard data. " & ex.Message & vbCrLf)
            Exit Sub
        End Try

        If IsNothing(XDocSeq) Then
            Message.Add("No XSequence instructions were found in the clipboard.")
        Else
            Dim XmlSeq As New System.Xml.XmlDocument
            Try
                XmlSeq.LoadXml(XDocSeq.ToString) 'Convert XDocSeq to an XmlDocument to process with XSeq.
                'Run the sequence:
                XSeq.RunXSequence(XmlSeq, Status)
            Catch ex As Exception
                Message.AddWarning("Error restoring HTML settings. " & ex.Message & vbCrLf)
            End Try
        End If
    End Sub

    Public Sub RunXSequence(ByVal XSequence As String)
        'Run the XMSequence
        Dim XmlSeq As New System.Xml.XmlDocument
        XmlSeq.LoadXml(XSequence)
        XSeq.RunXSequence(XmlSeq, Status)
    End Sub

    Private Sub XSeq_ErrorMsg(ErrMsg As String) Handles XSeq.ErrorMsg
        Message.AddWarning(ErrMsg & vbCrLf)
    End Sub

    Private Sub XSeq_Instruction(Data As String, Locn As String) Handles XSeq.Instruction
        'Execute each instruction produced by running the XSeq file.

        Select Case Locn

            'Restore Web Page Settings: -------------------------------------------------
            Case "Settings:Form:Name"
                FormName = Data

            Case "Settings:Form:Item:Name"
                ItemName = Data

            Case "Settings:Form:Item:Value"
                RestoreSetting(FormName, ItemName, Data)

            Case "Settings:Form:SelectId"
                SelectId = Data

            Case "Settings:Form:OptionText"
                RestoreOption(SelectId, Data)
            'END Restore Web Page Settings: ---------------------------------------------

            ''Start Project commands: ----------------------------------------------------

            'Case "StartProject:AppName"
            '    StartProject_AppName = Data

            'Case "StartProject:ConnectionName"
            '    StartProject_ConnName = Data

            'Case "StartProject:ProNetName"
            '    StartProject_ProNetName = Data

            'Case "StartProject:ProjectID"
            '    StartProject_ProjID = Data

            'Case "StartProject:ProjectName"
            '    StartProject_ProjName = Data

            'Case "StartProject:Command"
            '    Select Case Data
            '        Case "Apply"
            '            If StartProject_ProjName <> "" Then
            '                StartApp_ProjectName(StartProject_AppName, StartProject_ProjName, StartProject_ConnName)
            '            ElseIf StartProject_ProjID <> "" Then
            '                StartApp_ProjectID(StartProject_AppName, StartProject_ProjID, StartProject_ConnName)
            '            Else
            '                Message.AddWarning("Project not specified. Project Name and Project ID are blank." & vbCrLf)
            '            End If
            '        Case Else
            '            Message.AddWarning("Unknown Start Project command : " & Data & vbCrLf)
            '    End Select

            ''END Start project commands ---------------------------------------------

            Case "Settings"
                'Do nothing


            Case "EndOfSequence"
                'Main.Message.Add("End of processing sequence" & Data & vbCrLf)

            Case Else
                Message.AddWarning("Unknown location: " & Locn & "  Data: " & Data & vbCrLf)

        End Select
    End Sub

    'END Run an XSequence ------------------------------------------------------------------------------------------


    'Run an XMessage ===============================================================================================

    Public Sub RunXMessage(ByVal XMsg As String)
        'Run the XMessage by sending it to InstrReceived.
        InstrReceived = XMsg
    End Sub

    Public Sub SendXMessage(ByVal ConnName As String, ByVal XMsg As String)
        'Send the XMessage to the application with the connection name ConnName.
        If IsNothing(client) Then
            Message.Add("No client connection available!" & vbCrLf)
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.Add("client state is faulted. Message not sent!" & vbCrLf)
            Else
                If bgwSendMessage.IsBusy Then
                    Message.AddWarning("Send Message backgroundworker is busy." & vbCrLf)
                Else
                    Dim SendMessageParams As New Main.clsSendMessageParams
                    SendMessageParams.ProjectNetworkName = ProNetName
                    SendMessageParams.ConnectionName = ConnName
                    SendMessageParams.Message = XMsg
                    bgwSendMessage.RunWorkerAsync(SendMessageParams)
                    If ShowXMessages Then
                        Message.XAddText("Message sent to " & "[" & ProNetName & "]." & ConnName & ":" & vbCrLf, "XmlSentNotice")
                        Message.XAddXml(XMsg)
                        Message.XAddText(vbCrLf, "Normal") 'Add extra line
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub SendXMessageExt(ByVal ProNetName As String, ByVal ConnName As String, ByVal XMsg As String)
        'Send the XMsg to the application with the connection name ConnName and Project Network Name ProNetname.
        'This version can send the XMessage to a connection external to the current Project Network.
        If IsNothing(client) Then
            Message.Add("No client connection available!" & vbCrLf)
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.Add("client state is faulted. Message not sent!" & vbCrLf)
            Else
                If bgwSendMessage.IsBusy Then
                    Message.AddWarning("Send Message backgroundworker is busy." & vbCrLf)
                Else
                    Dim SendMessageParams As New Main.clsSendMessageParams
                    SendMessageParams.ProjectNetworkName = ProNetName
                    SendMessageParams.ConnectionName = ConnName
                    SendMessageParams.Message = XMsg
                    bgwSendMessage.RunWorkerAsync(SendMessageParams)
                    If ShowXMessages Then
                        Message.XAddText("Message sent to " & "[" & ProNetName & "]." & ConnName & ":" & vbCrLf, "XmlSentNotice")
                        Message.XAddXml(XMsg)
                        Message.XAddText(vbCrLf, "Normal") 'Add extra line
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub SendXMessageWait(ByVal ConnName As String, ByVal XMsg As String)
        'Send the XMsg to the application with the connection name ConnName.
        'Wait for the connection to be made.
        If IsNothing(client) Then
            Message.Add("No client connection available!" & vbCrLf)
        Else
            Try
                'Application.DoEvents() 'TRY THE METHOD WITHOUT THE DOEVENTS
                If client.State = ServiceModel.CommunicationState.Faulted Then
                    Message.Add("client state is faulted. Message not sent!" & vbCrLf)
                Else
                    Dim StartTime As Date = Now
                    Dim Duration As TimeSpan
                    'Wait up to 16 seconds for the connection ConnName to be established
                    While client.ConnectionExists(ProNetName, ConnName) = False 'Wait until the required connection is made.
                        System.Threading.Thread.Sleep(1000) 'Pause for 1000ms
                        Duration = Now - StartTime
                        If Duration.Seconds > 16 Then Exit While
                    End While

                    If client.ConnectionExists(ProNetName, ConnName) = False Then
                        Message.AddWarning("Connection not available: " & ConnName & " in application network: " & ProNetName & vbCrLf)
                    Else
                        If bgwSendMessage.IsBusy Then
                            Message.AddWarning("Send Message backgroundworker is busy." & vbCrLf)
                        Else
                            Dim SendMessageParams As New Main.clsSendMessageParams
                            SendMessageParams.ProjectNetworkName = ProNetName
                            SendMessageParams.ConnectionName = ConnName
                            SendMessageParams.Message = XMsg
                            bgwSendMessage.RunWorkerAsync(SendMessageParams)
                            If ShowXMessages Then
                                Message.XAddText("Message sent to " & "[" & ProNetName & "]." & ConnName & ":" & vbCrLf, "XmlSentNotice")
                                Message.XAddXml(XMsg)
                                Message.XAddText(vbCrLf, "Normal") 'Add extra line
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                Message.AddWarning(ex.Message & vbCrLf)
            End Try
        End If
    End Sub

    Public Sub SendXMessageExtWait(ByVal ProNetName As String, ByVal ConnName As String, ByVal XMsg As String)
        'Send the XMsg to the application with the connection name ConnName and Project Network Name ProNetName.
        'Wait for the connection to be made.
        'This version can send the XMessage to a connection external to the current Project Network.
        If IsNothing(client) Then
            Message.Add("No client connection available!" & vbCrLf)
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.Add("client state is faulted. Message not sent!" & vbCrLf)
            Else
                Dim StartTime As Date = Now
                Dim Duration As TimeSpan
                'Wait up to 16 seconds for the connection ConnName to be established
                While client.ConnectionExists(ProNetName, ConnName) = False
                    System.Threading.Thread.Sleep(1000) 'Pause for 1000ms
                    Duration = Now - StartTime
                    If Duration.Seconds > 16 Then Exit While
                End While

                If client.ConnectionExists(ProNetName, ConnName) = False Then
                    Message.AddWarning("Connection not available: " & ConnName & " in application network: " & ProNetName & vbCrLf)
                Else
                    If bgwSendMessage.IsBusy Then
                        Message.AddWarning("Send Message backgroundworker is busy." & vbCrLf)
                    Else
                        Dim SendMessageParams As New Main.clsSendMessageParams
                        SendMessageParams.ProjectNetworkName = ProNetName
                        SendMessageParams.ConnectionName = ConnName
                        SendMessageParams.Message = XMsg
                        bgwSendMessage.RunWorkerAsync(SendMessageParams)
                        If ShowXMessages Then
                            Message.XAddText("Message sent to " & "[" & ProNetName & "]." & ConnName & ":" & vbCrLf, "XmlSentNotice")
                            Message.XAddXml(XMsg)
                            Message.XAddText(vbCrLf, "Normal") 'Add extra line
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub XMsgInstruction(ByVal Info As String, ByVal Locn As String)
        'Send the XMessage Instruction to the JavaScript function XMsgInstruction for processing.
        Me.WebBrowser1.Document.InvokeScript("XMsgInstruction", New String() {Info, Locn})
    End Sub

    'END Run an XMessage -------------------------------------------------------------------------------------------


    'Get Information ===============================================================================================

    Public Function GetFormNo() As String
        'Return the Form Number of the current instance of the WebPage form.
        'Return FormNo.ToString
        Return "-1" 'The Main Form is not a Web Page form.
    End Function

    Public Function GetParentFormNo() As String
        'Return the Form Number of the Parent Form (that called this form).
        'Return ParentWebPageFormNo.ToString
        Return "-1" 'The Main Form does not have a Parent Web Page.
    End Function

    Public Function GetConnectionName() As String
        'Return the Connection Name of the Project.
        Return ConnectionName
    End Function

    Public Function GetProNetName() As String
        'Return the Project Network Name of the Project.
        Return ProNetName
    End Function

    Public Sub ParentProjectName(ByVal FormName As String, ByVal ItemName As String)
        'Return the Parent Project name:
        RestoreSetting(FormName, ItemName, Project.ParentProjectName)
    End Sub

    Public Sub ParentProjectPath(ByVal FormName As String, ByVal ItemName As String)
        'Return the Parent Project path:
        RestoreSetting(FormName, ItemName, Project.ParentProjectPath)
    End Sub

    Public Sub ParentProjectParameterValue(ByVal FormName As String, ByVal ItemName As String, ByVal ParameterName As String)
        'Return the specified Parent Project parameter value:
        RestoreSetting(FormName, ItemName, Project.ParentParameter(ParameterName).Value)
    End Sub

    Public Sub ProjectParameterValue(ByVal FormName As String, ByVal ItemName As String, ByVal ParameterName As String)
        'Return the specified Project parameter value:
        RestoreSetting(FormName, ItemName, Project.Parameter(ParameterName).Value)
    End Sub

    Public Sub ProjectNetworkName(ByVal FormName As String, ByVal ItemName As String)
        'Return the name of the Project Network:
        RestoreSetting(FormName, ItemName, Project.Parameter("ProNetName").Value)
    End Sub

    'END Get Information -------------------------------------------------------------------------------------------


    'Open a Web Page ===============================================================================================

    Public Sub OpenWebPage(ByVal FileName As String)
        'Open the web page with the specified File Name.

        If FileName = "" Then

        Else
            'First check if the HTML file is already open:
            Dim FileFound As Boolean = False
            If WebPageFormList.Count = 0 Then

            Else
                Dim I As Integer
                For I = 0 To WebPageFormList.Count - 1
                    If WebPageFormList(I) Is Nothing Then

                    Else
                        If WebPageFormList(I).FileName = FileName Then
                            FileFound = True
                            WebPageFormList(I).BringToFront
                        End If
                    End If
                Next
            End If

            If FileFound = False Then
                Dim FormNo As Integer = OpenNewWebPage()
                WebPageFormList(FormNo).FileName = FileName
                WebPageFormList(FormNo).OpenDocument
                WebPageFormList(FormNo).BringToFront
            End If
        End If
    End Sub

    'END Open a Web Page -------------------------------------------------------------------------------------------


    'Open and Close Projects =======================================================================================

    Public Sub OpenProjectAtRelativePath(ByVal RelativePath As String, ByVal ConnectionName As String)
        'Open the Project at the specified Relative Path using the specified Connection Name.

        Dim ProjectPath As String
        If RelativePath.StartsWith("\") Then
            ProjectPath = Project.Path & RelativePath
            client.StartProjectAtPath(ProjectPath, ConnectionName)
        Else
            ProjectPath = Project.Path & "\" & RelativePath
            client.StartProjectAtPath(ProjectPath, ConnectionName)
        End If
    End Sub

    Public Sub CheckOpenProjectAtRelativePath(ByVal RelativePath As String, ByVal ConnectionName As String)
        'Check if the project at the specified Relative Path is open.
        'Open it if it is not already open.
        'Open the Project at the specified Relative Path using the specified Connection Name.

        Dim ProjectPath As String
        If RelativePath.StartsWith("\") Then
            ProjectPath = Project.Path & RelativePath
            If client.ProjectOpen(ProjectPath) Then
                'Project is already open.
            Else
                client.StartProjectAtPath(ProjectPath, ConnectionName)
            End If
        Else
            ProjectPath = Project.Path & "\" & RelativePath
            If client.ProjectOpen(ProjectPath) Then
                'Project is already open.
            Else
                client.StartProjectAtPath(ProjectPath, ConnectionName)
            End If
        End If
    End Sub

    Public Sub OpenProjectAtProNetPath(ByVal RelativePath As String, ByVal ConnectionName As String)
        'Open the Project at the specified Path (relative to the Project Network Path) using the specified Connection Name.

        Dim ProjectPath As String
        If RelativePath.StartsWith("\") Then
            If Project.ParameterExists("ProNetPath") Then
                ProjectPath = Project.GetParameter("ProNetPath") & RelativePath
                client.StartProjectAtPath(ProjectPath, ConnectionName)
            Else
                Message.AddWarning("The Project Network Path is not known." & vbCrLf)
            End If
        Else
            If Project.ParameterExists("ProNetPath") Then
                ProjectPath = Project.GetParameter("ProNetPath") & "\" & RelativePath
                client.StartProjectAtPath(ProjectPath, ConnectionName)
            Else
                Message.AddWarning("The Project Network Path is not known." & vbCrLf)
            End If
        End If
    End Sub

    Public Sub CheckOpenProjectAtProNetPath(ByVal RelativePath As String, ByVal ConnectionName As String)
        'Check if the project at the specified Path (relative to the Project Network Path) is open.
        'Open it if it is not already open.
        'Open the Project at the specified Path using the specified Connection Name.

        Dim ProjectPath As String
        If RelativePath.StartsWith("\") Then
            If Project.ParameterExists("ProNetPath") Then
                ProjectPath = Project.GetParameter("ProNetPath") & RelativePath
                'client.StartProjectAtPath(ProjectPath, ConnectionName)
                If client.ProjectOpen(ProjectPath) Then
                    'Project is already open.
                Else
                    client.StartProjectAtPath(ProjectPath, ConnectionName)
                End If
            Else
                Message.AddWarning("The Project Network Path is not known." & vbCrLf)
            End If
        Else
            If Project.ParameterExists("ProNetPath") Then
                ProjectPath = Project.GetParameter("ProNetPath") & "\" & RelativePath
                'client.StartProjectAtPath(ProjectPath, ConnectionName)
                If client.ProjectOpen(ProjectPath) Then
                    'Project is already open.
                Else
                    client.StartProjectAtPath(ProjectPath, ConnectionName)
                End If
            Else
                Message.AddWarning("The Project Network Path is not known." & vbCrLf)
            End If
        End If
    End Sub


    Public Sub CloseProjectAtConnection(ByVal ProNetName As String, ByVal ConnectionName As String)
        'Close the Project at the specified connection.

        If IsNothing(client) Then
            Message.Add("No client connection available!" & vbCrLf)
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.Add("client state is faulted. Message not sent!" & vbCrLf)
            Else
                'Create the XML instructions to close the application at the connection.
                Dim decl As New XDeclaration("1.0", "utf-8", "yes")
                Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.
                Dim xmessage As New XElement("XMsg") 'This indicates the start of the message in the XMessage class

                'NOTE: No reply expected. No need to provide the following client information(?)
                'Dim clientConnName As New XElement("ClientConnectionName", Me.ConnectionName)
                'xmessage.Add(clientConnName)

                Dim command As New XElement("Command", "Close")
                xmessage.Add(command)
                doc.Add(xmessage)

                'Show the message sent:
                Message.XAddText("Message sent to: [" & ProNetName & "]." & ConnectionName & ":" & vbCrLf, "XmlSentNotice")
                Message.XAddXml(doc.ToString)
                Message.XAddText(vbCrLf, "Normal") 'Add extra line

                client.SendMessage(ProNetName, ConnectionName, doc.ToString)
            End If
        End If
    End Sub

    'END Open and Close Projects -----------------------------------------------------------------------------------


    'System Methods ================================================================================================

    Public Sub SaveHtmlSettings(ByVal xSettings As String, ByVal FileName As String)
        'Save the Html settings for a web page.

        'Convert the XSettings to XML format:
        Dim XmlHeader As String = "<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>"
        Dim XDocSettings As New System.Xml.Linq.XDocument

        Try
            XDocSettings = System.Xml.Linq.XDocument.Parse(XmlHeader & vbCrLf & xSettings)
        Catch ex As Exception
            Message.AddWarning("Error saving HTML settings file. " & ex.Message & vbCrLf)
        End Try

        Project.SaveXmlData(FileName, XDocSettings)
    End Sub

    Public Sub RestoreHtmlSettings()
        'Restore the Html settings for a web page.

        Dim SettingsFileName As String = WorkflowFileName & "Settings"
        Dim XDocSettings As New System.Xml.Linq.XDocument
        Project.ReadXmlData(SettingsFileName, XDocSettings)

        If XDocSettings Is Nothing Then
            'Message.Add("No HTML Settings file : " & SettingsFileName & vbCrLf)
        Else
            Dim XSettings As New System.Xml.XmlDocument
            Try
                XSettings.LoadXml(XDocSettings.ToString)
                'Run the Settings file:
                XSeq.RunXSequence(XSettings, Status)
            Catch ex As Exception
                Message.AddWarning("Error restoring HTML settings. " & ex.Message & vbCrLf)
            End Try
        End If
    End Sub

    Public Sub RestoreSetting(ByVal FormName As String, ByVal ItemName As String, ByVal ItemValue As String)
        'Restore the setting value with the specified Form Name and Item Name.
        Me.WebBrowser1.Document.InvokeScript("RestoreSetting", New String() {FormName, ItemName, ItemValue})
    End Sub

    Public Sub RestoreOption(ByVal SelectId As String, ByVal OptionText As String)
        'Restore the Option text in the Select control with the Id SelectId.
        Me.WebBrowser1.Document.InvokeScript("RestoreOption", New String() {SelectId, OptionText})
    End Sub

    Private Sub SaveWebPageSettings()
        'Call the SaveSettings JavaScript function:
        Try
            Me.WebBrowser1.Document.InvokeScript("SaveSettings")
        Catch ex As Exception
            Message.AddWarning("Web page settings not saved: " & ex.Message & vbCrLf)
        End Try
    End Sub

    'END System Methods --------------------------------------------------------------------------------------------


    'Legacy Code (These methods should no longer be used) ==========================================================

    Public Sub JSMethodTest1()
        'Test method that is called from JavaScript.
        Message.Add("JSMethodTest1 called OK." & vbCrLf)
    End Sub

    Public Sub JSMethodTest2(ByVal Var1 As String, ByVal Var2 As String)
        'Test method that is called from JavaScript.
        Message.Add("Var1 = " & Var1 & " Var2 = " & Var2 & vbCrLf)
    End Sub

    Public Sub JSDisplayXml(ByRef XDoc As XDocument)
        Message.Add(XDoc.ToString & vbCrLf & vbCrLf)
    End Sub

    Public Sub ShowMessage(ByVal Msg As String)
        Message.Add(Msg)
    End Sub

    Public Sub AddText(ByVal Msg As String, ByVal TextType As String)
        Message.AddText(Msg, TextType)
    End Sub

    'END Legacy Code -----------------------------------------------------------------------------------------------


#End Region 'Methods Called by JavaScript -------------------------------------------------------------------------------------------------------------------------------


#Region " Project Events Code"

    Private Sub Project_Message(Msg As String) Handles Project.Message
        'Display the Project message:
        Message.Add(Msg & vbCrLf)
    End Sub

    Private Sub Project_ErrorMessage(Msg As String) Handles Project.ErrorMessage
        'Display the Project error message:
        Message.AddWarning(Msg & vbCrLf)
    End Sub

    Private Sub Project_Closing() Handles Project.Closing
        'The current project is closing.
        CloseProject()
        'SaveFormSettings() 'Save the form settings - they are saved in the Project before is closes.
        'SaveProjectSettings() 'Update this subroutine if project settings need to be saved.
        'Project.Usage.SaveUsageInfo() 'Save the current project usage information.
        'Project.UnlockProject() 'Unlock the current project before it Is closed.
        'If ConnectedToComNet Then DisconnectFromComNet() 'ADDED 9Apr20
    End Sub


    Private Sub CloseProject()
        'Close the Project:
        SaveFormSettings() 'Save the form settings - they are saved in the Project before is closes.
        SaveProjectSettings() 'Update this subroutine if project settings need to be saved.
        Project.Usage.SaveUsageInfo() 'Save the current project usage information.
        Project.UnlockProject() 'Unlock the current project before it Is closed.
        If ConnectedToComNet Then DisconnectFromComNet() 'ADDED 9Apr20
    End Sub

    Private Sub Project_Selected() Handles Project.Selected
        'A new project has been selected.
        OpenProject()
        'RestoreFormSettings()
        'Project.ReadProjectInfoFile()

        'Project.ReadParameters()
        'Project.ReadParentParameters()
        'If Project.ParentParameterExists("ProNetName") Then
        '    Project.AddParameter("ProNetName", Project.ParentParameter("ProNetName").Value, Project.ParentParameter("ProNetName").Description) 'AddParameter will update the parameter if it already exists.
        '    ProNetName = Project.Parameter("ProNetName").Value
        'Else
        '    ProNetName = Project.GetParameter("ProNetName")
        'End If
        'If Project.ParentParameterExists("ProNetPath") Then 'Get the parent parameter value - it may have been updated.
        '    Project.AddParameter("ProNetPath", Project.ParentParameter("ProNetPath").Value, Project.ParentParameter("ProNetPath").Description) 'AddParameter will update the parameter if it already exists.
        '    ProNetPath = Project.Parameter("ProNetPath").Value
        'Else
        '    ProNetPath = Project.GetParameter("ProNetPath") 'If the parameter does not exist, the value is set to ""
        'End If
        'Project.SaveParameters() 'These should be saved now - child projects look for parent parameters in the parameter file.

        'Project.LockProject() 'Lock the project while it is open in this application.

        'Project.Usage.StartTime = Now

        'ApplicationInfo.SettingsLocn = Project.SettingsLocn
        'Message.SettingsLocn = Project.SettingsLocn
        'Message.Show() 'Added 18May19

        ''Restore the new project settings:
        'RestoreProjectSettings() 'Update this subroutine if project settings need to be restored.

        'ShowProjectInfo()

        'If Project.ConnectOnOpen Then
        '    ConnectToComNet() 'The Project is set to connect when it is opened.
        'ElseIf ApplicationInfo.ConnectOnStartup Then
        '    ConnectToComNet() 'The Application is set to connect when it is started.
        'Else
        '    'Don't connect to ComNet.
        'End If

    End Sub

    Private Sub OpenProject()
        'Open the Project:
        RestoreFormSettings()
        Project.ReadProjectInfoFile()

        Project.ReadParameters()
        Project.ReadParentParameters()
        If Project.ParentParameterExists("ProNetName") Then
            Project.AddParameter("ProNetName", Project.ParentParameter("ProNetName").Value, Project.ParentParameter("ProNetName").Description) 'AddParameter will update the parameter if it already exists.
            ProNetName = Project.Parameter("ProNetName").Value
        Else
            ProNetName = Project.GetParameter("ProNetName")
        End If
        If Project.ParentParameterExists("ProNetPath") Then 'Get the parent parameter value - it may have been updated.
            Project.AddParameter("ProNetPath", Project.ParentParameter("ProNetPath").Value, Project.ParentParameter("ProNetPath").Description) 'AddParameter will update the parameter if it already exists.
            ProNetPath = Project.Parameter("ProNetPath").Value
        Else
            ProNetPath = Project.GetParameter("ProNetPath") 'If the parameter does not exist, the value is set to ""
        End If
        Project.SaveParameters() 'These should be saved now - child projects look for parent parameters in the parameter file.

        Project.LockProject() 'Lock the project while it is open in this application.

        Project.Usage.StartTime = Now

        ApplicationInfo.SettingsLocn = Project.SettingsLocn
        Message.SettingsLocn = Project.SettingsLocn
        Message.Show() 'Added 18May19

        'Restore the new project settings:
        RestoreProjectSettings() 'Update this subroutine if project settings need to be restored.

        ShowProjectInfo()

        If Project.ConnectOnOpen Then
            ConnectToComNet() 'The Project is set to connect when it is opened.
        ElseIf ApplicationInfo.ConnectOnStartup Then
            ConnectToComNet() 'The Application is set to connect when it is started.
        Else
            'Don't connect to ComNet.
        End If
    End Sub

    Private Sub chkConnect_LostFocus(sender As Object, e As EventArgs) Handles chkConnect.LostFocus
        If chkConnect.Checked Then
            Project.ConnectOnOpen = True
        Else
            Project.ConnectOnOpen = False
        End If
        Project.SaveProjectInfoFile()
    End Sub

#End Region 'Project Events Code

#Region " Online/Offline Code" '=========================================================================================================================================

    Private Sub btnOnline_Click(sender As Object, e As EventArgs) Handles btnOnline.Click
        'Connect to or disconnect from the Message System (ComNet).
        If ConnectedToComNet = False Then
            ConnectToComNet()
        Else
            DisconnectFromComNet()
        End If
    End Sub

    Private Sub ConnectToComNet()
        'Connect to the Message Service. (ComNet)

        If IsNothing(client) Then
            client = New ServiceReference1.MsgServiceClient(New System.ServiceModel.InstanceContext(New MsgServiceCallback))
        End If

        'UPDATE 14 Feb 2021 - If the VS2019 version of the ADVL Network is running it may not detected by ComNetRunning()!
        'Check if the Message Service is running by trying to open a connection:
        Try
            client.Endpoint.Binding.SendTimeout = New System.TimeSpan(0, 0, 16) 'Temporarily set the send timeaout to 16 seconds (8 seconds is too short for a slow computer!)
            ConnectionName = ApplicationInfo.Name 'This name will be modified if it is already used in an existing connection.
            ConnectionName = client.Connect(ProNetName, ApplicationInfo.Name, ConnectionName, Project.Name, Project.Description, Project.Type, Project.Path, False, False)
            If ConnectionName <> "" Then
                Message.Add("Connected to the Andorville™ Network with Connection Name: [" & ProNetName & "]." & ConnectionName & vbCrLf)
                client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
                btnOnline.Text = "Online"
                btnOnline.ForeColor = Color.ForestGreen
                ConnectedToComNet = True
                SendApplicationInfo()
                SendProjectInfo()
                client.GetAdvlNetworkAppInfoAsync() 'Update the Exe Path in case it has changed. This path may be needed in the future to start the ComNet (Message Service).

                bgwComCheck.WorkerReportsProgress = True
                bgwComCheck.WorkerSupportsCancellation = True
                If bgwComCheck.IsBusy Then
                    'The ComCheck thread is already running.
                Else
                    bgwComCheck.RunWorkerAsync() 'Start the ComCheck thread.
                End If
                Exit Sub 'Connection made OK
            Else
                'Message.Add("Connection to the Andorville™ Network failed!" & vbCrLf)
                Message.Add("The Andorville™ Network was not found. Attempting to start it." & vbCrLf)
                client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
            End If
        Catch ex As System.TimeoutException
            Message.Add("Message Service Check Timeout error. Check if the Andorville™ Network (Message Service) is running." & vbCrLf)
            client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
            Message.Add("Attempting to start the Message Service." & vbCrLf)
        Catch ex As Exception
            Message.Add("Error message: " & ex.Message & vbCrLf)
            client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
            Message.Add("Attempting to start the Message Service." & vbCrLf)
        End Try

        If ComNetRunning() Then
            'The Application.Lock file has been found at AdvlNetworkAppPath
            'The Message Service is Running.
        Else 'The Message Service is NOT running'
            'Start the Andorville™ Network:
            If AdvlNetworkAppPath = "" Then
                Message.AddWarning("Andorville™ Network application path is unknown." & vbCrLf)
            Else
                If System.IO.File.Exists(AdvlNetworkExePath) Then 'OK to start the Message Service application:
                    Shell(Chr(34) & AdvlNetworkExePath & Chr(34), AppWinStyle.NormalFocus) 'Start Message Service application with no argument
                Else
                    'Incorrect Message Service Executable path.
                    Message.AddWarning("Andorville™ Network exe file not found. Service not started." & vbCrLf)
                End If
            End If
        End If

        'Try to fix a faulted client state:
        If client.State = ServiceModel.CommunicationState.Faulted Then
            client = Nothing
            client = New ServiceReference1.MsgServiceClient(New System.ServiceModel.InstanceContext(New MsgServiceCallback))
        End If

        If client.State = ServiceModel.CommunicationState.Faulted Then
            Message.AddWarning("Client state is faulted. Connection not made!" & vbCrLf)
        Else
            Try
                client.Endpoint.Binding.SendTimeout = New System.TimeSpan(0, 0, 16) 'Temporarily set the send timeaout to 16 seconds (8 seconds is too short for a slow computer!)

                ConnectionName = ApplicationInfo.Name 'This name will be modified if it is already used in an existing connection.
                ConnectionName = client.Connect(ProNetName, ApplicationInfo.Name, ConnectionName, Project.Name, Project.Description, Project.Type, Project.Path, False, False)

                If ConnectionName <> "" Then
                    Message.Add("Connected to the Andorville™ Network with Connection Name: [" & ProNetName & "]." & ConnectionName & vbCrLf)
                    client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
                    btnOnline.Text = "Online"
                    btnOnline.ForeColor = Color.ForestGreen
                    ConnectedToComNet = True
                    SendApplicationInfo()
                    SendProjectInfo()
                    client.GetAdvlNetworkAppInfoAsync() 'Update the Exe Path in case it has changed. This path may be needed in the future to start the ComNet (Message Service).

                    bgwComCheck.WorkerReportsProgress = True
                    bgwComCheck.WorkerSupportsCancellation = True
                    If bgwComCheck.IsBusy Then
                        'The ComCheck thread is already running.
                    Else
                        bgwComCheck.RunWorkerAsync() 'Start the ComCheck thread.
                    End If

                Else
                    Message.Add("Connection to the Andorville™ Network failed!" & vbCrLf)
                    client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
                End If
            Catch ex As System.TimeoutException
                Message.Add("Timeout error. Check if the Andorville™ Network (Message Service) is running." & vbCrLf)
            Catch ex As Exception
                Message.Add("Error message: " & ex.Message & vbCrLf)
                client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
            End Try
        End If
    End Sub

    Private Sub ConnectToComNet(ByVal ConnName As String)
        'Connect to the Message Service (ComNet) with the connection name ConnName.

        'UPDATE 14 Feb 2021 - If the VS2019 version of the ADVL Network is running it may not be detected by ComNetRunning()!
        'Check if the Message Service is running by trying to open a connection:

        If IsNothing(client) Then
            client = New ServiceReference1.MsgServiceClient(New System.ServiceModel.InstanceContext(New MsgServiceCallback))
        End If

        Try
            client.Endpoint.Binding.SendTimeout = New System.TimeSpan(0, 0, 16) 'Temporarily set the send timeaout to 16 seconds (8 seconds is too short for a slow computer!)
            ConnectionName = ConnName 'This name will be modified if it is already used in an existing connection.
            ConnectionName = client.Connect(ProNetName, ApplicationInfo.Name, ConnectionName, Project.Name, Project.Description, Project.Type, Project.Path, False, False)
            If ConnectionName <> "" Then
                Message.Add("Connected to the Andorville™ Network with Connection Name: [" & ProNetName & "]." & ConnectionName & vbCrLf)
                client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
                btnOnline.Text = "Online"
                btnOnline.ForeColor = Color.ForestGreen
                ConnectedToComNet = True
                SendApplicationInfo()
                SendProjectInfo()
                client.GetAdvlNetworkAppInfoAsync() 'Update the Exe Path in case it has changed. This path may be needed in the future to start the ComNet (Message Service).

                bgwComCheck.WorkerReportsProgress = True
                bgwComCheck.WorkerSupportsCancellation = True
                If bgwComCheck.IsBusy Then
                    'The ComCheck thread is already running.
                Else
                    bgwComCheck.RunWorkerAsync() 'Start the ComCheck thread.
                End If
                Exit Sub 'Connection made OK
            Else
                'Message.Add("Connection to the Andorville™ Network failed!" & vbCrLf)
                Message.Add("The Andorville™ Network was not found. Attempting to start it." & vbCrLf)
                client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
            End If
        Catch ex As System.TimeoutException
            Message.Add("Message Service Check Timeout error. Check if the Andorville™ Network (Message Service) is running." & vbCrLf)
            client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
            Message.Add("Attempting to start the Message Service." & vbCrLf)
        Catch ex As Exception
            Message.Add("Error message: " & ex.Message & vbCrLf)
            client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
            Message.Add("Attempting to start the Message Service." & vbCrLf)
        End Try


        If ConnectedToComNet = False Then
            If IsNothing(client) Then
                client = New ServiceReference1.MsgServiceClient(New System.ServiceModel.InstanceContext(New MsgServiceCallback))
            End If

            'Try to fix a faulted client state:
            If client.State = ServiceModel.CommunicationState.Faulted Then
                client = Nothing
                client = New ServiceReference1.MsgServiceClient(New System.ServiceModel.InstanceContext(New MsgServiceCallback))
            End If

            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.AddWarning("client state is faulted. Connection not made!" & vbCrLf)
            Else
                Try
                    client.Endpoint.Binding.SendTimeout = New System.TimeSpan(0, 0, 16) 'Temporarily set the send timeout to 16 seconds (8 seconds is too short for a slow computer!)
                    ConnectionName = ConnName 'This name will be modified if it is already used in an existing connection.
                    ConnectionName = client.Connect(ProNetName, ApplicationInfo.Name, ConnectionName, Project.Name, Project.Description, Project.Type, Project.Path, False, False)

                    If ConnectionName <> "" Then
                        Message.Add("Connected to the Andorville™ Network with Connection Name: [" & ProNetName & "]." & ConnectionName & vbCrLf)
                        client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
                        btnOnline.Text = "Online"
                        btnOnline.ForeColor = Color.ForestGreen
                        ConnectedToComNet = True
                        SendApplicationInfo()
                        SendProjectInfo()
                        client.GetAdvlNetworkAppInfoAsync() 'Update the Exe Path in case it has changed. This path may be needed in the future to start the ComNet (Message Service).

                        bgwComCheck.WorkerReportsProgress = True
                        bgwComCheck.WorkerSupportsCancellation = True
                        If bgwComCheck.IsBusy Then
                            'The ComCheck thread is already running.
                        Else
                            bgwComCheck.RunWorkerAsync() 'Start the ComCheck thread.
                        End If

                    Else
                        Message.Add("Connection to the Andorville™ Network failed!" & vbCrLf)
                        client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
                    End If
                Catch ex As System.TimeoutException
                    Message.Add("Timeout error. Check if the Andorville™ Network (Message Service) is running." & vbCrLf)
                Catch ex As Exception
                    Message.Add("Error message: " & ex.Message & vbCrLf)
                    client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
                End Try
            End If
        Else
            Message.AddWarning("Already connected to the Andorville™ Network (Message Service)." & vbCrLf)
        End If
    End Sub

    Private Sub DisconnectFromComNet()
        'Disconnect from the Communication Network (Message Service).

        If ConnectedToComNet = True Then
            If IsNothing(client) Then
                Message.Add("Already disconnected from the Andorville™ Network (Message Service)." & vbCrLf)
                btnOnline.Text = "Offline"
                btnOnline.ForeColor = Color.Red
                ConnectedToComNet = False
                ConnectionName = ""
            Else
                If client.State = ServiceModel.CommunicationState.Faulted Then
                    Message.Add("client state is faulted." & vbCrLf)
                    ConnectionName = ""
                Else
                    Try
                        client.Disconnect(ProNetName, ConnectionName)
                        btnOnline.Text = "Offline"
                        btnOnline.ForeColor = Color.Red
                        ConnectedToComNet = False
                        ConnectionName = ""
                        Message.Add("Disconnected from the Andorville™ Network (Message Service)." & vbCrLf)

                        If bgwComCheck.IsBusy Then
                            bgwComCheck.CancelAsync()
                        End If

                    Catch ex As Exception
                        Message.AddWarning("Error disconnecting from Andorville™ Network (Message Service): " & ex.Message & vbCrLf)
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub SendApplicationInfo()
        'Send the application information to the Network application.

        If IsNothing(client) Then
            Message.Add("No client connection available!" & vbCrLf)
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.Add("Client state is faulted. Message not sent!" & vbCrLf)
            Else
                'Create the XML instructions to send application information.
                Dim decl As New XDeclaration("1.0", "utf-8", "yes")
                Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.
                Dim xmessage As New XElement("XMsg") 'This indicates the start of the message in the XMessage class
                Dim applicationInfo As New XElement("ApplicationInfo")
                Dim name As New XElement("Name", Me.ApplicationInfo.Name)
                applicationInfo.Add(name)

                Dim text As New XElement("Text", "Monte Carlo")
                applicationInfo.Add(text)

                Dim exePath As New XElement("ExecutablePath", Me.ApplicationInfo.ExecutablePath)
                applicationInfo.Add(exePath)

                Dim directory As New XElement("Directory", Me.ApplicationInfo.ApplicationDir)
                applicationInfo.Add(directory)
                Dim description As New XElement("Description", Me.ApplicationInfo.Description)
                applicationInfo.Add(description)
                xmessage.Add(applicationInfo)
                doc.Add(xmessage)

                'Show the message sent to ComNet:
                Message.XAddText("Message sent to " & "Message Service" & ":" & vbCrLf, "XmlSentNotice")
                Message.XAddXml(doc.ToString)
                Message.XAddText(vbCrLf, "Normal") 'Add extra line

                client.SendMessage("", "MessageService", doc.ToString)
            End If
        End If
    End Sub

    Private Sub SendProjectInfo()
        'Send the project information to the Network application.

        If ConnectedToComNet = False Then
            Message.AddWarning("The application is not connected to the Message Service." & vbCrLf)
        Else 'Connected to the Message Service (ComNet).
            If IsNothing(client) Then
                Message.Add("No client connection available!" & vbCrLf)
            Else
                If client.State = ServiceModel.CommunicationState.Faulted Then
                    Message.Add("Client state is faulted. Message not sent!" & vbCrLf)
                Else
                    'Construct the XMessage to send to AppNet:
                    Dim decl As New XDeclaration("1.0", "utf-8", "yes")
                    Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.
                    Dim xmessage As New XElement("XMsg") 'This indicates the start of the message in the XMessage class
                    Dim projectInfo As New XElement("ProjectInfo")

                    Dim Path As New XElement("Path", Project.Path)
                    projectInfo.Add(Path)
                    xmessage.Add(projectInfo)
                    doc.Add(xmessage)

                    'Show the message sent to the Message Service:
                    Message.XAddText("Message sent to " & "Message Service" & ":" & vbCrLf, "XmlSentNotice")
                    Message.XAddXml(doc.ToString)
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                    client.SendMessage("", "MessageService", doc.ToString)
                End If
            End If
        End If
    End Sub

    Public Sub SendProjectInfo(ByVal ProjectPath As String)
        'Send the project information to the Network application.
        'This version of SendProjectInfo uses the ProjectPath argument.

        If ConnectedToComNet = False Then
            Message.AddWarning("The application is not connected to the Message Service." & vbCrLf)
        Else 'Connected to the Message Service (ComNet).
            If IsNothing(client) Then
                Message.Add("No client connection available!" & vbCrLf)
            Else
                If client.State = ServiceModel.CommunicationState.Faulted Then
                    Message.Add("Client state is faulted. Message not sent!" & vbCrLf)
                Else
                    'Construct the XMessage to send to AppNet:
                    Dim decl As New XDeclaration("1.0", "utf-8", "yes")
                    Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.
                    Dim xmessage As New XElement("XMsg") 'This indicates the start of the message in the XMessage class
                    Dim projectInfo As New XElement("ProjectInfo")

                    'Dim Path As New XElement("Path", Project.Path)
                    Dim Path As New XElement("Path", ProjectPath)
                    projectInfo.Add(Path)
                    xmessage.Add(projectInfo)
                    doc.Add(xmessage)

                    'Show the message sent to the Message Service:
                    Message.XAddText("Message sent to " & "Message Service" & ":" & vbCrLf, "XmlSentNotice")
                    Message.XAddXml(doc.ToString)
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                    client.SendMessage("", "MessageService", doc.ToString)
                End If
            End If
        End If
    End Sub

    Private Function ComNetRunning() As Boolean
        'Return True if ComNet (Message Service) is running.
        ''If System.IO.File.Exists(MsgServiceAppPath & "\Application.Lock") Then
        'If System.IO.File.Exists(AdvlNetworkAppPath & "\Application.Lock") Then
        '    Return True
        'Else
        '    Return False
        'End If

        'If MsgServiceAppPath = "" Then
        If AdvlNetworkAppPath = "" Then
            'Message.Add("Message Service application path is not known." & vbCrLf)
            Message.Add("Andorville™ Network application path is not known." & vbCrLf)
            'Message.Add("Run the Message Service before connecting to update the path." & vbCrLf)
            Message.Add("Run the Andorville™ Network before connecting to update the path." & vbCrLf)
            Return False
        Else
            'If System.IO.File.Exists(MsgServiceAppPath & "\Application.Lock") Then
            If System.IO.File.Exists(AdvlNetworkAppPath & "\Application.Lock") Then
                'Message.Add("AppLock found - ComNet is running." & vbCrLf)
                Return True
            Else
                'Message.Add("AppLock not found - ComNet is running." & vbCrLf)
                Return False
            End If
        End If

    End Function

#End Region 'Online/Offline code ----------------------------------------------------------------------------------------------------------------------------------------

    Private Sub TabPage2_Enter(sender As Object, e As EventArgs) Handles TabPage2.Enter
        'Update the current duration:

        'txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & ":" &
        '                           Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & ":" &
        '                           Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & ":" &
        '                           Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c)

        txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & "d:" &
                                   Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & "h:" &
                                   Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & "m:" &
                                   Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c) & "s"

        Timer1.Interval = 5000 '5 seconds
        Timer1.Enabled = True
        Timer1.Start()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'Update the current duration:

        'txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & ":" &
        '                   Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & ":" &
        '                   Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & ":" &
        '                   Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c)

        txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & "d:" &
                           Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & "h:" &
                           Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & "m:" &
                           Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c) & "s"
    End Sub

    Private Sub TabPage2_Leave(sender As Object, e As EventArgs) Handles TabPage2.Leave
        Timer1.Enabled = False
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        'Add the current project to the Message Service list.

        If Project.ParentProjectName <> "" Then
            Message.AddWarning("This project has a parent: " & Project.ParentProjectName & vbCrLf)
            Message.AddWarning("Child projects can not be added to the list." & vbCrLf)
            Exit Sub
        End If

        If ConnectedToComNet = False Then
            Message.AddWarning("The application is not connected to the Message Service." & vbCrLf)
        Else 'Connected to the Message Service (ComNet).
            If IsNothing(client) Then
                Message.Add("No client connection available!" & vbCrLf)
            Else
                If client.State = ServiceModel.CommunicationState.Faulted Then
                    Message.Add("Client state is faulted. Message not sent!" & vbCrLf)
                Else
                    'Construct the XMessage to send to AppNet:
                    Dim decl As New XDeclaration("1.0", "utf-8", "yes")
                    Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.
                    Dim xmessage As New XElement("XMsg") 'This indicates the start of the message in the XMessage class
                    Dim projectInfo As New XElement("ProjectInfo")

                    Dim Path As New XElement("Path", Project.Path)
                    projectInfo.Add(Path)
                    xmessage.Add(projectInfo)
                    doc.Add(xmessage)

                    'Show the message sent to AppNet:
                    Message.XAddText("Message sent to " & "Message Service" & ":" & vbCrLf, "XmlSentNotice")
                    Message.XAddXml(doc.ToString)
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                    client.SendMessage("", "MessageService", doc.ToString)
                End If
            End If
        End If
    End Sub

    Private Sub btnOpenProject_Click(sender As Object, e As EventArgs) Handles btnOpenProject.Click
        'Open the Project directory or archive.
        If Project.Type = ADVL_Utilities_Library_1.Project.Types.Archive Then
            If IsNothing(ProjectArchive) Then
                ProjectArchive = New frmArchive
                ProjectArchive.Show()
                'ProjectArchive.Text = "Project Archive"
                ProjectArchive.Title = "Project Archive"
                ProjectArchive.Path = Project.Path
            Else
                ProjectArchive.Show()
                ProjectArchive.BringToFront()
            End If
        Else
            Process.Start(Project.Path)
        End If
    End Sub

    Private Sub ProjectArchive_FormClosed(sender As Object, e As FormClosedEventArgs) Handles ProjectArchive.FormClosed
        ProjectArchive = Nothing
    End Sub

    Private Sub btnOpenSettings_Click(sender As Object, e As EventArgs) Handles btnOpenSettings.Click
        'Open the Settings directory or archive.
        If Project.SettingsLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Directory Then
            Process.Start(Project.SettingsLocn.Path)
        ElseIf Project.SettingsLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Archive Then
            If IsNothing(SettingsArchive) Then
                SettingsArchive = New frmArchive
                SettingsArchive.Show()
                'SettingsArchive.Text = "Settings Archive"
                SettingsArchive.Title = "Settings Archive"
                SettingsArchive.Path = Project.SettingsLocn.Path
            Else
                SettingsArchive.Show()
                SettingsArchive.BringToFront()
            End If
        End If
    End Sub

    Private Sub SettingsArchive_FormClosed(sender As Object, e As FormClosedEventArgs) Handles SettingsArchive.FormClosed
        SettingsArchive = Nothing
    End Sub

    Private Sub btnOpenData_Click(sender As Object, e As EventArgs) Handles btnOpenData.Click
        'Open the Data directory or archive.
        If Project.DataLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Directory Then
            Process.Start(Project.DataLocn.Path)
        ElseIf Project.DataLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Archive Then
            If IsNothing(DataArchive) Then
                DataArchive = New frmArchive
                DataArchive.Show()
                'DataArchive.Text = "Data Archive"
                DataArchive.Title = "Data Archive"
                DataArchive.Path = Project.DataLocn.Path
            Else
                DataArchive.Show()
                DataArchive.BringToFront()
            End If
        End If
    End Sub

    Private Sub DataArchive_FormClosed(sender As Object, e As FormClosedEventArgs) Handles DataArchive.FormClosed
        DataArchive = Nothing
    End Sub

    Private Sub btnOpenSystem_Click(sender As Object, e As EventArgs) Handles btnOpenSystem.Click
        'Open the System directory or archive.
        If Project.SystemLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Directory Then
            Process.Start(Project.SystemLocn.Path)
        ElseIf Project.SystemLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Archive Then
            If IsNothing(SystemArchive) Then
                SystemArchive = New frmArchive
                SystemArchive.Show()
                'SystemArchive.Text = "System Archive"
                SystemArchive.Title = "System Archive"
                SystemArchive.Path = Project.SystemLocn.Path
            Else
                SystemArchive.Show()
                SystemArchive.BringToFront()
            End If
        End If
    End Sub

    Private Sub SystemArchive_FormClosed(sender As Object, e As FormClosedEventArgs) Handles SystemArchive.FormClosed
        SystemArchive = Nothing
    End Sub

    Private Sub btnOpenAppDir_Click(sender As Object, e As EventArgs) Handles btnOpenAppDir.Click
        Process.Start(ApplicationInfo.ApplicationDir)
    End Sub

    Private Sub btnOpenParentDir_Click(sender As Object, e As EventArgs) Handles btnOpenParentDir.Click
        'Open the Parent directory of the selected project.
        Dim ParentDir As String = System.IO.Directory.GetParent(Project.Path).FullName
        If System.IO.Directory.Exists(ParentDir) Then
            Process.Start(ParentDir)
        Else
            Message.AddWarning("The parent directory was not found: " & ParentDir & vbCrLf)
        End If
    End Sub

    Private Sub btnCreateArchive_Click(sender As Object, e As EventArgs) Handles btnCreateArchive.Click
        'Create a Project Archive file.
        If Project.Type = ADVL_Utilities_Library_1.Project.Types.Archive Then
            'The project is contained in a .AdvlProject file.
            'This file will be saved in a zip file in the parent directory with the same name but with extension .AdvlArchive

            'Dim ParentDir As String = System.IO.Directory.GetParent(Project.Path).FullName
            'Dim ProjectArchiveName As String = System.IO.Path.GetFileNameWithoutExtension(Project.Path) & ".AdvlArchive"

            'If My.Computer.FileSystem.FileExists(ParentDir & "\" & ProjectArchiveName) Then 'The Project Archive file already exists.
            '    Message.Add("The Project Archive file already exists: " & ParentDir & "\" & ProjectArchiveName & vbCrLf)
            'Else 'The Project Archive file does not exist. OK to create the Archive.
            '    System.IO.Compression.ZipFile.CreateFromDirectory(Project.Path, ParentDir & "\" & ProjectArchiveName)

            '    'Remove all Lock files:
            '    'To Do

            '    Message.Add("Project Archive file created: " & ParentDir & "\" & ProjectArchiveName & vbCrLf)
            'End If

            'UPDATE: .AdvlProject files are alreay zip files. No need to generate an archive.
            Message.Add("The Project is an Archive type. It is already in an archived format." & vbCrLf)

        Else
            'The project is contained in the directory Project.Path.
            'This directory and contents will be saved in a zip file in the parent directory with the same name but with extension .AdvlArchive.

            Dim ParentDir As String = System.IO.Directory.GetParent(Project.Path).FullName
            Dim ProjectArchiveName As String = System.IO.Path.GetFileName(Project.Path) & ".AdvlArchive"

            If My.Computer.FileSystem.FileExists(ParentDir & "\" & ProjectArchiveName) Then 'The Project Archive file already exists.
                Message.Add("The Project Archive file already exists: " & ParentDir & "\" & ProjectArchiveName & vbCrLf)
            Else 'The Project Archive file does not exist. OK to create the Archive.
                System.IO.Compression.ZipFile.CreateFromDirectory(Project.Path, ParentDir & "\" & ProjectArchiveName)

                'Remove all Lock files:
                Dim Zip As System.IO.Compression.ZipArchive
                Zip = System.IO.Compression.ZipFile.Open(ParentDir & "\" & ProjectArchiveName, IO.Compression.ZipArchiveMode.Update)
                Dim DeleteList As New List(Of String) 'List of entry names to delete
                Dim myEntry As System.IO.Compression.ZipArchiveEntry
                For Each entry As System.IO.Compression.ZipArchiveEntry In Zip.Entries
                    If entry.Name = "Project.Lock" Then
                        'entry.Delete()
                        DeleteList.Add(entry.FullName)
                    End If
                Next
                For Each item In DeleteList
                    myEntry = Zip.GetEntry(item)
                    myEntry.Delete()
                Next
                Zip.Dispose()

                Message.Add("Project Archive file created: " & ParentDir & "\" & ProjectArchiveName & vbCrLf)
            End If
        End If
    End Sub

    Private Sub TabPage2_DragEnter(sender As Object, e As DragEventArgs) Handles TabPage2.DragEnter
        'DragEnter: An object has been dragged into TabPage2 - Project Information tab.
        'This code is required to get the link to the item(s) being dragged into Project Information:
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Link
        End If
    End Sub

    Private Sub TabPage2_DragDrop(sender As Object, e As DragEventArgs) Handles TabPage2.DragDrop
        'A file has been dropped into the Project Information tab.

        Dim Path As String()
        Path = e.Data.GetData(DataFormats.FileDrop)
        Dim I As Integer

        If Path.Count > 0 Then
            If Path.Count > 1 Then
                Message.AddWarning("More than one file has been dropped into the Project Information tab. Only the first one will be opened." & vbCrLf)
            End If

            Try
                Dim ArchivedProjectPath As String = Path(0)
                If ArchivedProjectPath.EndsWith(".AdvlArchive") Then
                    Message.Add("The archived project will be opened: " & vbCrLf & ArchivedProjectPath & vbCrLf)
                    OpenArchivedProject(ArchivedProjectPath)
                Else
                    Message.Add("The dropped file is not an archived project: " & vbCrLf & ArchivedProjectPath & vbCrLf)
                End If
            Catch ex As Exception
                Message.AddWarning("Error opening dropped archived project. " & ex.Message & vbCrLf)
            End Try
        End If

    End Sub

    Private Sub btnOpenArchive_Click(sender As Object, e As EventArgs) Handles btnOpenArchive.Click
        'Open a Project Archive file.

        'Use the OpenFileDialog to look for an .AdvlArchive file.
        'Start looking in the ParentDir.

        OpenFileDialog1.Title = "Select an Archived Project File"
        OpenFileDialog1.InitialDirectory = System.IO.Directory.GetParent(Project.Path).FullName
        OpenFileDialog1.Filter = "Archived Project|*.AdvlArchive"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim FileName As String = OpenFileDialog1.FileName
            OpenArchivedProject(FileName)
            ''Message.Add("Selected Archived Project file: " & FileName & vbCrLf)
            'Dim Zip As System.IO.Compression.ZipArchive
            'Try
            '    Zip = System.IO.Compression.ZipFile.OpenRead(FileName)

            '    Dim Entry As System.IO.Compression.ZipArchiveEntry = Zip.GetEntry("Project_Info_ADVL_2.xml")
            '    If IsNothing(Entry) Then
            '        Message.AddWarning("The file is not an Archived Andorville Project." & vbCrLf)
            '        'Check if it has a .AdvlProject extension -  
            '        'NOTE: the Archive project type, with the .AdvlProject extension will be deprecated.
            '        'Hybrid projects are the preferred type.

            '    Else
            '        Message.Add("The file is an Archived Andorville Project." & vbCrLf)
            '        Dim ParentDir As String = System.IO.Directory.GetParent(FileName).FullName
            '        Dim ProjectName As String = System.IO.Path.GetFileNameWithoutExtension(FileName)
            '        Message.Add("The Project will be expanded in the directory: " & ParentDir & vbCrLf)
            '        Message.Add("The Project name will be: " & ProjectName & vbCrLf)
            '        Zip.Dispose()
            '        If System.IO.Directory.Exists(ParentDir & "\" & ProjectName) Then
            '            Message.AddWarning("The Project already exists: " & ParentDir & "\" & ProjectName & vbCrLf)
            '        Else
            '            System.IO.Compression.ZipFile.ExtractToDirectory(FileName, ParentDir & "\" & ProjectName)
            '        End If
            '    End If
            'Catch ex As Exception
            '    Message.AddWarning("Error opening Archived Andorville Project: " & ex.Message & vbCrLf)
            'End Try
        End If
    End Sub

    Private Sub OpenArchivedProject(ByVal FilePath As String)
        'Open the archived project at the specified path.

        Dim Zip As System.IO.Compression.ZipArchive
        Try
            Zip = System.IO.Compression.ZipFile.OpenRead(FilePath)

            Dim Entry As System.IO.Compression.ZipArchiveEntry = Zip.GetEntry("Project_Info_ADVL_2.xml")
            If IsNothing(Entry) Then
                Message.AddWarning("The file is not an Archived Andorville Project." & vbCrLf)
                'Check if it is an Archive project type with a .AdvlProject extension.
                'NOTE: the Archive project type, with the .AdvlProject extension may be deprecated. (Note these are already zip files so no need to archive.)
                'Hybrid projects are the preferred type.

            Else
                Message.Add("The file is an Archived Andorville Project." & vbCrLf)
                Dim ParentDir As String = System.IO.Directory.GetParent(FilePath).FullName
                Dim ProjectName As String = System.IO.Path.GetFileNameWithoutExtension(FilePath)
                Message.Add("The Project will be expanded in the directory: " & ParentDir & vbCrLf)
                Message.Add("The Project name will be: " & ProjectName & vbCrLf)
                Zip.Dispose()
                If System.IO.Directory.Exists(ParentDir & "\" & ProjectName) Then
                    Message.AddWarning("The Project already exists: " & ParentDir & "\" & ProjectName & vbCrLf)
                Else
                    System.IO.Compression.ZipFile.ExtractToDirectory(FilePath, ParentDir & "\" & ProjectName) 'Extract the project from the archive
                    'AddProjectToList(ParentDir & "\" & ProjectName) 'Add the project to the list
                    Project.AddProjectToList(ParentDir & "\" & ProjectName) 'UPDATED CODE 12May21
                    'Open the project
                    'Project_Closing() 'Close the current project
                    CloseProject()  'Close the current project
                    Project.SelectProject(ParentDir & "\" & ProjectName) 'Select the project at the specifed path.
                    OpenProject() 'Open the selected project.
                End If
            End If
        Catch ex As Exception
            Message.AddWarning("Error opening Archived Andorville Project: " & ex.Message & vbCrLf)
        End Try

    End Sub

    'REMOVE: THIS CODE IS NOW IN The Project class:
    'Private Sub AddProjectToList(ByVal ProjectPath As String)
    '    'Add the Project at ProjectPath to the Project List.

    '    Dim ProjectInfoXDoc As System.Xml.Linq.XDocument = XDocument.Load(ProjectPath & "\Project_Info_ADVL_2.xml")
    '    Dim ProjectSummary As New ADVL_Utilities_Library_1.ProjectSummary

    '    If ProjectInfoXDoc Is Nothing Then
    '        Message.AddWarning("No project information was found. The project was not added to the list." & vbCrLf)
    '    Else
    '        If ProjectInfoXDoc.<Project>.<Application>.<Name>.Value <> ApplicationInfo.Name Then
    '            Message.AddWarning("The Project Application Name is: " & ProjectInfoXDoc.<Project>.<Application>.<Name>.Value & vbCrLf)
    '            Message.AddWarning("This does not match the current Application Name: " & ApplicationInfo.Name & vbCrLf)
    '        Else
    '            Select Case ProjectInfoXDoc.<Project>.<Type>.Value
    '                Case "Directory"
    '                    ProjectSummary.AuthorName = ProjectInfoXDoc.<Project>.<Author>.<Name>.Value
    '                    ProjectSummary.CreationDate = ProjectInfoXDoc.<Project>.<CreationDate>.Value
    '                    ProjectSummary.Description = ProjectInfoXDoc.<Project>.<Description>.Value
    '                    ProjectSummary.Name = ProjectInfoXDoc.<Project>.<Name>.Value
    '                    ProjectSummary.Path = ProjectPath
    '                    ProjectSummary.Type = Project.Types.Directory
    '                    AddProject(ProjectSummary)

    '                Case "Hybrid"
    '                    ProjectSummary.AuthorName = ProjectInfoXDoc.<Project>.<Author>.<Name>.Value
    '                    ProjectSummary.CreationDate = ProjectInfoXDoc.<Project>.<CreationDate>.Value
    '                    ProjectSummary.Description = ProjectInfoXDoc.<Project>.<Description>.Value
    '                    ProjectSummary.Name = ProjectInfoXDoc.<Project>.<Name>.Value
    '                    ProjectSummary.Path = ProjectPath
    '                    ProjectSummary.Type = Project.Types.Hybrid
    '                    AddProject(ProjectSummary)

    '                Case Else

    '            End Select
    '        End If
    '    End If
    'End Sub


    'REMOVE: THIS CODE IS NOW IN The Project class:
    'Private Sub AddProject(ByRef Summary As ADVL_Utilities_Library_1.ProjectSummary)
    '    'Add the Project summary information to the project list.

    '    Dim ProjectList As New List(Of ADVL_Utilities_Library_1.ProjectSummary) 'List of projects

    '    'Read the Project list:
    '    If System.IO.File.Exists(ApplicationInfo.ApplicationDir & "\Project_List_ADVL_2.xml") Then 'The latest ADVL_2 format version of the Project List file exists.
    '        Dim ProjectListXDoc As System.Xml.Linq.XDocument = XDocument.Load(ApplicationInfo.ApplicationDir & "\Project_List_ADVL_2.xml")
    '        'ReadProjectListAdvl_2(ProjectListXDoc)
    '        Dim Projects = From item In ProjectListXDoc.<ProjectList>.<Project>
    '        For Each item In Projects
    '            Dim NewProject As New ADVL_Utilities_Library_1.ProjectSummary
    '            NewProject.Name = item.<Name>.Value
    '            NewProject.Description = item.<Description>.Value
    '            Select Case item.<Type>.Value
    '                Case "None"
    '                    NewProject.Type = Project.Types.None
    '                Case "Directory"
    '                    NewProject.Type = Project.Types.Directory
    '                Case "Archive"
    '                    NewProject.Type = Project.Types.Archive
    '                Case "Hybrid"
    '                    NewProject.Type = Project.Types.Hybrid
    '            End Select
    '            NewProject.Path = item.<Path>.Value
    '            NewProject.CreationDate = item.<CreationDate>.Value
    '            NewProject.AuthorName = item.<AuthorName>.Value
    '            If item.<Status>.Value = Nothing Then
    '                'The Project list file records do not contain the Status field.
    '            Else
    '                NewProject.Status = item.<Status>.Value
    '            End If
    '            ProjectList.Add(NewProject)
    '        Next

    '        'Add the new project to the list:
    '        ProjectList.Add(Summary)

    '        'Write the Project list:
    '        Dim UpdatedProjectListXDoc = <?xml version="1.0" encoding="utf-8"?>
    '                                     <!---->
    '                                     <!--Project List File-->
    '                                     <ProjectList>
    '                                         <FormatCode>ADVL_2</FormatCode>
    '                                         <ApplicationName><%= ApplicationInfo.Name %></ApplicationName>
    '                                         <%= From item In ProjectList
    '                                             Select
    '                                      <Project>
    '                                          <Name><%= item.Name %></Name>
    '                                          <Description><%= item.Description %></Description>
    '                                          <Type><%= item.Type %></Type>
    '                                          <Path><%= item.Path %></Path>
    '                                          <CreationDate><%= Format(item.CreationDate, "d-MMM-yyyy H:mm:ss") %></CreationDate>
    '                                          <AuthorName><%= item.AuthorName %></AuthorName>
    '                                          <Status><%= item.Status %></Status>
    '                                      </Project>
    '                                         %>
    '                                     </ProjectList>

    '        UpdatedProjectListXDoc.Save(ApplicationInfo.ApplicationDir & "\Project_List_ADVL_2.xml")
    '    Else
    '        Message.AddWarning("The project list was not found. The project was not added." & vbCrLf)
    '    End If
    'End Sub




#Region " Process XMessages" '===========================================================================================================================================

    Private Sub XMsg_Instruction(Data As String, Locn As String) Handles XMsg.Instruction
        'Process an XMessage instruction.
        'An XMessage is a simplified XSequence. It is used to exchange information between Andorville™ applications.
        '
        'An XSequence file is an AL-H7™ Information Sequence stored in an XML format.
        'AL-H7™ is the name of a programming system that uses sequences of data and location value pairs to store information or processing steps.
        'Any program, mathematical expression or data set can be expressed as an Information Sequence.

        'Add code here to process the XMessage instructions.
        'See other Andorville™ applications for examples.

        If IsDBNull(Data) Then
            Data = ""
        End If

        'Intercept instructions with the prefix "WebPage_"
        If Locn.StartsWith("WebPage_") Then 'Send the Data, Location data to the correct Web Page:
            'Message.Add("Web Page Location: " & Locn & vbCrLf)
            If Locn.Contains(":") Then
                Dim EndOfWebPageNoString As Integer = Locn.IndexOf(":")
                If Locn.Contains("-") Then
                    Dim HyphenLocn As Integer = Locn.IndexOf("-")
                    If HyphenLocn < EndOfWebPageNoString Then 'Web Page Location contains a sub-location in the web page - WebPage_1-SubLocn:Locn - SubLocn:Locn will be sent to Web page 1
                        EndOfWebPageNoString = HyphenLocn
                    End If
                End If
                Dim PageNoLen As Integer = EndOfWebPageNoString - 8
                Dim WebPageNoString As String = Locn.Substring(8, PageNoLen)
                Dim WebPageNo As Integer = CInt(WebPageNoString)
                Dim WebPageData As String = Data
                Dim WebPageLocn As String = Locn.Substring(EndOfWebPageNoString + 1)

                'Message.Add("WebPageData = " & WebPageData & "  WebPageLocn = " & WebPageLocn & vbCrLf)

                WebPageFormList(WebPageNo).XMsgInstruction(WebPageData, WebPageLocn)
            Else
                Message.AddWarning("XMessage instruction location is not complete: " & Locn & vbCrLf)
            End If
        Else

            Select Case Locn

                Case "ClientProNetName"
                    ClientProNetName = Data 'The name of the Client Application Network requesting service. AD

                Case "ClientName"
                    ClientAppName = Data 'The name of the Client application requesting service.

                Case "ClientConnectionName"
                    ClientConnName = Data 'The name of the client connection requesting service.

                Case "ClientLocn" 'The Location within the Client requesting service.
                    Dim statusOK As New XElement("Status", "OK") 'Add Status OK element when the Client Location is changed
                    xlocns(xlocns.Count - 1).Add(statusOK)

                    xmessage.Add(xlocns(xlocns.Count - 1)) 'Add the instructions for the last location to the reply xmessage
                    xlocns.Add(New XElement(Data)) 'Start the new location instructions

                Case "OnCompletion"
                    OnCompletionInstruction = Data

                Case "Main"
                 'Blank message - do nothing.

                Case "Main:EndInstruction"
                    Select Case Data
                        Case "Stop"
                            'Stop at the end of the instruction sequence.

                            'Add other cases here:
                    End Select

                Case "Main:Status"
                    Select Case Data
                        Case "OK"
                            'Main instructions completed OK
                    End Select



                Case "Command"
                    Select Case Data
                        Case "ConnectToComNet" 'Startup Command
                            If ConnectedToComNet = False Then
                                ConnectToComNet()
                            End If
                        Case "AppComCheck"
                            'Add the Appplication Communication info to the reply message:
                            Dim clientProNetName As New XElement("ClientProNetName", ProNetName) 'The Project Network Name
                            xlocns(xlocns.Count - 1).Add(clientProNetName)
                            Dim clientName As New XElement("ClientName", "ADVL_Monte_Carlo_1") 'The name of this application.
                            xlocns(xlocns.Count - 1).Add(clientName)
                            Dim clientConnectionName As New XElement("ClientConnectionName", ConnectionName)
                            xlocns(xlocns.Count - 1).Add(clientConnectionName)
                            '<Status>OK</Status> will be automatically appended to the XMessage before it is sent.
                    End Select


            'Startup Command Arguments ================================================
                Case "ProNetName"
                'This is currently not used.
                'The ProNetName is determined elsewhere.

                Case "ProjectName"
                    If Project.OpenProject(Data) = True Then
                        ProjectSelected = True 'Project has been opened OK.
                    Else
                        ProjectSelected = False 'Project could not be opened.
                    End If

                Case "ProjectID"
                    Message.AddWarning("Add code to handle ProjectID parameter at StartUp!" & vbCrLf)
                'Note the ComNet will usually select a project using ProjectPath.

                Case "ProjectPath"
                    If Project.OpenProjectPath(Data) = True Then
                        ProjectSelected = True 'Project has been opened OK.
                        'THE PROJECT IS LOCKED IN THE Form.Load EVENT:

                        ApplicationInfo.SettingsLocn = Project.SettingsLocn
                        Message.SettingsLocn = Project.SettingsLocn 'Set up the Message object
                        Message.Show() 'Added 18May19

                        'txtTotalDuration.Text = Project.Usage.TotalDuration.Days.ToString.PadLeft(5, "0"c) & ":" &
                        '              Project.Usage.TotalDuration.Hours.ToString.PadLeft(2, "0"c) & ":" &
                        '              Project.Usage.TotalDuration.Minutes.ToString.PadLeft(2, "0"c) & ":" &
                        '              Project.Usage.TotalDuration.Seconds.ToString.PadLeft(2, "0"c)

                        'txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & ":" &
                        '               Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & ":" &
                        '               Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & ":" &
                        '               Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c)

                        txtTotalDuration.Text = Project.Usage.TotalDuration.Days.ToString.PadLeft(5, "0"c) & "d:" &
                                        Project.Usage.TotalDuration.Hours.ToString.PadLeft(2, "0"c) & "h:" &
                                        Project.Usage.TotalDuration.Minutes.ToString.PadLeft(2, "0"c) & "m:" &
                                        Project.Usage.TotalDuration.Seconds.ToString.PadLeft(2, "0"c) & "s"

                        txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & "d:" &
                                       Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & "h:" &
                                       Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & "m:" &
                                       Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c) & "s"

                    Else
                        ProjectSelected = False 'Project could not be opened.
                        Message.AddWarning("Project could not be opened at path: " & Data & vbCrLf)
                    End If

                Case "ConnectionName"
                    StartupConnectionName = Data
            '--------------------------------------------------------------------------

            'Application Information  =================================================
            'returned by client.GetAdvlNetworkAppInfoAsync()
                Case "AdvlNetworkAppInfo:Name"
                'The name of the Andorville™ Network Application. (Not used.)

                Case "AdvlNetworkAppInfo:ExePath"
                    'The executable file path of the Andorville™ Network Application.
                    AdvlNetworkExePath = Data

                Case "AdvlNetworkAppInfo:Path"
                    'The path of the Andorville™ Network Application (ComNet). (This is where an Application.Lock file will be found while ComNet is running.)
                    AdvlNetworkAppPath = Data
           '---------------------------------------------------------------------------

           'Message Window Instructions  ==============================================
                Case "MessageWindow:Left"
                    If IsNothing(Message.MessageForm) Then
                        Message.ApplicationName = ApplicationInfo.Name
                        Message.SettingsLocn = Project.SettingsLocn
                        Message.Show()
                    End If
                    Message.MessageForm.Left = Data
                Case "MessageWindow:Top"
                    If IsNothing(Message.MessageForm) Then
                        Message.ApplicationName = ApplicationInfo.Name
                        Message.SettingsLocn = Project.SettingsLocn
                        Message.Show()
                    End If
                    Message.MessageForm.Top = Data
                Case "MessageWindow:Width"
                    If IsNothing(Message.MessageForm) Then
                        Message.ApplicationName = ApplicationInfo.Name
                        Message.SettingsLocn = Project.SettingsLocn
                        Message.Show()
                    End If
                    Message.MessageForm.Width = Data
                Case "MessageWindow:Height"
                    If IsNothing(Message.MessageForm) Then
                        Message.ApplicationName = ApplicationInfo.Name
                        Message.SettingsLocn = Project.SettingsLocn
                        Message.Show()
                    End If
                    Message.MessageForm.Height = Data
                Case "MessageWindow:Command"
                    Select Case Data
                        Case "BringToFront"
                            If IsNothing(Message.MessageForm) Then
                                Message.ApplicationName = ApplicationInfo.Name
                                Message.SettingsLocn = Project.SettingsLocn
                                Message.Show()
                            End If
                            Message.MessageForm.Activate()
                            Message.MessageForm.TopMost = True
                            Message.MessageForm.TopMost = False
                        Case "SaveSettings"
                            Message.MessageForm.SaveFormSettings()
                    End Select

            '---------------------------------------------------------------------------

            'Command to bring the Application window to the front:
                Case "ApplicationWindow:Command"
                    Select Case Data
                        Case "BringToFront"
                            Me.Activate()
                            Me.TopMost = True
                            Me.TopMost = False
                    End Select

                Case "EndOfSequence"
                    'End of Information Sequence reached.
                    'Add Status OK element at the end of the sequence:
                    Dim statusOK As New XElement("Status", "OK")
                    xlocns(xlocns.Count - 1).Add(statusOK)

                    Select Case EndInstruction
                        Case "Stop"
                            'No instructions.

                            'Add any other Cases here:

                        Case Else
                            Message.AddWarning("Unknown End Instruction: " & EndInstruction & vbCrLf)
                    End Select
                    EndInstruction = "Stop"

                    'Add the final EndInstruction:
                    If OnCompletionInstruction = "Stop" Then
                        'Final EndInstruction is not required.
                    Else
                        Dim xEndInstruction As New XElement("EndInstruction", OnCompletionInstruction)
                        xlocns(xlocns.Count - 1).Add(xEndInstruction)
                        OnCompletionInstruction = "Stop" 'Reset the OnCompletion Instruction
                    End If

                Case Else
                    Message.AddWarning("Unknown location: " & Locn & vbCrLf)
                    Message.AddWarning("            data: " & Data & vbCrLf & vbCrLf)
            End Select
        End If
    End Sub

    Private Sub XMsgLocal_Instruction(Data As String, Locn As String) Handles XMsgLocal.Instruction
        'Process an XMessage instruction locally.

        If IsDBNull(Data) Then
            Data = ""
        End If

        'Intercept instructions with the prefix "WebPage_"
        If Locn.StartsWith("WebPage_") Then 'Send the Data, Location data to the correct Web Page:
            'Message.Add("Web Page Location: " & Locn & vbCrLf)
            If Locn.Contains(":") Then
                Dim EndOfWebPageNoString As Integer = Locn.IndexOf(":")
                If Locn.Contains("-") Then
                    Dim HyphenLocn As Integer = Locn.IndexOf("-")
                    If HyphenLocn < EndOfWebPageNoString Then 'Web Page Location contains a sub-location in the web page - WebPage_1-SubLocn:Locn - SubLocn:Locn will be sent to Web page 1
                        EndOfWebPageNoString = HyphenLocn
                    End If
                End If
                Dim PageNoLen As Integer = EndOfWebPageNoString - 8
                Dim WebPageNoString As String = Locn.Substring(8, PageNoLen)
                Dim WebPageNo As Integer = CInt(WebPageNoString)
                Dim WebPageData As String = Data
                Dim WebPageLocn As String = Locn.Substring(EndOfWebPageNoString + 1)

                'Message.Add("WebPageData = " & WebPageData & "  WebPageLocn = " & WebPageLocn & vbCrLf)

                WebPageFormList(WebPageNo).XMsgInstruction(WebPageData, WebPageLocn)
            Else
                Message.AddWarning("XMessage instruction location is not complete: " & Locn & vbCrLf)
            End If
        Else

            Select Case Locn
                Case "ClientName"
                    ClientAppName = Data 'The name of the Client requesting service.

                'UPDATE:
                Case "OnCompletion"
                    OnCompletionInstruction = Data

                Case "Main"
                 'Blank message - do nothing.


                Case "Main:EndInstruction"
                    Select Case Data
                        Case "Stop"
                            'Stop at the end of the instruction sequence.

                            'Add other cases here:
                    End Select

                Case "Main:Status"
                    Select Case Data
                        Case "OK"
                            'Main instructions completed OK
                    End Select

                Case "EndOfSequence"
                    'End of Information Vector Sequence reached.

                Case Else
                    Message.AddWarning("Local XMessage: " & Locn & vbCrLf)
                    Message.AddWarning("Unknown location: " & Locn & vbCrLf)
                    Message.AddWarning("            data: " & Data & vbCrLf & vbCrLf)
            End Select
        End If
    End Sub



#End Region 'Process XMessages ------------------------------------------------------------------------------------------------------------------------------------------


    Private Sub ToolStripMenuItem1_EditWorkflowTabPage_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1_EditWorkflowTabPage.Click
        'Edit the Workflow Web Page:

        If WorkflowFileName = "" Then
            Message.AddWarning("No page to edit." & vbCrLf)
        Else
            Dim FormNo As Integer = OpenNewHtmlDisplayPage()
            HtmlDisplayFormList(FormNo).FileName = WorkflowFileName
            HtmlDisplayFormList(FormNo).OpenDocument
        End If

    End Sub

    Private Sub ToolStripMenuItem1_ShowStartPageInWorkflowTab_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1_ShowStartPageInWorkflowTab.Click
        'Show the Start Page in the Workflow Tab:
        OpenStartPage()
    End Sub

    Private Sub bgwComCheck_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgwComCheck.DoWork
        'The communications check thread.
        While ConnectedToComNet
            Try
                If client.IsAlive() Then
                    bgwComCheck.ReportProgress(1, Format(Now, "HH:mm:ss") & " Connection OK." & vbCrLf)
                Else
                    bgwComCheck.ReportProgress(1, Format(Now, "HH:mm:ss") & " Connection Fault.")
                End If
            Catch ex As Exception
                bgwComCheck.ReportProgress(1, "Error in bgeComCheck_DoWork!" & vbCrLf)
                bgwComCheck.ReportProgress(1, ex.Message & vbCrLf)
            End Try

            System.Threading.Thread.Sleep(1800000) 'Sleep time in milliseconds (30 minutes)
        End While
    End Sub

    Private Sub bgwComCheck_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgwComCheck.ProgressChanged
        Message.Add(e.UserState.ToString) 'Show the ComCheck message 
    End Sub

    Private Sub XMsg_ErrorMsg(ErrMsg As String) Handles XMsg.ErrorMsg
        Message.AddWarning(ErrMsg & vbCrLf)
    End Sub

    Private Sub bgwSendMessage_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgwSendMessage.DoWork
        'Send a message on a separate thread:
        Try
            If IsNothing(client) Then
                bgwSendMessage.ReportProgress(1, "No Connection available. Message not sent!")
            Else
                If client.State = ServiceModel.CommunicationState.Faulted Then
                    bgwSendMessage.ReportProgress(1, "Connection state is faulted. Message not sent!")
                Else
                    Dim SendMessageParams As clsSendMessageParams = e.Argument
                    client.SendMessage(SendMessageParams.ProjectNetworkName, SendMessageParams.ConnectionName, SendMessageParams.Message)
                End If
            End If
        Catch ex As Exception
            bgwSendMessage.ReportProgress(1, ex.Message)
        End Try
    End Sub

    Private Sub bgwSendMessage_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgwSendMessage.ProgressChanged
        'Display an error message:
        Message.AddWarning("Send Message error: " & e.UserState.ToString & vbCrLf) 'Show the bgwSendMessage message 
    End Sub

    Private Sub bgwSendMessageAlt_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgwSendMessageAlt.DoWork
        'Alternative SendMessage background worker - used to send a message while instructions are being processed. 
        'Send a message on a separate thread
        Try
            If IsNothing(client) Then
                bgwSendMessageAlt.ReportProgress(1, "No Connection available. Message not sent!")
            Else
                If client.State = ServiceModel.CommunicationState.Faulted Then
                    bgwSendMessageAlt.ReportProgress(1, "Connection state is faulted. Message not sent!")
                Else
                    Dim SendMessageParamsAlt As clsSendMessageParams = e.Argument
                    client.SendMessage(SendMessageParamsAlt.ProjectNetworkName, SendMessageParamsAlt.ConnectionName, SendMessageParamsAlt.Message)
                End If
            End If
        Catch ex As Exception
            bgwSendMessageAlt.ReportProgress(1, ex.Message)
        End Try
    End Sub

    Private Sub bgwSendMessageAlt_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgwSendMessageAlt.ProgressChanged
        'Display an error message:
        Message.AddWarning("Send Message error: " & e.UserState.ToString & vbCrLf) 'Show the bgwSendMessageAlt message 
    End Sub

    Private Sub bgwRunInstruction_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgwRunInstruction.DoWork
        'Run a single instruction.
        Try
            Dim Instruction As clsInstructionParams = e.Argument
            XMsg_Instruction(Instruction.Info, Instruction.Locn)
        Catch ex As Exception
            bgwRunInstruction.ReportProgress(1, ex.Message)
        End Try
    End Sub

    Private Sub bgwRunInstruction_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgwRunInstruction.ProgressChanged
        'Display an error message:
        Message.AddWarning("Run Instruction error: " & e.UserState.ToString & vbCrLf) 'Show the bgwRunInstruction message 
    End Sub


    Private Sub btnShowProjectInfo_Click(sender As Object, e As EventArgs) Handles btnShowProjectInfo.Click
        'Show the current Project information:
        Message.Add("--------------------------------------------------------------------------------------" & vbCrLf)
        Message.Add("Project ------------------------ " & vbCrLf)
        Message.Add("   Name: " & Project.Name & vbCrLf)
        Message.Add("   Type: " & Project.Type.ToString & vbCrLf)
        Message.Add("   Description: " & Project.Description & vbCrLf)
        Message.Add("   Creation Date: " & Project.CreationDate & vbCrLf)
        Message.Add("   ID: " & Project.ID & vbCrLf)
        Message.Add("   Relative Path: " & Project.RelativePath & vbCrLf)
        Message.Add("   Path: " & Project.Path & vbCrLf & vbCrLf)

        Message.Add("Parent Project ----------------- " & vbCrLf)
        Message.Add("   Name: " & Project.ParentProjectName & vbCrLf)
        Message.Add("   Path: " & Project.ParentProjectPath & vbCrLf)

        Message.Add("Application -------------------- " & vbCrLf)
        Message.Add("   Name: " & Project.Application.Name & vbCrLf)
        Message.Add("   Description: " & Project.Application.Description & vbCrLf)
        Message.Add("   Path: " & Project.ApplicationDir & vbCrLf)

        Message.Add("Settings ----------------------- " & vbCrLf)
        Message.Add("   Settings Relative Location Type: " & Project.SettingsRelLocn.Type.ToString & vbCrLf)
        Message.Add("   Settings Relative Location Path: " & Project.SettingsRelLocn.Path & vbCrLf)
        Message.Add("   Settings Location Type: " & Project.SettingsLocn.Type.ToString & vbCrLf)
        Message.Add("   Settings Location Path: " & Project.SettingsLocn.Path & vbCrLf)

        Message.Add("Data --------------------------- " & vbCrLf)
        Message.Add("   Data Relative Location Type: " & Project.DataRelLocn.Type.ToString & vbCrLf)
        Message.Add("   Data Relative Location Path: " & Project.DataRelLocn.Path & vbCrLf)
        Message.Add("   Data Location Type: " & Project.DataLocn.Type.ToString & vbCrLf)
        Message.Add("   Data Location Path: " & Project.DataLocn.Path & vbCrLf)

        Message.Add("System ------------------------- " & vbCrLf)
        Message.Add("   System Relative Location Type: " & Project.SystemRelLocn.Type.ToString & vbCrLf)
        Message.Add("   System Relative Location Path: " & Project.SystemRelLocn.Path & vbCrLf)
        Message.Add("   System Location Type: " & Project.SystemLocn.Type.ToString & vbCrLf)
        Message.Add("   System Location Path: " & Project.SystemLocn.Path & vbCrLf)
        Message.Add("======================================================================================" & vbCrLf)

    End Sub

    Private Sub Message_ShowXMessagesChanged(Show As Boolean) Handles Message.ShowXMessagesChanged
        ShowXMessages = Show
    End Sub

    Private Sub Message_ShowSysMessagesChanged(Show As Boolean) Handles Message.ShowSysMessagesChanged
        ShowSysMessages = Show
    End Sub

    Private Sub Project_NewProjectCreated(ProjectPath As String) Handles Project.NewProjectCreated
        SendProjectInfo(ProjectPath) 'Send the path of the new project to the Network application. The new project will be added to the list of projects.
    End Sub


#Region " Monte Carlo Methods - Methods used to implement the Monte Carlo Simulation." '=======================================================================================================


#Region " Summary Tab" '=======================================================================================================================================================================

    Private Sub btnNewMCModel_Click(sender As Object, e As EventArgs) Handles btnNewMCModel.Click
        'Create a new Monte Carlo model.

        'Get the new model FileName, DataName, DataLabel and Description:
        Dim EntryForm As New ADVL_Utilities_Library_1.frmNewDataNameModal
        EntryForm.EntryName = "NewMCModel"
        EntryForm.Title = "New Monte Carlo Model"
        EntryForm.FileExtension = "MonteCarlo"
        EntryForm.GetFileName = True
        EntryForm.GetDataName = True
        EntryForm.GetDataLabel = True
        EntryForm.GetDataDescription = True
        EntryForm.SettingsLocn = Project.SettingsLocn
        EntryForm.DataLocn = Project.DataLocn
        EntryForm.ApplicationName = ApplicationInfo.Name
        EntryForm.RestoreFormSettings()
        If EntryForm.ShowDialog() = DialogResult.OK Then

            'Check if the existing model has been changed since the last save.
            If txtMCModelFileName.Text.Trim = "" Then
                'There is no model to save.
            Else
                If MonteCarlo.Modified Then
                    Dim Result As DialogResult = MessageBox.Show("Do you want to save the changes in the current Monte Carlo model?", "Warning", MessageBoxButtons.YesNoCancel)
                    If Result = DialogResult.Yes Then
                        SaveMonteCarloModel()
                    ElseIf Result = DialogResult.Cancel Then
                        Exit Sub
                    Else
                        'Contunue without saving the current model.
                        MonteCarlo.Modified = False
                    End If
                End If
            End If

            txtMCModelFileName.Text = EntryForm.FileName
            txtMCModelName.Text = EntryForm.DataName
            txtMCModelLabel.Text = EntryForm.DataLabel
            txtMCModelDescr.Text = EntryForm.DataDescription
        Else
            Exit Sub
        End If

        'Clear the old model parameters:
        txtNTrials.Text = "10000"
        txtNTrials2.Text = "10000"
        txtTrialNo.Text = "1"

        cmbCorrelations.Items.Clear()

        'chkPDF.Checked = True
        'chkCDF.Checked = False
        'chkRevCDF.Checked = False
        txtNRVPoints.Text = "512"
        txtXMin.Text = -5
        chkAutoXMin.Checked = True
        txtXMax.Text = "5"
        chkAutoXMax.Checked = True
        txtXGridInt.Text = "0.2"

        MonteCarlo.Clear()
        UpdateTableList() 'Table selection options need to be updated after the tables have been cleared in MonteCarlo
        dgvMCVariables.Rows.Clear()

        MonteCarlo.NTrials = 10000

        'Clear the Correlations tab:
        txtCorrelationName.Text = ""
        lblCorrMatNo.Text = 0
        lblCorrMatCount.Text = 0
        txtCorrelationDesc.Text = ""
        cmbCorrTableName.Items.Clear()
        txtNCorrVars.Text = "2"
        txtCorrMatrixFormat.Text = ""
        txtCorrCholFormat.Text = ""

        dgvResults.DataSource = Nothing
        'dgvResults.RowCount = 0 'ERROR
        dgvResults.Rows.Clear()
        'dgvCorrMatrix.RowCount = 0 'ERROR
        dgvCorrMatrix.Rows.Clear()
        'dgvCholesky.RowCount = 0 'ERROR
        dgvCholesky.Rows.Clear()
        txtNCorrVars.Text = "2"
        SetUpCorrMat(2)

    End Sub

    Private Sub UpdateTableList()
        'Update the list of tables in cmbMCTableName

        cmbMCTableName.Items.Clear()
        cmbCopyFromTable.Items.Clear()
        cmbCopyToTable.Items.Clear()
        cmbSortTable.Items.Clear()
        cmbCopyDataFromTable.Items.Clear()
        cmbCopyDataToTable.Items.Clear()
        cmbCorrTableName.Items.Clear() 'The Table list on the Correlations Tab should be updated too.
        cmbDeleteTable.Items.Clear()
        cmbCopyColFromTable.Items.Clear()
        cmbCopyColToTable.Items.Clear()

        cboDestTable.Items.Clear() 'The list of Destination Tables for a new Random Variable in the Variables tab.
        cboDestTable.Items.Add("Calculations") 'First default option
        cboDestTable.Items.Add("New Table") 'Second default option

        For Each item In MonteCarlo.Data.Tables
            cmbMCTableName.Items.Add(item.TableName)
            cmbCopyFromTable.Items.Add(item.TableName)
            cmbCopyToTable.Items.Add(item.TableName)
            cmbSortTable.Items.Add(item.TableName)
            cmbCopyDataFromTable.Items.Add(item.TableName)
            cmbCopyDataToTable.Items.Add(item.TableName)
            'cmbCorrTableName.Items.Add(item.TableName)
            If cmbCorrTableName.Items.Contains(item.TableName) Then Else cmbCorrTableName.Items.Add(item.TableName) 'Avoid duplicating items on the list
            'cboDestTable.Items.Add(item.TableName)
            If item.TableName <> "Calculations" Then cboDestTable.Items.Add(item.TableName) 'Avoid duplicating the Calculations table in the list.
            cmbDeleteTable.Items.Add(item.TableName)
            cmbCopyColFromTable.Items.Add(item.TableName)
            cmbCopyColToTable.Items.Add(item.TableName)
        Next
        cmbMCTableName.Text = ""
        cmbMCTableName.SelectedIndex = cmbMCTableName.FindStringExact(MCTableName)

        cmbCopyFromTable.Text = ""
        cmbCopyToTable.Text = ""
        cmbSortTable.Text = ""
        cmbCopyDataFromTable.Text = ""
        cmbCopyDataToTable.Text = ""
        cmbDeleteTable.Text = ""
        cmbCopyColFromTable.Text = ""
        cmbCopyColToTable.Text = ""
    End Sub

    Private Sub UpdateMCDataTableView()
        'Update the data table display:

        If MonteCarlo.Data.Tables.Contains(MCTableName) Then
            dgvResults.Columns.Clear() 'Without clearing the columns, the column order can be set by a previous table view.
            dgvResults.AutoGenerateColumns = True
            dgvResults.DataSource = MonteCarlo.Data.Tables(MCTableName)
            dgvResults.AutoResizeColumns()
            dgvResults.Update()
            dgvResults.Refresh()

            cmbMCTableName.SelectedIndex = cmbMCTableName.FindStringExact(MCTableName)

            For Each Item In MonteCarlo.DataInfo
                If Item.Table = MCTableName Then
                    If Item.Format = "" Then
                        'No format defined.
                    Else
                        Try
                            dgvResults.Columns(Item.Name).DefaultCellStyle.Format = Item.Format
                        Catch ex As Exception
                            Message.AddWarning("Error formatting column " & Item.Name & " : " & ex.Message & vbCrLf)
                        End Try

                    End If
                End If
            Next
        Else
            dgvResults.Columns.Clear()
        End If
    End Sub

    Private Sub SetUpCorrMat(ByVal NVars As Integer)
        'Set up the correlation matrix with the number of random variables = NVars.

        dgvCorrMatrix.ColumnCount = NVars + 1
        dgvCorrMatrix.RowCount = NVars + 1

        Dim I As Integer
        Dim J As Integer
        Dim CorrMatName As String = txtCorrelationName.Text
        For I = 1 To NVars
            dgvCorrMatrix.Rows(I).Cells(0).Value = "" 'Set the Uncorrelated random variable names to ""
            dgvCorrMatrix.Rows(0).Cells(I).Value = "" 'Set the Correlated random variable names to ""
            dgvCorrMatrix.Columns(I).HeaderText = "Correlated Variable " & I
            dgvCorrMatrix.Rows(I).Cells(I).Value = "1"
            dgvCorrMatrix.Rows(I).Cells(I).ReadOnly = True
            dgvCorrMatrix.Rows(I).Cells(I).Style.BackColor = Color.LightGray
            For J = 1 To I - 1
                dgvCorrMatrix.Rows(I).Cells(J).Value = ""
            Next
            For J = I + 1 To NVars
                dgvCorrMatrix.Rows(I).Cells(J).Value = ""
                dgvCorrMatrix.Rows(I).Cells(J).ReadOnly = True
                dgvCorrMatrix.Rows(I).Cells(J).Style.BackColor = Color.WhiteSmoke
            Next
        Next

        dgvCholesky.ColumnCount = NVars
        dgvCholesky.RowCount = NVars

    End Sub

    Private Sub SaveMonteCarloModel()
        Dim FileName As String = Trim(txtMCModelFileName.Text)

        'Check if a file name has been specified:
        If FileName = "" Then
            Message.AddWarning("Please enter a file name." & vbCrLf)
            Exit Sub
        End If

        'Check the fine name extension:
        If LCase(FileName).EndsWith(".montecarlo") Then
            FileName = IO.Path.GetFileNameWithoutExtension(FileName) & ".MonteCarlo"
        ElseIf FileName.Contains(".") Then
            Message.AddWarning("Unknown file extension: " & IO.Path.GetExtension(FileName) & vbCrLf)
            Exit Sub
        Else
            FileName = FileName & ".MonteCarlo"
        End If

        txtMCModelFileName.Text = FileName

        'Update the Monte Carlo settings:
        MonteCarlo.Name = txtMCModelName.Text
        MonteCarlo.Label = txtMCModelLabel.Text.Trim
        MonteCarlo.Description = txtMCModelDescr.Text
        MonteCarlo.NTrials = txtNTrials.Text
        MonteCarlo.CalcSeqFile = txtCalcFileName.Text

        'Save the Random Variable specifications to MonteCarlo:

        Project.SaveXmlData(FileName, MonteCarlo.MonteCarloToXDoc)
        MonteCarlo.Modified = False

        If CalcSeqModified = True Then SaveSeq(txtCalcFileName.Text.Trim)

        If chkSaveMCData.Checked Then
            'Save the realized data in a binary file:

            'UPDATE: Save all tables.
            If MonteCarlo.Data.Tables.Count = 0 Then
                Message.AddWarning("The Monte Carlo data set does not contain any tables" & vbCrLf)
            Else
                Dim DataFileName As String
                DataFileName = System.IO.Path.GetFileNameWithoutExtension(FileName) & ".MCData"
                Dim TableData As New IO.MemoryStream
                MonteCarlo.Data.WriteXml(TableData, XmlWriteMode.WriteSchema)
                Project.SaveData(DataFileName, TableData)
            End If
            'Exit Sub

            ''OLD CODE:
            'If MonteCarlo.Data.Tables.Contains("DataTable") Then
            '    Dim BinaryFileName As String
            '    BinaryFileName = System.IO.Path.GetFileNameWithoutExtension(FileName) & ".MCData"
            '    Dim TableData As New IO.MemoryStream
            '    MonteCarlo.Data.Tables("DataTable").WriteXml(TableData, XmlWriteMode.WriteSchema)
            '    Project.SaveData(BinaryFileName, TableData)

            'Else
            '    Message.AddWarning("The Monte Carlo data set does not contain a table named 'DataTable'" & vbCrLf)
            'End If
        End If
    End Sub

    Private Sub SaveSeq(ByVal FileName As String)
        'Save the Calculation Sequence in the specified file name.

        If FileName.Trim = "" Then
            Message.AddWarning("The Calculation Sequence file name is blank." & vbCrLf)
        Else
            Dim XDoc = <?xml version="1.0" encoding="utf-8"?>
                       <!---->
                       <!--Calculation Sequence File-->
                       <CalculationSequence>
                           <Name><%= txtCalcName.Text.Trim %></Name>
                           <Description><%= txtCalcDescr.Text.Trim %></Description>
                           <!--Calculation Information-->
                           <%= CalcInfoToXDoc().<CalculationInfoList> %>
                           <!--Column Names-->
                           <%= ColumnNamesToXDoc().<ColumnList> %>
                           <!--Scalar Data-->
                           <%= ScalarDataToXDoc().<ScalarDataList> %>
                           <!--Calculation Tree-->
                           <%= CalculationTreeToXDoc().<CalculationTree> %>
                       </CalculationSequence>
            Project.SaveXmlData(FileName, XDoc)

            CalcSeqModified = False
        End If
    End Sub

    Private Function CalcInfoToXDoc() As System.Xml.Linq.XDocument
        'Return the Calculation Information in the CalcInfo dictionary in an XDocument
        Dim XDoc = <?xml version="1.0" encoding="utf-8"?>
                   <CalculationInfoList>
                       <%= From item In CalcInfo
                           Select
                           <Calculation>
                               <Name><%= item.Key %></Name>
                               <Text><%= item.Value.Text %></Text>
                               <Units><%= item.Value.Units %></Units>
                               <UnitsAbbrev><%= item.Value.UnitsAbbrev %></UnitsAbbrev>
                               <Description><%= item.Value.Description %></Description>
                               <Type><%= item.Value.Type %></Type>
                               <Status><%= item.Value.Status %></Status>
                               <StatusCode><%= item.Value.StatusCode %></StatusCode>
                               <CopyList>
                                   <%= From listItem In item.Value.CopyList
                                       Select
                                       <Name><%= listItem %></Name> %>
                               </CopyList>
                           </Calculation> %>
                   </CalculationInfoList>
        Return XDoc
    End Function

    Private Function ColumnNamesToXDoc() As System.Xml.Linq.XDocument
        'Return the Column Names in the ColumnName dictionary in an XDocument
        Dim XDoc = <?xml version="1.0" encoding="utf-8"?>
                   <ColumnList>
                       <%= From item In ColumnInfo
                           Select
                           <Column>
                               <Variable><%= item.Key %></Variable>
                               <Name><%= item.Value.Name %></Name>
                               <Type><%= item.Value.Type %></Type>
                           </Column> %>
                   </ColumnList>

        Return XDoc
    End Function

    Private Function ScalarDataToXDoc() As System.Xml.Linq.XDocument
        'Return the Scalar Data in an XDocument
        Dim XDoc = <?xml version="1.0" encoding="utf-8"?>
                   <ScalarDataList>
                       <%= From item In ScalarData
                           Select
                           <Scalar>
                               <Name><%= item.Key %></Name>
                               <Value><%= item.Value %></Value>
                           </Scalar> %>
                   </ScalarDataList>
        Return XDoc
    End Function

    Private Function CalculationTreeToXDoc() As System.Xml.Linq.XDocument
        'Return the Calculation Tree as an XDocument

        Dim decl As New XDeclaration("1.0", "utf-8", "yes")
        Dim XDoc As New XDocument(decl, Nothing)
        Dim myCalculationTree As New XElement("CalculationTree")

        SaveCalcNode(myCalculationTree, "", trvCalculations.Nodes)
        XDoc.Add(myCalculationTree)
        Return XDoc
    End Function

    Private Sub SaveCalcNode(ByRef myElement As XElement, ByVal Parent As String, ByRef tnc As TreeNodeCollection)
        'Save the nodes in the TreeNodeCollection in the XElement
        'This method calls itself recursively to save all nodes in trvCalculations

        If tnc.Count = 0 Then 'Leaf
        Else
            Dim I As Integer
            For I = 0 To tnc.Count - 1
                Dim NodeKey As String = tnc(I).Name
                Dim myNode As New XElement(System.Xml.XmlConvert.EncodeName(NodeKey)) 'A space character os not allowed in an XElement name. Replace spaces with &sp characters.
                Dim myNodeText As New XElement("Text", tnc(I).Text)
                myNode.Add(myNodeText)

                If tnc(I).Nodes.Count > 0 Then
                    Dim isExpanded As New XElement("IsExpanded", tnc(I).IsExpanded)
                    myNode.Add(isExpanded)
                End If

                If CalcInfo.ContainsKey(NodeKey) Then
                    Dim myNodeType As New XElement("Type", CalcInfo(NodeKey).Type)
                Else
                    Message.Add("The Node Name: " & NodeKey & " is not in the CalcInfo dictionary!" & vbCrLf)
                End If
                SaveCalcNode(myNode, tnc(I).Name, tnc(I).Nodes)
                myElement.Add(myNode)
            Next
        End If
    End Sub

    Private Sub btnSaveMCModel_Click(sender As Object, e As EventArgs) Handles btnSaveMCModel.Click
        SaveMonteCarloModel()    'Save the Monte Carlo model.
    End Sub

    Private Sub btnOpenMCModel_Click(sender As Object, e As EventArgs) Handles btnOpenMCModel.Click
        'Open a Monte Carlo model.
        Dim FileName As String = Project.SelectDataFile("Monte Carlo files", "MonteCarlo")
        If FileName = "" Then
            'No file has been selected.
        Else
            OpenMCModel(FileName)
        End If

    End Sub

    Private Sub OpenMCModel(ByVal FileName As String)
        'Open a Monte Carlo model.

        'Remove the existing model data:
        txtMCModelFileName.Text = ""
        txtMCModelName.Text = ""
        txtMCModelLabel.Text = ""
        txtMCModelDescr.Text = ""
        txtNTrials.Text = "10000"
        txtNTrials2.Text = "10000"
        txtTrialNo.Text = "1"
        'chkPDF.Checked = True
        'chkCDF.Checked = False
        'chkRevCDF.Checked = False
        txtNRVPoints.Text = "512"
        txtXMin.Text = -5
        chkAutoXMin.Checked = True
        txtXMax.Text = "5"
        chkAutoXMax.Checked = True
        txtXGridInt.Text = "0.2"
        MonteCarlo.Clear()

        UpdateTableList() 'Table selection options need to be updated after the tables have been cleared in MonteCarlo

        dgvMCVariables.Rows.Clear()
        dgvResults.DataSource = Nothing
        'dgvResults.RowCount = 0 'ERROR
        dgvResults.Rows.Clear()
        dgvCorrMatrix.RowCount = 0
        dgvCholesky.RowCount = 0
        txtNCorrVars.Text = "2"
        SetUpCorrMat(2)

        Dim XDoc As System.Xml.Linq.XDocument

        Project.ReadXmlData(FileName, XDoc)
        MonteCarlo.FileName = FileName
        txtMCModelFileName.Text = FileName

        MonteCarlo.XDocToMonteCarlo(XDoc)

        txtMCModelName.Text = MonteCarlo.Name
        txtMCModelLabel.Text = MonteCarlo.Label
        txtMCModelDescr.Text = MonteCarlo.Description
        txtNTrials.Text = MonteCarlo.NTrials
        txtNTrials2.Text = MonteCarlo.NTrials
        txtTrialNo.Text = "1"

        If MonteCarlo.CalcSeqFile = "" Then
            'No calculation sequence file is defined.
        Else
            OpenSeq(MonteCarlo.CalcSeqFile)
        End If

        DisplayMCVariables()

        'Select the first Random Variable in the table:
        If dgvMCVariables.Rows.Count > 0 Then
            dgvMCVariables.Rows(0).Cells(0).Selected = True
            MonteCarlo.SelVarIndex = 0
            ShowRVChartSettings(0)
        End If

        'Show the first correlation matrix:
        cmbCorrelations.Items.Clear()
        If MonteCarlo.Correlations.Count > 0 Then
            'ShowCorrMatData(0)
            'Update the Correlations list:
            For Each item In MonteCarlo.Correlations
                cmbCorrelations.Items.Add(item.Key)
            Next
            If MonteCarlo.SelCorrMatName = "" Then
                cmbCorrelations.SelectedIndex = -1
            Else
                cmbCorrelations.SelectedIndex = cmbCorrelations.FindStringExact(MonteCarlo.SelCorrMatName)
                ShowCorrMatData(cmbCorrelations.SelectedIndex)
            End If
        End If
        'Message.Add("Finished showing first correlation matrix." & vbCrLf)

        'Populate the Chart List
        cmbChartList.Items.Clear()
        For Each item In MonteCarlo.ChartList
            cmbChartList.Items.Add(item.Key)
        Next

        If chkSaveMCData.Checked Then 'Open the realised data if available.
            Dim DataFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileName) & ".MCData"

            'Restore all tables.
            If Project.DataFileExists(DataFileName) Then
                Dim TableData As New IO.MemoryStream
                Project.ReadData(DataFileName, TableData)
                TableData.Position = 0
                MonteCarlo.Data.ReadXml(TableData, XmlReadMode.ReadSchema)
                UpdateTableList()
            Else
                Message.AddWarning("Monte Carlo data file not found: " & DataFileName & vbCrLf)
            End If
        End If
    End Sub

#End Region 'Summary Tab ----------------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Random Variables Tab" '==============================================================================================================================================================

    Public Sub ShowRVChartSettings(ByVal ItemNo As Integer)
        'Show the Random Variable plot settings for ItemNo.

        If ItemNo < MonteCarlo.DataInfo.Count Then
            If ItemNo >= 0 Then
                'chkPDF.Checked = MonteCarlo.DataInfo(ItemNo).ShowPDF
                txtPdfLineColor.BackColor = MonteCarlo.DataInfo(ItemNo).PdfLineColor
                txtPdfLineThickness.Text = MonteCarlo.DataInfo(ItemNo).PdfLineThickness
                'chkCDF.Checked = MonteCarlo.DataInfo(ItemNo).ShowCDF
                txtCdfLineColor.BackColor = MonteCarlo.DataInfo(ItemNo).CDFLineColor
                txtCdfLineThickness.Text = MonteCarlo.DataInfo(ItemNo).CDFLineThickness
                'chkRevCDF.Checked = MonteCarlo.DataInfo(ItemNo).ShowRevCDF
                txtRevCdfLineColor.BackColor = MonteCarlo.DataInfo(ItemNo).RevCDFLineColor
                txtRevCdfLineThickness.Text = MonteCarlo.DataInfo(ItemNo).RevCDFLineThickness
                txtNRVPoints.Text = MonteCarlo.DataInfo(ItemNo).NDisplayPoints
                txtXMin.Text = MonteCarlo.DataInfo(ItemNo).XMin
                chkAutoXMin.Checked = MonteCarlo.DataInfo(ItemNo).AutoXMin
                txtXMax.Text = MonteCarlo.DataInfo(ItemNo).XMax
                chkAutoXMax.Checked = MonteCarlo.DataInfo(ItemNo).AutoXMax
                txtYMax.Text = MonteCarlo.DataInfo(ItemNo).YMax
                chkAutoYMax.Checked = MonteCarlo.DataInfo(ItemNo).AutoYMax
                txtXGridInt.Text = MonteCarlo.DataInfo(ItemNo).XGridInterval
                txtTop.Text = MonteCarlo.DataInfo(ItemNo).Top
                txtLeft.Text = MonteCarlo.DataInfo(ItemNo).Left
                txtHeight.Text = MonteCarlo.DataInfo(ItemNo).Height
                txtWidth.Text = MonteCarlo.DataInfo(ItemNo).Width
            End If
        Else
            Message.AddWarning("Chart settings not found for Data Item Number: " & ItemNo & vbCrLf)
        End If
    End Sub

    Private Sub ShowCorrMatData(ByVal CorrMatIndex As Integer)
        'Show the Correlation Matrix data corresponding to the key number CorrMatIndex.
        ShowCorrMatData(MonteCarlo.Correlations.Keys(CorrMatIndex))
    End Sub

    Private Sub ShowCorrMatData(ByVal CorrMatName As String)
        'Show the Correlation Matrix data corresponding to the key CorrMatName.

        If MonteCarlo.Correlations.ContainsKey(CorrMatName) Then
            'Find the index of the current Key (CorrMatName)
            lblCorrMatNo.Text = 0 'If the key is not found, the Correlation Matrix number display remains as 0.
            Dim I As Integer
            For I = 0 To MonteCarlo.Correlations.Count - 1
                If MonteCarlo.Correlations.Keys(I) = CorrMatName Then
                    lblCorrMatNo.Text = I + 1
                    Exit For
                End If
            Next
            lblCorrMatCount.Text = MonteCarlo.Correlations.Count
            txtCorrelationName.Text = CorrMatName
            txtCorrelationDesc.Text = MonteCarlo.Correlations(CorrMatName).Description
            Dim NVars As Integer = MonteCarlo.Correlations(CorrMatName).NVariables
            txtNCorrVars.Text = NVars
            SetUpCorrMat(NVars)
            txtCorrMatrixFormat.Text = MonteCarlo.Correlations(CorrMatName).DisplayFormat
            dgvCorrMatrix.DefaultCellStyle.Format = txtCorrMatrixFormat.Text
            txtCorrCholFormat.Text = MonteCarlo.Correlations(CorrMatName).CholDisplayFormat
            dgvCholesky.DefaultCellStyle.Format = txtCorrCholFormat.Text

            Dim TableName As String = MonteCarlo.Correlations(CorrMatName).TableName

            If cmbCorrTableName.Items.Contains(TableName) Then
                cmbCorrTableName.SelectedIndex = cmbCorrTableName.FindStringExact(TableName) 'No item will be selected if the list of tables has not yet been added!
            Else
                If TableName = "" Then
                    'Dont add a blank table name to the list
                    cmbCorrTableName.SelectedIndex = -1
                Else
                    cmbCorrTableName.Items.Add(TableName) 'Add the table to the list if required.
                    cmbCorrTableName.SelectedIndex = cmbCorrTableName.FindStringExact(TableName) 'No item will be selected if the list of tables has not yet been added!
                End If
            End If

            'Check if the Uncorrelated Random Variables in Correlations exist in the DataInfo list.
            Dim RVMissing As Boolean = False
            Dim RVFound As Boolean
            For I = 0 To NVars - 1
                RVFound = False
                For Each item In MonteCarlo.DataInfo
                    If item.Table = TableName Then
                        If item.Name = MonteCarlo.Correlations(CorrMatName).UnCorrRV((I)) Then
                            RVFound = True
                            Exit For
                        End If
                    End If
                Next
                If RVFound = False Then
                    Message.AddWarning("The Uncorrelated Random Variable: " & MonteCarlo.Correlations(CorrMatName).UnCorrRV(I) & " does not exist in the DataInfo list." & vbCrLf)
                    RVMissing = True
                End If
            Next

            If RVMissing = True Then
                Message.AddWarning("This Correlation Matrix contains unknown Random Variables." & vbCrLf)

                'Show the correlation coefficents only:
                Dim J As Integer
                For I = 0 To NVars - 1
                    'MonteCarlo.Data may not have the table data loaded - add any missing Column Names to cboCorrVariables if required:
                    If cboCorrVariables.Items.Contains(MonteCarlo.Correlations(CorrMatName).UnCorrRV(I)) Then Else cboCorrVariables.Items.Add(MonteCarlo.Correlations(CorrMatName).UnCorrRV(I))
                    dgvCorrMatrix.Rows(I + 1).Cells(0).Value = MonteCarlo.Correlations(CorrMatName).UnCorrRV(I)
                    For J = 0 To NVars - 1
                        dgvCorrMatrix.Rows(0).Cells(J + 1).Value = MonteCarlo.Correlations(CorrMatName).CorrRV(J)
                        dgvCorrMatrix.Rows(I + 1).Cells(J + 1).Value = MonteCarlo.Correlations(CorrMatName).Array(I, J)
                    Next
                Next

            Else
                Dim J As Integer
                For I = 0 To NVars - 1
                    'UPDATE: MonteCarlo.Data may not have the table data loaded - add any missing Column Names to cboCorrVariables if required:
                    If cboCorrVariables.Items.Contains(MonteCarlo.Correlations(CorrMatName).UnCorrRV(I)) Then Else cboCorrVariables.Items.Add(MonteCarlo.Correlations(CorrMatName).UnCorrRV(I))
                    dgvCorrMatrix.Rows(I + 1).Cells(0).Value = MonteCarlo.Correlations(CorrMatName).UnCorrRV(I)
                    For J = 0 To NVars - 1
                        dgvCorrMatrix.Rows(0).Cells(J + 1).Value = MonteCarlo.Correlations(CorrMatName).CorrRV(J)
                        dgvCorrMatrix.Rows(I + 1).Cells(J + 1).Value = MonteCarlo.Correlations(CorrMatName).Array(I, J)
                    Next
                Next
            End If
        Else
            ClearCorrMatDataDisplay()
            Message.AddWarning("The correlation matrix name: " & CorrMatName & " was not found." & vbCrLf)
        End If
    End Sub

    Private Sub ClearCorrMatDataDisplay()
        'Clear the Correlation Matrix data display.

        lblCorrMatNo.Text = 0
        lblCorrMatCount.Text = MonteCarlo.Correlations.Count
        txtCorrelationName.Text = ""
        txtCorrelationDesc.Text = ""
        txtNCorrVars.Text = "2"
        SetUpCorrMat(2)
        dgvCorrMatrix.Rows(0).Cells(1).Value = ""
        dgvCorrMatrix.Rows(0).Cells(2).Value = ""
        dgvCorrMatrix.Rows(1).Cells(0).Value = ""
        dgvCorrMatrix.Rows(2).Cells(0).Value = ""
        dgvCorrMatrix.Rows(1).Cells(2).Value = ""
        dgvCorrMatrix.Rows(2).Cells(1).Value = ""
        dgvCholesky.Rows(0).Cells(0).Value = ""
        dgvCholesky.Rows(0).Cells(1).Value = ""
        dgvCholesky.Rows(1).Cells(0).Value = ""
        dgvCholesky.Rows(1).Cells(1).Value = ""
    End Sub

    Private Sub OpenSeq(ByVal FileName As String)
        'Open the Calculation Sequence file named FileName

        If FileName.Trim = "" Then
            Message.AddWarning("The Calculation Sequence file name is blank." & vbCrLf)
        Else
            If CalcSeqModified = True Then SaveSeq(txtCalcFileName.Text.Trim)

            Dim XDoc As New System.Xml.Linq.XDocument
            Project.ReadXmlData(FileName, XDoc)

            If XDoc Is Nothing Then
                Message.AddWarning("The selected file is empty." & vbCrLf)
                Exit Sub
            End If

            txtCalcFileName.Text = FileName
            txtCalcName.Text = XDoc.<CalculationSequence>.<Name>.Value
            txtCalcDescr.Text = XDoc.<CalculationSequence>.<Description>.Value

            'Restore the CalcInfo() dictionary:
            Dim CalcInfoList = From item In XDoc.<CalculationSequence>.<CalculationInfoList>.<Calculation>
            Dim Name As String
            CalcInfo.Clear()
            For Each item In CalcInfoList
                Dim NewCalcInfo As CalcOpInfo = New CalcOpInfo
                Name = item.<Name>.Value
                If item.<Text>.Value <> Nothing Then NewCalcInfo.Text = item.<Text>.Value
                If item.<Units>.Value <> Nothing Then NewCalcInfo.Units = item.<Units>.Value
                If item.<UnitsAbbrev>.Value <> Nothing Then NewCalcInfo.UnitsAbbrev = item.<UnitsAbbrev>.Value
                NewCalcInfo.Description = item.<Description>.Value
                NewCalcInfo.Type = item.<Type>.Value
                NewCalcInfo.Status = item.<Status>.Value
                If item.<StatusCode>.Value <> Nothing Then NewCalcInfo.StatusCode = item.<StatusCode>.Value
                Dim Copies = From listItem In item.<CopyList>
                For Each listItem In Copies
                    NewCalcInfo.CopyList.Add(listItem)
                Next
                CalcInfo.Add(Name, NewCalcInfo)
            Next

            'Restore the ColumnName() dictionary:
            Dim ColumnList = From item In XDoc.<CalculationSequence>.<ColumnList>.<Column>
            ColumnInfo.Clear()
            Dim VarName As String
            For Each item In ColumnList
                VarName = item.<Variable>.Value
                ColumnInfo.Add(VarName, New DataColumnInfo)
                ColumnInfo(VarName).Name = item.<Name>.Value
                ColumnInfo(VarName).Type = item.<Type>.Value
            Next


            'Restore the ScalarData() dictionary:
            Dim ScalarInfoList = From item In XDoc.<CalculationSequence>.<ScalarDataList>.<Scalar>
            ScalarData.Clear()
            For Each item In ScalarInfoList
                ScalarData.Add(item.<Name>.Value, item.<Value>.Value)
            Next

            'Restore the Calculation Tree:
            trvCalculations.Nodes.Clear()
            Dim I As Integer

            'Convert he XDocument to an XmlDocument:
            Dim XmlDoc As New System.Xml.XmlDocument
            XmlDoc.LoadXml(XDoc.ToString)

            ProcessCalcNode(XmlDoc.GetElementsByTagName("CalculationTree").Item(0), trvCalculations.Nodes, "", True)

        End If
    End Sub

    Private Sub DisplayMCVariables()
        'Display the Monte Carlo Variables.

        'Show the list of Random Variables in the Variables table;
        dgvMCVariables.Rows.Clear()

        Dim I As Integer
        Dim NItems As Integer = MonteCarlo.DataInfo.Count
        dgvMCVariables.RowCount = NItems
        For I = 0 To MonteCarlo.DataInfo.Count - 1
            dgvMCVariables.Rows(I).Cells(0).Value = MonteCarlo.DataInfo(I).Name
            dgvMCVariables.Rows(I).Cells(1).Value = MonteCarlo.DataInfo(I).Units
            dgvMCVariables.Rows(I).Cells(2).Value = MonteCarlo.DataInfo(I).UnitsAbbrev
            'dgvMCVariables.Rows(I).Cells(2).Value = MonteCarlo.DataInfo(I).Description
            'dgvMCVariables.Rows(I).Cells(3).Value = MonteCarlo.DataInfo(I).DataSetType
            'SetGridDistParams(MonteCarlo.DataInfo(I).DataSetType, I)

            'dgvMCVariables.Rows(I).Cells(4).Value = MonteCarlo.DataInfo(I).DataType

            'dgvMCVariables.Rows(I).Cells(5).Value = MonteCarlo.DataInfo(I).Sampling
            'dgvMCVariables.Rows(I).Cells(6).Value = MonteCarlo.DataInfo(I).Table
            'dgvMCVariables.Rows(I).Cells(7).Value = MonteCarlo.DataInfo(I).ParameterAName
            'If Double.IsNaN(MonteCarlo.DataInfo(I).ParameterAValue) Then dgvMCVariables.Rows(I).Cells(8).Value = "" Else dgvMCVariables.Rows(I).Cells(8).Value = MonteCarlo.DataInfo(I).ParameterAValue
            'dgvMCVariables.Rows(I).Cells(9).Value = MonteCarlo.DataInfo(I).ParameterBName
            'If Double.IsNaN(MonteCarlo.DataInfo(I).ParameterBValue) Then dgvMCVariables.Rows(I).Cells(10).Value = "" Else dgvMCVariables.Rows(I).Cells(10).Value = MonteCarlo.DataInfo(I).ParameterBValue
            'dgvMCVariables.Rows(I).Cells(11).Value = MonteCarlo.DataInfo(I).ParameterCName
            'If Double.IsNaN(MonteCarlo.DataInfo(I).ParameterCValue) Then dgvMCVariables.Rows(I).Cells(12).Value = "" Else dgvMCVariables.Rows(I).Cells(12).Value = MonteCarlo.DataInfo(I).ParameterCValue
            'dgvMCVariables.Rows(I).Cells(13).Value = MonteCarlo.DataInfo(I).ParameterDName
            'If Double.IsNaN(MonteCarlo.DataInfo(I).ParameterDValue) Then dgvMCVariables.Rows(I).Cells(14).Value = "" Else dgvMCVariables.Rows(I).Cells(14).Value = MonteCarlo.DataInfo(I).ParameterDValue
            'dgvMCVariables.Rows(I).Cells(15).Value = MonteCarlo.DataInfo(I).ParameterEName
            'If Double.IsNaN(MonteCarlo.DataInfo(I).ParameterEValue) Then dgvMCVariables.Rows(I).Cells(16).Value = "" Else dgvMCVariables.Rows(I).Cells(16).Value = MonteCarlo.DataInfo(I).ParameterEValue
            'If MonteCarlo.DataInfo(I).Seed = -1 Then dgvMCVariables.Rows(I).Cells(17).Value = "" Else dgvMCVariables.Rows(I).Cells(17).Value = MonteCarlo.DataInfo(I).Seed
            dgvMCVariables.Rows(I).Cells(3).Value = MonteCarlo.DataInfo(I).Description
            dgvMCVariables.Rows(I).Cells(4).Value = MonteCarlo.DataInfo(I).DataSetType
            SetGridDistParams(MonteCarlo.DataInfo(I).DataSetType, I)

            dgvMCVariables.Rows(I).Cells(5).Value = MonteCarlo.DataInfo(I).DataType

            dgvMCVariables.Rows(I).Cells(6).Value = MonteCarlo.DataInfo(I).Sampling
            dgvMCVariables.Rows(I).Cells(7).Value = MonteCarlo.DataInfo(I).Table
            dgvMCVariables.Rows(I).Cells(8).Value = MonteCarlo.DataInfo(I).ParameterAName
            If Double.IsNaN(MonteCarlo.DataInfo(I).ParameterAValue) Then dgvMCVariables.Rows(I).Cells(9).Value = "" Else dgvMCVariables.Rows(I).Cells(9).Value = MonteCarlo.DataInfo(I).ParameterAValue
            dgvMCVariables.Rows(I).Cells(10).Value = MonteCarlo.DataInfo(I).ParameterBName
            If Double.IsNaN(MonteCarlo.DataInfo(I).ParameterBValue) Then dgvMCVariables.Rows(I).Cells(11).Value = "" Else dgvMCVariables.Rows(I).Cells(11).Value = MonteCarlo.DataInfo(I).ParameterBValue
            dgvMCVariables.Rows(I).Cells(12).Value = MonteCarlo.DataInfo(I).ParameterCName
            If Double.IsNaN(MonteCarlo.DataInfo(I).ParameterCValue) Then dgvMCVariables.Rows(I).Cells(13).Value = "" Else dgvMCVariables.Rows(I).Cells(13).Value = MonteCarlo.DataInfo(I).ParameterCValue
            dgvMCVariables.Rows(I).Cells(14).Value = MonteCarlo.DataInfo(I).ParameterDName
            If Double.IsNaN(MonteCarlo.DataInfo(I).ParameterDValue) Then dgvMCVariables.Rows(I).Cells(15).Value = "" Else dgvMCVariables.Rows(I).Cells(15).Value = MonteCarlo.DataInfo(I).ParameterDValue
            dgvMCVariables.Rows(I).Cells(17).Value = MonteCarlo.DataInfo(I).ParameterEName
            If Double.IsNaN(MonteCarlo.DataInfo(I).ParameterEValue) Then dgvMCVariables.Rows(I).Cells(17).Value = "" Else dgvMCVariables.Rows(I).Cells(17).Value = MonteCarlo.DataInfo(I).ParameterEValue
            If MonteCarlo.DataInfo(I).Seed = -1 Then dgvMCVariables.Rows(I).Cells(18).Value = "" Else dgvMCVariables.Rows(I).Cells(18).Value = MonteCarlo.DataInfo(I).Seed

            MonteCarlo.DataInfo(I).Row = I
        Next

        dgvMCVariables.AutoResizeColumns()
        dgvMCVariables.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells

    End Sub

    Private Sub SetGridDistParams(ByVal DistName As String, ByVal RowNo As Integer)
        'Set the Distribution parameters for the selected distribution in dgvMCVariables.

        Select Case DistName
            Case "Data Table"
                'A new Table has been specified.
                Dim TableName As String = dgvMCVariables.Rows(RowNo).Cells(0).Value
                'Check if this Table name is already in cboDestTable
                If cboDestTable.Items.Contains(TableName) Then
                Else
                    cboDestTable.Items.Add(TableName) 'Add this Table Name to the list of avaialble destination tables.
                End If

                'Deactivate the Data Value Type selection:
                'dgvMCVariables.Rows(RowNo).Cells(4).Value = ""
                'dgvMCVariables.Rows(RowNo).Cells(4).Style.BackColor = Color.LightGray
                'dgvMCVariables.Rows(RowNo).Cells(4).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(5).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(5).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(5).ReadOnly = True
                MonteCarlo.DataInfo(RowNo).DataType = "" 'A Data Table does not have a DataType parameter.

                'Deactivate the Sampling selection:
                'dgvMCVariables.Rows(RowNo).Cells(5).Value = ""
                'dgvMCVariables.Rows(RowNo).Cells(5).Style.BackColor = Color.LightGray
                'dgvMCVariables.Rows(RowNo).Cells(5).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(6).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(6).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(6).ReadOnly = True
                MonteCarlo.DataInfo(RowNo).Sampling = "" 'A Data Table does not have a Sampling parameter.

                'Deactivate the Destination Table selection:
                'dgvMCVariables.Rows(RowNo).Cells(6).Value = ""
                'dgvMCVariables.Rows(RowNo).Cells(6).Style.BackColor = Color.LightGray
                'dgvMCVariables.Rows(RowNo).Cells(6).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(7).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(7).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(7).ReadOnly = True
                MonteCarlo.DataInfo(RowNo).Table = "" 'A Data Table does not have a Destination Table parameter.

                'Deactivate the Parameter A Name:
                'dgvMCVariables.Rows(RowNo).Cells(7).Value = ""
                'dgvMCVariables.Rows(RowNo).Cells(7).Style.BackColor = Color.LightGray
                'dgvMCVariables.Rows(RowNo).Cells(7).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(8).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(8).ReadOnly = True
                'Deactivate the Parameter A Value:
                'dgvMCVariables.Rows(RowNo).Cells(8).Value = ""
                'dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.LightGray
                'dgvMCVariables.Rows(RowNo).Cells(8).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(9).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(9).ReadOnly = True
                'Deactivate the Parameter B Name:
                'dgvMCVariables.Rows(RowNo).Cells(9).Value = ""
                'dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.LightGray
                'dgvMCVariables.Rows(RowNo).Cells(9).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(10).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(10).ReadOnly = True
                'Deactivate the Parameter B Value:
                'dgvMCVariables.Rows(RowNo).Cells(10).Value = ""
                'dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.LightGray
                'dgvMCVariables.Rows(RowNo).Cells(10).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(11).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(11).ReadOnly = True
                'Deactivate the Parameter C Name:
                'dgvMCVariables.Rows(RowNo).Cells(11).Value = ""
                'dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.LightGray
                'dgvMCVariables.Rows(RowNo).Cells(11).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(12).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(12).ReadOnly = True
                'Deactivate the Parameter C Value:
                'dgvMCVariables.Rows(RowNo).Cells(12).Value = ""
                'dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.LightGray
                'dgvMCVariables.Rows(RowNo).Cells(12).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(13).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(13).ReadOnly = True
                'Deactivate the Parameter D Name:
                'dgvMCVariables.Rows(RowNo).Cells(13).Value = ""
                'dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.LightGray
                'dgvMCVariables.Rows(RowNo).Cells(13).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(14).ReadOnly = True
                'Deactivate the Parameter D Value:
                'dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                'dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                'dgvMCVariables.Rows(RowNo).Cells(14).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).ReadOnly = True
                'Deactivate the Parameter E Name:
                'dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                'dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                'dgvMCVariables.Rows(RowNo).Cells(15).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).ReadOnly = True
                'Deactivate the Parameter E Value:
                'dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                'dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                'dgvMCVariables.Rows(RowNo).Cells(16).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).ReadOnly = True
                'Deactivate the Seed column:
                'dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                'dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                'dgvMCVariables.Rows(RowNo).Cells(17).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(18).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(18).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(18).ReadOnly = True
                'Deactivate the Plot column:
                'dgvMCVariables.Rows(RowNo).Cells(18).Value = ""
                'dgvMCVariables.Rows(RowNo).Cells(18).Style.BackColor = Color.LightGray
                'dgvMCVariables.Rows(RowNo).Cells(18).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(19).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(19).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(19).ReadOnly = True

            Case "Trial Number" 'The trial number of a realization.
                'Deactivate the Sampling selection:
                dgvMCVariables.Rows(RowNo).Cells(6).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(6).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(6).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(8).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(9).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(9).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(10).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(10).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(11).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(11).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(12).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(12).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(13).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(13).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(14).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).ReadOnly = True
                'Deactivate the Seed column:
                dgvMCVariables.Rows(RowNo).Cells(18).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(18).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(18).ReadOnly = True
                'Deactivate the Plot column:
                dgvMCVariables.Rows(RowNo).Cells(19).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(19).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(19).ReadOnly = True

            Case "Imported Data" 'Imported data is real data that has been imported for analysis.
                'Deactivate the Sampling selection:
                dgvMCVariables.Rows(RowNo).Cells(6).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(6).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(6).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(8).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(8).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(9).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(9).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(10).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(10).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(11).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(11).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(12).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(12).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(13).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(13).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(14).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).ReadOnly = True
                'Deactivate the Seed column:
                dgvMCVariables.Rows(RowNo).Cells(18).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(18).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(18).ReadOnly = True

            Case "Normal Scores"  'The standard normal CDF scaled to a variance of 1. Used in the Iman-Conover method of generating correlated variables.
                'Deactivate the Sampling selection:
                dgvMCVariables.Rows(RowNo).Cells(6).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(6).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(6).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(8).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(8).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(9).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(9).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(10).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(11).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(12).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(13).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                'Deactivate the Seed column:
                dgvMCVariables.Rows(RowNo).Cells(18).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(18).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(18).ReadOnly = True

            Case "C2 - Beta"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "Alpha"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = "Beta"
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(12).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(13).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                MonteCarlo.DataInfo(RowNo).IsDiscrete = False

            Case "C4 - Beta Scaled"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "Alpha"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = "Beta"
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(12).Value = "Mu"
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(14).Value = "Sigma"
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                MonteCarlo.DataInfo(RowNo).IsDiscrete = False

                'Case "Burr"

                'Case "Categorical"

            Case "C2 - Cauchy"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "x0"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = "Gamma"
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(12).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(13).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                MonteCarlo.DataInfo(RowNo).IsDiscrete = False

            Case "C1 - Chi Squared"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "k"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(11).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(12).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(13).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                MonteCarlo.DataInfo(RowNo).IsDiscrete = False

            Case "C2 - Continuous Uniform"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "a"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = "b"
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(12).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(13).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                MonteCarlo.DataInfo(RowNo).IsDiscrete = False

            Case "C1 - Exponential"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "Lambda"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(11).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(12).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(13).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                MonteCarlo.DataInfo(RowNo).IsDiscrete = False

            Case "C2 - Fisher-Snedecor"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "d1"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = "d2"
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(12).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(13).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                MonteCarlo.DataInfo(RowNo).IsDiscrete = False

            Case "C2 - Gamma"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "Alpha"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = "Beta"
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(12).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(13).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                MonteCarlo.DataInfo(RowNo).IsDiscrete = False

            Case "C2 - Inverse Gaussian"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "Mu"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = "Lambda"
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(12).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(13).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                MonteCarlo.DataInfo(RowNo).IsDiscrete = False

            Case "C2 - Log Normal"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "Mu"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = "Sigma"
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(12).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(13).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                MonteCarlo.DataInfo(RowNo).IsDiscrete = False

            Case "C2 - Normal"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "Mean"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(8).ReadOnly = False
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).ReadOnly = False
                dgvMCVariables.Rows(RowNo).Cells(10).Value = "Std Dev"
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).ReadOnly = False
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(11).ReadOnly = False
                dgvMCVariables.Rows(RowNo).Cells(12).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(12).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(13).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(13).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(14).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).ReadOnly = True
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).ReadOnly = True
                MonteCarlo.DataInfo(RowNo).IsDiscrete = False

            Case "C2 - Pareto"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "xm"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = "Alpha"
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(12).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(13).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                MonteCarlo.DataInfo(RowNo).IsDiscrete = False

            Case "C1 - Rayleigh"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "Sigma"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(11).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(12).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(13).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                MonteCarlo.DataInfo(RowNo).IsDiscrete = False

            Case "C4 - Skewed Generalized Error"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "Mu"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = "Sigma"
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(12).Value = "Lambda"
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(14).Value = "p"
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                MonteCarlo.DataInfo(RowNo).IsDiscrete = False

            Case "C5 - Skewed Generalized T"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "Mu"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = "Sigma"
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(12).Value = "Lambda"
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(14).Value = "p"
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(16).Value = "q"
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.White
                MonteCarlo.DataInfo(RowNo).IsDiscrete = False

            Case "C3 - Student's T"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "Mu"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = "Sigma"
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(12).Value = "Nu"
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                MonteCarlo.DataInfo(RowNo).IsDiscrete = False

            Case "C3 - Triangular"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "a - Minimum"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = "b - Maximum"
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(12).Value = "c - Peak"
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                MonteCarlo.DataInfo(RowNo).IsDiscrete = False

            Case "C3 - Truncated Pareto"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "xm"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = "Alpha"
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(12).Value = "T"
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                MonteCarlo.DataInfo(RowNo).IsDiscrete = False

            Case "D1 - Bernoulli"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "P success"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(11).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(12).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(13).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                MonteCarlo.DataInfo(RowNo).IsDiscrete = True

            Case "D2 - Binomial"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "P success"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = "No. trials"
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(12).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(13).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                MonteCarlo.DataInfo(RowNo).IsDiscrete = True

            Case "D1 - Categorical"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "P Mass()"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(11).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(12).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(13).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                MonteCarlo.DataInfo(RowNo).IsDiscrete = True

            Case "D2 - Discrete Uniform"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "a"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = "b"
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(12).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(13).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                MonteCarlo.DataInfo(RowNo).IsDiscrete = True

            Case "D1 - Geometric"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "P success"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(11).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(12).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(13).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                MonteCarlo.DataInfo(RowNo).IsDiscrete = True

            Case "D3 - Hypergeometric"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "Population"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = "Successes"
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(12).Value = "Draws"
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                MonteCarlo.DataInfo(RowNo).IsDiscrete = True

            Case "D2 - Negative Binomial"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "r"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = "P success"
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(12).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(13).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                MonteCarlo.DataInfo(RowNo).IsDiscrete = True

            Case "D1 - Poisson"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "Lambda"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(11).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(12).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(13).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                MonteCarlo.DataInfo(RowNo).IsDiscrete = True

            Case "D2 - Zipf"
                dgvMCVariables.Rows(RowNo).Cells(8).Value = "s"
                dgvMCVariables.Rows(RowNo).Cells(8).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(9).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(10).Value = "n"
                dgvMCVariables.Rows(RowNo).Cells(10).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(11).Style.BackColor = Color.White
                dgvMCVariables.Rows(RowNo).Cells(12).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(12).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(13).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(13).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(14).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(14).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(15).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(15).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(16).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(16).Style.BackColor = Color.LightGray
                dgvMCVariables.Rows(RowNo).Cells(17).Value = ""
                dgvMCVariables.Rows(RowNo).Cells(17).Style.BackColor = Color.LightGray
                MonteCarlo.DataInfo(RowNo).IsDiscrete = True

            Case Else
                Message.AddWarning("Unknown distribution: " & DistName & vbCrLf)

        End Select

    End Sub

    Private Sub txtNTrials_LostFocus(sender As Object, e As EventArgs) Handles txtNTrials.LostFocus
        MonteCarlo.NTrials = txtNTrials.Text
        txtNTrials2.Text = txtNTrials.Text
    End Sub

    Private Sub btnGenRVData_Click(sender As Object, e As EventArgs) Handles btnGenRVData.Click
        'Generate the Random Variable data.

        Dim StartTime As Date = Now

        MonteCarlo.GenerateData()

        Dim Duration As TimeSpan = Now - StartTime
        Message.Add("Generated random variable data: " & " Time taken: " & Duration.TotalMilliseconds & " ms" & vbCrLf & vbCrLf)

        UpdateTableList()

        MCTableName = "Calculations"

    End Sub

    Private Sub btnApplyCorrelations_Click(sender As Object, e As EventArgs) Handles btnApplyCorrelations.Click
        If cmbCorrelations.SelectedIndex > -1 Then
            MonteCarlo.ApplyCorrMatrix(cmbCorrelations.SelectedItem.ToString)
            UpdateTableList()
        Else
            Message.AddWarning("Please select a correlation matrix." & vbCrLf)
        End If
    End Sub

    Private Sub btnGetVarsRandScores_Click(sender As Object, e As EventArgs) Handles btnGetVarsRandScores.Click
        If cmbCorrelations.SelectedIndex > -1 Then
            MonteCarlo.GetSortedVarsRandomScores(cmbCorrelations.SelectedItem.ToString)
            UpdateTableList()
        Else
            Message.AddWarning("Please select a correlation matrix." & vbCrLf)
        End If
    End Sub

    Private Sub btnRankScores_Click(sender As Object, e As EventArgs) Handles btnRankScores.Click
        If cmbCorrelations.SelectedIndex > -1 Then
            MonteCarlo.RankScores(cmbCorrelations.SelectedItem.ToString)
            UpdateTableList()
        Else
            Message.AddWarning("Please select a correlation matrix." & vbCrLf)
        End If
    End Sub

    Private Sub btnRankVars_Click(sender As Object, e As EventArgs) Handles btnRankVars.Click
        If cmbCorrelations.SelectedIndex > -1 Then
            MonteCarlo.RankVariables(cmbCorrelations.SelectedItem.ToString)
            UpdateTableList()
        Else
            Message.AddWarning("Please select a correlation matrix." & vbCrLf)
        End If
    End Sub

    Private Sub cmbCorrelations_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCorrelations.SelectedIndexChanged
        'The selected Correlation Matrix has changed.
        If cmbCorrelations.Focused Then
            If cmbCorrelations.SelectedIndex = -1 Then
                MonteCarlo.SelCorrMatName = ""
            Else
                MonteCarlo.SelCorrMatName = cmbCorrelations.SelectedItem.ToString
            End If
        End If
    End Sub

    Private Sub btnRunAllTrialCalcs_Click(sender As Object, e As EventArgs) Handles btnRunAllTrialCalcs.Click
        'Run all the trials
        Try
            If MonteCarlo.Data.Tables.Contains("Calculations") Then
                Dim ColumnName As String
                Dim ColumnType As String
                Dim ItemName As String
                Dim ColumnMissing As Boolean = False
                Dim NodeError As Boolean = False
                Dim Input As New List(Of InputInfo)
                Dim Output As New List(Of OutputInfo)
                Dim CalcInfoToDelete As New List(Of String)

                Dim StartTime As Date = Now

                For Each item In CalcInfo
                    If item.Value.Type = "Input Variable" Then
                        'Get the value from the Calculations table
                        ItemName = item.Key
                        If ColumnInfo.ContainsKey(ItemName) Then
                            Dim NewInputInfo As New InputInfo
                            NewInputInfo.Name = ItemName
                            NewInputInfo.ColumnName = ColumnInfo(ItemName).Name
                            Input.Add(NewInputInfo)
                        Else
                            CalcInfoToDelete.Add(ItemName)
                        End If
                    End If
                Next

                If ColumnMissing = False Then
                    'Create any required Output Value Columns in the Calculations table:
                    For Each item In CalcInfo
                        If item.Value.Type = "Output Value" Then
                            ItemName = item.Key
                            If ColumnInfo.ContainsKey(ItemName) Then
                                ColumnName = ColumnInfo(ItemName).Name
                                ColumnType = ColumnInfo(ItemName).Type

                                Dim NewOutputInfo As New OutputInfo
                                NewOutputInfo.Name = ItemName
                                NewOutputInfo.ColumnName = ColumnName
                                Dim myNode As TreeNode() = trvCalculations.Nodes.Find(ItemName, True) 'Find the Output Value node.
                                If myNode.Count = 1 Then
                                    NewOutputInfo.Node = myNode(0)
                                    Output.Add(NewOutputInfo)

                                    If MonteCarlo.Data.Tables("Calculations").Columns.Contains(ColumnName) Then
                                        'The Output Value column exists in the Calculations table.
                                    Else
                                        'Create the Output Value column:
                                        MonteCarlo.CreateNewColumn("Calculations", ColumnName, ColumnType)
                                    End If

                                ElseIf myNode.Count = 0 Then
                                    CalcInfoToDelete.Add(ItemName)
                                Else
                                    Message.AddWarning("More than one Calculation Tree node found for Output Value: " & ItemName & vbCrLf)
                                    NodeError = True
                                End If
                            Else
                                Message.AddWarning("Output Value column information not found for Output Value: " & ItemName & vbCrLf)
                                ColumnMissing = True
                            End If
                        End If
                    Next

                    'Delete any unused Output Value entries:
                    For Each item In CalcInfoToDelete
                        CalcInfo.Remove(item)
                    Next

                    If ColumnMissing = False Then
                        If NodeError = False Then
                            Dim I As Integer
                            For I = 1 To MonteCarlo.NTrials
                                'Get the Input Variables for the current trial:
                                For Each item In Input
                                    ScalarData(item.Name) = MonteCarlo.Data.Tables("Calculations").Rows(I - 1).Item(item.ColumnName)
                                Next
                                'Recalculate each Output Value and save in the Calculations table:
                                For Each item In Output
                                    RecalcNode(item.Node)
                                    MonteCarlo.Data.Tables("Calculations").Rows(I - 1).Item(item.ColumnName) = ScalarData(item.Name)
                                Next
                            Next
                            Message.Add("Monte Carlo simulation complete." & vbCrLf)

                            Dim Duration As TimeSpan = Now - StartTime
                            Message.Add("Time taken: " & Duration.TotalMilliseconds & " ms" & vbCrLf & vbCrLf)

                        Else
                            Message.AddWarning("Output Value node information not found for at least one Output Value: " & vbCrLf)
                        End If
                    Else
                        Message.AddWarning("Calculations column name not found for at lease one Output Value" & vbCrLf)
                    End If
                Else
                    Message.AddWarning("At least one Input Variable column is missing from the Calculations table. The Monte Carlo simulation can not be run." & vbCrLf)
                End If
            Else
                Message.AddWarning("There is no Calculations table. No Monte Carlo input data is available." & vbCrLf)
            End If
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub RecalcNode(ByVal myNode As TreeNode)
        'Recalculate the node.
        'This method assumes the input values have been updated and all the child nodes values are recalculated.

        Dim ItemName As String = myNode.Text

        If CalcInfo.ContainsKey(ItemName) Then
            Select Case CalcInfo(ItemName).Type
                Case "Calculation Sequence"
                    'Recalculate each child node.
                    For Each childNode As TreeNode In myNode.Nodes
                        RecalcNode(childNode)
                    Next

                Case "Input Variable"
                    'This is a leaf node - no recalculation needed.

                Case "Input Variable User Defined"
                    'This is a leaf node - no recalculation needed.

                Case "Output Value"
                    'Recalculate the child node.
                    RecalcNode(myNode.Nodes(0))
                    ScalarData(myNode.Name) = ScalarData(myNode.Nodes(0).Text) 'Copy the recalculated child node value to the Output Value

                Case "Process"
                    'Recalculate each child node.
                    For Each childNode As TreeNode In myNode.Nodes
                        RecalcNode(childNode)
                    Next

                Case "Value Process"
                    'Recalculate the child node.
                    RecalcNode(myNode.Nodes(0))

                Case "Collection"
                    'Recalculate each child node.
                    For Each childNode As TreeNode In myNode.Nodes
                        RecalcNode(childNode)
                    Next

                Case "Value Copy"
                    'This is a leaf node - no recalculation needed.

                Case "Constant Value E"
                    'This is a leaf node - no recalculation needed.

                Case "Constant Value Pi"
                    'This is a leaf node - no recalculation needed.

                Case "Constant Value User Defined"
                    'This is a leaf node - no recalculation needed.

                Case "Add"
                    'This should have two child nodes.
                    RecalcNode(myNode.Nodes(0))
                    RecalcNode(myNode.Nodes(1))
                    ScalarData(myNode.Text) = ScalarData(myNode.Nodes(0).Text) + ScalarData(myNode.Nodes(1).Text)

                Case "Subtract"
                    'This should have two child nodes.
                    RecalcNode(myNode.Nodes(0))
                    RecalcNode(myNode.Nodes(1))
                    ScalarData(myNode.Text) = ScalarData(myNode.Nodes(0).Text) - ScalarData(myNode.Nodes(1).Text)

                Case "Multiply"
                    'This should have two child nodes.
                    RecalcNode(myNode.Nodes(0))
                    RecalcNode(myNode.Nodes(1))
                    ScalarData(myNode.Text) = ScalarData(myNode.Nodes(0).Text) * ScalarData(myNode.Nodes(1).Text)

                Case "Divide"
                    'This should have two child nodes.
                    RecalcNode(myNode.Nodes(0))
                    RecalcNode(myNode.Nodes(1))
                    ScalarData(myNode.Text) = ScalarData(myNode.Nodes(0).Text) / ScalarData(myNode.Nodes(1).Text)

                Case "Sum"
                    'Process each child node.
                    Dim Sum As Double = 0
                    For Each childNode As TreeNode In myNode.Nodes
                        RecalcNode(childNode)
                        Sum += ScalarData(childNode.Text)
                    Next
                    ScalarData(myNode.Text) = Sum

                Case "Product"
                    'Process each child node.
                    Dim Product As Double = 1
                    For Each childNode As TreeNode In myNode.Nodes
                        RecalcNode(childNode)
                        Product *= ScalarData(childNode.Text)
                    Next
                    ScalarData(myNode.Text) = Product

                Case "Sine"
                    'This should have one child node.
                    RecalcNode(myNode.Nodes(0))
                    ScalarData(myNode.Text) = Math.Sin(ScalarData(myNode.Nodes(0).Text))

                Case "Cosine"
                    'This should have one child node.
                    RecalcNode(myNode.Nodes(0))
                    ScalarData(myNode.Text) = Math.Cos(ScalarData(myNode.Nodes(0).Text))

                Case "Tangent"
                    'This should have one child node.
                    RecalcNode(myNode.Nodes(0))
                    ScalarData(myNode.Text) = Math.Tan(ScalarData(myNode.Nodes(0).Text))

                Case "ArcSine"
                    'This should have one child node.
                    RecalcNode(myNode.Nodes(0))
                    ScalarData(myNode.Text) = Math.Asin(ScalarData(myNode.Nodes(0).Text))

                Case "ArcCosine"
                    'This should have one child node.
                    RecalcNode(myNode.Nodes(0))
                    ScalarData(myNode.Text) = Math.Acos(ScalarData(myNode.Nodes(0).Text))

                Case "ArcTangent"
                    'This should have one child node.
                    RecalcNode(myNode.Nodes(0))
                    ScalarData(myNode.Text) = Math.Atan(ScalarData(myNode.Nodes(0).Text))

                Case "Degrees To Radians"
                    'This should have one child node.
                    RecalcNode(myNode.Nodes(0))
                    ScalarData(myNode.Text) = ScalarData(myNode.Nodes(0).Text) * 2 * Math.PI / 360

                Case "Radians To Degrees"
                    'This should have one child node.
                    RecalcNode(myNode.Nodes(0))
                    ScalarData(myNode.Text) = ScalarData(myNode.Nodes(0).Text) / 2 / Math.PI * 360

                Case "Power Of E"
                    'This should have one child node.
                    RecalcNode(myNode.Nodes(0))
                    ScalarData(myNode.Text) = Math.E ^ ScalarData(myNode.Nodes(0).Text)

                Case "Natural Logarithm"
                    'This should have one child node.
                    RecalcNode(myNode.Nodes(0))
                    ScalarData(myNode.Text) = Math.Log(ScalarData(myNode.Nodes(0).Text))

                Case "Power Of Ten"
                    'This should have one child node.
                    RecalcNode(myNode.Nodes(0))
                    ScalarData(myNode.Text) = 10 ^ ScalarData(myNode.Nodes(0).Text)

                Case "Logarithm"
                    'This should have one child node.
                    RecalcNode(myNode.Nodes(0))
                    ScalarData(myNode.Text) = Math.Log10(ScalarData(myNode.Nodes(0).Text))

                Case "Square"
                    'This should have one child node.
                    RecalcNode(myNode.Nodes(0))
                    ScalarData(myNode.Text) = ScalarData(myNode.Nodes(0).Text) ^ 2

                Case "Square Root"
                    'This should have one child node.
                    RecalcNode(myNode.Nodes(0))
                    ScalarData(myNode.Text) = Math.Sqrt(ScalarData(myNode.Nodes(0).Text))

                Case "Cube"
                    'This should have one child node.
                    RecalcNode(myNode.Nodes(0))
                    ScalarData(myNode.Text) = ScalarData(myNode.Nodes(0).Text) ^ 3

                Case "Cube Root"
                    'This should have one child node.
                    RecalcNode(myNode.Nodes(0))
                    ScalarData(myNode.Text) = ScalarData(myNode.Nodes(0).Text) ^ (1 / 3)

                Case "Exponentiate"
                    'This should have two child nodes.
                    RecalcNode(myNode.Nodes(0))
                    RecalcNode(myNode.Nodes(1))
                    ScalarData(myNode.Text) = ScalarData(myNode.Nodes(0).Text) ^ ScalarData(myNode.Nodes(1).Text)

                Case "Root"
                    'This should have two child nodes.
                    RecalcNode(myNode.Nodes(0))
                    RecalcNode(myNode.Nodes(1))
                    ScalarData(myNode.Text) = ScalarData(myNode.Nodes(0).Text) ^ (1 / ScalarData(myNode.Nodes(1).Text))

                Case "Equals"
                    'This should have one child node.
                    RecalcNode(myNode.Nodes(0))
                    ScalarData(myNode.Text) = ScalarData(myNode.Nodes(0).Text)

                Case "Negate"
                    'This should have one child node.
                    RecalcNode(myNode.Nodes(0))
                    ScalarData(myNode.Text) = -ScalarData(myNode.Nodes(0).Text)

                Case "Invert"
                    'This should have one child node.
                    RecalcNode(myNode.Nodes(0))
                    ScalarData(myNode.Text) = 1 / ScalarData(myNode.Nodes(0).Text)

                Case "Absolute"
                    'This should have one child node.
                    RecalcNode(myNode.Nodes(0))
                    ScalarData(myNode.Text) = Math.Abs(ScalarData(myNode.Nodes(0).Text))

                Case "Round"
                    'This should have one child node.
                    RecalcNode(myNode.Nodes(0))
                    ScalarData(myNode.Text) = Math.Round(ScalarData(myNode.Nodes(0).Text))

                Case "Round Up"
                    'This should have one child node.
                    RecalcNode(myNode.Nodes(0))
                    ScalarData(myNode.Text) = Math.Ceiling(ScalarData(myNode.Nodes(0).Text))

                Case "Round Down"
                    'This should have one child node.
                    RecalcNode(myNode.Nodes(0))
                    ScalarData(myNode.Text) = Math.Floor(ScalarData(myNode.Nodes(0).Text))

                Case "If Gt"
                    'This should have four child nodes.
                    RecalcNode(myNode.Nodes(0))
                    RecalcNode(myNode.Nodes(1))
                    RecalcNode(myNode.Nodes(2))
                    RecalcNode(myNode.Nodes(3))
                    If ScalarData(myNode.Nodes(0).Text) > ScalarData(myNode.Nodes(1).Text) Then ScalarData(myNode.Text) = ScalarData(myNode.Nodes(2).Text) Else ScalarData(myNode.Text) = ScalarData(myNode.Nodes(3).Text)

                Case "If GtEq"
                    'This should have four child nodes.
                    RecalcNode(myNode.Nodes(0))
                    RecalcNode(myNode.Nodes(1))
                    RecalcNode(myNode.Nodes(2))
                    RecalcNode(myNode.Nodes(3))
                    If ScalarData(myNode.Nodes(0).Text) >= ScalarData(myNode.Nodes(1).Text) Then ScalarData(myNode.Text) = ScalarData(myNode.Nodes(2).Text) Else ScalarData(myNode.Text) = ScalarData(myNode.Nodes(3).Text)

                Case "If Eq"
                    'This should have four child nodes.
                    RecalcNode(myNode.Nodes(0))
                    RecalcNode(myNode.Nodes(1))
                    RecalcNode(myNode.Nodes(2))
                    RecalcNode(myNode.Nodes(3))
                    If ScalarData(myNode.Nodes(0).Text) = ScalarData(myNode.Nodes(1).Text) Then ScalarData(myNode.Text) = ScalarData(myNode.Nodes(2).Text) Else ScalarData(myNode.Text) = ScalarData(myNode.Nodes(3).Text)

                Case "If LtEq"
                    'This should have four child nodes.
                    RecalcNode(myNode.Nodes(0))
                    RecalcNode(myNode.Nodes(1))
                    RecalcNode(myNode.Nodes(2))
                    RecalcNode(myNode.Nodes(3))
                    If ScalarData(myNode.Nodes(0).Text) <= ScalarData(myNode.Nodes(1).Text) Then ScalarData(myNode.Text) = ScalarData(myNode.Nodes(2).Text) Else ScalarData(myNode.Text) = ScalarData(myNode.Nodes(3).Text)

                Case "If Lt"
                    'This should have four child nodes.
                    RecalcNode(myNode.Nodes(0))
                    RecalcNode(myNode.Nodes(1))
                    RecalcNode(myNode.Nodes(2))
                    RecalcNode(myNode.Nodes(3))
                    If ScalarData(myNode.Nodes(0).Text) < ScalarData(myNode.Nodes(1).Text) Then ScalarData(myNode.Text) = ScalarData(myNode.Nodes(2).Text) Else ScalarData(myNode.Text) = ScalarData(myNode.Nodes(3).Text)

                Case Else
                    Message.AddWarning("Unknown node type: " & CalcInfo(SelItemName).Type & vbCrLf)
            End Select
        End If
    End Sub

    Private Sub btnAddRV_Click(sender As Object, e As EventArgs) Handles btnAddRV.Click
        'Add a new Random Variable to the Monte Carlo Variables grid.

        If dgvMCVariables.Rows.Count <> MonteCarlo.DataInfo.Count Then
            Message.AddWarning("The number of variables displayed (" & dgvMCVariables.Rows.Count & ") does not match the number of items stored in the DataInfo list (" & MonteCarlo.DataInfo.Count & ")." & vbCrLf)
            Message.AddWarning("Redisplaying the DataInfo list." & vbCrLf)
        End If

        dgvMCVariables.Rows.Add() 'Add the new Random Variable grid row. 
        MonteCarlo.DataInfo.Add(New clsMonteCarlo.DataInformation) 'Add a corresponding entry to the DataInfo list.  
    End Sub

    Private Sub btnGenSelRVData_Click(sender As Object, e As EventArgs) Handles btnGenSelRVData.Click
        'Generate the selected Random Variable data.
        If dgvMCVariables.SelectedCells.Count = 0 Then
            Message.AddWarning("Please select a Random Variable." & vbCrLf)
        Else
            Dim RowNo As Integer = dgvMCVariables.SelectedCells(0).RowIndex
            Dim RVName As String = dgvMCVariables.Rows(RowNo).Cells(0).Value
            If RVName = "" Then
                Message.AddWarning("Please enter a name for the Random Variable." & vbCrLf)
            Else
                MonteCarlo.GenerateData(RowNo)
                UpdateTableList()
                UpdateMCDataTableView()
            End If
        End If
    End Sub

    Private Sub btnMoveRVUp_Click(sender As Object, e As EventArgs) Handles btnMoveRVUp.Click
        'Move the selected Variable up one position.
        If dgvMCVariables.SelectedCells.Count = 0 Then
            Message.AddWarning("Please select a Random Variable." & vbCrLf)
        Else
            Dim SelRow As Integer = dgvMCVariables.SelectedCells(0).RowIndex
            MonteCarlo.MoveDataInfoEntryUp(SelRow)
            DisplayMCVariables()
        End If
    End Sub

    Private Sub btnMoveRVDown_Click(sender As Object, e As EventArgs) Handles btnMoveRVDown.Click
        'Move the selected Variable down one position.
        If dgvMCVariables.SelectedCells.Count = 0 Then
            Message.AddWarning("Please select a Random Variable." & vbCrLf)
        Else
            Dim SelRow As Integer = dgvMCVariables.SelectedCells(0).RowIndex
            MonteCarlo.MoveDataInfoEntryDown(SelRow)
            DisplayMCVariables()
        End If
    End Sub

    Private Sub btnDeleteRV_Click(sender As Object, e As EventArgs) Handles btnDeleteRV.Click
        'Delete the selected Variable
        If dgvMCVariables.SelectedCells.Count = 0 Then
            Message.AddWarning("Please select a Random Variable." & vbCrLf)
        Else
            Dim SelRow As Integer = dgvMCVariables.SelectedCells(0).RowIndex
            MonteCarlo.DataInfo.RemoveAt(SelRow)
            DisplayMCVariables()
        End If
    End Sub

    'Private Sub chkPDF_CheckedChanged(sender As Object, e As EventArgs) Handles chkPDF.CheckedChanged
    '    Dim ItemNo As Integer

    '    If dgvMCVariables.SelectedCells().Count = 0 Then
    '        If chkPDF.Focused Then Message.AddWarning("Please select a data item." & vbCrLf)
    '    Else
    '        ItemNo = dgvMCVariables.SelectedCells(0).RowIndex
    '        MonteCarlo.DataInfo(ItemNo).ShowPDF = chkPDF.Checked
    '    End If
    'End Sub

    'Private Sub chkCDF_CheckedChanged(sender As Object, e As EventArgs) Handles chkCDF.CheckedChanged
    '    Dim ItemNo As Integer

    '    If dgvMCVariables.SelectedCells().Count = 0 Then
    '        If chkCDF.Focused Then Message.AddWarning("Please select a data item." & vbCrLf)
    '    Else
    '        ItemNo = dgvMCVariables.SelectedCells(0).RowIndex
    '        MonteCarlo.DataInfo(ItemNo).ShowCDF = chkCDF.Checked
    '    End If
    'End Sub

    'Private Sub chkInvCDF_CheckedChanged(sender As Object, e As EventArgs) Handles chkRevCDF.CheckedChanged
    '    Dim ItemNo As Integer

    '    If dgvMCVariables.SelectedCells().Count = 0 Then
    '        If chkRevCDF.Focused Then Message.AddWarning("Please select a data item." & vbCrLf)
    '    Else
    '        ItemNo = dgvMCVariables.SelectedCells(0).RowIndex
    '        MonteCarlo.DataInfo(ItemNo).ShowRevCDF = chkRevCDF.Checked
    '    End If
    'End Sub

    Private Sub txtNRVPoints_TextChanged(sender As Object, e As EventArgs) Handles txtNRVPoints.TextChanged

    End Sub

    Private Sub txtNRVPoints_LostFocus(sender As Object, e As EventArgs) Handles txtNRVPoints.LostFocus
        Dim ItemNo As Integer

        If dgvMCVariables.SelectedCells().Count = 0 Then
            If txtNRVPoints.Focused Then Message.AddWarning("Please select a data item." & vbCrLf)
        Else
            ItemNo = dgvMCVariables.SelectedCells(0).RowIndex
            MonteCarlo.DataInfo(ItemNo).NDisplayPoints = txtNRVPoints.Text
        End If
    End Sub

    Private Sub txtXMin_TextChanged(sender As Object, e As EventArgs) Handles txtXMin.TextChanged

    End Sub

    Private Sub txtXMin_LostFocus(sender As Object, e As EventArgs) Handles txtXMin.LostFocus
        Dim ItemNo As Integer

        If dgvMCVariables.SelectedCells().Count = 0 Then
            'Message.AddWarning("Please select a data item." & vbCrLf)
            If txtXMin.Focused Then Message.AddWarning("Please select a data item." & vbCrLf)
        Else
            ItemNo = dgvMCVariables.SelectedCells(0).RowIndex
            MonteCarlo.DataInfo(ItemNo).XMin = txtXMin.Text
        End If
    End Sub

    Private Sub txtXMax_TextChanged(sender As Object, e As EventArgs) Handles txtXMax.TextChanged

    End Sub

    Private Sub txtXMax_LostFocus(sender As Object, e As EventArgs) Handles txtXMax.LostFocus
        Dim ItemNo As Integer

        If dgvMCVariables.SelectedCells().Count = 0 Then
            If txtXMax.Focused Then Message.AddWarning("Please select a data item." & vbCrLf)
        Else
            ItemNo = dgvMCVariables.SelectedCells(0).RowIndex
            MonteCarlo.DataInfo(ItemNo).XMax = txtXMax.Text
        End If
    End Sub

    Private Sub txtXGridInt_TextChanged(sender As Object, e As EventArgs) Handles txtXGridInt.TextChanged

    End Sub

    Private Sub txtXGridInt_LostFocus(sender As Object, e As EventArgs) Handles txtXGridInt.LostFocus
        Dim ItemNo As Integer

        If dgvMCVariables.SelectedCells().Count = 0 Then
            If txtXGridInt.Focused Then Message.AddWarning("Please select a data item." & vbCrLf)
        Else
            ItemNo = dgvMCVariables.SelectedCells(0).RowIndex
            MonteCarlo.DataInfo(ItemNo).XGridInterval = txtXGridInt.Text
        End If
    End Sub

    Private Sub chkAutoYMax_CheckedChanged(sender As Object, e As EventArgs) Handles chkAutoYMax.CheckedChanged
        Dim ItemNo As Integer
        If dgvMCVariables.SelectedCells().Count = 0 Then
            If chkAutoYMax.Focused Then Message.AddWarning("Please select a data item." & vbCrLf)
        Else
            ItemNo = dgvMCVariables.SelectedCells(0).RowIndex
            MonteCarlo.DataInfo(ItemNo).AutoYMax = chkAutoYMax.Checked
        End If
    End Sub

    Private Sub txtYMax_TextChanged(sender As Object, e As EventArgs) Handles txtYMax.TextChanged

    End Sub

    Private Sub txtYMax_LostFocus(sender As Object, e As EventArgs) Handles txtYMax.LostFocus
        Dim ItemNo As Integer

        If dgvMCVariables.SelectedCells().Count = 0 Then
            If txtYMax.Focused Then Message.AddWarning("Please select a data item." & vbCrLf)
        Else
            ItemNo = dgvMCVariables.SelectedCells(0).RowIndex
            MonteCarlo.DataInfo(ItemNo).YMax = txtYMax.Text
        End If
    End Sub

    Private Sub chkAutoXMin_CheckedChanged(sender As Object, e As EventArgs) Handles chkAutoXMin.CheckedChanged
        Dim ItemNo As Integer

        If dgvMCVariables.SelectedCells().Count = 0 Then
            If chkAutoXMin.Focused Then Message.AddWarning("Please select a data item." & vbCrLf)
        Else
            ItemNo = dgvMCVariables.SelectedCells(0).RowIndex
            MonteCarlo.DataInfo(ItemNo).AutoXMin = chkAutoXMin.Checked
        End If
    End Sub

    Private Sub chkAutoXMax_CheckedChanged(sender As Object, e As EventArgs) Handles chkAutoXMax.CheckedChanged
        Dim ItemNo As Integer

        If dgvMCVariables.SelectedCells().Count = 0 Then
            If chkAutoXMax.Focused Then Message.AddWarning("Please select a data item." & vbCrLf)
        Else
            ItemNo = dgvMCVariables.SelectedCells(0).RowIndex
            MonteCarlo.DataInfo(ItemNo).AutoXMax = chkAutoXMax.Checked
        End If
    End Sub

    Private Sub btnPlot_Click(sender As Object, e As EventArgs) Handles btnPlot.Click
        'Plot the selected Random Variable
        If dgvMCVariables.SelectedCells.Count = 0 Then
            Message.AddWarning("Please select a Random Variable." & vbCrLf)
        Else
            Dim SelRow As Integer = dgvMCVariables.SelectedCells(0).RowIndex


            'Message.Add("Plotting random variable " & MonteCarlo.DataInfo(SelRow).Name & " in table " & MonteCarlo.DataInfo(SelRow).Table & vbCrLf)
            'First check if the Random Variable chart is already open:
            Dim ChartFound As Boolean = False
            If DistribChartFormList.Count = 0 Then

            Else
                Dim I As Integer
                For I = 0 To DistribChartFormList.Count - 1
                    If DistribChartFormList(I) Is Nothing Then
                        'Empty item in the list - ignore.
                    Else
                        If DistribChartFormList(I).IndexNo = MonteCarlo.SelVarIndex Then
                            ChartFound = True
                            DistribChartFormList(I).BringToFront 'Bring form to the front.
                            DistribChartFormList(I).IndexNo = SelRow
                            DistribChartFormList(I).VariableName = MonteCarlo.DataInfo(SelRow).Name 'Update the random variable name.
                            'DistribChartFormList(I).RestoreFormSettings
                            DistribChartFormList(I).TableName = MonteCarlo.DataInfo(SelRow).Table
                            DistribChartFormList(I).Units = dgvMCVariables.Rows(SelRow).Cells(1).Value
                            DistribChartFormList(I).UnitsAbbrev = dgvMCVariables.Rows(SelRow).Cells(2).Value
                            'DistribChartFormList(I).Description = dgvMCVariables.Rows(SelRow).Cells(2).Value
                            DistribChartFormList(I).Description = dgvMCVariables.Rows(SelRow).Cells(3).Value
                            'DistribChartFormList(I).DistributionName = dgvMCVariables.Rows(SelRow).Cells(3).Value 'Update the Data Set Type (or distribution name).
                            DistribChartFormList(I).DistributionName = dgvMCVariables.Rows(SelRow).Cells(4).Value 'Update the Data Set Type (or distribution name).
                            DistribChartFormList(I).ParamA = MonteCarlo.DataInfo(SelRow).ParameterAValue
                            DistribChartFormList(I).ParamB = MonteCarlo.DataInfo(SelRow).ParameterBValue
                            DistribChartFormList(I).ParamC = MonteCarlo.DataInfo(SelRow).ParameterCValue
                            DistribChartFormList(I).ParamD = MonteCarlo.DataInfo(SelRow).ParameterDValue
                            DistribChartFormList(I).ParamE = MonteCarlo.DataInfo(SelRow).ParameterEValue
                            DistribChartFormList(I).XMin = MonteCarlo.DataInfo(SelRow).XMin
                            DistribChartFormList(I).AutoXMin = MonteCarlo.DataInfo(SelRow).AutoXMin
                            DistribChartFormList(I).XMax = MonteCarlo.DataInfo(SelRow).XMax
                            DistribChartFormList(I).AutoXMax = MonteCarlo.DataInfo(SelRow).AutoXMax
                            DistribChartFormList(I).YMax = MonteCarlo.DataInfo(SelRow).YMax
                            DistribChartFormList(I).AutoYMax = MonteCarlo.DataInfo(SelRow).AutoYMax
                            DistribChartFormList(I).GridInterval = MonteCarlo.DataInfo(SelRow).XGridInterval
                            DistribChartFormList(I).RestoreFormSettings
                            DistribChartFormList(I).Top = MonteCarlo.DataInfo(SelRow).Top
                            DistribChartFormList(I).Left = MonteCarlo.DataInfo(SelRow).Left
                            DistribChartFormList(I).Height = MonteCarlo.DataInfo(SelRow).Height
                            DistribChartFormList(I).Width = MonteCarlo.DataInfo(SelRow).Width
                            DistribChartFormList(I).NPoints = MonteCarlo.DataInfo(SelRow).NDisplayPoints
                            DistribChartFormList(I).ShowPDF = MonteCarlo.DataInfo(SelRow).ShowPDF
                            DistribChartFormList(I).PDFLineColor = MonteCarlo.DataInfo(SelRow).PdfLineColor
                            DistribChartFormList(I).PDFLineThickness = MonteCarlo.DataInfo(SelRow).PdfLineThickness
                            DistribChartFormList(I).ShowCDF = MonteCarlo.DataInfo(SelRow).ShowCDF
                            DistribChartFormList(I).CDFLineColor = MonteCarlo.DataInfo(SelRow).CDFLineColor
                            DistribChartFormList(I).CDFLineThickness = MonteCarlo.DataInfo(SelRow).CDFLineThickness
                            DistribChartFormList(I).ShowRevCDF = MonteCarlo.DataInfo(SelRow).ShowRevCDF
                            DistribChartFormList(I).RevCDFLineColor = MonteCarlo.DataInfo(SelRow).RevCDFLineColor
                            DistribChartFormList(I).RevCDFLineThickness = MonteCarlo.DataInfo(SelRow).RevCDFLineThickness
                            DistribChartFormList(I).ShowLegend = MonteCarlo.DataInfo(SelRow).ShowLegend
                            'DistribChartFormList(I).GenerateDistribution
                            DistribChartFormList(I).ShowCharts
                            'DistribChartFormList(I).ShowPdfInterval
                            DistribChartFormList(I).CheckFormPos()
                            Exit For
                        End If
                    End If
                Next
            End If

            If ChartFound = False Then
                Dim FormNo As Integer = NewDistribChartDisplay()
                DistribChartFormList(FormNo).IndexNo = SelRow
                DistribChartFormList(FormNo).VariableName = MonteCarlo.DataInfo(SelRow).Name
                'DistribChartFormList(FormNo).RestoreFormSettings
                DistribChartFormList(FormNo).TableName = MonteCarlo.DataInfo(SelRow).Table
                DistribChartFormList(FormNo).Units = dgvMCVariables.Rows(SelRow).Cells(1).Value
                DistribChartFormList(FormNo).UnitsAbbrev = dgvMCVariables.Rows(SelRow).Cells(2).Value
                'DistribChartFormList(FormNo).Description = dgvMCVariables.Rows(SelRow).Cells(2).Value
                DistribChartFormList(FormNo).Description = dgvMCVariables.Rows(SelRow).Cells(3).Value
                'DistribChartFormList(FormNo).DistributionName = dgvMCVariables.Rows(SelRow).Cells(3).Value
                DistribChartFormList(FormNo).DistributionName = dgvMCVariables.Rows(SelRow).Cells(4).Value
                DistribChartFormList(FormNo).ParamA = MonteCarlo.DataInfo(SelRow).ParameterAValue
                DistribChartFormList(FormNo).ParamB = MonteCarlo.DataInfo(SelRow).ParameterBValue
                DistribChartFormList(FormNo).ParamC = MonteCarlo.DataInfo(SelRow).ParameterCValue
                DistribChartFormList(FormNo).ParamD = MonteCarlo.DataInfo(SelRow).ParameterDValue
                DistribChartFormList(FormNo).ParamE = MonteCarlo.DataInfo(SelRow).ParameterEValue
                DistribChartFormList(FormNo).XMin = MonteCarlo.DataInfo(SelRow).XMin
                DistribChartFormList(FormNo).AutoXMin = MonteCarlo.DataInfo(SelRow).AutoXMin
                DistribChartFormList(FormNo).XMax = MonteCarlo.DataInfo(SelRow).XMax
                DistribChartFormList(FormNo).AutoXMax = MonteCarlo.DataInfo(SelRow).AutoXMax
                DistribChartFormList(FormNo).YMax = MonteCarlo.DataInfo(SelRow).YMax
                DistribChartFormList(FormNo).AutoYMax = MonteCarlo.DataInfo(SelRow).AutoYMax
                DistribChartFormList(FormNo).GridInterval = MonteCarlo.DataInfo(SelRow).XGridInterval
                DistribChartFormList(FormNo).RestoreFormSettings
                DistribChartFormList(FormNo).Top = MonteCarlo.DataInfo(SelRow).Top
                DistribChartFormList(FormNo).Left = MonteCarlo.DataInfo(SelRow).Left
                DistribChartFormList(FormNo).Height = MonteCarlo.DataInfo(SelRow).Height
                DistribChartFormList(FormNo).Width = MonteCarlo.DataInfo(SelRow).Width
                DistribChartFormList(FormNo).NPoints = MonteCarlo.DataInfo(SelRow).NDisplayPoints
                'DistribChartFormList(FormNo).PDFLineColor = MonteCarlo.DataInfo(SelRow).PdfLineColor
                'DistribChartFormList(FormNo).PDFLineThickness = MonteCarlo.DataInfo(SelRow).PdfLineThickness
                DistribChartFormList(FormNo).ShowPDF = MonteCarlo.DataInfo(SelRow).ShowPDF
                DistribChartFormList(FormNo).PDFLineColor = MonteCarlo.DataInfo(SelRow).PdfLineColor
                DistribChartFormList(FormNo).PDFLineThickness = MonteCarlo.DataInfo(SelRow).PdfLineThickness
                DistribChartFormList(FormNo).ShowCDF = MonteCarlo.DataInfo(SelRow).ShowCDF
                DistribChartFormList(FormNo).CDFLineColor = MonteCarlo.DataInfo(SelRow).CDFLineColor
                DistribChartFormList(FormNo).CDFLineThickness = MonteCarlo.DataInfo(SelRow).CDFLineThickness
                DistribChartFormList(FormNo).ShowRevCDF = MonteCarlo.DataInfo(SelRow).ShowRevCDF
                DistribChartFormList(FormNo).RevCDFLineColor = MonteCarlo.DataInfo(SelRow).RevCDFLineColor
                DistribChartFormList(FormNo).RevCDFLineThickness = MonteCarlo.DataInfo(SelRow).RevCDFLineThickness
                DistribChartFormList(FormNo).ShowLegend = MonteCarlo.DataInfo(SelRow).ShowLegend
                'DistribChartFormList(FormNo).GenerateDistribution
                DistribChartFormList(FormNo).ShowCharts
                'DistribChartFormList(FormNo).ShowPdfInterval
                DistribChartFormList(FormNo).CheckFormPos()
            End If
        End If
    End Sub

    Private Sub cmbRVAlignment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRVAlignment.SelectedIndexChanged
        Dim ItemNo As Integer
        If dgvMCVariables.SelectedCells().Count = 0 Then
            If txtXGridInt.Focused Then Message.AddWarning("Please select a data item." & vbCrLf)
        Else
            ItemNo = dgvMCVariables.SelectedCells(0).RowIndex
            MonteCarlo.DataInfo(ItemNo).Alignment = cmbRVAlignment.SelectedItem.ToString
        End If
    End Sub

    Private Sub txtRVFormat_TextChanged(sender As Object, e As EventArgs) Handles txtRVFormat.TextChanged

    End Sub

    Private Sub txtRVFormat_LostFocus(sender As Object, e As EventArgs) Handles txtRVFormat.LostFocus
        Dim ItemNo As Integer
        If dgvMCVariables.SelectedCells().Count = 0 Then
            If txtXGridInt.Focused Then Message.AddWarning("Please select a data item." & vbCrLf)
        Else
            ItemNo = dgvMCVariables.SelectedCells(0).RowIndex
            MonteCarlo.DataInfo(ItemNo).Format = txtRVFormat.Text
        End If
    End Sub

    Private Sub btnFormatHelp_Click(sender As Object, e As EventArgs) Handles btnFormatHelp.Click
        'Show Format inforamtion.
        MessageBox.Show("Format string examples:" & vbCrLf & "N4 - Number displayed with thousands separator and 4 decimal places" & vbCrLf & "F4 - Number displayed with 4 decimal places.", "Number Formatting")
    End Sub

    Private Sub dgvMCVariables_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMCVariables.CellContentClick

    End Sub

    Private Sub dgvMCVariables_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMCVariables.CellClick

        Dim SelRow As Integer = e.RowIndex
        MonteCarlo.SelVarIndex = SelRow
        'Update the displayed plot settings:
        ShowRVChartSettings(SelRow)
        ShowRVTableSettings(SelRow)

        'If e.ColumnIndex = 18 Then 'Plot button pressed.
        If e.ColumnIndex = 19 Then 'Plot button pressed.
            If MonteCarlo.SelVarIndex = -1 Then
                Message.AddWarning("A Random variable has not been selected." & vbCrLf)
            ElseIf MonteCarlo.DataInfo(SelRow).DataSetType = "Data Table" Then
                'Do nothing - a Data Table entry can not be plotted.
            ElseIf MonteCarlo.DataInfo(SelRow).DataSetType = "Trial Number" Then
                'Do nothing - a Trial Number entry can not be plotted.
            Else
                Message.Add("Plotting random variable " & MonteCarlo.DataInfo(SelRow).Name & " in table " & MonteCarlo.DataInfo(SelRow).Table & vbCrLf)
                'First check if the Random Variable chart is already open:
                Dim ChartFound As Boolean = False
                If DistribChartFormList.Count = 0 Then

                Else
                    Dim I As Integer
                    For I = 0 To DistribChartFormList.Count - 1
                        If DistribChartFormList(I) Is Nothing Then
                            'Empty item in the list - ignore.
                        Else
                            If DistribChartFormList(I).IndexNo = MonteCarlo.SelVarIndex Then
                                ChartFound = True
                                DistribChartFormList(I).BringToFront 'Bring form to the front.
                                DistribChartFormList(I).IndexNo = SelRow
                                DistribChartFormList(I).VariableName = MonteCarlo.DataInfo(SelRow).Name 'Update the random variable name.
                                'DistribChartFormList(I).RestoreFormSettings
                                DistribChartFormList(I).TableName = MonteCarlo.DataInfo(SelRow).Table
                                DistribChartFormList(I).Units = dgvMCVariables.Rows(SelRow).Cells(1).Value
                                DistribChartFormList(I).UnitsAbbrev = dgvMCVariables.Rows(SelRow).Cells(2).Value
                                'DistribChartFormList(I).Description = dgvMCVariables.Rows(SelRow).Cells(2).Value
                                DistribChartFormList(I).Description = dgvMCVariables.Rows(SelRow).Cells(3).Value
                                'DistribChartFormList(I).DistributionName = dgvMCVariables.Rows(SelRow).Cells(3).Value 'Update the Data Set Type (or distribution name).
                                DistribChartFormList(I).DistributionName = dgvMCVariables.Rows(SelRow).Cells(4).Value 'Update the Data Set Type (or distribution name).
                                DistribChartFormList(I).ParamA = MonteCarlo.DataInfo(SelRow).ParameterAValue
                                DistribChartFormList(I).ParamB = MonteCarlo.DataInfo(SelRow).ParameterBValue
                                DistribChartFormList(I).ParamC = MonteCarlo.DataInfo(SelRow).ParameterCValue
                                DistribChartFormList(I).ParamD = MonteCarlo.DataInfo(SelRow).ParameterDValue
                                DistribChartFormList(I).ParamE = MonteCarlo.DataInfo(SelRow).ParameterEValue
                                DistribChartFormList(I).XMin = MonteCarlo.DataInfo(SelRow).XMin
                                DistribChartFormList(I).AutoXMin = MonteCarlo.DataInfo(SelRow).AutoXMin
                                DistribChartFormList(I).XMax = MonteCarlo.DataInfo(SelRow).XMax
                                DistribChartFormList(I).AutoXMax = MonteCarlo.DataInfo(SelRow).AutoXMax
                                DistribChartFormList(I).YMax = MonteCarlo.DataInfo(SelRow).YMax
                                DistribChartFormList(I).AutoYMax = MonteCarlo.DataInfo(SelRow).AutoYMax
                                DistribChartFormList(I).GridInterval = MonteCarlo.DataInfo(SelRow).XGridInterval
                                DistribChartFormList(I).RestoreFormSettings
                                DistribChartFormList(I).Top = MonteCarlo.DataInfo(SelRow).Top
                                DistribChartFormList(I).Left = MonteCarlo.DataInfo(SelRow).Left
                                DistribChartFormList(I).Height = MonteCarlo.DataInfo(SelRow).Height
                                DistribChartFormList(I).Width = MonteCarlo.DataInfo(SelRow).Width
                                DistribChartFormList(I).NPoints = MonteCarlo.DataInfo(SelRow).NDisplayPoints
                                'DistribChartFormList(I).PDFLineColor = MonteCarlo.DataInfo(SelRow).PdfLineColor
                                'DistribChartFormList(I).PDFLineThickness = MonteCarlo.DataInfo(SelRow).PdfLineThickness
                                DistribChartFormList(I).ShowPDF = MonteCarlo.DataInfo(SelRow).ShowPDF
                                DistribChartFormList(I).PDFLineColor = MonteCarlo.DataInfo(SelRow).PdfLineColor
                                DistribChartFormList(I).PDFLineThickness = MonteCarlo.DataInfo(SelRow).PdfLineThickness
                                DistribChartFormList(I).ShowCDF = MonteCarlo.DataInfo(SelRow).ShowCDF
                                DistribChartFormList(I).CDFLineColor = MonteCarlo.DataInfo(SelRow).CDFLineColor
                                DistribChartFormList(I).CDFLineThickness = MonteCarlo.DataInfo(SelRow).CDFLineThickness
                                DistribChartFormList(I).ShowRevCDF = MonteCarlo.DataInfo(SelRow).ShowRevCDF
                                DistribChartFormList(I).RevCDFLineColor = MonteCarlo.DataInfo(SelRow).RevCDFLineColor
                                DistribChartFormList(I).RevCDFLineThickness = MonteCarlo.DataInfo(SelRow).RevCDFLineThickness
                                DistribChartFormList(I).ShowLegend = MonteCarlo.DataInfo(SelRow).ShowLegend
                                'DistribChartFormList(I).GenerateDistribution
                                DistribChartFormList(I).ShowCharts
                                Exit For
                            End If
                        End If

                    Next
                End If

                If ChartFound = False Then
                    Dim FormNo As Integer = NewDistribChartDisplay()
                    DistribChartFormList(FormNo).IndexNo = SelRow
                    DistribChartFormList(FormNo).VariableName = MonteCarlo.DataInfo(SelRow).Name
                    'DistribChartFormList(FormNo).RestoreFormSettings
                    DistribChartFormList(FormNo).TableName = MonteCarlo.DataInfo(SelRow).Table
                    DistribChartFormList(FormNo).Units = dgvMCVariables.Rows(SelRow).Cells(1).Value
                    DistribChartFormList(FormNo).UnitsAbbrev = dgvMCVariables.Rows(SelRow).Cells(2).Value
                    'DistribChartFormList(FormNo).Description = dgvMCVariables.Rows(SelRow).Cells(2).Value
                    DistribChartFormList(FormNo).Description = dgvMCVariables.Rows(SelRow).Cells(3).Value
                    'DistribChartFormList(FormNo).DistributionName = dgvMCVariables.Rows(SelRow).Cells(3).Value
                    DistribChartFormList(FormNo).DistributionName = dgvMCVariables.Rows(SelRow).Cells(4).Value
                    DistribChartFormList(FormNo).ParamA = MonteCarlo.DataInfo(SelRow).ParameterAValue
                    DistribChartFormList(FormNo).ParamB = MonteCarlo.DataInfo(SelRow).ParameterBValue
                    DistribChartFormList(FormNo).ParamC = MonteCarlo.DataInfo(SelRow).ParameterCValue
                    DistribChartFormList(FormNo).ParamD = MonteCarlo.DataInfo(SelRow).ParameterDValue
                    DistribChartFormList(FormNo).ParamE = MonteCarlo.DataInfo(SelRow).ParameterEValue
                    DistribChartFormList(FormNo).XMin = MonteCarlo.DataInfo(SelRow).XMin
                    DistribChartFormList(FormNo).AutoXMin = MonteCarlo.DataInfo(SelRow).AutoXMin
                    DistribChartFormList(FormNo).XMax = MonteCarlo.DataInfo(SelRow).XMax
                    DistribChartFormList(FormNo).AutoXMax = MonteCarlo.DataInfo(SelRow).AutoXMax
                    DistribChartFormList(FormNo).YMax = MonteCarlo.DataInfo(SelRow).YMax
                    DistribChartFormList(FormNo).AutoYMax = MonteCarlo.DataInfo(SelRow).AutoYMax
                    DistribChartFormList(FormNo).GridInterval = MonteCarlo.DataInfo(SelRow).XGridInterval
                    DistribChartFormList(FormNo).RestoreFormSettings
                    DistribChartFormList(FormNo).Top = MonteCarlo.DataInfo(SelRow).Top
                    DistribChartFormList(FormNo).Left = MonteCarlo.DataInfo(SelRow).Left
                    DistribChartFormList(FormNo).Height = MonteCarlo.DataInfo(SelRow).Height
                    DistribChartFormList(FormNo).Width = MonteCarlo.DataInfo(SelRow).Width
                    DistribChartFormList(FormNo).NPoints = MonteCarlo.DataInfo(SelRow).NDisplayPoints
                    'DistribChartFormList(FormNo).PDFLineColor = MonteCarlo.DataInfo(SelRow).PdfLineColor
                    'DistribChartFormList(FormNo).PDFLineThickness = MonteCarlo.DataInfo(SelRow).PdfLineThickness
                    DistribChartFormList(FormNo).ShowPDF = MonteCarlo.DataInfo(SelRow).ShowPDF
                    DistribChartFormList(FormNo).PDFLineColor = MonteCarlo.DataInfo(SelRow).PdfLineColor
                    DistribChartFormList(FormNo).PDFLineThickness = MonteCarlo.DataInfo(SelRow).PdfLineThickness
                    DistribChartFormList(FormNo).ShowCDF = MonteCarlo.DataInfo(SelRow).ShowCDF
                    DistribChartFormList(FormNo).CDFLineColor = MonteCarlo.DataInfo(SelRow).CDFLineColor
                    DistribChartFormList(FormNo).CDFLineThickness = MonteCarlo.DataInfo(SelRow).CDFLineThickness
                    DistribChartFormList(FormNo).ShowRevCDF = MonteCarlo.DataInfo(SelRow).ShowRevCDF
                    DistribChartFormList(FormNo).RevCDFLineColor = MonteCarlo.DataInfo(SelRow).RevCDFLineColor
                    DistribChartFormList(FormNo).RevCDFLineThickness = MonteCarlo.DataInfo(SelRow).RevCDFLineThickness
                    DistribChartFormList(FormNo).ShowLegend = MonteCarlo.DataInfo(SelRow).ShowLegend
                    'DistribChartFormList(FormNo).GenerateDistribution
                    DistribChartFormList(FormNo).ShowCharts
                End If
            End If
        End If
    End Sub

    Public Sub ShowRVTableSettings(ByVal ItemNo As Integer)
        'Show the Random Variable table settings for ItemNo.
        If ItemNo = -1 Then Exit Sub
        If ItemNo < MonteCarlo.DataInfo.Count Then
            txtRVColNo.Text = MonteCarlo.DataInfo(ItemNo).ColumnNo
            txtRVFormat.Text = MonteCarlo.DataInfo(ItemNo).Format
            If MonteCarlo.DataInfo(ItemNo).Alignment = "" Then
                cmbRVAlignment.SelectedIndex = cmbRVAlignment.FindStringExact("NotSet")
            Else
                cmbRVAlignment.SelectedIndex = cmbRVAlignment.FindStringExact(MonteCarlo.DataInfo(ItemNo).Alignment)
            End If
        Else
            Message.AddWarning("Table display settings not found for Data Item Number: " & ItemNo & vbCrLf)
        End If

    End Sub

    Private Sub dgvMCVariables_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMCVariables.CellEndEdit
        'A Monte Carlo Variables cell has been edited.
        Dim RowNo As Integer = e.RowIndex
        Dim ColNo As Integer = e.ColumnIndex

        If MonteCarlo.SelVarIndex = RowNo Then '(Note: MonteCarlo.SelVarIndex is set to RowNo when a cell is clicked, so this If statement should always be true.)
            Try
                Select Case ColNo

                    Case 0 'Name
                        MonteCarlo.DataInfo(RowNo).Name = dgvMCVariables.Rows(RowNo).Cells(0).Value 'Update the Name

                    Case 1 'Units
                        MonteCarlo.DataInfo(RowNo).Units = dgvMCVariables.Rows(RowNo).Cells(1).Value 'Update the Units

                    Case 2 'Units Abbrev
                        MonteCarlo.DataInfo(RowNo).UnitsAbbrev = dgvMCVariables.Rows(RowNo).Cells(2).Value 'Update the Units Abbreviation

                        'Case 2 'Description
                    Case 3 'Description
                        'MonteCarlo.DataInfo(RowNo).Description = dgvMCVariables.Rows(RowNo).Cells(2).Value 'Update the Description
                        MonteCarlo.DataInfo(RowNo).Description = dgvMCVariables.Rows(RowNo).Cells(3).Value 'Update the Description

                        'Case 3 'Data Set Type
                    Case 4 'Data Set Type
                        'MonteCarlo.DataInfo(RowNo).DataSetType = dgvMCVariables.Rows(RowNo).Cells(3).Value 'Update the Data Set Type
                        'MonteCarlo.DataInfo(RowNo).ParameterAName = dgvMCVariables.Rows(RowNo).Cells(7).Value 'Update Parameter A Name
                        'MonteCarlo.DataInfo(RowNo).ParameterBName = dgvMCVariables.Rows(RowNo).Cells(9).Value 'Update Parameter B Name
                        'MonteCarlo.DataInfo(RowNo).ParameterCName = dgvMCVariables.Rows(RowNo).Cells(11).Value 'Update Parameter C Name
                        'MonteCarlo.DataInfo(RowNo).ParameterDName = dgvMCVariables.Rows(RowNo).Cells(13).Value 'Update Parameter D Name
                        'MonteCarlo.DataInfo(RowNo).ParameterEName = dgvMCVariables.Rows(RowNo).Cells(15).Value 'Update Parameter E Name
                        MonteCarlo.DataInfo(RowNo).DataSetType = dgvMCVariables.Rows(RowNo).Cells(4).Value 'Update the Data Set Type
                        MonteCarlo.DataInfo(RowNo).ParameterAName = dgvMCVariables.Rows(RowNo).Cells(8).Value 'Update Parameter A Name
                        MonteCarlo.DataInfo(RowNo).ParameterBName = dgvMCVariables.Rows(RowNo).Cells(10).Value 'Update Parameter B Name
                        MonteCarlo.DataInfo(RowNo).ParameterCName = dgvMCVariables.Rows(RowNo).Cells(12).Value 'Update Parameter C Name
                        MonteCarlo.DataInfo(RowNo).ParameterDName = dgvMCVariables.Rows(RowNo).Cells(14).Value 'Update Parameter D Name
                        MonteCarlo.DataInfo(RowNo).ParameterEName = dgvMCVariables.Rows(RowNo).Cells(16).Value 'Update Parameter E Name

                        'Case 4 'Data Value Type
                    Case 5 'Data Value Type
                        'MonteCarlo.DataInfo(RowNo).DataType = dgvMCVariables.Rows(RowNo).Cells(4).Value 'Update the Data Value Type
                        MonteCarlo.DataInfo(RowNo).DataType = dgvMCVariables.Rows(RowNo).Cells(5).Value 'Update the Data Value Type

                        'Case 5 'Sampling
                    Case 6 'Sampling
                        'MonteCarlo.DataInfo(RowNo).Sampling = dgvMCVariables.Rows(RowNo).Cells(5).Value 'Update the Sampling type
                        MonteCarlo.DataInfo(RowNo).Sampling = dgvMCVariables.Rows(RowNo).Cells(6).Value 'Update the Sampling type

                        'Case 6
                    Case 7
                        'MonteCarlo.DataInfo(RowNo).Table = dgvMCVariables.Rows(RowNo).Cells(6).Value 'Update the Destination Table
                        MonteCarlo.DataInfo(RowNo).Table = dgvMCVariables.Rows(RowNo).Cells(7).Value 'Update the Destination Table

                        'Case 8 'Parameter A Value
                    Case 9 'Parameter A Value
                        'If dgvMCVariables.Rows(RowNo).Cells(8).Value = "" Then
                        '    MonteCarlo.DataInfo(RowNo).ParameterAValue = Double.NaN
                        'ElseIf dgvMCVariables.Rows(RowNo).Cells(8).Value = "NaN" Then
                        '    MonteCarlo.DataInfo(RowNo).ParameterAValue = Double.NaN
                        'Else
                        '    MonteCarlo.DataInfo(RowNo).ParameterAValue = dgvMCVariables.Rows(RowNo).Cells(8).Value 'Update the Parameter A Value
                        'End If
                        If dgvMCVariables.Rows(RowNo).Cells(9).Value = "" Then
                            MonteCarlo.DataInfo(RowNo).ParameterAValue = Double.NaN
                        ElseIf dgvMCVariables.Rows(RowNo).Cells(9).Value = "NaN" Then
                            MonteCarlo.DataInfo(RowNo).ParameterAValue = Double.NaN
                        Else
                            MonteCarlo.DataInfo(RowNo).ParameterAValue = dgvMCVariables.Rows(RowNo).Cells(9).Value 'Update the Parameter A Value
                        End If

                    'Case 10 'Parameter B Value
                    '    If dgvMCVariables.Rows(RowNo).Cells(10).Value = "" Then
                    '        MonteCarlo.DataInfo(RowNo).ParameterBValue = Double.NaN
                    '    ElseIf dgvMCVariables.Rows(RowNo).Cells(10).Value = "NaN" Then
                    '        MonteCarlo.DataInfo(RowNo).ParameterBValue = Double.NaN
                    '    Else
                    '        MonteCarlo.DataInfo(RowNo).ParameterBValue = dgvMCVariables.Rows(RowNo).Cells(10).Value 'Update the Parameter B Value
                    '    End If
                    Case 11 'Parameter B Value
                        If dgvMCVariables.Rows(RowNo).Cells(11).Value = "" Then
                            MonteCarlo.DataInfo(RowNo).ParameterBValue = Double.NaN
                        ElseIf dgvMCVariables.Rows(RowNo).Cells(11).Value = "NaN" Then
                            MonteCarlo.DataInfo(RowNo).ParameterBValue = Double.NaN
                        Else
                            MonteCarlo.DataInfo(RowNo).ParameterBValue = dgvMCVariables.Rows(RowNo).Cells(11).Value 'Update the Parameter B Value
                        End If

                    'Case 12 'Parameter C Value
                    '    If dgvMCVariables.Rows(RowNo).Cells(12).Value = "" Then
                    '        MonteCarlo.DataInfo(RowNo).ParameterCValue = Double.NaN
                    '    ElseIf dgvMCVariables.Rows(RowNo).Cells(12).Value = "NaN" Then
                    '        MonteCarlo.DataInfo(RowNo).ParameterCValue = Double.NaN
                    '    Else
                    '        MonteCarlo.DataInfo(RowNo).ParameterCValue = dgvMCVariables.Rows(RowNo).Cells(12).Value 'Update the Parameter C Value
                    '    End If
                    Case 13 'Parameter C Value
                        If dgvMCVariables.Rows(RowNo).Cells(13).Value = "" Then
                            MonteCarlo.DataInfo(RowNo).ParameterCValue = Double.NaN
                        ElseIf dgvMCVariables.Rows(RowNo).Cells(13).Value = "NaN" Then
                            MonteCarlo.DataInfo(RowNo).ParameterCValue = Double.NaN
                        Else
                            MonteCarlo.DataInfo(RowNo).ParameterCValue = dgvMCVariables.Rows(RowNo).Cells(13).Value 'Update the Parameter C Value
                        End If

                    'Case 14 'Parameter D Value
                    '    If dgvMCVariables.Rows(RowNo).Cells(14).Value = "" Then
                    '        MonteCarlo.DataInfo(RowNo).ParameterDValue = Double.NaN
                    '    ElseIf dgvMCVariables.Rows(RowNo).Cells(14).Value = "NaN" Then
                    '        MonteCarlo.DataInfo(RowNo).ParameterDValue = Double.NaN
                    '    Else
                    '        MonteCarlo.DataInfo(RowNo).ParameterDValue = dgvMCVariables.Rows(RowNo).Cells(14).Value 'Update the Parameter D Value
                    '    End If
                    Case 15 'Parameter D Value
                        If dgvMCVariables.Rows(RowNo).Cells(15).Value = "" Then
                            MonteCarlo.DataInfo(RowNo).ParameterDValue = Double.NaN
                        ElseIf dgvMCVariables.Rows(RowNo).Cells(15).Value = "NaN" Then
                            MonteCarlo.DataInfo(RowNo).ParameterDValue = Double.NaN
                        Else
                            MonteCarlo.DataInfo(RowNo).ParameterDValue = dgvMCVariables.Rows(RowNo).Cells(15).Value 'Update the Parameter D Value
                        End If

                    'Case 16 'Parameter E Value
                    '    If dgvMCVariables.Rows(RowNo).Cells(16).Value = "" Then
                    '        MonteCarlo.DataInfo(RowNo).ParameterEValue = Double.NaN
                    '    ElseIf dgvMCVariables.Rows(RowNo).Cells(16).Value = "NaN" Then
                    '        MonteCarlo.DataInfo(RowNo).ParameterEValue = Double.NaN
                    '    Else
                    '        MonteCarlo.DataInfo(RowNo).ParameterEValue = dgvMCVariables.Rows(RowNo).Cells(16).Value 'Update the Parameter E Value
                    '    End If
                    Case 17 'Parameter E Value
                        If dgvMCVariables.Rows(RowNo).Cells(17).Value = "" Then
                            MonteCarlo.DataInfo(RowNo).ParameterEValue = Double.NaN
                        ElseIf dgvMCVariables.Rows(RowNo).Cells(17).Value = "NaN" Then
                            MonteCarlo.DataInfo(RowNo).ParameterEValue = Double.NaN
                        Else
                            MonteCarlo.DataInfo(RowNo).ParameterEValue = dgvMCVariables.Rows(RowNo).Cells(17).Value 'Update the Parameter E Value
                        End If

                        'Case 17 'Seed value
                        '    If dgvMCVariables.Rows(RowNo).Cells(17).Value = "" Then 'The seed value is not used. Set it to Double.NaN
                        '        MonteCarlo.DataInfo(RowNo).Seed = -1 'Update the Seed Value
                        '    ElseIf dgvMCVariables.Rows(RowNo).Cells(17).Value = "NaN" Then
                        '        MonteCarlo.DataInfo(RowNo).Seed = -1 'Update the Seed Value
                        '    Else
                        '        Dim SeedVal As Integer = CInt(dgvMCVariables.Rows(RowNo).Cells(17).Value)
                        '        If SeedVal < 0 Then
                        '            Message.AddWarning("The seed value should be a positive integer!" & vbCrLf)
                        '            SeedVal = -SeedVal
                        '        End If
                        '        dgvMCVariables.Rows(RowNo).Cells(17).Value = SeedVal
                        '        Dim CurrentName As String = MonteCarlo.DataInfo(RowNo).Name
                        '        MonteCarlo.DataInfo(RowNo).Seed = SeedVal 'Update the Seed Value
                        '        For Each item In MonteCarlo.DataInfo
                        '            If item.Seed = SeedVal Then 'Possible duplicate seed
                        '                If item.Name = CurrentName Then

                        '                Else
                        '                    Beep()
                        '                    Message.AddWarning("This seed value is the same as the random variable named: " & item.Name & vbCrLf)
                        '                    Message.AddWarning("Using the same seed value will result in correlated variables!" & vbCrLf)
                        '                End If
                        '            End If
                        '        Next
                        '    End If
                    Case 18 'Seed value
                        If dgvMCVariables.Rows(RowNo).Cells(18).Value = "" Then 'The seed value is not used. Set it to Double.NaN
                            MonteCarlo.DataInfo(RowNo).Seed = -1 'Update the Seed Value
                        ElseIf dgvMCVariables.Rows(RowNo).Cells(18).Value = "NaN" Then
                            MonteCarlo.DataInfo(RowNo).Seed = -1 'Update the Seed Value
                        Else
                            Dim SeedVal As Integer = CInt(dgvMCVariables.Rows(RowNo).Cells(18).Value)
                            If SeedVal < 0 Then
                                Message.AddWarning("The seed value should be a positive integer!" & vbCrLf)
                                SeedVal = -SeedVal
                            End If
                            dgvMCVariables.Rows(RowNo).Cells(18).Value = SeedVal
                            Dim CurrentName As String = MonteCarlo.DataInfo(RowNo).Name
                            MonteCarlo.DataInfo(RowNo).Seed = SeedVal 'Update the Seed Value
                            For Each item In MonteCarlo.DataInfo
                                If item.Seed = SeedVal Then 'Possible duplicate seed
                                    If item.Name = CurrentName Then

                                    Else
                                        Beep()
                                        Message.AddWarning("This seed value is the same as the random variable named: " & item.Name & vbCrLf)
                                        Message.AddWarning("Using the same seed value will result in correlated variables!" & vbCrLf)
                                    End If
                                End If
                            Next
                        End If

                    Case Else
                        Message.AddWarning("Invalid column number: " & ColNo & vbCrLf)
                End Select

                dgvMCVariables.AutoResizeColumns()
                dgvMCVariables.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader 'TOO NARROW
                dgvMCVariables.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            Catch ex As Exception
                Message.AddWarning(ex.Message & vbCrLf)
            End Try
        Else
            Message.AddWarning("The Row Number selected in the grid (" & RowNo & ") does not match the selected Index (" & MonteCarlo.SelVarIndex & ")" & vbCrLf)
        End If
    End Sub

    Private Sub dgvMCVariables_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvMCVariables.DataError
        'Message.AddWarning("Monte Carlo variables grid: " & vbCrLf & e.Exception.Message & vbCrLf)
    End Sub

    Private Sub dgvMCVariables_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgvMCVariables.EditingControlShowing
        'If dgvMCVariables.CurrentCell.ColumnIndex = 3 Then 'Data Set Type selected
        If dgvMCVariables.CurrentCell.ColumnIndex = 4 Then 'Data Set Type selected
            Dim combo As ComboBox = CType(e.Control, ComboBox)
            If (combo IsNot Nothing) Then
                combo.Name = "cboDataSetType"
                'Remove current handler:
                RemoveHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf ComboBox_SelectionChangeCommitted)
                'Add the event handler:
                AddHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf ComboBox_SelectionChangeCommitted)
            End If

            'ElseIf dgvMCVariables.CurrentCell.ColumnIndex = 4 Then 'Data Value Type selected
        ElseIf dgvMCVariables.CurrentCell.ColumnIndex = 5 Then 'Data Value Type selected
            Dim combo As ComboBox = CType(e.Control, ComboBox)
            If (combo IsNot Nothing) Then
                combo.Name = "cboDataValueType"
                'Remove current handler:
                RemoveHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf ComboBox_SelectionChangeCommitted)
                'Add the event handler:
                AddHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf ComboBox_SelectionChangeCommitted)
            End If

            'ElseIf dgvMCVariables.CurrentCell.ColumnIndex = 5 Then 'Sampling selected
        ElseIf dgvMCVariables.CurrentCell.ColumnIndex = 6 Then 'Sampling selected
            Dim combo As ComboBox = CType(e.Control, ComboBox)
            If (combo IsNot Nothing) Then
                combo.Name = "cboDistSampling"
                'Remove current handler:
                RemoveHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf ComboBox_SelectionChangeCommitted)
                'Add the event handler:
                AddHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf ComboBox_SelectionChangeCommitted)
            End If

            'ElseIf dgvMCVariables.CurrentCell.ColumnIndex = 6 Then 'Destination Table selected
        ElseIf dgvMCVariables.CurrentCell.ColumnIndex = 7 Then 'Destination Table selected
            Dim combo As ComboBox = CType(e.Control, ComboBox)
            If (combo IsNot Nothing) Then
                combo.Name = "cboDestTable"
                'Remove current handler:
                RemoveHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf ComboBox_SelectionChangeCommitted)
                'Add the event handler:
                AddHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf ComboBox_SelectionChangeCommitted)
            End If
        End If
    End Sub

    Private Sub ComboBox_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim combo As ComboBox = CType(sender, ComboBox)

        If combo.Name = "cboDataSetType" Then
            Message.Add("Selected distribution: " & combo.SelectedItem.ToString & vbCrLf)

            Dim RowNo As Integer = dgvMCVariables.SelectedCells(0).RowIndex
            Message.Add("Selected row: " & RowNo & vbCrLf)

            SetGridDistParams(combo.SelectedItem.ToString, RowNo)

        ElseIf combo.Name = "cboDataValueType" Then
            'Data Value Type selected.

        ElseIf combo.Name = "cboDistSampling" Then
            'Distribution sampling selected.

        ElseIf combo.Name = "cboDestTable" Then
            'Destination table selected.

        Else
            Message.AddWarning("Unknown combo box: " & combo.Name & vbCrLf)
        End If

    End Sub

    Private Sub txtLeft_TextChanged(sender As Object, e As EventArgs) Handles txtLeft.TextChanged

    End Sub

    Private Sub txtTop_TextChanged(sender As Object, e As EventArgs) Handles txtTop.TextChanged

    End Sub

    Private Sub txtWidth_TextChanged(sender As Object, e As EventArgs) Handles txtWidth.TextChanged

    End Sub

    Private Sub txtHeight_TextChanged(sender As Object, e As EventArgs) Handles txtHeight.TextChanged

    End Sub

    Private Sub txtLeft_LostFocus(sender As Object, e As EventArgs) Handles txtLeft.LostFocus
        'Update the left of the distribution chart:
        If dgvMCVariables.SelectedCells.Count = 0 Then
            'A random variable has not been selected
        Else
            Dim SelRow As Integer = dgvMCVariables.SelectedCells(0).RowIndex
            Dim ChartFound As Boolean = False
            If DistribChartFormList.Count = 0 Then
                'There are no distribution charts open
            Else
                Dim I As Integer
                For I = 0 To DistribChartFormList.Count - 1
                    If DistribChartFormList(I) Is Nothing Then
                        'Empty item in the list - ignore.
                    Else
                        If DistribChartFormList(I).IndexNo = MonteCarlo.SelVarIndex Then
                            DistribChartFormList(I).Left = txtLeft.Text
                            DistribChartFormList(I).SaveFormSettings()
                        End If
                    End If
                Next
                MonteCarlo.DataInfo(SelRow).Left = txtLeft.Text
            End If
        End If
    End Sub

    Private Sub txtTop_LostFocus(sender As Object, e As EventArgs) Handles txtTop.LostFocus
        'Update the top of the distribution chart:
        If dgvMCVariables.SelectedCells.Count = 0 Then
            'A random variable has not been selected
        Else
            Dim SelRow As Integer = dgvMCVariables.SelectedCells(0).RowIndex
            Dim ChartFound As Boolean = False
            If DistribChartFormList.Count = 0 Then
                'There are no distribution charts open
            Else
                Dim I As Integer
                For I = 0 To DistribChartFormList.Count - 1
                    If DistribChartFormList(I) Is Nothing Then
                        'Empty item in the list - ignore.
                    Else
                        If DistribChartFormList(I).IndexNo = MonteCarlo.SelVarIndex Then
                            DistribChartFormList(I).Top = txtTop.Text
                            DistribChartFormList(I).SaveFormSettings()
                        End If
                    End If
                Next
                MonteCarlo.DataInfo(SelRow).Top = txtTop.Text
            End If
        End If
    End Sub

    Private Sub txtWidth_LostFocus(sender As Object, e As EventArgs) Handles txtWidth.LostFocus
        'Update the width of the distribution chart:
        If dgvMCVariables.SelectedCells.Count = 0 Then
            'A random variable has not been selected
        Else
            Dim SelRow As Integer = dgvMCVariables.SelectedCells(0).RowIndex
            Dim ChartFound As Boolean = False
            If DistribChartFormList.Count = 0 Then
                'There are no distribution charts open
            Else
                Dim I As Integer
                For I = 0 To DistribChartFormList.Count - 1
                    If DistribChartFormList(I) Is Nothing Then
                        'Empty item in the list - ignore.
                    Else
                        If DistribChartFormList(I).IndexNo = MonteCarlo.SelVarIndex Then
                            DistribChartFormList(I).Width = txtWidth.Text
                            DistribChartFormList(I).SaveFormSettings()
                        End If
                    End If
                Next
                MonteCarlo.DataInfo(SelRow).Width = txtWidth.Text
            End If
        End If
    End Sub

    Private Sub txtHeight_LostFocus(sender As Object, e As EventArgs) Handles txtHeight.LostFocus
        'Update the height of the distribution chart:
        If dgvMCVariables.SelectedCells.Count = 0 Then
            'A random variable has not been selected
        Else
            Dim SelRow As Integer = dgvMCVariables.SelectedCells(0).RowIndex
            Dim ChartFound As Boolean = False
            If DistribChartFormList.Count = 0 Then
                'There are no distribution charts open
            Else
                Dim I As Integer
                For I = 0 To DistribChartFormList.Count - 1
                    If DistribChartFormList(I) Is Nothing Then
                        'Empty item in the list - ignore.
                    Else
                        If DistribChartFormList(I).IndexNo = MonteCarlo.SelVarIndex Then
                            DistribChartFormList(I).Height = txtHeight.Text
                            DistribChartFormList(I).SaveFormSettings()
                        End If
                    End If
                Next

                MonteCarlo.DataInfo(SelRow).Height = txtHeight.Text
            End If
        End If
    End Sub

    Private Sub txtLineColor_TextChanged(sender As Object, e As EventArgs) Handles txtPdfLineColor.TextChanged

    End Sub

    Private Sub txtLineColor_Click(sender As Object, e As EventArgs) Handles txtPdfLineColor.Click
        'Select a PDF line color:
        Dim ItemNo As Integer

        If dgvMCVariables.SelectedCells().Count = 0 Then
            If txtXGridInt.Focused Then Message.AddWarning("Please select a data item." & vbCrLf)
        Else
            ItemNo = dgvMCVariables.SelectedCells(0).RowIndex
            ColorDialog1.ShowDialog()
            txtPdfLineColor.BackColor = ColorDialog1.Color
            MonteCarlo.DataInfo(ItemNo).PDFLineColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub txtLineThickness_TextChanged(sender As Object, e As EventArgs) Handles txtPdfLineThickness.TextChanged

    End Sub

    Private Sub txtLineThickness_LostFocus(sender As Object, e As EventArgs) Handles txtPdfLineThickness.LostFocus
        Dim ItemNo As Integer

        If dgvMCVariables.SelectedCells().Count = 0 Then
            If txtXGridInt.Focused Then Message.AddWarning("Please select a data item." & vbCrLf)
        Else
            ItemNo = dgvMCVariables.SelectedCells(0).RowIndex
            MonteCarlo.DataInfo(ItemNo).PdfLineThickness = txtPdfLineThickness.Text
        End If
    End Sub

    Private Sub txtCdfLineColor_TextChanged(sender As Object, e As EventArgs) Handles txtCdfLineColor.TextChanged

    End Sub

    Private Sub txtCdfLineColor_Click(sender As Object, e As EventArgs) Handles txtCdfLineColor.Click
        'Select a CDF line color:
        Dim ItemNo As Integer

        If dgvMCVariables.SelectedCells().Count = 0 Then
            If txtXGridInt.Focused Then Message.AddWarning("Please select a data item." & vbCrLf)
        Else
            ItemNo = dgvMCVariables.SelectedCells(0).RowIndex
            ColorDialog1.ShowDialog()
            txtCdfLineColor.BackColor = ColorDialog1.Color
            MonteCarlo.DataInfo(ItemNo).CDFLineColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub txtCdfLineThickness_TextChanged(sender As Object, e As EventArgs) Handles txtCdfLineThickness.TextChanged

    End Sub

    Private Sub txtCdfLineThickness_LostFocus(sender As Object, e As EventArgs) Handles txtCdfLineThickness.LostFocus
        Dim ItemNo As Integer

        If dgvMCVariables.SelectedCells().Count = 0 Then
            If txtXGridInt.Focused Then Message.AddWarning("Please select a data item." & vbCrLf)
        Else
            ItemNo = dgvMCVariables.SelectedCells(0).RowIndex
            MonteCarlo.DataInfo(ItemNo).CDFLineThickness = txtCdfLineThickness.Text
        End If
    End Sub

    Private Sub txtRevCdfLineColor_TextChanged(sender As Object, e As EventArgs) Handles txtRevCdfLineColor.TextChanged

    End Sub

    Private Sub txtRevCdfLineColor_Click(sender As Object, e As EventArgs) Handles txtRevCdfLineColor.Click
        'Select a Rev CDF line color:
        Dim ItemNo As Integer

        If dgvMCVariables.SelectedCells().Count = 0 Then
            If txtXGridInt.Focused Then Message.AddWarning("Please select a data item." & vbCrLf)
        Else
            ItemNo = dgvMCVariables.SelectedCells(0).RowIndex
            ColorDialog1.ShowDialog()
            txtRevCdfLineColor.BackColor = ColorDialog1.Color
            MonteCarlo.DataInfo(ItemNo).RevCDFLineColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub txtRevCdfLineThickness_TextChanged(sender As Object, e As EventArgs) Handles txtRevCdfLineThickness.TextChanged

    End Sub

    Private Sub txtRevCdfLineThickness_LostFocus(sender As Object, e As EventArgs) Handles txtRevCdfLineThickness.LostFocus
        Dim ItemNo As Integer

        If dgvMCVariables.SelectedCells().Count = 0 Then
            If txtXGridInt.Focused Then Message.AddWarning("Please select a data item." & vbCrLf)
        Else
            ItemNo = dgvMCVariables.SelectedCells(0).RowIndex
            MonteCarlo.DataInfo(ItemNo).RevCDFLineThickness = txtRevCdfLineThickness.Text
        End If
    End Sub


#End Region 'Random Variables Tab -------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Correlations Tab" '==================================================================================================================================================================

    Private Sub btnNewCorr_Click(sender As Object, e As EventArgs) Handles btnNewCorr.Click
        'Create a new Correlation Matrix.

        'Get the new matrix Name and Description:
        Dim EntryForm As New ADVL_Utilities_Library_1.frmNewDataNameModal
        EntryForm.EntryName = "NewCorrMatrix"
        EntryForm.Title = "New Correlation Matrix"
        EntryForm.GetFileName = False
        EntryForm.GetDataName = True
        EntryForm.GetDataLabel = False
        EntryForm.GetDataDescription = True
        EntryForm.SettingsLocn = Project.SettingsLocn
        EntryForm.DataLocn = Project.DataLocn
        EntryForm.ApplicationName = ApplicationInfo.Name
        EntryForm.RestoreFormSettings()

        Dim CorrMatName As String
        Dim CorrMatDescr As String
        If EntryForm.ShowDialog() = DialogResult.OK Then
            CorrMatName = EntryForm.DataName
            CorrMatDescr = EntryForm.DataDescription
            If MonteCarlo.Correlations.ContainsKey(CorrMatName) Then
                Message.AddWarning("The correlation matrix name is already used: " & CorrMatName & vbCrLf)
                Exit Sub
            Else
                txtCorrelationName.Text = CorrMatName
                txtCorrelationDesc.Text = CorrMatDescr
            End If
        Else
            Exit Sub
        End If

        MonteCarlo.Correlations.Add(CorrMatName, New clsMonteCarlo.CorrInfo)
        cmbCorrelations.Items.Add(CorrMatName)
        MonteCarlo.SelCorrMatName = CorrMatName 'Store the selected Correaltion Matrix name in MonteCarlo
        MonteCarlo.Correlations(CorrMatName).Description = txtCorrelationDesc.Text
        MonteCarlo.Correlations(CorrMatName).NVariables = 2
        txtNCorrVars.Text = "2"
        SetUpCorrMat(2)
        If cmbCorrTableName.SelectedIndex = -1 Then
            MonteCarlo.Correlations(CorrMatName).TableName = ""
        Else
            MonteCarlo.Correlations(CorrMatName).TableName = cmbCorrTableName.SelectedItem.ToString
        End If

        'Find the index of the current Key (CorrMatName)
        lblCorrMatNo.Text = 0 'If the key is not found, the Correlation Matrix number display remains as 0.
        For I = 0 To MonteCarlo.Correlations.Count - 1
            If MonteCarlo.Correlations.Keys(I) = CorrMatName Then
                lblCorrMatNo.Text = I + 1
                Exit For
            End If
        Next

        lblCorrMatCount.Text = MonteCarlo.Correlations.Count
    End Sub

    Private Sub btnDeleteCorr_Click(sender As Object, e As EventArgs) Handles btnDeleteCorr.Click
        'Delete the selected Correlation Matrix.

        Dim CorrMatNo As Integer = lblCorrMatNo.Text
        Dim CorrMatName As String = txtCorrelationName.Text
        MonteCarlo.Correlations.Remove(CorrMatName)

        cmbCorrelations.Items.Clear()
        If MonteCarlo.Correlations.Count > 0 Then
            'Update the Correlations list:
            For Each item In MonteCarlo.Correlations
                cmbCorrelations.Items.Add(item.Key)
            Next
        End If

        lblCorrMatCount.Text = MonteCarlo.Correlations.Count
        If CorrMatNo > MonteCarlo.Correlations.Count Then
            CorrMatNo = MonteCarlo.Correlations.Count
            lblCorrMatNo.Text = CorrMatNo
        End If
        If CorrMatNo > 0 Then
            ShowCorrMatData(CorrMatNo - 1) 'CorrMatIndex = CorrMatNo - 1
        Else
            ClearCorrMatDataDisplay()
        End If
    End Sub

    Private Sub btnPrevCorr_Click(sender As Object, e As EventArgs) Handles btnPrevCorr.Click
        'Show the previous correlation matrix.
        Dim CorrMatNo As Integer = lblCorrMatNo.Text
        If CorrMatNo = 0 Then
            Message.AddWarning("No available correlation matrix data to show." & vbCrLf)
        ElseIf CorrMatNo = 1 Then
            Message.AddWarning("Already showing the first correlation matrix data." & vbCrLf)
        Else
            ShowCorrMatData(CorrMatNo - 2)
            TransCholeskyCorrMatrix()
        End If
    End Sub

    Private Sub btnNextCorr_Click(sender As Object, e As EventArgs) Handles btnNextCorr.Click
        'Show the next correlation matrix.
        Dim CorrMatNo As Integer = lblCorrMatNo.Text
        If CorrMatNo >= MonteCarlo.Correlations.Count Then
            Message.AddWarning("Already showing the last correlation matrix data." & vbCrLf)
        Else
            ShowCorrMatData(CorrMatNo)
            TransCholeskyCorrMatrix()
        End If

    End Sub

    Private Sub TransCholeskyCorrMatrix()
        'Generate the Transposed Cholesky Decomposition

        Dim NCorrVars As Integer = txtNCorrVars.Text

        Dim Array(NCorrVars - 1, NCorrVars - 1) As Double

        Dim I As Integer
        Dim J As Integer

        For I = 1 To NCorrVars
            For J = 1 To NCorrVars
                Array(I - 1, J - 1) = Val(dgvCorrMatrix.Rows(I).Cells(J).Value)
            Next
        Next

        Dim CorrMat = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfArray(Array)

        Try
            Dim Chol = CorrMat.Cholesky
            For I = 0 To NCorrVars - 1
                For J = 0 To NCorrVars - 1
                    dgvCholesky.Rows(J).Cells(I).Value = Chol.Factor(I, J)
                Next
            Next
            Label54.Text = "Transposed Cholesky Decomposition:"
            dgvCholesky.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            dgvCholesky.AutoResizeColumns()
        Catch ex As Exception
            Message.AddWarning("Cholesky decomposition error:" & vbCrLf & ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub cmbCorrTableName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCorrTableName.SelectedIndexChanged
        'A data table has been selected.

        If cmbCorrTableName.SelectedIndex = -1 Then
            Message.AddWarning("A table has not been selected." & vbCrLf)
        Else
            Dim TableName As String = cmbCorrTableName.SelectedItem.ToString
            If MonteCarlo.SelCorrMatName <> "" Then MonteCarlo.Correlations(MonteCarlo.SelCorrMatName).TableName = TableName
            If MonteCarlo.Data.Tables.Contains(TableName) Then
                cboCorrVariables.Items.Clear()
                For Each Col As DataColumn In MonteCarlo.Data.Tables(TableName).Columns
                    cboCorrVariables.Items.Add(Col.ColumnName)
                Next
            Else
                If cmbCorrTableName.Focused Then Message.AddWarning("The table was not found: " & TableName & vbCrLf)
            End If
        End If
    End Sub

    Private Sub txtNCorrVars_TextChanged(sender As Object, e As EventArgs) Handles txtNCorrVars.TextChanged

    End Sub

    Private Sub txtNCorrVars_LostFocus(sender As Object, e As EventArgs) Handles txtNCorrVars.LostFocus
        'The number of correlated random variables has changed.
        Dim NCorrVars As Integer = txtNCorrVars.Text
        Dim CorrMatName As String = txtCorrelationName.Text
        If MonteCarlo.Correlations(CorrMatName).NVariables = NCorrVars Then
            'The number of Correlated varibles has not changed.
        Else
            MonteCarlo.Correlations(CorrMatName).NVariables = NCorrVars
            SetUpCorrMat(NCorrVars)
        End If
    End Sub

    Private Sub btnCholesky_Click(sender As Object, e As EventArgs) Handles btnCholesky.Click
        'Generate the Cholesky Decomposition of the Correlation matrix
        CholeskyCorrMatrix()
    End Sub

    Private Sub CholeskyCorrMatrix()
        'Generate the Cholesky Decomposition of the Correlation matrix

        Dim NCorrVars As Integer = txtNCorrVars.Text

        Dim Array(NCorrVars - 1, NCorrVars - 1) As Double

        Dim I As Integer
        Dim J As Integer

        For I = 1 To NCorrVars
            For J = 1 To NCorrVars
                Array(I - 1, J - 1) = Val(dgvCorrMatrix.Rows(I).Cells(J).Value)
            Next
        Next

        Dim CorrMat = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfArray(Array)

        Try
            Dim Chol = CorrMat.Cholesky
            For I = 0 To NCorrVars - 1
                For J = 0 To NCorrVars - 1
                    dgvCholesky.Rows(I).Cells(J).Value = Chol.Factor(I, J)
                Next
            Next
            Label54.Text = "Cholesky Decomposition:"
            dgvCholesky.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            dgvCholesky.AutoResizeColumns()
        Catch ex As Exception
            Message.AddWarning("Cholesky decomposition error:" & vbCrLf & ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub btnTransCholesky_Click(sender As Object, e As EventArgs) Handles btnTransCholesky.Click
        'Generate the Transposed Cholesky Decomposition
        TransCholeskyCorrMatrix()
    End Sub

    Private Sub btnDisplayAllCorrs_Click(sender As Object, e As EventArgs) Handles btnDisplayAllCorrs.Click
        'Display all correlations

        Message.Add("List of all correlation matrices:" & vbCrLf)
        For Each item In MonteCarlo.Correlations
            Message.Add(vbCrLf & "Correlation name: " & item.Key & vbCrLf)
            Message.Add("Correlation description: " & item.Value.Description & vbCrLf)
            Message.Add("Number of variables: " & item.Value.NVariables & vbCrLf)
            Message.Add("Uncorrelated Random variable list: " & vbCrLf)
            For Each unCorrRvItem In item.Value.UnCorrRV
                Message.Add(" " & unCorrRvItem & vbCrLf)
            Next
            Message.Add("Correlated Random variable list: " & vbCrLf)
            For Each rvItem In item.Value.CorrRV
                Message.Add(" " & rvItem & vbCrLf)
            Next
            Dim I As Integer
            Dim J As Integer
            Message.Add("Correlation coefficients: " & vbCrLf)
            For I = 0 To item.Value.NVariables - 1
                For J = 0 To item.Value.NVariables - 1
                    Message.Add(" " & item.Value.Array(I, J))
                Next
                Message.Add(vbCrLf)
            Next
        Next
    End Sub

    Private Sub btnApplyCorrMat_Click(sender As Object, e As EventArgs) Handles btnApplyCorrMat.Click
        'Apply the Correlation Matrix:
        'Multiply the matrix of Input random variables by the transposed correlation Cholesky matrix.

        Dim CorrMatName As String = txtCorrelationName.Text
        If MonteCarlo.Correlations.ContainsKey(CorrMatName) Then
            MonteCarlo.ApplyCorrMatrix(CorrMatName)
            'Update the data table display:
            dgvResults.AutoGenerateColumns = True
            dgvResults.DataSource = MonteCarlo.Data.Tables("DataTable")
            dgvResults.AutoResizeColumns()
            dgvResults.Update()
            dgvResults.Refresh()
        Else
            Message.AddWarning("The correlation matrix was not found: " & CorrMatName & vbCrLf)
        End If

    End Sub

    Private Sub btnMatrixOps_Click(sender As Object, e As EventArgs) Handles btnMatrixOps.Click
        'Open a MatrixOps form.
        Dim MprtixOpsNo As Integer = OpenNewMatrixOps()
    End Sub


    Private Sub txtCorrMatrixFormat_LostFocus(sender As Object, e As EventArgs) Handles txtCorrMatrixFormat.LostFocus
        'The Correlation Matrix display format has changed.
        dgvCorrMatrix.DefaultCellStyle.Format = txtCorrMatrixFormat.Text
        Dim CorrMatName As String = txtCorrelationName.Text
        If CorrMatName = "" Then
            Message.AddWarning("Please open or create a Correlation Matrix." & vbCrLf)
        Else
            If MonteCarlo.Correlations.ContainsKey(CorrMatName) Then
                MonteCarlo.Correlations(CorrMatName).DisplayFormat = txtCorrMatrixFormat.Text
            Else
                Message.AddWarning("The Correlation Matrix " & CorrMatName & " was not found." & vbCrLf)
            End If
        End If
    End Sub

    Private Sub txtCorrCholFormat_LostFocus(sender As Object, e As EventArgs) Handles txtCorrCholFormat.LostFocus
        'The Correlation Matrix Cholesky Decomposition display format has changed.
        dgvCholesky.DefaultCellStyle.Format = txtCorrCholFormat.Text
        Dim CorrMatName As String = txtCorrelationName.Text
        If CorrMatName = "" Then
            Message.AddWarning("Please open or create a Correlation Matrix." & vbCrLf)
        Else
            If MonteCarlo.Correlations.ContainsKey(CorrMatName) Then
                MonteCarlo.Correlations(CorrMatName).CholDisplayFormat = txtCorrCholFormat.Text
            Else
                Message.AddWarning("The Correlation Matrix " & CorrMatName & " was not found." & vbCrLf)
            End If
        End If
    End Sub


    Private Sub btnFormatHelp2_Click(sender As Object, e As EventArgs) Handles btnFormatHelp2.Click
        'Show Format information.
        MessageBox.Show("Format string examples:" & vbCrLf & "N4 - Number displayed with thousands separator and 4 decimal places" & vbCrLf & "F4 - Number displayed with 4 decimal places.", "Number Formatting")
    End Sub

    Private Sub btnCopyCorrMatrix_Click(sender As Object, e As EventArgs) Handles btnCopyCorrMatrix.Click
        'Copy the Correlation matrix data to MatrixClipboard.
        MatrixClipboard.Clear()
        MatrixClipboard.CBMatrix.Name = "Correlation Matrix"
        MatrixClipboard.CBMatrix.Description = txtCorrelationDesc.Text
        MatrixClipboard.CBMatrix.NCols = dgvCorrMatrix.ColumnCount - 1
        MatrixClipboard.CBMatrix.NRows = dgvCorrMatrix.RowCount - 1
        MatrixClipboard.CBMatrix.Format = txtCorrMatrixFormat.Text
        Dim RowIndex As Integer
        Dim ColIndex As Integer
        For RowIndex = 0 To MatrixClipboard.CBMatrix.NRows - 1
            For ColIndex = 0 To MatrixClipboard.CBMatrix.NCols - 1
                MatrixClipboard.CBMatrix.Data(RowIndex, ColIndex) = dgvCorrMatrix.Rows(RowIndex + 1).Cells(ColIndex + 1).Value
            Next
        Next
    End Sub

    Private Sub dgvCorrMatrix_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCorrMatrix.CellEndEdit
        'An item in the Correlation Matrix has been changed.

        Dim RowNo As Integer = e.RowIndex
        Dim ColNo As Integer = e.ColumnIndex
        Dim CorrMatName As String = txtCorrelationName.Text

        If CorrMatName = "" Then
            Message.AddWarning("Please open or create a Correlation Matrix." & vbCrLf)
        Else
            If MonteCarlo.Correlations.ContainsKey(CorrMatName) Then
                If ColNo = 0 Then 'A Random Variable has been selected
                    'Copy the Random Variable name to the corresponding location in the top row.
                    dgvCorrMatrix.Rows(0).Cells(RowNo).Value = dgvCorrMatrix.Rows(RowNo).Cells(ColNo).Value

                    MonteCarlo.Correlations(CorrMatName).UnCorrRV(RowNo - 1) = dgvCorrMatrix.Rows(RowNo).Cells(ColNo).Value
                    MonteCarlo.Correlations(CorrMatName).CorrRV(RowNo - 1) = dgvCorrMatrix.Rows(RowNo).Cells(ColNo).Value
                ElseIf RowNo = 0 Then
                    MonteCarlo.Correlations(CorrMatName).CorrRV(ColNo - 1) = dgvCorrMatrix.Rows(RowNo).Cells(ColNo).Value
                Else
                    'Copy the correlation value to the corresponding upper right location.
                    dgvCorrMatrix.Rows(ColNo).Cells(RowNo).Value = dgvCorrMatrix.Rows(RowNo).Cells(ColNo).Value

                    MonteCarlo.Correlations(CorrMatName).Array(RowNo - 1, ColNo - 1) = dgvCorrMatrix.Rows(RowNo).Cells(ColNo).Value
                    MonteCarlo.Correlations(CorrMatName).Array(ColNo - 1, RowNo - 1) = dgvCorrMatrix.Rows(RowNo).Cells(ColNo).Value
                End If
            Else
                Message.AddWarning("The Correlation Matrix " & CorrMatName & " was not found." & vbCrLf)
            End If
        End If
    End Sub

    Private Sub dgvCorrMatrix_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvCorrMatrix.DataError
        Message.AddWarning(e.Exception.Message & vbCrLf)
    End Sub

#End Region 'Correlations Tab -----------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Calculations \ Information" '========================================================================================================================================================

    Private Sub btnOpenCalc_Click(sender As Object, e As EventArgs) Handles btnOpenCalc.Click
        'Open a Calculation Sequence
        Dim FileName As String = Project.SelectDataFile("Calculation Sequence", "CalcSeq")
        OpenSeq(FileName)
    End Sub

    Private Sub btnSaveCalc_Click(sender As Object, e As EventArgs) Handles btnSaveCalc.Click
        'Save the Calculation Sequence.

        Dim FileName As String = txtCalcFileName.Text
        SaveSeq(FileName)
    End Sub

    Private Sub btnNewCalc_Click(sender As Object, e As EventArgs) Handles btnNewCalc.Click
        'Create a new Calculation Sequence.

        Dim CalcSeqName As String

        'Get the new Calculation Sequence FileName, DataName and Description:
        Dim EntryForm As New ADVL_Utilities_Library_1.frmNewDataNameModal
        EntryForm.EntryName = "NewCalcSeq"
        EntryForm.Title = "New Calculation Sequence"
        EntryForm.FileExtension = "CalcSeq"
        EntryForm.GetFileName = True
        EntryForm.GetDataName = True
        EntryForm.GetDataDescription = True
        EntryForm.SettingsLocn = Project.SettingsLocn
        EntryForm.DataLocn = Project.DataLocn
        EntryForm.ApplicationName = ApplicationInfo.Name
        If EntryForm.ShowDialog() = DialogResult.OK Then
            txtCalcFileName.Text = EntryForm.FileName
            CalcSeqName = EntryForm.DataName
            txtCalcName.Text = CalcSeqName
            txtCalcDescr.Text = EntryForm.DataDescription
        Else
            Exit Sub
        End If

        If CalcSeqModified = True Then SaveSeq(txtCalcFileName.Text.Trim)

        CalcInfo.Clear()
        ScalarData.Clear()

        CalcInfo.Add(CalcSeqName, New CalcOpInfo)
        CalcInfo(CalcSeqName).Description = txtCalcDescr.Text.Trim
        CalcInfo(CalcSeqName).Type = "Calculation Sequence"

        trvCalculations.Nodes.Clear() 'Clear the nodes in the Calculations tree.
        Dim Node1 As TreeNode = New TreeNode(CalcSeqName, 82, 83)
        Node1.Name = CalcSeqName
        trvCalculations.Nodes.Add(Node1)

    End Sub

    Private Sub trvCalculations_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles trvCalculations.AfterSelect
        'Show the Calculation Node information on the Information tab.

        SelItemName = e.Node.Name
        SelDataName = e.Node.Text
        SelNode = e.Node 'Save the selected node - this may be needed later
        txtItemName.Text = SelItemName
        txtEditNodeName.Text = SelItemName
        txtEditNodeText.Text = SelDataName


        If CalcInfo.ContainsKey(SelItemName) Then
            txtItemType.Text = CalcInfo(SelItemName).Type
            txtEditNodeType.Text = CalcInfo(SelItemName).Type
            txtItemStatus.Text = CalcInfo(SelItemName).Status
            txtItemUnits.Text = CalcInfo(SelItemName).Units
            txtItemUnitsAbbrev.Text = CalcInfo(SelItemName).UnitsAbbrev
            txtItemDescription.Text = CalcInfo(SelItemName).Description
            txtEditNodeDescr.Text = CalcInfo(SelItemName).Description
            'If CalcInfo(SelItemName).Type = "Calculation Sequence" Then
            txtEditNodeUnits.Text = CalcInfo(SelItemName).Units
            txtEditNodeUnitsAbbrev.Text = CalcInfo(SelItemName).UnitsAbbrev

            txtEditColumnName.Enabled = False
            txtEditColumnName.Text = ""

            'Else
            'End If
            Select Case CalcInfo(SelItemName).Type
                Case "Calculation Sequence"
                    ShowNoItem()
                    txtEditNodeDataType.Text = "None"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False
                    'txtScalarName.Enabled = False
                    'txtScalarItem.Enabled = False

                    ShowNoItem()

                Case "Input Variable"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Scalar"
                    If ColumnInfo.ContainsKey(SelDataName) Then
                        txtColumnName.Enabled = True
                        txtColumnType.Enabled = True
                        txtColumnName.Text = ColumnInfo(SelDataName).Name
                        txtColumnType.Text = ColumnInfo(SelDataName).Type
                        txtEditColumnName.Enabled = False 'Editing of the Input column name currently not supported.
                        txtEditColumnName.Text = ColumnInfo(SelDataName).Name
                    Else
                        txtColumnName.Enabled = False
                        txtColumnType.Enabled = False
                        txtEditColumnName.Enabled = False
                        txtEditColumnName.Text = ""
                        Message.AddWarning("Column information not found for node: " & SelDataName & vbCrLf)
                    End If
                    If ColumnInfo.ContainsKey(SelDataName) Then SeriesAnalysisOpen(ColumnInfo(SelDataName).Name) 'This will bring the analysis form to the front if it is open

                Case "Input Variable User Defined"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Input Variable User Defined"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Output Value"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Output Value"
                    If ColumnInfo.ContainsKey(SelDataName) Then
                        txtColumnName.Enabled = True
                        txtColumnType.Enabled = True
                        txtColumnName.Text = ColumnInfo(SelDataName).Name
                        txtColumnType.Text = ColumnInfo(SelDataName).Type
                        txtEditColumnName.Enabled = True
                        txtEditColumnName.Text = ColumnInfo(SelDataName).Name
                    Else
                        txtColumnName.Enabled = False
                        txtColumnType.Enabled = False
                        txtEditColumnName.Enabled = False
                        txtEditColumnName.Text = ""
                        Message.AddWarning("Column information not found for node: " & SelDataName & vbCrLf)
                    End If
                    SeriesAnalysisOpen(ColumnInfo(SelDataName).Name) 'This will bring the analysis form to the front if it is open

                Case "Process"
                    ShowNoItem()
                    txtEditNodeDataType.Text = "Process"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Value Process"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Value Process"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Collection"
                    ShowNoItem()
                    txtEditNodeDataType.Text = "Collection"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Value Copy"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Value Copy"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Constant Value E"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Constant Value E"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Constant Value Pi"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Constant Value Pi"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Constant Value User Defined"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Constant Value User Defined"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Add"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Add"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Subtract"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Subtract"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Multiply"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Multiply"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Divide"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Divide"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Sum"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Sum"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Product"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Product"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Sine"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Sine"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Cosine"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Cosine"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Tangent"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Tangent"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "ArcSine"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "ArcSine"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "ArcCosine"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "ArcCosine"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "ArcTangent"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "ArcTangent"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Degrees To Radians"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Degrees To Radians"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Radians To Degrees"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Radians To Degrees"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Power Of E"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Power Of E"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Natural Logarithm"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Natural Logarithm"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Power Of Ten"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Power Of Ten"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Logarithm"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Logarithm"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Square"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Square"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Square Root"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Square Root"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Cube"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Cube"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Cube Root"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Cube Root"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Exponentiate"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Exponentiate"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Root"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Root"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Equals"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Equals"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Negate"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Negate"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Invert"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Invert"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Absolute"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Absolute"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Round"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Round"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Round Up"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Round Up"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "Round Down"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "Round Down"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "If Gt"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "If Gt"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "If GtEq"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "If GtEq"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "If Eq"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "If Eq"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "If LtEq"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "If LtEq"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case "If Lt"
                    ShowScalarItem(SelDataName)
                    txtEditNodeDataType.Text = "If Lt"
                    txtColumnName.Enabled = False
                    txtColumnType.Enabled = False

                Case Else
                    Message.AddWarning("Unknown node type: " & CalcInfo(SelItemName).Type & vbCrLf)
            End Select

        Else
            txtItemType.Text = ""
            txtItemStatus.Text = ""
            txtItemUnits.Text = ""
            txtItemUnitsAbbrev.Text = ""
            txtItemDescription.Text = ""

            txtEditNodeUnits.Text = ""
            txtEditNodeUnitsAbbrev.Text = ""
        End If
    End Sub

    Private Sub ShowNoItem()
        'Show no item value on the Information tab.

        'Disable the Scalar display:
        txtScalarName.Enabled = False
        txtScalarItem.Enabled = False
    End Sub

    Private Sub ShowScalarItem(ByVal ItemName As String)
        'Show the Scalar item

        If ScalarData.ContainsKey(ItemName) Then
            'Enable the Scalar display:
            txtScalarName.Enabled = True
            txtScalarName.Text = ItemName
            txtScalarItem.Enabled = True
            'Display the Scalar value:
            txtScalarItem.Text = ScalarData(ItemName)
        Else
            Message.AddWarning("The Scalar Item was not found: " & ItemName & vbCrLf)
        End If
    End Sub

    Private Sub btnSeriesAnalysis_Click(sender As Object, e As EventArgs) Handles btnSeriesAnalysis.Click
        'Analyse the data series in the selected column

        If txtColumnName.Enabled Then
            If txtColumnName.Text.Trim = "" Then
                Message.AddWarning("The series column name is blank." & vbCrLf)
            Else
                Dim ColumnName As String = txtColumnName.Text.Trim
                Dim ScalarName As String = txtScalarName.Text.Trim
                If MonteCarlo.Data.Tables.Contains("Calculations") Then
                    If MonteCarlo.Data.Tables("Calculations").Columns.Contains(ColumnName) Then
                        Dim FormNo As Integer
                        If chkAllowDuplicateCharts.Checked Then

                            FormNo = OpenNewSeriesAnalysis()
                            SeriesAnalysisList(FormNo).DataSource = MonteCarlo
                            SeriesAnalysisList(FormNo).DataSourceDescription = "Monte Carlo Model"
                            SeriesAnalysisList(FormNo).SourceTableName = "Calculations"
                            SeriesAnalysisList(FormNo).SourceColumnName = ColumnName
                            SeriesAnalysisList(FormNo).ScalarName = ScalarName

                            Dim RowNo As Integer = MonteCarlo.DataInfoNameIndex(ColumnName)
                            If RowNo = -1 Then
                                SeriesAnalysisList(FormNo).IsDiscrete = False
                                SeriesAnalysisList(FormNo).DistributionName = ""
                            Else
                                SeriesAnalysisList(FormNo).IsDiscrete = MonteCarlo.DataInfo(RowNo).IsDiscrete
                                SeriesAnalysisList(FormNo).DistributionName = MonteCarlo.DataInfo(RowNo).DataSetType
                                SeriesAnalysisList(FormNo).ParamAName = MonteCarlo.DataInfo(RowNo).ParameterAName
                                SeriesAnalysisList(FormNo).ParamAValue = MonteCarlo.DataInfo(RowNo).ParameterAValue
                                SeriesAnalysisList(FormNo).ParamBName = MonteCarlo.DataInfo(RowNo).ParameterBName
                                SeriesAnalysisList(FormNo).ParamBValue = MonteCarlo.DataInfo(RowNo).ParameterBValue
                                SeriesAnalysisList(FormNo).ParamCName = MonteCarlo.DataInfo(RowNo).ParameterCName
                                SeriesAnalysisList(FormNo).ParamCValue = MonteCarlo.DataInfo(RowNo).ParameterCValue
                                SeriesAnalysisList(FormNo).ParamDName = MonteCarlo.DataInfo(RowNo).ParameterDName
                                SeriesAnalysisList(FormNo).ParamDValue = MonteCarlo.DataInfo(RowNo).ParameterDValue
                                SeriesAnalysisList(FormNo).ParamEName = MonteCarlo.DataInfo(RowNo).ParameterEName
                                SeriesAnalysisList(FormNo).ParamEValue = MonteCarlo.DataInfo(RowNo).ParameterEValue
                            End If
                            SeriesAnalysisList(FormNo).Show

                        Else
                            'Dim FormNo As Integer = SeriesAnalysisFormNo(ColumnName)
                            FormNo = SeriesAnalysisFormNo(ColumnName)
                            If FormNo <> -1 Then
                                'The form is already open
                                SeriesAnalysisList(FormNo).DataSource = MonteCarlo
                                SeriesAnalysisList(FormNo).DataSourceDescription = "Monte Carlo Model"
                                SeriesAnalysisList(FormNo).SourceTableName = "Calculations"
                                SeriesAnalysisList(FormNo).SourceColumnName = ColumnName
                                SeriesAnalysisList(FormNo).ScalarName = ScalarName

                                Dim RowNo As Integer = MonteCarlo.DataInfoNameIndex(ColumnName)
                                If RowNo = -1 Then
                                    SeriesAnalysisList(FormNo).IsDiscrete = False
                                    SeriesAnalysisList(FormNo).DistributionName = ""
                                Else
                                    SeriesAnalysisList(FormNo).IsDiscrete = MonteCarlo.DataInfo(RowNo).IsDiscrete
                                    SeriesAnalysisList(FormNo).DistributionName = MonteCarlo.DataInfo(RowNo).DataSetType
                                    SeriesAnalysisList(FormNo).ParamAName = MonteCarlo.DataInfo(RowNo).ParameterAName
                                    SeriesAnalysisList(FormNo).ParamAValue = MonteCarlo.DataInfo(RowNo).ParameterAValue
                                    SeriesAnalysisList(FormNo).ParamBName = MonteCarlo.DataInfo(RowNo).ParameterBName
                                    SeriesAnalysisList(FormNo).ParamBValue = MonteCarlo.DataInfo(RowNo).ParameterBValue
                                    SeriesAnalysisList(FormNo).ParamCName = MonteCarlo.DataInfo(RowNo).ParameterCName
                                    SeriesAnalysisList(FormNo).ParamCValue = MonteCarlo.DataInfo(RowNo).ParameterCValue
                                    SeriesAnalysisList(FormNo).ParamDName = MonteCarlo.DataInfo(RowNo).ParameterDName
                                    SeriesAnalysisList(FormNo).ParamDValue = MonteCarlo.DataInfo(RowNo).ParameterDValue
                                    SeriesAnalysisList(FormNo).ParamEName = MonteCarlo.DataInfo(RowNo).ParameterEName
                                    SeriesAnalysisList(FormNo).ParamEValue = MonteCarlo.DataInfo(RowNo).ParameterEValue
                                End If
                                SeriesAnalysisList(FormNo).UpdateCharts()
                            Else
                                FormNo = OpenNewSeriesAnalysis()
                                SeriesAnalysisList(FormNo).DataSource = MonteCarlo
                                SeriesAnalysisList(FormNo).DataSourceDescription = "Monte Carlo Model"
                                SeriesAnalysisList(FormNo).SourceTableName = "Calculations"
                                SeriesAnalysisList(FormNo).SourceColumnName = ColumnName
                                SeriesAnalysisList(FormNo).ScalarName = ScalarName

                                Dim RowNo As Integer = MonteCarlo.DataInfoNameIndex(ColumnName)
                                If RowNo = -1 Then
                                    SeriesAnalysisList(FormNo).IsDiscrete = False
                                    SeriesAnalysisList(FormNo).DistributionName = ""
                                Else
                                    SeriesAnalysisList(FormNo).IsDiscrete = MonteCarlo.DataInfo(RowNo).IsDiscrete
                                    SeriesAnalysisList(FormNo).DistributionName = MonteCarlo.DataInfo(RowNo).DataSetType
                                    SeriesAnalysisList(FormNo).ParamAName = MonteCarlo.DataInfo(RowNo).ParameterAName
                                    SeriesAnalysisList(FormNo).ParamAValue = MonteCarlo.DataInfo(RowNo).ParameterAValue
                                    SeriesAnalysisList(FormNo).ParamBName = MonteCarlo.DataInfo(RowNo).ParameterBName
                                    SeriesAnalysisList(FormNo).ParamBValue = MonteCarlo.DataInfo(RowNo).ParameterBValue
                                    SeriesAnalysisList(FormNo).ParamCName = MonteCarlo.DataInfo(RowNo).ParameterCName
                                    SeriesAnalysisList(FormNo).ParamCValue = MonteCarlo.DataInfo(RowNo).ParameterCValue
                                    SeriesAnalysisList(FormNo).ParamDName = MonteCarlo.DataInfo(RowNo).ParameterDName
                                    SeriesAnalysisList(FormNo).ParamDValue = MonteCarlo.DataInfo(RowNo).ParameterDValue
                                    SeriesAnalysisList(FormNo).ParamEName = MonteCarlo.DataInfo(RowNo).ParameterEName
                                    SeriesAnalysisList(FormNo).ParamEValue = MonteCarlo.DataInfo(RowNo).ParameterEValue
                                End If
                                SeriesAnalysisList(FormNo).Show
                            End If
                        End If
                    Else
                        Message.AddWarning("The column was not found in the data table: " & ColumnName & vbCrLf)
                    End If
                Else
                    Message.AddWarning("The Calculations data table was not found." & vbCrLf)
                End If

            End If
        Else
            Message.AddWarning("The selected node does not have a data series in the Calculations table." & vbCrLf)
        End If
    End Sub

    Private Sub btnInputSeriesAnalysis_Click(sender As Object, e As EventArgs) Handles btnInputSeriesAnalysis.Click
        'Display the Series Analysis forms for all Input Variables

        For Each Item In CalcInfo
            If Item.Value.Type = "Input Variable" Then
                Dim ItemName As String = Item.Key
                If ColumnInfo.ContainsKey(ItemName) Then
                    Dim ColumnName As String = ColumnInfo(ItemName).Name
                    Dim ScalarName As String = txtScalarName.Text.Trim
                    If MonteCarlo.Data.Tables.Contains("Calculations") Then
                        If MonteCarlo.Data.Tables("Calculations").Columns.Contains(ColumnName) Then
                            If SeriesAnalysisOpen(ColumnName) Then
                                'The form is already open
                            Else
                                Dim FormNo As Integer = OpenNewSeriesAnalysis()
                                'SeriesAnalysisList(FormNo).Show
                                SeriesAnalysisList(FormNo).DataSource = MonteCarlo
                                SeriesAnalysisList(FormNo).DataSourceDescription = "Monte Carlo Model"
                                SeriesAnalysisList(FormNo).SourceTableName = "Calculations"
                                SeriesAnalysisList(FormNo).SourceColumnName = ColumnName
                                SeriesAnalysisList(FormNo).ScalarName = ScalarName

                                Dim RowNo As Integer = MonteCarlo.DataInfoNameIndex(ColumnName)
                                If RowNo = -1 Then
                                    SeriesAnalysisList(FormNo).IsDiscrete = False
                                    SeriesAnalysisList(FormNo).DistributionName = ""
                                Else
                                    SeriesAnalysisList(FormNo).IsDiscrete = MonteCarlo.DataInfo(RowNo).IsDiscrete
                                    'SeriesAnalysisList(FormNo).DistributionName = MonteCarlo.DataInfo(RowNo).Name
                                    SeriesAnalysisList(FormNo).DistributionName = MonteCarlo.DataInfo(RowNo).DataSetType
                                    SeriesAnalysisList(FormNo).ParamAName = MonteCarlo.DataInfo(RowNo).ParameterAName
                                    SeriesAnalysisList(FormNo).ParamAValue = MonteCarlo.DataInfo(RowNo).ParameterAValue
                                    SeriesAnalysisList(FormNo).ParamBName = MonteCarlo.DataInfo(RowNo).ParameterBName
                                    SeriesAnalysisList(FormNo).ParamBValue = MonteCarlo.DataInfo(RowNo).ParameterBValue
                                    SeriesAnalysisList(FormNo).ParamCName = MonteCarlo.DataInfo(RowNo).ParameterCName
                                    SeriesAnalysisList(FormNo).ParamCValue = MonteCarlo.DataInfo(RowNo).ParameterCValue
                                    SeriesAnalysisList(FormNo).ParamDName = MonteCarlo.DataInfo(RowNo).ParameterDName
                                    SeriesAnalysisList(FormNo).ParamDValue = MonteCarlo.DataInfo(RowNo).ParameterDValue
                                    SeriesAnalysisList(FormNo).ParamEName = MonteCarlo.DataInfo(RowNo).ParameterEName
                                    SeriesAnalysisList(FormNo).ParamEValue = MonteCarlo.DataInfo(RowNo).ParameterEValue
                                End If

                                SeriesAnalysisList(FormNo).Show
                            End If
                        Else
                            Message.AddWarning("The column was not found in the data table: " & ColumnName & vbCrLf)
                        End If
                    Else
                        Message.AddWarning("The Calculations data table was not found." & vbCrLf)
                    End If
                Else
                    'No column information is available for the selected Input Variable!
                End If
            End If
        Next
    End Sub

    Private Sub btnOutputSeriesAnalysis_Click(sender As Object, e As EventArgs) Handles btnOutputSeriesAnalysis.Click
        'Display the Series Analysis forms for all Output Values

        For Each Item In CalcInfo
            If Item.Value.Type = "Output Value" Then
                Dim ItemName As String = Item.Key
                If ColumnInfo.ContainsKey(ItemName) Then
                    Dim ColumnName As String = ColumnInfo(ItemName).Name
                    Dim ScalarName As String = txtScalarName.Text.Trim
                    If MonteCarlo.Data.Tables.Contains("Calculations") Then
                        If MonteCarlo.Data.Tables("Calculations").Columns.Contains(ColumnName) Then
                            Dim FormNo As Integer = SeriesAnalysisFormNo(ColumnName)
                            'If SeriesAnalysisOpen(ColumnName) Then
                            If FormNo <> -1 Then
                                'The form is already open
                                SeriesAnalysisList(FormNo).DataSource = MonteCarlo
                                SeriesAnalysisList(FormNo).DataSourceDescription = "Monte Carlo Model"
                                SeriesAnalysisList(FormNo).SourceTableName = "Calculations"
                                SeriesAnalysisList(FormNo).SourceColumnName = ColumnName
                                SeriesAnalysisList(FormNo).ScalarName = ScalarName

                                Dim RowNo As Integer = MonteCarlo.DataInfoNameIndex(ColumnName)
                                If RowNo = -1 Then
                                    SeriesAnalysisList(FormNo).IsDiscrete = False
                                    SeriesAnalysisList(FormNo).DistributionName = ""
                                Else
                                    SeriesAnalysisList(FormNo).IsDiscrete = MonteCarlo.DataInfo(RowNo).IsDiscrete
                                    'SeriesAnalysisList(FormNo).DistributionName = MonteCarlo.DataInfo(RowNo).Name
                                    SeriesAnalysisList(FormNo).DistributionName = MonteCarlo.DataInfo(RowNo).DataSetType
                                    SeriesAnalysisList(FormNo).ParamAName = MonteCarlo.DataInfo(RowNo).ParameterAName
                                    SeriesAnalysisList(FormNo).ParamAValue = MonteCarlo.DataInfo(RowNo).ParameterAValue
                                    SeriesAnalysisList(FormNo).ParamBName = MonteCarlo.DataInfo(RowNo).ParameterBName
                                    SeriesAnalysisList(FormNo).ParamBValue = MonteCarlo.DataInfo(RowNo).ParameterBValue
                                    SeriesAnalysisList(FormNo).ParamCName = MonteCarlo.DataInfo(RowNo).ParameterCName
                                    SeriesAnalysisList(FormNo).ParamCValue = MonteCarlo.DataInfo(RowNo).ParameterCValue
                                    SeriesAnalysisList(FormNo).ParamDName = MonteCarlo.DataInfo(RowNo).ParameterDName
                                    SeriesAnalysisList(FormNo).ParamDValue = MonteCarlo.DataInfo(RowNo).ParameterDValue
                                    SeriesAnalysisList(FormNo).ParamEName = MonteCarlo.DataInfo(RowNo).ParameterEName
                                    SeriesAnalysisList(FormNo).ParamEValue = MonteCarlo.DataInfo(RowNo).ParameterEValue
                                End If
                            Else
                                'Dim FormNo As Integer = OpenNewSeriesAnalysis()
                                FormNo = OpenNewSeriesAnalysis()
                                'SeriesAnalysisList(FormNo).Show
                                SeriesAnalysisList(FormNo).DataSource = MonteCarlo
                                SeriesAnalysisList(FormNo).DataSourceDescription = "Monte Carlo Model"
                                SeriesAnalysisList(FormNo).SourceTableName = "Calculations"
                                SeriesAnalysisList(FormNo).SourceColumnName = ColumnName
                                SeriesAnalysisList(FormNo).ScalarName = ScalarName

                                Dim RowNo As Integer = MonteCarlo.DataInfoNameIndex(ColumnName)
                                If RowNo = -1 Then
                                    SeriesAnalysisList(FormNo).IsDiscrete = False
                                    SeriesAnalysisList(FormNo).DistributionName = ""
                                Else
                                    SeriesAnalysisList(FormNo).IsDiscrete = MonteCarlo.DataInfo(RowNo).IsDiscrete
                                    'SeriesAnalysisList(FormNo).DistributionName = MonteCarlo.DataInfo(RowNo).Name
                                    SeriesAnalysisList(FormNo).DistributionName = MonteCarlo.DataInfo(RowNo).DataSetType
                                    SeriesAnalysisList(FormNo).ParamAName = MonteCarlo.DataInfo(RowNo).ParameterAName
                                    SeriesAnalysisList(FormNo).ParamAValue = MonteCarlo.DataInfo(RowNo).ParameterAValue
                                    SeriesAnalysisList(FormNo).ParamBName = MonteCarlo.DataInfo(RowNo).ParameterBName
                                    SeriesAnalysisList(FormNo).ParamBValue = MonteCarlo.DataInfo(RowNo).ParameterBValue
                                    SeriesAnalysisList(FormNo).ParamCName = MonteCarlo.DataInfo(RowNo).ParameterCName
                                    SeriesAnalysisList(FormNo).ParamCValue = MonteCarlo.DataInfo(RowNo).ParameterCValue
                                    SeriesAnalysisList(FormNo).ParamDName = MonteCarlo.DataInfo(RowNo).ParameterDName
                                    SeriesAnalysisList(FormNo).ParamDValue = MonteCarlo.DataInfo(RowNo).ParameterDValue
                                    SeriesAnalysisList(FormNo).ParamEName = MonteCarlo.DataInfo(RowNo).ParameterEName
                                    SeriesAnalysisList(FormNo).ParamEValue = MonteCarlo.DataInfo(RowNo).ParameterEValue
                                End If

                                SeriesAnalysisList(FormNo).Show
                            End If
                        Else
                            Message.AddWarning("The column was not found in the data table: " & ColumnName & vbCrLf)
                        End If
                    Else
                        Message.AddWarning("The Calculations data table was not found." & vbCrLf)
                    End If
                Else
                    'No column information is available for the selected Output Value!
                End If
            End If
        Next
    End Sub

    Private Sub btnCheckCalcTree_Click(sender As Object, e As EventArgs) Handles btnCheckCalcTree.Click
        'Check the calculation tree.

        If trvCalculations.Nodes.Count > 0 Then
            Dim NoErrors As Boolean = CheckCalcTree(trvCalculations.Nodes(0))
            If NoErrors = True Then
                Message.Add("No errors found in the Calculation tree." & vbCrLf)
            Else
                Message.AddWarning("Errors found in the Calculation tree." & vbCrLf)
            End If
        Else
            Message.AddWarning("The Calculation tree view has no nodes." & vbCrLf)
        End If
    End Sub

    Private Function CheckCalcTree(ByVal myNode As TreeNode) As Boolean
        'Check the calculation tree
        'Return True if the tree has no structural errors.
        'The wrong number of child nodes for a calculation type is a structural error.

        Dim NoErrors As Boolean = True
        Dim ItemName As String = myNode.Text

        If CalcInfo.ContainsKey(ItemName) Then
            Select Case CalcInfo(ItemName).Type
                Case "Calculation Sequence"
                    'This can have many nodes.
                    'Check each child node.
                    For Each childNode As TreeNode In myNode.Nodes
                        NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                    Next
                    Return NoErrors

                Case "Input Variable"
                    'This is a leaf node - no child nodes expected.
                    If myNode.Nodes.Count > 0 Then
                        Message.AddWarning("The Input Variable node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. No child nodes are expected." & vbCrLf)
                        Return False
                    Else
                        Return True
                    End If

                Case "Input Variable User Defined"
                    'This is a leaf node - no child nodes expected.
                    If myNode.Nodes.Count > 0 Then
                        Message.AddWarning("The Input Variable User Defined node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. No child nodes are expected." & vbCrLf)
                        Return False
                    Else
                        Return True
                    End If

                Case "Output Value"
                    'This should have one child node.
                    If myNode.Nodes.Count = 1 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0))
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Output Value node named " & myNode.Name & " does not have a child node. A single child node is expected." & vbCrLf)
                        Return False
                    Else
                        Message.AddWarning("The Output Value node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. A single child node is expected." & vbCrLf)
                        'Check for other errors in the child nodes:
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Process"
                    'This can have many nodes.
                    'Check each child node.
                    For Each childNode As TreeNode In myNode.Nodes
                        NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                    Next
                    Return NoErrors

                Case "Value Process"
                    'This should have one child node.

                    ''Check each child node.
                    'For Each childNode As TreeNode In myNode.Nodes
                    '    NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                    'Next

                    If myNode.Nodes.Count = 1 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0))
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Tangent node named " & myNode.Name & " does not have a child node. A single child node is expected." & vbCrLf)
                        Return False
                    Else
                        Message.AddWarning("The Tangent node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. A single child node is expected." & vbCrLf)
                        'Check for other errors in the child nodes:
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Collection"
                    'This can have many nodes.
                    'Check each child node.
                    For Each childNode As TreeNode In myNode.Nodes
                        NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                    Next
                    Return NoErrors

                Case "Value Copy"
                    'This is a leaf node - no child nodes expected.
                    If myNode.Nodes.Count > 0 Then
                        Message.AddWarning("The Value Copy node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. No child nodes are expected." & vbCrLf)
                        Return False
                    Else
                        Return True
                    End If

                Case "Constant Value E"
                    'This is a leaf node - no child nodes expected.
                    If myNode.Nodes.Count > 0 Then
                        Message.AddWarning("The Constant Value E node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. No child nodes are expected." & vbCrLf)
                        Return False
                    Else
                        Return True
                    End If

                Case "Constant Value Pi"
                    'This is a leaf node - no child nodes expected.
                    If myNode.Nodes.Count > 0 Then
                        Message.AddWarning("The Constant Value Pi node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. No child nodes are expected." & vbCrLf)
                        Return False
                    Else
                        Return True
                    End If

                Case "Constant Value User Defined"
                    'This is a leaf node - no child nodes expected.
                    If myNode.Nodes.Count > 0 Then
                        Message.AddWarning("The Constant Value User Defined node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. No child nodes are expected." & vbCrLf)
                        Return False
                    Else
                        Return True
                    End If

                Case "Add"
                    'This should have two child nodes.
                    If myNode.Nodes.Count = 2 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'If any child node contains an error then NoErrors will be False
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(1)) 'If any child node contains an error then NoErrors will be False
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Add node named " & myNode.Name & " has no child nodes. Two child nodes are expected." & vbCrLf)
                        Return False
                    ElseIf myNode.Nodes.Count = 1 Then
                        Message.AddWarning("The Add node named " & myNode.Name & " has one child node. Two child nodes are expected." & vbCrLf)
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'Check the node for any ther errors.
                        Return False
                    Else
                        Message.AddWarning("The Add node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. Two child nodes are expected." & vbCrLf)
                        'Check each child node for additional erros.
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Subtract"
                    'This should have two child nodes.
                    If myNode.Nodes.Count = 2 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'If any child node contains an error then NoErrors will be False
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(1)) 'If any child node contains an error then NoErrors will be False
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Subtract node named " & myNode.Name & " has no child nodes. Two child nodes are expected." & vbCrLf)
                        Return False
                    ElseIf myNode.Nodes.Count = 1 Then
                        Message.AddWarning("The Subtract node named " & myNode.Name & " has one child node. Two child nodes are expected." & vbCrLf)
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'Check the node for any ther errors.
                        Return False
                    Else
                        Message.AddWarning("The Subtract node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. Two child nodes are expected." & vbCrLf)
                        'Check each child node for additional erros.
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Multiply"
                    'This should have two child nodes.
                    If myNode.Nodes.Count = 2 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'If any child node contains an error then NoErrors will be False
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(1)) 'If any child node contains an error then NoErrors will be False
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Multiply node named " & myNode.Name & " has no child nodes. Two child nodes are expected." & vbCrLf)
                        Return False
                    ElseIf myNode.Nodes.Count = 1 Then
                        Message.AddWarning("The Multiply node named " & myNode.Name & " has one child node. Two child nodes are expected." & vbCrLf)
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'Check the node for any ther errors.
                        Return False
                    Else
                        Message.AddWarning("The Multiply node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. Two child nodes are expected." & vbCrLf)
                        'Check each child node for additional erros.
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Divide"
                    'This should have two child nodes.
                    If myNode.Nodes.Count = 2 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'If any child node contains an error then NoErrors will be False
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(1)) 'If any child node contains an error then NoErrors will be False
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Divide node named " & myNode.Name & " has no child nodes. Two child nodes are expected." & vbCrLf)
                        Return False
                    ElseIf myNode.Nodes.Count = 1 Then
                        Message.AddWarning("The Divide node named " & myNode.Name & " has one child node. Two child nodes are expected." & vbCrLf)
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'Check the node for any ther errors.
                        Return False
                    Else
                        Message.AddWarning("The Divide node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. Two child nodes are expected." & vbCrLf)
                        'Check each child node for additional erros.
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Sum"
                    'This can have many nodes.
                    'Check each child node.
                    For Each childNode As TreeNode In myNode.Nodes
                        NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                    Next
                    Return NoErrors

                Case "Product"
                    'This can have many nodes.
                    'Check each child node.
                    For Each childNode As TreeNode In myNode.Nodes
                        NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                    Next
                    Return NoErrors

                Case "Sine"
                    'This should have one child node.
                    If myNode.Nodes.Count = 1 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0))
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Sine node named " & myNode.Name & " does not have a child node. A single child node is expected." & vbCrLf)
                        Return False
                    Else
                        Message.AddWarning("The Sine node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. A single child node is expected." & vbCrLf)
                        'Check for other errors in the child nodes:
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Cosine"
                    'This should have one child node.
                    If myNode.Nodes.Count = 1 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0))
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Cosine node named " & myNode.Name & " does not have a child node. A single child node is expected." & vbCrLf)
                        Return False
                    Else
                        Message.AddWarning("The Cosine node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. A single child node is expected." & vbCrLf)
                        'Check for other errors in the child nodes:
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Tangent"
                    'This should have one child node.
                    If myNode.Nodes.Count = 1 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0))
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Tangent node named " & myNode.Name & " does not have a child node. A single child node is expected." & vbCrLf)
                        Return False
                    Else
                        Message.AddWarning("The Tangent node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. A single child node is expected." & vbCrLf)
                        'Check for other errors in the child nodes:
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "ArcSine"
                    'This should have one child node.
                    If myNode.Nodes.Count = 1 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0))
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The ArcSine node named " & myNode.Name & " does not have a child node. A single child node is expected." & vbCrLf)
                        Return False
                    Else
                        Message.AddWarning("The ArcSine node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. A single child node is expected." & vbCrLf)
                        'Check for other errors in the child nodes:
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "ArcCosine"
                    'This should have one child node.
                    If myNode.Nodes.Count = 1 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0))
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The ArcCosine node named " & myNode.Name & " does not have a child node. A single child node is expected." & vbCrLf)
                        Return False
                    Else
                        Message.AddWarning("The ArcCosine node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. A single child node is expected." & vbCrLf)
                        'Check for other errors in the child nodes:
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Output Value"
                    'This should have one child node.
                    If myNode.Nodes.Count = 1 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0))
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Output Value node named " & myNode.Name & " does not have a child node. A single child node is expected." & vbCrLf)
                        Return False
                    Else
                        Message.AddWarning("The ArcTangent node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. A single child node is expected." & vbCrLf)
                        'Check for other errors in the child nodes:
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Degrees To Radians"
                    'This should have one child node.
                    If myNode.Nodes.Count = 1 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0))
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Degrees To Radians node named " & myNode.Name & " does not have a child node. A single child node is expected." & vbCrLf)
                        Return False
                    Else
                        Message.AddWarning("The Degrees To Radians node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. A single child node is expected." & vbCrLf)
                        'Check for other errors in the child nodes:
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Radians To Degrees"
                    'This should have one child node.
                    If myNode.Nodes.Count = 1 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0))
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Radians To Degrees node named " & myNode.Name & " does not have a child node. A single child node is expected." & vbCrLf)
                        Return False
                    Else
                        Message.AddWarning("The Radians To Degrees node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. A single child node is expected." & vbCrLf)
                        'Check for other errors in the child nodes:
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Power Of E"
                    'This should have one child node.
                    If myNode.Nodes.Count = 1 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0))
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Power Of E node named " & myNode.Name & " does not have a child node. A single child node is expected." & vbCrLf)
                        Return False
                    Else
                        Message.AddWarning("The Power Of E node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. A single child node is expected." & vbCrLf)
                        'Check for other errors in the child nodes:
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Natural Logarithm"
                    'This should have one child node.
                    If myNode.Nodes.Count = 1 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0))
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Natural Logarithm node named " & myNode.Name & " does not have a child node. A single child node is expected." & vbCrLf)
                        Return False
                    Else
                        Message.AddWarning("The Natural Logarithm node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. A single child node is expected." & vbCrLf)
                        'Check for other errors in the child nodes:
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Power Of Ten"
                    'This should have one child node.
                    If myNode.Nodes.Count = 1 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0))
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Power Of Ten node named " & myNode.Name & " does not have a child node. A single child node is expected." & vbCrLf)
                        Return False
                    Else
                        Message.AddWarning("The Power Of Ten node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. A single child node is expected." & vbCrLf)
                        'Check for other errors in the child nodes:
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Logarithm"
                    'This should have one child node.
                    If myNode.Nodes.Count = 1 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0))
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Logarithm node named " & myNode.Name & " does not have a child node. A single child node is expected." & vbCrLf)
                        Return False
                    Else
                        Message.AddWarning("The Logarithm node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. A single child node is expected." & vbCrLf)
                        'Check for other errors in the child nodes:
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Square"
                    'This should have one child node.
                    If myNode.Nodes.Count = 1 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0))
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Square node named " & myNode.Name & " does not have a child node. A single child node is expected." & vbCrLf)
                        Return False
                    Else
                        Message.AddWarning("The Square node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. A single child node is expected." & vbCrLf)
                        'Check for other errors in the child nodes:
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Square Root"
                    'This should have one child node.
                    If myNode.Nodes.Count = 1 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0))
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Square Root node named " & myNode.Name & " does not have a child node. A single child node is expected." & vbCrLf)
                        Return False
                    Else
                        Message.AddWarning("The Square Root node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. A single child node is expected." & vbCrLf)
                        'Check for other errors in the child nodes:
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Cube"
                    'This should have one child node.
                    If myNode.Nodes.Count = 1 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0))
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Cube node named " & myNode.Name & " does not have a child node. A single child node is expected." & vbCrLf)
                        Return False
                    Else
                        Message.AddWarning("The Cube node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. A single child node is expected." & vbCrLf)
                        'Check for other errors in the child nodes:
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Cube Root"
                    'This should have one child node.
                    If myNode.Nodes.Count = 1 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0))
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Cube Root node named " & myNode.Name & " does not have a child node. A single child node is expected." & vbCrLf)
                        Return False
                    Else
                        Message.AddWarning("The Cube Root node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. A single child node is expected." & vbCrLf)
                        'Check for other errors in the child nodes:
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Exponentiate"
                    'This should have two child nodes.
                    ' = x ^ y
                    If myNode.Nodes.Count = 2 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'If any child node contains an error then NoErrors will be False
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(1)) 'If any child node contains an error then NoErrors will be False
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Add node named " & myNode.Name & " has no child nodes. Two child nodes are expected." & vbCrLf)
                        Return False
                    ElseIf myNode.Nodes.Count = 1 Then
                        Message.AddWarning("The Add node named " & myNode.Name & " has one child node. Two child nodes are expected." & vbCrLf)
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'Check the node for any ther errors.
                        Return False
                    Else
                        Message.AddWarning("The Add node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. Two child nodes are expected." & vbCrLf)
                        'Check each child node for additional erros.
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Root"
                    'This should have two child nodes.
                    ' = x ^ (1/y)
                    If myNode.Nodes.Count = 2 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'If any child node contains an error then NoErrors will be False
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(1)) 'If any child node contains an error then NoErrors will be False
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Add node named " & myNode.Name & " has no child nodes. Two child nodes are expected." & vbCrLf)
                        Return False
                    ElseIf myNode.Nodes.Count = 1 Then
                        Message.AddWarning("The Add node named " & myNode.Name & " has one child node. Two child nodes are expected." & vbCrLf)
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'Check the node for any ther errors.
                        Return False
                    Else
                        Message.AddWarning("The Add node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. Two child nodes are expected." & vbCrLf)
                        'Check each child node for additional erros.
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Equals"
                    'This should have one child node.
                    If myNode.Nodes.Count = 1 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0))
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Equals node named " & myNode.Name & " does not have a child node. A single child node is expected." & vbCrLf)
                        Return False
                    Else
                        Message.AddWarning("The Equals node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. A single child node is expected." & vbCrLf)
                        'Check for other errors in the child nodes:
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Negate"
                    'This should have one child node.
                    If myNode.Nodes.Count = 1 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0))
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Negate node named " & myNode.Name & " does not have a child node. A single child node is expected." & vbCrLf)
                        Return False
                    Else
                        Message.AddWarning("The Negate node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. A single child node is expected." & vbCrLf)
                        'Check for other errors in the child nodes:
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Invert"
                    'This should have one child node.
                    If myNode.Nodes.Count = 1 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0))
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Invert node named " & myNode.Name & " does not have a child node. A single child node is expected." & vbCrLf)
                        Return False
                    Else
                        Message.AddWarning("The Invert node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. A single child node is expected." & vbCrLf)
                        'Check for other errors in the child nodes:
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Absolute"
                    'This should have one child node.
                    If myNode.Nodes.Count = 1 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0))
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Absolute node named " & myNode.Name & " does not have a child node. A single child node is expected." & vbCrLf)
                        Return False
                    Else
                        Message.AddWarning("The Absolute node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. A single child node is expected." & vbCrLf)
                        'Check for other errors in the child nodes:
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Round"
                    'This should have one child node.
                    If myNode.Nodes.Count = 1 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0))
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Round node named " & myNode.Name & " does not have a child node. A single child node is expected." & vbCrLf)
                        Return False
                    Else
                        Message.AddWarning("The Round node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. A single child node is expected." & vbCrLf)
                        'Check for other errors in the child nodes:
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Round Up"
                    'This should have one child node.
                    If myNode.Nodes.Count = 1 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0))
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Round Up node named " & myNode.Name & " does not have a child node. A single child node is expected." & vbCrLf)
                        Return False
                    Else
                        Message.AddWarning("The Round Up node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. A single child node is expected." & vbCrLf)
                        'Check for other errors in the child nodes:
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Round Down"
                    'This should have one child node.
                    If myNode.Nodes.Count = 1 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0))
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The Round Down node named " & myNode.Name & " does not have a child node. A single child node is expected." & vbCrLf)
                        Return False
                    Else
                        Message.AddWarning("The Round Down node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. A single child node is expected." & vbCrLf)
                        'Check for other errors in the child nodes:
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "If Gt"
                    'This should have four child nodes.
                    If myNode.Nodes.Count = 4 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'If any child node contains an error then NoErrors will be False
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(1)) 'If any child node contains an error then NoErrors will be False
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(2)) 'If any child node contains an error then NoErrors will be False
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(3)) 'If any child node contains an error then NoErrors will be False
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The If Gt node named " & myNode.Name & " has no child nodes. Four child nodes are expected." & vbCrLf)
                        Return False
                    ElseIf myNode.Nodes.Count = 1 Then
                        Message.AddWarning("The If Gt node named " & myNode.Name & " has one child node. Four child nodes are expected." & vbCrLf)
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'Check the node for any ther errors.
                        Return False
                    ElseIf myNode.Nodes.Count = 2 Then
                        Message.AddWarning("The If Gt node named " & myNode.Name & " has two child node. Four child nodes are expected." & vbCrLf)
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'Check the node for any other errors.
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(1)) 'Check the node for any other errors.
                        Return False
                    ElseIf myNode.Nodes.Count = 3 Then
                        Message.AddWarning("The If Gt node named " & myNode.Name & " has three child node. Four child nodes are expected." & vbCrLf)
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'Check the node for any other errors.
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(1)) 'Check the node for any other errors.
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(2)) 'Check the node for any other errors.
                        Return False
                    Else
                        Message.AddWarning("The If Gt node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. Four child nodes are expected." & vbCrLf)
                        'Check each child node for additional erros.
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "If GtEq"
                    'This should have four child nodes.
                    If myNode.Nodes.Count = 4 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'If any child node contains an error then NoErrors will be False
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(1)) 'If any child node contains an error then NoErrors will be False
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(2)) 'If any child node contains an error then NoErrors will be False
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(3)) 'If any child node contains an error then NoErrors will be False
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The If GtEq node named " & myNode.Name & " has no child nodes. Four child nodes are expected." & vbCrLf)
                        Return False
                    ElseIf myNode.Nodes.Count = 1 Then
                        Message.AddWarning("The If GtEq node named " & myNode.Name & " has one child node. Four child nodes are expected." & vbCrLf)
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'Check the node for any ther errors.
                        Return False
                    ElseIf myNode.Nodes.Count = 2 Then
                        Message.AddWarning("The If GtEq node named " & myNode.Name & " has two child node. Four child nodes are expected." & vbCrLf)
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'Check the node for any other errors.
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(1)) 'Check the node for any other errors.
                        Return False
                    ElseIf myNode.Nodes.Count = 3 Then
                        Message.AddWarning("The If GtEq node named " & myNode.Name & " has three child node. Four child nodes are expected." & vbCrLf)
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'Check the node for any other errors.
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(1)) 'Check the node for any other errors.
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(2)) 'Check the node for any other errors.
                        Return False
                    Else
                        Message.AddWarning("The If GtEq node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. Four child nodes are expected." & vbCrLf)
                        'Check each child node for additional erros.
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "If Eq"
                    'This should have four child nodes.
                    If myNode.Nodes.Count = 4 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'If any child node contains an error then NoErrors will be False
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(1)) 'If any child node contains an error then NoErrors will be False
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(2)) 'If any child node contains an error then NoErrors will be False
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(3)) 'If any child node contains an error then NoErrors will be False
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The If Eq node named " & myNode.Name & " has no child nodes. Four child nodes are expected." & vbCrLf)
                        Return False
                    ElseIf myNode.Nodes.Count = 1 Then
                        Message.AddWarning("The If Eq node named " & myNode.Name & " has one child node. Four child nodes are expected." & vbCrLf)
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'Check the node for any ther errors.
                        Return False
                    ElseIf myNode.Nodes.Count = 2 Then
                        Message.AddWarning("The If Eq node named " & myNode.Name & " has two child node. Four child nodes are expected." & vbCrLf)
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'Check the node for any other errors.
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(1)) 'Check the node for any other errors.
                        Return False
                    ElseIf myNode.Nodes.Count = 3 Then
                        Message.AddWarning("The If Eq node named " & myNode.Name & " has three child node. Four child nodes are expected." & vbCrLf)
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'Check the node for any other errors.
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(1)) 'Check the node for any other errors.
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(2)) 'Check the node for any other errors.
                        Return False
                    Else
                        Message.AddWarning("The If Eq node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. Four child nodes are expected." & vbCrLf)
                        'Check each child node for additional erros.
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "If LtEq"
                    'This should have four child nodes.
                    If myNode.Nodes.Count = 4 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'If any child node contains an error then NoErrors will be False
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(1)) 'If any child node contains an error then NoErrors will be False
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(2)) 'If any child node contains an error then NoErrors will be False
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(3)) 'If any child node contains an error then NoErrors will be False
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The If LtEq node named " & myNode.Name & " has no child nodes. Four child nodes are expected." & vbCrLf)
                        Return False
                    ElseIf myNode.Nodes.Count = 1 Then
                        Message.AddWarning("The If LtEq node named " & myNode.Name & " has one child node. Four child nodes are expected." & vbCrLf)
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'Check the node for any ther errors.
                        Return False
                    ElseIf myNode.Nodes.Count = 2 Then
                        Message.AddWarning("The If LtEq node named " & myNode.Name & " has two child node. Four child nodes are expected." & vbCrLf)
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'Check the node for any other errors.
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(1)) 'Check the node for any other errors.
                        Return False
                    ElseIf myNode.Nodes.Count = 3 Then
                        Message.AddWarning("The If LtEq node named " & myNode.Name & " has three child node. Four child nodes are expected." & vbCrLf)
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'Check the node for any other errors.
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(1)) 'Check the node for any other errors.
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(2)) 'Check the node for any other errors.
                        Return False
                    Else
                        Message.AddWarning("The If LtEq node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. Four child nodes are expected." & vbCrLf)
                        'Check each child node for additional erros.
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case "Lt"
                    'This should have four child nodes.
                    If myNode.Nodes.Count = 4 Then
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'If any child node contains an error then NoErrors will be False
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(1)) 'If any child node contains an error then NoErrors will be False
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(2)) 'If any child node contains an error then NoErrors will be False
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(3)) 'If any child node contains an error then NoErrors will be False
                        Return NoErrors
                    ElseIf myNode.Nodes.Count = 0 Then
                        Message.AddWarning("The If Lt node named " & myNode.Name & " has no child nodes. Four child nodes are expected." & vbCrLf)
                        Return False
                    ElseIf myNode.Nodes.Count = 1 Then
                        Message.AddWarning("The If Lt node named " & myNode.Name & " has one child node. Four child nodes are expected." & vbCrLf)
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'Check the node for any ther errors.
                        Return False
                    ElseIf myNode.Nodes.Count = 2 Then
                        Message.AddWarning("The If Lt node named " & myNode.Name & " has two child node. Four child nodes are expected." & vbCrLf)
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'Check the node for any other errors.
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(1)) 'Check the node for any other errors.
                        Return False
                    ElseIf myNode.Nodes.Count = 3 Then
                        Message.AddWarning("The If Lt node named " & myNode.Name & " has three child node. Four child nodes are expected." & vbCrLf)
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(0)) 'Check the node for any other errors.
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(1)) 'Check the node for any other errors.
                        NoErrors = NoErrors And CheckCalcTree(myNode.Nodes(2)) 'Check the node for any other errors.
                        Return False
                    Else
                        Message.AddWarning("The If Lt node named " & myNode.Name & " has " & myNode.Nodes.Count & " child nodes. Four child nodes are expected." & vbCrLf)
                        'Check each child node for additional erros.
                        For Each childNode As TreeNode In myNode.Nodes
                            NoErrors = NoErrors And CheckCalcTree(childNode) 'If any child node contains an error then NoErrors will be False
                        Next
                        Return False
                    End If

                Case Else
                    Message.AddWarning("Unknown node type: " & CalcInfo(SelItemName).Type & vbCrLf)
                    Return False
            End Select
        End If
    End Function

    Private Sub txtScalarItem_LostFocus(sender As Object, e As EventArgs) Handles txtScalarItem.LostFocus
        If ScalarData.ContainsKey(SelDataName) Then
            ScalarData(SelDataName) = txtScalarItem.Text
        End If
    End Sub

    Private Sub btnPrevTrial_Click(sender As Object, e As EventArgs) Handles btnPrevTrial.Click
        'Select the next trial number
        TrialNo = TrialNo - 1
    End Sub

    Private Sub btnNextTrial_Click(sender As Object, e As EventArgs) Handles btnNextTrial.Click
        'Select the previous trial number
        TrialNo = TrialNo + 1
    End Sub

    Private Sub btnRunTrial_Click(sender As Object, e As EventArgs) Handles btnRunTrial.Click
        'Run the selected trial number.

        If MonteCarlo.Data.Tables.Contains("Calculations") Then
            Dim ColumnName As String
            Dim ColumnType As String
            Dim ItemName As String
            For Each item In CalcInfo
                If item.Value.Type = "Input Variable" Then
                    'Get the value from the Calculations table
                    ItemName = item.Key
                    If ColumnInfo.ContainsKey(ItemName) Then
                        ColumnName = ColumnInfo(ItemName).Name
                        ScalarData(ItemName) = MonteCarlo.Data.Tables("Calculations").Rows(TrialNo - 1).Item(ColumnName)
                    Else
                        Message.AddWarning("Calculations column name not found for Input Variable: " & ItemName & vbCrLf)
                    End If
                End If
            Next
            'Get the Calculated values
            For Each item In CalcInfo
                If item.Value.Type = "Output Value" Then
                    ItemName = item.Key
                    'Check that the output value Column exists in the Calculations table
                    If ColumnInfo.ContainsKey(ItemName) Then
                        ColumnName = ColumnInfo(ItemName).Name
                        ColumnType = ColumnInfo(ItemName).Type
                        If MonteCarlo.Data.Tables("Calculations").Columns.Contains(ColumnName) Then
                            'The Output Value column exists in the Calculations table.
                        Else
                            'Create the Output Value column:
                            MonteCarlo.CreateNewColumn("Calculations", ColumnName, ColumnType)
                        End If

                        'Recalculate the value for this trial
                        Dim myNode As TreeNode() = trvCalculations.Nodes.Find(ItemName, True) 'Find the Output Value node.
                        If myNode.Count = 1 Then
                            RecalcNode(myNode(0))
                            'Save the updated value:
                            MonteCarlo.Data.Tables("Calculations").Rows(TrialNo - 1).Item(ColumnName) = ScalarData(ItemName)
                        ElseIf myNode.Count = 0 Then
                            Message.AddWarning("Calculation Tree node not found for Output Value: " & ItemName & vbCrLf)
                        Else
                            Message.AddWarning("More than one Calculation Tree node found for Output Value: " & ItemName & vbCrLf)
                        End If
                    Else
                        Message.AddWarning("Calculations column name not found for Output Value: " & ItemName & vbCrLf)
                    End If
                End If
            Next
        Else
            Message.AddWarning("There is no Calculations table. No Monte Carlo input data is available." & vbCrLf)
        End If
    End Sub

    Private Sub btnRunAllTrials_Click(sender As Object, e As EventArgs) Handles btnRunAllTrials.Click
        'Run all the trials

        If MonteCarlo.Data.Tables.Contains("Calculations") Then
            Dim ColumnName As String
            Dim ColumnType As String
            Dim ItemName As String
            Dim ColumnMissing As Boolean = False
            Dim NodeError As Boolean = False
            Dim Input As New List(Of InputInfo)
            Dim Output As New List(Of OutputInfo)
            Dim CalcInfoToDelete As New List(Of String)

            Dim StartTime As Date = Now

            For Each item In CalcInfo
                If item.Value.Type = "Input Variable" Then
                    'Get the value from the Calculations table
                    ItemName = item.Key
                    If ColumnInfo.ContainsKey(ItemName) Then
                        ' ColumnName = ColumnInfo(ItemName).Name
                        ' ScalarData(ItemName) = MonteCarlo.Data.Tables("Calculations").Rows(TrialNo - 1).Item(ColumnName)
                        Dim NewInputInfo As New InputInfo
                        NewInputInfo.Name = ItemName
                        NewInputInfo.ColumnName = ColumnInfo(ItemName).Name
                        Input.Add(NewInputInfo)
                    Else
                        If chkRemoveUnusedValues.Checked Then
                            CalcInfoToDelete.Add(ItemName)
                        Else
                            Message.AddWarning("Calculations column name not found for Input Variable: " & ItemName & vbCrLf)
                            ColumnMissing = True
                        End If
                    End If
                End If
            Next

            If ColumnMissing = False Then
                'Create any required Output Value Columns in the Calculations table:
                'Dim CalcInfoToDelete As New List(Of String)
                For Each item In CalcInfo
                    If item.Value.Type = "Output Value" Then
                        ItemName = item.Key
                        If ColumnInfo.ContainsKey(ItemName) Then
                            ColumnName = ColumnInfo(ItemName).Name
                            ColumnType = ColumnInfo(ItemName).Type

                            'NOTE: the following code has been moved to the new location below. The Output Value column is now only created if a single corresponding Calculation Tree node is found.
                            'If MonteCarlo.Data.Tables("Calculations").Columns.Contains(ColumnName) Then
                            '    'The Output Value column exists in the Calculations table.
                            'Else
                            '    'Create the Output Value column:
                            '    MonteCarlo.CreateNewColumn("Calculations", ColumnName, ColumnType)
                            'End If

                            Dim NewOutputInfo As New OutputInfo
                            NewOutputInfo.Name = ItemName
                            NewOutputInfo.ColumnName = ColumnName
                            Dim myNode As TreeNode() = trvCalculations.Nodes.Find(ItemName, True) 'Find the Output Value node.
                            If myNode.Count = 1 Then
                                NewOutputInfo.Node = myNode(0)
                                Output.Add(NewOutputInfo)

                                If MonteCarlo.Data.Tables("Calculations").Columns.Contains(ColumnName) Then
                                    'The Output Value column exists in the Calculations table.
                                Else
                                    'Create the Output Value column:
                                    MonteCarlo.CreateNewColumn("Calculations", ColumnName, ColumnType)
                                End If

                            ElseIf myNode.Count = 0 Then
                                If chkRemoveUnusedValues.Checked Then 'Remove the unused Output Value entry from CalcInfo()
                                    CalcInfoToDelete.Add(ItemName)
                                Else
                                    Message.AddWarning("Calculation Tree node not found for Output Value: " & ItemName & vbCrLf)
                                    NodeError = True
                                End If
                            Else
                                Message.AddWarning("More than one Calculation Tree node found for Output Value: " & ItemName & vbCrLf)
                                NodeError = True
                            End If
                        Else
                            Message.AddWarning("Output Value column information not found for Output Value: " & ItemName & vbCrLf)
                            ColumnMissing = True
                        End If
                    End If
                Next

                'Delete any unused Output Value entries:
                For Each item In CalcInfoToDelete
                    CalcInfo.Remove(item)
                Next

                If ColumnMissing = False Then
                    If NodeError = False Then
                        Dim I As Integer
                        For I = 1 To MonteCarlo.NTrials
                            'Get the Input Variables for the current trial:
                            For Each item In Input
                                ScalarData(item.Name) = MonteCarlo.Data.Tables("Calculations").Rows(I - 1).Item(item.ColumnName)
                            Next
                            'Recalculate each Output Value and save in the Calculations table:
                            For Each item In Output
                                RecalcNode(item.Node)
                                MonteCarlo.Data.Tables("Calculations").Rows(I - 1).Item(item.ColumnName) = ScalarData(item.Name)
                            Next
                        Next
                        Message.Add("Monte Carlo simulation complete." & vbCrLf)

                        Dim Duration As TimeSpan = Now - StartTime
                        Message.Add("Time taken: " & Duration.TotalMilliseconds & " ms" & vbCrLf)

                    Else
                        Message.AddWarning("Output Value node information not found for at least one Output Value: " & vbCrLf)
                    End If
                Else
                    Message.AddWarning("Calculations column name not found for at lease one Output Value" & vbCrLf)
                End If
            Else
                Message.AddWarning("At least one Input Variable column is missing from the Calculations table. The Monte Carlo simulation can not be run." & vbCrLf)
            End If
        Else
            Message.AddWarning("There is no Calculations table. No Monte Carlo input data is available." & vbCrLf)
        End If

    End Sub

    Private Sub btnRecalculateNode_Click(sender As Object, e As EventArgs) Handles btnRecalculateNode.Click
        'Recalculate the value of the selected node
        RecalcNode(SelNode)
        ShowScalarItem(SelItemName)
    End Sub

    Private Sub txtItemDescription_LostFocus(sender As Object, e As EventArgs) Handles txtItemDescription.LostFocus
        If CalcInfo.ContainsKey(SelItemName) Then
            CalcInfo(SelItemName).Description = txtItemDescription.Text
        End If
    End Sub

    Private Sub txtItemUnitsAbbrev_LostFocus(sender As Object, e As EventArgs) Handles txtItemUnitsAbbrev.LostFocus
        If CalcInfo.ContainsKey(SelItemName) Then
            CalcInfo(SelItemName).UnitsAbbrev = txtItemUnitsAbbrev.Text
        End If
    End Sub

    Private Sub txtItemUnits_LostFocus(sender As Object, e As EventArgs) Handles txtItemUnits.LostFocus
        If CalcInfo.ContainsKey(SelItemName) Then
            CalcInfo(SelItemName).Units = txtItemUnits.Text
        End If
    End Sub

    Private Sub UpdateNode(ByRef myNode As TreeNode)
        'Update the calculation in myNode
        'The updates are propagated up the tree.

        Dim ItemName As String = myNode.Text

        If CalcInfo.ContainsKey(ItemName) Then

            Select Case CalcInfo(ItemName).Type
                Case "Calculation Sequence"
                    'No calculation
                Case "Input Variable"
                    UpdateNode(myNode.Parent)   'Update parent node
                Case "Input Variable User Defined"
                    UpdateNode(myNode.Parent)   'Update parent node
                Case "Output Value"
                    If HasOneChildDataNode(myNode) Then
                        ScalarData(ItemName) = ScalarData(myNode.Nodes(0).Text) 'Get the Output Value from the child node. (There should be a single child node.)                       
                        UpdateNode(myNode.Parent)
                    End If
                Case "Process"
                      'No calculation
                Case "Value Process"
                    UpdateNode(myNode.Parent)   'Update parent node
                Case "Collection"
                      'No calculation
                Case "Value Copy"
                     'No calculation
                Case "Constant Value E"
                     'No calculation
                Case "Constant Value Pi"
                     'No calculation
                Case "Constant Value User Defined"
                    UpdateNode(myNode.Parent)   'Update parent node
                Case "Add"
                    If HasTwoChildDataNodes(myNode) Then
                        ScalarData(ItemName) = ScalarData(myNode.Nodes(0).Text) + ScalarData(myNode.Nodes(1).Text)
                        UpdateNode(myNode.Parent)   'Update parent node
                    End If
                Case "Subtract"
                    If HasTwoChildDataNodes(myNode) Then
                        ScalarData(ItemName) = ScalarData(myNode.Nodes(0).Text) - ScalarData(myNode.Nodes(1).Text)
                        UpdateNode(myNode.Parent)   'Update parent node
                    End If
                Case "Multiply"
                    If HasTwoChildDataNodes(myNode) Then
                        ScalarData(ItemName) = ScalarData(myNode.Nodes(0).Text) * ScalarData(myNode.Nodes(1).Text)
                        UpdateNode(myNode.Parent)   'Update parent node
                    End If
                Case "Divide"
                    If HasTwoChildDataNodes(myNode) Then
                        ScalarData(ItemName) = ScalarData(myNode.Nodes(0).Text) / ScalarData(myNode.Nodes(1).Text)
                        UpdateNode(myNode.Parent)   'Update parent node
                    End If
                Case "Sum"
                    Dim I As Integer
                    Dim Sum As Double = 0
                    Dim NodeText As String
                    For I = 0 To myNode.Nodes.Count - 1
                        NodeText = myNode.Nodes(I).Text
                        If ScalarData.ContainsKey(NodeText) Then
                            Sum += ScalarData(NodeText)
                        End If
                    Next
                    ScalarData(ItemName) = Sum
                    UpdateNode(myNode.Parent)
                Case "Product"
                    Dim I As Integer
                    Dim Product As Double = 1
                    Dim NodeText As String
                    For I = 0 To myNode.Nodes.Count - 1
                        NodeText = myNode.Nodes(I).Text
                        If ScalarData.ContainsKey(NodeText) Then
                            Product *= ScalarData(NodeText)
                        End If
                    Next
                    ScalarData(ItemName) = Product
                    UpdateNode(myNode.Parent)
                Case "Sine"
                    If HasOneChildDataNode(myNode) Then
                        ScalarData(ItemName) = Math.Sin(ScalarData(myNode.Nodes(0).Text))
                        UpdateNode(myNode.Parent)
                    End If
                Case "Cosine"
                    If HasOneChildDataNode(myNode) Then
                        ScalarData(ItemName) = Math.Cos(ScalarData(myNode.Nodes(0).Text))
                        UpdateNode(myNode.Parent)
                    End If
                Case "Tangent"
                    If HasOneChildDataNode(myNode) Then
                        ScalarData(ItemName) = Math.Tan(ScalarData(myNode.Nodes(0).Text))
                        UpdateNode(myNode.Parent)
                    End If
                Case "ArcSine"
                    If HasOneChildDataNode(myNode) Then
                        ScalarData(ItemName) = Math.Asin(ScalarData(myNode.Nodes(0).Text))
                        UpdateNode(myNode.Parent)
                    End If
                Case "ArcCosine"
                    If HasOneChildDataNode(myNode) Then
                        ScalarData(ItemName) = Math.Acos(ScalarData(myNode.Nodes(0).Text))
                        UpdateNode(myNode.Parent)
                    End If
                Case "ArcTangent"
                    If HasOneChildDataNode(myNode) Then
                        ScalarData(ItemName) = Math.Atan(ScalarData(myNode.Nodes(0).Text))
                        UpdateNode(myNode.Parent)
                    End If
                Case "Degrees To Radians"
                    If HasOneChildDataNode(myNode) Then
                        ScalarData(ItemName) = ScalarData(myNode.Nodes(0).Text) * 2 * Math.PI / 360
                        UpdateNode(myNode.Parent)
                    End If
                Case "Radians To Degrees"
                    If HasOneChildDataNode(myNode) Then
                        ScalarData(ItemName) = ScalarData(myNode.Nodes(0).Text) / 2 / Math.PI * 360
                        UpdateNode(myNode.Parent)
                    End If
                Case "Power Of E"
                    If HasOneChildDataNode(myNode) Then
                        ScalarData(ItemName) = Math.E ^ ScalarData(myNode.Nodes(0).Text)
                        UpdateNode(myNode.Parent)
                    End If
                Case "Natural Logarithm"
                    If HasOneChildDataNode(myNode) Then
                        ScalarData(ItemName) = Math.Log(ScalarData(myNode.Nodes(0).Text))
                        UpdateNode(myNode.Parent)
                    End If
                Case "Power Of Ten"
                    If HasOneChildDataNode(myNode) Then
                        ScalarData(ItemName) = 10 ^ ScalarData(myNode.Nodes(0).Text)
                        UpdateNode(myNode.Parent)
                    End If
                Case "Logarithm"
                    If HasOneChildDataNode(myNode) Then
                        ScalarData(ItemName) = Math.Log10(ScalarData(myNode.Nodes(0).Text))
                        UpdateNode(myNode.Parent)
                    End If
                Case "Square"
                    If HasOneChildDataNode(myNode) Then
                        ScalarData(ItemName) = ScalarData(myNode.Nodes(0).Text) ^ 2
                        UpdateNode(myNode.Parent)
                    End If
                Case "Square Root"
                    If HasOneChildDataNode(myNode) Then
                        ScalarData(ItemName) = Math.Sqrt(ScalarData(myNode.Nodes(0).Text))
                        UpdateNode(myNode.Parent)
                    End If
                Case "Cube"
                    If HasOneChildDataNode(myNode) Then
                        ScalarData(ItemName) = ScalarData(myNode.Nodes(0).Text) ^ 3
                        UpdateNode(myNode.Parent)
                    End If
                Case "Cube Root"
                    If HasOneChildDataNode(myNode) Then
                        ScalarData(ItemName) = ScalarData(myNode.Nodes(0).Text) ^ (1 / 3)
                        UpdateNode(myNode.Parent)
                    End If
                Case "Exponentiate"
                    If HasTwoChildDataNodes(myNode) Then
                        ScalarData(ItemName) = ScalarData(myNode.Nodes(0).Text) ^ ScalarData(myNode.Nodes(1).Text)
                        UpdateNode(myNode.Parent)   'Update parent node
                    End If
                Case "Root"
                    If HasTwoChildDataNodes(myNode) Then
                        ScalarData(ItemName) = ScalarData(myNode.Nodes(0).Text) ^ (1 / ScalarData(myNode.Nodes(1).Text))
                        UpdateNode(myNode.Parent)   'Update parent node
                    End If
                Case "Equals"
                    If HasOneChildDataNode(myNode) Then
                        ScalarData(ItemName) = ScalarData(myNode.Nodes(0).Text)
                        UpdateNode(myNode.Parent)
                    End If
                Case "Negate"
                    If HasOneChildDataNode(myNode) Then
                        ScalarData(ItemName) = -ScalarData(myNode.Nodes(0).Text)
                        UpdateNode(myNode.Parent)
                    End If
                Case "Invert"
                    If HasOneChildDataNode(myNode) Then
                        ScalarData(ItemName) = 1 / ScalarData(myNode.Nodes(0).Text)
                        UpdateNode(myNode.Parent)
                    End If
                Case "Absolute"
                    If HasOneChildDataNode(myNode) Then
                        ScalarData(ItemName) = Math.Abs(ScalarData(myNode.Nodes(0).Text))
                        UpdateNode(myNode.Parent)
                    End If
                Case "Round"
                    If HasOneChildDataNode(myNode) Then
                        ScalarData(ItemName) = Math.Round(ScalarData(myNode.Nodes(0).Text))
                        UpdateNode(myNode.Parent)
                    End If
                Case "Round Up"
                    If HasOneChildDataNode(myNode) Then
                        ScalarData(ItemName) = Math.Ceiling(ScalarData(myNode.Nodes(0).Text))
                        UpdateNode(myNode.Parent)
                    End If
                Case "Round Down"
                    If HasOneChildDataNode(myNode) Then
                        ScalarData(ItemName) = Math.Floor(ScalarData(myNode.Nodes(0).Text))
                        UpdateNode(myNode.Parent)
                    End If
                Case "If Gt"
                    If HasFourChildDataNodes(myNode) Then
                        If ScalarData(myNode.Nodes(0).Text) > ScalarData(myNode.Nodes(1).Text) Then ScalarData(ItemName) = ScalarData(myNode.Nodes(2).Text) Else ScalarData(ItemName) = ScalarData(myNode.Nodes(3).Text)
                        UpdateNode(myNode.Parent)
                    End If
                Case "If GtEq"
                    If HasFourChildDataNodes(myNode) Then
                        If ScalarData(myNode.Nodes(0).Text) >= ScalarData(myNode.Nodes(1).Text) Then ScalarData(ItemName) = ScalarData(myNode.Nodes(2).Text) Else ScalarData(ItemName) = ScalarData(myNode.Nodes(3).Text)
                        UpdateNode(myNode.Parent)
                    End If
                Case "If Eq"
                    If HasFourChildDataNodes(myNode) Then
                        If ScalarData(myNode.Nodes(0).Text) = ScalarData(myNode.Nodes(1).Text) Then ScalarData(ItemName) = ScalarData(myNode.Nodes(2).Text) Else ScalarData(ItemName) = ScalarData(myNode.Nodes(3).Text)
                        UpdateNode(myNode.Parent)
                    End If
                Case "If LtEq"
                    If HasFourChildDataNodes(myNode) Then
                        If ScalarData(myNode.Nodes(0).Text) <= ScalarData(myNode.Nodes(1).Text) Then ScalarData(ItemName) = ScalarData(myNode.Nodes(2).Text) Else ScalarData(ItemName) = ScalarData(myNode.Nodes(3).Text)
                        UpdateNode(myNode.Parent)
                    End If
                Case "If Lt"
                    If HasFourChildDataNodes(myNode) Then
                        If ScalarData(myNode.Nodes(0).Text) < ScalarData(myNode.Nodes(1).Text) Then ScalarData(ItemName) = ScalarData(myNode.Nodes(2).Text) Else ScalarData(ItemName) = ScalarData(myNode.Nodes(3).Text)
                        UpdateNode(myNode.Parent)
                    End If
                Case Else
                    Message.AddWarning("Unknown node type: " & CalcInfo(ItemName).Type & vbCrLf)
            End Select
        Else
            Message.AddWarning("Unknown calculation name: " & ItemName & vbCrLf)
        End If

    End Sub

    Private Function HasTwoChildDataNodes(ByRef myNode As TreeNode) As Boolean
        'Return True if myNode has two child data nodes.
        If myNode.Nodes.Count = 2 Then
            Dim FirstChildNodeText As String = myNode.Nodes(0).Text
            Dim SecondChildNodeText As String = myNode.Nodes(1).Text
            If CalcInfo.ContainsKey(FirstChildNodeText) Then
                If CalcInfo.ContainsKey(SecondChildNodeText) Then
                    If ScalarData.ContainsKey(FirstChildNodeText) Then
                        If ScalarData.ContainsKey(SecondChildNodeText) Then
                            If ScalarData.ContainsKey(ItemName) Then
                                'ScalarData(ItemName) = ScalarData(FirstChildNodeText) + ScalarData(SecondChildNodeText)
                                'UpdateNode(myNode.Parent)   'Update parent node
                                Return True
                            Else
                                Message.AddWarning("The Node contains no data: " & ItemName & vbCrLf)
                                Return False
                            End If
                        Else
                            Message.AddWarning("The Child Node contains no data: " & SecondChildNodeText & vbCrLf)
                            Return False
                        End If
                    Else
                        Message.AddWarning("The Child Node contains no data: " & FirstChildNodeText & vbCrLf)
                        Return False
                    End If
                Else
                    Message.AddWarning("Unknown Child Node operation name: " & SecondChildNodeText & vbCrLf)
                    Return False
                End If
            Else
                Message.AddWarning("Unknown Child Node operation name: " & FirstChildNodeText & vbCrLf)
                Return False
            End If
        ElseIf myNode.Nodes.Count = 0 Then
            Message.AddWarning("The node named " & ItemName & " does not have a child node." & vbCrLf)
            Return False
        ElseIf myNode.Nodes.Count = 1 Then
            Message.AddWarning("The named " & ItemName & " only has one child node." & vbCrLf)
            Return False
        Else
            Message.AddWarning("The named " & ItemName & " has more than two child nodes." & vbCrLf)
            Return False
        End If
    End Function

    Private Function HasOneChildDataNode(ByRef myNode As TreeNode) As Boolean
        'Return True if myNode has one child data node.
        If myNode.Nodes.Count = 1 Then
            Dim ChildNodeText As String = myNode.Nodes(0).Text
            If CalcInfo.ContainsKey(ChildNodeText) Then
                If ScalarData.ContainsKey(ChildNodeText) Then
                    Return True
                Else
                    Message.AddWarning("The Child Node contains no data: " & ChildNodeText & vbCrLf)
                    Return False
                End If
            Else
                Message.AddWarning("Unknown Child Node operation name: " & ChildNodeText & vbCrLf)
                Return False
            End If
        ElseIf myNode.Nodes.Count = 0 Then
            Message.AddWarning("The node named " & ItemName & " does not have a child node." & vbCrLf)
            Return False
        Else
            Message.AddWarning("The named " & ItemName & " has more than one child node." & vbCrLf)
            Return False
        End If

    End Function

    Private Function HasFourChildDataNodes(ByRef myNode As TreeNode) As Boolean
        'Return True if myNode has four child data nodes.
        If myNode.Nodes.Count = 4 Then
            Dim FirstChildNodeText As String = myNode.Nodes(0).Text
            Dim SecondChildNodeText As String = myNode.Nodes(1).Text
            Dim ThirdChildNodeText As String = myNode.Nodes(2).Text
            Dim FourthChildNodeText As String = myNode.Nodes(3).Text
            If CalcInfo.ContainsKey(FirstChildNodeText) Then
                If CalcInfo.ContainsKey(SecondChildNodeText) Then
                    If CalcInfo.ContainsKey(ThirdChildNodeText) Then
                        If CalcInfo.ContainsKey(FourthChildNodeText) Then
                            If ScalarData.ContainsKey(FirstChildNodeText) Then
                                If ScalarData.ContainsKey(SecondChildNodeText) Then
                                    If ScalarData.ContainsKey(ThirdChildNodeText) Then
                                        If ScalarData.ContainsKey(FourthChildNodeText) Then
                                            Return True
                                        Else
                                            Message.AddWarning("The Child Node contains no data: " & FourthChildNodeText & vbCrLf)
                                            Return False
                                        End If
                                    Else
                                        Message.AddWarning("The Child Node contains no data: " & ThirdChildNodeText & vbCrLf)
                                        Return False
                                    End If
                                Else
                                    Message.AddWarning("The Child Node contains no data: " & SecondChildNodeText & vbCrLf)
                                    Return False
                                End If
                            Else
                                Message.AddWarning("The Child Node contains no data: " & FirstChildNodeText & vbCrLf)
                                Return False
                            End If
                        Else
                            Message.AddWarning("Unknown Child Node operation name: " & FourthChildNodeText & vbCrLf)
                            Return False
                        End If
                    Else
                        Message.AddWarning("Unknown Child Node operation name: " & ThirdChildNodeText & vbCrLf)
                        Return False
                    End If
                Else
                    Message.AddWarning("Unknown Child Node operation name: " & SecondChildNodeText & vbCrLf)
                    Return False
                End If
            Else
                Message.AddWarning("Unknown Child Node operation name: " & FirstChildNodeText & vbCrLf)
                Return False
            End If
        ElseIf myNode.Nodes.Count = 0 Then
            Message.AddWarning("The node named " & ItemName & " does not have a child node." & vbCrLf)
            Return False
        ElseIf myNode.Nodes.Count = 1 Then
            Message.AddWarning("The named " & ItemName & " only has one child node." & vbCrLf)
            Return False
        ElseIf myNode.Nodes.Count = 2 Then
            Message.AddWarning("The named " & ItemName & " only has two child nodes." & vbCrLf)
            Return False
        ElseIf myNode.Nodes.Count = 3 Then
            Message.AddWarning("The named " & ItemName & " only has three child nodes." & vbCrLf)
            Return False
        Else
            Message.AddWarning("The named " & ItemName & " has more than four child nodes." & vbCrLf)
            Return False
        End If

    End Function


#End Region 'Calculations \ Information -------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Calculations \ Create Tab" '=========================================================================================================================================================

    Private Sub pbIconInputVar_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconInputVar.MouseMove
        lblTxtInputOutput.Text = "Calculations table Column name:"
        txtNodeInfo.Text = "Input Variable node." & vbCrLf & "Contains an input variable used in the calculations. The node name must be unique. The node text is the same as the the column in the Calculations table containing the corresponding input values. Enter the column name below the Description."
    End Sub

    Private Sub pbIconInputVar_Click(sender As Object, e As EventArgs) Handles pbIconInputVar.Click
        rbInputVar.Checked = True
    End Sub

    Private Sub rbInputVar_CheckedChanged(sender As Object, e As EventArgs) Handles rbInputVar.CheckedChanged
        If rbInputVar.Checked = True Then
            txtInputOutput.Enabled = False
            lblCmbInputOutput.Text = "Input column name:"
            cmbInputOutput.Enabled = True
            cmbInputOutput.Items.Clear()
            For Each item In MonteCarlo.DataInfo
                cmbInputOutput.Items.Add(item.Name)
            Next
        End If
    End Sub

    Private Sub pbIconUserDefInputVar_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconUserDefInputVar.MouseMove
        lblTxtInputOutput.Text = "Default value:"
        txtNodeInfo.Text = "User Defined Input Variable node." & vbCrLf & "Allows the user to define the node value before a Monte Carlo model is run. The value can be set on the Information tab. Enter a default value below the Description."
    End Sub

    Private Sub pbIconUserDefInputVar_Click(sender As Object, e As EventArgs) Handles pbIconUserDefInputVar.Click
        rbUserDefInputVar.Checked = True
    End Sub

    Private Sub rbUserDefInputVar_CheckedChanged(sender As Object, e As EventArgs) Handles rbUserDefInputVar.CheckedChanged
        If rbUserDefInputVar.Checked Then
            txtInputOutput.Enabled = True
            lblTxtInputOutput.Text = "Default value:"
            cmbInputOutput.Enabled = False
            'Else
            '    txtInputOutput.Enabled = False
        End If
    End Sub

    Private Sub pbIconOutputVal_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconOutputVal.MouseMove
        lblTxtInputOutput.Text = "Calculations table Column name:"
        txtNodeInfo.Text = "Output Variable node." & vbCrLf & "Contains a calculated output value that will be written to the Calculations table. The node name must be unique. The node text is the same as the column in the calculations table that the output value will be added to. Enter the column name below the Description."
    End Sub

    Private Sub pbIconOutputVal_Click(sender As Object, e As EventArgs) Handles pbIconOutputVal.Click
        rbOutputVal.Checked = True
    End Sub

    Private Sub rbOutputVal_CheckedChanged(sender As Object, e As EventArgs) Handles rbOutputVal.CheckedChanged
        If rbOutputVal.Checked Then
            lblTxtInputOutput.Text = "Output column name:"
            txtInputOutput.Enabled = True
            lblCmbInputOutput.Text = "Data type:"
            cmbInputOutput.Enabled = True
            cmbInputOutput.Items.Clear()
            cmbInputOutput.Items.Add("Single")
            cmbInputOutput.Items.Add("Double")
        End If
    End Sub

    Private Sub pbIconProcess_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconProcess.MouseMove
        txtNodeInfo.Text = "Calculation Process node." & vbCrLf & "The name of a calculation process."
    End Sub

    Private Sub pbIconProcess_Click(sender As Object, e As EventArgs) Handles pbIconProcess.Click
        rbProcess.Checked = True
    End Sub

    Private Sub rbProcess_CheckedChanged(sender As Object, e As EventArgs) Handles rbProcess.CheckedChanged
        If rbProcess.Checked Then cmbDataSource.Enabled = False
    End Sub

    Private Sub pbIconValProcess_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconValProcess.MouseMove
        txtNodeInfo.Text = "Calculation Process value node." & vbCrLf & "The name of a calculation process. The node contains the value produced by the calculation."
    End Sub

    Private Sub pbIconValProcess_Click(sender As Object, e As EventArgs) Handles pbIconValProcess.Click
        rbValProcess.Checked = True
    End Sub

    Private Sub rbValProcess_CheckedChanged(sender As Object, e As EventArgs) Handles rbValProcess.CheckedChanged
        If rbValProcess.Checked Then cmbDataSource.Enabled = False
    End Sub

    Private Sub pbIconCollection_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconCollection.MouseMove
        txtNodeInfo.Text = "Collection node." & vbCrLf & "Contains a collection of other nodes."
    End Sub

    Private Sub pbIconCollection_Click(sender As Object, e As EventArgs) Handles pbIconCollection.Click
        rbCollection.Checked = True
    End Sub

    Private Sub rbCollection_CheckedChanged(sender As Object, e As EventArgs) Handles rbCollection.CheckedChanged
        If rbCollection.Checked Then cmbDataSource.Enabled = False
    End Sub

    Private Sub pbIconInputVarCopy_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconValueCopy.MouseMove
        'txtNodeInfo.Text = "Copy of Input Variable node." & vbCrLf & "Contains a copy of the value of an Input Variable. The node name must be unique. The node text is the same as the name of the node whose value is copied."
        txtNodeInfo.Text = "Value Copy node." & vbCrLf & "Contains a copy of the value of an input variable, output variable or any other value node. The node name must be unique. The node text is the same as the name of the node whose value is copied."
    End Sub

    Private Sub pbIconInputVarCopy_Click(sender As Object, e As EventArgs) Handles pbIconValueCopy.Click
        rbValueCopy.Checked = True
    End Sub

    Private Sub rbValueCopy_CheckedChanged(sender As Object, e As EventArgs) Handles rbValueCopy.CheckedChanged
        If rbValueCopy.Checked Then
            cmbDataSource.Enabled = True
            cmbDataSource.Items.Clear()
            For Each item In ScalarData
                cmbDataSource.Items.Add(item.Key)
            Next
            'Else
            '    cmbDataSource.Enabled = False
        End If
    End Sub

    Private Sub pbIconConstE_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconConstE.MouseMove
        txtNodeInfo.Text = "Constant e value node." & vbCrLf & "Contains the constant value of e."
    End Sub

    Private Sub pbIconConstE_Click(sender As Object, e As EventArgs) Handles pbIconConstE.Click
        rbConstE.Checked = True
    End Sub

    Private Sub rbConstE_CheckedChanged(sender As Object, e As EventArgs) Handles rbConstE.CheckedChanged
        If rbConstE.Checked Then txtConstVal.Enabled = False
    End Sub


    Private Sub pbIconConstI_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconConstI.MouseMove
        txtNodeInfo.Text = "Constant i value node." & vbCrLf & "Contains the constant value of i. (Not implemented in this version of the Calculation Tree.)"
    End Sub

    Private Sub pbIconConstI_Click(sender As Object, e As EventArgs) Handles pbIconConstI.Click
        rbConstI.Checked = True
    End Sub

    Private Sub pbIconConstPi_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconConstPi.MouseMove
        txtNodeInfo.Text = "Constant Pi value node." & vbCrLf & "Contains the constant value of Pi."
    End Sub

    Private Sub pbIconConstPi_Click(sender As Object, e As EventArgs) Handles pbIconConstPi.Click
        rbConstPi.Checked = True
    End Sub

    Private Sub rbConstPi_CheckedChanged(sender As Object, e As EventArgs) Handles rbConstPi.CheckedChanged
        If rbConstPi.Checked Then txtConstVal.Enabled = False
    End Sub

    Private Sub pbIconConstUserDef_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconConstUserDef.MouseMove
        txtNodeInfo.Text = "User Defined Constant value node." & vbCrLf & "A constant value defined by the user. The node name must be unique. The node text is the constant value."
    End Sub

    Private Sub pbIconConstUserDef_Click(sender As Object, e As EventArgs) Handles pbIconConstUserDef.Click
        rbConstUserDef.Checked = True
    End Sub

    Private Sub rbConstUserDef_CheckedChanged(sender As Object, e As EventArgs) Handles rbConstUserDef.CheckedChanged
        If rbConstUserDef.Checked Then txtConstVal.Enabled = True
    End Sub

    Private Sub pbIconAdd_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconAdd.MouseMove
        txtNodeInfo.Text = "Add two values node." & vbCrLf
    End Sub

    Private Sub pbIconAdd_Click(sender As Object, e As EventArgs) Handles pbIconAdd.Click
        rbAdd.Checked = True
    End Sub

    Private Sub pbIconSubtract_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconSubtract.MouseMove
        txtNodeInfo.Text = "Subtract a value node." & vbCrLf
    End Sub

    Private Sub pbIconSubtract_Click(sender As Object, e As EventArgs) Handles pbIconSubtract.Click
        rbSubtract.Checked = True
    End Sub

    Private Sub pbIconMultiply_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconMultiply.MouseMove
        txtNodeInfo.Text = "Multiply two values node." & vbCrLf
    End Sub

    Private Sub pbIconMultiply_Click(sender As Object, e As EventArgs) Handles pbIconMultiply.Click
        rbMultiply.Checked = True
    End Sub

    Private Sub pbIconDivide_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconDivide.MouseMove
        txtNodeInfo.Text = "Divide by a value node." & vbCrLf
    End Sub

    Private Sub pbIconDivide_Click(sender As Object, e As EventArgs) Handles pbIconDivide.Click
        rbDivide.Checked = True
    End Sub

    Private Sub pbIconSum_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconSum.MouseMove
        txtNodeInfo.Text = "Sum values node." & vbCrLf
    End Sub

    Private Sub pbIconSum_Click(sender As Object, e As EventArgs) Handles pbIconSum.Click
        rbSum.Checked = True
    End Sub

    Private Sub pbIconProduct_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconProduct.MouseMove
        txtNodeInfo.Text = "Product of values node." & vbCrLf
    End Sub

    Private Sub pbIconProduct_Click(sender As Object, e As EventArgs) Handles pbIconProduct.Click
        rbProduct.Checked = True
    End Sub

    Private Sub pbIconSin_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconSin.MouseMove
        txtNodeInfo.Text = "Sine of value node." & vbCrLf
    End Sub

    Private Sub pbIconSin_Click(sender As Object, e As EventArgs) Handles pbIconSin.Click
        rbSin.Checked = True
    End Sub

    Private Sub pbIconCos_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconCos.MouseMove
        txtNodeInfo.Text = "Cosine of value node." & vbCrLf
    End Sub

    Private Sub pbIconCos_Click(sender As Object, e As EventArgs) Handles pbIconCos.Click
        rbCos.Checked = True
    End Sub

    Private Sub pbIconTan_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconTan.MouseMove
        txtNodeInfo.Text = "Tangent of value node." & vbCrLf
    End Sub

    Private Sub pbIconTan_Click(sender As Object, e As EventArgs) Handles pbIconTan.Click
        rbTan.Checked = True
    End Sub

    Private Sub pbIconArcSin_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconArcSin.MouseMove
        txtNodeInfo.Text = "ArcSine of value node." & vbCrLf
    End Sub

    Private Sub pbIconArcSin_Click(sender As Object, e As EventArgs) Handles pbIconArcSin.Click
        rbArcSin.Checked = True
    End Sub

    Private Sub pbIconArcCos_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconArcCos.MouseMove
        txtNodeInfo.Text = "ArcCosine of value node." & vbCrLf
    End Sub

    Private Sub pbIconArcCos_Click(sender As Object, e As EventArgs) Handles pbIconArcCos.Click
        rbArcCos.Checked = True
    End Sub

    Private Sub pbIconArcTan_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconArcTan.MouseMove
        txtNodeInfo.Text = "ArcTangent of value node." & vbCrLf
    End Sub

    Private Sub pbIconArcTan_Click(sender As Object, e As EventArgs) Handles pbIconArcTan.Click
        rbArcTan.Checked = True
    End Sub

    Private Sub pbIconDegToRad_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconDegToRad.MouseMove
        txtNodeInfo.Text = "Convert degrees to radians node." & vbCrLf
    End Sub

    Private Sub pbIconDegToRad_Click(sender As Object, e As EventArgs) Handles pbIconDegToRad.Click
        rbDegToRad.Checked = True
    End Sub

    Private Sub pbIconRadToDeg_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconRadToDeg.MouseMove
        txtNodeInfo.Text = "Convert radians to degrees node." & vbCrLf
    End Sub

    Private Sub pbIconRadToDeg_Click(sender As Object, e As EventArgs) Handles pbIconRadToDeg.Click
        rbRadToDeg.Checked = True
    End Sub

    Private Sub pbIconEPower_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconEPower.MouseMove
        txtNodeInfo.Text = "Power of e node." & vbCrLf
    End Sub

    Private Sub pbIconEPower_Click(sender As Object, e As EventArgs) Handles pbIconEPower.Click
        rbEPower.Checked = True
    End Sub

    Private Sub pbIconLn_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconLn.MouseMove
        txtNodeInfo.Text = "Natural logarithm node." & vbCrLf
    End Sub

    Private Sub pbIconLn_Click(sender As Object, e As EventArgs) Handles pbIconLn.Click
        rbLn.Checked = True
    End Sub

    Private Sub pbIconTenPower_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconTenPower.MouseMove
        txtNodeInfo.Text = "Power of 10 node." & vbCrLf
    End Sub

    Private Sub pbIconTenPower_Click(sender As Object, e As EventArgs) Handles pbIconTenPower.Click
        rbTenPower.Checked = True
    End Sub

    Private Sub pbIconLog_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconLog.MouseMove
        txtNodeInfo.Text = "Logarithm node." & vbCrLf
    End Sub

    Private Sub pbIconLog_Click(sender As Object, e As EventArgs) Handles pbIconLog.Click
        rbLog.Checked = True
    End Sub

    Private Sub pbIconSquare_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconSquare.MouseMove
        txtNodeInfo.Text = "Value squared node." & vbCrLf
    End Sub

    Private Sub pbIconSquare_Click(sender As Object, e As EventArgs) Handles pbIconSquare.Click
        rbSquare.Checked = True
    End Sub

    Private Sub pbIconSquareRoot_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconSquareRoot.MouseMove
        txtNodeInfo.Text = "Square root of value node." & vbCrLf
    End Sub

    Private Sub pbIconSquareRoot_Click(sender As Object, e As EventArgs) Handles pbIconSquareRoot.Click
        rbSquareRoot.Checked = True
    End Sub

    Private Sub pbIconCube_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconCube.MouseMove
        txtNodeInfo.Text = "Value cubed node." & vbCrLf
    End Sub

    Private Sub pbIconCube_Click(sender As Object, e As EventArgs) Handles pbIconCube.Click
        rbCube.Checked = True
    End Sub

    Private Sub pbIconCubeRoot_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconCubeRoot.MouseMove
        txtNodeInfo.Text = "Cube root of value node." & vbCrLf
    End Sub

    Private Sub pbIconCubeRoot_Click(sender As Object, e As EventArgs) Handles pbIconCubeRoot.Click
        rbCubeRoot.Checked = True
    End Sub

    Private Sub pbIconYthPower_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconYthPower.MouseMove
        txtNodeInfo.Text = "Yth power of a value node." & vbCrLf
    End Sub

    Private Sub pbIconYthPower_Click(sender As Object, e As EventArgs) Handles pbIconYthPower.Click
        rbYthPower.Checked = True
    End Sub

    'Private Sub rbYthRoot_MouseMove(sender As Object, e As MouseEventArgs) Handles rbYthRoot.MouseMove
    '    txtNodeInfo.Text = "Yth root of a value node." & vbCrLf
    'End Sub
    Private Sub pbIconYthRoot_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconYthRoot.MouseMove
        txtNodeInfo.Text = "Yth root of a value node." & vbCrLf
    End Sub

    Private Sub rbYthRoot_Click(sender As Object, e As EventArgs) Handles rbYthRoot.Click
        rbYthRoot.Checked = True
    End Sub

    'Private Sub rbNegate_MouseMove(sender As Object, e As MouseEventArgs) Handles rbNegate.MouseMove
    '    txtNodeInfo.Text = "Negate value node." & vbCrLf
    'End Sub
    Private Sub pbIconNegate_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconNegate.MouseMove
        txtNodeInfo.Text = "Negate value node." & vbCrLf
    End Sub

    Private Sub rbNegate_Click(sender As Object, e As EventArgs) Handles rbNegate.Click
        rbNegate.Checked = True
    End Sub

    'Private Sub rbInverse_MouseMove(sender As Object, e As MouseEventArgs) Handles rbInverse.MouseMove
    '    txtNodeInfo.Text = "Inverse value node." & vbCrLf
    'End Sub
    Private Sub pbIconInvert_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconInvert.MouseMove
        txtNodeInfo.Text = "Invert value node." & vbCrLf
    End Sub

    Private Sub rbInvert_Click(sender As Object, e As EventArgs) Handles rbInvert.Click
        rbInvert.Checked = True
    End Sub

    'Private Sub rbAbsoluteVal_MouseMove(sender As Object, e As MouseEventArgs) Handles rbAbsoluteVal.MouseMove
    '    txtNodeInfo.Text = "Absolute value node." & vbCrLf
    'End Sub
    Private Sub pbIconAbsoluteVal_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconAbsoluteVal.MouseMove
        txtNodeInfo.Text = "Absolute value node." & vbCrLf
    End Sub

    Private Sub rbAbsoluteVal_Click(sender As Object, e As EventArgs) Handles rbAbsoluteVal.Click
        rbAbsoluteVal.Checked = True
    End Sub

    'Private Sub rbRound_MouseMove(sender As Object, e As MouseEventArgs) Handles rbRound.MouseMove
    '    txtNodeInfo.Text = "Round value node." & vbCrLf
    'End Sub
    Private Sub pbIconRound_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconRound.MouseMove
        txtNodeInfo.Text = "Round value node." & vbCrLf
    End Sub

    Private Sub rbRound_Click(sender As Object, e As EventArgs) Handles rbRound.Click
        rbRound.Checked = True
    End Sub

    'Private Sub rbRoundUp_MouseMove(sender As Object, e As MouseEventArgs) Handles rbRoundUp.MouseMove
    '    txtNodeInfo.Text = "Round up value node." & vbCrLf
    'End Sub
    Private Sub pbIconRoundUp_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconRoundUp.MouseMove
        txtNodeInfo.Text = "Round up value node." & vbCrLf
    End Sub

    Private Sub rbRoundUp_Click(sender As Object, e As EventArgs) Handles rbRoundUp.Click
        rbRoundUp.Checked = True
    End Sub

    'Private Sub rbRoundDown_MouseMove(sender As Object, e As MouseEventArgs) Handles rbRoundDown.MouseMove

    'End Sub
    Private Sub pbIconRoundDown_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconRoundDown.MouseMove
        txtNodeInfo.Text = "Round down value node." & vbCrLf
    End Sub

    Private Sub rbRoundDown_Click(sender As Object, e As EventArgs) Handles rbRoundDown.Click
        rbRoundDown.Checked = True
    End Sub

    Private Sub pbIconIfLt_Click(sender As Object, e As EventArgs) Handles pbIconIfLt.Click

    End Sub

    Private Sub pbIconIfGt_Click(sender As Object, e As EventArgs) Handles pbIconIfGt.Click

    End Sub

    Private Sub pbIconIfGt_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconIfGt.MouseMove
        txtNodeInfo.Text = "Branch statement: If A > B Then C Else D" & vbCrLf & "where A, B, C & D are the four child nodes." & vbCrLf
    End Sub

    Private Sub pbIconIfGtEq_Click(sender As Object, e As EventArgs) Handles pbIconIfGtEq.Click

    End Sub

    Private Sub pbIconIfGtEq_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconIfGtEq.MouseMove
        txtNodeInfo.Text = "Branch statement: If A >= B Then C Else D" & vbCrLf & "where A, B, C & D are the four child nodes." & vbCrLf
    End Sub

    Private Sub pbIconIfEq_Click(sender As Object, e As EventArgs) Handles pbIconIfEq.Click

    End Sub

    Private Sub pbIconIfEq_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconIfEq.MouseMove
        txtNodeInfo.Text = "Branch statement: If A = B Then C Else D" & vbCrLf & "where A, B, C & D are the four child nodes." & vbCrLf
    End Sub

    Private Sub pbIconIfLtEq_Click(sender As Object, e As EventArgs) Handles pbIconIfLtEq.Click

    End Sub

    Private Sub pbIconIfLtEq_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconIfLtEq.MouseMove
        txtNodeInfo.Text = "Branch statement: If A <= B Then C Else D" & vbCrLf & "where A, B, C & D are the four child nodes." & vbCrLf
    End Sub

    Private Sub pbIconIfLt_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconIfLt.MouseMove
        txtNodeInfo.Text = "Branch statement: If A < B Then C Else D" & vbCrLf & "where A, B, C & D are the four child nodes." & vbCrLf
    End Sub



    Private Sub rbIfGt_CheckedChanged(sender As Object, e As EventArgs) Handles rbIfGt.CheckedChanged

    End Sub

    Private Sub rbIfGt_Click(sender As Object, e As EventArgs) Handles rbIfGt.Click
        rbIfGt.Checked = True
    End Sub

    Private Sub rbIfGtEq_CheckedChanged(sender As Object, e As EventArgs) Handles rbIfGtEq.CheckedChanged

    End Sub

    Private Sub rbIfGtEq_Click(sender As Object, e As EventArgs) Handles rbIfGtEq.Click
        rbIfGtEq.Checked = True
    End Sub

    Private Sub rbIfEq_CheckedChanged(sender As Object, e As EventArgs) Handles rbIfEq.CheckedChanged

    End Sub

    Private Sub rbIfEq_Click(sender As Object, e As EventArgs) Handles rbIfEq.Click
        rbIfEq.Checked = True
    End Sub

    Private Sub rbIfLtEq_CheckedChanged(sender As Object, e As EventArgs) Handles rbIfLtEq.CheckedChanged

    End Sub

    Private Sub rbIfLtEq_Click(sender As Object, e As EventArgs) Handles rbIfLtEq.Click
        rbIfLtEq.Checked = True
    End Sub

    Private Sub rbIfLt_CheckedChanged(sender As Object, e As EventArgs) Handles rbIfLt.CheckedChanged

    End Sub

    Private Sub rbIfLt_Click(sender As Object, e As EventArgs) Handles rbIfLt.Click
        rbIfLt.Checked = True
    End Sub



    Private Sub btnInsertData_Click(sender As Object, e As EventArgs) Handles btnInsertData.Click
        'Insert an Input or Output data node:

        'Dim DataName As String = txtDataName.Text.Trim
        'Dim DataDescr As String = txtDataDescr.Text.Trim
        Dim DataName As String = txtNodeName.Text.Trim
        Dim DataDescr As String = txtNodeDescr.Text.Trim
        Dim DataUnits As String = txtNodeUnits.Text.Trim
        Dim DataUnitsAbbrev As String = txtNodeUnitsAbbrev.Text.Trim

        If DataName = "" Then
            Message.AddWarning("Please enter a name for the Data node." & vbCrLf)
        Else
            If CalcInfo.ContainsKey(DataName) Then
                Message.AddWarning("The node name is already used: " & DataName & vbCrLf)
            Else
                CalcSeqModified = True
                If rbInputVar.Checked Then 'Insert an Input Variable node.
                    CalcInfo.Add(DataName, New CalcOpInfo)
                    CalcInfo(DataName).Units = DataUnits
                    CalcInfo(DataName).UnitsAbbrev = DataUnitsAbbrev
                    CalcInfo(DataName).Description = DataDescr
                    CalcInfo(DataName).Type = "Input Variable"
                    ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(DataName, DataName, 0, 1)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, DataName, DataName, 0, 1) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbUserDefInputVar.Checked Then 'Insert a User Defined Input Variable node.
                    CalcInfo.Add(DataName, New CalcOpInfo)
                    CalcInfo(DataName).Units = DataUnits
                    CalcInfo(DataName).UnitsAbbrev = DataUnitsAbbrev
                    CalcInfo(DataName).Description = DataDescr
                    CalcInfo(DataName).Type = "Input Variable User Defined"
                    ScalarData.Add(DataName, txtInputOutput.Text) 'Set default value. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(DataName, DataName, 2, 3)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, DataName, DataName, 2, 3) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbOutputVal.Checked Then 'Insert an Output Value node.
                    If txtInputOutput.Text.Trim = "" Then
                        Message.AddWarning("Please enter a name for the Column to contain the output data." & vbCrLf)
                    Else
                        If cmbInputOutput.SelectedIndex = -1 Then
                            Message.AddWarning("Please select a data type for the output data." & vbCrLf)
                        Else
                            ColumnInfo.Add(DataName, New DataColumnInfo)
                            ColumnInfo(DataName).Name = txtInputOutput.Text.Trim
                            ColumnInfo(DataName).Type = cmbInputOutput.SelectedItem.ToString
                            CalcInfo.Add(DataName, New CalcOpInfo)
                            CalcInfo(DataName).Units = DataUnits
                            CalcInfo(DataName).UnitsAbbrev = DataUnitsAbbrev
                            CalcInfo(DataName).Description = DataDescr
                            CalcInfo(DataName).Type = "Output Value"
                            ScalarData.Add(DataName, 1)  'This will be overwritten when the sequence is run. The value will be written to the Calculations table.
                            If trvCalculations.SelectedNode Is Nothing Then
                                trvCalculations.Nodes.Add(DataName, DataName, 4, 5)
                            Else
                                Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                                Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                                Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, DataName, DataName, 4, 5) 'Add the new node the the parent of the selected node at the same index position.
                                trvCalculations.SelectedNode.Remove()
                                NewNode.Nodes.Add(SelNode)
                                trvCalculations.SelectedNode = NewNode
                            End If
                        End If
                    End If

                Else
                    Message.AddWarning("No node type has been selected." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnReplaceData_Click(sender As Object, e As EventArgs) Handles btnReplaceData.Click
        'Replace selected node with an Input or Output data node:

        'Dim DataName As String = txtDataName.Text.Trim
        'Dim DataDescr As String = txtDataDescr.Text.Trim
        Dim DataName As String = txtNodeName.Text.Trim
        Dim DataDescr As String = txtNodeDescr.Text.Trim
        Dim DataUnits As String = txtNodeUnits.Text.Trim
        Dim DataUnitsAbbrev As String = txtNodeUnitsAbbrev.Text.Trim


        If DataName = "" Then
            Message.AddWarning("Please enter a name for the Data node." & vbCrLf)
        Else
            If trvCalculations.SelectedNode Is Nothing Then
                Message.AddWarning("Please select a node to replace with a Data node." & vbCrLf)
            Else
                Dim OldDataName As String = trvCalculations.SelectedNode.Name
                If CalcInfo(OldDataName).CopyList.Count = 0 Then
                    If CalcInfo.ContainsKey(DataName) And (DataName <> OldDataName) Then 'If DataName = OldDataName the node name will be reused when the Node is replaced
                        Message.AddWarning("The node name is already used: " & DataName & vbCrLf)
                    Else
                        CalcSeqModified = True
                        If rbInputVar.Checked Then 'Replace selected node with an Input Variable node.
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(DataName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(DataName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(DataName).Units = DataUnits
                            CalcInfo(DataName).UnitsAbbrev = DataUnitsAbbrev
                            CalcInfo(DataName).Description = DataDescr
                            CalcInfo(DataName).Type = "Input Variable"
                            ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            trvCalculations.SelectedNode.Name = DataName
                            trvCalculations.SelectedNode.Text = DataName
                            trvCalculations.SelectedNode.ImageIndex = 0
                            trvCalculations.SelectedNode.SelectedImageIndex = 1

                        ElseIf rbUserDefInputVar.Checked Then 'Replace selected node with a User Defined Input Variable node.
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(DataName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(DataName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(DataName).Units = DataUnits
                            CalcInfo(DataName).UnitsAbbrev = DataUnitsAbbrev
                            CalcInfo(DataName).Description = DataDescr
                            CalcInfo(DataName).Type = "Input Variable User Defined"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(DataName, txtInputOutput.Text) 'Set default value. 
                            trvCalculations.SelectedNode.Name = DataName
                            trvCalculations.SelectedNode.Text = DataName
                            trvCalculations.SelectedNode.ImageIndex = 0
                            trvCalculations.SelectedNode.SelectedImageIndex = 1

                        ElseIf rbOutputVal.Checked Then 'Replace selected node with an Output Value node.
                            If txtInputOutput.Text.Trim = "" Then
                                Message.AddWarning("Please enter a name for the Column to contain the output data." & vbCrLf)
                            Else
                                If cmbInputOutput.SelectedIndex = -1 Then
                                    Message.AddWarning("Please select a data type for the output data." & vbCrLf)
                                Else
                                    If ScalarData.ContainsKey(OldDataName) Then
                                        ScalarData.Remove(OldDataName) 'Remove the old data
                                    End If
                                    'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                                    If CalcInfo.ContainsKey(DataName) Then
                                        'The existing entry is being re-used
                                    Else
                                        CalcInfo.Add(DataName, New CalcOpInfo)
                                        CalcInfo.Remove(OldDataName) 'Remove the old entry
                                    End If

                                    If ColumnInfo.ContainsKey(DataName) Then ColumnInfo.Remove(DataName)

                                    ColumnInfo.Add(DataName, New DataColumnInfo)
                                    ColumnInfo(DataName).Name = txtInputOutput.Text.Trim
                                    ColumnInfo(DataName).Type = cmbInputOutput.SelectedItem.ToString
                                    CalcInfo(DataName).Units = DataUnits
                                    CalcInfo(DataName).UnitsAbbrev = DataUnitsAbbrev
                                    CalcInfo(DataName).Description = DataDescr
                                    CalcInfo(DataName).Type = "Output Value"
                                    ScalarData.Add(DataName, 1) 'This will be overwritten when the sequence is run. The value will be written to the Calculations table.
                                    trvCalculations.SelectedNode.Name = DataName
                                    trvCalculations.SelectedNode.Text = DataName
                                    trvCalculations.SelectedNode.ImageIndex = 0
                                    trvCalculations.SelectedNode.SelectedImageIndex = 1
                                End If
                            End If

                        Else
                            Message.AddWarning("No node type has been selected." & vbCrLf)
                        End If
                    End If
                Else
                    Message.AddWarning("This node data is copied in other nodes. Remove the copies before replacing this node." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnAppendData_Click(sender As Object, e As EventArgs) Handles btnAppendData.Click
        'Append an Input or Output data node:

        'Dim DataName As String = txtDataName.Text.Trim
        'Dim DataDescr As String = txtDataDescr.Text.Trim
        Dim DataName As String = txtNodeName.Text.Trim
        Dim DataDescr As String = txtNodeDescr.Text.Trim
        Dim DataUnits As String = txtNodeUnits.Text.Trim
        Dim DataUnitsAbbrev As String = txtNodeUnitsAbbrev.Text.Trim


        If DataName = "" Then
            Message.AddWarning("Please enter a name for the Data node." & vbCrLf)
        Else
            If CalcInfo.ContainsKey(DataName) Then
                Message.AddWarning("The node name is already used: " & DataName & vbCrLf)
            Else
                If rbInputVar.Checked Then 'Append an Input Variable node.
                    If cmbInputOutput.SelectedIndex = -1 Then
                        Message.AddWarning("Please select the name of the  Column containing the input variable data." & vbCrLf)
                    Else
                        If ColumnInfo.ContainsKey(DataName) Then
                            Message.AddWarning("An item named " & DataName & " already exists. Please select another name." & vbCrLf)
                        Else
                            ColumnInfo.Add(DataName, New DataColumnInfo)
                            ColumnInfo(DataName).Name = cmbInputOutput.SelectedItem.ToString
                            ColumnInfo(DataName).Type = "" 'If DataName is not found in DataInfo, the Type remains as ""
                            For Each item In MonteCarlo.DataInfo
                                If item.Name = ColumnInfo(DataName).Name Then
                                    ColumnInfo(DataName).Type = item.DataType
                                    Exit For
                                End If
                            Next
                            CalcInfo.Add(DataName, New CalcOpInfo)
                            CalcInfo(DataName).Units = DataUnits
                            CalcInfo(DataName).UnitsAbbrev = DataUnitsAbbrev
                            CalcInfo(DataName).Description = DataDescr
                            CalcInfo(DataName).Type = "Input Variable"
                            If trvCalculations.SelectedNode Is Nothing Then
                                trvCalculations.Nodes.Add(DataName, DataName, 0, 1)
                            Else
                                trvCalculations.SelectedNode.Nodes.Add(DataName, DataName, 0, 1)
                            End If
                            ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                        End If
                    End If

                ElseIf rbUserDefInputVar.Checked Then 'Append a User Defined Input Variable node.
                    CalcInfo.Add(DataName, New CalcOpInfo)
                    CalcInfo(DataName).Units = DataUnits
                    CalcInfo(DataName).UnitsAbbrev = DataUnitsAbbrev
                    CalcInfo(DataName).Description = DataDescr
                    CalcInfo(DataName).Type = "Input Variable User Defined"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(DataName, DataName, 2, 3)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(DataName, DataName, 2, 3)
                    End If
                    ScalarData.Add(DataName, txtInputOutput.Text) 'Set default value. 

                ElseIf rbOutputVal.Checked Then 'Append an Output Value node.
                    If txtInputOutput.Text.Trim = "" Then
                        Message.AddWarning("Please enter a name for the Column to contain the output data." & vbCrLf)
                    Else
                        If cmbInputOutput.SelectedIndex = -1 Then
                            Message.AddWarning("Please select a data type for the output data." & vbCrLf)
                        Else
                            If ColumnInfo.ContainsKey(DataName) Then
                                Message.AddWarning("An item named " & DataName & " already exists. Please select another name." & vbCrLf)
                            Else
                                ColumnInfo.Add(DataName, New DataColumnInfo)
                                ColumnInfo(DataName).Name = txtInputOutput.Text.Trim
                                ColumnInfo(DataName).Type = cmbInputOutput.SelectedItem.ToString
                                CalcInfo.Add(DataName, New CalcOpInfo)
                                CalcInfo(DataName).Units = DataUnits
                                CalcInfo(DataName).UnitsAbbrev = DataUnitsAbbrev
                                CalcInfo(DataName).Description = DataDescr
                                CalcInfo(DataName).Type = "Output Value"
                                If trvCalculations.SelectedNode Is Nothing Then
                                    trvCalculations.Nodes.Add(DataName, DataName, 4, 5)
                                Else
                                    trvCalculations.SelectedNode.Nodes.Add(DataName, DataName, 4, 5)
                                End If
                                ScalarData.Add(DataName, 1) 'This will be overwritten when the sequence is run. The value will be written to the Calculations table.
                            End If
                        End If
                    End If

                Else
                    Message.AddWarning("No node type has been selected." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnInsertSpecialNode_Click(sender As Object, e As EventArgs) Handles btnInsertSpecialNode.Click
        'Insert a Special node:

        'Dim SpecNodeName As String = txtDataName.Text.Trim
        'Dim SpecNodeDescr As String = txtDataDescr.Text.Trim
        Dim SpecNodeName As String = txtNodeName.Text.Trim
        Dim SpecNodeDescr As String = txtNodeDescr.Text.Trim
        Dim SpecNodeUnits As String = txtNodeUnits.Text.Trim
        Dim SpecNodeUnitsAbbrev As String = txtNodeUnitsAbbrev.Text.Trim


        If SpecNodeName = "" Then
            Message.AddWarning("Please enter a name for the Special node." & vbCrLf)
        Else
            If CalcInfo.ContainsKey(SpecNodeName) Then
                Message.AddWarning("The node name is already used: " & SpecNodeName & vbCrLf)
            Else
                If rbProcess.Checked Then 'Insert a Process node.
                    CalcInfo.Add(SpecNodeName, New CalcOpInfo)
                    CalcInfo(SpecNodeName).Units = SpecNodeUnits
                    CalcInfo(SpecNodeName).UnitsAbbrev = SpecNodeUnitsAbbrev
                    CalcInfo(SpecNodeName).Description = SpecNodeDescr
                    CalcInfo(SpecNodeName).Type = "Process"
                    'ScalarData.Add(SpecNodeName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(SpecNodeName, SpecNodeName, 6, 7)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, SpecNodeName, SpecNodeName, 6, 7) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbValProcess.Checked Then 'Insert a Value Process node.
                    CalcInfo.Add(SpecNodeName, New CalcOpInfo)
                    CalcInfo(SpecNodeName).Units = SpecNodeUnits
                    CalcInfo(SpecNodeName).UnitsAbbrev = SpecNodeUnitsAbbrev
                    CalcInfo(SpecNodeName).Description = SpecNodeDescr
                    CalcInfo(SpecNodeName).Type = "Value Process"
                    ScalarData.Add(SpecNodeName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(SpecNodeName, SpecNodeName, 8, 9)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, SpecNodeName, SpecNodeName, 8, 9) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbCollection.Checked Then 'Insert a Collection node.
                    CalcInfo.Add(SpecNodeName, New CalcOpInfo)
                    CalcInfo(SpecNodeName).Units = SpecNodeUnits
                    CalcInfo(SpecNodeName).UnitsAbbrev = SpecNodeUnitsAbbrev
                    CalcInfo(SpecNodeName).Description = SpecNodeDescr
                    CalcInfo(SpecNodeName).Type = "Collection"
                    'ScalarData.Add(SpecNodeName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(SpecNodeName, SpecNodeName, 10, 11)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, SpecNodeName, SpecNodeName, 10, 11) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbValueCopy.Checked Then 'Insert a Value Copy node.
                    If cmbDataSource.SelectedIndex = -1 Then
                        Message.AddWarning("Select a data source." & vbCrLf)
                    Else
                        CalcInfo.Add(SpecNodeName, New CalcOpInfo)
                        CalcInfo(SpecNodeName).Units = SpecNodeUnits
                        CalcInfo(SpecNodeName).UnitsAbbrev = SpecNodeUnitsAbbrev
                        CalcInfo(SpecNodeName).Description = SpecNodeDescr
                        CalcInfo(SpecNodeName).Type = "Value Copy"
                        'ScalarData.Add(SpecNodeName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                        Dim CopiedValueName As String = cmbDataSource.SelectedItem.ToString
                        If trvCalculations.SelectedNode Is Nothing Then
                            trvCalculations.Nodes.Add(SpecNodeName, CopiedValueName, 12, 13)
                        Else
                            Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                            Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                            Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, SpecNodeName, CopiedValueName, 12, 13) 'Add the new node the the parent of the selected node at the same index position.
                            trvCalculations.SelectedNode.Remove()
                            NewNode.Nodes.Add(SelNode)
                            trvCalculations.SelectedNode = NewNode
                        End If
                        CalcInfo(CopiedValueName).CopyList.Add(SpecNodeName)
                    End If
                Else
                    Message.AddWarning("No node type has been selected." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnReplaceSpecialNode_Click(sender As Object, e As EventArgs) Handles btnReplaceSpecialNode.Click
        'Replace selected node with a Special node:

        'Dim SpecNodeName As String = txtSpecialNodeName.Text.Trim
        'Dim SpecNodeDescr As String = txtSpecialNodeDescr.Text.Trim
        Dim SpecNodeName As String = txtNodeName.Text.Trim
        Dim SpecNodeDescr As String = txtNodeDescr.Text.Trim
        Dim SpecNodeUnits As String = txtNodeUnits.Text.Trim
        Dim SpecNodeUnitsAbbrev As String = txtNodeUnitsAbbrev.Text.Trim

        If SpecNodeName = "" Then
            Message.AddWarning("Please enter a name for the Special node." & vbCrLf)
        Else
            If trvCalculations.SelectedNode Is Nothing Then
                Message.AddWarning("Please select a node to replace with a Special node." & vbCrLf)
            Else
                Dim OldDataName As String = trvCalculations.SelectedNode.Name
                If CalcInfo(OldDataName).CopyList.Count = 0 Then
                    If CalcInfo.ContainsKey(SpecNodeName) And (SpecNodeName <> OldDataName) Then 'If DataName = OldDataName the node name will be reused when the Node is replaced
                        Message.AddWarning("The node name is already used: " & SpecNodeName & vbCrLf)
                    Else
                        If rbProcess.Checked Then 'Replace selected node with a Process node.
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(SpecNodeName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(SpecNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(SpecNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(SpecNodeName).Units = SpecNodeUnits
                            CalcInfo(SpecNodeName).UnitsAbbrev = SpecNodeUnitsAbbrev
                            CalcInfo(SpecNodeName).Description = SpecNodeDescr
                            CalcInfo(SpecNodeName).Type = "Process"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            trvCalculations.SelectedNode.Name = SpecNodeName
                            trvCalculations.SelectedNode.Text = SpecNodeName
                            trvCalculations.SelectedNode.ImageIndex = 6
                            trvCalculations.SelectedNode.SelectedImageIndex = 7

                        ElseIf rbValProcess.Checked Then 'Replace selected node with a Value Process node.
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(SpecNodeName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(SpecNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(SpecNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(SpecNodeName).Units = SpecNodeUnits
                            CalcInfo(SpecNodeName).UnitsAbbrev = SpecNodeUnitsAbbrev
                            CalcInfo(SpecNodeName).Description = SpecNodeDescr
                            CalcInfo(SpecNodeName).Type = "Value Process"
                            ScalarData.Add(SpecNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = SpecNodeName
                            trvCalculations.SelectedNode.Text = SpecNodeName
                            trvCalculations.SelectedNode.ImageIndex = 8
                            trvCalculations.SelectedNode.SelectedImageIndex = 9

                        ElseIf rbCollection.Checked Then 'Replace selected node with a Collection node.
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(SpecNodeName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(SpecNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(SpecNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(SpecNodeName).Units = SpecNodeUnits
                            CalcInfo(SpecNodeName).UnitsAbbrev = SpecNodeUnitsAbbrev
                            CalcInfo(SpecNodeName).Description = SpecNodeDescr
                            CalcInfo(SpecNodeName).Type = "Collection"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            trvCalculations.SelectedNode.Name = SpecNodeName
                            trvCalculations.SelectedNode.Text = SpecNodeName
                            trvCalculations.SelectedNode.ImageIndex = 10
                            trvCalculations.SelectedNode.SelectedImageIndex = 11

                        ElseIf rbValueCopy.Checked Then 'Replace selected node with a Value Copy node.
                            If cmbDataSource.SelectedIndex = -1 Then
                                Message.AddWarning("Select a data source." & vbCrLf)
                            Else
                                If ScalarData.ContainsKey(OldDataName) Then
                                    ScalarData.Remove(OldDataName) 'Remove the old data
                                End If
                                'If CalcInfo.ContainsKey(SpecNodeName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                                If CalcInfo.ContainsKey(SpecNodeName) Then
                                    'The existing entry is being re-used
                                Else
                                    CalcInfo.Add(SpecNodeName, New CalcOpInfo)
                                    CalcInfo.Remove(OldDataName) 'Remove the old entry
                                End If
                                CalcInfo(SpecNodeName).Units = SpecNodeUnits
                                CalcInfo(SpecNodeName).UnitsAbbrev = SpecNodeUnitsAbbrev
                                CalcInfo(SpecNodeName).Description = SpecNodeDescr
                                CalcInfo(SpecNodeName).Type = "Value Copy"
                                Dim CopiedValueName As String = cmbDataSource.SelectedItem.ToString
                                ScalarData.Add(SpecNodeName, 1) 'This value will be overwritten when the sequence is run. 
                                trvCalculations.SelectedNode.Name = SpecNodeName
                                'trvCalculations.SelectedNode.Text = SpecNodeName
                                trvCalculations.SelectedNode.Text = CopiedValueName
                                trvCalculations.SelectedNode.ImageIndex = 12
                                trvCalculations.SelectedNode.SelectedImageIndex = 13
                            End If

                        Else
                            Message.AddWarning("No node type has been selected." & vbCrLf)
                        End If
                    End If
                Else
                    Message.AddWarning("This node data is copied in other nodes. Remove the copies before replacing this node." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnAppendSpecialNode_Click(sender As Object, e As EventArgs) Handles btnAppendSpecialNode.Click
        'Append a Special node:

        'Dim SpecNodeName As String = txtSpecialNodeName.Text.Trim
        'Dim SpecNodeDescr As String = txtSpecialNodeDescr.Text.Trim
        Dim SpecNodeName As String = txtNodeName.Text.Trim
        Dim SpecNodeDescr As String = txtNodeDescr.Text.Trim
        Dim SpecNodeUnits As String = txtNodeUnits.Text.Trim
        Dim SpecNodeUnitsAbbrev As String = txtNodeUnitsAbbrev.Text.Trim

        If SpecNodeName = "" Then
            Message.AddWarning("Please enter a name for the Special node." & vbCrLf)
        Else
            If CalcInfo.ContainsKey(SpecNodeName) Then
                Message.AddWarning("The node name is already used: " & SpecNodeName & vbCrLf)
            Else
                If rbProcess.Checked Then 'Append a Process node.
                    CalcInfo.Add(SpecNodeName, New CalcOpInfo)
                    CalcInfo(SpecNodeName).Units = SpecNodeUnits
                    CalcInfo(SpecNodeName).UnitsAbbrev = SpecNodeUnitsAbbrev
                    CalcInfo(SpecNodeName).Description = SpecNodeDescr
                    CalcInfo(SpecNodeName).Type = "Process"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(SpecNodeName, SpecNodeName, 6, 7)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(SpecNodeName, SpecNodeName, 6, 7)
                    End If

                ElseIf rbValProcess.Checked Then 'Append a Value Process node.
                    CalcInfo.Add(SpecNodeName, New CalcOpInfo)
                    CalcInfo(SpecNodeName).Units = SpecNodeUnits
                    CalcInfo(SpecNodeName).UnitsAbbrev = SpecNodeUnitsAbbrev
                    CalcInfo(SpecNodeName).Description = SpecNodeDescr
                    CalcInfo(SpecNodeName).Type = "Value Process"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(SpecNodeName, SpecNodeName, 8, 9)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(SpecNodeName, SpecNodeName, 8, 9)
                    End If
                    ScalarData.Add(SpecNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbCollection.Checked Then 'Append a Collection node.
                    CalcInfo.Add(SpecNodeName, New CalcOpInfo)
                    CalcInfo(SpecNodeName).Units = SpecNodeUnits
                    CalcInfo(SpecNodeName).UnitsAbbrev = SpecNodeUnitsAbbrev
                    CalcInfo(SpecNodeName).Description = SpecNodeDescr
                    CalcInfo(SpecNodeName).Type = "Collection"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(SpecNodeName, SpecNodeName, 10, 11)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(SpecNodeName, SpecNodeName, 10, 11)
                    End If

                ElseIf rbValueCopy.Checked Then 'Append a Value Copy node.
                    If cmbDataSource.SelectedIndex = -1 Then
                        Message.AddWarning("Select a data source." & vbCrLf)
                    Else
                        CalcInfo.Add(SpecNodeName, New CalcOpInfo)
                        CalcInfo(SpecNodeName).Units = SpecNodeUnits
                        CalcInfo(SpecNodeName).UnitsAbbrev = SpecNodeUnitsAbbrev
                        CalcInfo(SpecNodeName).Description = SpecNodeDescr
                        CalcInfo(SpecNodeName).Type = "Value Copy"
                        Dim CopiedValueName As String = cmbDataSource.SelectedItem.ToString
                        If trvCalculations.SelectedNode Is Nothing Then
                            trvCalculations.Nodes.Add(SpecNodeName, CopiedValueName, 12, 13)
                        Else
                            trvCalculations.SelectedNode.Nodes.Add(SpecNodeName, CopiedValueName, 12, 13)
                        End If
                        CalcInfo(CopiedValueName).CopyList.Add(SpecNodeName)
                        'ScalarData.Add(SpecNodeName, 1) 'Dont need this - use the value at CopiedValueName (?)
                    End If
                Else
                    Message.AddWarning("No node type has been selected." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnInsertConst_Click(sender As Object, e As EventArgs) Handles btnInsertConst.Click
        'Insert a Constant value node:

        'Dim ConstNodeName As String = txtConstName.Text.Trim
        'Dim ConstNodeDescr As String = txtConstDescr.Text.Trim
        Dim ConstNodeName As String = txtNodeName.Text.Trim
        Dim ConstNodeDescr As String = txtNodeDescr.Text.Trim
        Dim ConstNodeUnits As String = txtNodeUnits.Text.Trim
        Dim ConstNodeUnitsAbbrev As String = txtNodeUnitsAbbrev.Text.Trim


        If ConstNodeName = "" Then
            Message.AddWarning("Please enter a name for the Constant node." & vbCrLf)
        Else
            If CalcInfo.ContainsKey(ConstNodeName) Then
                Message.AddWarning("The node name is already used: " & ConstNodeName & vbCrLf)
            Else
                If rbConstE.Checked Then 'Insert a Constant e node
                    CalcInfo.Add(ConstNodeName, New CalcOpInfo)
                    CalcInfo(ConstNodeName).Units = ConstNodeUnits
                    CalcInfo(ConstNodeName).UnitsAbbrev = ConstNodeUnitsAbbrev
                    CalcInfo(ConstNodeName).Description = ConstNodeDescr
                    CalcInfo(ConstNodeName).Type = "Constant Value E"
                    ScalarData.Add(ConstNodeName, Math.E)
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(ConstNodeName, ConstNodeName, 14, 15)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, ConstNodeName, ConstNodeName, 14, 15) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbConstPi.Checked Then 'Insert a Constant Pi node
                    CalcInfo.Add(ConstNodeName, New CalcOpInfo)
                    CalcInfo(ConstNodeName).Units = ConstNodeUnits
                    CalcInfo(ConstNodeName).UnitsAbbrev = ConstNodeUnitsAbbrev
                    CalcInfo(ConstNodeName).Description = ConstNodeDescr
                    CalcInfo(ConstNodeName).Type = "Constant Value Pi"
                    ScalarData.Add(ConstNodeName, Math.E)
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(ConstNodeName, ConstNodeName, 18, 19)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, ConstNodeName, ConstNodeName, 18, 19) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbConstUserDef.Checked Then 'Insert a User Defined Constant node
                    CalcInfo.Add(ConstNodeName, New CalcOpInfo)
                    CalcInfo(ConstNodeName).Units = ConstNodeUnits
                    CalcInfo(ConstNodeName).UnitsAbbrev = ConstNodeUnitsAbbrev
                    CalcInfo(ConstNodeName).Description = ConstNodeDescr
                    CalcInfo(ConstNodeName).Type = "Constant Value User Defined"
                    Dim UserDefinedValue As Double = txtConstVal.Text
                    ScalarData.Add(ConstNodeName, UserDefinedValue)
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(ConstNodeName, ConstNodeName, 20, 21)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, ConstNodeName, ConstNodeName, 20, 21) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                Else
                    Message.AddWarning("No node type has been selected." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnAppendConst_Click(sender As Object, e As EventArgs) Handles btnAppendConst.Click
        'Append a Constant value node:

        'Dim ConstNodeName As String = txtConstName.Text.Trim
        'Dim ConstNodeDescr As String = txtConstDescr.Text.Trim
        Dim ConstNodeName As String = txtNodeName.Text.Trim
        Dim ConstNodeDescr As String = txtNodeDescr.Text.Trim
        Dim ConstNodeUnits As String = txtNodeUnits.Text.Trim
        Dim ConstNodeUnitsAbbrev As String = txtNodeUnitsAbbrev.Text.Trim

        If ConstNodeName = "" Then
            Message.AddWarning("Please enter a name for the Constant node." & vbCrLf)
        Else
            If CalcInfo.ContainsKey(ConstNodeName) Then
                Message.AddWarning("The node name is already used: " & ConstNodeName & vbCrLf)
            Else
                If rbConstE.Checked Then 'Append a Constant e node
                    CalcInfo.Add(ConstNodeName, New CalcOpInfo)
                    CalcInfo(ConstNodeName).Units = ConstNodeUnits
                    CalcInfo(ConstNodeName).UnitsAbbrev = ConstNodeUnitsAbbrev
                    CalcInfo(ConstNodeName).Description = ConstNodeDescr
                    CalcInfo(ConstNodeName).Type = "Constant Value E"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(ConstNodeName, ConstNodeName, 14, 15)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(ConstNodeName, ConstNodeName, 14, 15)
                    End If
                    ScalarData.Add(ConstNodeName, Math.E)

                ElseIf rbConstPi.Checked Then 'Append a Constant Pi node
                    CalcInfo.Add(ConstNodeName, New CalcOpInfo)
                    CalcInfo(ConstNodeName).Units = ConstNodeUnits
                    CalcInfo(ConstNodeName).UnitsAbbrev = ConstNodeUnitsAbbrev
                    CalcInfo(ConstNodeName).Description = ConstNodeDescr
                    CalcInfo(ConstNodeName).Type = "Constant Value Pi"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(ConstNodeName, ConstNodeName, 18, 19)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(ConstNodeName, ConstNodeName, 18, 19)
                    End If
                    ScalarData.Add(ConstNodeName, Math.PI)

                ElseIf rbConstUserDef.Checked Then 'Append a User Defined Constant node
                    CalcInfo.Add(ConstNodeName, New CalcOpInfo)
                    CalcInfo(ConstNodeName).Units = ConstNodeUnits
                    CalcInfo(ConstNodeName).UnitsAbbrev = ConstNodeUnitsAbbrev
                    CalcInfo(ConstNodeName).Description = ConstNodeDescr
                    CalcInfo(ConstNodeName).Type = "Constant Value User Defined"
                    Dim UserDefinedValue As Double = txtConstVal.Text
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(ConstNodeName, ConstNodeName, 20, 21)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(ConstNodeName, ConstNodeName, 20, 21)
                    End If
                    ScalarData.Add(ConstNodeName, UserDefinedValue)

                Else
                    Message.AddWarning("No node type has been selected." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnInsertArithNode_Click(sender As Object, e As EventArgs) Handles btnInsertArithNode.Click
        'Insert an Arithmetic node:

        'Dim ArithNodeName As String = txtArithNodeName.Text.Trim
        'Dim ArithNodeDescr As String = txtArithNodeDescr.Text.Trim
        Dim ArithNodeName As String = txtNodeName.Text.Trim
        Dim ArithNodeDescr As String = txtNodeDescr.Text.Trim
        Dim ArithNodeUnits As String = txtNodeUnits.Text.Trim
        Dim ArithNodeUnitsAbbrev As String = txtNodeUnitsAbbrev.Text.Trim


        If ArithNodeName = "" Then
            Message.AddWarning("Please enter a name for the Arithmetic node." & vbCrLf)
        Else
            If CalcInfo.ContainsKey(ArithNodeName) Then
                Message.AddWarning("The node name is already used: " & ArithNodeName & vbCrLf)
            Else
                If rbAdd.Checked Then
                    CalcInfo.Add(ArithNodeName, New CalcOpInfo)
                    CalcInfo(ArithNodeName).Units = ArithNodeUnits
                    CalcInfo(ArithNodeName).UnitsAbbrev = ArithNodeUnitsAbbrev
                    CalcInfo(ArithNodeName).Description = ArithNodeDescr
                    CalcInfo(ArithNodeName).Type = "Add"
                    ScalarData.Add(ArithNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(ArithNodeName, ArithNodeName, 22, 23)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, ArithNodeName, ArithNodeName, 22, 23) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbSubtract.Checked Then
                    CalcInfo.Add(ArithNodeName, New CalcOpInfo)
                    CalcInfo(ArithNodeName).Units = ArithNodeUnits
                    CalcInfo(ArithNodeName).UnitsAbbrev = ArithNodeUnitsAbbrev
                    CalcInfo(ArithNodeName).Description = ArithNodeDescr
                    CalcInfo(ArithNodeName).Type = "Subtract"
                    ScalarData.Add(ArithNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(ArithNodeName, ArithNodeName, 24, 25)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, ArithNodeName, ArithNodeName, 24, 25) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbMultiply.Checked Then
                    CalcInfo.Add(ArithNodeName, New CalcOpInfo)
                    CalcInfo(ArithNodeName).Units = ArithNodeUnits
                    CalcInfo(ArithNodeName).UnitsAbbrev = ArithNodeUnitsAbbrev
                    CalcInfo(ArithNodeName).Description = ArithNodeDescr
                    CalcInfo(ArithNodeName).Type = "Multiply"
                    ScalarData.Add(ArithNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(ArithNodeName, ArithNodeName, 26, 27)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, ArithNodeName, ArithNodeName, 26, 27) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbDivide.Checked Then
                    CalcInfo.Add(ArithNodeName, New CalcOpInfo)
                    CalcInfo(ArithNodeName).Units = ArithNodeUnits
                    CalcInfo(ArithNodeName).UnitsAbbrev = ArithNodeUnitsAbbrev
                    CalcInfo(ArithNodeName).Description = ArithNodeDescr
                    CalcInfo(ArithNodeName).Type = "Divide"
                    ScalarData.Add(ArithNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(ArithNodeName, ArithNodeName, 28, 29)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, ArithNodeName, ArithNodeName, 28, 29) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbSum.Checked Then
                    CalcInfo.Add(ArithNodeName, New CalcOpInfo)
                    CalcInfo(ArithNodeName).Units = ArithNodeUnits
                    CalcInfo(ArithNodeName).UnitsAbbrev = ArithNodeUnitsAbbrev
                    CalcInfo(ArithNodeName).Description = ArithNodeDescr
                    CalcInfo(ArithNodeName).Type = "Sum"
                    ScalarData.Add(ArithNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(ArithNodeName, ArithNodeName, 30, 31)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, ArithNodeName, ArithNodeName, 30, 31) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbProduct.Checked Then
                    CalcInfo.Add(ArithNodeName, New CalcOpInfo)
                    CalcInfo(ArithNodeName).Units = ArithNodeUnits
                    CalcInfo(ArithNodeName).UnitsAbbrev = ArithNodeUnitsAbbrev
                    CalcInfo(ArithNodeName).Description = ArithNodeDescr
                    CalcInfo(ArithNodeName).Type = "Product"
                    ScalarData.Add(ArithNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(ArithNodeName, ArithNodeName, 32, 33)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, ArithNodeName, ArithNodeName, 32, 33) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                Else
                    Message.AddWarning("No node type has been selected." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnReplaceArithNode_Click(sender As Object, e As EventArgs) Handles btnReplaceArithNode.Click
        'Replace selected node with an Arithmetic node:

        'Dim ArithNodeName As String = txtArithNodeName.Text.Trim
        'Dim ArithNodeDescr As String = txtArithNodeDescr.Text.Trim
        Dim ArithNodeName As String = txtNodeName.Text.Trim
        Dim ArithNodeDescr As String = txtNodeDescr.Text.Trim
        Dim ArithNodeUnits As String = txtNodeUnits.Text.Trim
        Dim ArithNodeUnitsAbbrev As String = txtNodeUnitsAbbrev.Text.Trim

        If ArithNodeName = "" Then
            Message.AddWarning("Please enter a name for the Arithmetic node." & vbCrLf)
        Else
            If trvCalculations.SelectedNode Is Nothing Then
                Message.AddWarning("Please select a node to replace with an Arithmetic node." & vbCrLf)
            Else
                Dim OldDataName As String = trvCalculations.SelectedNode.Name
                If CalcInfo(OldDataName).CopyList.Count = 0 Then
                    If CalcInfo.ContainsKey(ArithNodeName) And (ArithNodeName <> OldDataName) Then 'If DataName = OldDataName the node name will be reused when the Node is replaced
                        Message.AddWarning("The node name is already used: " & ArithNodeName & vbCrLf)
                    Else
                        If rbAdd.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(ArithNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(ArithNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(ArithNodeName).Units = ArithNodeUnits
                            CalcInfo(ArithNodeName).UnitsAbbrev = ArithNodeUnitsAbbrev
                            CalcInfo(ArithNodeName).Description = ArithNodeDescr
                            CalcInfo(ArithNodeName).Type = "Add"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(ArithNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = ArithNodeName
                            trvCalculations.SelectedNode.Text = ArithNodeName
                            trvCalculations.SelectedNode.ImageIndex = 22
                            trvCalculations.SelectedNode.SelectedImageIndex = 23

                        ElseIf rbSubtract.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(ArithNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(ArithNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(ArithNodeName).Units = ArithNodeUnits
                            CalcInfo(ArithNodeName).UnitsAbbrev = ArithNodeUnitsAbbrev
                            CalcInfo(ArithNodeName).Description = ArithNodeDescr
                            CalcInfo(ArithNodeName).Type = "Subtract"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(ArithNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = ArithNodeName
                            trvCalculations.SelectedNode.Text = ArithNodeName
                            trvCalculations.SelectedNode.ImageIndex = 24
                            trvCalculations.SelectedNode.SelectedImageIndex = 25

                        ElseIf rbMultiply.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(ArithNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(ArithNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(ArithNodeName).Units = ArithNodeUnits
                            CalcInfo(ArithNodeName).UnitsAbbrev = ArithNodeUnitsAbbrev
                            CalcInfo(ArithNodeName).Description = ArithNodeDescr
                            CalcInfo(ArithNodeName).Type = "Multiply"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(ArithNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = ArithNodeName
                            trvCalculations.SelectedNode.Text = ArithNodeName
                            trvCalculations.SelectedNode.ImageIndex = 26
                            trvCalculations.SelectedNode.SelectedImageIndex = 27

                        ElseIf rbDivide.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(ArithNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(ArithNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(ArithNodeName).Units = ArithNodeUnits
                            CalcInfo(ArithNodeName).UnitsAbbrev = ArithNodeUnitsAbbrev
                            CalcInfo(ArithNodeName).Description = ArithNodeDescr
                            CalcInfo(ArithNodeName).Type = "Divide"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(ArithNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = ArithNodeName
                            trvCalculations.SelectedNode.Text = ArithNodeName
                            trvCalculations.SelectedNode.ImageIndex = 28
                            trvCalculations.SelectedNode.SelectedImageIndex = 29

                        ElseIf rbSum.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(ArithNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(ArithNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(ArithNodeName).Units = ArithNodeUnits
                            CalcInfo(ArithNodeName).UnitsAbbrev = ArithNodeUnitsAbbrev
                            CalcInfo(ArithNodeName).Description = ArithNodeDescr
                            CalcInfo(ArithNodeName).Type = "Sum"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(ArithNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = ArithNodeName
                            trvCalculations.SelectedNode.Text = ArithNodeName
                            trvCalculations.SelectedNode.ImageIndex = 30
                            trvCalculations.SelectedNode.SelectedImageIndex = 31

                        ElseIf rbProduct.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(ArithNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(ArithNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(ArithNodeName).Units = ArithNodeUnits
                            CalcInfo(ArithNodeName).UnitsAbbrev = ArithNodeUnitsAbbrev
                            CalcInfo(ArithNodeName).Description = ArithNodeDescr
                            CalcInfo(ArithNodeName).Type = "Product"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(ArithNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = ArithNodeName
                            trvCalculations.SelectedNode.Text = ArithNodeName
                            trvCalculations.SelectedNode.ImageIndex = 32
                            trvCalculations.SelectedNode.SelectedImageIndex = 33

                        Else
                            Message.AddWarning("No node type has been selected." & vbCrLf)
                        End If
                    End If
                Else
                    Message.AddWarning("This node data is copied in other nodes. Remove the copies before replacing this node." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnAppendArithNode_Click(sender As Object, e As EventArgs) Handles btnAppendArithNode.Click
        'Append an Arithmetic node:

        'Dim ArithNodeName As String = txtArithNodeName.Text.Trim
        'Dim ArithNodeDescr As String = txtArithNodeDescr.Text.Trim
        Dim ArithNodeName As String = txtNodeName.Text.Trim
        Dim ArithNodeDescr As String = txtNodeDescr.Text.Trim
        Dim ArithNodeUnits As String = txtNodeUnits.Text.Trim
        Dim ArithNodeUnitsAbbrev As String = txtNodeUnitsAbbrev.Text.Trim

        If ArithNodeName = "" Then
            Message.AddWarning("Please enter a name for the Arithmetic node." & vbCrLf)
        Else
            If CalcInfo.ContainsKey(ArithNodeName) Then
                Message.AddWarning("The node name is already used: " & ArithNodeName & vbCrLf)
            Else
                If rbAdd.Checked Then
                    CalcInfo.Add(ArithNodeName, New CalcOpInfo)
                    CalcInfo(ArithNodeName).Units = ArithNodeUnits
                    CalcInfo(ArithNodeName).UnitsAbbrev = ArithNodeUnitsAbbrev
                    CalcInfo(ArithNodeName).Description = ArithNodeDescr
                    CalcInfo(ArithNodeName).Type = "Add"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(ArithNodeName, ArithNodeName, 22, 23)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(ArithNodeName, ArithNodeName, 22, 23)
                    End If
                    ScalarData.Add(ArithNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbSubtract.Checked Then
                    CalcInfo.Add(ArithNodeName, New CalcOpInfo)
                    CalcInfo(ArithNodeName).Units = ArithNodeUnits
                    CalcInfo(ArithNodeName).UnitsAbbrev = ArithNodeUnitsAbbrev
                    CalcInfo(ArithNodeName).Description = ArithNodeDescr
                    CalcInfo(ArithNodeName).Type = "Subtract"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(ArithNodeName, ArithNodeName, 24, 25)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(ArithNodeName, ArithNodeName, 24, 25)
                    End If
                    ScalarData.Add(ArithNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbMultiply.Checked Then
                    CalcInfo.Add(ArithNodeName, New CalcOpInfo)
                    CalcInfo(ArithNodeName).Description = ArithNodeDescr
                    CalcInfo(ArithNodeName).Type = "Multiply"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(ArithNodeName, ArithNodeName, 26, 27)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(ArithNodeName, ArithNodeName, 26, 27)
                    End If
                    ScalarData.Add(ArithNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbDivide.Checked Then
                    CalcInfo.Add(ArithNodeName, New CalcOpInfo)
                    CalcInfo(ArithNodeName).Units = ArithNodeUnits
                    CalcInfo(ArithNodeName).UnitsAbbrev = ArithNodeUnitsAbbrev
                    CalcInfo(ArithNodeName).Description = ArithNodeDescr
                    CalcInfo(ArithNodeName).Type = "Divide"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(ArithNodeName, ArithNodeName, 28, 29)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(ArithNodeName, ArithNodeName, 28, 29)
                    End If
                    ScalarData.Add(ArithNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbSum.Checked Then
                    CalcInfo.Add(ArithNodeName, New CalcOpInfo)
                    CalcInfo(ArithNodeName).Units = ArithNodeUnits
                    CalcInfo(ArithNodeName).UnitsAbbrev = ArithNodeUnitsAbbrev
                    CalcInfo(ArithNodeName).Description = ArithNodeDescr
                    CalcInfo(ArithNodeName).Type = "Sum"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(ArithNodeName, ArithNodeName, 30, 31)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(ArithNodeName, ArithNodeName, 30, 31)
                    End If
                    ScalarData.Add(ArithNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbProduct.Checked Then
                    CalcInfo.Add(ArithNodeName, New CalcOpInfo)
                    CalcInfo(ArithNodeName).Units = ArithNodeUnits
                    CalcInfo(ArithNodeName).UnitsAbbrev = ArithNodeUnitsAbbrev
                    CalcInfo(ArithNodeName).Description = ArithNodeDescr
                    CalcInfo(ArithNodeName).Type = "Product"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(ArithNodeName, ArithNodeName, 32, 33)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(ArithNodeName, ArithNodeName, 32, 33)
                    End If
                    ScalarData.Add(ArithNodeName, 1) 'This value will be overwritten when the sequence is run. 
                Else
                    Message.AddWarning("No node type has been selected." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnInsertTrigNode_Click(sender As Object, e As EventArgs) Handles btnInsertTrigNode.Click
        'Insert a Trigonometric node:

        'Dim TrigNodeName As String = txtTrigNodeName.Text.Trim
        'Dim TrigNodeDescr As String = txtTrigNodeDescr.Text.Trim
        Dim TrigNodeName As String = txtNodeName.Text.Trim
        Dim TrigNodeDescr As String = txtNodeDescr.Text.Trim
        Dim TrigNodeUnits As String = txtNodeUnits.Text.Trim
        Dim TrigNodeUnitsAbbrev As String = txtNodeUnitsAbbrev.Text.Trim


        If TrigNodeName = "" Then
            Message.AddWarning("Please enter a name for the Trigonometric node." & vbCrLf)
        Else
            If CalcInfo.ContainsKey(TrigNodeName) Then
                Message.AddWarning("The node name is already used: " & TrigNodeName & vbCrLf)
            Else
                If rbSin.Checked Then
                    CalcInfo.Add(TrigNodeName, New CalcOpInfo)
                    CalcInfo(TrigNodeName).Units = TrigNodeUnits
                    CalcInfo(TrigNodeName).UnitsAbbrev = TrigNodeUnitsAbbrev
                    CalcInfo(TrigNodeName).Description = TrigNodeDescr
                    CalcInfo(TrigNodeName).Type = "Sine"
                    ScalarData.Add(TrigNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(TrigNodeName, TrigNodeName, 34, 35)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, TrigNodeName, TrigNodeName, 34, 35) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbCos.Checked Then
                    CalcInfo.Add(TrigNodeName, New CalcOpInfo)
                    CalcInfo(TrigNodeName).Units = TrigNodeUnits
                    CalcInfo(TrigNodeName).UnitsAbbrev = TrigNodeUnitsAbbrev
                    CalcInfo(TrigNodeName).Description = TrigNodeDescr
                    CalcInfo(TrigNodeName).Type = "Cosine"
                    ScalarData.Add(TrigNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(TrigNodeName, TrigNodeName, 36, 37)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, TrigNodeName, TrigNodeName, 36, 37) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbTan.Checked Then
                    CalcInfo.Add(TrigNodeName, New CalcOpInfo)
                    CalcInfo(TrigNodeName).Units = TrigNodeUnits
                    CalcInfo(TrigNodeName).UnitsAbbrev = TrigNodeUnitsAbbrev
                    CalcInfo(TrigNodeName).Description = TrigNodeDescr
                    CalcInfo(TrigNodeName).Type = "Tangent"
                    ScalarData.Add(TrigNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(TrigNodeName, TrigNodeName, 38, 39)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, TrigNodeName, TrigNodeName, 38, 39) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbArcSin.Checked Then
                    CalcInfo.Add(TrigNodeName, New CalcOpInfo)
                    CalcInfo(TrigNodeName).Units = TrigNodeUnits
                    CalcInfo(TrigNodeName).UnitsAbbrev = TrigNodeUnitsAbbrev
                    CalcInfo(TrigNodeName).Description = TrigNodeDescr
                    CalcInfo(TrigNodeName).Type = "ArcSine"
                    ScalarData.Add(TrigNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(TrigNodeName, TrigNodeName, 40, 41)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, TrigNodeName, TrigNodeName, 40, 41) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbArcCos.Checked Then
                    CalcInfo.Add(TrigNodeName, New CalcOpInfo)
                    CalcInfo(TrigNodeName).Units = TrigNodeUnits
                    CalcInfo(TrigNodeName).UnitsAbbrev = TrigNodeUnitsAbbrev
                    CalcInfo(TrigNodeName).Description = TrigNodeDescr
                    CalcInfo(TrigNodeName).Type = "ArcCosine"
                    ScalarData.Add(TrigNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(TrigNodeName, TrigNodeName, 42, 43)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, TrigNodeName, TrigNodeName, 42, 43) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbArcTan.Checked Then
                    CalcInfo.Add(TrigNodeName, New CalcOpInfo)
                    CalcInfo(TrigNodeName).Units = TrigNodeUnits
                    CalcInfo(TrigNodeName).UnitsAbbrev = TrigNodeUnitsAbbrev
                    CalcInfo(TrigNodeName).Description = TrigNodeDescr
                    CalcInfo(TrigNodeName).Type = "ArcTangent"
                    ScalarData.Add(TrigNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(TrigNodeName, TrigNodeName, 44, 45)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, TrigNodeName, TrigNodeName, 44, 44) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbDegToRad.Checked Then
                    CalcInfo.Add(TrigNodeName, New CalcOpInfo)
                    CalcInfo(TrigNodeName).Units = TrigNodeUnits
                    CalcInfo(TrigNodeName).UnitsAbbrev = TrigNodeUnitsAbbrev
                    CalcInfo(TrigNodeName).Description = TrigNodeDescr
                    CalcInfo(TrigNodeName).Type = "Degrees To Radians"
                    ScalarData.Add(TrigNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(TrigNodeName, TrigNodeName, 46, 47)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, TrigNodeName, TrigNodeName, 46, 47) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbRadToDeg.Checked Then
                    CalcInfo.Add(TrigNodeName, New CalcOpInfo)
                    CalcInfo(TrigNodeName).Units = TrigNodeUnits
                    CalcInfo(TrigNodeName).UnitsAbbrev = TrigNodeUnitsAbbrev
                    CalcInfo(TrigNodeName).Description = TrigNodeDescr
                    CalcInfo(TrigNodeName).Type = "Radians To Degrees"
                    ScalarData.Add(TrigNodeName, 45) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(TrigNodeName, TrigNodeName, 48, 49)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, TrigNodeName, TrigNodeName, 48, 49) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If
                Else
                    Message.AddWarning("No node type has been selected." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnReplaceTrigNode_Click(sender As Object, e As EventArgs) Handles btnReplaceTrigNode.Click
        'Replace selected node with an Trigonometric node:

        'Dim TrigNodeName As String = txtTrigNodeName.Text.Trim
        'Dim TrigNodeDescr As String = txtTrigNodeDescr.Text.Trim
        Dim TrigNodeName As String = txtNodeName.Text.Trim
        Dim TrigNodeDescr As String = txtNodeDescr.Text.Trim
        Dim TrigNodeUnits As String = txtNodeUnits.Text.Trim
        Dim TrigNodeUnitsAbbrev As String = txtNodeUnitsAbbrev.Text.Trim

        If TrigNodeName = "" Then
            Message.AddWarning("Please enter a name for the Trigonometric node." & vbCrLf)
        Else
            If trvCalculations.SelectedNode Is Nothing Then
                Message.AddWarning("Please select a node to replace with a Data node." & vbCrLf)
            Else
                Dim OldDataName As String = trvCalculations.SelectedNode.Name
                If CalcInfo(OldDataName).CopyList.Count = 0 Then
                    If CalcInfo.ContainsKey(TrigNodeName) And (TrigNodeName <> OldDataName) Then 'If DataName = OldDataName the node name will be reused when the Node is replaced
                        Message.AddWarning("The node name is already used: " & TrigNodeName & vbCrLf)
                    Else
                        If rbSin.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(TrigNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(TrigNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(TrigNodeName).Units = TrigNodeUnits
                            CalcInfo(TrigNodeName).UnitsAbbrev = TrigNodeUnitsAbbrev
                            CalcInfo(TrigNodeName).Description = TrigNodeDescr
                            CalcInfo(TrigNodeName).Type = "Sine"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(TrigNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = TrigNodeName
                            trvCalculations.SelectedNode.Text = TrigNodeName
                            trvCalculations.SelectedNode.ImageIndex = 34
                            trvCalculations.SelectedNode.SelectedImageIndex = 35

                        ElseIf rbCos.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(TrigNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(TrigNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(TrigNodeName).Units = TrigNodeUnits
                            CalcInfo(TrigNodeName).UnitsAbbrev = TrigNodeUnitsAbbrev
                            CalcInfo(TrigNodeName).Description = TrigNodeDescr
                            CalcInfo(TrigNodeName).Type = "Cosine"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(TrigNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = TrigNodeName
                            trvCalculations.SelectedNode.Text = TrigNodeName
                            trvCalculations.SelectedNode.ImageIndex = 36
                            trvCalculations.SelectedNode.SelectedImageIndex = 37

                        ElseIf rbTan.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(TrigNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(TrigNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(TrigNodeName).Units = TrigNodeUnits
                            CalcInfo(TrigNodeName).UnitsAbbrev = TrigNodeUnitsAbbrev
                            CalcInfo(TrigNodeName).Description = TrigNodeDescr
                            CalcInfo(TrigNodeName).Type = "Tangent"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(TrigNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = TrigNodeName
                            trvCalculations.SelectedNode.Text = TrigNodeName
                            trvCalculations.SelectedNode.ImageIndex = 38
                            trvCalculations.SelectedNode.SelectedImageIndex = 39

                        ElseIf rbArcSin.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(TrigNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(TrigNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(TrigNodeName).Units = TrigNodeUnits
                            CalcInfo(TrigNodeName).UnitsAbbrev = TrigNodeUnitsAbbrev
                            CalcInfo(TrigNodeName).Description = TrigNodeDescr
                            CalcInfo(TrigNodeName).Type = "ArcSine"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(TrigNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = TrigNodeName
                            trvCalculations.SelectedNode.Text = TrigNodeName
                            trvCalculations.SelectedNode.ImageIndex = 40
                            trvCalculations.SelectedNode.SelectedImageIndex = 41

                        ElseIf rbArcCos.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(TrigNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(TrigNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(TrigNodeName).Units = TrigNodeUnits
                            CalcInfo(TrigNodeName).UnitsAbbrev = TrigNodeUnitsAbbrev
                            CalcInfo(TrigNodeName).Description = TrigNodeDescr
                            CalcInfo(TrigNodeName).Type = "ArcCosine"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(TrigNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = TrigNodeName
                            trvCalculations.SelectedNode.Text = TrigNodeName
                            trvCalculations.SelectedNode.ImageIndex = 42
                            trvCalculations.SelectedNode.SelectedImageIndex = 43

                        ElseIf rbArcTan.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(TrigNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(TrigNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(TrigNodeName).Units = TrigNodeUnits
                            CalcInfo(TrigNodeName).UnitsAbbrev = TrigNodeUnitsAbbrev
                            CalcInfo(TrigNodeName).Description = TrigNodeDescr
                            CalcInfo(TrigNodeName).Type = "ArcTangent"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(TrigNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = TrigNodeName
                            trvCalculations.SelectedNode.Text = TrigNodeName
                            trvCalculations.SelectedNode.ImageIndex = 44
                            trvCalculations.SelectedNode.SelectedImageIndex = 45

                        ElseIf rbDegToRad.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(TrigNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(TrigNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(TrigNodeName).Units = TrigNodeUnits
                            CalcInfo(TrigNodeName).UnitsAbbrev = TrigNodeUnitsAbbrev
                            CalcInfo(TrigNodeName).Description = TrigNodeDescr
                            CalcInfo(TrigNodeName).Type = "Degrees To Radians"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(TrigNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = TrigNodeName
                            trvCalculations.SelectedNode.Text = TrigNodeName
                            trvCalculations.SelectedNode.ImageIndex = 46
                            trvCalculations.SelectedNode.SelectedImageIndex = 47

                        ElseIf rbRadToDeg.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(TrigNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(TrigNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(TrigNodeName).Units = TrigNodeUnits
                            CalcInfo(TrigNodeName).UnitsAbbrev = TrigNodeUnitsAbbrev
                            CalcInfo(TrigNodeName).Description = TrigNodeDescr
                            CalcInfo(TrigNodeName).Type = "Radians To Degrees"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(TrigNodeName, 45) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = TrigNodeName
                            trvCalculations.SelectedNode.Text = TrigNodeName
                            trvCalculations.SelectedNode.ImageIndex = 48
                            trvCalculations.SelectedNode.SelectedImageIndex = 49

                        Else
                            Message.AddWarning("No node type has been selected." & vbCrLf)
                        End If
                    End If
                Else
                    Message.AddWarning("This node data is copied in other nodes. Remove the copies before replacing this node." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnAppendTrigNode_Click(sender As Object, e As EventArgs) Handles btnAppendTrigNode.Click
        'Append an Trigonometric node:

        'Dim TrigNodeName As String = txtTrigNodeName.Text.Trim
        'Dim TrigNodeDescr As String = txtTrigNodeDescr.Text.Trim
        Dim TrigNodeName As String = txtNodeName.Text.Trim
        Dim TrigNodeDescr As String = txtNodeDescr.Text.Trim
        Dim TrigNodeUnits As String = txtNodeUnits.Text.Trim
        Dim TrigNodeUnitsAbbrev As String = txtNodeUnitsAbbrev.Text.Trim

        If TrigNodeName = "" Then
            Message.AddWarning("Please enter a name for the Trigonometric node." & vbCrLf)
        Else
            If CalcInfo.ContainsKey(TrigNodeName) Then
                Message.AddWarning("The node name is already used: " & TrigNodeName & vbCrLf)
            Else
                If rbSin.Checked Then
                    CalcInfo.Add(TrigNodeName, New CalcOpInfo)
                    CalcInfo(TrigNodeName).Units = TrigNodeUnits
                    CalcInfo(TrigNodeName).UnitsAbbrev = TrigNodeUnitsAbbrev
                    CalcInfo(TrigNodeName).Description = TrigNodeDescr
                    CalcInfo(TrigNodeName).Type = "Sine"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(TrigNodeName, TrigNodeName, 34, 35)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(TrigNodeName, TrigNodeName, 34, 35)
                    End If
                    ScalarData.Add(TrigNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbCos.Checked Then
                    CalcInfo.Add(TrigNodeName, New CalcOpInfo)
                    CalcInfo(TrigNodeName).Units = TrigNodeUnits
                    CalcInfo(TrigNodeName).UnitsAbbrev = TrigNodeUnitsAbbrev
                    CalcInfo(TrigNodeName).Description = TrigNodeDescr
                    CalcInfo(TrigNodeName).Type = "Cosine"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(TrigNodeName, TrigNodeName, 36, 37)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(TrigNodeName, TrigNodeName, 36, 37)
                    End If
                    ScalarData.Add(TrigNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbTan.Checked Then
                    CalcInfo.Add(TrigNodeName, New CalcOpInfo)
                    CalcInfo(TrigNodeName).Units = TrigNodeUnits
                    CalcInfo(TrigNodeName).UnitsAbbrev = TrigNodeUnitsAbbrev
                    CalcInfo(TrigNodeName).Description = TrigNodeDescr
                    CalcInfo(TrigNodeName).Type = "Tangent"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(TrigNodeName, TrigNodeName, 38, 39)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(TrigNodeName, TrigNodeName, 38, 39)
                    End If
                    ScalarData.Add(TrigNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbArcSin.Checked Then
                    CalcInfo.Add(TrigNodeName, New CalcOpInfo)
                    CalcInfo(TrigNodeName).Units = TrigNodeUnits
                    CalcInfo(TrigNodeName).UnitsAbbrev = TrigNodeUnitsAbbrev
                    CalcInfo(TrigNodeName).Description = TrigNodeDescr
                    CalcInfo(TrigNodeName).Type = "ArcSine"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(TrigNodeName, TrigNodeName, 40, 41)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(TrigNodeName, TrigNodeName, 40, 41)
                    End If
                    ScalarData.Add(TrigNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbArcCos.Checked Then
                    CalcInfo.Add(TrigNodeName, New CalcOpInfo)
                    CalcInfo(TrigNodeName).Units = TrigNodeUnits
                    CalcInfo(TrigNodeName).UnitsAbbrev = TrigNodeUnitsAbbrev
                    CalcInfo(TrigNodeName).Description = TrigNodeDescr
                    CalcInfo(TrigNodeName).Type = "ArcCosine"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(TrigNodeName, TrigNodeName, 42, 43)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(TrigNodeName, TrigNodeName, 42, 43)
                    End If
                    ScalarData.Add(TrigNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbArcTan.Checked Then
                    CalcInfo.Add(TrigNodeName, New CalcOpInfo)
                    CalcInfo(TrigNodeName).Units = TrigNodeUnits
                    CalcInfo(TrigNodeName).UnitsAbbrev = TrigNodeUnitsAbbrev
                    CalcInfo(TrigNodeName).Description = TrigNodeDescr
                    CalcInfo(TrigNodeName).Type = "ArcTangent"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(TrigNodeName, TrigNodeName, 44, 45)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(TrigNodeName, TrigNodeName, 44, 45)
                    End If
                    ScalarData.Add(TrigNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbDegToRad.Checked Then
                    CalcInfo.Add(TrigNodeName, New CalcOpInfo)
                    CalcInfo(TrigNodeName).Units = TrigNodeUnits
                    CalcInfo(TrigNodeName).UnitsAbbrev = TrigNodeUnitsAbbrev
                    CalcInfo(TrigNodeName).Description = TrigNodeDescr
                    CalcInfo(TrigNodeName).Type = "Degrees To Radians"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(TrigNodeName, TrigNodeName, 46, 47)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(TrigNodeName, TrigNodeName, 46, 47)
                    End If
                    ScalarData.Add(TrigNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbRadToDeg.Checked Then
                    CalcInfo.Add(TrigNodeName, New CalcOpInfo)
                    CalcInfo(TrigNodeName).Units = TrigNodeUnits
                    CalcInfo(TrigNodeName).UnitsAbbrev = TrigNodeUnitsAbbrev
                    CalcInfo(TrigNodeName).Description = TrigNodeDescr
                    CalcInfo(TrigNodeName).Type = "Radians To Degrees"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(TrigNodeName, TrigNodeName, 48, 49)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(TrigNodeName, TrigNodeName, 48, 49)
                    End If
                    ScalarData.Add(TrigNodeName, 45) 'This value will be overwritten when the sequence is run. 

                Else
                    Message.AddWarning("No node type has been selected." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnInsertLogNode_Click(sender As Object, e As EventArgs) Handles btnInsertLogNode.Click
        'Insert an Log node:

        'Dim LogNodeName As String = txtLogNodeName.Text.Trim
        'Dim LogNodeDescr As String = txtLogNodeDescr.Text.Trim
        Dim LogNodeName As String = txtNodeName.Text.Trim
        Dim LogNodeDescr As String = txtNodeDescr.Text.Trim
        Dim LogNodeUnits As String = txtNodeUnits.Text.Trim
        Dim LogNodeUnitsAbbrev As String = txtNodeUnitsAbbrev.Text.Trim

        If LogNodeName = "" Then
            Message.AddWarning("Please enter a name for the Log node." & vbCrLf)
        Else
            If CalcInfo.ContainsKey(LogNodeName) Then
                Message.AddWarning("The node name is already used: " & LogNodeName & vbCrLf)
            Else
                If rbEPower.Checked Then
                    CalcInfo.Add(LogNodeName, New CalcOpInfo)
                    CalcInfo(LogNodeName).Units = LogNodeUnits
                    CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                    CalcInfo(LogNodeName).Description = LogNodeDescr
                    CalcInfo(LogNodeName).Type = "Power Of E"
                    ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(LogNodeName, LogNodeName, 50, 51)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, LogNodeName, LogNodeName, 50, 51) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbLn.Checked Then
                    CalcInfo.Add(LogNodeName, New CalcOpInfo)
                    CalcInfo(LogNodeName).Units = LogNodeUnits
                    CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                    CalcInfo(LogNodeName).Description = LogNodeDescr
                    CalcInfo(LogNodeName).Type = "Natural Logarithm"
                    ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(LogNodeName, LogNodeName, 52, 53)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, LogNodeName, LogNodeName, 52, 53) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbTenPower.Checked Then
                    CalcInfo.Add(LogNodeName, New CalcOpInfo)
                    CalcInfo(LogNodeName).Units = LogNodeUnits
                    CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                    CalcInfo(LogNodeName).Description = LogNodeDescr
                    CalcInfo(LogNodeName).Type = "Power Of Ten"
                    ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(LogNodeName, LogNodeName, 54, 55)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, LogNodeName, LogNodeName, 54, 55) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbLog.Checked Then
                    CalcInfo.Add(LogNodeName, New CalcOpInfo)
                    CalcInfo(LogNodeName).Units = LogNodeUnits
                    CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                    CalcInfo(LogNodeName).Description = LogNodeDescr
                    CalcInfo(LogNodeName).Type = "Logarithm"
                    ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(LogNodeName, LogNodeName, 56, 57)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, LogNodeName, LogNodeName, 56, 57) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbSquare.Checked Then
                    CalcInfo.Add(LogNodeName, New CalcOpInfo)
                    CalcInfo(LogNodeName).Units = LogNodeUnits
                    CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                    CalcInfo(LogNodeName).Description = LogNodeDescr
                    CalcInfo(LogNodeName).Type = "Square"
                    ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(LogNodeName, LogNodeName, 58, 59)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, LogNodeName, LogNodeName, 58, 59) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbSquareRoot.Checked Then
                    CalcInfo.Add(LogNodeName, New CalcOpInfo)
                    CalcInfo(LogNodeName).Units = LogNodeUnits
                    CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                    CalcInfo(LogNodeName).Description = LogNodeDescr
                    CalcInfo(LogNodeName).Type = "Square Root"
                    ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(LogNodeName, LogNodeName, 60, 61)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, LogNodeName, LogNodeName, 60, 61) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbCube.Checked Then
                    CalcInfo.Add(LogNodeName, New CalcOpInfo)
                    CalcInfo(LogNodeName).Units = LogNodeUnits
                    CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                    CalcInfo(LogNodeName).Description = LogNodeDescr
                    CalcInfo(LogNodeName).Type = "Cube"
                    ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(LogNodeName, LogNodeName, 62, 63)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, LogNodeName, LogNodeName, 62, 63) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbCubeRoot.Checked Then
                    CalcInfo.Add(LogNodeName, New CalcOpInfo)
                    CalcInfo(LogNodeName).Units = LogNodeUnits
                    CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                    CalcInfo(LogNodeName).Description = LogNodeDescr
                    CalcInfo(LogNodeName).Type = "Cube Root"
                    ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(LogNodeName, LogNodeName, 64, 65)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, LogNodeName, LogNodeName, 64, 65) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbYthPower.Checked Then
                    CalcInfo.Add(LogNodeName, New CalcOpInfo)
                    CalcInfo(LogNodeName).Units = LogNodeUnits
                    CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                    CalcInfo(LogNodeName).Description = LogNodeDescr
                    CalcInfo(LogNodeName).Type = "Exponentiate"
                    ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(LogNodeName, LogNodeName, 66, 67)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, LogNodeName, LogNodeName, 66, 67) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbYthRoot.Checked Then
                    CalcInfo.Add(LogNodeName, New CalcOpInfo)
                    CalcInfo(LogNodeName).Units = LogNodeUnits
                    CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                    CalcInfo(LogNodeName).Description = LogNodeDescr
                    CalcInfo(LogNodeName).Type = "Root"
                    ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(LogNodeName, LogNodeName, 68, 69)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, LogNodeName, LogNodeName, 68, 69) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                Else
                    Message.AddWarning("No node type has been selected." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnReplaceLogNode_Click(sender As Object, e As EventArgs) Handles btnReplaceLogNode.Click
        'Replace the selected node with a Log node:

        'Dim LogNodeName As String = txtLogNodeName.Text.Trim
        'Dim LogNodeDescr As String = txtLogNodeDescr.Text.Trim
        Dim LogNodeName As String = txtNodeName.Text.Trim
        Dim LogNodeDescr As String = txtNodeDescr.Text.Trim
        Dim LogNodeUnits As String = txtNodeUnits.Text.Trim
        Dim LogNodeUnitsAbbrev As String = txtNodeUnitsAbbrev.Text.Trim

        If LogNodeName = "" Then
            Message.AddWarning("Please enter a name for the Log node." & vbCrLf)
        Else
            If trvCalculations.SelectedNode Is Nothing Then
                Message.AddWarning("Please select a node to replace with a Log node." & vbCrLf)
            Else
                Dim OldDataName As String = trvCalculations.SelectedNode.Name
                If CalcInfo(OldDataName).CopyList.Count = 0 Then
                    If CalcInfo.ContainsKey(LogNodeName) And (LogNodeName <> OldDataName) Then 'If DataName = OldDataName the node name will be reused when the Node is replaced
                        Message.AddWarning("The node name is already used: " & LogNodeName & vbCrLf)
                    Else
                        If rbEPower.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(LogNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(LogNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(LogNodeName).Units = LogNodeUnits
                            CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                            CalcInfo(LogNodeName).Description = LogNodeDescr
                            CalcInfo(LogNodeName).Type = "Power Of E"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = LogNodeName
                            trvCalculations.SelectedNode.Text = LogNodeName
                            trvCalculations.SelectedNode.ImageIndex = 50
                            trvCalculations.SelectedNode.SelectedImageIndex = 51

                        ElseIf rbLn.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(LogNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(LogNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(LogNodeName).Units = LogNodeUnits
                            CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                            CalcInfo(LogNodeName).Description = LogNodeDescr
                            CalcInfo(LogNodeName).Type = "Natural Logarithm"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = LogNodeName
                            trvCalculations.SelectedNode.Text = LogNodeName
                            trvCalculations.SelectedNode.ImageIndex = 52
                            trvCalculations.SelectedNode.SelectedImageIndex = 53

                        ElseIf rbTenPower.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(LogNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(LogNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(LogNodeName).Units = LogNodeUnits
                            CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                            CalcInfo(LogNodeName).Description = LogNodeDescr
                            CalcInfo(LogNodeName).Type = "Power Of Ten"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = LogNodeName
                            trvCalculations.SelectedNode.Text = LogNodeName
                            trvCalculations.SelectedNode.ImageIndex = 54
                            trvCalculations.SelectedNode.SelectedImageIndex = 55

                        ElseIf rbLog.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(LogNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(LogNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(LogNodeName).Units = LogNodeUnits
                            CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                            CalcInfo(LogNodeName).Description = LogNodeDescr
                            CalcInfo(LogNodeName).Type = "Logarithm"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = LogNodeName
                            trvCalculations.SelectedNode.Text = LogNodeName
                            trvCalculations.SelectedNode.ImageIndex = 56
                            trvCalculations.SelectedNode.SelectedImageIndex = 57

                        ElseIf rbSquare.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(LogNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(LogNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(LogNodeName).Units = LogNodeUnits
                            CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                            CalcInfo(LogNodeName).Description = LogNodeDescr
                            CalcInfo(LogNodeName).Type = "Square"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = LogNodeName
                            trvCalculations.SelectedNode.Text = LogNodeName
                            trvCalculations.SelectedNode.ImageIndex = 58
                            trvCalculations.SelectedNode.SelectedImageIndex = 59

                        ElseIf rbSquareRoot.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(LogNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(LogNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(LogNodeName).Units = LogNodeUnits
                            CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                            CalcInfo(LogNodeName).Description = LogNodeDescr
                            CalcInfo(LogNodeName).Type = "Square Root"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = LogNodeName
                            trvCalculations.SelectedNode.Text = LogNodeName
                            trvCalculations.SelectedNode.ImageIndex = 60
                            trvCalculations.SelectedNode.SelectedImageIndex = 61

                        ElseIf rbCube.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(LogNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(LogNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(LogNodeName).Units = LogNodeUnits
                            CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                            CalcInfo(LogNodeName).Description = LogNodeDescr
                            CalcInfo(LogNodeName).Type = "Cube"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = LogNodeName
                            trvCalculations.SelectedNode.Text = LogNodeName
                            trvCalculations.SelectedNode.ImageIndex = 62
                            trvCalculations.SelectedNode.SelectedImageIndex = 63

                        ElseIf rbCubeRoot.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(LogNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(LogNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(LogNodeName).Units = LogNodeUnits
                            CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                            CalcInfo(LogNodeName).Description = LogNodeDescr
                            CalcInfo(LogNodeName).Type = "Cube Root"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = LogNodeName
                            trvCalculations.SelectedNode.Text = LogNodeName
                            trvCalculations.SelectedNode.ImageIndex = 64
                            trvCalculations.SelectedNode.SelectedImageIndex = 65

                        ElseIf rbYthPower.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(LogNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(LogNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(LogNodeName).Units = LogNodeUnits
                            CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                            CalcInfo(LogNodeName).Description = LogNodeDescr
                            CalcInfo(LogNodeName).Type = "Exponentiate"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = LogNodeName
                            trvCalculations.SelectedNode.Text = LogNodeName
                            trvCalculations.SelectedNode.ImageIndex = 66
                            trvCalculations.SelectedNode.SelectedImageIndex = 67

                        ElseIf rbYthRoot.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(LogNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(LogNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(LogNodeName).Units = LogNodeUnits
                            CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                            CalcInfo(LogNodeName).Description = LogNodeDescr
                            CalcInfo(LogNodeName).Type = "Root"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = LogNodeName
                            trvCalculations.SelectedNode.Text = LogNodeName
                            trvCalculations.SelectedNode.ImageIndex = 68
                            trvCalculations.SelectedNode.SelectedImageIndex = 69

                        Else
                            Message.AddWarning("No node type has been selected." & vbCrLf)
                        End If
                    End If
                Else
                    Message.AddWarning("This node data is copied in other nodes. Remove the copies before replacing this node." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnAppendLogNode_Click(sender As Object, e As EventArgs) Handles btnAppendLogNode.Click
        'Append a Log node:

        'Dim LogNodeName As String = txtLogNodeName.Text.Trim
        'Dim LogNodeDescr As String = txtLogNodeDescr.Text.Trim
        Dim LogNodeName As String = txtNodeName.Text.Trim
        Dim LogNodeDescr As String = txtNodeDescr.Text.Trim
        Dim LogNodeUnits As String = txtNodeUnits.Text.Trim
        Dim LogNodeUnitsAbbrev As String = txtNodeUnitsAbbrev.Text.Trim

        If LogNodeName = "" Then
            Message.AddWarning("Please enter a name for the Log node." & vbCrLf)
        Else
            If CalcInfo.ContainsKey(LogNodeName) Then
                Message.AddWarning("The node name is already used: " & LogNodeName & vbCrLf)
            Else
                If rbEPower.Checked Then
                    CalcInfo.Add(LogNodeName, New CalcOpInfo)
                    CalcInfo(LogNodeName).Units = LogNodeUnits
                    CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                    CalcInfo(LogNodeName).Description = LogNodeDescr
                    CalcInfo(LogNodeName).Type = "Power Of E"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(LogNodeName, LogNodeName, 50, 51)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(LogNodeName, LogNodeName, 50, 51)
                    End If
                    ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbLn.Checked Then
                    CalcInfo.Add(LogNodeName, New CalcOpInfo)
                    CalcInfo(LogNodeName).Units = LogNodeUnits
                    CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                    CalcInfo(LogNodeName).Description = LogNodeDescr
                    CalcInfo(LogNodeName).Type = "Natural Logarithm"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(LogNodeName, LogNodeName, 52, 53)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(LogNodeName, LogNodeName, 52, 53)
                    End If
                    ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbTenPower.Checked Then
                    CalcInfo.Add(LogNodeName, New CalcOpInfo)
                    CalcInfo(LogNodeName).Units = LogNodeUnits
                    CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                    CalcInfo(LogNodeName).Description = LogNodeDescr
                    CalcInfo(LogNodeName).Type = "Power Of Ten"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(LogNodeName, LogNodeName, 54, 55)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(LogNodeName, LogNodeName, 54, 55)
                    End If
                    ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbLog.Checked Then
                    CalcInfo.Add(LogNodeName, New CalcOpInfo)
                    CalcInfo(LogNodeName).Units = LogNodeUnits
                    CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                    CalcInfo(LogNodeName).Description = LogNodeDescr
                    CalcInfo(LogNodeName).Type = "Logarithm"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(LogNodeName, LogNodeName, 56, 57)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(LogNodeName, LogNodeName, 56, 57)
                    End If
                    ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbSquare.Checked Then
                    CalcInfo.Add(LogNodeName, New CalcOpInfo)
                    CalcInfo(LogNodeName).Units = LogNodeUnits
                    CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                    CalcInfo(LogNodeName).Description = LogNodeDescr
                    CalcInfo(LogNodeName).Type = "Square"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(LogNodeName, LogNodeName, 58, 59)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(LogNodeName, LogNodeName, 58, 59)
                    End If
                    ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbSquareRoot.Checked Then
                    CalcInfo.Add(LogNodeName, New CalcOpInfo)
                    CalcInfo(LogNodeName).Units = LogNodeUnits
                    CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                    CalcInfo(LogNodeName).Description = LogNodeDescr
                    CalcInfo(LogNodeName).Type = "Square Root"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(LogNodeName, LogNodeName, 60, 61)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(LogNodeName, LogNodeName, 60, 61)
                    End If
                    ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbCube.Checked Then
                    CalcInfo.Add(LogNodeName, New CalcOpInfo)
                    CalcInfo(LogNodeName).Units = LogNodeUnits
                    CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                    CalcInfo(LogNodeName).Description = LogNodeDescr
                    CalcInfo(LogNodeName).Type = "Cube"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(LogNodeName, LogNodeName, 62, 63)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(LogNodeName, LogNodeName, 62, 63)
                    End If
                    ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbCubeRoot.Checked Then
                    CalcInfo.Add(LogNodeName, New CalcOpInfo)
                    CalcInfo(LogNodeName).Units = LogNodeUnits
                    CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                    CalcInfo(LogNodeName).Description = LogNodeDescr
                    CalcInfo(LogNodeName).Type = "Cube Root"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(LogNodeName, LogNodeName, 64, 65)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(LogNodeName, LogNodeName, 64, 65)
                    End If
                    ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbYthPower.Checked Then
                    CalcInfo.Add(LogNodeName, New CalcOpInfo)
                    CalcInfo(LogNodeName).Units = LogNodeUnits
                    CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                    CalcInfo(LogNodeName).Description = LogNodeDescr
                    CalcInfo(LogNodeName).Type = "Exponentiate"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(LogNodeName, LogNodeName, 66, 67)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(LogNodeName, LogNodeName, 66, 67)
                    End If
                    ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbYthRoot.Checked Then
                    CalcInfo.Add(LogNodeName, New CalcOpInfo)
                    CalcInfo(LogNodeName).Units = LogNodeUnits
                    CalcInfo(LogNodeName).UnitsAbbrev = LogNodeUnitsAbbrev
                    CalcInfo(LogNodeName).Description = LogNodeDescr
                    CalcInfo(LogNodeName).Type = "Root"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(LogNodeName, LogNodeName, 68, 69)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(LogNodeName, LogNodeName, 68, 69)
                    End If
                    ScalarData.Add(LogNodeName, 1) 'This value will be overwritten when the sequence is run. 

                Else
                    Message.AddWarning("No node type has been selected." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnInsertOneInputNode_Click(sender As Object, e As EventArgs) Handles btnInsertOneInputNode.Click
        'Insert a One Input node:

        'Dim OneInputNodeName As String = txtOneInputNodeName.Text.Trim
        'Dim OneInputDescr As String = txtOneInputNodeDescr.Text.Trim
        Dim OneInputNodeName As String = txtNodeName.Text.Trim
        Dim OneInputDescr As String = txtNodeDescr.Text.Trim
        Dim OneInputUnits As String = txtNodeUnits.Text.Trim
        Dim OneInputUnitsAbbrev As String = txtNodeUnitsAbbrev.Text.Trim


        If OneInputNodeName = "" Then
            Message.AddWarning("Please enter a name for the One Input node." & vbCrLf)
        Else
            If CalcInfo.ContainsKey(OneInputNodeName) Then
                Message.AddWarning("The node name is already used: " & OneInputNodeName & vbCrLf)
            Else
                If rbEquals.Checked Then
                    CalcInfo.Add(OneInputNodeName, New CalcOpInfo)
                    CalcInfo(OneInputNodeName).Units = OneInputUnits
                    CalcInfo(OneInputNodeName).UnitsAbbrev = OneInputUnitsAbbrev
                    CalcInfo(OneInputNodeName).Description = OneInputDescr
                    CalcInfo(OneInputNodeName).Type = "Equals"
                    ScalarData.Add(OneInputNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(OneInputNodeName, OneInputNodeName, 84, 85)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, OneInputNodeName, OneInputNodeName, 84, 85) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbNegate.Checked Then
                    CalcInfo.Add(OneInputNodeName, New CalcOpInfo)
                    CalcInfo(OneInputNodeName).Units = OneInputUnits
                    CalcInfo(OneInputNodeName).UnitsAbbrev = OneInputUnitsAbbrev
                    CalcInfo(OneInputNodeName).Description = OneInputDescr
                    CalcInfo(OneInputNodeName).Type = "Negate"
                    ScalarData.Add(OneInputNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(OneInputNodeName, OneInputNodeName, 70, 71)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, OneInputNodeName, OneInputNodeName, 70, 71) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbInvert.Checked Then
                    CalcInfo.Add(OneInputNodeName, New CalcOpInfo)
                    CalcInfo(OneInputNodeName).Units = OneInputUnits
                    CalcInfo(OneInputNodeName).UnitsAbbrev = OneInputUnitsAbbrev
                    CalcInfo(OneInputNodeName).Description = OneInputDescr
                    CalcInfo(OneInputNodeName).Type = "Invert"
                    ScalarData.Add(OneInputNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(OneInputNodeName, OneInputNodeName, 72, 73)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, OneInputNodeName, OneInputNodeName, 72, 73) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbAbsoluteVal.Checked Then
                    CalcInfo.Add(OneInputNodeName, New CalcOpInfo)
                    CalcInfo(OneInputNodeName).Units = OneInputUnits
                    CalcInfo(OneInputNodeName).UnitsAbbrev = OneInputUnitsAbbrev
                    CalcInfo(OneInputNodeName).Description = OneInputDescr
                    CalcInfo(OneInputNodeName).Type = "Absolute"
                    ScalarData.Add(OneInputNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(OneInputNodeName, OneInputNodeName, 74, 75)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, OneInputNodeName, OneInputNodeName, 74, 75) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbRound.Checked Then
                    CalcInfo.Add(OneInputNodeName, New CalcOpInfo)
                    CalcInfo(OneInputNodeName).Units = OneInputUnits
                    CalcInfo(OneInputNodeName).UnitsAbbrev = OneInputUnitsAbbrev
                    CalcInfo(OneInputNodeName).Description = OneInputDescr
                    CalcInfo(OneInputNodeName).Type = "Round"
                    ScalarData.Add(OneInputNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(OneInputNodeName, OneInputNodeName, 76, 77)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, OneInputNodeName, OneInputNodeName, 76, 77) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbRoundUp.Checked Then
                    CalcInfo.Add(OneInputNodeName, New CalcOpInfo)
                    CalcInfo(OneInputNodeName).Units = OneInputUnits
                    CalcInfo(OneInputNodeName).UnitsAbbrev = OneInputUnitsAbbrev
                    CalcInfo(OneInputNodeName).Description = OneInputDescr
                    CalcInfo(OneInputNodeName).Type = "Round Up"
                    ScalarData.Add(OneInputNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(OneInputNodeName, OneInputNodeName, 78, 79)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, OneInputNodeName, OneInputNodeName, 78, 79) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbRoundDown.Checked Then
                    CalcInfo.Add(OneInputNodeName, New CalcOpInfo)
                    CalcInfo(OneInputNodeName).Units = OneInputUnits
                    CalcInfo(OneInputNodeName).UnitsAbbrev = OneInputUnitsAbbrev
                    CalcInfo(OneInputNodeName).Description = OneInputDescr
                    CalcInfo(OneInputNodeName).Type = "Round Down"
                    ScalarData.Add(OneInputNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(OneInputNodeName, OneInputNodeName, 80, 81)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, OneInputNodeName, OneInputNodeName, 80, 81) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                Else
                    Message.AddWarning("No node type has been selected." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnReplaceOneInputNode_Click(sender As Object, e As EventArgs) Handles btnReplaceOneInputNode.Click
        'Replace the selected node with a One Input node:

        'Dim OneInputNodeName As String = txtOneInputNodeName.Text.Trim
        'Dim OneInputDescr As String = txtOneInputNodeDescr.Text.Trim
        Dim OneInputNodeName As String = txtNodeName.Text.Trim
        Dim OneInputDescr As String = txtNodeDescr.Text.Trim
        Dim OneInputUnits As String = txtNodeUnits.Text.Trim
        Dim OneInputUnitsAbbrev As String = txtNodeUnitsAbbrev.Text.Trim

        If OneInputNodeName = "" Then
            Message.AddWarning("Please enter a name for the One Input node." & vbCrLf)
        Else
            If trvCalculations.SelectedNode Is Nothing Then
                Message.AddWarning("Please select a node to replace with a Data node." & vbCrLf)
            Else
                Dim OldDataName As String = trvCalculations.SelectedNode.Name
                If CalcInfo(OldDataName).CopyList.Count = 0 Then
                    If CalcInfo.ContainsKey(OneInputNodeName) And (OneInputNodeName <> OldDataName) Then 'If DataName = OldDataName the node name will be reused when the Node is replaced
                        Message.AddWarning("The node name is already used: " & OneInputNodeName & vbCrLf)
                    Else
                        If rbEquals.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(OneInputNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(OneInputNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(OneInputNodeName).Units = OneInputUnits
                            CalcInfo(OneInputNodeName).UnitsAbbrev = OneInputUnitsAbbrev
                            CalcInfo(OneInputNodeName).Description = OneInputDescr
                            CalcInfo(OneInputNodeName).Type = "Equals"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(OneInputNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = OneInputNodeName
                            trvCalculations.SelectedNode.Text = OneInputNodeName
                            trvCalculations.SelectedNode.ImageIndex = 84
                            trvCalculations.SelectedNode.SelectedImageIndex = 85

                        ElseIf rbNegate.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(OneInputNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(OneInputNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(OneInputNodeName).Units = OneInputUnits
                            CalcInfo(OneInputNodeName).UnitsAbbrev = OneInputUnitsAbbrev
                            CalcInfo(OneInputNodeName).Description = OneInputDescr
                            CalcInfo(OneInputNodeName).Type = "Negate"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(OneInputNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = OneInputNodeName
                            trvCalculations.SelectedNode.Text = OneInputNodeName
                            trvCalculations.SelectedNode.ImageIndex = 70
                            trvCalculations.SelectedNode.SelectedImageIndex = 71

                        ElseIf rbInvert.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(OneInputNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(OneInputNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(OneInputNodeName).Units = OneInputUnits
                            CalcInfo(OneInputNodeName).UnitsAbbrev = OneInputUnitsAbbrev
                            CalcInfo(OneInputNodeName).Description = OneInputDescr
                            CalcInfo(OneInputNodeName).Type = "Invert"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(OneInputNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = OneInputNodeName
                            trvCalculations.SelectedNode.Text = OneInputNodeName
                            trvCalculations.SelectedNode.ImageIndex = 72
                            trvCalculations.SelectedNode.SelectedImageIndex = 73

                        ElseIf rbAbsoluteVal.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(OneInputNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(OneInputNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(OneInputNodeName).Units = OneInputUnits
                            CalcInfo(OneInputNodeName).UnitsAbbrev = OneInputUnitsAbbrev
                            CalcInfo(OneInputNodeName).Description = OneInputDescr
                            CalcInfo(OneInputNodeName).Type = "Absolute"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(OneInputNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = OneInputNodeName
                            trvCalculations.SelectedNode.Text = OneInputNodeName
                            trvCalculations.SelectedNode.ImageIndex = 74
                            trvCalculations.SelectedNode.SelectedImageIndex = 75

                        ElseIf rbRound.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(OneInputNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(OneInputNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(OneInputNodeName).Units = OneInputUnits
                            CalcInfo(OneInputNodeName).UnitsAbbrev = OneInputUnitsAbbrev
                            CalcInfo(OneInputNodeName).Description = OneInputDescr
                            CalcInfo(OneInputNodeName).Type = "Round"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(OneInputNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = OneInputNodeName
                            trvCalculations.SelectedNode.Text = OneInputNodeName
                            trvCalculations.SelectedNode.ImageIndex = 76
                            trvCalculations.SelectedNode.SelectedImageIndex = 77

                        ElseIf rbRoundUp.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(OneInputNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(OneInputNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(OneInputNodeName).Units = OneInputUnits
                            CalcInfo(OneInputNodeName).UnitsAbbrev = OneInputUnitsAbbrev
                            CalcInfo(OneInputNodeName).Description = OneInputDescr
                            CalcInfo(OneInputNodeName).Type = "Round Up"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(OneInputNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = OneInputNodeName
                            trvCalculations.SelectedNode.Text = OneInputNodeName
                            trvCalculations.SelectedNode.ImageIndex = 78
                            trvCalculations.SelectedNode.SelectedImageIndex = 79

                        ElseIf rbRoundDown.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            'If CalcInfo.ContainsKey(DataName) Then Else CalcInfo.Add(DataName, New CalcOpInfo)
                            If CalcInfo.ContainsKey(OneInputNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(OneInputNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(OneInputNodeName).Units = OneInputUnits
                            CalcInfo(OneInputNodeName).UnitsAbbrev = OneInputUnitsAbbrev
                            CalcInfo(OneInputNodeName).Description = OneInputDescr
                            CalcInfo(OneInputNodeName).Type = "Round Down"
                            'ScalarData.Add(DataName, 1) 'Set default value of 1. This will be overwritten by data from the Calculations table when the calculation sequence is run.
                            ScalarData.Add(OneInputNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = OneInputNodeName
                            trvCalculations.SelectedNode.Text = OneInputNodeName
                            trvCalculations.SelectedNode.ImageIndex = 80
                            trvCalculations.SelectedNode.SelectedImageIndex = 81

                        Else
                            Message.AddWarning("No node type has been selected." & vbCrLf)
                        End If
                    End If
                Else
                    Message.AddWarning("This node data is copied in other nodes. Remove the copies before replacing this node." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnAppendOneInputNode_Click(sender As Object, e As EventArgs) Handles btnAppendOneInputNode.Click
        'Append a One Input node:

        'Dim OneInputNodeName As String = txtOneInputNodeName.Text.Trim
        'Dim OneInputDescr As String = txtOneInputNodeDescr.Text.Trim
        Dim OneInputNodeName As String = txtNodeName.Text.Trim
        Dim OneInputDescr As String = txtNodeDescr.Text.Trim
        Dim OneInputUnits As String = txtNodeUnits.Text.Trim
        Dim OneInputUnitsAbbrev As String = txtNodeUnitsAbbrev.Text.Trim

        If OneInputNodeName = "" Then
            Message.AddWarning("Please enter a name for the One Input node." & vbCrLf)
        Else
            If CalcInfo.ContainsKey(OneInputNodeName) Then
                Message.AddWarning("The node name is already used: " & OneInputNodeName & vbCrLf)
            Else
                If rbEquals.Checked Then
                    CalcInfo.Add(OneInputNodeName, New CalcOpInfo)
                    CalcInfo(OneInputNodeName).Units = OneInputUnits
                    CalcInfo(OneInputNodeName).UnitsAbbrev = OneInputUnitsAbbrev
                    CalcInfo(OneInputNodeName).Description = OneInputDescr
                    CalcInfo(OneInputNodeName).Type = "Equals"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(OneInputNodeName, OneInputNodeName, 84, 85)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(OneInputNodeName, OneInputNodeName, 84, 85)
                    End If
                    ScalarData.Add(OneInputNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbNegate.Checked Then
                    CalcInfo.Add(OneInputNodeName, New CalcOpInfo)
                    CalcInfo(OneInputNodeName).Units = OneInputUnits
                    CalcInfo(OneInputNodeName).UnitsAbbrev = OneInputUnitsAbbrev
                    CalcInfo(OneInputNodeName).Description = OneInputDescr
                    CalcInfo(OneInputNodeName).Type = "Negate"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(OneInputNodeName, OneInputNodeName, 70, 71)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(OneInputNodeName, OneInputNodeName, 70, 71)
                    End If
                    ScalarData.Add(OneInputNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbInvert.Checked Then
                    CalcInfo.Add(OneInputNodeName, New CalcOpInfo)
                    CalcInfo(OneInputNodeName).Units = OneInputUnits
                    CalcInfo(OneInputNodeName).UnitsAbbrev = OneInputUnitsAbbrev
                    CalcInfo(OneInputNodeName).Description = OneInputDescr
                    CalcInfo(OneInputNodeName).Type = "Invert"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(OneInputNodeName, OneInputNodeName, 72, 73)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(OneInputNodeName, OneInputNodeName, 72, 73)
                    End If
                    ScalarData.Add(OneInputNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbAbsoluteVal.Checked Then
                    CalcInfo.Add(OneInputNodeName, New CalcOpInfo)
                    CalcInfo(OneInputNodeName).Units = OneInputUnits
                    CalcInfo(OneInputNodeName).UnitsAbbrev = OneInputUnitsAbbrev
                    CalcInfo(OneInputNodeName).Description = OneInputDescr
                    CalcInfo(OneInputNodeName).Type = "Absolute"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(OneInputNodeName, OneInputNodeName, 74, 75)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(OneInputNodeName, OneInputNodeName, 74, 75)
                    End If
                    ScalarData.Add(OneInputNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbRound.Checked Then
                    CalcInfo.Add(OneInputNodeName, New CalcOpInfo)
                    CalcInfo(OneInputNodeName).Units = OneInputUnits
                    CalcInfo(OneInputNodeName).UnitsAbbrev = OneInputUnitsAbbrev
                    CalcInfo(OneInputNodeName).Description = OneInputDescr
                    CalcInfo(OneInputNodeName).Type = "Round"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(OneInputNodeName, OneInputNodeName, 76, 77)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(OneInputNodeName, OneInputNodeName, 76, 77)
                    End If
                    ScalarData.Add(OneInputNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbRoundUp.Checked Then
                    CalcInfo.Add(OneInputNodeName, New CalcOpInfo)
                    CalcInfo(OneInputNodeName).Units = OneInputUnits
                    CalcInfo(OneInputNodeName).UnitsAbbrev = OneInputUnitsAbbrev
                    CalcInfo(OneInputNodeName).Description = OneInputDescr
                    CalcInfo(OneInputNodeName).Type = "Round Up"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(OneInputNodeName, OneInputNodeName, 78, 79)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(OneInputNodeName, OneInputNodeName, 78, 79)
                    End If
                    ScalarData.Add(OneInputNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbRoundDown.Checked Then
                    CalcInfo.Add(OneInputNodeName, New CalcOpInfo)
                    CalcInfo(OneInputNodeName).Units = OneInputUnits
                    CalcInfo(OneInputNodeName).UnitsAbbrev = OneInputUnitsAbbrev
                    CalcInfo(OneInputNodeName).Description = OneInputDescr
                    CalcInfo(OneInputNodeName).Type = "Round Down"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(OneInputNodeName, OneInputNodeName, 80, 81)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(OneInputNodeName, OneInputNodeName, 80, 81)
                    End If
                    ScalarData.Add(OneInputNodeName, 1) 'This value will be overwritten when the sequence is run. 

                Else
                    Message.AddWarning("No node type has been selected." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnInsertBranch_Click(sender As Object, e As EventArgs) Handles btnInsertBranch.Click
        'Insert a Branch node:

        'Dim BranchNodeName As String = txtBranchName.Text.Trim
        'Dim BranchNodeDescr As String = txtBranchDescr.Text.Trim
        Dim BranchNodeName As String = txtNodeName.Text.Trim
        Dim BranchNodeDescr As String = txtNodeDescr.Text.Trim
        Dim BranchNodeUnits As String = txtNodeUnits.Text.Trim
        Dim BranchNodeUnitsAbbrev As String = txtNodeUnitsAbbrev.Text.Trim


        If BranchNodeName = "" Then
            Message.AddWarning("Please enter a name for the Branch node." & vbCrLf)
        Else
            If CalcInfo.ContainsKey(BranchNodeName) Then
                Message.AddWarning("The node name is already used: " & BranchNodeName & vbCrLf)
            Else
                If rbIfGt.Checked Then
                    CalcInfo.Add(BranchNodeName, New CalcOpInfo)
                    CalcInfo(BranchNodeName).Units = BranchNodeUnits
                    CalcInfo(BranchNodeName).UnitsAbbrev = BranchNodeUnitsAbbrev
                    CalcInfo(BranchNodeName).Description = BranchNodeDescr
                    CalcInfo(BranchNodeName).Type = "If Gt"
                    ScalarData.Add(BranchNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(BranchNodeName, BranchNodeName, 90, 91)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, BranchNodeName, BranchNodeName, 90, 91) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbIfGtEq.Checked Then
                    CalcInfo.Add(BranchNodeName, New CalcOpInfo)
                    CalcInfo(BranchNodeName).Units = BranchNodeUnits
                    CalcInfo(BranchNodeName).UnitsAbbrev = BranchNodeUnitsAbbrev
                    CalcInfo(BranchNodeName).Description = BranchNodeDescr
                    CalcInfo(BranchNodeName).Type = "If GtEq"
                    ScalarData.Add(BranchNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(BranchNodeName, BranchNodeName, 92, 93)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, BranchNodeName, BranchNodeName, 92, 93) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbIfEq.Checked Then
                    CalcInfo.Add(BranchNodeName, New CalcOpInfo)
                    CalcInfo(BranchNodeName).Units = BranchNodeUnits
                    CalcInfo(BranchNodeName).UnitsAbbrev = BranchNodeUnitsAbbrev
                    CalcInfo(BranchNodeName).Description = BranchNodeDescr
                    CalcInfo(BranchNodeName).Type = "If Eq"
                    ScalarData.Add(BranchNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(BranchNodeName, BranchNodeName, 86, 87)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, BranchNodeName, BranchNodeName, 86, 87) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbIfLtEq.Checked Then
                    CalcInfo.Add(BranchNodeName, New CalcOpInfo)
                    CalcInfo(BranchNodeName).Units = BranchNodeUnits
                    CalcInfo(BranchNodeName).UnitsAbbrev = BranchNodeUnitsAbbrev
                    CalcInfo(BranchNodeName).Description = BranchNodeDescr
                    CalcInfo(BranchNodeName).Type = "If LtEq"
                    ScalarData.Add(BranchNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(BranchNodeName, BranchNodeName, 94, 95)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, BranchNodeName, BranchNodeName, 94, 95) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                ElseIf rbIfLt.Checked Then
                    CalcInfo.Add(BranchNodeName, New CalcOpInfo)
                    CalcInfo(BranchNodeName).Units = BranchNodeUnits
                    CalcInfo(BranchNodeName).UnitsAbbrev = BranchNodeUnitsAbbrev
                    CalcInfo(BranchNodeName).Description = BranchNodeDescr
                    CalcInfo(BranchNodeName).Type = "If Lt"
                    ScalarData.Add(BranchNodeName, 1) 'This value will be overwritten when the sequence is run. 
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(BranchNodeName, BranchNodeName, 88, 89)
                    Else
                        Dim SelNode As TreeNode = trvCalculations.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvCalculations.SelectedNode.Index
                        Dim NewNode As TreeNode = trvCalculations.SelectedNode.Parent.Nodes.Insert(SelIndex, BranchNodeName, BranchNodeName, 88, 89) 'Add the new node the the parent of the selected node at the same index position.
                        trvCalculations.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvCalculations.SelectedNode = NewNode
                    End If

                Else
                    Message.AddWarning("No node type has been selected." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnReplaceBranch_Click(sender As Object, e As EventArgs) Handles btnReplaceBranch.Click
        'Replace a selected node with a Branch node:

        'Dim BranchNodeName As String = txtBranchName.Text.Trim
        'Dim BranchNodeDescr As String = txtBranchDescr.Text.Trim
        Dim BranchNodeName As String = txtNodeName.Text.Trim
        Dim BranchNodeDescr As String = txtNodeDescr.Text.Trim
        Dim BranchNodeUnits As String = txtNodeUnits.Text.Trim
        Dim BranchNodeUnitsAbbrev As String = txtNodeUnitsAbbrev.Text.Trim

        If BranchNodeName = "" Then
            Message.AddWarning("Please enter a name for the Branch node." & vbCrLf)
        Else
            If trvCalculations.SelectedNode Is Nothing Then
                Message.AddWarning("Please select a node to replace with a Branch node." & vbCrLf)
            Else
                Dim OldDataName As String = trvCalculations.SelectedNode.Name
                If CalcInfo(OldDataName).CopyList.Count = 0 Then
                    If CalcInfo.ContainsKey(BranchNodeName) And (BranchNodeName <> OldDataName) Then 'If DataName = OldDataName the node name will be reused when the Node is replaced
                        Message.AddWarning("The node name is already used: " & BranchNodeName & vbCrLf)
                    Else
                        If rbIfGt.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            If CalcInfo.ContainsKey(BranchNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(BranchNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(BranchNodeName).Units = BranchNodeUnits
                            CalcInfo(BranchNodeName).UnitsAbbrev = BranchNodeUnitsAbbrev
                            CalcInfo(BranchNodeName).Description = BranchNodeDescr
                            CalcInfo(BranchNodeName).Type = "If Gt"
                            ScalarData.Add(BranchNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = BranchNodeName
                            trvCalculations.SelectedNode.Text = BranchNodeName
                            trvCalculations.SelectedNode.ImageIndex = 90
                            trvCalculations.SelectedNode.SelectedImageIndex = 91

                        ElseIf rbIfGtEq.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            If CalcInfo.ContainsKey(BranchNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(BranchNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(BranchNodeName).Units = BranchNodeUnits
                            CalcInfo(BranchNodeName).UnitsAbbrev = BranchNodeUnitsAbbrev
                            CalcInfo(BranchNodeName).Description = BranchNodeDescr
                            CalcInfo(BranchNodeName).Type = "If GtEq"
                            ScalarData.Add(BranchNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = BranchNodeName
                            trvCalculations.SelectedNode.Text = BranchNodeName
                            trvCalculations.SelectedNode.ImageIndex = 92
                            trvCalculations.SelectedNode.SelectedImageIndex = 93

                        ElseIf rbIfEq.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            If CalcInfo.ContainsKey(BranchNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(BranchNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(BranchNodeName).Units = BranchNodeUnits
                            CalcInfo(BranchNodeName).UnitsAbbrev = BranchNodeUnitsAbbrev
                            CalcInfo(BranchNodeName).Description = BranchNodeDescr
                            CalcInfo(BranchNodeName).Type = "If Eq"
                            ScalarData.Add(BranchNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = BranchNodeName
                            trvCalculations.SelectedNode.Text = BranchNodeName
                            trvCalculations.SelectedNode.ImageIndex = 86
                            trvCalculations.SelectedNode.SelectedImageIndex = 87

                        ElseIf rbIfLtEq.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            If CalcInfo.ContainsKey(BranchNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(BranchNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(BranchNodeName).Units = BranchNodeUnits
                            CalcInfo(BranchNodeName).UnitsAbbrev = BranchNodeUnitsAbbrev
                            CalcInfo(BranchNodeName).Description = BranchNodeDescr
                            CalcInfo(BranchNodeName).Type = "If LtEq"
                            ScalarData.Add(BranchNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = BranchNodeName
                            trvCalculations.SelectedNode.Text = BranchNodeName
                            trvCalculations.SelectedNode.ImageIndex = 94
                            trvCalculations.SelectedNode.SelectedImageIndex = 95

                        ElseIf rbIfLt.Checked Then
                            If ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old data
                            End If
                            If CalcInfo.ContainsKey(BranchNodeName) Then
                                'The existing entry is being re-used
                            Else
                                CalcInfo.Add(BranchNodeName, New CalcOpInfo)
                                CalcInfo.Remove(OldDataName) 'Remove the old entry
                            End If
                            CalcInfo(BranchNodeName).Units = BranchNodeUnits
                            CalcInfo(BranchNodeName).UnitsAbbrev = BranchNodeUnitsAbbrev
                            CalcInfo(BranchNodeName).Description = BranchNodeDescr
                            CalcInfo(BranchNodeName).Type = "If Lt"
                            ScalarData.Add(BranchNodeName, 1) 'This value will be overwritten when the sequence is run. 
                            trvCalculations.SelectedNode.Name = BranchNodeName
                            trvCalculations.SelectedNode.Text = BranchNodeName
                            trvCalculations.SelectedNode.ImageIndex = 88
                            trvCalculations.SelectedNode.SelectedImageIndex = 89

                        Else
                            Message.AddWarning("No node type has been selected." & vbCrLf)
                        End If
                    End If
                Else
                    Message.AddWarning("This node data is copied in other nodes. Remove the copies before replacing this node." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnAppendBranch_Click(sender As Object, e As EventArgs) Handles btnAppendBranch.Click
        'Append a Branch node:

        'Dim BranchNodeName As String = txtBranchName.Text.Trim
        'Dim BranchNodeDescr As String = txtBranchDescr.Text.Trim
        Dim BranchNodeName As String = txtNodeName.Text.Trim
        Dim BranchNodeDescr As String = txtNodeDescr.Text.Trim
        Dim BranchNodeUnits As String = txtNodeUnits.Text.Trim
        Dim BranchNodeUnitsAbbrev As String = txtNodeUnitsAbbrev.Text.Trim

        If BranchNodeName = "" Then
            Message.AddWarning("Please enter a name for the Branch node." & vbCrLf)
        Else
            If CalcInfo.ContainsKey(BranchNodeName) Then
                Message.AddWarning("The node name is already used: " & BranchNodeName & vbCrLf)
            Else
                If rbIfGt.Checked Then
                    CalcInfo.Add(BranchNodeName, New CalcOpInfo)
                    CalcInfo(BranchNodeName).Units = BranchNodeUnits
                    CalcInfo(BranchNodeName).UnitsAbbrev = BranchNodeUnitsAbbrev
                    CalcInfo(BranchNodeName).Description = BranchNodeDescr
                    CalcInfo(BranchNodeName).Type = "If Gt"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(BranchNodeName, BranchNodeName, 90, 91)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(BranchNodeName, BranchNodeName, 90, 91)
                    End If
                    ScalarData.Add(BranchNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbIfGtEq.Checked Then
                    CalcInfo.Add(BranchNodeName, New CalcOpInfo)
                    CalcInfo(BranchNodeName).Units = BranchNodeUnits
                    CalcInfo(BranchNodeName).UnitsAbbrev = BranchNodeUnitsAbbrev
                    CalcInfo(BranchNodeName).Description = BranchNodeDescr
                    CalcInfo(BranchNodeName).Type = "If GtEq"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(BranchNodeName, BranchNodeName, 92, 93)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(BranchNodeName, BranchNodeName, 92, 93)
                    End If
                    ScalarData.Add(BranchNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbIfEq.Checked Then
                    CalcInfo.Add(BranchNodeName, New CalcOpInfo)
                    CalcInfo(BranchNodeName).Units = BranchNodeUnits
                    CalcInfo(BranchNodeName).UnitsAbbrev = BranchNodeUnitsAbbrev
                    CalcInfo(BranchNodeName).Description = BranchNodeDescr
                    CalcInfo(BranchNodeName).Type = "If Eq"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(BranchNodeName, BranchNodeName, 86, 87)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(BranchNodeName, BranchNodeName, 86, 87)
                    End If
                    ScalarData.Add(BranchNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbIfLtEq.Checked Then
                    CalcInfo.Add(BranchNodeName, New CalcOpInfo)
                    CalcInfo(BranchNodeName).Units = BranchNodeUnits
                    CalcInfo(BranchNodeName).UnitsAbbrev = BranchNodeUnitsAbbrev
                    CalcInfo(BranchNodeName).Description = BranchNodeDescr
                    CalcInfo(BranchNodeName).Type = "If LtEq"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(BranchNodeName, BranchNodeName, 94, 95)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(BranchNodeName, BranchNodeName, 94, 95)
                    End If
                    ScalarData.Add(BranchNodeName, 1) 'This value will be overwritten when the sequence is run. 

                ElseIf rbIfLt.Checked Then
                    CalcInfo.Add(BranchNodeName, New CalcOpInfo)
                    CalcInfo(BranchNodeName).Units = BranchNodeUnits
                    CalcInfo(BranchNodeName).UnitsAbbrev = BranchNodeUnitsAbbrev
                    CalcInfo(BranchNodeName).Description = BranchNodeDescr
                    CalcInfo(BranchNodeName).Type = "If Lt"
                    If trvCalculations.SelectedNode Is Nothing Then
                        trvCalculations.Nodes.Add(BranchNodeName, BranchNodeName, 88, 89)
                    Else
                        trvCalculations.SelectedNode.Nodes.Add(BranchNodeName, BranchNodeName, 88, 89)
                    End If
                    ScalarData.Add(BranchNodeName, 1) 'This value will be overwritten when the sequence is run. 

                Else
                    Message.AddWarning("No node type has been selected." & vbCrLf)
                End If
            End If
        End If
    End Sub















    Private Sub ProcessCalcNode(ByVal xml_Node As System.Xml.XmlNode, ByVal tnc As TreeNodeCollection, ByVal Spaces As String, ByVal ParentNodeIsExpanded As Boolean)
        'Opening the Calculation Sequence. Process the Child Nodes in the Calculation Tree.
        'This method calls itself to process the child node branches.

        Dim NodeInfo As System.Xml.XmlNode
        Dim NodeText As String = ""
        Dim NodeKey As String = ""
        Dim IsExpanded As Boolean = True
        Dim HasNodes As Boolean = True

        Dim NodeType As String = ""

        For Each ChildNode As System.Xml.XmlNode In xml_Node.ChildNodes
            Dim myNodeText As System.Xml.XmlNode
            myNodeText = ChildNode.SelectSingleNode("Text")
            If IsNothing(myNodeText) Then
                '/Text node not found
            Else
                Dim myNodeTextValue As String = myNodeText.InnerText
                NodeKey = System.Xml.XmlConvert.DecodeName(ChildNode.Name)
                If CalcInfo.ContainsKey(NodeKey) Then
                    NodeType = CalcInfo(NodeKey).Type

                    'Read Calculation Node IsExpanded:
                    NodeInfo = ChildNode.SelectSingleNode("IsExpanded")
                    If NodeInfo Is Nothing Then
                        HasNodes = False
                        IsExpanded = True
                    Else
                        IsExpanded = NodeInfo.InnerText
                    End If

                    CalcInfo(NodeKey).Text = myNodeTextValue 'NOTE this is also loaded separately when the CalculationInfoList is read.

                    Select Case NodeType
                        Case "Calculation Sequence"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 82, 83) 'Add a node to the tree node collection: Key, Text, ImageIndex, SelectedImageIndex.
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Input Variable"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 0, 1)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Input Variable User Defined"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 2, 3)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Output Value"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 4, 5)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Process"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 6, 7)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Value Process"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 8, 9)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Collection"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 10, 11)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Value Copy"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 12, 13)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Constant Value E"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 14, 15)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Constant Value Pi"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 18, 19)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Constant Value User Defined"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 20, 21)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Add"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 22, 23)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Subtract"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 24, 25)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Multiply"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 26, 27)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Divide"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 28, 29)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Sum"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 30, 31)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Product"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 32, 33)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Sine"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 34, 35)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Cosine"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 36, 37)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Tangent"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 38, 39)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "ArcSine"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 40, 41)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "ArcCosine"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 42, 43)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "ArcTangent"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 44, 45)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Degrees To Radians"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 46, 47)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Radians To Degrees"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 48, 49)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Power Of Ten"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 54, 55)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Logarithm"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 56, 57)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Square"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 58, 59)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Square Root"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 60, 61)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Cube"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 62, 63)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Cube Root"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 64, 65)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Exponentiate"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 66, 67)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Root"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 68, 69)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Equals"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 84, 85)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Negate"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 70, 71)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Invert"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 72, 73)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Absolute"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 74, 75)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Round"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 76, 77)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Round Up"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 78, 79)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Round Down"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 80, 81)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "If Gt"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 90, 91)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "If GtEq"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 92, 93)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "If Eq"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 86, 87)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "If LtEq"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 94, 95)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "If Lt"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 88, 89)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessCalcNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case Else
                            Message.AddWarning("Unknown node type: " & NodeType & vbCrLf)
                    End Select

                End If

            End If

        Next

    End Sub


#End Region 'Calculations \ Create Tab --------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Calculations \ Edit Nodes" '=========================================================================================================================================================

    Private Sub btnMoveNodeUp_Click(sender As Object, e As EventArgs) Handles btnMoveNodeUp.Click
        'Move the selected item up in the Document Tree.

        If trvCalculations.SelectedNode Is Nothing Then
            'No node has been selected.
        Else
            Dim Node As TreeNode
            Node = trvCalculations.SelectedNode
            Dim index As Integer = Node.Index
            If index = 0 Then
                'Already at the first node.
                Node.TreeView.Focus()
            Else
                Dim Parent As TreeNode = Node.Parent
                Parent.Nodes.RemoveAt(index)
                Parent.Nodes.Insert(index - 1, Node)
                trvCalculations.SelectedNode = Node
                Node.TreeView.Focus()
                CalcSeqModified = True
            End If
        End If
    End Sub

    Private Sub btnMoveNodeDown_Click(sender As Object, e As EventArgs) Handles btnMoveNodeDown.Click
        'Move the selected item down in the Document Tree.

        If trvCalculations.SelectedNode Is Nothing Then
            'No node has been selected.
        Else
            Dim Node As TreeNode
            Node = trvCalculations.SelectedNode
            Dim index As Integer = Node.Index
            Dim Parent As TreeNode = Node.Parent
            If index < Parent.Nodes.Count - 1 Then
                Parent.Nodes.RemoveAt(index)
                Parent.Nodes.Insert(index + 1, Node)
                trvCalculations.SelectedNode = Node
                Node.TreeView.Focus()
                CalcSeqModified = True
            Else
                'Already at the last node.
                Node.TreeView.Focus()
            End If
        End If
    End Sub

    Private Sub btnCloneNode_Click(sender As Object, e As EventArgs) Handles btnCloneNode.Click
        'Clone the selected node 
        'This is done when the "Copy" button is pressed.

        If trvCalculations.SelectedNode Is Nothing Then
            Message.AddWarning("Please select a node to copy." & vbCrLf)
        Else
            CutNode = trvCalculations.SelectedNode.Clone
        End If
    End Sub

    Private Sub btnCutNode_Click(sender As Object, e As EventArgs) Handles btnCutNode.Click
        'Cut the selected node

        If trvCalculations.SelectedNode Is Nothing Then
            Message.AddWarning("Please select a node to cut." & vbCrLf)
        Else
            CutNode = trvCalculations.SelectedNode
            trvCalculations.SelectedNode.Remove()
            CalcSeqModified = True
        End If
    End Sub

    Private Sub btnPasteNode_Click(sender As Object, e As EventArgs) Handles btnPasteNode.Click
        'Paste the copied or cut node.

        If CutNode Is Nothing Then
            Message.AddWarning("Please cut or copy a node to paste." & vbCrLf)
        Else
            If trvCalculations.SelectedNode Is Nothing Then
                Message.AddWarning("Please select a node to paste to." & vbCrLf)
            Else
                trvCalculations.SelectedNode.Nodes.Add(CutNode)
                CalcSeqModified = True
            End If
        End If
    End Sub

    Private Sub btnDeleteNode_Click(sender As Object, e As EventArgs) Handles btnDeleteNode.Click
        'Delete the selected node.

        If ScalarData.ContainsKey(SelDataName) Then
            ScalarData.Remove(SelDataName)
        End If
        If ColumnInfo.ContainsKey(SelItemName) Then
            ColumnInfo.Remove(SelItemName)
        End If
        If SelDataName <> SelItemName Then 'This node contains a copy of data from the node named SelDataName
            If CalcInfo(SelItemName).CopyList.Contains(SelDataName) Then
                CalcInfo(SelItemName).CopyList.Remove(SelDataName)
            End If
        End If
        trvCalculations.SelectedNode.Remove()
        CalcSeqModified = True
    End Sub

    Private Sub btnApplyChanges_Click(sender As Object, e As EventArgs) Handles btnApplyChanges.Click
        'Apply the changes to the node.

        If CalcInfo.ContainsKey(SelItemName) Then
            'Update the Node Text if it has changed:
            If SelDataName = txtEditNodeText.Text.Trim Then
                'The Node Text has not been changed (This is also the ScalarData index)
            Else
                Dim NewSelDataName As String = txtEditNodeText.Text.Trim
                If ScalarData.ContainsKey(SelDataName) Then
                    If ScalarData.ContainsKey(NewSelDataName) Then
                        ScalarData.Add(NewSelDataName, ScalarData(SelDataName))
                        ScalarData.Remove(SelDataName)
                        SelDataName = NewSelDataName
                        SelNode.Text = SelDataName
                    Else
                        Message.AddWarning("Edit Failed - ScalarData already contains " & NewSelDataName & vbCrLf)
                        Exit Sub
                    End If
                Else
                    Message.AddWarning("ScalarData does not contain " & SelDataName & vbCrLf)
                End If
            End If

            CalcInfo(SelItemName).Description = txtEditNodeDescr.Text.Trim      'Update the Node description
            CalcInfo(SelItemName).Units = txtEditNodeUnits.Text.Trim            'Update the Node units.
            CalcInfo(SelItemName).UnitsAbbrev = txtEditNodeUnitsAbbrev.Text.Trim

            If CalcInfo(SelItemName).Type = "Output Value" Then
                'Update the Column Name if it has changed
                Dim NewColumnName As String = txtEditColumnName.Text.Trim
                If ColumnInfo(SelItemName).Name = NewColumnName Then
                    'The column name has not been changed
                Else
                    ColumnInfo(SelItemName).Name = NewColumnName
                    Message.Add("The Output Value column name has been changed from " & ColumnInfo(SelItemName).Name & " to " & NewColumnName & vbCrLf)
                End If
            End If

            CalcSeqModified = True
        Else
            Message.AddWarning("CalcInfo does not contain " & SelItemName & vbCrLf)
        End If
    End Sub

#End Region 'Calculations \ Edit Nodes --------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Data Table" '========================================================================================================================================================================

    Private Sub cmbMCTableName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMCTableName.SelectedIndexChanged
        'A different table has been selected for display.
        If cmbMCTableName.Focused Then
            MCTableName = cmbMCTableName.SelectedItem.ToString
        Else
            'Dont update the view if the selection has been made programmatically.
        End If
    End Sub

    Private Sub btnPlotChart_Click(sender As Object, e As EventArgs) Handles btnPlotChart.Click
        'Open a new Chart form and display the selected chart:

        If cmbChartList.SelectedIndex = -1 Then
            Message.AddWarning("Select a chart to plot." & vbCrLf)
        Else
            Dim ChartNo As Integer = OpenNewChart()
            ChartList(ChartNo).DataSource = MonteCarlo
            ChartList(ChartNo).ChartName = cmbChartList.SelectedItem.ToString
            ChartList(ChartNo).Plot
        End If
    End Sub

    Private Sub btnPaste_Click(sender As Object, e As EventArgs) Handles btnPaste.Click
        'Paste data from the clipboard into dgvResults
        'http://franshbotes.blogspot.com/2011/07/paste-excel-data-into-datagrid-in-vbnet.html

        Dim CBData As String
        Try
            CBData = Clipboard.GetText
            Dim I, J As Integer
            Dim Lines() As String = CBData.Split(ControlChars.NewLine)
            Dim Items() As String
            Dim CC, Row, Col As Integer
            Row = dgvResults.SelectedCells(0).RowIndex
            Col = dgvResults.SelectedCells(0).ColumnIndex

            For I = 0 To Lines.Length - 1
                If Lines(I) <> "" Then
                    Items = Lines(I).Split(vbTab)
                    CC = Col
                    For J = 0 To Items.Length - 1
                        If CC > dgvResults.ColumnCount - 1 Then Exit Sub
                        If Row > dgvResults.Rows.Count - 1 Then Exit Sub
                        dgvResults.Item(CC, Row).Value = Val(Items(J).TrimStart.Replace(",", ""))
                        CC += 1
                    Next
                    Row += 1
                End If
            Next
        Catch ex As Exception

        End Try

    End Sub


    Private Sub dgvResults_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvResults.DataError
        'Handle a data error in dgvResults

        Dim Row As Integer = e.RowIndex
        Dim Col As Integer = e.ColumnIndex

        Message.AddWarning("Data error in the Results table." & vbCrLf)
        dgvResults.Item(Col, Row).Value = ""

    End Sub

    Private Sub btnChartMonteCarlo_Click(sender As Object, e As EventArgs) Handles btnChartMonteCarlo.Click
        'Open a Chart form and select the Distribution data table.
        Dim ChartNo As Integer = OpenNewChart()
        ChartList(ChartNo).DataSource = MonteCarlo 'New coded - uses DataSource property
        ChartList(ChartNo).TableName = MCTableName
        ChartList(ChartNo).Plot
    End Sub


#End Region 'Data Table -----------------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Table Operations" '==================================================================================================================================================================

    Private Sub cmbCopyFromTable_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCopyFromTable.SelectedIndexChanged
        'The CopyFromTable selection has changed.
        'Update the list of avaialble columns.
        Dim TableName As String = cmbCopyFromTable.SelectedItem.ToString
        cmbColumnToCopy.Items.Clear()

        For Each Col As DataColumn In MonteCarlo.Data.Tables(TableName).Columns
            cmbColumnToCopy.Items.Add(Col.ColumnName)
        Next
    End Sub

    Private Sub cmbColumnToCopy_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbColumnToCopy.SelectedIndexChanged
        If txtColumnList.Text.Trim = "" Then
            txtColumnList.Text = cmbColumnToCopy.SelectedItem.ToString
        Else
            txtColumnList.Text = txtColumnList.Text & ", " & cmbColumnToCopy.SelectedItem.ToString
        End If
        If ColList Is Nothing Then
            ReDim Preserve ColList(0 To 0)
        Else
            ReDim Preserve ColList(UBound(ColList) + 1)
        End If
        ColList(UBound(ColList)) = cmbColumnToCopy.SelectedItem.ToString
    End Sub

    Private Sub btnClearColList_Click(sender As Object, e As EventArgs)
        txtColumnList.Text = ""
        ColList = Nothing
    End Sub

    Private Sub btnCopyCol_Click(sender As Object, e As EventArgs) Handles btnCopyCol.Click
        'Copy a column to another table.

        If cmbCopyFromTable.SelectedIndex = -1 Then
            Message.AddWarning("Please select a table to copy the column from." & vbCrLf)
            Exit Sub
        End If

        If cmbColumnToCopy.SelectedIndex = -1 Then
            Message.AddWarning("Please select a column to copy." & vbCrLf)
            Exit Sub
        End If

        If rbCopyToTable.Checked Then
            'If cmbColumnToCopy.SelectedIndex = -1 Then
            If cmbCopyToTable.SelectedIndex = -1 Then
                Message.AddWarning("Please select a table to copy the column to." & vbCrLf)
            Else
                Dim FromTableName As String = cmbCopyFromTable.SelectedItem.ToString
                Dim ToTableName As String = cmbCopyToTable.SelectedItem.ToString
                MonteCarlo.CopyColumn(FromTableName, ColList, ToTableName)
                'If RecordSequence = True Then
                '    Try
                '        Sequence.XmlHtmDisplay1.SelectedText = "  <CopyMonteCarloColumnToTable>" & vbCrLf _
                '     & "    <FromTableName>" & FromTableName & "</FromTableName>" & vbCrLf _
                '     & "    <ColumnList>" & txtColumnList.Text & "</ColumnList>" & vbCrLf _
                '     & "    <ToTableName>" & ToTableName & "</ToTableName>" & vbCrLf _
                '     & "    <Command>OK</Command>" & vbCrLf _
                '     & "  </CopyMonteCarloColumnToTable>" & vbCrLf
                '    Catch ex As Exception
                '        Message.AddWarning("Error recording Copy Column sequence: " & vbCrLf & ex.Message & vbCrLf)
                '    End Try
                'End If
            End If
        Else
            If txtNewTableName.Text.Trim = "" Then
                Message.AddWarning("Please enter a name for the new table." & vbCrLf)
            Else
                Dim FromTablename As String = cmbCopyFromTable.SelectedItem.ToString
                Dim NewTableName As String = txtNewTableName.Text.Trim
                MonteCarlo.CopyColumn(FromTablename, ColList, NewTableName)
                UpdateTableList()
                'If RecordSequence = True Then
                '    Try
                '        Sequence.XmlHtmDisplay1.SelectedText = "  <CopyMonteCarloColumnToTable>" & vbCrLf _
                '     & "    <FromTableName>" & FromTablename & "</FromTableName>" & vbCrLf _
                '     & "    <ColumnList>" & txtColumnList.Text & "</ColumnList>" & vbCrLf _
                '     & "    <NewTableName>" & NewTableName & "</NewTableName>" & vbCrLf _
                '     & "    <Command>OK</Command>" & vbCrLf _
                '     & "  </CopyMonteCarloColumnToTable>" & vbCrLf
                '    Catch ex As Exception
                '        Message.AddWarning("Error recording Copy Column sequence: " & vbCrLf & ex.Message & vbCrLf)
                '    End Try
                'End If
            End If
        End If
    End Sub

    Private Sub cmbCopyColFromTable_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCopyColFromTable.SelectedIndexChanged
        'The CopyColFromTable selection has changed.
        'Update the list of avaialble columns.
        Dim TableName As String = cmbCopyColFromTable.SelectedItem.ToString
        cmbSingleColToCopy.Items.Clear()

        For Each Col As DataColumn In MonteCarlo.Data.Tables(TableName).Columns
            cmbSingleColToCopy.Items.Add(Col.ColumnName)
        Next
    End Sub

    Private Sub chkNewColName_CheckedChanged(sender As Object, e As EventArgs) Handles chkNewColName.CheckedChanged
        If chkNewColName.Checked Then
            txtNewColName.Enabled = True
        Else
            txtNewColName.Enabled = False
        End If
    End Sub

    Private Sub btnCopySingleCol_Click(sender As Object, e As EventArgs) Handles btnCopySingleCol.Click
        'Copy a single column to another table.

        If cmbCopyColFromTable.SelectedIndex = -1 Then
            Message.AddWarning("Please select a table to copy the single column from." & vbCrLf)
            Exit Sub
        End If

        If cmbSingleColToCopy.SelectedIndex = -1 Then
            Message.AddWarning("Please select a single column to copy." & vbCrLf)
            Exit Sub
        End If


        If rbCopyColToTable.Checked Then
            If cmbCopyColToTable.SelectedIndex = -1 Then
                Message.AddWarning("Please select a table to copy the column to." & vbCrLf)
            Else
                Dim FromTableName As String = cmbCopyColFromTable.SelectedItem.ToString
                Dim ToTableName As String = cmbCopyColToTable.SelectedItem.ToString
                If chkNewColName.Checked Then
                    Dim NewColName As String = txtNewColName.Text.Trim
                    MonteCarlo.CopyColumn(FromTableName, cmbSingleColToCopy.SelectedItem.ToString, ToTableName, NewColName)
                Else 'Use the same column name in the copy.
                    'MonteCarlo.CopyColumn(FromTableName, ColList, ToTableName)
                    MonteCarlo.CopyColumn(FromTableName, cmbSingleColToCopy.SelectedItem.ToString, ToTableName)
                End If
            End If
        Else 'Copy the column to a new table
            If txtNewCopyColTable.Text.Trim = "" Then
                Message.AddWarning("Please enter a name for the new table." & vbCrLf)
            Else
                Dim FromTableName As String = cmbCopyColFromTable.SelectedItem.ToString
                Dim ToTableName As String = txtNewCopyColTable.Text.Trim
                If chkNewColName.Checked Then
                    Dim NewColName As String = txtNewColName.Text.Trim
                    MonteCarlo.CopyColumn(FromTableName, cmbSingleColToCopy.SelectedItem.ToString, ToTableName, NewColName)
                Else 'Use the same column name in the copy.
                    'MonteCarlo.CopyColumn(FromTableName, ColList, ToTableName)
                    MonteCarlo.CopyColumn(FromTableName, cmbSingleColToCopy.SelectedItem.ToString, ToTableName)
                End If
            End If
        End If
    End Sub

    Private Sub cmbSortTable_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSortTable.SelectedIndexChanged
        'The SortTable selection has changed.
        'Update the list of avaialble columns.
        Dim TableName As String = cmbSortTable.SelectedItem.ToString
        cmbSortColumn.Items.Clear()

        For Each Col As DataColumn In MonteCarlo.Data.Tables(TableName).Columns
            cmbSortColumn.Items.Add(Col.ColumnName)
        Next
    End Sub

    Private Sub btnSortCol_Click(sender As Object, e As EventArgs) Handles btnSortCol.Click
        'Sort the selected column
        If cmbSortTable.SelectedIndex = -1 Then
            Message.AddWarning("Select a table to sort." & vbCrLf)
            Exit Sub
        End If

        If cmbSortColumn.SelectedIndex = -1 Then
            Message.AddWarning("Select a column to sort." & vbCrLf)
            Exit Sub
        End If

        Dim TableName As String = cmbSortTable.SelectedItem.ToString
        Dim ColName As String = cmbSortColumn.SelectedItem.ToString

        If rbSortAscending.Checked Then
            'MonteCarlo.Data.Tables(TableName).DefaultView.Sort = ColName & " ASC"
            MonteCarlo.SortColumn(TableName, ColName, True)
        Else
            'MonteCarlo.Data.Tables(TableName).DefaultView.Sort = ColName & " DESC"
            MonteCarlo.SortColumn(TableName, ColName, False)
        End If

        If MCTableName = TableName Then UpdateMCDataTableView()
    End Sub

    Private Sub btnShuffleCol_Click(sender As Object, e As EventArgs) Handles btnShuffleCol.Click
        'Shuffle the selected column
        If cmbSortTable.SelectedIndex = -1 Then
            Message.AddWarning("Select a table." & vbCrLf)
            Exit Sub
        End If

        If cmbSortColumn.SelectedIndex = -1 Then
            Message.AddWarning("Select a column to sort." & vbCrLf)
            Exit Sub
        End If

        Dim TableName As String = cmbSortTable.SelectedItem.ToString
        Dim ColName As String = cmbSortColumn.SelectedItem.ToString

        MonteCarlo.ShuffleColumn(TableName, ColName)
        If MCTableName = TableName Then UpdateMCDataTableView()
    End Sub

    Private Sub btnDeleteCol_Click(sender As Object, e As EventArgs) Handles btnDeleteCol.Click
        'Delete the selected column

        If cmbSortTable.SelectedIndex = -1 Then
            Message.AddWarning("Select a table." & vbCrLf)
            Exit Sub
        End If

        If cmbSortColumn.SelectedIndex = -1 Then
            Message.AddWarning("Select a column to delete." & vbCrLf)
            Exit Sub
        End If

        Dim TableName As String = cmbSortTable.SelectedItem.ToString
        Dim ColName As String = cmbSortColumn.SelectedItem.ToString

        MonteCarlo.Data.Tables(TableName).Columns.Remove(ColName)


    End Sub

    Private Sub btnCreateTable_Click(sender As Object, e As EventArgs) Handles btnCreateTable.Click
        'Create a new table.

        If txtCreateTableName.Text.Trim = "" Then
            Message.AddWarning("Please enter a name for the new table." & vbCrLf)
        Else
            Dim CreateTableName As String = txtCreateTableName.Text.Trim
            If MonteCarlo.Data.Tables.Contains(CreateTableName) Then
                Message.AddWarning("The table name is already used." & vbCrLf)
            Else
                MonteCarlo.Data.Tables.Add(CreateTableName)
                UpdateTableList()
            End If
        End If
    End Sub

    Private Sub btnDeleteTable_Click(sender As Object, e As EventArgs) Handles btnDeleteTable.Click
        'Delete a table

        'If cmbSortTable.SelectedIndex = -1 Then
        If cmbDeleteTable.SelectedIndex = -1 Then
            Message.AddWarning("Select a table to delete." & vbCrLf)
            Exit Sub
        End If

        'Dim TableName As String = cmbSortTable.SelectedItem.ToString
        Dim TableName As String = cmbDeleteTable.SelectedItem.ToString
        If MonteCarlo.Data.Tables.Contains(TableName) Then
            MonteCarlo.Data.Tables.Remove(TableName)
            UpdateTableList()
        Else

        End If


    End Sub

    Private Sub btnClearTableData_Click(sender As Object, e As EventArgs) Handles btnClearTableData.Click
        'Clear the data in a table

        If cmbDeleteTable.SelectedIndex = -1 Then
            Message.AddWarning("Select a table to delete." & vbCrLf)
            Exit Sub
        End If

        Dim TableName As String = cmbDeleteTable.SelectedItem.ToString
        If MonteCarlo.Data.Tables.Contains(TableName) Then
            MonteCarlo.Data.Tables(TableName).Rows.Clear()
        End If

    End Sub

    Private Sub btnClearColumns_Click(sender As Object, e As EventArgs) Handles btnClearColumns.Click
        'Clear the columns in a table

        If cmbDeleteTable.SelectedIndex = -1 Then
            Message.AddWarning("Select a table to delete." & vbCrLf)
            Exit Sub
        End If

        Dim TableName As String = cmbDeleteTable.SelectedItem.ToString
        If MonteCarlo.Data.Tables.Contains(TableName) Then
            MonteCarlo.Data.Tables(TableName).Columns.Clear()
        End If
    End Sub

    Private Sub cmbCopyDataFromTable_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCopyDataFromTable.SelectedIndexChanged
        'The cmbCopyDataFromTable selection has changed.
        'Update the list of avaialble columns.
        Dim TableName As String = cmbCopyDataFromTable.SelectedItem.ToString
        cmbCopyDataFromColumn.Items.Clear()

        For Each Col As DataColumn In MonteCarlo.Data.Tables(TableName).Columns
            cmbCopyDataFromColumn.Items.Add(Col.ColumnName)
        Next
    End Sub

    Private Sub cmbCopyDataToTable_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCopyDataToTable.SelectedIndexChanged
        'The cmbCopyDataToTable selection has changed.
        'Update the list of avaialble columns.
        Dim TableName As String = cmbCopyDataToTable.SelectedItem.ToString
        cmbCopyDataToColumn.Items.Clear()

        For Each Col As DataColumn In MonteCarlo.Data.Tables(TableName).Columns
            cmbCopyDataToColumn.Items.Add(Col.ColumnName)
        Next
    End Sub


    Private Sub btnCopyColData_Click(sender As Object, e As EventArgs) Handles btnCopyColData.Click
        'Copy the data from the selected column in one table to another selected column in another table.

        If cmbCopyDataFromTable.SelectedIndex = -1 Then
            Message.AddWarning("Please select a table to copy the column data from." & vbCrLf)
            Exit Sub
        End If

        If cmbCopyDataFromColumn.SelectedIndex = -1 Then
            Message.AddWarning("Please select a column to copy the data from." & vbCrLf)
            Exit Sub
        End If

        If cmbCopyDataToTable.SelectedIndex = -1 Then
            Message.AddWarning("Please select a table to copy the column data to." & vbCrLf)
            Exit Sub
        End If

        If cmbCopyDataToColumn.SelectedIndex = -1 Then
            Message.AddWarning("Please select a column to copy the data to." & vbCrLf)
            Exit Sub
        End If

        Dim FromTableName As String = cmbCopyDataFromTable.SelectedItem.ToString
        Dim FromColumnName As String = cmbCopyDataFromColumn.SelectedItem.ToString
        Dim ToTableName As String = cmbCopyDataToTable.SelectedItem.ToString
        Dim ToColumnName As String = cmbCopyDataToColumn.SelectedItem.ToString

        'Dim sortedTable As System.Data.DataTable = MonteCarlo.Data.Tables(FromTableName).DefaultView.ToTable("SortedTable", False, FromColumnName)
        'Dim I As Integer
        'For I = 0 To sortedTable.Rows.Count - 1
        '    MonteCarlo.Data.Tables(ToTableName).Rows(I).Item(ToColumnName) = sortedTable.Rows(I).Item(FromColumnName)
        'Next
        MonteCarlo.CopyColumnData(FromTableName, FromColumnName, ToTableName, ToColumnName)
    End Sub

















#End Region 'Table Operations -----------------------------------------------------------------------------------------------------------------------------------------------------------------

    Private Sub MonteCarlo_DataLoaded() Handles MonteCarlo.DataLoaded

    End Sub

    Private Sub MonteCarlo_ErrorMessage(Msg As String) Handles MonteCarlo.ErrorMessage
        Message.AddWarning(Msg)
    End Sub

    Private Sub MonteCarlo_Message(Msg As String) Handles MonteCarlo.Message
        Message.Add(Msg)
    End Sub



#End Region 'Monte Carlo Methods --------------------------------------------------------------------------------------------------------------------------------------------------------------



#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Events - Events raised by this form." '=========================================================================================================================================

#End Region 'Form Events ----------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Classes - Other classes used by this form." '===================================================================================================================================

    Public Class clsSendMessageParams
        'Parameters used when sending a message using the Message Service.
        Public ProjectNetworkName As String
        Public ConnectionName As String
        Public Message As String
    End Class

    Public Class clsInstructionParams
        'Parameters used when executing an instruction.
        Public Info As String 'The information in an instruction.
        Public Locn As String 'The location to send the information.
    End Class

    Private Sub txtCorrMatrixFormat_TextChanged(sender As Object, e As EventArgs) Handles txtCorrMatrixFormat.TextChanged

    End Sub



#End Region 'Form Classes ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


End Class


