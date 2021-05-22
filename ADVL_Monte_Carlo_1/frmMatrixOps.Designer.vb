<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMatrixOps
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMatrixOps))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnFormatHelp = New System.Windows.Forms.Button()
        Me.txtInputMatrixFormat = New System.Windows.Forms.TextBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtInputMatrixName = New System.Windows.Forms.TextBox()
        Me.btnPasteInputMatrix = New System.Windows.Forms.Button()
        Me.btnCopyInputMatrix = New System.Windows.Forms.Button()
        Me.btnSaveInputMatrix = New System.Windows.Forms.Button()
        Me.btnNewInputMatrix = New System.Windows.Forms.Button()
        Me.btnOpenInputMatrix = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtInputMatrixNCols = New System.Windows.Forms.TextBox()
        Me.txtInputMatrixNRows = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtInputMatrixDescr = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtInputMatrixFileName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvInputMatrix = New System.Windows.Forms.DataGridView()
        Me.txtOutputMatrixFormat = New System.Windows.Forms.TextBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtOutputMatrixFileName = New System.Windows.Forms.TextBox()
        Me.btnCopyOutputMatrix = New System.Windows.Forms.Button()
        Me.btnSaveOutputMatrix = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtOutputMatrixNCols = New System.Windows.Forms.TextBox()
        Me.txtOutputMatrixNRows = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnApplyOneMatrixOp = New System.Windows.Forms.Button()
        Me.cmbOneMatrixOps = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtOutputMatrixDescr = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtOutputMatrixName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dgvOutputMatrix = New System.Windows.Forms.DataGridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.txtInputMatrix1Format = New System.Windows.Forms.TextBox()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtInputMatrix1Name = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtInputMatrix1NCols = New System.Windows.Forms.TextBox()
        Me.txtInputMatrix1NRows = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtInputMatrix1Descr = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtInputMatrix1FileName = New System.Windows.Forms.TextBox()
        Me.dgvInputMatrix1 = New System.Windows.Forms.DataGridView()
        Me.btnPasteInputMatrix1 = New System.Windows.Forms.Button()
        Me.btnCopyInputMatrix1 = New System.Windows.Forms.Button()
        Me.btnSaveInputMatrix1 = New System.Windows.Forms.Button()
        Me.btnNewInputMatrix1 = New System.Windows.Forms.Button()
        Me.btnOpenInputMatrix1 = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtInputMatrix2Format = New System.Windows.Forms.TextBox()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtInputMatrix2Name = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtInputMatrix2NCols = New System.Windows.Forms.TextBox()
        Me.txtInputMatrix2NRows = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtInputMatrix2Descr = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtInputMatrix2FileName = New System.Windows.Forms.TextBox()
        Me.dgvInputMatrix2 = New System.Windows.Forms.DataGridView()
        Me.btnPasteInputMatrix2 = New System.Windows.Forms.Button()
        Me.btnCopyInputMatrix2 = New System.Windows.Forms.Button()
        Me.btnSaveInputMatrix2 = New System.Windows.Forms.Button()
        Me.btnNewInputMatrix2 = New System.Windows.Forms.Button()
        Me.btnOpenInputMatrix2 = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtOutputCalcMatrixFormat = New System.Windows.Forms.TextBox()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtOutputCalcMatrixDescr = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtOutputCalcMatrixName = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtOutputCalcMatrixNCols = New System.Windows.Forms.TextBox()
        Me.txtOutputCalcMatrixNRows = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.txtOutputCalcMatrixFileName = New System.Windows.Forms.TextBox()
        Me.dgvOutputCalcMatrix = New System.Windows.Forms.DataGridView()
        Me.btnCopyOutputCalcMatrix = New System.Windows.Forms.Button()
        Me.btnSaveOutputCalcMatrix = New System.Windows.Forms.Button()
        Me.btnApplyTwoMatrixOp = New System.Windows.Forms.Button()
        Me.cmbTwoMatrixOp = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.btnUpdateList = New System.Windows.Forms.Button()
        Me.txtSelMatrixName = New System.Windows.Forms.TextBox()
        Me.txtSelMatrixNCols = New System.Windows.Forms.TextBox()
        Me.txtSelMatrixNRows = New System.Windows.Forms.TextBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.txtSelMatrixDescr = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.btnDeleteMatrix = New System.Windows.Forms.Button()
        Me.btnCopyMatrix = New System.Windows.Forms.Button()
        Me.btnOpenMatrix = New System.Windows.Forms.Button()
        Me.lstMatrices = New System.Windows.Forms.ListBox()
        Me.SplitContainer6 = New System.Windows.Forms.SplitContainer()
        Me.txtMatrixFormat = New System.Windows.Forms.TextBox()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.dgvMatrix = New System.Windows.Forms.DataGridView()
        Me.txtMatrixName = New System.Windows.Forms.TextBox()
        Me.txtMatrixDescr = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.txtMatrixFileName = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.txtMatrixNRows = New System.Windows.Forms.TextBox()
        Me.txtMatrixNCols = New System.Windows.Forms.TextBox()
        Me.txtSymmetric = New System.Windows.Forms.TextBox()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.txtHermitian = New System.Windows.Forms.TextBox()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.txtDeterminant = New System.Windows.Forms.TextBox()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.txtRank = New System.Windows.Forms.TextBox()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.SplitContainer5 = New System.Windows.Forms.SplitContainer()
        Me.txtSeqDescr = New System.Windows.Forms.TextBox()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.txtSeqName = New System.Windows.Forms.TextBox()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.btnNewSeq = New System.Windows.Forms.Button()
        Me.btnSaveSeq = New System.Windows.Forms.Button()
        Me.btnOpenSeq = New System.Windows.Forms.Button()
        Me.txtSeqFileName = New System.Windows.Forms.TextBox()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.trvMatrixOps = New System.Windows.Forms.TreeView()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.btnShowItem = New System.Windows.Forms.Button()
        Me.txtScalarName = New System.Windows.Forms.TextBox()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btnPasteData = New System.Windows.Forms.Button()
        Me.btnCalcMatrixItem = New System.Windows.Forms.Button()
        Me.btnFormatHelp2 = New System.Windows.Forms.Button()
        Me.txtMatrixItemFileName = New System.Windows.Forms.TextBox()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.btnPasteMatrixItem = New System.Windows.Forms.Button()
        Me.btnCopyMatrixItem = New System.Windows.Forms.Button()
        Me.btnSaveMatrixItem = New System.Windows.Forms.Button()
        Me.btnNewMatrixItem = New System.Windows.Forms.Button()
        Me.btnOpenMatrixItem = New System.Windows.Forms.Button()
        Me.txtMatrixItemFormat = New System.Windows.Forms.TextBox()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.dgvMatrixItem = New System.Windows.Forms.DataGridView()
        Me.txtMatrixItemName = New System.Windows.Forms.TextBox()
        Me.txtMatrixItemDescr = New System.Windows.Forms.TextBox()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.txtMatrixItemNRows = New System.Windows.Forms.TextBox()
        Me.txtMatrixItemNCols = New System.Windows.Forms.TextBox()
        Me.txtScalarItem = New System.Windows.Forms.TextBox()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.txtItemStatus = New System.Windows.Forms.TextBox()
        Me.txtItemDescription = New System.Windows.Forms.TextBox()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.txtItemType = New System.Windows.Forms.TextBox()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.cmbDataSource = New System.Windows.Forms.ComboBox()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.btnReplaceSpecialNode = New System.Windows.Forms.Button()
        Me.btnInsertSpecialNode = New System.Windows.Forms.Button()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.txtSpecialNodeDescr = New System.Windows.Forms.TextBox()
        Me.txtSpecialNodeName = New System.Windows.Forms.TextBox()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.btnAppendSpecialNode = New System.Windows.Forms.Button()
        Me.rbCollection = New System.Windows.Forms.RadioButton()
        Me.pbIconCollection = New System.Windows.Forms.PictureBox()
        Me.rbMatrixCopy = New System.Windows.Forms.RadioButton()
        Me.pbIconMatrixCopy = New System.Windows.Forms.PictureBox()
        Me.rbScalarCopy = New System.Windows.Forms.RadioButton()
        Me.pbIconScalarCopy = New System.Windows.Forms.PictureBox()
        Me.txtNodeInfo = New System.Windows.Forms.TextBox()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnReplaceTwoMatrixOp = New System.Windows.Forms.Button()
        Me.btnInsertTwoMatrixOp = New System.Windows.Forms.Button()
        Me.txtTwoMatrixOpDescr = New System.Windows.Forms.TextBox()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.txtTwoMatrixOpName = New System.Windows.Forms.TextBox()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.btnAppendTwoMatrixOp = New System.Windows.Forms.Button()
        Me.pbIconMatrixMultMatrix = New System.Windows.Forms.PictureBox()
        Me.pbIconMatrixAddMatrix = New System.Windows.Forms.PictureBox()
        Me.rbMultMatrix = New System.Windows.Forms.RadioButton()
        Me.rbAddMatrix = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rbCorrelation = New System.Windows.Forms.RadioButton()
        Me.rbCovariance = New System.Windows.Forms.RadioButton()
        Me.pbIconCorrelation = New System.Windows.Forms.PictureBox()
        Me.pbIconCovariance = New System.Windows.Forms.PictureBox()
        Me.btnReplaceOneMatrixOp = New System.Windows.Forms.Button()
        Me.btnInsertOneMatrixOp = New System.Windows.Forms.Button()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.txtOneMatrixOpDescr = New System.Windows.Forms.TextBox()
        Me.txtOneMatrixOpName = New System.Windows.Forms.TextBox()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.btnAppendOneMatrixOp = New System.Windows.Forms.Button()
        Me.pbIconMatrixDivScalar = New System.Windows.Forms.PictureBox()
        Me.pbIconMatrixMultScalar = New System.Windows.Forms.PictureBox()
        Me.pbIconMatrixAddScalar = New System.Windows.Forms.PictureBox()
        Me.pbIconMatrixTransChol = New System.Windows.Forms.PictureBox()
        Me.pbIconMatrixCholesky = New System.Windows.Forms.PictureBox()
        Me.pbIconMatrixInverse = New System.Windows.Forms.PictureBox()
        Me.pbIconMatrixTranspose = New System.Windows.Forms.PictureBox()
        Me.rbDivScalar = New System.Windows.Forms.RadioButton()
        Me.rbMultScalar = New System.Windows.Forms.RadioButton()
        Me.rbAddScalar = New System.Windows.Forms.RadioButton()
        Me.rbTransCholesky = New System.Windows.Forms.RadioButton()
        Me.rbCholesky = New System.Windows.Forms.RadioButton()
        Me.rbInverse = New System.Windows.Forms.RadioButton()
        Me.rbTranspose = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnReplaceData = New System.Windows.Forms.Button()
        Me.btnInsertData = New System.Windows.Forms.Button()
        Me.rbProcess = New System.Windows.Forms.RadioButton()
        Me.pbIconProcess = New System.Windows.Forms.PictureBox()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.txtDataDescr = New System.Windows.Forms.TextBox()
        Me.txtDataName = New System.Windows.Forms.TextBox()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.rbMatrixProcess = New System.Windows.Forms.RadioButton()
        Me.pbIconScalarProcess = New System.Windows.Forms.PictureBox()
        Me.rbScalarProcess = New System.Windows.Forms.RadioButton()
        Me.pbIconMatrixProcess = New System.Windows.Forms.PictureBox()
        Me.pbIconMatrixPreDefScalar = New System.Windows.Forms.PictureBox()
        Me.pbIconMatrixUserDefScalar = New System.Windows.Forms.PictureBox()
        Me.pbIconMatrixOpen = New System.Windows.Forms.PictureBox()
        Me.pbIconMatrixUserDef = New System.Windows.Forms.PictureBox()
        Me.pbIconMatrix = New System.Windows.Forms.PictureBox()
        Me.rbUserDefScalar = New System.Windows.Forms.RadioButton()
        Me.rbScalar = New System.Windows.Forms.RadioButton()
        Me.rbMatrix = New System.Windows.Forms.RadioButton()
        Me.btnAppendData = New System.Windows.Forms.Button()
        Me.rbUserDefMatrix = New System.Windows.Forms.RadioButton()
        Me.rbOpenMatrixFile = New System.Windows.Forms.RadioButton()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.btnCleanupNodeList = New System.Windows.Forms.Button()
        Me.btnShowNodeList = New System.Windows.Forms.Button()
        Me.btnCheckNodeCopyLists = New System.Windows.Forms.Button()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.txtEditNodeDataType = New System.Windows.Forms.TextBox()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.btnApplyChanges = New System.Windows.Forms.Button()
        Me.txtEditNodeDescr = New System.Windows.Forms.TextBox()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.txtEditNodeText = New System.Windows.Forms.TextBox()
        Me.txtEditNodeType = New System.Windows.Forms.TextBox()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.txtEditNodeName = New System.Windows.Forms.TextBox()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.btnMoveNodeDown = New System.Windows.Forms.Button()
        Me.btnMoveNodeUp = New System.Windows.Forms.Button()
        Me.btnCloneNode = New System.Windows.Forms.Button()
        Me.btnPasteNode = New System.Windows.Forms.Button()
        Me.btnCutNode = New System.Windows.Forms.Button()
        Me.btnListChildNodes = New System.Windows.Forms.Button()
        Me.btnDeleteNode = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1_OpenNode = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.dgvInputMatrix, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvOutputMatrix, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.dgvInputMatrix1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvInputMatrix2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvOutputCalcMatrix, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        CType(Me.SplitContainer4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        CType(Me.SplitContainer6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer6.Panel1.SuspendLayout()
        Me.SplitContainer6.Panel2.SuspendLayout()
        Me.SplitContainer6.SuspendLayout()
        CType(Me.dgvMatrix, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.SplitContainer5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer5.Panel1.SuspendLayout()
        Me.SplitContainer5.Panel2.SuspendLayout()
        Me.SplitContainer5.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.dgvMatrixItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.pbIconCollection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbIconMatrixCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbIconScalarCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.pbIconMatrixMultMatrix, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbIconMatrixAddMatrix, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.pbIconCorrelation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbIconCovariance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbIconMatrixDivScalar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbIconMatrixMultScalar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbIconMatrixAddScalar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbIconMatrixTransChol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbIconMatrixCholesky, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbIconMatrixInverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbIconMatrixTranspose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.pbIconProcess, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbIconScalarProcess, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbIconMatrixProcess, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbIconMatrixPreDefScalar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbIconMatrixUserDefScalar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbIconMatrixOpen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbIconMatrixUserDef, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbIconMatrix, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage7.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(12, 40)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1090, 643)
        Me.TabControl1.TabIndex = 11
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.SplitContainer1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1082, 617)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "One Matrix Operations"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnFormatHelp)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtInputMatrixFormat)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label43)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label12)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtInputMatrixName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnPasteInputMatrix)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnCopyInputMatrix)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnSaveInputMatrix)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNewInputMatrix)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnOpenInputMatrix)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label10)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtInputMatrixNCols)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtInputMatrixNRows)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtInputMatrixDescr)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtInputMatrixFileName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvInputMatrix)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtOutputMatrixFormat)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label44)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label13)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtOutputMatrixFileName)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCopyOutputMatrix)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSaveOutputMatrix)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label11)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtOutputMatrixNCols)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtOutputMatrixNRows)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label9)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label7)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnApplyOneMatrixOp)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cmbOneMatrixOps)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label5)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtOutputMatrixDescr)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label4)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtOutputMatrixName)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgvOutputMatrix)
        Me.SplitContainer1.Size = New System.Drawing.Size(1076, 611)
        Me.SplitContainer1.SplitterDistance = 465
        Me.SplitContainer1.TabIndex = 0
        '
        'btnFormatHelp
        '
        Me.btnFormatHelp.Location = New System.Drawing.Point(358, 111)
        Me.btnFormatHelp.Name = "btnFormatHelp"
        Me.btnFormatHelp.Size = New System.Drawing.Size(21, 22)
        Me.btnFormatHelp.TabIndex = 21
        Me.btnFormatHelp.Text = "?"
        Me.btnFormatHelp.UseVisualStyleBackColor = True
        '
        'txtInputMatrixFormat
        '
        Me.txtInputMatrixFormat.Location = New System.Drawing.Point(288, 113)
        Me.txtInputMatrixFormat.Name = "txtInputMatrixFormat"
        Me.txtInputMatrixFormat.Size = New System.Drawing.Size(64, 20)
        Me.txtInputMatrixFormat.TabIndex = 20
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(240, 116)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(42, 13)
        Me.Label43.TabIndex = 19
        Me.Label43.Text = "Format:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(12, 64)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(26, 13)
        Me.Label12.TabIndex = 18
        Me.Label12.Text = "File:"
        '
        'txtInputMatrixName
        '
        Me.txtInputMatrixName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInputMatrixName.Location = New System.Drawing.Point(55, 87)
        Me.txtInputMatrixName.Name = "txtInputMatrixName"
        Me.txtInputMatrixName.Size = New System.Drawing.Size(407, 20)
        Me.txtInputMatrixName.TabIndex = 17
        '
        'btnPasteInputMatrix
        '
        Me.btnPasteInputMatrix.Location = New System.Drawing.Point(284, 6)
        Me.btnPasteInputMatrix.Name = "btnPasteInputMatrix"
        Me.btnPasteInputMatrix.Size = New System.Drawing.Size(48, 22)
        Me.btnPasteInputMatrix.TabIndex = 16
        Me.btnPasteInputMatrix.Text = "Paste"
        Me.btnPasteInputMatrix.UseVisualStyleBackColor = True
        '
        'btnCopyInputMatrix
        '
        Me.btnCopyInputMatrix.Location = New System.Drawing.Point(233, 6)
        Me.btnCopyInputMatrix.Name = "btnCopyInputMatrix"
        Me.btnCopyInputMatrix.Size = New System.Drawing.Size(45, 22)
        Me.btnCopyInputMatrix.TabIndex = 15
        Me.btnCopyInputMatrix.Text = "Copy"
        Me.btnCopyInputMatrix.UseVisualStyleBackColor = True
        '
        'btnSaveInputMatrix
        '
        Me.btnSaveInputMatrix.Location = New System.Drawing.Point(182, 6)
        Me.btnSaveInputMatrix.Name = "btnSaveInputMatrix"
        Me.btnSaveInputMatrix.Size = New System.Drawing.Size(45, 22)
        Me.btnSaveInputMatrix.TabIndex = 14
        Me.btnSaveInputMatrix.Text = "Save"
        Me.btnSaveInputMatrix.UseVisualStyleBackColor = True
        '
        'btnNewInputMatrix
        '
        Me.btnNewInputMatrix.Location = New System.Drawing.Point(131, 6)
        Me.btnNewInputMatrix.Name = "btnNewInputMatrix"
        Me.btnNewInputMatrix.Size = New System.Drawing.Size(45, 22)
        Me.btnNewInputMatrix.TabIndex = 13
        Me.btnNewInputMatrix.Text = "New"
        Me.btnNewInputMatrix.UseVisualStyleBackColor = True
        '
        'btnOpenInputMatrix
        '
        Me.btnOpenInputMatrix.Location = New System.Drawing.Point(80, 6)
        Me.btnOpenInputMatrix.Name = "btnOpenInputMatrix"
        Me.btnOpenInputMatrix.Size = New System.Drawing.Size(45, 22)
        Me.btnOpenInputMatrix.TabIndex = 12
        Me.btnOpenInputMatrix.Text = "Open"
        Me.btnOpenInputMatrix.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(11, 90)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(38, 13)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Name:"
        '
        'txtInputMatrixNCols
        '
        Me.txtInputMatrixNCols.Location = New System.Drawing.Point(198, 113)
        Me.txtInputMatrixNCols.Name = "txtInputMatrixNCols"
        Me.txtInputMatrixNCols.Size = New System.Drawing.Size(36, 20)
        Me.txtInputMatrixNCols.TabIndex = 8
        '
        'txtInputMatrixNRows
        '
        Me.txtInputMatrixNRows.Location = New System.Drawing.Point(120, 113)
        Me.txtInputMatrixNRows.Name = "txtInputMatrixNRows"
        Me.txtInputMatrixNRows.Size = New System.Drawing.Size(36, 20)
        Me.txtInputMatrixNRows.TabIndex = 7
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(162, 116)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(30, 13)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Cols:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(77, 116)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(37, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Rows:"
        '
        'txtInputMatrixDescr
        '
        Me.txtInputMatrixDescr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInputMatrixDescr.Location = New System.Drawing.Point(3, 139)
        Me.txtInputMatrixDescr.Multiline = True
        Me.txtInputMatrixDescr.Name = "txtInputMatrixDescr"
        Me.txtInputMatrixDescr.Size = New System.Drawing.Size(459, 68)
        Me.txtInputMatrixDescr.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 123)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Description:"
        '
        'txtInputMatrixFileName
        '
        Me.txtInputMatrixFileName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInputMatrixFileName.Location = New System.Drawing.Point(55, 61)
        Me.txtInputMatrixFileName.Name = "txtInputMatrixFileName"
        Me.txtInputMatrixFileName.Size = New System.Drawing.Size(407, 20)
        Me.txtInputMatrixFileName.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Input Matrix"
        '
        'dgvInputMatrix
        '
        Me.dgvInputMatrix.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvInputMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInputMatrix.Location = New System.Drawing.Point(3, 213)
        Me.dgvInputMatrix.Name = "dgvInputMatrix"
        Me.dgvInputMatrix.Size = New System.Drawing.Size(459, 395)
        Me.dgvInputMatrix.TabIndex = 0
        '
        'txtOutputMatrixFormat
        '
        Me.txtOutputMatrixFormat.Location = New System.Drawing.Point(287, 113)
        Me.txtOutputMatrixFormat.Name = "txtOutputMatrixFormat"
        Me.txtOutputMatrixFormat.Size = New System.Drawing.Size(64, 20)
        Me.txtOutputMatrixFormat.TabIndex = 21
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(239, 116)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(42, 13)
        Me.Label44.TabIndex = 20
        Me.Label44.Text = "Format:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(9, 64)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(26, 13)
        Me.Label13.TabIndex = 19
        Me.Label13.Text = "File:"
        '
        'txtOutputMatrixFileName
        '
        Me.txtOutputMatrixFileName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOutputMatrixFileName.Location = New System.Drawing.Point(53, 61)
        Me.txtOutputMatrixFileName.Name = "txtOutputMatrixFileName"
        Me.txtOutputMatrixFileName.Size = New System.Drawing.Size(551, 20)
        Me.txtOutputMatrixFileName.TabIndex = 17
        '
        'btnCopyOutputMatrix
        '
        Me.btnCopyOutputMatrix.Location = New System.Drawing.Point(136, 6)
        Me.btnCopyOutputMatrix.Name = "btnCopyOutputMatrix"
        Me.btnCopyOutputMatrix.Size = New System.Drawing.Size(45, 22)
        Me.btnCopyOutputMatrix.TabIndex = 16
        Me.btnCopyOutputMatrix.Text = "Copy"
        Me.btnCopyOutputMatrix.UseVisualStyleBackColor = True
        '
        'btnSaveOutputMatrix
        '
        Me.btnSaveOutputMatrix.Location = New System.Drawing.Point(85, 6)
        Me.btnSaveOutputMatrix.Name = "btnSaveOutputMatrix"
        Me.btnSaveOutputMatrix.Size = New System.Drawing.Size(45, 22)
        Me.btnSaveOutputMatrix.TabIndex = 13
        Me.btnSaveOutputMatrix.Text = "Save"
        Me.btnSaveOutputMatrix.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(9, 90)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(38, 13)
        Me.Label11.TabIndex = 13
        Me.Label11.Text = "Name:"
        '
        'txtOutputMatrixNCols
        '
        Me.txtOutputMatrixNCols.Location = New System.Drawing.Point(200, 113)
        Me.txtOutputMatrixNCols.Name = "txtOutputMatrixNCols"
        Me.txtOutputMatrixNCols.Size = New System.Drawing.Size(33, 20)
        Me.txtOutputMatrixNCols.TabIndex = 12
        '
        'txtOutputMatrixNRows
        '
        Me.txtOutputMatrixNRows.Location = New System.Drawing.Point(125, 113)
        Me.txtOutputMatrixNRows.Name = "txtOutputMatrixNRows"
        Me.txtOutputMatrixNRows.Size = New System.Drawing.Size(33, 20)
        Me.txtOutputMatrixNRows.TabIndex = 9
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(164, 116)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(30, 13)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "Cols:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(82, 116)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(37, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Rows:"
        '
        'btnApplyOneMatrixOp
        '
        Me.btnApplyOneMatrixOp.Location = New System.Drawing.Point(272, 33)
        Me.btnApplyOneMatrixOp.Name = "btnApplyOneMatrixOp"
        Me.btnApplyOneMatrixOp.Size = New System.Drawing.Size(45, 22)
        Me.btnApplyOneMatrixOp.TabIndex = 11
        Me.btnApplyOneMatrixOp.Text = "Apply"
        Me.btnApplyOneMatrixOp.UseVisualStyleBackColor = True
        '
        'cmbOneMatrixOps
        '
        Me.cmbOneMatrixOps.FormattingEnabled = True
        Me.cmbOneMatrixOps.Location = New System.Drawing.Point(72, 34)
        Me.cmbOneMatrixOps.Name = "cmbOneMatrixOps"
        Me.cmbOneMatrixOps.Size = New System.Drawing.Size(194, 21)
        Me.cmbOneMatrixOps.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 37)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Operation:"
        '
        'txtOutputMatrixDescr
        '
        Me.txtOutputMatrixDescr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOutputMatrixDescr.Location = New System.Drawing.Point(3, 139)
        Me.txtOutputMatrixDescr.Multiline = True
        Me.txtOutputMatrixDescr.Name = "txtOutputMatrixDescr"
        Me.txtOutputMatrixDescr.Size = New System.Drawing.Size(601, 68)
        Me.txtOutputMatrixDescr.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 123)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = " Description:"
        '
        'txtOutputMatrixName
        '
        Me.txtOutputMatrixName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOutputMatrixName.Location = New System.Drawing.Point(53, 87)
        Me.txtOutputMatrixName.Name = "txtOutputMatrixName"
        Me.txtOutputMatrixName.Size = New System.Drawing.Size(551, 20)
        Me.txtOutputMatrixName.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Output Matrix"
        '
        'dgvOutputMatrix
        '
        Me.dgvOutputMatrix.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvOutputMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOutputMatrix.Location = New System.Drawing.Point(3, 213)
        Me.dgvOutputMatrix.Name = "dgvOutputMatrix"
        Me.dgvOutputMatrix.Size = New System.Drawing.Size(601, 395)
        Me.dgvOutputMatrix.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.SplitContainer2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1082, 617)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Two Matrix Operations"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtOutputCalcMatrixFormat)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label47)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label32)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtOutputCalcMatrixDescr)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label28)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtOutputCalcMatrixName)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label29)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtOutputCalcMatrixNCols)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtOutputCalcMatrixNRows)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label30)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label31)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtOutputCalcMatrixFileName)
        Me.SplitContainer2.Panel2.Controls.Add(Me.dgvOutputCalcMatrix)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnCopyOutputCalcMatrix)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnSaveOutputCalcMatrix)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnApplyTwoMatrixOp)
        Me.SplitContainer2.Panel2.Controls.Add(Me.cmbTwoMatrixOp)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label16)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label17)
        Me.SplitContainer2.Size = New System.Drawing.Size(1076, 611)
        Me.SplitContainer2.SplitterDistance = 692
        Me.SplitContainer2.TabIndex = 0
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtInputMatrix1Format)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label45)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label18)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtInputMatrix1Name)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label19)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtInputMatrix1NCols)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtInputMatrix1NRows)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label20)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label21)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtInputMatrix1Descr)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label22)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtInputMatrix1FileName)
        Me.SplitContainer3.Panel1.Controls.Add(Me.dgvInputMatrix1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.btnPasteInputMatrix1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.btnCopyInputMatrix1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.btnSaveInputMatrix1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.btnNewInputMatrix1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.btnOpenInputMatrix1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label14)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.txtInputMatrix2Format)
        Me.SplitContainer3.Panel2.Controls.Add(Me.Label46)
        Me.SplitContainer3.Panel2.Controls.Add(Me.Label23)
        Me.SplitContainer3.Panel2.Controls.Add(Me.txtInputMatrix2Name)
        Me.SplitContainer3.Panel2.Controls.Add(Me.Label24)
        Me.SplitContainer3.Panel2.Controls.Add(Me.txtInputMatrix2NCols)
        Me.SplitContainer3.Panel2.Controls.Add(Me.txtInputMatrix2NRows)
        Me.SplitContainer3.Panel2.Controls.Add(Me.Label25)
        Me.SplitContainer3.Panel2.Controls.Add(Me.Label26)
        Me.SplitContainer3.Panel2.Controls.Add(Me.txtInputMatrix2Descr)
        Me.SplitContainer3.Panel2.Controls.Add(Me.Label27)
        Me.SplitContainer3.Panel2.Controls.Add(Me.txtInputMatrix2FileName)
        Me.SplitContainer3.Panel2.Controls.Add(Me.dgvInputMatrix2)
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnPasteInputMatrix2)
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnCopyInputMatrix2)
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnSaveInputMatrix2)
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnNewInputMatrix2)
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnOpenInputMatrix2)
        Me.SplitContainer3.Panel2.Controls.Add(Me.Label15)
        Me.SplitContainer3.Size = New System.Drawing.Size(692, 611)
        Me.SplitContainer3.SplitterDistance = 338
        Me.SplitContainer3.TabIndex = 0
        '
        'txtInputMatrix1Format
        '
        Me.txtInputMatrix1Format.Location = New System.Drawing.Point(224, 99)
        Me.txtInputMatrix1Format.Name = "txtInputMatrix1Format"
        Me.txtInputMatrix1Format.Size = New System.Drawing.Size(64, 20)
        Me.txtInputMatrix1Format.TabIndex = 35
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(176, 102)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(42, 13)
        Me.Label45.TabIndex = 34
        Me.Label45.Text = "Format:"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(13, 50)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(26, 13)
        Me.Label18.TabIndex = 33
        Me.Label18.Text = "File:"
        '
        'txtInputMatrix1Name
        '
        Me.txtInputMatrix1Name.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInputMatrix1Name.Location = New System.Drawing.Point(56, 73)
        Me.txtInputMatrix1Name.Name = "txtInputMatrix1Name"
        Me.txtInputMatrix1Name.Size = New System.Drawing.Size(279, 20)
        Me.txtInputMatrix1Name.TabIndex = 32
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(12, 76)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(38, 13)
        Me.Label19.TabIndex = 31
        Me.Label19.Text = "Name:"
        '
        'txtInputMatrix1NCols
        '
        Me.txtInputMatrix1NCols.Location = New System.Drawing.Point(134, 99)
        Me.txtInputMatrix1NCols.Name = "txtInputMatrix1NCols"
        Me.txtInputMatrix1NCols.Size = New System.Drawing.Size(36, 20)
        Me.txtInputMatrix1NCols.TabIndex = 30
        '
        'txtInputMatrix1NRows
        '
        Me.txtInputMatrix1NRows.Location = New System.Drawing.Point(56, 99)
        Me.txtInputMatrix1NRows.Name = "txtInputMatrix1NRows"
        Me.txtInputMatrix1NRows.Size = New System.Drawing.Size(36, 20)
        Me.txtInputMatrix1NRows.TabIndex = 29
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(98, 102)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(30, 13)
        Me.Label20.TabIndex = 28
        Me.Label20.Text = "Cols:"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(13, 102)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(37, 13)
        Me.Label21.TabIndex = 27
        Me.Label21.Text = "Rows:"
        '
        'txtInputMatrix1Descr
        '
        Me.txtInputMatrix1Descr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInputMatrix1Descr.Location = New System.Drawing.Point(3, 139)
        Me.txtInputMatrix1Descr.Multiline = True
        Me.txtInputMatrix1Descr.Name = "txtInputMatrix1Descr"
        Me.txtInputMatrix1Descr.Size = New System.Drawing.Size(332, 68)
        Me.txtInputMatrix1Descr.TabIndex = 26
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(6, 123)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(63, 13)
        Me.Label22.TabIndex = 25
        Me.Label22.Text = "Description:"
        '
        'txtInputMatrix1FileName
        '
        Me.txtInputMatrix1FileName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInputMatrix1FileName.Location = New System.Drawing.Point(56, 47)
        Me.txtInputMatrix1FileName.Name = "txtInputMatrix1FileName"
        Me.txtInputMatrix1FileName.Size = New System.Drawing.Size(279, 20)
        Me.txtInputMatrix1FileName.TabIndex = 24
        '
        'dgvInputMatrix1
        '
        Me.dgvInputMatrix1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvInputMatrix1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInputMatrix1.Location = New System.Drawing.Point(3, 213)
        Me.dgvInputMatrix1.Name = "dgvInputMatrix1"
        Me.dgvInputMatrix1.Size = New System.Drawing.Size(332, 395)
        Me.dgvInputMatrix1.TabIndex = 23
        '
        'btnPasteInputMatrix1
        '
        Me.btnPasteInputMatrix1.Location = New System.Drawing.Point(287, 6)
        Me.btnPasteInputMatrix1.Name = "btnPasteInputMatrix1"
        Me.btnPasteInputMatrix1.Size = New System.Drawing.Size(48, 22)
        Me.btnPasteInputMatrix1.TabIndex = 22
        Me.btnPasteInputMatrix1.Text = "Paste"
        Me.btnPasteInputMatrix1.UseVisualStyleBackColor = True
        '
        'btnCopyInputMatrix1
        '
        Me.btnCopyInputMatrix1.Location = New System.Drawing.Point(236, 6)
        Me.btnCopyInputMatrix1.Name = "btnCopyInputMatrix1"
        Me.btnCopyInputMatrix1.Size = New System.Drawing.Size(45, 22)
        Me.btnCopyInputMatrix1.TabIndex = 21
        Me.btnCopyInputMatrix1.Text = "Copy"
        Me.btnCopyInputMatrix1.UseVisualStyleBackColor = True
        '
        'btnSaveInputMatrix1
        '
        Me.btnSaveInputMatrix1.Location = New System.Drawing.Point(185, 6)
        Me.btnSaveInputMatrix1.Name = "btnSaveInputMatrix1"
        Me.btnSaveInputMatrix1.Size = New System.Drawing.Size(45, 22)
        Me.btnSaveInputMatrix1.TabIndex = 20
        Me.btnSaveInputMatrix1.Text = "Save"
        Me.btnSaveInputMatrix1.UseVisualStyleBackColor = True
        '
        'btnNewInputMatrix1
        '
        Me.btnNewInputMatrix1.Location = New System.Drawing.Point(134, 6)
        Me.btnNewInputMatrix1.Name = "btnNewInputMatrix1"
        Me.btnNewInputMatrix1.Size = New System.Drawing.Size(45, 22)
        Me.btnNewInputMatrix1.TabIndex = 19
        Me.btnNewInputMatrix1.Text = "New"
        Me.btnNewInputMatrix1.UseVisualStyleBackColor = True
        '
        'btnOpenInputMatrix1
        '
        Me.btnOpenInputMatrix1.Location = New System.Drawing.Point(83, 6)
        Me.btnOpenInputMatrix1.Name = "btnOpenInputMatrix1"
        Me.btnOpenInputMatrix1.Size = New System.Drawing.Size(45, 22)
        Me.btnOpenInputMatrix1.TabIndex = 18
        Me.btnOpenInputMatrix1.Text = "Open"
        Me.btnOpenInputMatrix1.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 11)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(71, 13)
        Me.Label14.TabIndex = 17
        Me.Label14.Text = "Input Matrix 1"
        '
        'txtInputMatrix2Format
        '
        Me.txtInputMatrix2Format.Location = New System.Drawing.Point(224, 99)
        Me.txtInputMatrix2Format.Name = "txtInputMatrix2Format"
        Me.txtInputMatrix2Format.Size = New System.Drawing.Size(64, 20)
        Me.txtInputMatrix2Format.TabIndex = 45
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(176, 102)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(42, 13)
        Me.Label46.TabIndex = 44
        Me.Label46.Text = "Format:"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(13, 50)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(26, 13)
        Me.Label23.TabIndex = 43
        Me.Label23.Text = "File:"
        '
        'txtInputMatrix2Name
        '
        Me.txtInputMatrix2Name.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInputMatrix2Name.Location = New System.Drawing.Point(56, 73)
        Me.txtInputMatrix2Name.Name = "txtInputMatrix2Name"
        Me.txtInputMatrix2Name.Size = New System.Drawing.Size(291, 20)
        Me.txtInputMatrix2Name.TabIndex = 42
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(12, 76)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(38, 13)
        Me.Label24.TabIndex = 41
        Me.Label24.Text = "Name:"
        '
        'txtInputMatrix2NCols
        '
        Me.txtInputMatrix2NCols.Location = New System.Drawing.Point(134, 99)
        Me.txtInputMatrix2NCols.Name = "txtInputMatrix2NCols"
        Me.txtInputMatrix2NCols.Size = New System.Drawing.Size(36, 20)
        Me.txtInputMatrix2NCols.TabIndex = 40
        '
        'txtInputMatrix2NRows
        '
        Me.txtInputMatrix2NRows.Location = New System.Drawing.Point(56, 99)
        Me.txtInputMatrix2NRows.Name = "txtInputMatrix2NRows"
        Me.txtInputMatrix2NRows.Size = New System.Drawing.Size(36, 20)
        Me.txtInputMatrix2NRows.TabIndex = 39
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(98, 102)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(30, 13)
        Me.Label25.TabIndex = 38
        Me.Label25.Text = "Cols:"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(13, 102)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(37, 13)
        Me.Label26.TabIndex = 37
        Me.Label26.Text = "Rows:"
        '
        'txtInputMatrix2Descr
        '
        Me.txtInputMatrix2Descr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInputMatrix2Descr.Location = New System.Drawing.Point(3, 139)
        Me.txtInputMatrix2Descr.Multiline = True
        Me.txtInputMatrix2Descr.Name = "txtInputMatrix2Descr"
        Me.txtInputMatrix2Descr.Size = New System.Drawing.Size(344, 68)
        Me.txtInputMatrix2Descr.TabIndex = 36
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(6, 123)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(63, 13)
        Me.Label27.TabIndex = 35
        Me.Label27.Text = "Description:"
        '
        'txtInputMatrix2FileName
        '
        Me.txtInputMatrix2FileName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInputMatrix2FileName.Location = New System.Drawing.Point(56, 47)
        Me.txtInputMatrix2FileName.Name = "txtInputMatrix2FileName"
        Me.txtInputMatrix2FileName.Size = New System.Drawing.Size(291, 20)
        Me.txtInputMatrix2FileName.TabIndex = 34
        '
        'dgvInputMatrix2
        '
        Me.dgvInputMatrix2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvInputMatrix2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInputMatrix2.Location = New System.Drawing.Point(3, 213)
        Me.dgvInputMatrix2.Name = "dgvInputMatrix2"
        Me.dgvInputMatrix2.Size = New System.Drawing.Size(344, 395)
        Me.dgvInputMatrix2.TabIndex = 29
        '
        'btnPasteInputMatrix2
        '
        Me.btnPasteInputMatrix2.Location = New System.Drawing.Point(287, 6)
        Me.btnPasteInputMatrix2.Name = "btnPasteInputMatrix2"
        Me.btnPasteInputMatrix2.Size = New System.Drawing.Size(48, 22)
        Me.btnPasteInputMatrix2.TabIndex = 28
        Me.btnPasteInputMatrix2.Text = "Paste"
        Me.btnPasteInputMatrix2.UseVisualStyleBackColor = True
        '
        'btnCopyInputMatrix2
        '
        Me.btnCopyInputMatrix2.Location = New System.Drawing.Point(236, 6)
        Me.btnCopyInputMatrix2.Name = "btnCopyInputMatrix2"
        Me.btnCopyInputMatrix2.Size = New System.Drawing.Size(45, 22)
        Me.btnCopyInputMatrix2.TabIndex = 27
        Me.btnCopyInputMatrix2.Text = "Copy"
        Me.btnCopyInputMatrix2.UseVisualStyleBackColor = True
        '
        'btnSaveInputMatrix2
        '
        Me.btnSaveInputMatrix2.Location = New System.Drawing.Point(185, 6)
        Me.btnSaveInputMatrix2.Name = "btnSaveInputMatrix2"
        Me.btnSaveInputMatrix2.Size = New System.Drawing.Size(45, 22)
        Me.btnSaveInputMatrix2.TabIndex = 26
        Me.btnSaveInputMatrix2.Text = "Save"
        Me.btnSaveInputMatrix2.UseVisualStyleBackColor = True
        '
        'btnNewInputMatrix2
        '
        Me.btnNewInputMatrix2.Location = New System.Drawing.Point(134, 6)
        Me.btnNewInputMatrix2.Name = "btnNewInputMatrix2"
        Me.btnNewInputMatrix2.Size = New System.Drawing.Size(45, 22)
        Me.btnNewInputMatrix2.TabIndex = 25
        Me.btnNewInputMatrix2.Text = "New"
        Me.btnNewInputMatrix2.UseVisualStyleBackColor = True
        '
        'btnOpenInputMatrix2
        '
        Me.btnOpenInputMatrix2.Location = New System.Drawing.Point(83, 6)
        Me.btnOpenInputMatrix2.Name = "btnOpenInputMatrix2"
        Me.btnOpenInputMatrix2.Size = New System.Drawing.Size(45, 22)
        Me.btnOpenInputMatrix2.TabIndex = 24
        Me.btnOpenInputMatrix2.Text = "Open"
        Me.btnOpenInputMatrix2.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 11)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(71, 13)
        Me.Label15.TabIndex = 23
        Me.Label15.Text = "Input Matrix 2"
        '
        'txtOutputCalcMatrixFormat
        '
        Me.txtOutputCalcMatrixFormat.Location = New System.Drawing.Point(287, 113)
        Me.txtOutputCalcMatrixFormat.Name = "txtOutputCalcMatrixFormat"
        Me.txtOutputCalcMatrixFormat.Size = New System.Drawing.Size(64, 20)
        Me.txtOutputCalcMatrixFormat.TabIndex = 55
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(239, 116)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(42, 13)
        Me.Label47.TabIndex = 54
        Me.Label47.Text = "Format:"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(7, 123)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(63, 13)
        Me.Label32.TabIndex = 53
        Me.Label32.Text = "Description:"
        '
        'txtOutputCalcMatrixDescr
        '
        Me.txtOutputCalcMatrixDescr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOutputCalcMatrixDescr.Location = New System.Drawing.Point(3, 139)
        Me.txtOutputCalcMatrixDescr.Multiline = True
        Me.txtOutputCalcMatrixDescr.Name = "txtOutputCalcMatrixDescr"
        Me.txtOutputCalcMatrixDescr.Size = New System.Drawing.Size(374, 68)
        Me.txtOutputCalcMatrixDescr.TabIndex = 52
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(7, 64)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(26, 13)
        Me.Label28.TabIndex = 51
        Me.Label28.Text = "File:"
        '
        'txtOutputCalcMatrixName
        '
        Me.txtOutputCalcMatrixName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOutputCalcMatrixName.Location = New System.Drawing.Point(50, 87)
        Me.txtOutputCalcMatrixName.Name = "txtOutputCalcMatrixName"
        Me.txtOutputCalcMatrixName.Size = New System.Drawing.Size(327, 20)
        Me.txtOutputCalcMatrixName.TabIndex = 50
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(6, 90)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(38, 13)
        Me.Label29.TabIndex = 49
        Me.Label29.Text = "Name:"
        '
        'txtOutputCalcMatrixNCols
        '
        Me.txtOutputCalcMatrixNCols.Location = New System.Drawing.Point(197, 113)
        Me.txtOutputCalcMatrixNCols.Name = "txtOutputCalcMatrixNCols"
        Me.txtOutputCalcMatrixNCols.Size = New System.Drawing.Size(36, 20)
        Me.txtOutputCalcMatrixNCols.TabIndex = 48
        '
        'txtOutputCalcMatrixNRows
        '
        Me.txtOutputCalcMatrixNRows.Location = New System.Drawing.Point(119, 113)
        Me.txtOutputCalcMatrixNRows.Name = "txtOutputCalcMatrixNRows"
        Me.txtOutputCalcMatrixNRows.Size = New System.Drawing.Size(36, 20)
        Me.txtOutputCalcMatrixNRows.TabIndex = 47
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(161, 116)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(30, 13)
        Me.Label30.TabIndex = 46
        Me.Label30.Text = "Cols:"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(76, 116)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(37, 13)
        Me.Label31.TabIndex = 45
        Me.Label31.Text = "Rows:"
        '
        'txtOutputCalcMatrixFileName
        '
        Me.txtOutputCalcMatrixFileName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOutputCalcMatrixFileName.Location = New System.Drawing.Point(50, 61)
        Me.txtOutputCalcMatrixFileName.Name = "txtOutputCalcMatrixFileName"
        Me.txtOutputCalcMatrixFileName.Size = New System.Drawing.Size(327, 20)
        Me.txtOutputCalcMatrixFileName.TabIndex = 44
        '
        'dgvOutputCalcMatrix
        '
        Me.dgvOutputCalcMatrix.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvOutputCalcMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOutputCalcMatrix.Location = New System.Drawing.Point(3, 213)
        Me.dgvOutputCalcMatrix.Name = "dgvOutputCalcMatrix"
        Me.dgvOutputCalcMatrix.Size = New System.Drawing.Size(374, 395)
        Me.dgvOutputCalcMatrix.TabIndex = 30
        '
        'btnCopyOutputCalcMatrix
        '
        Me.btnCopyOutputCalcMatrix.Location = New System.Drawing.Point(186, 6)
        Me.btnCopyOutputCalcMatrix.Name = "btnCopyOutputCalcMatrix"
        Me.btnCopyOutputCalcMatrix.Size = New System.Drawing.Size(45, 22)
        Me.btnCopyOutputCalcMatrix.TabIndex = 22
        Me.btnCopyOutputCalcMatrix.Text = "Copy"
        Me.btnCopyOutputCalcMatrix.UseVisualStyleBackColor = True
        '
        'btnSaveOutputCalcMatrix
        '
        Me.btnSaveOutputCalcMatrix.Location = New System.Drawing.Point(135, 6)
        Me.btnSaveOutputCalcMatrix.Name = "btnSaveOutputCalcMatrix"
        Me.btnSaveOutputCalcMatrix.Size = New System.Drawing.Size(45, 22)
        Me.btnSaveOutputCalcMatrix.TabIndex = 21
        Me.btnSaveOutputCalcMatrix.Text = "Save"
        Me.btnSaveOutputCalcMatrix.UseVisualStyleBackColor = True
        '
        'btnApplyTwoMatrixOp
        '
        Me.btnApplyTwoMatrixOp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnApplyTwoMatrixOp.Location = New System.Drawing.Point(332, 33)
        Me.btnApplyTwoMatrixOp.Name = "btnApplyTwoMatrixOp"
        Me.btnApplyTwoMatrixOp.Size = New System.Drawing.Size(45, 22)
        Me.btnApplyTwoMatrixOp.TabIndex = 20
        Me.btnApplyTwoMatrixOp.Text = "Apply"
        Me.btnApplyTwoMatrixOp.UseVisualStyleBackColor = True
        '
        'cmbTwoMatrixOp
        '
        Me.cmbTwoMatrixOp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbTwoMatrixOp.FormattingEnabled = True
        Me.cmbTwoMatrixOp.Location = New System.Drawing.Point(68, 34)
        Me.cmbTwoMatrixOp.Name = "cmbTwoMatrixOp"
        Me.cmbTwoMatrixOp.Size = New System.Drawing.Size(258, 21)
        Me.cmbTwoMatrixOp.TabIndex = 19
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(6, 37)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(56, 13)
        Me.Label16.TabIndex = 18
        Me.Label16.Text = "Operation:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(6, 11)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(123, 13)
        Me.Label17.TabIndex = 17
        Me.Label17.Text = "Output Calculated Matrix"
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.SplitContainer4)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(1082, 617)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Information"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.btnUpdateList)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtSelMatrixName)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtSelMatrixNCols)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtSelMatrixNRows)
        Me.SplitContainer4.Panel1.Controls.Add(Me.Label41)
        Me.SplitContainer4.Panel1.Controls.Add(Me.Label42)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtSelMatrixDescr)
        Me.SplitContainer4.Panel1.Controls.Add(Me.Label40)
        Me.SplitContainer4.Panel1.Controls.Add(Me.Label39)
        Me.SplitContainer4.Panel1.Controls.Add(Me.btnDeleteMatrix)
        Me.SplitContainer4.Panel1.Controls.Add(Me.btnCopyMatrix)
        Me.SplitContainer4.Panel1.Controls.Add(Me.btnOpenMatrix)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lstMatrices)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.SplitContainer6)
        Me.SplitContainer4.Size = New System.Drawing.Size(1082, 617)
        Me.SplitContainer4.SplitterDistance = 380
        Me.SplitContainer4.TabIndex = 0
        '
        'btnUpdateList
        '
        Me.btnUpdateList.Location = New System.Drawing.Point(168, 8)
        Me.btnUpdateList.Name = "btnUpdateList"
        Me.btnUpdateList.Size = New System.Drawing.Size(78, 22)
        Me.btnUpdateList.TabIndex = 52
        Me.btnUpdateList.Text = "Update List"
        Me.btnUpdateList.UseVisualStyleBackColor = True
        '
        'txtSelMatrixName
        '
        Me.txtSelMatrixName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSelMatrixName.Location = New System.Drawing.Point(49, 36)
        Me.txtSelMatrixName.Name = "txtSelMatrixName"
        Me.txtSelMatrixName.Size = New System.Drawing.Size(328, 20)
        Me.txtSelMatrixName.TabIndex = 51
        '
        'txtSelMatrixNCols
        '
        Me.txtSelMatrixNCols.Location = New System.Drawing.Point(226, 62)
        Me.txtSelMatrixNCols.Name = "txtSelMatrixNCols"
        Me.txtSelMatrixNCols.Size = New System.Drawing.Size(36, 20)
        Me.txtSelMatrixNCols.TabIndex = 50
        '
        'txtSelMatrixNRows
        '
        Me.txtSelMatrixNRows.Location = New System.Drawing.Point(150, 62)
        Me.txtSelMatrixNRows.Name = "txtSelMatrixNRows"
        Me.txtSelMatrixNRows.Size = New System.Drawing.Size(36, 20)
        Me.txtSelMatrixNRows.TabIndex = 49
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(190, 65)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(30, 13)
        Me.Label41.TabIndex = 48
        Me.Label41.Text = "Cols:"
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(107, 65)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(37, 13)
        Me.Label42.TabIndex = 47
        Me.Label42.Text = "Rows:"
        '
        'txtSelMatrixDescr
        '
        Me.txtSelMatrixDescr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSelMatrixDescr.Location = New System.Drawing.Point(6, 88)
        Me.txtSelMatrixDescr.Multiline = True
        Me.txtSelMatrixDescr.Name = "txtSelMatrixDescr"
        Me.txtSelMatrixDescr.Size = New System.Drawing.Size(371, 68)
        Me.txtSelMatrixDescr.TabIndex = 46
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(6, 72)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(63, 13)
        Me.Label40.TabIndex = 45
        Me.Label40.Text = "Description:"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(5, 39)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(38, 13)
        Me.Label39.TabIndex = 44
        Me.Label39.Text = "Name:"
        '
        'btnDeleteMatrix
        '
        Me.btnDeleteMatrix.Location = New System.Drawing.Point(114, 8)
        Me.btnDeleteMatrix.Name = "btnDeleteMatrix"
        Me.btnDeleteMatrix.Size = New System.Drawing.Size(48, 22)
        Me.btnDeleteMatrix.TabIndex = 43
        Me.btnDeleteMatrix.Text = "Delete"
        Me.btnDeleteMatrix.UseVisualStyleBackColor = True
        '
        'btnCopyMatrix
        '
        Me.btnCopyMatrix.Location = New System.Drawing.Point(60, 8)
        Me.btnCopyMatrix.Name = "btnCopyMatrix"
        Me.btnCopyMatrix.Size = New System.Drawing.Size(48, 22)
        Me.btnCopyMatrix.TabIndex = 42
        Me.btnCopyMatrix.Text = "Copy"
        Me.btnCopyMatrix.UseVisualStyleBackColor = True
        '
        'btnOpenMatrix
        '
        Me.btnOpenMatrix.Location = New System.Drawing.Point(6, 8)
        Me.btnOpenMatrix.Name = "btnOpenMatrix"
        Me.btnOpenMatrix.Size = New System.Drawing.Size(48, 22)
        Me.btnOpenMatrix.TabIndex = 41
        Me.btnOpenMatrix.Text = "Open"
        Me.btnOpenMatrix.UseVisualStyleBackColor = True
        '
        'lstMatrices
        '
        Me.lstMatrices.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstMatrices.FormattingEnabled = True
        Me.lstMatrices.Location = New System.Drawing.Point(3, 162)
        Me.lstMatrices.Name = "lstMatrices"
        Me.lstMatrices.Size = New System.Drawing.Size(374, 433)
        Me.lstMatrices.TabIndex = 40
        '
        'SplitContainer6
        '
        Me.SplitContainer6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer6.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer6.Name = "SplitContainer6"
        '
        'SplitContainer6.Panel1
        '
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtMatrixFormat)
        Me.SplitContainer6.Panel1.Controls.Add(Me.Label48)
        Me.SplitContainer6.Panel1.Controls.Add(Me.Label33)
        Me.SplitContainer6.Panel1.Controls.Add(Me.dgvMatrix)
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtMatrixName)
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtMatrixDescr)
        Me.SplitContainer6.Panel1.Controls.Add(Me.Label38)
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtMatrixFileName)
        Me.SplitContainer6.Panel1.Controls.Add(Me.Label37)
        Me.SplitContainer6.Panel1.Controls.Add(Me.Label36)
        Me.SplitContainer6.Panel1.Controls.Add(Me.Label35)
        Me.SplitContainer6.Panel1.Controls.Add(Me.Label34)
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtMatrixNRows)
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtMatrixNCols)
        '
        'SplitContainer6.Panel2
        '
        Me.SplitContainer6.Panel2.Controls.Add(Me.txtSymmetric)
        Me.SplitContainer6.Panel2.Controls.Add(Me.Label55)
        Me.SplitContainer6.Panel2.Controls.Add(Me.txtHermitian)
        Me.SplitContainer6.Panel2.Controls.Add(Me.Label54)
        Me.SplitContainer6.Panel2.Controls.Add(Me.txtDeterminant)
        Me.SplitContainer6.Panel2.Controls.Add(Me.Label53)
        Me.SplitContainer6.Panel2.Controls.Add(Me.txtRank)
        Me.SplitContainer6.Panel2.Controls.Add(Me.Label52)
        Me.SplitContainer6.Size = New System.Drawing.Size(698, 617)
        Me.SplitContainer6.SplitterDistance = 442
        Me.SplitContainer6.TabIndex = 0
        '
        'txtMatrixFormat
        '
        Me.txtMatrixFormat.Location = New System.Drawing.Point(219, 88)
        Me.txtMatrixFormat.Name = "txtMatrixFormat"
        Me.txtMatrixFormat.Size = New System.Drawing.Size(64, 20)
        Me.txtMatrixFormat.TabIndex = 47
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Location = New System.Drawing.Point(171, 91)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(42, 13)
        Me.Label48.TabIndex = 46
        Me.Label48.Text = "Format:"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(7, 39)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(26, 13)
        Me.Label33.TabIndex = 35
        Me.Label33.Text = "File:"
        '
        'dgvMatrix
        '
        Me.dgvMatrix.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMatrix.Location = New System.Drawing.Point(3, 202)
        Me.dgvMatrix.Name = "dgvMatrix"
        Me.dgvMatrix.Size = New System.Drawing.Size(436, 412)
        Me.dgvMatrix.TabIndex = 19
        '
        'txtMatrixName
        '
        Me.txtMatrixName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMatrixName.Location = New System.Drawing.Point(51, 62)
        Me.txtMatrixName.Name = "txtMatrixName"
        Me.txtMatrixName.Size = New System.Drawing.Size(388, 20)
        Me.txtMatrixName.TabIndex = 34
        '
        'txtMatrixDescr
        '
        Me.txtMatrixDescr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMatrixDescr.Location = New System.Drawing.Point(3, 128)
        Me.txtMatrixDescr.Multiline = True
        Me.txtMatrixDescr.Name = "txtMatrixDescr"
        Me.txtMatrixDescr.Size = New System.Drawing.Size(436, 68)
        Me.txtMatrixDescr.TabIndex = 23
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(7, 13)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(35, 13)
        Me.Label38.TabIndex = 20
        Me.Label38.Text = "Matrix"
        '
        'txtMatrixFileName
        '
        Me.txtMatrixFileName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMatrixFileName.Location = New System.Drawing.Point(51, 36)
        Me.txtMatrixFileName.Name = "txtMatrixFileName"
        Me.txtMatrixFileName.Size = New System.Drawing.Size(388, 20)
        Me.txtMatrixFileName.TabIndex = 21
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(6, 112)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(63, 13)
        Me.Label37.TabIndex = 22
        Me.Label37.Text = "Description:"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(7, 91)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(37, 13)
        Me.Label36.TabIndex = 24
        Me.Label36.Text = "Rows:"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(93, 91)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(30, 13)
        Me.Label35.TabIndex = 25
        Me.Label35.Text = "Cols:"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(7, 65)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(38, 13)
        Me.Label34.TabIndex = 28
        Me.Label34.Text = "Name:"
        '
        'txtMatrixNRows
        '
        Me.txtMatrixNRows.Location = New System.Drawing.Point(51, 88)
        Me.txtMatrixNRows.Name = "txtMatrixNRows"
        Me.txtMatrixNRows.Size = New System.Drawing.Size(36, 20)
        Me.txtMatrixNRows.TabIndex = 26
        '
        'txtMatrixNCols
        '
        Me.txtMatrixNCols.Location = New System.Drawing.Point(129, 88)
        Me.txtMatrixNCols.Name = "txtMatrixNCols"
        Me.txtMatrixNCols.Size = New System.Drawing.Size(36, 20)
        Me.txtMatrixNCols.TabIndex = 27
        '
        'txtSymmetric
        '
        Me.txtSymmetric.Location = New System.Drawing.Point(80, 118)
        Me.txtSymmetric.Name = "txtSymmetric"
        Me.txtSymmetric.ReadOnly = True
        Me.txtSymmetric.Size = New System.Drawing.Size(58, 20)
        Me.txtSymmetric.TabIndex = 7
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Location = New System.Drawing.Point(7, 121)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(58, 13)
        Me.Label55.TabIndex = 6
        Me.Label55.Text = "Symmetric:"
        '
        'txtHermitian
        '
        Me.txtHermitian.Location = New System.Drawing.Point(80, 92)
        Me.txtHermitian.Name = "txtHermitian"
        Me.txtHermitian.ReadOnly = True
        Me.txtHermitian.Size = New System.Drawing.Size(58, 20)
        Me.txtHermitian.TabIndex = 5
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Location = New System.Drawing.Point(7, 95)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(54, 13)
        Me.Label54.TabIndex = 4
        Me.Label54.Text = "Hermitian:"
        '
        'txtDeterminant
        '
        Me.txtDeterminant.Location = New System.Drawing.Point(80, 66)
        Me.txtDeterminant.Name = "txtDeterminant"
        Me.txtDeterminant.ReadOnly = True
        Me.txtDeterminant.Size = New System.Drawing.Size(58, 20)
        Me.txtDeterminant.TabIndex = 3
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(7, 69)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(67, 13)
        Me.Label53.TabIndex = 2
        Me.Label53.Text = "Determinant:"
        '
        'txtRank
        '
        Me.txtRank.Location = New System.Drawing.Point(80, 40)
        Me.txtRank.Name = "txtRank"
        Me.txtRank.ReadOnly = True
        Me.txtRank.Size = New System.Drawing.Size(58, 20)
        Me.txtRank.TabIndex = 1
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Location = New System.Drawing.Point(7, 43)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(36, 13)
        Me.Label52.TabIndex = 0
        Me.Label52.Text = "Rank:"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.SplitContainer5)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(1082, 617)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Matrix Operation Sequence"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'SplitContainer5
        '
        Me.SplitContainer5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer5.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer5.Name = "SplitContainer5"
        '
        'SplitContainer5.Panel1
        '
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtSeqDescr)
        Me.SplitContainer5.Panel1.Controls.Add(Me.Label51)
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtSeqName)
        Me.SplitContainer5.Panel1.Controls.Add(Me.Label50)
        Me.SplitContainer5.Panel1.Controls.Add(Me.btnNewSeq)
        Me.SplitContainer5.Panel1.Controls.Add(Me.btnSaveSeq)
        Me.SplitContainer5.Panel1.Controls.Add(Me.btnOpenSeq)
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtSeqFileName)
        Me.SplitContainer5.Panel1.Controls.Add(Me.Label49)
        Me.SplitContainer5.Panel1.Controls.Add(Me.trvMatrixOps)
        '
        'SplitContainer5.Panel2
        '
        Me.SplitContainer5.Panel2.Controls.Add(Me.TabControl2)
        Me.SplitContainer5.Size = New System.Drawing.Size(1082, 617)
        Me.SplitContainer5.SplitterDistance = 360
        Me.SplitContainer5.TabIndex = 0
        '
        'txtSeqDescr
        '
        Me.txtSeqDescr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSeqDescr.Location = New System.Drawing.Point(3, 70)
        Me.txtSeqDescr.Multiline = True
        Me.txtSeqDescr.Name = "txtSeqDescr"
        Me.txtSeqDescr.Size = New System.Drawing.Size(354, 58)
        Me.txtSeqDescr.TabIndex = 247
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Location = New System.Drawing.Point(3, 54)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(63, 13)
        Me.Label51.TabIndex = 246
        Me.Label51.Text = "Description:"
        '
        'txtSeqName
        '
        Me.txtSeqName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSeqName.Location = New System.Drawing.Point(64, 31)
        Me.txtSeqName.Name = "txtSeqName"
        Me.txtSeqName.Size = New System.Drawing.Size(293, 20)
        Me.txtSeqName.TabIndex = 245
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Location = New System.Drawing.Point(3, 34)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(38, 13)
        Me.Label50.TabIndex = 244
        Me.Label50.Text = "Name:"
        '
        'btnNewSeq
        '
        Me.btnNewSeq.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNewSeq.Location = New System.Drawing.Point(319, 3)
        Me.btnNewSeq.Name = "btnNewSeq"
        Me.btnNewSeq.Size = New System.Drawing.Size(38, 22)
        Me.btnNewSeq.TabIndex = 243
        Me.btnNewSeq.Text = "New"
        Me.btnNewSeq.UseVisualStyleBackColor = True
        '
        'btnSaveSeq
        '
        Me.btnSaveSeq.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveSeq.Location = New System.Drawing.Point(273, 3)
        Me.btnSaveSeq.Name = "btnSaveSeq"
        Me.btnSaveSeq.Size = New System.Drawing.Size(40, 22)
        Me.btnSaveSeq.TabIndex = 241
        Me.btnSaveSeq.Text = "Save"
        Me.btnSaveSeq.UseVisualStyleBackColor = True
        '
        'btnOpenSeq
        '
        Me.btnOpenSeq.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOpenSeq.Location = New System.Drawing.Point(225, 3)
        Me.btnOpenSeq.Name = "btnOpenSeq"
        Me.btnOpenSeq.Size = New System.Drawing.Size(42, 22)
        Me.btnOpenSeq.TabIndex = 240
        Me.btnOpenSeq.Text = "Open"
        Me.btnOpenSeq.UseVisualStyleBackColor = True
        '
        'txtSeqFileName
        '
        Me.txtSeqFileName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSeqFileName.Location = New System.Drawing.Point(64, 5)
        Me.txtSeqFileName.Name = "txtSeqFileName"
        Me.txtSeqFileName.Size = New System.Drawing.Size(155, 20)
        Me.txtSeqFileName.TabIndex = 2
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Location = New System.Drawing.Point(3, 8)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(55, 13)
        Me.Label49.TabIndex = 1
        Me.Label49.Text = "File name:"
        '
        'trvMatrixOps
        '
        Me.trvMatrixOps.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.trvMatrixOps.ItemHeight = 40
        Me.trvMatrixOps.Location = New System.Drawing.Point(3, 134)
        Me.trvMatrixOps.Name = "trvMatrixOps"
        Me.trvMatrixOps.Size = New System.Drawing.Size(354, 480)
        Me.trvMatrixOps.TabIndex = 0
        '
        'TabControl2
        '
        Me.TabControl2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl2.Controls.Add(Me.TabPage5)
        Me.TabControl2.Controls.Add(Me.TabPage6)
        Me.TabControl2.Controls.Add(Me.TabPage7)
        Me.TabControl2.Location = New System.Drawing.Point(3, 3)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(712, 611)
        Me.TabControl2.TabIndex = 0
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.btnShowItem)
        Me.TabPage5.Controls.Add(Me.txtScalarName)
        Me.TabPage5.Controls.Add(Me.Label78)
        Me.TabPage5.Controls.Add(Me.GroupBox4)
        Me.TabPage5.Controls.Add(Me.txtScalarItem)
        Me.TabPage5.Controls.Add(Me.Label67)
        Me.TabPage5.Controls.Add(Me.Label66)
        Me.TabPage5.Controls.Add(Me.txtItemStatus)
        Me.TabPage5.Controls.Add(Me.txtItemDescription)
        Me.TabPage5.Controls.Add(Me.Label58)
        Me.TabPage5.Controls.Add(Me.txtItemType)
        Me.TabPage5.Controls.Add(Me.txtItemName)
        Me.TabPage5.Controls.Add(Me.Label57)
        Me.TabPage5.Controls.Add(Me.Label56)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(704, 585)
        Me.TabPage5.TabIndex = 0
        Me.TabPage5.Text = "Information"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'btnShowItem
        '
        Me.btnShowItem.Location = New System.Drawing.Point(6, 91)
        Me.btnShowItem.Name = "btnShowItem"
        Me.btnShowItem.Size = New System.Drawing.Size(78, 34)
        Me.btnShowItem.TabIndex = 68
        Me.btnShowItem.Text = "Show in New Window"
        Me.btnShowItem.UseVisualStyleBackColor = True
        '
        'txtScalarName
        '
        Me.txtScalarName.Location = New System.Drawing.Point(81, 131)
        Me.txtScalarName.Name = "txtScalarName"
        Me.txtScalarName.Size = New System.Drawing.Size(278, 20)
        Me.txtScalarName.TabIndex = 13
        '
        'Label78
        '
        Me.Label78.AutoSize = True
        Me.Label78.Location = New System.Drawing.Point(365, 134)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(37, 13)
        Me.Label78.TabIndex = 12
        Me.Label78.Text = "Value:"
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.btnPasteData)
        Me.GroupBox4.Controls.Add(Me.btnCalcMatrixItem)
        Me.GroupBox4.Controls.Add(Me.btnFormatHelp2)
        Me.GroupBox4.Controls.Add(Me.txtMatrixItemFileName)
        Me.GroupBox4.Controls.Add(Me.Label73)
        Me.GroupBox4.Controls.Add(Me.btnPasteMatrixItem)
        Me.GroupBox4.Controls.Add(Me.btnCopyMatrixItem)
        Me.GroupBox4.Controls.Add(Me.btnSaveMatrixItem)
        Me.GroupBox4.Controls.Add(Me.btnNewMatrixItem)
        Me.GroupBox4.Controls.Add(Me.btnOpenMatrixItem)
        Me.GroupBox4.Controls.Add(Me.txtMatrixItemFormat)
        Me.GroupBox4.Controls.Add(Me.Label68)
        Me.GroupBox4.Controls.Add(Me.dgvMatrixItem)
        Me.GroupBox4.Controls.Add(Me.txtMatrixItemName)
        Me.GroupBox4.Controls.Add(Me.txtMatrixItemDescr)
        Me.GroupBox4.Controls.Add(Me.Label69)
        Me.GroupBox4.Controls.Add(Me.Label70)
        Me.GroupBox4.Controls.Add(Me.Label71)
        Me.GroupBox4.Controls.Add(Me.Label72)
        Me.GroupBox4.Controls.Add(Me.txtMatrixItemNRows)
        Me.GroupBox4.Controls.Add(Me.txtMatrixItemNCols)
        Me.GroupBox4.Location = New System.Drawing.Point(9, 157)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(689, 422)
        Me.GroupBox4.TabIndex = 10
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Matrix:"
        '
        'btnPasteData
        '
        Me.btnPasteData.Location = New System.Drawing.Point(6, 187)
        Me.btnPasteData.Name = "btnPasteData"
        Me.btnPasteData.Size = New System.Drawing.Size(48, 36)
        Me.btnPasteData.TabIndex = 67
        Me.btnPasteData.Text = "Paste Data"
        Me.btnPasteData.UseVisualStyleBackColor = True
        '
        'btnCalcMatrixItem
        '
        Me.btnCalcMatrixItem.Location = New System.Drawing.Point(6, 159)
        Me.btnCalcMatrixItem.Name = "btnCalcMatrixItem"
        Me.btnCalcMatrixItem.Size = New System.Drawing.Size(48, 22)
        Me.btnCalcMatrixItem.TabIndex = 66
        Me.btnCalcMatrixItem.Text = "Calc"
        Me.btnCalcMatrixItem.UseVisualStyleBackColor = True
        '
        'btnFormatHelp2
        '
        Me.btnFormatHelp2.Location = New System.Drawing.Point(443, 71)
        Me.btnFormatHelp2.Name = "btnFormatHelp2"
        Me.btnFormatHelp2.Size = New System.Drawing.Size(21, 22)
        Me.btnFormatHelp2.TabIndex = 65
        Me.btnFormatHelp2.Text = "?"
        Me.btnFormatHelp2.UseVisualStyleBackColor = True
        '
        'txtMatrixItemFileName
        '
        Me.txtMatrixItemFileName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMatrixItemFileName.Location = New System.Drawing.Point(107, 19)
        Me.txtMatrixItemFileName.Name = "txtMatrixItemFileName"
        Me.txtMatrixItemFileName.Size = New System.Drawing.Size(576, 20)
        Me.txtMatrixItemFileName.TabIndex = 64
        '
        'Label73
        '
        Me.Label73.AutoSize = True
        Me.Label73.Location = New System.Drawing.Point(63, 24)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(26, 13)
        Me.Label73.TabIndex = 63
        Me.Label73.Text = "File:"
        '
        'btnPasteMatrixItem
        '
        Me.btnPasteMatrixItem.Location = New System.Drawing.Point(6, 131)
        Me.btnPasteMatrixItem.Name = "btnPasteMatrixItem"
        Me.btnPasteMatrixItem.Size = New System.Drawing.Size(48, 22)
        Me.btnPasteMatrixItem.TabIndex = 62
        Me.btnPasteMatrixItem.Text = "Paste"
        Me.btnPasteMatrixItem.UseVisualStyleBackColor = True
        '
        'btnCopyMatrixItem
        '
        Me.btnCopyMatrixItem.Location = New System.Drawing.Point(6, 103)
        Me.btnCopyMatrixItem.Name = "btnCopyMatrixItem"
        Me.btnCopyMatrixItem.Size = New System.Drawing.Size(45, 22)
        Me.btnCopyMatrixItem.TabIndex = 61
        Me.btnCopyMatrixItem.Text = "Copy"
        Me.btnCopyMatrixItem.UseVisualStyleBackColor = True
        '
        'btnSaveMatrixItem
        '
        Me.btnSaveMatrixItem.Location = New System.Drawing.Point(6, 75)
        Me.btnSaveMatrixItem.Name = "btnSaveMatrixItem"
        Me.btnSaveMatrixItem.Size = New System.Drawing.Size(45, 22)
        Me.btnSaveMatrixItem.TabIndex = 60
        Me.btnSaveMatrixItem.Text = "Save"
        Me.btnSaveMatrixItem.UseVisualStyleBackColor = True
        '
        'btnNewMatrixItem
        '
        Me.btnNewMatrixItem.Location = New System.Drawing.Point(6, 47)
        Me.btnNewMatrixItem.Name = "btnNewMatrixItem"
        Me.btnNewMatrixItem.Size = New System.Drawing.Size(45, 22)
        Me.btnNewMatrixItem.TabIndex = 14
        Me.btnNewMatrixItem.Text = "New"
        Me.btnNewMatrixItem.UseVisualStyleBackColor = True
        '
        'btnOpenMatrixItem
        '
        Me.btnOpenMatrixItem.Location = New System.Drawing.Point(6, 19)
        Me.btnOpenMatrixItem.Name = "btnOpenMatrixItem"
        Me.btnOpenMatrixItem.Size = New System.Drawing.Size(45, 22)
        Me.btnOpenMatrixItem.TabIndex = 59
        Me.btnOpenMatrixItem.Text = "Open"
        Me.btnOpenMatrixItem.UseVisualStyleBackColor = True
        '
        'txtMatrixItemFormat
        '
        Me.txtMatrixItemFormat.Location = New System.Drawing.Point(373, 71)
        Me.txtMatrixItemFormat.Name = "txtMatrixItemFormat"
        Me.txtMatrixItemFormat.Size = New System.Drawing.Size(64, 20)
        Me.txtMatrixItemFormat.TabIndex = 58
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Location = New System.Drawing.Point(325, 74)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(42, 13)
        Me.Label68.TabIndex = 57
        Me.Label68.Text = "Format:"
        '
        'dgvMatrixItem
        '
        Me.dgvMatrixItem.AllowUserToAddRows = False
        Me.dgvMatrixItem.AllowUserToDeleteRows = False
        Me.dgvMatrixItem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvMatrixItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMatrixItem.Location = New System.Drawing.Point(66, 152)
        Me.dgvMatrixItem.Name = "dgvMatrixItem"
        Me.dgvMatrixItem.Size = New System.Drawing.Size(617, 264)
        Me.dgvMatrixItem.TabIndex = 48
        '
        'txtMatrixItemName
        '
        Me.txtMatrixItemName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMatrixItemName.Location = New System.Drawing.Point(107, 45)
        Me.txtMatrixItemName.Name = "txtMatrixItemName"
        Me.txtMatrixItemName.ReadOnly = True
        Me.txtMatrixItemName.Size = New System.Drawing.Size(576, 20)
        Me.txtMatrixItemName.TabIndex = 56
        '
        'txtMatrixItemDescr
        '
        Me.txtMatrixItemDescr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMatrixItemDescr.Location = New System.Drawing.Point(66, 97)
        Me.txtMatrixItemDescr.Multiline = True
        Me.txtMatrixItemDescr.Name = "txtMatrixItemDescr"
        Me.txtMatrixItemDescr.Size = New System.Drawing.Size(617, 49)
        Me.txtMatrixItemDescr.TabIndex = 50
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Location = New System.Drawing.Point(63, 81)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(63, 13)
        Me.Label69.TabIndex = 49
        Me.Label69.Text = "Description:"
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Location = New System.Drawing.Point(162, 74)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(37, 13)
        Me.Label70.TabIndex = 51
        Me.Label70.Text = "Rows:"
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Location = New System.Drawing.Point(247, 74)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(30, 13)
        Me.Label71.TabIndex = 52
        Me.Label71.Text = "Cols:"
        '
        'Label72
        '
        Me.Label72.AutoSize = True
        Me.Label72.Location = New System.Drawing.Point(63, 48)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(38, 13)
        Me.Label72.TabIndex = 55
        Me.Label72.Text = "Name:"
        '
        'txtMatrixItemNRows
        '
        Me.txtMatrixItemNRows.Location = New System.Drawing.Point(205, 71)
        Me.txtMatrixItemNRows.Name = "txtMatrixItemNRows"
        Me.txtMatrixItemNRows.Size = New System.Drawing.Size(36, 20)
        Me.txtMatrixItemNRows.TabIndex = 53
        '
        'txtMatrixItemNCols
        '
        Me.txtMatrixItemNCols.Location = New System.Drawing.Point(283, 71)
        Me.txtMatrixItemNCols.Name = "txtMatrixItemNCols"
        Me.txtMatrixItemNCols.Size = New System.Drawing.Size(36, 20)
        Me.txtMatrixItemNCols.TabIndex = 54
        '
        'txtScalarItem
        '
        Me.txtScalarItem.Location = New System.Drawing.Point(408, 131)
        Me.txtScalarItem.Name = "txtScalarItem"
        Me.txtScalarItem.Size = New System.Drawing.Size(281, 20)
        Me.txtScalarItem.TabIndex = 9
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Location = New System.Drawing.Point(6, 134)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(69, 13)
        Me.Label67.TabIndex = 8
        Me.Label67.Text = "Scalar name:"
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Location = New System.Drawing.Point(6, 39)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(37, 13)
        Me.Label66.TabIndex = 7
        Me.Label66.Text = "Status"
        '
        'txtItemStatus
        '
        Me.txtItemStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtItemStatus.Location = New System.Drawing.Point(75, 36)
        Me.txtItemStatus.Name = "txtItemStatus"
        Me.txtItemStatus.Size = New System.Drawing.Size(623, 20)
        Me.txtItemStatus.TabIndex = 6
        '
        'txtItemDescription
        '
        Me.txtItemDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtItemDescription.Location = New System.Drawing.Point(90, 62)
        Me.txtItemDescription.Multiline = True
        Me.txtItemDescription.Name = "txtItemDescription"
        Me.txtItemDescription.Size = New System.Drawing.Size(608, 63)
        Me.txtItemDescription.TabIndex = 5
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Location = New System.Drawing.Point(6, 65)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(63, 13)
        Me.Label58.TabIndex = 4
        Me.Label58.Text = "Description:"
        '
        'txtItemType
        '
        Me.txtItemType.Location = New System.Drawing.Point(405, 10)
        Me.txtItemType.Name = "txtItemType"
        Me.txtItemType.ReadOnly = True
        Me.txtItemType.Size = New System.Drawing.Size(284, 20)
        Me.txtItemType.TabIndex = 3
        '
        'txtItemName
        '
        Me.txtItemName.Location = New System.Drawing.Point(75, 10)
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.ReadOnly = True
        Me.txtItemName.Size = New System.Drawing.Size(284, 20)
        Me.txtItemName.TabIndex = 2
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Location = New System.Drawing.Point(365, 13)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(34, 13)
        Me.Label57.TabIndex = 1
        Me.Label57.Text = "Type:"
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Location = New System.Drawing.Point(6, 13)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(59, 13)
        Me.Label56.TabIndex = 0
        Me.Label56.Text = "Item name:"
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.GroupBox5)
        Me.TabPage6.Controls.Add(Me.txtNodeInfo)
        Me.TabPage6.Controls.Add(Me.Label65)
        Me.TabPage6.Controls.Add(Me.GroupBox3)
        Me.TabPage6.Controls.Add(Me.GroupBox2)
        Me.TabPage6.Controls.Add(Me.GroupBox1)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(704, 585)
        Me.TabPage6.TabIndex = 1
        Me.TabPage6.Text = "Create"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.cmbDataSource)
        Me.GroupBox5.Controls.Add(Me.Label76)
        Me.GroupBox5.Controls.Add(Me.btnReplaceSpecialNode)
        Me.GroupBox5.Controls.Add(Me.btnInsertSpecialNode)
        Me.GroupBox5.Controls.Add(Me.Label74)
        Me.GroupBox5.Controls.Add(Me.txtSpecialNodeDescr)
        Me.GroupBox5.Controls.Add(Me.txtSpecialNodeName)
        Me.GroupBox5.Controls.Add(Me.Label75)
        Me.GroupBox5.Controls.Add(Me.btnAppendSpecialNode)
        Me.GroupBox5.Controls.Add(Me.rbCollection)
        Me.GroupBox5.Controls.Add(Me.pbIconCollection)
        Me.GroupBox5.Controls.Add(Me.rbMatrixCopy)
        Me.GroupBox5.Controls.Add(Me.pbIconMatrixCopy)
        Me.GroupBox5.Controls.Add(Me.rbScalarCopy)
        Me.GroupBox5.Controls.Add(Me.pbIconScalarCopy)
        Me.GroupBox5.Location = New System.Drawing.Point(464, 244)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(221, 309)
        Me.GroupBox5.TabIndex = 104
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Special Nodes:"
        '
        'cmbDataSource
        '
        Me.cmbDataSource.FormattingEnabled = True
        Me.cmbDataSource.Location = New System.Drawing.Point(5, 276)
        Me.cmbDataSource.Name = "cmbDataSource"
        Me.cmbDataSource.Size = New System.Drawing.Size(209, 21)
        Me.cmbDataSource.TabIndex = 116
        '
        'Label76
        '
        Me.Label76.AutoSize = True
        Me.Label76.Location = New System.Drawing.Point(3, 260)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(70, 13)
        Me.Label76.TabIndex = 115
        Me.Label76.Text = "Data Source:"
        '
        'btnReplaceSpecialNode
        '
        Me.btnReplaceSpecialNode.Location = New System.Drawing.Point(99, 137)
        Me.btnReplaceSpecialNode.Name = "btnReplaceSpecialNode"
        Me.btnReplaceSpecialNode.Size = New System.Drawing.Size(56, 22)
        Me.btnReplaceSpecialNode.TabIndex = 114
        Me.btnReplaceSpecialNode.Text = "Replace"
        Me.btnReplaceSpecialNode.UseVisualStyleBackColor = True
        '
        'btnInsertSpecialNode
        '
        Me.btnInsertSpecialNode.Location = New System.Drawing.Point(51, 137)
        Me.btnInsertSpecialNode.Name = "btnInsertSpecialNode"
        Me.btnInsertSpecialNode.Size = New System.Drawing.Size(42, 22)
        Me.btnInsertSpecialNode.TabIndex = 113
        Me.btnInsertSpecialNode.Text = "Insert"
        Me.btnInsertSpecialNode.UseVisualStyleBackColor = True
        '
        'Label74
        '
        Me.Label74.AutoSize = True
        Me.Label74.Location = New System.Drawing.Point(5, 188)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(63, 13)
        Me.Label74.TabIndex = 112
        Me.Label74.Text = "Description:"
        '
        'txtSpecialNodeDescr
        '
        Me.txtSpecialNodeDescr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSpecialNodeDescr.Location = New System.Drawing.Point(5, 204)
        Me.txtSpecialNodeDescr.Multiline = True
        Me.txtSpecialNodeDescr.Name = "txtSpecialNodeDescr"
        Me.txtSpecialNodeDescr.Size = New System.Drawing.Size(211, 53)
        Me.txtSpecialNodeDescr.TabIndex = 111
        '
        'txtSpecialNodeName
        '
        Me.txtSpecialNodeName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSpecialNodeName.Location = New System.Drawing.Point(5, 165)
        Me.txtSpecialNodeName.Name = "txtSpecialNodeName"
        Me.txtSpecialNodeName.Size = New System.Drawing.Size(211, 20)
        Me.txtSpecialNodeName.TabIndex = 110
        '
        'Label75
        '
        Me.Label75.AutoSize = True
        Me.Label75.Location = New System.Drawing.Point(5, 149)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(38, 13)
        Me.Label75.TabIndex = 109
        Me.Label75.Text = "Name:"
        '
        'btnAppendSpecialNode
        '
        Me.btnAppendSpecialNode.Location = New System.Drawing.Point(161, 137)
        Me.btnAppendSpecialNode.Name = "btnAppendSpecialNode"
        Me.btnAppendSpecialNode.Size = New System.Drawing.Size(54, 22)
        Me.btnAppendSpecialNode.TabIndex = 108
        Me.btnAppendSpecialNode.Text = "Append"
        Me.btnAppendSpecialNode.UseVisualStyleBackColor = True
        '
        'rbCollection
        '
        Me.rbCollection.AutoSize = True
        Me.rbCollection.Location = New System.Drawing.Point(52, 30)
        Me.rbCollection.Name = "rbCollection"
        Me.rbCollection.Size = New System.Drawing.Size(71, 17)
        Me.rbCollection.TabIndex = 95
        Me.rbCollection.TabStop = True
        Me.rbCollection.Text = "Collection"
        Me.rbCollection.UseVisualStyleBackColor = True
        '
        'pbIconCollection
        '
        Me.pbIconCollection.Location = New System.Drawing.Point(6, 19)
        Me.pbIconCollection.Name = "pbIconCollection"
        Me.pbIconCollection.Size = New System.Drawing.Size(40, 40)
        Me.pbIconCollection.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIconCollection.TabIndex = 94
        Me.pbIconCollection.TabStop = False
        '
        'rbMatrixCopy
        '
        Me.rbMatrixCopy.AutoSize = True
        Me.rbMatrixCopy.Location = New System.Drawing.Point(52, 109)
        Me.rbMatrixCopy.Name = "rbMatrixCopy"
        Me.rbMatrixCopy.Size = New System.Drawing.Size(80, 17)
        Me.rbMatrixCopy.TabIndex = 93
        Me.rbMatrixCopy.TabStop = True
        Me.rbMatrixCopy.Text = "Matrix Copy"
        Me.rbMatrixCopy.UseVisualStyleBackColor = True
        '
        'pbIconMatrixCopy
        '
        Me.pbIconMatrixCopy.Location = New System.Drawing.Point(6, 97)
        Me.pbIconMatrixCopy.Name = "pbIconMatrixCopy"
        Me.pbIconMatrixCopy.Size = New System.Drawing.Size(40, 40)
        Me.pbIconMatrixCopy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIconMatrixCopy.TabIndex = 92
        Me.pbIconMatrixCopy.TabStop = False
        '
        'rbScalarCopy
        '
        Me.rbScalarCopy.AutoSize = True
        Me.rbScalarCopy.Location = New System.Drawing.Point(52, 70)
        Me.rbScalarCopy.Name = "rbScalarCopy"
        Me.rbScalarCopy.Size = New System.Drawing.Size(82, 17)
        Me.rbScalarCopy.TabIndex = 91
        Me.rbScalarCopy.TabStop = True
        Me.rbScalarCopy.Text = "Scalar Copy"
        Me.rbScalarCopy.UseVisualStyleBackColor = True
        '
        'pbIconScalarCopy
        '
        Me.pbIconScalarCopy.Location = New System.Drawing.Point(6, 58)
        Me.pbIconScalarCopy.Name = "pbIconScalarCopy"
        Me.pbIconScalarCopy.Size = New System.Drawing.Size(40, 40)
        Me.pbIconScalarCopy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIconScalarCopy.TabIndex = 90
        Me.pbIconScalarCopy.TabStop = False
        '
        'txtNodeInfo
        '
        Me.txtNodeInfo.Location = New System.Drawing.Point(7, 488)
        Me.txtNodeInfo.Multiline = True
        Me.txtNodeInfo.Name = "txtNodeInfo"
        Me.txtNodeInfo.Size = New System.Drawing.Size(222, 65)
        Me.txtNodeInfo.TabIndex = 103
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Location = New System.Drawing.Point(6, 472)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(91, 13)
        Me.Label65.TabIndex = 102
        Me.Label65.Text = "Node Information:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnReplaceTwoMatrixOp)
        Me.GroupBox3.Controls.Add(Me.btnInsertTwoMatrixOp)
        Me.GroupBox3.Controls.Add(Me.txtTwoMatrixOpDescr)
        Me.GroupBox3.Controls.Add(Me.Label64)
        Me.GroupBox3.Controls.Add(Me.txtTwoMatrixOpName)
        Me.GroupBox3.Controls.Add(Me.Label61)
        Me.GroupBox3.Controls.Add(Me.btnAppendTwoMatrixOp)
        Me.GroupBox3.Controls.Add(Me.pbIconMatrixMultMatrix)
        Me.GroupBox3.Controls.Add(Me.pbIconMatrixAddMatrix)
        Me.GroupBox3.Controls.Add(Me.rbMultMatrix)
        Me.GroupBox3.Controls.Add(Me.rbAddMatrix)
        Me.GroupBox3.Location = New System.Drawing.Point(464, 6)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(222, 232)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Two Matrix Operations:"
        '
        'btnReplaceTwoMatrixOp
        '
        Me.btnReplaceTwoMatrixOp.Location = New System.Drawing.Point(100, 97)
        Me.btnReplaceTwoMatrixOp.Name = "btnReplaceTwoMatrixOp"
        Me.btnReplaceTwoMatrixOp.Size = New System.Drawing.Size(56, 22)
        Me.btnReplaceTwoMatrixOp.TabIndex = 108
        Me.btnReplaceTwoMatrixOp.Text = "Replace"
        Me.btnReplaceTwoMatrixOp.UseVisualStyleBackColor = True
        '
        'btnInsertTwoMatrixOp
        '
        Me.btnInsertTwoMatrixOp.Location = New System.Drawing.Point(52, 97)
        Me.btnInsertTwoMatrixOp.Name = "btnInsertTwoMatrixOp"
        Me.btnInsertTwoMatrixOp.Size = New System.Drawing.Size(42, 22)
        Me.btnInsertTwoMatrixOp.TabIndex = 107
        Me.btnInsertTwoMatrixOp.Text = "Insert"
        Me.btnInsertTwoMatrixOp.UseVisualStyleBackColor = True
        '
        'txtTwoMatrixOpDescr
        '
        Me.txtTwoMatrixOpDescr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTwoMatrixOpDescr.Location = New System.Drawing.Point(6, 164)
        Me.txtTwoMatrixOpDescr.Multiline = True
        Me.txtTwoMatrixOpDescr.Name = "txtTwoMatrixOpDescr"
        Me.txtTwoMatrixOpDescr.Size = New System.Drawing.Size(210, 53)
        Me.txtTwoMatrixOpDescr.TabIndex = 103
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Location = New System.Drawing.Point(6, 148)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(63, 13)
        Me.Label64.TabIndex = 102
        Me.Label64.Text = "Description:"
        '
        'txtTwoMatrixOpName
        '
        Me.txtTwoMatrixOpName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTwoMatrixOpName.Location = New System.Drawing.Point(6, 125)
        Me.txtTwoMatrixOpName.Name = "txtTwoMatrixOpName"
        Me.txtTwoMatrixOpName.Size = New System.Drawing.Size(210, 20)
        Me.txtTwoMatrixOpName.TabIndex = 101
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Location = New System.Drawing.Point(6, 109)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(38, 13)
        Me.Label61.TabIndex = 100
        Me.Label61.Text = "Name:"
        '
        'btnAppendTwoMatrixOp
        '
        Me.btnAppendTwoMatrixOp.Location = New System.Drawing.Point(162, 97)
        Me.btnAppendTwoMatrixOp.Name = "btnAppendTwoMatrixOp"
        Me.btnAppendTwoMatrixOp.Size = New System.Drawing.Size(54, 22)
        Me.btnAppendTwoMatrixOp.TabIndex = 99
        Me.btnAppendTwoMatrixOp.Text = "Append"
        Me.btnAppendTwoMatrixOp.UseVisualStyleBackColor = True
        '
        'pbIconMatrixMultMatrix
        '
        Me.pbIconMatrixMultMatrix.Location = New System.Drawing.Point(6, 58)
        Me.pbIconMatrixMultMatrix.Name = "pbIconMatrixMultMatrix"
        Me.pbIconMatrixMultMatrix.Size = New System.Drawing.Size(40, 40)
        Me.pbIconMatrixMultMatrix.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIconMatrixMultMatrix.TabIndex = 98
        Me.pbIconMatrixMultMatrix.TabStop = False
        '
        'pbIconMatrixAddMatrix
        '
        Me.pbIconMatrixAddMatrix.Location = New System.Drawing.Point(6, 19)
        Me.pbIconMatrixAddMatrix.Name = "pbIconMatrixAddMatrix"
        Me.pbIconMatrixAddMatrix.Size = New System.Drawing.Size(40, 40)
        Me.pbIconMatrixAddMatrix.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIconMatrixAddMatrix.TabIndex = 97
        Me.pbIconMatrixAddMatrix.TabStop = False
        '
        'rbMultMatrix
        '
        Me.rbMultMatrix.AutoSize = True
        Me.rbMultMatrix.Location = New System.Drawing.Point(52, 70)
        Me.rbMultMatrix.Name = "rbMultMatrix"
        Me.rbMultMatrix.Size = New System.Drawing.Size(114, 17)
        Me.rbMultMatrix.TabIndex = 7
        Me.rbMultMatrix.TabStop = True
        Me.rbMultMatrix.Text = "Multiply by a Matrix"
        Me.rbMultMatrix.UseVisualStyleBackColor = True
        '
        'rbAddMatrix
        '
        Me.rbAddMatrix.AutoSize = True
        Me.rbAddMatrix.Location = New System.Drawing.Point(52, 31)
        Me.rbAddMatrix.Name = "rbAddMatrix"
        Me.rbAddMatrix.Size = New System.Drawing.Size(84, 17)
        Me.rbAddMatrix.TabIndex = 6
        Me.rbAddMatrix.TabStop = True
        Me.rbAddMatrix.Text = "Add a Matrix"
        Me.rbAddMatrix.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rbCorrelation)
        Me.GroupBox2.Controls.Add(Me.rbCovariance)
        Me.GroupBox2.Controls.Add(Me.pbIconCorrelation)
        Me.GroupBox2.Controls.Add(Me.pbIconCovariance)
        Me.GroupBox2.Controls.Add(Me.btnReplaceOneMatrixOp)
        Me.GroupBox2.Controls.Add(Me.btnInsertOneMatrixOp)
        Me.GroupBox2.Controls.Add(Me.Label62)
        Me.GroupBox2.Controls.Add(Me.txtOneMatrixOpDescr)
        Me.GroupBox2.Controls.Add(Me.txtOneMatrixOpName)
        Me.GroupBox2.Controls.Add(Me.Label60)
        Me.GroupBox2.Controls.Add(Me.btnAppendOneMatrixOp)
        Me.GroupBox2.Controls.Add(Me.pbIconMatrixDivScalar)
        Me.GroupBox2.Controls.Add(Me.pbIconMatrixMultScalar)
        Me.GroupBox2.Controls.Add(Me.pbIconMatrixAddScalar)
        Me.GroupBox2.Controls.Add(Me.pbIconMatrixTransChol)
        Me.GroupBox2.Controls.Add(Me.pbIconMatrixCholesky)
        Me.GroupBox2.Controls.Add(Me.pbIconMatrixInverse)
        Me.GroupBox2.Controls.Add(Me.pbIconMatrixTranspose)
        Me.GroupBox2.Controls.Add(Me.rbDivScalar)
        Me.GroupBox2.Controls.Add(Me.rbMultScalar)
        Me.GroupBox2.Controls.Add(Me.rbAddScalar)
        Me.GroupBox2.Controls.Add(Me.rbTransCholesky)
        Me.GroupBox2.Controls.Add(Me.rbCholesky)
        Me.GroupBox2.Controls.Add(Me.rbInverse)
        Me.GroupBox2.Controls.Add(Me.rbTranspose)
        Me.GroupBox2.Location = New System.Drawing.Point(235, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(223, 505)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "One Matrix Operations"
        '
        'rbCorrelation
        '
        Me.rbCorrelation.AutoSize = True
        Me.rbCorrelation.Location = New System.Drawing.Point(52, 341)
        Me.rbCorrelation.Name = "rbCorrelation"
        Me.rbCorrelation.Size = New System.Drawing.Size(75, 17)
        Me.rbCorrelation.TabIndex = 111
        Me.rbCorrelation.TabStop = True
        Me.rbCorrelation.Text = "Correlation"
        Me.rbCorrelation.UseVisualStyleBackColor = True
        '
        'rbCovariance
        '
        Me.rbCovariance.AutoSize = True
        Me.rbCovariance.Location = New System.Drawing.Point(52, 304)
        Me.rbCovariance.Name = "rbCovariance"
        Me.rbCovariance.Size = New System.Drawing.Size(79, 17)
        Me.rbCovariance.TabIndex = 110
        Me.rbCovariance.TabStop = True
        Me.rbCovariance.Text = "Covariance"
        Me.rbCovariance.UseVisualStyleBackColor = True
        '
        'pbIconCorrelation
        '
        Me.pbIconCorrelation.Location = New System.Drawing.Point(6, 331)
        Me.pbIconCorrelation.Name = "pbIconCorrelation"
        Me.pbIconCorrelation.Size = New System.Drawing.Size(40, 40)
        Me.pbIconCorrelation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIconCorrelation.TabIndex = 109
        Me.pbIconCorrelation.TabStop = False
        '
        'pbIconCovariance
        '
        Me.pbIconCovariance.Location = New System.Drawing.Point(6, 292)
        Me.pbIconCovariance.Name = "pbIconCovariance"
        Me.pbIconCovariance.Size = New System.Drawing.Size(40, 40)
        Me.pbIconCovariance.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIconCovariance.TabIndex = 108
        Me.pbIconCovariance.TabStop = False
        '
        'btnReplaceOneMatrixOp
        '
        Me.btnReplaceOneMatrixOp.Location = New System.Drawing.Point(100, 371)
        Me.btnReplaceOneMatrixOp.Name = "btnReplaceOneMatrixOp"
        Me.btnReplaceOneMatrixOp.Size = New System.Drawing.Size(56, 22)
        Me.btnReplaceOneMatrixOp.TabIndex = 107
        Me.btnReplaceOneMatrixOp.Text = "Replace"
        Me.btnReplaceOneMatrixOp.UseVisualStyleBackColor = True
        '
        'btnInsertOneMatrixOp
        '
        Me.btnInsertOneMatrixOp.Location = New System.Drawing.Point(52, 371)
        Me.btnInsertOneMatrixOp.Name = "btnInsertOneMatrixOp"
        Me.btnInsertOneMatrixOp.Size = New System.Drawing.Size(42, 22)
        Me.btnInsertOneMatrixOp.TabIndex = 106
        Me.btnInsertOneMatrixOp.Text = "Insert"
        Me.btnInsertOneMatrixOp.UseVisualStyleBackColor = True
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Location = New System.Drawing.Point(6, 422)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(63, 13)
        Me.Label62.TabIndex = 101
        Me.Label62.Text = "Description:"
        '
        'txtOneMatrixOpDescr
        '
        Me.txtOneMatrixOpDescr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOneMatrixOpDescr.Location = New System.Drawing.Point(6, 438)
        Me.txtOneMatrixOpDescr.Multiline = True
        Me.txtOneMatrixOpDescr.Name = "txtOneMatrixOpDescr"
        Me.txtOneMatrixOpDescr.Size = New System.Drawing.Size(211, 53)
        Me.txtOneMatrixOpDescr.TabIndex = 100
        '
        'txtOneMatrixOpName
        '
        Me.txtOneMatrixOpName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOneMatrixOpName.Location = New System.Drawing.Point(6, 399)
        Me.txtOneMatrixOpName.Name = "txtOneMatrixOpName"
        Me.txtOneMatrixOpName.Size = New System.Drawing.Size(211, 20)
        Me.txtOneMatrixOpName.TabIndex = 99
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.Location = New System.Drawing.Point(6, 383)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(38, 13)
        Me.Label60.TabIndex = 98
        Me.Label60.Text = "Name:"
        '
        'btnAppendOneMatrixOp
        '
        Me.btnAppendOneMatrixOp.Location = New System.Drawing.Point(162, 371)
        Me.btnAppendOneMatrixOp.Name = "btnAppendOneMatrixOp"
        Me.btnAppendOneMatrixOp.Size = New System.Drawing.Size(54, 22)
        Me.btnAppendOneMatrixOp.TabIndex = 97
        Me.btnAppendOneMatrixOp.Text = "Append"
        Me.btnAppendOneMatrixOp.UseVisualStyleBackColor = True
        '
        'pbIconMatrixDivScalar
        '
        Me.pbIconMatrixDivScalar.Location = New System.Drawing.Point(6, 253)
        Me.pbIconMatrixDivScalar.Name = "pbIconMatrixDivScalar"
        Me.pbIconMatrixDivScalar.Size = New System.Drawing.Size(40, 40)
        Me.pbIconMatrixDivScalar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIconMatrixDivScalar.TabIndex = 96
        Me.pbIconMatrixDivScalar.TabStop = False
        '
        'pbIconMatrixMultScalar
        '
        Me.pbIconMatrixMultScalar.Location = New System.Drawing.Point(6, 214)
        Me.pbIconMatrixMultScalar.Name = "pbIconMatrixMultScalar"
        Me.pbIconMatrixMultScalar.Size = New System.Drawing.Size(40, 40)
        Me.pbIconMatrixMultScalar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIconMatrixMultScalar.TabIndex = 95
        Me.pbIconMatrixMultScalar.TabStop = False
        '
        'pbIconMatrixAddScalar
        '
        Me.pbIconMatrixAddScalar.Location = New System.Drawing.Point(6, 175)
        Me.pbIconMatrixAddScalar.Name = "pbIconMatrixAddScalar"
        Me.pbIconMatrixAddScalar.Size = New System.Drawing.Size(40, 40)
        Me.pbIconMatrixAddScalar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIconMatrixAddScalar.TabIndex = 94
        Me.pbIconMatrixAddScalar.TabStop = False
        '
        'pbIconMatrixTransChol
        '
        Me.pbIconMatrixTransChol.Location = New System.Drawing.Point(6, 136)
        Me.pbIconMatrixTransChol.Name = "pbIconMatrixTransChol"
        Me.pbIconMatrixTransChol.Size = New System.Drawing.Size(40, 40)
        Me.pbIconMatrixTransChol.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIconMatrixTransChol.TabIndex = 93
        Me.pbIconMatrixTransChol.TabStop = False
        '
        'pbIconMatrixCholesky
        '
        Me.pbIconMatrixCholesky.Location = New System.Drawing.Point(6, 97)
        Me.pbIconMatrixCholesky.Name = "pbIconMatrixCholesky"
        Me.pbIconMatrixCholesky.Size = New System.Drawing.Size(40, 40)
        Me.pbIconMatrixCholesky.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIconMatrixCholesky.TabIndex = 92
        Me.pbIconMatrixCholesky.TabStop = False
        '
        'pbIconMatrixInverse
        '
        Me.pbIconMatrixInverse.Location = New System.Drawing.Point(6, 58)
        Me.pbIconMatrixInverse.Name = "pbIconMatrixInverse"
        Me.pbIconMatrixInverse.Size = New System.Drawing.Size(40, 40)
        Me.pbIconMatrixInverse.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIconMatrixInverse.TabIndex = 91
        Me.pbIconMatrixInverse.TabStop = False
        '
        'pbIconMatrixTranspose
        '
        Me.pbIconMatrixTranspose.Location = New System.Drawing.Point(6, 19)
        Me.pbIconMatrixTranspose.Name = "pbIconMatrixTranspose"
        Me.pbIconMatrixTranspose.Size = New System.Drawing.Size(40, 40)
        Me.pbIconMatrixTranspose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIconMatrixTranspose.TabIndex = 90
        Me.pbIconMatrixTranspose.TabStop = False
        '
        'rbDivScalar
        '
        Me.rbDivScalar.AutoSize = True
        Me.rbDivScalar.Location = New System.Drawing.Point(52, 265)
        Me.rbDivScalar.Name = "rbDivScalar"
        Me.rbDivScalar.Size = New System.Drawing.Size(111, 17)
        Me.rbDivScalar.TabIndex = 6
        Me.rbDivScalar.TabStop = True
        Me.rbDivScalar.Text = "Divide by a Scalar"
        Me.rbDivScalar.UseVisualStyleBackColor = True
        '
        'rbMultScalar
        '
        Me.rbMultScalar.AutoSize = True
        Me.rbMultScalar.Location = New System.Drawing.Point(52, 226)
        Me.rbMultScalar.Name = "rbMultScalar"
        Me.rbMultScalar.Size = New System.Drawing.Size(116, 17)
        Me.rbMultScalar.TabIndex = 5
        Me.rbMultScalar.TabStop = True
        Me.rbMultScalar.Text = "Multiply by a Scalar"
        Me.rbMultScalar.UseVisualStyleBackColor = True
        '
        'rbAddScalar
        '
        Me.rbAddScalar.AutoSize = True
        Me.rbAddScalar.Location = New System.Drawing.Point(52, 187)
        Me.rbAddScalar.Name = "rbAddScalar"
        Me.rbAddScalar.Size = New System.Drawing.Size(86, 17)
        Me.rbAddScalar.TabIndex = 4
        Me.rbAddScalar.TabStop = True
        Me.rbAddScalar.Text = "Add a Scalar"
        Me.rbAddScalar.UseVisualStyleBackColor = True
        '
        'rbTransCholesky
        '
        Me.rbTransCholesky.AutoSize = True
        Me.rbTransCholesky.Location = New System.Drawing.Point(52, 148)
        Me.rbTransCholesky.Name = "rbTransCholesky"
        Me.rbTransCholesky.Size = New System.Drawing.Size(127, 17)
        Me.rbTransCholesky.TabIndex = 3
        Me.rbTransCholesky.TabStop = True
        Me.rbTransCholesky.Text = "Transposed Cholesky"
        Me.rbTransCholesky.UseVisualStyleBackColor = True
        '
        'rbCholesky
        '
        Me.rbCholesky.AutoSize = True
        Me.rbCholesky.Location = New System.Drawing.Point(52, 109)
        Me.rbCholesky.Name = "rbCholesky"
        Me.rbCholesky.Size = New System.Drawing.Size(131, 17)
        Me.rbCholesky.TabIndex = 2
        Me.rbCholesky.TabStop = True
        Me.rbCholesky.Text = "Cholesky Factorization"
        Me.rbCholesky.UseVisualStyleBackColor = True
        '
        'rbInverse
        '
        Me.rbInverse.AutoSize = True
        Me.rbInverse.Location = New System.Drawing.Point(52, 70)
        Me.rbInverse.Name = "rbInverse"
        Me.rbInverse.Size = New System.Drawing.Size(60, 17)
        Me.rbInverse.TabIndex = 1
        Me.rbInverse.TabStop = True
        Me.rbInverse.Text = "Inverse"
        Me.rbInverse.UseVisualStyleBackColor = True
        '
        'rbTranspose
        '
        Me.rbTranspose.AutoSize = True
        Me.rbTranspose.Location = New System.Drawing.Point(52, 31)
        Me.rbTranspose.Name = "rbTranspose"
        Me.rbTranspose.Size = New System.Drawing.Size(75, 17)
        Me.rbTranspose.TabIndex = 0
        Me.rbTranspose.TabStop = True
        Me.rbTranspose.Text = "Transpose"
        Me.rbTranspose.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnReplaceData)
        Me.GroupBox1.Controls.Add(Me.btnInsertData)
        Me.GroupBox1.Controls.Add(Me.rbProcess)
        Me.GroupBox1.Controls.Add(Me.pbIconProcess)
        Me.GroupBox1.Controls.Add(Me.Label63)
        Me.GroupBox1.Controls.Add(Me.txtDataDescr)
        Me.GroupBox1.Controls.Add(Me.txtDataName)
        Me.GroupBox1.Controls.Add(Me.Label59)
        Me.GroupBox1.Controls.Add(Me.rbMatrixProcess)
        Me.GroupBox1.Controls.Add(Me.pbIconScalarProcess)
        Me.GroupBox1.Controls.Add(Me.rbScalarProcess)
        Me.GroupBox1.Controls.Add(Me.pbIconMatrixProcess)
        Me.GroupBox1.Controls.Add(Me.pbIconMatrixPreDefScalar)
        Me.GroupBox1.Controls.Add(Me.pbIconMatrixUserDefScalar)
        Me.GroupBox1.Controls.Add(Me.pbIconMatrixOpen)
        Me.GroupBox1.Controls.Add(Me.pbIconMatrixUserDef)
        Me.GroupBox1.Controls.Add(Me.pbIconMatrix)
        Me.GroupBox1.Controls.Add(Me.rbUserDefScalar)
        Me.GroupBox1.Controls.Add(Me.rbScalar)
        Me.GroupBox1.Controls.Add(Me.rbMatrix)
        Me.GroupBox1.Controls.Add(Me.btnAppendData)
        Me.GroupBox1.Controls.Add(Me.rbUserDefMatrix)
        Me.GroupBox1.Controls.Add(Me.rbOpenMatrixFile)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(223, 460)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Input or Calculated Data"
        '
        'btnReplaceData
        '
        Me.btnReplaceData.Location = New System.Drawing.Point(100, 332)
        Me.btnReplaceData.Name = "btnReplaceData"
        Me.btnReplaceData.Size = New System.Drawing.Size(56, 22)
        Me.btnReplaceData.TabIndex = 106
        Me.btnReplaceData.Text = "Replace"
        Me.btnReplaceData.UseVisualStyleBackColor = True
        '
        'btnInsertData
        '
        Me.btnInsertData.Location = New System.Drawing.Point(52, 332)
        Me.btnInsertData.Name = "btnInsertData"
        Me.btnInsertData.Size = New System.Drawing.Size(42, 22)
        Me.btnInsertData.TabIndex = 105
        Me.btnInsertData.Text = "Insert"
        Me.btnInsertData.UseVisualStyleBackColor = True
        '
        'rbProcess
        '
        Me.rbProcess.AutoSize = True
        Me.rbProcess.Location = New System.Drawing.Point(52, 226)
        Me.rbProcess.Name = "rbProcess"
        Me.rbProcess.Size = New System.Drawing.Size(63, 17)
        Me.rbProcess.TabIndex = 104
        Me.rbProcess.TabStop = True
        Me.rbProcess.Text = "Process"
        Me.rbProcess.UseVisualStyleBackColor = True
        '
        'pbIconProcess
        '
        Me.pbIconProcess.Location = New System.Drawing.Point(6, 214)
        Me.pbIconProcess.Name = "pbIconProcess"
        Me.pbIconProcess.Size = New System.Drawing.Size(40, 40)
        Me.pbIconProcess.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIconProcess.TabIndex = 103
        Me.pbIconProcess.TabStop = False
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Location = New System.Drawing.Point(6, 384)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(63, 13)
        Me.Label63.TabIndex = 102
        Me.Label63.Text = "Description:"
        '
        'txtDataDescr
        '
        Me.txtDataDescr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDataDescr.Location = New System.Drawing.Point(6, 400)
        Me.txtDataDescr.Multiline = True
        Me.txtDataDescr.Name = "txtDataDescr"
        Me.txtDataDescr.Size = New System.Drawing.Size(211, 53)
        Me.txtDataDescr.TabIndex = 96
        '
        'txtDataName
        '
        Me.txtDataName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDataName.Location = New System.Drawing.Point(6, 361)
        Me.txtDataName.Name = "txtDataName"
        Me.txtDataName.Size = New System.Drawing.Size(211, 20)
        Me.txtDataName.TabIndex = 95
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Location = New System.Drawing.Point(6, 344)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(38, 13)
        Me.Label59.TabIndex = 94
        Me.Label59.Text = "Name:"
        '
        'rbMatrixProcess
        '
        Me.rbMatrixProcess.AutoSize = True
        Me.rbMatrixProcess.Location = New System.Drawing.Point(52, 304)
        Me.rbMatrixProcess.Name = "rbMatrixProcess"
        Me.rbMatrixProcess.Size = New System.Drawing.Size(94, 17)
        Me.rbMatrixProcess.TabIndex = 93
        Me.rbMatrixProcess.TabStop = True
        Me.rbMatrixProcess.Text = "Matrix Process"
        Me.rbMatrixProcess.UseVisualStyleBackColor = True
        '
        'pbIconScalarProcess
        '
        Me.pbIconScalarProcess.Location = New System.Drawing.Point(6, 253)
        Me.pbIconScalarProcess.Name = "pbIconScalarProcess"
        Me.pbIconScalarProcess.Size = New System.Drawing.Size(40, 40)
        Me.pbIconScalarProcess.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIconScalarProcess.TabIndex = 92
        Me.pbIconScalarProcess.TabStop = False
        '
        'rbScalarProcess
        '
        Me.rbScalarProcess.AutoSize = True
        Me.rbScalarProcess.Location = New System.Drawing.Point(52, 265)
        Me.rbScalarProcess.Name = "rbScalarProcess"
        Me.rbScalarProcess.Size = New System.Drawing.Size(96, 17)
        Me.rbScalarProcess.TabIndex = 91
        Me.rbScalarProcess.TabStop = True
        Me.rbScalarProcess.Text = "Scalar Process"
        Me.rbScalarProcess.UseVisualStyleBackColor = True
        '
        'pbIconMatrixProcess
        '
        Me.pbIconMatrixProcess.Location = New System.Drawing.Point(6, 292)
        Me.pbIconMatrixProcess.Name = "pbIconMatrixProcess"
        Me.pbIconMatrixProcess.Size = New System.Drawing.Size(40, 40)
        Me.pbIconMatrixProcess.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIconMatrixProcess.TabIndex = 90
        Me.pbIconMatrixProcess.TabStop = False
        '
        'pbIconMatrixPreDefScalar
        '
        Me.pbIconMatrixPreDefScalar.Location = New System.Drawing.Point(6, 19)
        Me.pbIconMatrixPreDefScalar.Name = "pbIconMatrixPreDefScalar"
        Me.pbIconMatrixPreDefScalar.Size = New System.Drawing.Size(40, 40)
        Me.pbIconMatrixPreDefScalar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIconMatrixPreDefScalar.TabIndex = 89
        Me.pbIconMatrixPreDefScalar.TabStop = False
        '
        'pbIconMatrixUserDefScalar
        '
        Me.pbIconMatrixUserDefScalar.Location = New System.Drawing.Point(6, 136)
        Me.pbIconMatrixUserDefScalar.Name = "pbIconMatrixUserDefScalar"
        Me.pbIconMatrixUserDefScalar.Size = New System.Drawing.Size(40, 40)
        Me.pbIconMatrixUserDefScalar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIconMatrixUserDefScalar.TabIndex = 88
        Me.pbIconMatrixUserDefScalar.TabStop = False
        '
        'pbIconMatrixOpen
        '
        Me.pbIconMatrixOpen.Location = New System.Drawing.Point(6, 97)
        Me.pbIconMatrixOpen.Name = "pbIconMatrixOpen"
        Me.pbIconMatrixOpen.Size = New System.Drawing.Size(40, 40)
        Me.pbIconMatrixOpen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIconMatrixOpen.TabIndex = 87
        Me.pbIconMatrixOpen.TabStop = False
        '
        'pbIconMatrixUserDef
        '
        Me.pbIconMatrixUserDef.Location = New System.Drawing.Point(6, 175)
        Me.pbIconMatrixUserDef.Name = "pbIconMatrixUserDef"
        Me.pbIconMatrixUserDef.Size = New System.Drawing.Size(40, 40)
        Me.pbIconMatrixUserDef.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIconMatrixUserDef.TabIndex = 86
        Me.pbIconMatrixUserDef.TabStop = False
        '
        'pbIconMatrix
        '
        Me.pbIconMatrix.Location = New System.Drawing.Point(6, 58)
        Me.pbIconMatrix.Name = "pbIconMatrix"
        Me.pbIconMatrix.Size = New System.Drawing.Size(40, 40)
        Me.pbIconMatrix.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIconMatrix.TabIndex = 85
        Me.pbIconMatrix.TabStop = False
        '
        'rbUserDefScalar
        '
        Me.rbUserDefScalar.AutoSize = True
        Me.rbUserDefScalar.Location = New System.Drawing.Point(52, 148)
        Me.rbUserDefScalar.Name = "rbUserDefScalar"
        Me.rbUserDefScalar.Size = New System.Drawing.Size(120, 17)
        Me.rbUserDefScalar.TabIndex = 4
        Me.rbUserDefScalar.TabStop = True
        Me.rbUserDefScalar.Text = "User Defined Scalar"
        Me.rbUserDefScalar.UseVisualStyleBackColor = True
        '
        'rbScalar
        '
        Me.rbScalar.AutoSize = True
        Me.rbScalar.Location = New System.Drawing.Point(52, 31)
        Me.rbScalar.Name = "rbScalar"
        Me.rbScalar.Size = New System.Drawing.Size(55, 17)
        Me.rbScalar.TabIndex = 3
        Me.rbScalar.TabStop = True
        Me.rbScalar.Text = "Scalar"
        Me.rbScalar.UseVisualStyleBackColor = True
        '
        'rbMatrix
        '
        Me.rbMatrix.AutoSize = True
        Me.rbMatrix.Location = New System.Drawing.Point(52, 70)
        Me.rbMatrix.Name = "rbMatrix"
        Me.rbMatrix.Size = New System.Drawing.Size(53, 17)
        Me.rbMatrix.TabIndex = 2
        Me.rbMatrix.TabStop = True
        Me.rbMatrix.Text = "Matrix"
        Me.rbMatrix.UseVisualStyleBackColor = True
        '
        'btnAppendData
        '
        Me.btnAppendData.Location = New System.Drawing.Point(162, 333)
        Me.btnAppendData.Name = "btnAppendData"
        Me.btnAppendData.Size = New System.Drawing.Size(54, 22)
        Me.btnAppendData.TabIndex = 1
        Me.btnAppendData.Text = "Append"
        Me.btnAppendData.UseVisualStyleBackColor = True
        '
        'rbUserDefMatrix
        '
        Me.rbUserDefMatrix.AutoSize = True
        Me.rbUserDefMatrix.Location = New System.Drawing.Point(52, 187)
        Me.rbUserDefMatrix.Name = "rbUserDefMatrix"
        Me.rbUserDefMatrix.Size = New System.Drawing.Size(118, 17)
        Me.rbUserDefMatrix.TabIndex = 1
        Me.rbUserDefMatrix.TabStop = True
        Me.rbUserDefMatrix.Text = "User Defined Matrix"
        Me.rbUserDefMatrix.UseVisualStyleBackColor = True
        '
        'rbOpenMatrixFile
        '
        Me.rbOpenMatrixFile.AutoSize = True
        Me.rbOpenMatrixFile.Location = New System.Drawing.Point(52, 109)
        Me.rbOpenMatrixFile.Name = "rbOpenMatrixFile"
        Me.rbOpenMatrixFile.Size = New System.Drawing.Size(101, 17)
        Me.rbOpenMatrixFile.TabIndex = 0
        Me.rbOpenMatrixFile.TabStop = True
        Me.rbOpenMatrixFile.Text = "Open Matrix File"
        Me.rbOpenMatrixFile.UseVisualStyleBackColor = True
        '
        'TabPage7
        '
        Me.TabPage7.Controls.Add(Me.btnCleanupNodeList)
        Me.TabPage7.Controls.Add(Me.btnShowNodeList)
        Me.TabPage7.Controls.Add(Me.btnCheckNodeCopyLists)
        Me.TabPage7.Controls.Add(Me.GroupBox6)
        Me.TabPage7.Controls.Add(Me.btnMoveNodeDown)
        Me.TabPage7.Controls.Add(Me.btnMoveNodeUp)
        Me.TabPage7.Controls.Add(Me.btnCloneNode)
        Me.TabPage7.Controls.Add(Me.btnPasteNode)
        Me.TabPage7.Controls.Add(Me.btnCutNode)
        Me.TabPage7.Controls.Add(Me.btnListChildNodes)
        Me.TabPage7.Controls.Add(Me.btnDeleteNode)
        Me.TabPage7.Location = New System.Drawing.Point(4, 22)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Size = New System.Drawing.Size(704, 585)
        Me.TabPage7.TabIndex = 2
        Me.TabPage7.Text = "Edit Nodes"
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'btnCleanupNodeList
        '
        Me.btnCleanupNodeList.Location = New System.Drawing.Point(13, 303)
        Me.btnCleanupNodeList.Name = "btnCleanupNodeList"
        Me.btnCleanupNodeList.Size = New System.Drawing.Size(129, 22)
        Me.btnCleanupNodeList.TabIndex = 264
        Me.btnCleanupNodeList.Text = "Clean-up Node List"
        Me.btnCleanupNodeList.UseVisualStyleBackColor = True
        '
        'btnShowNodeList
        '
        Me.btnShowNodeList.Location = New System.Drawing.Point(13, 275)
        Me.btnShowNodeList.Name = "btnShowNodeList"
        Me.btnShowNodeList.Size = New System.Drawing.Size(129, 22)
        Me.btnShowNodeList.TabIndex = 263
        Me.btnShowNodeList.Text = "Show Node List"
        Me.btnShowNodeList.UseVisualStyleBackColor = True
        '
        'btnCheckNodeCopyLists
        '
        Me.btnCheckNodeCopyLists.Location = New System.Drawing.Point(13, 247)
        Me.btnCheckNodeCopyLists.Name = "btnCheckNodeCopyLists"
        Me.btnCheckNodeCopyLists.Size = New System.Drawing.Size(129, 22)
        Me.btnCheckNodeCopyLists.TabIndex = 262
        Me.btnCheckNodeCopyLists.Text = "Check Node Copy Lists"
        Me.btnCheckNodeCopyLists.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.txtEditNodeDataType)
        Me.GroupBox6.Controls.Add(Me.Label82)
        Me.GroupBox6.Controls.Add(Me.btnApplyChanges)
        Me.GroupBox6.Controls.Add(Me.txtEditNodeDescr)
        Me.GroupBox6.Controls.Add(Me.Label81)
        Me.GroupBox6.Controls.Add(Me.Label80)
        Me.GroupBox6.Controls.Add(Me.txtEditNodeText)
        Me.GroupBox6.Controls.Add(Me.txtEditNodeType)
        Me.GroupBox6.Controls.Add(Me.Label79)
        Me.GroupBox6.Controls.Add(Me.txtEditNodeName)
        Me.GroupBox6.Controls.Add(Me.Label77)
        Me.GroupBox6.Location = New System.Drawing.Point(93, 9)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(486, 204)
        Me.GroupBox6.TabIndex = 261
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Text Edit"
        '
        'txtEditNodeDataType
        '
        Me.txtEditNodeDataType.Location = New System.Drawing.Point(358, 45)
        Me.txtEditNodeDataType.Name = "txtEditNodeDataType"
        Me.txtEditNodeDataType.ReadOnly = True
        Me.txtEditNodeDataType.Size = New System.Drawing.Size(122, 20)
        Me.txtEditNodeDataType.TabIndex = 24
        '
        'Label82
        '
        Me.Label82.AutoSize = True
        Me.Label82.Location = New System.Drawing.Point(319, 48)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(33, 13)
        Me.Label82.TabIndex = 23
        Me.Label82.Text = "Data:"
        '
        'btnApplyChanges
        '
        Me.btnApplyChanges.Location = New System.Drawing.Point(6, 162)
        Me.btnApplyChanges.Name = "btnApplyChanges"
        Me.btnApplyChanges.Size = New System.Drawing.Size(65, 36)
        Me.btnApplyChanges.TabIndex = 22
        Me.btnApplyChanges.Text = "Apply Changes"
        Me.btnApplyChanges.UseVisualStyleBackColor = True
        '
        'txtEditNodeDescr
        '
        Me.txtEditNodeDescr.Location = New System.Drawing.Point(77, 97)
        Me.txtEditNodeDescr.Multiline = True
        Me.txtEditNodeDescr.Name = "txtEditNodeDescr"
        Me.txtEditNodeDescr.Size = New System.Drawing.Size(403, 101)
        Me.txtEditNodeDescr.TabIndex = 21
        '
        'Label81
        '
        Me.Label81.AutoSize = True
        Me.Label81.Location = New System.Drawing.Point(6, 100)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(60, 13)
        Me.Label81.TabIndex = 20
        Me.Label81.Text = "Description"
        '
        'Label80
        '
        Me.Label80.AutoSize = True
        Me.Label80.Location = New System.Drawing.Point(6, 74)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(31, 13)
        Me.Label80.TabIndex = 19
        Me.Label80.Text = "Text:"
        '
        'txtEditNodeText
        '
        Me.txtEditNodeText.Location = New System.Drawing.Point(77, 71)
        Me.txtEditNodeText.Name = "txtEditNodeText"
        Me.txtEditNodeText.ReadOnly = True
        Me.txtEditNodeText.Size = New System.Drawing.Size(403, 20)
        Me.txtEditNodeText.TabIndex = 18
        '
        'txtEditNodeType
        '
        Me.txtEditNodeType.Location = New System.Drawing.Point(77, 45)
        Me.txtEditNodeType.Name = "txtEditNodeType"
        Me.txtEditNodeType.ReadOnly = True
        Me.txtEditNodeType.Size = New System.Drawing.Size(236, 20)
        Me.txtEditNodeType.TabIndex = 17
        '
        'Label79
        '
        Me.Label79.AutoSize = True
        Me.Label79.Location = New System.Drawing.Point(6, 48)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(34, 13)
        Me.Label79.TabIndex = 16
        Me.Label79.Text = "Type:"
        '
        'txtEditNodeName
        '
        Me.txtEditNodeName.Location = New System.Drawing.Point(77, 19)
        Me.txtEditNodeName.Name = "txtEditNodeName"
        Me.txtEditNodeName.Size = New System.Drawing.Size(403, 20)
        Me.txtEditNodeName.TabIndex = 15
        '
        'Label77
        '
        Me.Label77.AutoSize = True
        Me.Label77.Location = New System.Drawing.Point(6, 22)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(65, 13)
        Me.Label77.TabIndex = 14
        Me.Label77.Text = "Node name:"
        '
        'btnMoveNodeDown
        '
        Me.btnMoveNodeDown.Location = New System.Drawing.Point(13, 37)
        Me.btnMoveNodeDown.Name = "btnMoveNodeDown"
        Me.btnMoveNodeDown.Size = New System.Drawing.Size(74, 22)
        Me.btnMoveNodeDown.TabIndex = 260
        Me.btnMoveNodeDown.Text = "Move down"
        Me.btnMoveNodeDown.UseVisualStyleBackColor = True
        '
        'btnMoveNodeUp
        '
        Me.btnMoveNodeUp.Location = New System.Drawing.Point(13, 9)
        Me.btnMoveNodeUp.Name = "btnMoveNodeUp"
        Me.btnMoveNodeUp.Size = New System.Drawing.Size(74, 22)
        Me.btnMoveNodeUp.TabIndex = 259
        Me.btnMoveNodeUp.Text = "Move up"
        Me.btnMoveNodeUp.UseVisualStyleBackColor = True
        '
        'btnCloneNode
        '
        Me.btnCloneNode.Location = New System.Drawing.Point(13, 65)
        Me.btnCloneNode.Name = "btnCloneNode"
        Me.btnCloneNode.Size = New System.Drawing.Size(74, 22)
        Me.btnCloneNode.TabIndex = 15
        Me.btnCloneNode.Text = "Copy"
        Me.btnCloneNode.UseVisualStyleBackColor = True
        '
        'btnPasteNode
        '
        Me.btnPasteNode.Location = New System.Drawing.Point(13, 121)
        Me.btnPasteNode.Name = "btnPasteNode"
        Me.btnPasteNode.Size = New System.Drawing.Size(74, 22)
        Me.btnPasteNode.TabIndex = 14
        Me.btnPasteNode.Text = "Paste"
        Me.btnPasteNode.UseVisualStyleBackColor = True
        '
        'btnCutNode
        '
        Me.btnCutNode.Location = New System.Drawing.Point(13, 93)
        Me.btnCutNode.Name = "btnCutNode"
        Me.btnCutNode.Size = New System.Drawing.Size(74, 22)
        Me.btnCutNode.TabIndex = 13
        Me.btnCutNode.Text = "Cut"
        Me.btnCutNode.UseVisualStyleBackColor = True
        '
        'btnListChildNodes
        '
        Me.btnListChildNodes.Location = New System.Drawing.Point(13, 219)
        Me.btnListChildNodes.Name = "btnListChildNodes"
        Me.btnListChildNodes.Size = New System.Drawing.Size(129, 22)
        Me.btnListChildNodes.TabIndex = 12
        Me.btnListChildNodes.Text = "List Child Nodes"
        Me.btnListChildNodes.UseVisualStyleBackColor = True
        '
        'btnDeleteNode
        '
        Me.btnDeleteNode.Location = New System.Drawing.Point(13, 149)
        Me.btnDeleteNode.Name = "btnDeleteNode"
        Me.btnDeleteNode.Size = New System.Drawing.Size(74, 22)
        Me.btnDeleteNode.TabIndex = 10
        Me.btnDeleteNode.Text = "Delete"
        Me.btnDeleteNode.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(1054, 12)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(48, 22)
        Me.btnExit.TabIndex = 10
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "00 Operation Seq.jpg")
        Me.ImageList1.Images.SetKeyName(1, "01 Open Operation Seq.jpg")
        Me.ImageList1.Images.SetKeyName(2, "02 Scalar.jpg")
        Me.ImageList1.Images.SetKeyName(3, "03 Open Scalar.jpg")
        Me.ImageList1.Images.SetKeyName(4, "04 Matrix.jpg")
        Me.ImageList1.Images.SetKeyName(5, "05 Open Matrix.jpg")
        Me.ImageList1.Images.SetKeyName(6, "06 Open File.jpg")
        Me.ImageList1.Images.SetKeyName(7, "07 Open - Open File.jpg")
        Me.ImageList1.Images.SetKeyName(8, "08 Scalar - User Defined.jpg")
        Me.ImageList1.Images.SetKeyName(9, "09 Open Scalar - User Defined.jpg")
        Me.ImageList1.Images.SetKeyName(10, "10 Matrix - User Defined.jpg")
        Me.ImageList1.Images.SetKeyName(11, "11 Open Matrix - User Defined.jpg")
        Me.ImageList1.Images.SetKeyName(12, "12 Process.jpg")
        Me.ImageList1.Images.SetKeyName(13, "13 Open Process.jpg")
        Me.ImageList1.Images.SetKeyName(14, "14 Scalar Process.jpg")
        Me.ImageList1.Images.SetKeyName(15, "15 Open Scalar Process.jpg")
        Me.ImageList1.Images.SetKeyName(16, "16 Matrix Process.jpg")
        Me.ImageList1.Images.SetKeyName(17, "17 Open Matrix Process.jpg")
        Me.ImageList1.Images.SetKeyName(18, "18 MatrixTranspose.jpg")
        Me.ImageList1.Images.SetKeyName(19, "19 Open Matrix Transpose.jpg")
        Me.ImageList1.Images.SetKeyName(20, "20 Matrix Inverse.jpg")
        Me.ImageList1.Images.SetKeyName(21, "21 Open Matrix Inverse.jpg")
        Me.ImageList1.Images.SetKeyName(22, "22 Cholesky Factorization.jpg")
        Me.ImageList1.Images.SetKeyName(23, "23 Open Cholesky Factorization.jpg")
        Me.ImageList1.Images.SetKeyName(24, "24 Transposed Cholesky.jpg")
        Me.ImageList1.Images.SetKeyName(25, "25 Open Transposed Cholesky.jpg")
        Me.ImageList1.Images.SetKeyName(26, "26 Matrix Add Scalar H547.jpg")
        Me.ImageList1.Images.SetKeyName(27, "27 Open Matrix Add Scalar.jpg")
        Me.ImageList1.Images.SetKeyName(28, "28 Matrix Multiply Scalar.jpg")
        Me.ImageList1.Images.SetKeyName(29, "29 Open Matrix Multiply Scalar.jpg")
        Me.ImageList1.Images.SetKeyName(30, "30 Matrix Divide Scalar.jpg")
        Me.ImageList1.Images.SetKeyName(31, "31 Open Matrix Divide Scalar.jpg")
        Me.ImageList1.Images.SetKeyName(32, "33 Matrix Add Matrix.jpg")
        Me.ImageList1.Images.SetKeyName(33, "33 Open Matrix Add Matrix.jpg")
        Me.ImageList1.Images.SetKeyName(34, "34 Matrix Multiply Matrix.jpg")
        Me.ImageList1.Images.SetKeyName(35, "35 Open Matrix Multiply Matrix.jpg")
        Me.ImageList1.Images.SetKeyName(36, "36 Collection.jpg")
        Me.ImageList1.Images.SetKeyName(37, "37 Open Collection.jpg")
        Me.ImageList1.Images.SetKeyName(38, "38 Scalar Copy.jpg")
        Me.ImageList1.Images.SetKeyName(39, "39 Open Scalar Copy.jpg")
        Me.ImageList1.Images.SetKeyName(40, "40 Matrix Copy.jpg")
        Me.ImageList1.Images.SetKeyName(41, "41 Open Matrix Copy.jpg")
        Me.ImageList1.Images.SetKeyName(42, "42 Covariance.jpg")
        Me.ImageList1.Images.SetKeyName(43, "43 Open Covariance.jpg")
        Me.ImageList1.Images.SetKeyName(44, "44 Correlation.jpg")
        Me.ImageList1.Images.SetKeyName(45, "45 Open Correlation.jpg")
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1_OpenNode})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(136, 26)
        '
        'ToolStripMenuItem1_OpenNode
        '
        Me.ToolStripMenuItem1_OpenNode.Name = "ToolStripMenuItem1_OpenNode"
        Me.ToolStripMenuItem1_OpenNode.Size = New System.Drawing.Size(135, 22)
        Me.ToolStripMenuItem1_OpenNode.Text = "Open Node"
        '
        'frmMatrixOps
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1114, 695)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnExit)
        Me.Name = "frmMatrixOps"
        Me.Text = "Matrix Operations"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.dgvInputMatrix, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvOutputMatrix, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.Panel2.PerformLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.dgvInputMatrix1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvInputMatrix2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvOutputCalcMatrix, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel1.PerformLayout()
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer4.ResumeLayout(False)
        Me.SplitContainer6.Panel1.ResumeLayout(False)
        Me.SplitContainer6.Panel1.PerformLayout()
        Me.SplitContainer6.Panel2.ResumeLayout(False)
        Me.SplitContainer6.Panel2.PerformLayout()
        CType(Me.SplitContainer6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer6.ResumeLayout(False)
        CType(Me.dgvMatrix, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.SplitContainer5.Panel1.ResumeLayout(False)
        Me.SplitContainer5.Panel1.PerformLayout()
        Me.SplitContainer5.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer5.ResumeLayout(False)
        Me.TabControl2.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.dgvMatrixItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage6.ResumeLayout(False)
        Me.TabPage6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.pbIconCollection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbIconMatrixCopy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbIconScalarCopy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.pbIconMatrixMultMatrix, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbIconMatrixAddMatrix, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.pbIconCorrelation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbIconCovariance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbIconMatrixDivScalar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbIconMatrixMultScalar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbIconMatrixAddScalar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbIconMatrixTransChol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbIconMatrixCholesky, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbIconMatrixInverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbIconMatrixTranspose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.pbIconProcess, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbIconScalarProcess, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbIconMatrixProcess, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbIconMatrixPreDefScalar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbIconMatrixUserDefScalar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbIconMatrixOpen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbIconMatrixUserDef, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbIconMatrix, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage7.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents btnFormatHelp As Button
    Friend WithEvents txtInputMatrixFormat As TextBox
    Friend WithEvents Label43 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents txtInputMatrixName As TextBox
    Friend WithEvents btnPasteInputMatrix As Button
    Friend WithEvents btnCopyInputMatrix As Button
    Friend WithEvents btnSaveInputMatrix As Button
    Friend WithEvents btnNewInputMatrix As Button
    Friend WithEvents btnOpenInputMatrix As Button
    Friend WithEvents Label10 As Label
    Friend WithEvents txtInputMatrixNCols As TextBox
    Friend WithEvents txtInputMatrixNRows As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents txtInputMatrixDescr As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtInputMatrixFileName As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents dgvInputMatrix As DataGridView
    Friend WithEvents txtOutputMatrixFormat As TextBox
    Friend WithEvents Label44 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents txtOutputMatrixFileName As TextBox
    Friend WithEvents btnCopyOutputMatrix As Button
    Friend WithEvents btnSaveOutputMatrix As Button
    Friend WithEvents Label11 As Label
    Friend WithEvents txtOutputMatrixNCols As TextBox
    Friend WithEvents txtOutputMatrixNRows As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents btnApplyOneMatrixOp As Button
    Friend WithEvents cmbOneMatrixOps As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtOutputMatrixDescr As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtOutputMatrixName As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents dgvOutputMatrix As DataGridView
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents txtInputMatrix1Format As TextBox
    Friend WithEvents Label45 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents txtInputMatrix1Name As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents txtInputMatrix1NCols As TextBox
    Friend WithEvents txtInputMatrix1NRows As TextBox
    Friend WithEvents Label20 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents txtInputMatrix1Descr As TextBox
    Friend WithEvents Label22 As Label
    Friend WithEvents txtInputMatrix1FileName As TextBox
    Friend WithEvents dgvInputMatrix1 As DataGridView
    Friend WithEvents btnPasteInputMatrix1 As Button
    Friend WithEvents btnCopyInputMatrix1 As Button
    Friend WithEvents btnSaveInputMatrix1 As Button
    Friend WithEvents btnNewInputMatrix1 As Button
    Friend WithEvents btnOpenInputMatrix1 As Button
    Friend WithEvents Label14 As Label
    Friend WithEvents txtInputMatrix2Format As TextBox
    Friend WithEvents Label46 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents txtInputMatrix2Name As TextBox
    Friend WithEvents Label24 As Label
    Friend WithEvents txtInputMatrix2NCols As TextBox
    Friend WithEvents txtInputMatrix2NRows As TextBox
    Friend WithEvents Label25 As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents txtInputMatrix2Descr As TextBox
    Friend WithEvents Label27 As Label
    Friend WithEvents txtInputMatrix2FileName As TextBox
    Friend WithEvents dgvInputMatrix2 As DataGridView
    Friend WithEvents btnPasteInputMatrix2 As Button
    Friend WithEvents btnCopyInputMatrix2 As Button
    Friend WithEvents btnSaveInputMatrix2 As Button
    Friend WithEvents btnNewInputMatrix2 As Button
    Friend WithEvents btnOpenInputMatrix2 As Button
    Friend WithEvents Label15 As Label
    Friend WithEvents txtOutputCalcMatrixFormat As TextBox
    Friend WithEvents Label47 As Label
    Friend WithEvents Label32 As Label
    Friend WithEvents txtOutputCalcMatrixDescr As TextBox
    Friend WithEvents Label28 As Label
    Friend WithEvents txtOutputCalcMatrixName As TextBox
    Friend WithEvents Label29 As Label
    Friend WithEvents txtOutputCalcMatrixNCols As TextBox
    Friend WithEvents txtOutputCalcMatrixNRows As TextBox
    Friend WithEvents Label30 As Label
    Friend WithEvents Label31 As Label
    Friend WithEvents txtOutputCalcMatrixFileName As TextBox
    Friend WithEvents dgvOutputCalcMatrix As DataGridView
    Friend WithEvents btnCopyOutputCalcMatrix As Button
    Friend WithEvents btnSaveOutputCalcMatrix As Button
    Friend WithEvents btnApplyTwoMatrixOp As Button
    Friend WithEvents cmbTwoMatrixOp As ComboBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents SplitContainer4 As SplitContainer
    Friend WithEvents btnUpdateList As Button
    Friend WithEvents txtSelMatrixName As TextBox
    Friend WithEvents txtSelMatrixNCols As TextBox
    Friend WithEvents txtSelMatrixNRows As TextBox
    Friend WithEvents Label41 As Label
    Friend WithEvents Label42 As Label
    Friend WithEvents txtSelMatrixDescr As TextBox
    Friend WithEvents Label40 As Label
    Friend WithEvents Label39 As Label
    Friend WithEvents btnDeleteMatrix As Button
    Friend WithEvents btnCopyMatrix As Button
    Friend WithEvents btnOpenMatrix As Button
    Friend WithEvents lstMatrices As ListBox
    Friend WithEvents SplitContainer6 As SplitContainer
    Friend WithEvents txtMatrixFormat As TextBox
    Friend WithEvents Label48 As Label
    Friend WithEvents Label33 As Label
    Friend WithEvents dgvMatrix As DataGridView
    Friend WithEvents txtMatrixName As TextBox
    Friend WithEvents txtMatrixDescr As TextBox
    Friend WithEvents Label38 As Label
    Friend WithEvents txtMatrixFileName As TextBox
    Friend WithEvents Label37 As Label
    Friend WithEvents Label36 As Label
    Friend WithEvents Label35 As Label
    Friend WithEvents Label34 As Label
    Friend WithEvents txtMatrixNRows As TextBox
    Friend WithEvents txtMatrixNCols As TextBox
    Friend WithEvents txtSymmetric As TextBox
    Friend WithEvents Label55 As Label
    Friend WithEvents txtHermitian As TextBox
    Friend WithEvents Label54 As Label
    Friend WithEvents txtDeterminant As TextBox
    Friend WithEvents Label53 As Label
    Friend WithEvents txtRank As TextBox
    Friend WithEvents Label52 As Label
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents SplitContainer5 As SplitContainer
    Friend WithEvents txtSeqDescr As TextBox
    Friend WithEvents Label51 As Label
    Friend WithEvents txtSeqName As TextBox
    Friend WithEvents Label50 As Label
    Friend WithEvents btnNewSeq As Button
    Friend WithEvents btnSaveSeq As Button
    Friend WithEvents btnOpenSeq As Button
    Friend WithEvents txtSeqFileName As TextBox
    Friend WithEvents Label49 As Label
    Friend WithEvents trvMatrixOps As TreeView
    Friend WithEvents TabControl2 As TabControl
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents btnShowItem As Button
    Friend WithEvents txtScalarName As TextBox
    Friend WithEvents Label78 As Label
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents btnPasteData As Button
    Friend WithEvents btnCalcMatrixItem As Button
    Friend WithEvents btnFormatHelp2 As Button
    Friend WithEvents txtMatrixItemFileName As TextBox
    Friend WithEvents Label73 As Label
    Friend WithEvents btnPasteMatrixItem As Button
    Friend WithEvents btnCopyMatrixItem As Button
    Friend WithEvents btnSaveMatrixItem As Button
    Friend WithEvents btnNewMatrixItem As Button
    Friend WithEvents btnOpenMatrixItem As Button
    Friend WithEvents txtMatrixItemFormat As TextBox
    Friend WithEvents Label68 As Label
    Friend WithEvents dgvMatrixItem As DataGridView
    Friend WithEvents txtMatrixItemName As TextBox
    Friend WithEvents txtMatrixItemDescr As TextBox
    Friend WithEvents Label69 As Label
    Friend WithEvents Label70 As Label
    Friend WithEvents Label71 As Label
    Friend WithEvents Label72 As Label
    Friend WithEvents txtMatrixItemNRows As TextBox
    Friend WithEvents txtMatrixItemNCols As TextBox
    Friend WithEvents txtScalarItem As TextBox
    Friend WithEvents Label67 As Label
    Friend WithEvents Label66 As Label
    Friend WithEvents txtItemStatus As TextBox
    Friend WithEvents txtItemDescription As TextBox
    Friend WithEvents Label58 As Label
    Friend WithEvents txtItemType As TextBox
    Friend WithEvents txtItemName As TextBox
    Friend WithEvents Label57 As Label
    Friend WithEvents Label56 As Label
    Friend WithEvents TabPage6 As TabPage
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents cmbDataSource As ComboBox
    Friend WithEvents Label76 As Label
    Friend WithEvents btnReplaceSpecialNode As Button
    Friend WithEvents btnInsertSpecialNode As Button
    Friend WithEvents Label74 As Label
    Friend WithEvents txtSpecialNodeDescr As TextBox
    Friend WithEvents txtSpecialNodeName As TextBox
    Friend WithEvents Label75 As Label
    Friend WithEvents btnAppendSpecialNode As Button
    Friend WithEvents rbCollection As RadioButton
    Friend WithEvents pbIconCollection As PictureBox
    Friend WithEvents rbMatrixCopy As RadioButton
    Friend WithEvents pbIconMatrixCopy As PictureBox
    Friend WithEvents rbScalarCopy As RadioButton
    Friend WithEvents pbIconScalarCopy As PictureBox
    Friend WithEvents txtNodeInfo As TextBox
    Friend WithEvents Label65 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents btnReplaceTwoMatrixOp As Button
    Friend WithEvents btnInsertTwoMatrixOp As Button
    Friend WithEvents txtTwoMatrixOpDescr As TextBox
    Friend WithEvents Label64 As Label
    Friend WithEvents txtTwoMatrixOpName As TextBox
    Friend WithEvents Label61 As Label
    Friend WithEvents btnAppendTwoMatrixOp As Button
    Friend WithEvents pbIconMatrixMultMatrix As PictureBox
    Friend WithEvents pbIconMatrixAddMatrix As PictureBox
    Friend WithEvents rbMultMatrix As RadioButton
    Friend WithEvents rbAddMatrix As RadioButton
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents rbCorrelation As RadioButton
    Friend WithEvents rbCovariance As RadioButton
    Friend WithEvents pbIconCorrelation As PictureBox
    Friend WithEvents pbIconCovariance As PictureBox
    Friend WithEvents btnReplaceOneMatrixOp As Button
    Friend WithEvents btnInsertOneMatrixOp As Button
    Friend WithEvents Label62 As Label
    Friend WithEvents txtOneMatrixOpDescr As TextBox
    Friend WithEvents txtOneMatrixOpName As TextBox
    Friend WithEvents Label60 As Label
    Friend WithEvents btnAppendOneMatrixOp As Button
    Friend WithEvents pbIconMatrixDivScalar As PictureBox
    Friend WithEvents pbIconMatrixMultScalar As PictureBox
    Friend WithEvents pbIconMatrixAddScalar As PictureBox
    Friend WithEvents pbIconMatrixTransChol As PictureBox
    Friend WithEvents pbIconMatrixCholesky As PictureBox
    Friend WithEvents pbIconMatrixInverse As PictureBox
    Friend WithEvents pbIconMatrixTranspose As PictureBox
    Friend WithEvents rbDivScalar As RadioButton
    Friend WithEvents rbMultScalar As RadioButton
    Friend WithEvents rbAddScalar As RadioButton
    Friend WithEvents rbTransCholesky As RadioButton
    Friend WithEvents rbCholesky As RadioButton
    Friend WithEvents rbInverse As RadioButton
    Friend WithEvents rbTranspose As RadioButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnReplaceData As Button
    Friend WithEvents btnInsertData As Button
    Friend WithEvents rbProcess As RadioButton
    Friend WithEvents pbIconProcess As PictureBox
    Friend WithEvents Label63 As Label
    Friend WithEvents txtDataDescr As TextBox
    Friend WithEvents txtDataName As TextBox
    Friend WithEvents Label59 As Label
    Friend WithEvents rbMatrixProcess As RadioButton
    Friend WithEvents pbIconScalarProcess As PictureBox
    Friend WithEvents rbScalarProcess As RadioButton
    Friend WithEvents pbIconMatrixProcess As PictureBox
    Friend WithEvents pbIconMatrixPreDefScalar As PictureBox
    Friend WithEvents pbIconMatrixUserDefScalar As PictureBox
    Friend WithEvents pbIconMatrixOpen As PictureBox
    Friend WithEvents pbIconMatrixUserDef As PictureBox
    Friend WithEvents pbIconMatrix As PictureBox
    Friend WithEvents rbUserDefScalar As RadioButton
    Friend WithEvents rbScalar As RadioButton
    Friend WithEvents rbMatrix As RadioButton
    Friend WithEvents btnAppendData As Button
    Friend WithEvents rbUserDefMatrix As RadioButton
    Friend WithEvents rbOpenMatrixFile As RadioButton
    Friend WithEvents TabPage7 As TabPage
    Friend WithEvents btnCleanupNodeList As Button
    Friend WithEvents btnShowNodeList As Button
    Friend WithEvents btnCheckNodeCopyLists As Button
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents txtEditNodeDataType As TextBox
    Friend WithEvents Label82 As Label
    Friend WithEvents btnApplyChanges As Button
    Friend WithEvents txtEditNodeDescr As TextBox
    Friend WithEvents Label81 As Label
    Friend WithEvents Label80 As Label
    Friend WithEvents txtEditNodeText As TextBox
    Friend WithEvents txtEditNodeType As TextBox
    Friend WithEvents Label79 As Label
    Friend WithEvents txtEditNodeName As TextBox
    Friend WithEvents Label77 As Label
    Friend WithEvents btnMoveNodeDown As Button
    Friend WithEvents btnMoveNodeUp As Button
    Friend WithEvents btnCloneNode As Button
    Friend WithEvents btnPasteNode As Button
    Friend WithEvents btnCutNode As Button
    Friend WithEvents btnListChildNodes As Button
    Friend WithEvents btnDeleteNode As Button
    Friend WithEvents btnExit As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1_OpenNode As ToolStripMenuItem
End Class
