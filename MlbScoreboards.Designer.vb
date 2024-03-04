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
        components = New ComponentModel.Container()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As DataGridViewCellStyle = New DataGridViewCellStyle()
        StatusStrip1 = New StatusStrip()
        AllGamesUpdateData = New ToolStripStatusLabel()
        AllGamesUpdateProgressBar = New ToolStripProgressBar()
        ThisGameUpdateData = New ToolStripStatusLabel()
        Spacer = New ToolStripStatusLabel()
        MenuStrip1 = New MenuStrip()
        FileToolStripMenuItem = New ToolStripMenuItem()
        ConfigureToolStripMenuItem = New ToolStripMenuItem()
        QuitToolStripMenuItem = New ToolStripMenuItem()
        ViewToolStripMenuItem = New ToolStripMenuItem()
        ToolStripSeparator1 = New ToolStripSeparator()
        StandingsToolStripMenuItem = New ToolStripMenuItem()
        BoxscoreToolStripMenuItem = New ToolStripMenuItem()
        AboutToolStripMenuItem1 = New ToolStripMenuItem()
        calDatePicker = New DateTimePicker()
        dgvInnings = New DataGridView()
        team = New DataGridViewTextBoxColumn()
        lblGameTitle = New Label()
        imgDiamond = New PictureBox()
        lblBalls = New Label()
        lblStrikes = New Label()
        lblOuts = New Label()
        ScoreboardUpdateTimer = New Timer(components)
        dgvAwayLineup = New DataGridView()
        dgvHomeLineup = New DataGridView()
        lblAwayLineup = New Label()
        lblHomeLineup = New Label()
        lblMatchupPitcher = New Label()
        dgvGames = New DataGridView()
        lblGamePk = New Label()
        lblStatus = New Label()
        tbxCommentary = New RichTextBox()
        lblWeather = New Label()
        imgAwayLogo = New PictureBox()
        imgHomeLogo = New PictureBox()
        lblAwayWinnerLoser = New Label()
        lblHomeWinnerLoser = New Label()
        GameUpdateTimer = New Timer(components)
        lblPitchCount = New Label()
        lblMatchupBatter = New Label()
        dgvBroadcasters = New DataGridView()
        dgvPlaySummary = New DataGridView()
        lblCommentary = New Label()
        lblPlaySummary = New Label()
        lblBroadcasters = New Label()
        StatusStrip1.SuspendLayout()
        MenuStrip1.SuspendLayout()
        CType(dgvInnings, ComponentModel.ISupportInitialize).BeginInit()
        CType(imgDiamond, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvAwayLineup, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvHomeLineup, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvGames, ComponentModel.ISupportInitialize).BeginInit()
        CType(imgAwayLogo, ComponentModel.ISupportInitialize).BeginInit()
        CType(imgHomeLogo, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvBroadcasters, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvPlaySummary, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.ImageScalingSize = New Size(20, 20)
        StatusStrip1.Items.AddRange(New ToolStripItem() {AllGamesUpdateData, AllGamesUpdateProgressBar, ThisGameUpdateData, Spacer})
        StatusStrip1.Location = New Point(0, 1035)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Padding = New Padding(1, 0, 16, 0)
        StatusStrip1.Size = New Size(1924, 22)
        StatusStrip1.TabIndex = 0
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' AllGamesUpdateData
        ' 
        AllGamesUpdateData.BackColor = Color.White
        AllGamesUpdateData.Name = "AllGamesUpdateData"
        AllGamesUpdateData.Size = New Size(0, 17)
        AllGamesUpdateData.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' AllGamesUpdateProgressBar
        ' 
        AllGamesUpdateProgressBar.BackColor = SystemColors.Control
        AllGamesUpdateProgressBar.ForeColor = Color.FromArgb(CByte(0), CByte(192), CByte(0))
        AllGamesUpdateProgressBar.Name = "AllGamesUpdateProgressBar"
        AllGamesUpdateProgressBar.Size = New Size(114, 20)
        AllGamesUpdateProgressBar.Step = 1
        AllGamesUpdateProgressBar.Visible = False
        ' 
        ' ThisGameUpdateData
        ' 
        ThisGameUpdateData.BackColor = Color.White
        ThisGameUpdateData.BorderSides = ToolStripStatusLabelBorderSides.Left
        ThisGameUpdateData.Name = "ThisGameUpdateData"
        ThisGameUpdateData.Size = New Size(4, 17)
        ThisGameUpdateData.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Spacer
        ' 
        Spacer.Name = "Spacer"
        Spacer.Size = New Size(0, 17)
        Spacer.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.ImageScalingSize = New Size(20, 20)
        MenuStrip1.Items.AddRange(New ToolStripItem() {FileToolStripMenuItem, ViewToolStripMenuItem, AboutToolStripMenuItem1})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Padding = New Padding(7, 3, 0, 3)
        MenuStrip1.Size = New Size(1924, 29)
        MenuStrip1.TabIndex = 2
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' FileToolStripMenuItem
        ' 
        FileToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ConfigureToolStripMenuItem, QuitToolStripMenuItem})
        FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        FileToolStripMenuItem.Size = New Size(41, 23)
        FileToolStripMenuItem.Text = "File"
        ' 
        ' ConfigureToolStripMenuItem
        ' 
        ConfigureToolStripMenuItem.Name = "ConfigureToolStripMenuItem"
        ConfigureToolStripMenuItem.Size = New Size(138, 24)
        ConfigureToolStripMenuItem.Text = "Configure"
        ' 
        ' QuitToolStripMenuItem
        ' 
        QuitToolStripMenuItem.Name = "QuitToolStripMenuItem"
        QuitToolStripMenuItem.Size = New Size(138, 24)
        QuitToolStripMenuItem.Text = "Quit"
        ' 
        ' ViewToolStripMenuItem
        ' 
        ViewToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ToolStripSeparator1, StandingsToolStripMenuItem, BoxscoreToolStripMenuItem})
        ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        ViewToolStripMenuItem.Size = New Size(50, 23)
        ViewToolStripMenuItem.Text = "View"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(135, 6)
        ' 
        ' StandingsToolStripMenuItem
        ' 
        StandingsToolStripMenuItem.Name = "StandingsToolStripMenuItem"
        StandingsToolStripMenuItem.Size = New Size(138, 24)
        StandingsToolStripMenuItem.Text = "Standings"
        ' 
        ' BoxscoreToolStripMenuItem
        ' 
        BoxscoreToolStripMenuItem.Name = "BoxscoreToolStripMenuItem"
        BoxscoreToolStripMenuItem.Size = New Size(138, 24)
        BoxscoreToolStripMenuItem.Text = "Boxscore"
        ' 
        ' AboutToolStripMenuItem1
        ' 
        AboutToolStripMenuItem1.Name = "AboutToolStripMenuItem1"
        AboutToolStripMenuItem1.Size = New Size(59, 23)
        AboutToolStripMenuItem1.Text = "About"
        ' 
        ' calDatePicker
        ' 
        calDatePicker.CustomFormat = """MM/dd/yyyy"""
        calDatePicker.Font = New Font("Microsoft Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point)
        calDatePicker.Location = New Point(12, 56)
        calDatePicker.Margin = New Padding(3, 4, 3, 4)
        calDatePicker.Name = "calDatePicker"
        calDatePicker.Size = New Size(276, 23)
        calDatePicker.TabIndex = 4
        ' 
        ' dgvInnings
        ' 
        dgvInnings.AllowUserToAddRows = False
        dgvInnings.AllowUserToDeleteRows = False
        dgvInnings.AllowUserToResizeColumns = False
        dgvInnings.AllowUserToResizeRows = False
        dgvInnings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvInnings.BackgroundColor = Color.Green
        dgvInnings.CausesValidation = False
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = SystemColors.Control
        DataGridViewCellStyle1.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point)
        DataGridViewCellStyle1.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        dgvInnings.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        dgvInnings.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = SystemColors.Window
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point)
        DataGridViewCellStyle2.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = SystemColors.Window
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False
        dgvInnings.DefaultCellStyle = DataGridViewCellStyle2
        dgvInnings.EditMode = DataGridViewEditMode.EditProgrammatically
        dgvInnings.Location = New Point(509, 244)
        dgvInnings.MultiSelect = False
        dgvInnings.Name = "dgvInnings"
        dgvInnings.ReadOnly = True
        dgvInnings.RowHeadersVisible = False
        dgvInnings.RowHeadersWidth = 51
        dgvInnings.RowTemplate.Height = 29
        dgvInnings.ScrollBars = ScrollBars.Horizontal
        dgvInnings.SelectionMode = DataGridViewSelectionMode.CellSelect
        dgvInnings.ShowCellErrors = False
        dgvInnings.ShowCellToolTips = False
        dgvInnings.ShowEditingIcon = False
        dgvInnings.ShowRowErrors = False
        dgvInnings.Size = New Size(720, 125)
        dgvInnings.TabIndex = 6
        dgvInnings.VirtualMode = True
        ' 
        ' team
        ' 
        team.HeaderText = "Inning"
        team.MinimumWidth = 6
        team.Name = "team"
        team.Width = 125
        ' 
        ' lblGameTitle
        ' 
        lblGameTitle.AutoSize = True
        lblGameTitle.Font = New Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point)
        lblGameTitle.ForeColor = SystemColors.ControlLightLight
        lblGameTitle.Location = New Point(509, 56)
        lblGameTitle.Name = "lblGameTitle"
        lblGameTitle.Size = New Size(231, 20)
        lblGameTitle.TabIndex = 8
        lblGameTitle.Text = "Away @ Home - mm/dd/yyyy" & vbCrLf
        ' 
        ' imgDiamond
        ' 
        imgDiamond.ErrorImage = My.Resources.Resources.diamond
        imgDiamond.Image = My.Resources.Resources.diamond
        imgDiamond.ImageLocation = ""
        imgDiamond.InitialImage = My.Resources.Resources.diamond
        imgDiamond.Location = New Point(509, 396)
        imgDiamond.Name = "imgDiamond"
        imgDiamond.Size = New Size(190, 180)
        imgDiamond.SizeMode = PictureBoxSizeMode.StretchImage
        imgDiamond.TabIndex = 11
        imgDiamond.TabStop = False
        imgDiamond.Tag = "Diamond"
        imgDiamond.WaitOnLoad = True
        ' 
        ' lblBalls
        ' 
        lblBalls.AutoSize = True
        lblBalls.Font = New Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point)
        lblBalls.ForeColor = SystemColors.ControlLightLight
        lblBalls.Location = New Point(725, 492)
        lblBalls.Name = "lblBalls"
        lblBalls.Size = New Size(62, 17)
        lblBalls.TabIndex = 18
        lblBalls.Text = "Balls: 0"
        ' 
        ' lblStrikes
        ' 
        lblStrikes.AutoSize = True
        lblStrikes.Font = New Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point)
        lblStrikes.ForeColor = SystemColors.ControlLightLight
        lblStrikes.Location = New Point(725, 514)
        lblStrikes.Name = "lblStrikes"
        lblStrikes.Size = New Size(77, 17)
        lblStrikes.TabIndex = 19
        lblStrikes.Text = "Strikes: 0"
        ' 
        ' lblOuts
        ' 
        lblOuts.AutoSize = True
        lblOuts.Font = New Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point)
        lblOuts.ForeColor = SystemColors.ControlLightLight
        lblOuts.Location = New Point(725, 535)
        lblOuts.Name = "lblOuts"
        lblOuts.Size = New Size(61, 17)
        lblOuts.TabIndex = 20
        lblOuts.Text = "Outs: 0"
        ' 
        ' ScoreboardUpdateTimer
        ' 
        ScoreboardUpdateTimer.Enabled = True
        ScoreboardUpdateTimer.Interval = 30000
        ' 
        ' dgvAwayLineup
        ' 
        dgvAwayLineup.AllowUserToAddRows = False
        dgvAwayLineup.AllowUserToDeleteRows = False
        dgvAwayLineup.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvAwayLineup.BackgroundColor = Color.Green
        dgvAwayLineup.CausesValidation = False
        dgvAwayLineup.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = SystemColors.Window
        DataGridViewCellStyle3.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point)
        DataGridViewCellStyle3.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = SystemColors.Window
        DataGridViewCellStyle3.SelectionForeColor = SystemColors.ControlText
        DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False
        dgvAwayLineup.DefaultCellStyle = DataGridViewCellStyle3
        dgvAwayLineup.EditMode = DataGridViewEditMode.EditProgrammatically
        dgvAwayLineup.Location = New Point(1304, 106)
        dgvAwayLineup.MultiSelect = False
        dgvAwayLineup.Name = "dgvAwayLineup"
        dgvAwayLineup.ReadOnly = True
        dgvAwayLineup.RowHeadersVisible = False
        dgvAwayLineup.RowHeadersWidth = 51
        dgvAwayLineup.RowTemplate.Height = 29
        dgvAwayLineup.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvAwayLineup.ShowCellErrors = False
        dgvAwayLineup.ShowCellToolTips = False
        dgvAwayLineup.ShowEditingIcon = False
        dgvAwayLineup.ShowRowErrors = False
        dgvAwayLineup.Size = New Size(290, 661)
        dgvAwayLineup.TabIndex = 21
        ' 
        ' dgvHomeLineup
        ' 
        dgvHomeLineup.AllowUserToAddRows = False
        dgvHomeLineup.AllowUserToDeleteRows = False
        dgvHomeLineup.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvHomeLineup.BackgroundColor = Color.Green
        dgvHomeLineup.CausesValidation = False
        dgvHomeLineup.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = SystemColors.Window
        DataGridViewCellStyle4.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point)
        DataGridViewCellStyle4.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = SystemColors.Window
        DataGridViewCellStyle4.SelectionForeColor = SystemColors.ControlText
        DataGridViewCellStyle4.WrapMode = DataGridViewTriState.False
        dgvHomeLineup.DefaultCellStyle = DataGridViewCellStyle4
        dgvHomeLineup.EditMode = DataGridViewEditMode.EditProgrammatically
        dgvHomeLineup.Location = New Point(1618, 106)
        dgvHomeLineup.MultiSelect = False
        dgvHomeLineup.Name = "dgvHomeLineup"
        dgvHomeLineup.ReadOnly = True
        dgvHomeLineup.RowHeadersVisible = False
        dgvHomeLineup.RowHeadersWidth = 51
        dgvHomeLineup.RowTemplate.Height = 29
        dgvHomeLineup.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvHomeLineup.ShowCellErrors = False
        dgvHomeLineup.ShowCellToolTips = False
        dgvHomeLineup.ShowEditingIcon = False
        dgvHomeLineup.ShowRowErrors = False
        dgvHomeLineup.Size = New Size(290, 661)
        dgvHomeLineup.TabIndex = 22
        ' 
        ' lblAwayLineup
        ' 
        lblAwayLineup.AutoSize = True
        lblAwayLineup.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        lblAwayLineup.ForeColor = SystemColors.ControlLightLight
        lblAwayLineup.Location = New Point(1304, 73)
        lblAwayLineup.Name = "lblAwayLineup"
        lblAwayLineup.Size = New Size(94, 16)
        lblAwayLineup.TabIndex = 23
        lblAwayLineup.Text = "Away Lineup"
        ' 
        ' lblHomeLineup
        ' 
        lblHomeLineup.AutoSize = True
        lblHomeLineup.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        lblHomeLineup.ForeColor = SystemColors.ControlLightLight
        lblHomeLineup.Location = New Point(1618, 73)
        lblHomeLineup.Name = "lblHomeLineup"
        lblHomeLineup.Size = New Size(98, 16)
        lblHomeLineup.TabIndex = 24
        lblHomeLineup.Text = "Home Lineup"
        ' 
        ' lblMatchupPitcher
        ' 
        lblMatchupPitcher.AutoSize = True
        lblMatchupPitcher.Font = New Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point)
        lblMatchupPitcher.ForeColor = SystemColors.ControlLightLight
        lblMatchupPitcher.Location = New Point(725, 396)
        lblMatchupPitcher.Name = "lblMatchupPitcher"
        lblMatchupPitcher.Size = New Size(126, 17)
        lblMatchupPitcher.TabIndex = 25
        lblMatchupPitcher.Text = "Matchup-Pitcher"
        ' 
        ' dgvGames
        ' 
        dgvGames.AllowUserToAddRows = False
        dgvGames.AllowUserToDeleteRows = False
        dgvGames.AllowUserToResizeColumns = False
        dgvGames.AllowUserToResizeRows = False
        dgvGames.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvGames.BackgroundColor = Color.Green
        dgvGames.CausesValidation = False
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = SystemColors.Control
        DataGridViewCellStyle5.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point)
        DataGridViewCellStyle5.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = DataGridViewTriState.False
        dgvGames.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        dgvGames.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = SystemColors.Window
        DataGridViewCellStyle6.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point)
        DataGridViewCellStyle6.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = SystemColors.Window
        DataGridViewCellStyle6.SelectionForeColor = SystemColors.ControlText
        DataGridViewCellStyle6.WrapMode = DataGridViewTriState.False
        dgvGames.DefaultCellStyle = DataGridViewCellStyle6
        dgvGames.EditMode = DataGridViewEditMode.EditProgrammatically
        dgvGames.Location = New Point(11, 106)
        dgvGames.MultiSelect = False
        dgvGames.Name = "dgvGames"
        dgvGames.ReadOnly = True
        dgvGames.RowHeadersVisible = False
        dgvGames.RowHeadersWidth = 51
        dgvGames.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
        dgvGames.RowTemplate.Height = 29
        dgvGames.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvGames.ShowCellErrors = False
        dgvGames.ShowCellToolTips = False
        dgvGames.ShowEditingIcon = False
        dgvGames.ShowRowErrors = False
        dgvGames.Size = New Size(432, 661)
        dgvGames.TabIndex = 27
        dgvGames.VirtualMode = True
        ' 
        ' lblGamePk
        ' 
        lblGamePk.AutoSize = True
        lblGamePk.Font = New Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point)
        lblGamePk.ForeColor = SystemColors.ControlLightLight
        lblGamePk.Location = New Point(683, 106)
        lblGamePk.Name = "lblGamePk"
        lblGamePk.Size = New Size(70, 17)
        lblGamePk.TabIndex = 28
        lblGamePk.Text = "Game ID"
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.Font = New Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point)
        lblStatus.ForeColor = SystemColors.ControlLightLight
        lblStatus.Location = New Point(683, 139)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(101, 17)
        lblStatus.TabIndex = 29
        lblStatus.Text = "Game Status"
        ' 
        ' tbxCommentary
        ' 
        tbxCommentary.Font = New Font("Microsoft Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point)
        tbxCommentary.Location = New Point(509, 627)
        tbxCommentary.Name = "tbxCommentary"
        tbxCommentary.Size = New Size(719, 141)
        tbxCommentary.TabIndex = 30
        tbxCommentary.Text = "Play Commentary"
        ' 
        ' lblWeather
        ' 
        lblWeather.AutoSize = True
        lblWeather.Font = New Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point)
        lblWeather.ForeColor = SystemColors.ControlLightLight
        lblWeather.Location = New Point(685, 172)
        lblWeather.Name = "lblWeather"
        lblWeather.Size = New Size(69, 17)
        lblWeather.TabIndex = 31
        lblWeather.Text = "Weather"
        ' 
        ' imgAwayLogo
        ' 
        imgAwayLogo.ErrorImage = My.Resources.Resources.MLB
        imgAwayLogo.Image = My.Resources.Resources.MLB
        imgAwayLogo.InitialImage = My.Resources.Resources.MLB
        imgAwayLogo.Location = New Point(509, 106)
        imgAwayLogo.Name = "imgAwayLogo"
        imgAwayLogo.Size = New Size(155, 91)
        imgAwayLogo.SizeMode = PictureBoxSizeMode.StretchImage
        imgAwayLogo.TabIndex = 32
        imgAwayLogo.TabStop = False
        ' 
        ' imgHomeLogo
        ' 
        imgHomeLogo.ErrorImage = My.Resources.Resources.MLB
        imgHomeLogo.Image = My.Resources.Resources.MLB
        imgHomeLogo.InitialImage = My.Resources.Resources.MLB
        imgHomeLogo.Location = New Point(1073, 106)
        imgHomeLogo.Name = "imgHomeLogo"
        imgHomeLogo.Size = New Size(155, 91)
        imgHomeLogo.SizeMode = PictureBoxSizeMode.StretchImage
        imgHomeLogo.TabIndex = 33
        imgHomeLogo.TabStop = False
        ' 
        ' lblAwayWinnerLoser
        ' 
        lblAwayWinnerLoser.AutoSize = True
        lblAwayWinnerLoser.Font = New Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point)
        lblAwayWinnerLoser.ForeColor = SystemColors.ControlLightLight
        lblAwayWinnerLoser.Location = New Point(509, 209)
        lblAwayWinnerLoser.Name = "lblAwayWinnerLoser"
        lblAwayWinnerLoser.Size = New Size(93, 15)
        lblAwayWinnerLoser.TabIndex = 34
        lblAwayWinnerLoser.Text = "Winner-Loser"
        lblAwayWinnerLoser.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblHomeWinnerLoser
        ' 
        lblHomeWinnerLoser.AutoSize = True
        lblHomeWinnerLoser.Font = New Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point)
        lblHomeWinnerLoser.ForeColor = SystemColors.ControlLightLight
        lblHomeWinnerLoser.Location = New Point(1073, 209)
        lblHomeWinnerLoser.Name = "lblHomeWinnerLoser"
        lblHomeWinnerLoser.Size = New Size(93, 15)
        lblHomeWinnerLoser.TabIndex = 35
        lblHomeWinnerLoser.Text = "Winner-Loser"
        lblHomeWinnerLoser.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' GameUpdateTimer
        ' 
        GameUpdateTimer.Enabled = True
        GameUpdateTimer.Interval = 20000
        ' 
        ' lblPitchCount
        ' 
        lblPitchCount.AutoSize = True
        lblPitchCount.Font = New Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point)
        lblPitchCount.ForeColor = SystemColors.ControlLightLight
        lblPitchCount.Location = New Point(725, 557)
        lblPitchCount.Name = "lblPitchCount"
        lblPitchCount.Size = New Size(80, 17)
        lblPitchCount.TabIndex = 36
        lblPitchCount.Text = "Pitches: 0"
        ' 
        ' lblMatchupBatter
        ' 
        lblMatchupBatter.AutoSize = True
        lblMatchupBatter.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        lblMatchupBatter.ForeColor = SystemColors.ControlLightLight
        lblMatchupBatter.Location = New Point(725, 430)
        lblMatchupBatter.Name = "lblMatchupBatter"
        lblMatchupBatter.Size = New Size(111, 16)
        lblMatchupBatter.TabIndex = 38
        lblMatchupBatter.Text = "Matchup-Batter"
        ' 
        ' dgvBroadcasters
        ' 
        dgvBroadcasters.AllowUserToAddRows = False
        dgvBroadcasters.AllowUserToDeleteRows = False
        dgvBroadcasters.AllowUserToResizeColumns = False
        dgvBroadcasters.AllowUserToResizeRows = False
        dgvBroadcasters.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvBroadcasters.BackgroundColor = Color.Green
        dgvBroadcasters.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvBroadcasters.Location = New Point(1304, 809)
        dgvBroadcasters.MultiSelect = False
        dgvBroadcasters.Name = "dgvBroadcasters"
        dgvBroadcasters.ReadOnly = True
        dgvBroadcasters.RowHeadersVisible = False
        dgvBroadcasters.RowTemplate.Height = 28
        dgvBroadcasters.Size = New Size(604, 157)
        dgvBroadcasters.TabIndex = 39
        ' 
        ' dgvPlaySummary
        ' 
        dgvPlaySummary.AllowUserToAddRows = False
        dgvPlaySummary.AllowUserToDeleteRows = False
        dgvPlaySummary.AllowUserToResizeColumns = False
        dgvPlaySummary.AllowUserToResizeRows = False
        dgvPlaySummary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvPlaySummary.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvPlaySummary.BackgroundColor = Color.Green
        dgvPlaySummary.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvPlaySummary.Location = New Point(11, 809)
        dgvPlaySummary.MultiSelect = False
        dgvPlaySummary.Name = "dgvPlaySummary"
        dgvPlaySummary.ReadOnly = True
        dgvPlaySummary.RowHeadersVisible = False
        dgvPlaySummary.RowTemplate.Height = 28
        dgvPlaySummary.Size = New Size(1257, 157)
        dgvPlaySummary.TabIndex = 40
        ' 
        ' lblCommentary
        ' 
        lblCommentary.AutoSize = True
        lblCommentary.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        lblCommentary.ForeColor = SystemColors.ControlLightLight
        lblCommentary.Location = New Point(512, 596)
        lblCommentary.Name = "lblCommentary"
        lblCommentary.Size = New Size(128, 16)
        lblCommentary.TabIndex = 41
        lblCommentary.Text = "Play Commentary"
        ' 
        ' lblPlaySummary
        ' 
        lblPlaySummary.AutoSize = True
        lblPlaySummary.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        lblPlaySummary.ForeColor = SystemColors.ControlLightLight
        lblPlaySummary.Location = New Point(12, 780)
        lblPlaySummary.Name = "lblPlaySummary"
        lblPlaySummary.Size = New Size(106, 16)
        lblPlaySummary.TabIndex = 42
        lblPlaySummary.Text = "Play Summary"
        ' 
        ' lblBroadcasters
        ' 
        lblBroadcasters.AutoSize = True
        lblBroadcasters.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        lblBroadcasters.ForeColor = SystemColors.ControlLightLight
        lblBroadcasters.Location = New Point(1304, 780)
        lblBroadcasters.Name = "lblBroadcasters"
        lblBroadcasters.Size = New Size(100, 16)
        lblBroadcasters.TabIndex = 43
        lblBroadcasters.Text = "Broadcasters"
        ' 
        ' MlbScoreboards
        ' 
        AutoScaleDimensions = New SizeF(8F, 19F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.Green
        CausesValidation = False
        ClientSize = New Size(1924, 1057)
        Controls.Add(lblBroadcasters)
        Controls.Add(lblPlaySummary)
        Controls.Add(lblCommentary)
        Controls.Add(dgvPlaySummary)
        Controls.Add(dgvBroadcasters)
        Controls.Add(lblMatchupBatter)
        Controls.Add(lblPitchCount)
        Controls.Add(lblHomeWinnerLoser)
        Controls.Add(lblAwayWinnerLoser)
        Controls.Add(imgHomeLogo)
        Controls.Add(imgAwayLogo)
        Controls.Add(lblWeather)
        Controls.Add(tbxCommentary)
        Controls.Add(lblStatus)
        Controls.Add(lblGamePk)
        Controls.Add(dgvGames)
        Controls.Add(lblMatchupPitcher)
        Controls.Add(lblHomeLineup)
        Controls.Add(lblAwayLineup)
        Controls.Add(dgvHomeLineup)
        Controls.Add(dgvAwayLineup)
        Controls.Add(lblOuts)
        Controls.Add(lblStrikes)
        Controls.Add(lblBalls)
        Controls.Add(imgDiamond)
        Controls.Add(lblGameTitle)
        Controls.Add(dgvInnings)
        Controls.Add(calDatePicker)
        Controls.Add(StatusStrip1)
        Controls.Add(MenuStrip1)
        DoubleBuffered = True
        MainMenuStrip = MenuStrip1
        Margin = New Padding(3, 4, 3, 4)
        MinimumSize = New Size(680, 680)
        Name = "MlbScoreboards"
        Text = "MLB Live Scoreboards (C) 2022 MSRoth"
        WindowState = FormWindowState.Maximized
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        CType(dgvInnings, ComponentModel.ISupportInitialize).EndInit()
        CType(imgDiamond, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvAwayLineup, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvHomeLineup, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvGames, ComponentModel.ISupportInitialize).EndInit()
        CType(imgAwayLogo, ComponentModel.ISupportInitialize).EndInit()
        CType(imgHomeLogo, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvBroadcasters, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvPlaySummary, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

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
    Friend WithEvents lblMatchupPitcher As Label
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
    Friend WithEvents GameUpdateTimer As Timer
    Friend WithEvents StandingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents lblPitchCount As Label
    Friend WithEvents ThisGameUpdateData As ToolStripStatusLabel
    Friend WithEvents BoxscoreToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AllGamesUpdateProgressBar As ToolStripProgressBar
    Friend WithEvents Spacer As ToolStripStatusLabel
    Friend WithEvents lblMatchupBatter As Label
    Friend WithEvents dgvBroadcasters As DataGridView
    Friend WithEvents dgvPlaySummary As DataGridView
    Friend WithEvents lblCommentary As Label
    Friend WithEvents lblPlaySummary As Label
    Friend WithEvents lblBroadcasters As Label
End Class
