<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MlbScoreboard
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.AllGamesUpdateData = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ThisGameUpdateData = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfigureToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.QuitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.PlayRecapToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StandingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AllGamesUpdateData, Me.ThisGameUpdateData})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 665)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 16, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(1838, 24)
        Me.StatusStrip1.TabIndex = 0
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'AllGamesUpdateData
        '
        Me.AllGamesUpdateData.BackColor = System.Drawing.Color.White
        Me.AllGamesUpdateData.Name = "AllGamesUpdateData"
        Me.AllGamesUpdateData.Size = New System.Drawing.Size(0, 18)
        Me.AllGamesUpdateData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ThisGameUpdateData
        '
        Me.ThisGameUpdateData.BackColor = System.Drawing.Color.White
        Me.ThisGameUpdateData.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.ThisGameUpdateData.Name = "ThisGameUpdateData"
        Me.ThisGameUpdateData.Size = New System.Drawing.Size(4, 18)
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ViewToolStripMenuItem, Me.AboutToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(7, 3, 0, 3)
        Me.MenuStrip1.Size = New System.Drawing.Size(1838, 30)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfigureToolStripMenuItem, Me.QuitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(46, 24)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'ConfigureToolStripMenuItem
        '
        Me.ConfigureToolStripMenuItem.Name = "ConfigureToolStripMenuItem"
        Me.ConfigureToolStripMenuItem.Size = New System.Drawing.Size(157, 26)
        Me.ConfigureToolStripMenuItem.Text = "Configure"
        '
        'QuitToolStripMenuItem
        '
        Me.QuitToolStripMenuItem.Name = "QuitToolStripMenuItem"
        Me.QuitToolStripMenuItem.Size = New System.Drawing.Size(157, 26)
        Me.QuitToolStripMenuItem.Text = "Quit"
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator1, Me.PlayRecapToolStripMenuItem, Me.StandingsToolStripMenuItem})
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(55, 24)
        Me.ViewToolStripMenuItem.Text = "View"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(161, 6)
        '
        'PlayRecapToolStripMenuItem
        '
        Me.PlayRecapToolStripMenuItem.Name = "PlayRecapToolStripMenuItem"
        Me.PlayRecapToolStripMenuItem.Size = New System.Drawing.Size(164, 26)
        Me.PlayRecapToolStripMenuItem.Text = "Play Recap"
        '
        'StandingsToolStripMenuItem
        '
        Me.StandingsToolStripMenuItem.Name = "StandingsToolStripMenuItem"
        Me.StandingsToolStripMenuItem.Size = New System.Drawing.Size(164, 26)
        Me.StandingsToolStripMenuItem.Text = "Standings"
        '
        'AboutToolStripMenuItem1
        '
        Me.AboutToolStripMenuItem1.Name = "AboutToolStripMenuItem1"
        Me.AboutToolStripMenuItem1.Size = New System.Drawing.Size(64, 24)
        Me.AboutToolStripMenuItem1.Text = "About"
        '
        'calDatePicker
        '
        Me.calDatePicker.CustomFormat = """MM/dd/yyyy"""
        Me.calDatePicker.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.calDatePicker.Location = New System.Drawing.Point(12, 52)
        Me.calDatePicker.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.calDatePicker.Name = "calDatePicker"
        Me.calDatePicker.Size = New System.Drawing.Size(435, 27)
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
        Me.dgvInnings.CausesValidation = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvInnings.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvInnings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvInnings.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvInnings.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvInnings.Location = New System.Drawing.Point(548, 257)
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
        Me.dgvInnings.Size = New System.Drawing.Size(720, 120)
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
        Me.lblGameTitle.Location = New System.Drawing.Point(548, 59)
        Me.lblGameTitle.Name = "lblGameTitle"
        Me.lblGameTitle.Size = New System.Drawing.Size(249, 20)
        Me.lblGameTitle.TabIndex = 8
        Me.lblGameTitle.Text = "Away @ Home - mm/dd/yyyy" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'imgDiamond
        '
        Me.imgDiamond.ErrorImage = Global.MLBScoreBoard.My.Resources.Resources.diamond
        Me.imgDiamond.Image = Global.MLBScoreBoard.My.Resources.Resources.diamond
        Me.imgDiamond.ImageLocation = ""
        Me.imgDiamond.InitialImage = Global.MLBScoreBoard.My.Resources.Resources.diamond
        Me.imgDiamond.Location = New System.Drawing.Point(548, 448)
        Me.imgDiamond.Name = "imgDiamond"
        Me.imgDiamond.Size = New System.Drawing.Size(190, 190)
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
        Me.lblBalls.Location = New System.Drawing.Point(752, 450)
        Me.lblBalls.Name = "lblBalls"
        Me.lblBalls.Size = New System.Drawing.Size(74, 20)
        Me.lblBalls.TabIndex = 18
        Me.lblBalls.Text = "Balls: 0"
        '
        'lblStrikes
        '
        Me.lblStrikes.AutoSize = True
        Me.lblStrikes.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblStrikes.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblStrikes.Location = New System.Drawing.Point(752, 470)
        Me.lblStrikes.Name = "lblStrikes"
        Me.lblStrikes.Size = New System.Drawing.Size(90, 20)
        Me.lblStrikes.TabIndex = 19
        Me.lblStrikes.Text = "Strikes: 0"
        '
        'lblOuts
        '
        Me.lblOuts.AutoSize = True
        Me.lblOuts.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblOuts.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblOuts.Location = New System.Drawing.Point(752, 490)
        Me.lblOuts.Name = "lblOuts"
        Me.lblOuts.Size = New System.Drawing.Size(71, 20)
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
        Me.dgvAwayLineup.CausesValidation = False
        Me.dgvAwayLineup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvAwayLineup.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvAwayLineup.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvAwayLineup.Location = New System.Drawing.Point(1293, 112)
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
        Me.dgvAwayLineup.Size = New System.Drawing.Size(256, 526)
        Me.dgvAwayLineup.TabIndex = 21
        '
        'dgvHomeLineup
        '
        Me.dgvHomeLineup.AllowUserToAddRows = False
        Me.dgvHomeLineup.AllowUserToDeleteRows = False
        Me.dgvHomeLineup.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvHomeLineup.BackgroundColor = System.Drawing.Color.Green
        Me.dgvHomeLineup.CausesValidation = False
        Me.dgvHomeLineup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvHomeLineup.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgvHomeLineup.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvHomeLineup.Location = New System.Drawing.Point(1564, 112)
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
        Me.dgvHomeLineup.Size = New System.Drawing.Size(256, 526)
        Me.dgvHomeLineup.TabIndex = 22
        '
        'lblAwayLineup
        '
        Me.lblAwayLineup.AutoSize = True
        Me.lblAwayLineup.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblAwayLineup.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblAwayLineup.Location = New System.Drawing.Point(1293, 78)
        Me.lblAwayLineup.Name = "lblAwayLineup"
        Me.lblAwayLineup.Size = New System.Drawing.Size(99, 20)
        Me.lblAwayLineup.TabIndex = 23
        Me.lblAwayLineup.Text = "Away Lineup"
        '
        'lblHomeLineup
        '
        Me.lblHomeLineup.AutoSize = True
        Me.lblHomeLineup.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblHomeLineup.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblHomeLineup.Location = New System.Drawing.Point(1564, 78)
        Me.lblHomeLineup.Name = "lblHomeLineup"
        Me.lblHomeLineup.Size = New System.Drawing.Size(102, 20)
        Me.lblHomeLineup.TabIndex = 24
        Me.lblHomeLineup.Text = "Home Lineup"
        '
        'lblMatchup
        '
        Me.lblMatchup.AutoSize = True
        Me.lblMatchup.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblMatchup.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblMatchup.Location = New System.Drawing.Point(552, 392)
        Me.lblMatchup.Name = "lblMatchup"
        Me.lblMatchup.Size = New System.Drawing.Size(80, 20)
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
        Me.dgvGames.CausesValidation = False
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvGames.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvGames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvGames.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgvGames.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvGames.Location = New System.Drawing.Point(12, 112)
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
        Me.dgvGames.Size = New System.Drawing.Size(503, 526)
        Me.dgvGames.TabIndex = 27
        Me.dgvGames.VirtualMode = True
        '
        'lblGamePk
        '
        Me.lblGamePk.AutoSize = True
        Me.lblGamePk.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblGamePk.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblGamePk.Location = New System.Drawing.Point(723, 112)
        Me.lblGamePk.Name = "lblGamePk"
        Me.lblGamePk.Size = New System.Drawing.Size(83, 20)
        Me.lblGamePk.TabIndex = 28
        Me.lblGamePk.Text = "Game ID"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblStatus.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblStatus.Location = New System.Drawing.Point(724, 143)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(118, 20)
        Me.lblStatus.TabIndex = 29
        Me.lblStatus.Text = "Game Status"
        '
        'tbxCommentary
        '
        Me.tbxCommentary.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.tbxCommentary.Location = New System.Drawing.Point(855, 447)
        Me.tbxCommentary.Name = "tbxCommentary"
        Me.tbxCommentary.Size = New System.Drawing.Size(413, 191)
        Me.tbxCommentary.TabIndex = 30
        Me.tbxCommentary.Text = "Play Commentary"
        '
        'lblWeather
        '
        Me.lblWeather.AutoSize = True
        Me.lblWeather.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblWeather.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblWeather.Location = New System.Drawing.Point(727, 177)
        Me.lblWeather.Name = "lblWeather"
        Me.lblWeather.Size = New System.Drawing.Size(79, 20)
        Me.lblWeather.TabIndex = 31
        Me.lblWeather.Text = "Weather"
        '
        'imgAwayLogo
        '
        Me.imgAwayLogo.ErrorImage = Global.MLBScoreBoard.My.Resources.Resources.MLB
        Me.imgAwayLogo.Image = Global.MLBScoreBoard.My.Resources.Resources.MLB
        Me.imgAwayLogo.InitialImage = Global.MLBScoreBoard.My.Resources.Resources.MLB
        Me.imgAwayLogo.Location = New System.Drawing.Point(548, 112)
        Me.imgAwayLogo.Name = "imgAwayLogo"
        Me.imgAwayLogo.Size = New System.Drawing.Size(155, 96)
        Me.imgAwayLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgAwayLogo.TabIndex = 32
        Me.imgAwayLogo.TabStop = False
        '
        'imgHomeLogo
        '
        Me.imgHomeLogo.ErrorImage = Global.MLBScoreBoard.My.Resources.Resources.MLB
        Me.imgHomeLogo.Image = Global.MLBScoreBoard.My.Resources.Resources.MLB
        Me.imgHomeLogo.InitialImage = Global.MLBScoreBoard.My.Resources.Resources.MLB
        Me.imgHomeLogo.Location = New System.Drawing.Point(1113, 112)
        Me.imgHomeLogo.Name = "imgHomeLogo"
        Me.imgHomeLogo.Size = New System.Drawing.Size(155, 96)
        Me.imgHomeLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgHomeLogo.TabIndex = 33
        Me.imgHomeLogo.TabStop = False
        '
        'lblAwayWinnerLoser
        '
        Me.lblAwayWinnerLoser.AutoSize = True
        Me.lblAwayWinnerLoser.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblAwayWinnerLoser.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblAwayWinnerLoser.Location = New System.Drawing.Point(548, 220)
        Me.lblAwayWinnerLoser.Name = "lblAwayWinnerLoser"
        Me.lblAwayWinnerLoser.Size = New System.Drawing.Size(110, 18)
        Me.lblAwayWinnerLoser.TabIndex = 34
        Me.lblAwayWinnerLoser.Text = "Winner-Loser"
        Me.lblAwayWinnerLoser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblHomeWinnerLoser
        '
        Me.lblHomeWinnerLoser.AutoSize = True
        Me.lblHomeWinnerLoser.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblHomeWinnerLoser.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblHomeWinnerLoser.Location = New System.Drawing.Point(1113, 220)
        Me.lblHomeWinnerLoser.Name = "lblHomeWinnerLoser"
        Me.lblHomeWinnerLoser.Size = New System.Drawing.Size(110, 18)
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
        Me.lblPitchCount.Location = New System.Drawing.Point(552, 423)
        Me.lblPitchCount.Name = "lblPitchCount"
        Me.lblPitchCount.Size = New System.Drawing.Size(94, 20)
        Me.lblPitchCount.TabIndex = 36
        Me.lblPitchCount.Text = "Pitches: 0"
        '
        'MlbScoreboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Green
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(1838, 689)
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
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "MlbScoreboard"
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
End Class
