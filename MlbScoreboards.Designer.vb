<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MlbScoreboards
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.AllGamesUpdateData = New System.Windows.Forms.ToolStripStatusLabel()
        Me.AllGamesUpdateProgressBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.ThisGameUpdateData = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Spacer = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfigureToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.QuitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.PlayRecapToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StandingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BoxscoreToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.calDatePicker = New System.Windows.Forms.DateTimePicker()
        Me.dgvInnings = New System.Windows.Forms.DataGridView()
        Me.team = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblGameTitle = New System.Windows.Forms.Label()
        Me.imgDiamond = New System.Windows.Forms.PictureBox()
        Me.lblBalls = New System.Windows.Forms.Label()
        Me.lblStrikes = New System.Windows.Forms.Label()
        Me.lblOuts = New System.Windows.Forms.Label()
        Me.ScoreboardUpdateTimer = New System.Windows.Forms.Timer(Me.components)
        Me.dgvAwayLineup = New System.Windows.Forms.DataGridView()
        Me.dgvHomeLineup = New System.Windows.Forms.DataGridView()
        Me.lblAwayLineup = New System.Windows.Forms.Label()
        Me.lblHomeLineup = New System.Windows.Forms.Label()
        Me.lblMatchup = New System.Windows.Forms.Label()
        Me.dgvGames = New System.Windows.Forms.DataGridView()
        Me.lblGamePk = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.tbxCommentary = New System.Windows.Forms.RichTextBox()
        Me.lblWeather = New System.Windows.Forms.Label()
        Me.imgAwayLogo = New System.Windows.Forms.PictureBox()
        Me.imgHomeLogo = New System.Windows.Forms.PictureBox()
        Me.lblAwayWinnerLoser = New System.Windows.Forms.Label()
        Me.lblHomeWinnerLoser = New System.Windows.Forms.Label()
        Me.GameUpdateTimer = New System.Windows.Forms.Timer(Me.components)
        Me.lblPitchCount = New System.Windows.Forms.Label()
        Me.btnFindGames = New System.Windows.Forms.Button()
        Me.BroadcastsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.dgvInnings, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgDiamond, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvAwayLineup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvHomeLineup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvGames, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgAwayLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgHomeLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AllGamesUpdateData, Me.AllGamesUpdateProgressBar, Me.ThisGameUpdateData, Me.Spacer})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 623)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1723, 22)
        Me.StatusStrip1.TabIndex = 0
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'AllGamesUpdateData
        '
        Me.AllGamesUpdateData.BackColor = System.Drawing.Color.White
        Me.AllGamesUpdateData.Name = "AllGamesUpdateData"
        Me.AllGamesUpdateData.Size = New System.Drawing.Size(0, 17)
        Me.AllGamesUpdateData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'AllGamesUpdateProgressBar
        '
        Me.AllGamesUpdateProgressBar.BackColor = System.Drawing.SystemColors.Control
        Me.AllGamesUpdateProgressBar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.AllGamesUpdateProgressBar.Name = "AllGamesUpdateProgressBar"
        Me.AllGamesUpdateProgressBar.Size = New System.Drawing.Size(100, 16)
        Me.AllGamesUpdateProgressBar.Step = 1
        Me.AllGamesUpdateProgressBar.Visible = False
        '
        'ThisGameUpdateData
        '
        Me.ThisGameUpdateData.BackColor = System.Drawing.Color.White
        Me.ThisGameUpdateData.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.ThisGameUpdateData.Name = "ThisGameUpdateData"
        Me.ThisGameUpdateData.Size = New System.Drawing.Size(4, 17)
        Me.ThisGameUpdateData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Spacer
        '
        Me.Spacer.Name = "Spacer"
        Me.Spacer.Size = New System.Drawing.Size(0, 17)
        Me.Spacer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ViewToolStripMenuItem, Me.AboutToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1723, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfigureToolStripMenuItem, Me.QuitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'ConfigureToolStripMenuItem
        '
        Me.ConfigureToolStripMenuItem.Name = "ConfigureToolStripMenuItem"
        Me.ConfigureToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
        Me.ConfigureToolStripMenuItem.Text = "Configure"
        '
        'QuitToolStripMenuItem
        '
        Me.QuitToolStripMenuItem.Name = "QuitToolStripMenuItem"
        Me.QuitToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
        Me.QuitToolStripMenuItem.Text = "Quit"
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator1, Me.PlayRecapToolStripMenuItem, Me.StandingsToolStripMenuItem, Me.BoxscoreToolStripMenuItem, Me.BroadcastsToolStripMenuItem})
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.ViewToolStripMenuItem.Text = "View"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(128, 6)
        '
        'PlayRecapToolStripMenuItem
        '
        Me.PlayRecapToolStripMenuItem.Name = "PlayRecapToolStripMenuItem"
        Me.PlayRecapToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.PlayRecapToolStripMenuItem.Text = "Play Recap"
        '
        'StandingsToolStripMenuItem
        '
        Me.StandingsToolStripMenuItem.Name = "StandingsToolStripMenuItem"
        Me.StandingsToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.StandingsToolStripMenuItem.Text = "Standings"
        '
        'BoxscoreToolStripMenuItem
        '
        Me.BoxscoreToolStripMenuItem.Name = "BoxscoreToolStripMenuItem"
        Me.BoxscoreToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.BoxscoreToolStripMenuItem.Text = "Boxscore"
        '
        'AboutToolStripMenuItem1
        '
        Me.AboutToolStripMenuItem1.Name = "AboutToolStripMenuItem1"
        Me.AboutToolStripMenuItem1.Size = New System.Drawing.Size(52, 20)
        Me.AboutToolStripMenuItem1.Text = "About"
        '
        'calDatePicker
        '
        Me.calDatePicker.CustomFormat = """MM/dd/yyyy"""
        Me.calDatePicker.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.calDatePicker.Location = New System.Drawing.Point(24, 44)
        Me.calDatePicker.Name = "calDatePicker"
        Me.calDatePicker.Size = New System.Drawing.Size(252, 23)
        Me.calDatePicker.TabIndex = 4
        '
        'dgvInnings
        '
        Me.dgvInnings.AllowUserToAddRows = False
        Me.dgvInnings.AllowUserToDeleteRows = False
        Me.dgvInnings.AllowUserToResizeColumns = False
        Me.dgvInnings.AllowUserToResizeRows = False
        Me.dgvInnings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvInnings.BackgroundColor = System.Drawing.Color.Green
        Me.dgvInnings.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvInnings.CausesValidation = False
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvInnings.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.dgvInnings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvInnings.DefaultCellStyle = DataGridViewCellStyle8
        Me.dgvInnings.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvInnings.Location = New System.Drawing.Point(480, 193)
        Me.dgvInnings.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvInnings.MultiSelect = False
        Me.dgvInnings.Name = "dgvInnings"
        Me.dgvInnings.ReadOnly = True
        Me.dgvInnings.RowHeadersVisible = False
        Me.dgvInnings.RowHeadersWidth = 51
        Me.dgvInnings.RowTemplate.Height = 29
        Me.dgvInnings.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.dgvInnings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvInnings.ShowCellErrors = False
        Me.dgvInnings.ShowCellToolTips = False
        Me.dgvInnings.ShowEditingIcon = False
        Me.dgvInnings.ShowRowErrors = False
        Me.dgvInnings.Size = New System.Drawing.Size(630, 99)
        Me.dgvInnings.TabIndex = 6
        Me.dgvInnings.VirtualMode = True
        '
        'team
        '
        Me.team.HeaderText = "Inning"
        Me.team.MinimumWidth = 6
        Me.team.Name = "team"
        Me.team.Width = 125
        '
        'lblGameTitle
        '
        Me.lblGameTitle.AutoSize = True
        Me.lblGameTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblGameTitle.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblGameTitle.Location = New System.Drawing.Point(480, 44)
        Me.lblGameTitle.Name = "lblGameTitle"
        Me.lblGameTitle.Size = New System.Drawing.Size(211, 17)
        Me.lblGameTitle.TabIndex = 8
        Me.lblGameTitle.Text = "Away @ Home - mm/dd/yyyy" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'imgDiamond
        '
        Me.imgDiamond.ErrorImage = Global.MLBScoreBoard.My.Resources.Resources.diamond
        Me.imgDiamond.Image = Global.MLBScoreBoard.My.Resources.Resources.diamond
        Me.imgDiamond.ImageLocation = ""
        Me.imgDiamond.InitialImage = Global.MLBScoreBoard.My.Resources.Resources.diamond
        Me.imgDiamond.Location = New System.Drawing.Point(477, 350)
        Me.imgDiamond.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.imgDiamond.Name = "imgDiamond"
        Me.imgDiamond.Size = New System.Drawing.Size(166, 142)
        Me.imgDiamond.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgDiamond.TabIndex = 11
        Me.imgDiamond.TabStop = False
        Me.imgDiamond.Tag = "Diamond"
        Me.imgDiamond.WaitOnLoad = True
        '
        'lblBalls
        '
        Me.lblBalls.AutoSize = True
        Me.lblBalls.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblBalls.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblBalls.Location = New System.Drawing.Point(488, 501)
        Me.lblBalls.Name = "lblBalls"
        Me.lblBalls.Size = New System.Drawing.Size(62, 17)
        Me.lblBalls.TabIndex = 18
        Me.lblBalls.Text = "Balls: 0"
        '
        'lblStrikes
        '
        Me.lblStrikes.AutoSize = True
        Me.lblStrikes.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblStrikes.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblStrikes.Location = New System.Drawing.Point(488, 515)
        Me.lblStrikes.Name = "lblStrikes"
        Me.lblStrikes.Size = New System.Drawing.Size(77, 17)
        Me.lblStrikes.TabIndex = 19
        Me.lblStrikes.Text = "Strikes: 0"
        '
        'lblOuts
        '
        Me.lblOuts.AutoSize = True
        Me.lblOuts.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblOuts.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblOuts.Location = New System.Drawing.Point(488, 531)
        Me.lblOuts.Name = "lblOuts"
        Me.lblOuts.Size = New System.Drawing.Size(61, 17)
        Me.lblOuts.TabIndex = 20
        Me.lblOuts.Text = "Outs: 0"
        '
        'ScoreboardUpdateTimer
        '
        Me.ScoreboardUpdateTimer.Enabled = True
        Me.ScoreboardUpdateTimer.Interval = 30000
        '
        'dgvAwayLineup
        '
        Me.dgvAwayLineup.AllowUserToAddRows = False
        Me.dgvAwayLineup.AllowUserToDeleteRows = False
        Me.dgvAwayLineup.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvAwayLineup.BackgroundColor = System.Drawing.Color.Green
        Me.dgvAwayLineup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvAwayLineup.CausesValidation = False
        Me.dgvAwayLineup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvAwayLineup.DefaultCellStyle = DataGridViewCellStyle9
        Me.dgvAwayLineup.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvAwayLineup.Location = New System.Drawing.Point(1131, 84)
        Me.dgvAwayLineup.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvAwayLineup.MultiSelect = False
        Me.dgvAwayLineup.Name = "dgvAwayLineup"
        Me.dgvAwayLineup.ReadOnly = True
        Me.dgvAwayLineup.RowHeadersVisible = False
        Me.dgvAwayLineup.RowHeadersWidth = 51
        Me.dgvAwayLineup.RowTemplate.Height = 29
        Me.dgvAwayLineup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvAwayLineup.ShowCellErrors = False
        Me.dgvAwayLineup.ShowCellToolTips = False
        Me.dgvAwayLineup.ShowEditingIcon = False
        Me.dgvAwayLineup.ShowRowErrors = False
        Me.dgvAwayLineup.Size = New System.Drawing.Size(270, 522)
        Me.dgvAwayLineup.TabIndex = 21
        '
        'dgvHomeLineup
        '
        Me.dgvHomeLineup.AllowUserToAddRows = False
        Me.dgvHomeLineup.AllowUserToDeleteRows = False
        Me.dgvHomeLineup.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvHomeLineup.BackgroundColor = System.Drawing.Color.Green
        Me.dgvHomeLineup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvHomeLineup.CausesValidation = False
        Me.dgvHomeLineup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvHomeLineup.DefaultCellStyle = DataGridViewCellStyle10
        Me.dgvHomeLineup.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvHomeLineup.Location = New System.Drawing.Point(1420, 84)
        Me.dgvHomeLineup.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvHomeLineup.MultiSelect = False
        Me.dgvHomeLineup.Name = "dgvHomeLineup"
        Me.dgvHomeLineup.ReadOnly = True
        Me.dgvHomeLineup.RowHeadersVisible = False
        Me.dgvHomeLineup.RowHeadersWidth = 51
        Me.dgvHomeLineup.RowTemplate.Height = 29
        Me.dgvHomeLineup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvHomeLineup.ShowCellErrors = False
        Me.dgvHomeLineup.ShowCellToolTips = False
        Me.dgvHomeLineup.ShowEditingIcon = False
        Me.dgvHomeLineup.ShowRowErrors = False
        Me.dgvHomeLineup.Size = New System.Drawing.Size(270, 522)
        Me.dgvHomeLineup.TabIndex = 22
        '
        'lblAwayLineup
        '
        Me.lblAwayLineup.AutoSize = True
        Me.lblAwayLineup.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblAwayLineup.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblAwayLineup.Location = New System.Drawing.Point(1131, 58)
        Me.lblAwayLineup.Name = "lblAwayLineup"
        Me.lblAwayLineup.Size = New System.Drawing.Size(77, 15)
        Me.lblAwayLineup.TabIndex = 23
        Me.lblAwayLineup.Text = "Away Lineup"
        '
        'lblHomeLineup
        '
        Me.lblHomeLineup.AutoSize = True
        Me.lblHomeLineup.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblHomeLineup.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblHomeLineup.Location = New System.Drawing.Point(1420, 58)
        Me.lblHomeLineup.Name = "lblHomeLineup"
        Me.lblHomeLineup.Size = New System.Drawing.Size(81, 15)
        Me.lblHomeLineup.TabIndex = 24
        Me.lblHomeLineup.Text = "Home Lineup"
        '
        'lblMatchup
        '
        Me.lblMatchup.AutoSize = True
        Me.lblMatchup.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblMatchup.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblMatchup.Location = New System.Drawing.Point(480, 308)
        Me.lblMatchup.Name = "lblMatchup"
        Me.lblMatchup.Size = New System.Drawing.Size(69, 17)
        Me.lblMatchup.TabIndex = 25
        Me.lblMatchup.Text = "Matchup"
        '
        'dgvGames
        '
        Me.dgvGames.AllowUserToAddRows = False
        Me.dgvGames.AllowUserToDeleteRows = False
        Me.dgvGames.AllowUserToResizeColumns = False
        Me.dgvGames.AllowUserToResizeRows = False
        Me.dgvGames.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvGames.BackgroundColor = System.Drawing.Color.Green
        Me.dgvGames.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvGames.CausesValidation = False
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvGames.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.dgvGames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvGames.DefaultCellStyle = DataGridViewCellStyle12
        Me.dgvGames.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvGames.Location = New System.Drawing.Point(10, 84)
        Me.dgvGames.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvGames.MultiSelect = False
        Me.dgvGames.Name = "dgvGames"
        Me.dgvGames.ReadOnly = True
        Me.dgvGames.RowHeadersVisible = False
        Me.dgvGames.RowHeadersWidth = 51
        Me.dgvGames.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvGames.RowTemplate.Height = 29
        Me.dgvGames.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvGames.ShowCellErrors = False
        Me.dgvGames.ShowCellToolTips = False
        Me.dgvGames.ShowEditingIcon = False
        Me.dgvGames.ShowRowErrors = False
        Me.dgvGames.Size = New System.Drawing.Size(440, 522)
        Me.dgvGames.TabIndex = 27
        Me.dgvGames.VirtualMode = True
        '
        'lblGamePk
        '
        Me.lblGamePk.AutoSize = True
        Me.lblGamePk.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblGamePk.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblGamePk.Location = New System.Drawing.Point(633, 84)
        Me.lblGamePk.Name = "lblGamePk"
        Me.lblGamePk.Size = New System.Drawing.Size(70, 17)
        Me.lblGamePk.TabIndex = 28
        Me.lblGamePk.Text = "Game ID"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblStatus.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblStatus.Location = New System.Drawing.Point(633, 110)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(101, 17)
        Me.lblStatus.TabIndex = 29
        Me.lblStatus.Text = "Game Status"
        '
        'tbxCommentary
        '
        Me.tbxCommentary.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tbxCommentary.Location = New System.Drawing.Point(666, 349)
        Me.tbxCommentary.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tbxCommentary.Name = "tbxCommentary"
        Me.tbxCommentary.Size = New System.Drawing.Size(441, 257)
        Me.tbxCommentary.TabIndex = 30
        Me.tbxCommentary.Text = "Play Commentary"
        '
        'lblWeather
        '
        Me.lblWeather.AutoSize = True
        Me.lblWeather.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblWeather.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblWeather.Location = New System.Drawing.Point(634, 136)
        Me.lblWeather.Name = "lblWeather"
        Me.lblWeather.Size = New System.Drawing.Size(69, 17)
        Me.lblWeather.TabIndex = 31
        Me.lblWeather.Text = "Weather"
        '
        'imgAwayLogo
        '
        Me.imgAwayLogo.ErrorImage = Global.MLBScoreBoard.My.Resources.Resources.MLB
        Me.imgAwayLogo.Image = Global.MLBScoreBoard.My.Resources.Resources.MLB
        Me.imgAwayLogo.InitialImage = Global.MLBScoreBoard.My.Resources.Resources.MLB
        Me.imgAwayLogo.Location = New System.Drawing.Point(480, 84)
        Me.imgAwayLogo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.imgAwayLogo.Name = "imgAwayLogo"
        Me.imgAwayLogo.Size = New System.Drawing.Size(136, 72)
        Me.imgAwayLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgAwayLogo.TabIndex = 32
        Me.imgAwayLogo.TabStop = False
        '
        'imgHomeLogo
        '
        Me.imgHomeLogo.ErrorImage = Global.MLBScoreBoard.My.Resources.Resources.MLB
        Me.imgHomeLogo.Image = Global.MLBScoreBoard.My.Resources.Resources.MLB
        Me.imgHomeLogo.InitialImage = Global.MLBScoreBoard.My.Resources.Resources.MLB
        Me.imgHomeLogo.Location = New System.Drawing.Point(974, 84)
        Me.imgHomeLogo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.imgHomeLogo.Name = "imgHomeLogo"
        Me.imgHomeLogo.Size = New System.Drawing.Size(136, 72)
        Me.imgHomeLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgHomeLogo.TabIndex = 33
        Me.imgHomeLogo.TabStop = False
        '
        'lblAwayWinnerLoser
        '
        Me.lblAwayWinnerLoser.AutoSize = True
        Me.lblAwayWinnerLoser.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblAwayWinnerLoser.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblAwayWinnerLoser.Location = New System.Drawing.Point(480, 165)
        Me.lblAwayWinnerLoser.Name = "lblAwayWinnerLoser"
        Me.lblAwayWinnerLoser.Size = New System.Drawing.Size(93, 15)
        Me.lblAwayWinnerLoser.TabIndex = 34
        Me.lblAwayWinnerLoser.Text = "Winner-Loser"
        Me.lblAwayWinnerLoser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblHomeWinnerLoser
        '
        Me.lblHomeWinnerLoser.AutoSize = True
        Me.lblHomeWinnerLoser.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblHomeWinnerLoser.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblHomeWinnerLoser.Location = New System.Drawing.Point(974, 165)
        Me.lblHomeWinnerLoser.Name = "lblHomeWinnerLoser"
        Me.lblHomeWinnerLoser.Size = New System.Drawing.Size(93, 15)
        Me.lblHomeWinnerLoser.TabIndex = 35
        Me.lblHomeWinnerLoser.Text = "Winner-Loser"
        Me.lblHomeWinnerLoser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GameUpdateTimer
        '
        Me.GameUpdateTimer.Enabled = True
        Me.GameUpdateTimer.Interval = 20000
        '
        'lblPitchCount
        '
        Me.lblPitchCount.AutoSize = True
        Me.lblPitchCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblPitchCount.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblPitchCount.Location = New System.Drawing.Point(488, 548)
        Me.lblPitchCount.Name = "lblPitchCount"
        Me.lblPitchCount.Size = New System.Drawing.Size(80, 17)
        Me.lblPitchCount.TabIndex = 36
        Me.lblPitchCount.Text = "Pitches: 0"
        '
        'btnFindGames
        '
        Me.btnFindGames.BackColor = System.Drawing.SystemColors.Control
        Me.btnFindGames.Location = New System.Drawing.Point(295, 44)
        Me.btnFindGames.Name = "btnFindGames"
        Me.btnFindGames.Size = New System.Drawing.Size(122, 23)
        Me.btnFindGames.TabIndex = 37
        Me.btnFindGames.Text = "FindGames"
        Me.btnFindGames.UseVisualStyleBackColor = False
        '
        'BroadcastsToolStripMenuItem
        '
        Me.BroadcastsToolStripMenuItem.Name = "BroadcastsToolStripMenuItem"
        Me.BroadcastsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.BroadcastsToolStripMenuItem.Text = "Broadcasts"
        '
        'MlbScoreboards
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Green
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(1723, 645)
        Me.Controls.Add(Me.btnFindGames)
        Me.Controls.Add(Me.lblPitchCount)
        Me.Controls.Add(Me.lblHomeWinnerLoser)
        Me.Controls.Add(Me.lblAwayWinnerLoser)
        Me.Controls.Add(Me.imgHomeLogo)
        Me.Controls.Add(Me.imgAwayLogo)
        Me.Controls.Add(Me.lblWeather)
        Me.Controls.Add(Me.tbxCommentary)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.lblGamePk)
        Me.Controls.Add(Me.dgvGames)
        Me.Controls.Add(Me.lblMatchup)
        Me.Controls.Add(Me.lblHomeLineup)
        Me.Controls.Add(Me.lblAwayLineup)
        Me.Controls.Add(Me.dgvHomeLineup)
        Me.Controls.Add(Me.dgvAwayLineup)
        Me.Controls.Add(Me.lblOuts)
        Me.Controls.Add(Me.lblStrikes)
        Me.Controls.Add(Me.lblBalls)
        Me.Controls.Add(Me.imgDiamond)
        Me.Controls.Add(Me.lblGameTitle)
        Me.Controls.Add(Me.dgvInnings)
        Me.Controls.Add(Me.calDatePicker)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MlbScoreboards"
        Me.Text = "MLB Live Scoreboards (C) 2022 MSRoth"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.dgvInnings, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgDiamond, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvAwayLineup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvHomeLineup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvGames, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgAwayLogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgHomeLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents calDatePicker As DateTimePicker
    Friend WithEvents dgvInnings As DataGridView
    Friend WithEvents team As DataGridViewTextBoxColumn
    Friend WithEvents lblGameTitle As Label
    Friend WithEvents imgDiamond As PictureBox
    Friend WithEvents lblBalls As Label
    Friend WithEvents lblStrikes As Label
    Friend WithEvents lblOuts As Label
    Friend WithEvents AllGamesUpdateData As ToolStripStatusLabel
    Friend WithEvents dgvAwayLineup As DataGridView
    Friend WithEvents dgvHomeLineup As DataGridView
    Friend WithEvents lblAwayLineup As Label
    Friend WithEvents lblHomeLineup As Label
    Friend WithEvents lblMatchup As Label
    Friend WithEvents dgvGames As DataGridView
    Friend WithEvents ScoreboardUpdateTimer As Timer
    Friend WithEvents lblGamePk As Label
    Friend WithEvents lblStatus As Label
    Friend WithEvents tbxCommentary As RichTextBox
    Friend WithEvents QuitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ConfigureToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents lblWeather As Label
    Friend WithEvents ViewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents imgAwayLogo As PictureBox
    Friend WithEvents imgHomeLogo As PictureBox
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents lblAwayWinnerLoser As Label
    Friend WithEvents lblHomeWinnerLoser As Label
    Friend WithEvents PlayRecapToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GameUpdateTimer As Timer
    Friend WithEvents StandingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents lblPitchCount As Label
    Friend WithEvents ThisGameUpdateData As ToolStripStatusLabel
    Friend WithEvents BoxscoreToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents btnFindGames As Button
    Friend WithEvents AllGamesUpdateProgressBar As ToolStripProgressBar
    Friend WithEvents Spacer As ToolStripStatusLabel
    Friend WithEvents BroadcastsToolStripMenuItem As ToolStripMenuItem
End Class
