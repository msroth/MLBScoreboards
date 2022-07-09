<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MLBScoreboard
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
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.QuitToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.calDatePicker = New System.Windows.Forms.DateTimePicker()
        Me.cbxTeam = New System.Windows.Forms.ComboBox()
        Me.dgvInnings = New System.Windows.Forms.DataGridView()
        Me.team = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblGameTitle = New System.Windows.Forms.Label()
        Me.imgDiamond = New System.Windows.Forms.PictureBox()
        Me.txtCommentary = New System.Windows.Forms.TextBox()
        Me.txbPitch = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblBalls = New System.Windows.Forms.Label()
        Me.lblStrikes = New System.Windows.Forms.Label()
        Me.lblOuts = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.dgvAwayRoster = New System.Windows.Forms.DataGridView()
        Me.dgvHomeRoster = New System.Windows.Forms.DataGridView()
        Me.lblAwayRoster = New System.Windows.Forms.Label()
        Me.lblHomeRoster = New System.Windows.Forms.Label()
        Me.lblMatchup = New System.Windows.Forms.Label()
        Me.txbMatchup = New System.Windows.Forms.TextBox()
        Me.StatusStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.dgvInnings, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgDiamond, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvAwayRoster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvHomeRoster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 510)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 16, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(1417, 22)
        Me.StatusStrip1.TabIndex = 0
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.BackColor = System.Drawing.Color.White
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(0, 16)
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.AboutToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(7, 3, 0, 3)
        Me.MenuStrip1.Size = New System.Drawing.Size(1417, 30)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.QuitToolStripMenuItem1})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(46, 24)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'QuitToolStripMenuItem1
        '
        Me.QuitToolStripMenuItem1.Name = "QuitToolStripMenuItem1"
        Me.QuitToolStripMenuItem1.Size = New System.Drawing.Size(120, 26)
        Me.QuitToolStripMenuItem1.Text = "Quit"
        '
        'AboutToolStripMenuItem1
        '
        Me.AboutToolStripMenuItem1.Name = "AboutToolStripMenuItem1"
        Me.AboutToolStripMenuItem1.Size = New System.Drawing.Size(64, 24)
        Me.AboutToolStripMenuItem1.Text = "About"
        '
        'btnFind
        '
        Me.btnFind.Location = New System.Drawing.Point(519, 41)
        Me.btnFind.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(101, 31)
        Me.btnFind.TabIndex = 5
        Me.btnFind.Text = "Find Game"
        Me.btnFind.UseVisualStyleBackColor = True
        '
        'calDatePicker
        '
        Me.calDatePicker.CustomFormat = """MM/dd/yyyy"""
        Me.calDatePicker.Location = New System.Drawing.Point(256, 43)
        Me.calDatePicker.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.calDatePicker.Name = "calDatePicker"
        Me.calDatePicker.Size = New System.Drawing.Size(248, 27)
        Me.calDatePicker.TabIndex = 4
        '
        'cbxTeam
        '
        Me.cbxTeam.FormattingEnabled = True
        Me.cbxTeam.Location = New System.Drawing.Point(12, 43)
        Me.cbxTeam.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbxTeam.Name = "cbxTeam"
        Me.cbxTeam.Size = New System.Drawing.Size(229, 28)
        Me.cbxTeam.TabIndex = 3
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
        Me.dgvInnings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInnings.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvInnings.Enabled = False
        Me.dgvInnings.Location = New System.Drawing.Point(13, 129)
        Me.dgvInnings.MultiSelect = False
        Me.dgvInnings.Name = "dgvInnings"
        Me.dgvInnings.ReadOnly = True
        Me.dgvInnings.RowHeadersVisible = False
        Me.dgvInnings.RowHeadersWidth = 51
        Me.dgvInnings.RowTemplate.Height = 29
        Me.dgvInnings.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.dgvInnings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvInnings.ShowCellToolTips = False
        Me.dgvInnings.ShowEditingIcon = False
        Me.dgvInnings.Size = New System.Drawing.Size(823, 120)
        Me.dgvInnings.TabIndex = 6
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
        Me.lblGameTitle.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblGameTitle.Location = New System.Drawing.Point(13, 95)
        Me.lblGameTitle.Name = "lblGameTitle"
        Me.lblGameTitle.Size = New System.Drawing.Size(277, 20)
        Me.lblGameTitle.TabIndex = 8
        Me.lblGameTitle.Text = "Away @ Home - mm/dd/yyyy (gamePk) "
        '
        'imgDiamond
        '
        Me.imgDiamond.ErrorImage = Nothing
        Me.imgDiamond.Image = Global.MLBScoreBoard.My.Resources.Resources.diamond
        Me.imgDiamond.ImageLocation = ""
        Me.imgDiamond.InitialImage = Global.MLBScoreBoard.My.Resources.Resources.diamond
        Me.imgDiamond.Location = New System.Drawing.Point(13, 302)
        Me.imgDiamond.Name = "imgDiamond"
        Me.imgDiamond.Size = New System.Drawing.Size(190, 190)
        Me.imgDiamond.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgDiamond.TabIndex = 11
        Me.imgDiamond.TabStop = False
        Me.imgDiamond.Tag = "Diamond"
        Me.imgDiamond.WaitOnLoad = True
        '
        'txtCommentary
        '
        Me.txtCommentary.Location = New System.Drawing.Point(224, 347)
        Me.txtCommentary.Multiline = True
        Me.txtCommentary.Name = "txtCommentary"
        Me.txtCommentary.ReadOnly = True
        Me.txtCommentary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCommentary.Size = New System.Drawing.Size(612, 82)
        Me.txtCommentary.TabIndex = 12
        '
        'txbPitch
        '
        Me.txbPitch.Location = New System.Drawing.Point(224, 463)
        Me.txbPitch.Name = "txbPitch"
        Me.txbPitch.ReadOnly = True
        Me.txbPitch.Size = New System.Drawing.Size(612, 27)
        Me.txbPitch.TabIndex = 13
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label2.Location = New System.Drawing.Point(224, 324)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(125, 20)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Play Commentary"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label3.Location = New System.Drawing.Point(221, 440)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 20)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Last Pitch"
        '
        'lblBalls
        '
        Me.lblBalls.AutoSize = True
        Me.lblBalls.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblBalls.Location = New System.Drawing.Point(13, 267)
        Me.lblBalls.Name = "lblBalls"
        Me.lblBalls.Size = New System.Drawing.Size(55, 20)
        Me.lblBalls.TabIndex = 18
        Me.lblBalls.Text = "Balls: 0"
        '
        'lblStrikes
        '
        Me.lblStrikes.AutoSize = True
        Me.lblStrikes.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblStrikes.Location = New System.Drawing.Point(74, 267)
        Me.lblStrikes.Name = "lblStrikes"
        Me.lblStrikes.Size = New System.Drawing.Size(67, 20)
        Me.lblStrikes.TabIndex = 19
        Me.lblStrikes.Text = "Strikes: 0"
        '
        'lblOuts
        '
        Me.lblOuts.AutoSize = True
        Me.lblOuts.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblOuts.Location = New System.Drawing.Point(147, 267)
        Me.lblOuts.Name = "lblOuts"
        Me.lblOuts.Size = New System.Drawing.Size(54, 20)
        Me.lblOuts.TabIndex = 20
        Me.lblOuts.Text = "Outs: 0"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 15000
        '
        'dgvAwayRoster
        '
        Me.dgvAwayRoster.AllowUserToAddRows = False
        Me.dgvAwayRoster.AllowUserToDeleteRows = False
        Me.dgvAwayRoster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAwayRoster.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvAwayRoster.Location = New System.Drawing.Point(860, 129)
        Me.dgvAwayRoster.Name = "dgvAwayRoster"
        Me.dgvAwayRoster.ReadOnly = True
        Me.dgvAwayRoster.RowHeadersVisible = False
        Me.dgvAwayRoster.RowHeadersWidth = 51
        Me.dgvAwayRoster.RowTemplate.Height = 29
        Me.dgvAwayRoster.Size = New System.Drawing.Size(256, 346)
        Me.dgvAwayRoster.TabIndex = 21
        '
        'dgvHomeRoster
        '
        Me.dgvHomeRoster.AllowUserToAddRows = False
        Me.dgvHomeRoster.AllowUserToDeleteRows = False
        Me.dgvHomeRoster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvHomeRoster.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvHomeRoster.Location = New System.Drawing.Point(1138, 128)
        Me.dgvHomeRoster.Name = "dgvHomeRoster"
        Me.dgvHomeRoster.ReadOnly = True
        Me.dgvHomeRoster.RowHeadersVisible = False
        Me.dgvHomeRoster.RowHeadersWidth = 51
        Me.dgvHomeRoster.RowTemplate.Height = 29
        Me.dgvHomeRoster.Size = New System.Drawing.Size(256, 346)
        Me.dgvHomeRoster.TabIndex = 22
        '
        'lblAwayRoster
        '
        Me.lblAwayRoster.AutoSize = True
        Me.lblAwayRoster.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblAwayRoster.Location = New System.Drawing.Point(862, 96)
        Me.lblAwayRoster.Name = "lblAwayRoster"
        Me.lblAwayRoster.Size = New System.Drawing.Size(91, 20)
        Me.lblAwayRoster.TabIndex = 23
        Me.lblAwayRoster.Text = "Away Roster"
        '
        'lblHomeRoster
        '
        Me.lblHomeRoster.AutoSize = True
        Me.lblHomeRoster.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblHomeRoster.Location = New System.Drawing.Point(1138, 97)
        Me.lblHomeRoster.Name = "lblHomeRoster"
        Me.lblHomeRoster.Size = New System.Drawing.Size(96, 20)
        Me.lblHomeRoster.TabIndex = 24
        Me.lblHomeRoster.Text = "Home Roster"
        '
        'lblMatchup
        '
        Me.lblMatchup.AutoSize = True
        Me.lblMatchup.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblMatchup.Location = New System.Drawing.Point(227, 267)
        Me.lblMatchup.Name = "lblMatchup"
        Me.lblMatchup.Size = New System.Drawing.Size(170, 20)
        Me.lblMatchup.TabIndex = 25
        Me.lblMatchup.Text = "Pitcher - Batter Matchup"
        '
        'txbMatchup
        '
        Me.txbMatchup.Location = New System.Drawing.Point(227, 294)
        Me.txbMatchup.Name = "txbMatchup"
        Me.txbMatchup.Size = New System.Drawing.Size(609, 27)
        Me.txbMatchup.TabIndex = 26
        '
        'MLBScoreboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Green
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(1417, 532)
        Me.Controls.Add(Me.txbMatchup)
        Me.Controls.Add(Me.lblMatchup)
        Me.Controls.Add(Me.lblHomeRoster)
        Me.Controls.Add(Me.lblAwayRoster)
        Me.Controls.Add(Me.dgvHomeRoster)
        Me.Controls.Add(Me.dgvAwayRoster)
        Me.Controls.Add(Me.lblOuts)
        Me.Controls.Add(Me.lblStrikes)
        Me.Controls.Add(Me.lblBalls)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txbPitch)
        Me.Controls.Add(Me.txtCommentary)
        Me.Controls.Add(Me.imgDiamond)
        Me.Controls.Add(Me.lblGameTitle)
        Me.Controls.Add(Me.dgvInnings)
        Me.Controls.Add(Me.btnFind)
        Me.Controls.Add(Me.calDatePicker)
        Me.Controls.Add(Me.cbxTeam)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "MLBScoreboard"
        Me.Text = "MLB Live Scoreboards (C) 2021 MSRoth"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.dgvInnings, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgDiamond, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvAwayRoster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvHomeRoster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents QuitToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents btnFind As Button
    Friend WithEvents calDatePicker As DateTimePicker
    Friend WithEvents cbxTeam As ComboBox
    Friend WithEvents dgvInnings As DataGridView
    Friend WithEvents team As DataGridViewTextBoxColumn
    Friend WithEvents lblGameTitle As Label
    Friend WithEvents imgDiamond As PictureBox
    Friend WithEvents txtCommentary As TextBox
    Friend WithEvents txbPitch As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lblBalls As Label
    Friend WithEvents lblStrikes As Label
    Friend WithEvents lblOuts As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents dgvAwayRoster As DataGridView
    Friend WithEvents dgvHomeRoster As DataGridView
    Friend WithEvents lblAwayRoster As Label
    Friend WithEvents lblHomeRoster As Label
    Friend WithEvents lblMatchup As Label
    Friend WithEvents txbMatchup As TextBox
End Class
