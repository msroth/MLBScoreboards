<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MlbBoxscore
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
        Me.dgvAwayBatting = New System.Windows.Forms.DataGridView()
        Me.dgvHomeBatting = New System.Windows.Forms.DataGridView()
        Me.rtbAwayBattingFieldingDetails = New System.Windows.Forms.RichTextBox()
        Me.rtbHomeBattingFieldingDetails = New System.Windows.Forms.RichTextBox()
        Me.dgvAwayPitchers = New System.Windows.Forms.DataGridView()
        Me.dgvHomePitchers = New System.Windows.Forms.DataGridView()
        Me.rtbGameInfo = New System.Windows.Forms.RichTextBox()
        Me.lblAwayBatters = New System.Windows.Forms.Label()
        Me.lblHomeBatters = New System.Windows.Forms.Label()
        Me.lblAwayPitchers = New System.Windows.Forms.Label()
        Me.lblHomePitchers = New System.Windows.Forms.Label()
        Me.lblGameInfo = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.Away = New System.Windows.Forms.TabPage()
        Me.Home = New System.Windows.Forms.TabPage()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblGamePk = New System.Windows.Forms.Label()
        CType(Me.dgvAwayBatting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvHomeBatting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvAwayPitchers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvHomePitchers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.Away.SuspendLayout()
        Me.Home.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvAwayBatting
        '
        Me.dgvAwayBatting.AllowUserToAddRows = False
        Me.dgvAwayBatting.AllowUserToDeleteRows = False
        Me.dgvAwayBatting.AllowUserToOrderColumns = True
        Me.dgvAwayBatting.AllowUserToResizeColumns = False
        Me.dgvAwayBatting.AllowUserToResizeRows = False
        Me.dgvAwayBatting.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvAwayBatting.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dgvAwayBatting.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.dgvAwayBatting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAwayBatting.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvAwayBatting.Location = New System.Drawing.Point(16, 33)
        Me.dgvAwayBatting.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvAwayBatting.MultiSelect = False
        Me.dgvAwayBatting.Name = "dgvAwayBatting"
        Me.dgvAwayBatting.ReadOnly = True
        Me.dgvAwayBatting.RowHeadersVisible = False
        Me.dgvAwayBatting.RowHeadersWidth = 51
        Me.dgvAwayBatting.RowTemplate.Height = 29
        Me.dgvAwayBatting.ShowCellErrors = False
        Me.dgvAwayBatting.ShowCellToolTips = False
        Me.dgvAwayBatting.ShowEditingIcon = False
        Me.dgvAwayBatting.ShowRowErrors = False
        Me.dgvAwayBatting.Size = New System.Drawing.Size(525, 302)
        Me.dgvAwayBatting.TabIndex = 0
        '
        'dgvHomeBatting
        '
        Me.dgvHomeBatting.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvHomeBatting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvHomeBatting.Location = New System.Drawing.Point(21, 31)
        Me.dgvHomeBatting.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvHomeBatting.Name = "dgvHomeBatting"
        Me.dgvHomeBatting.RowHeadersVisible = False
        Me.dgvHomeBatting.RowHeadersWidth = 51
        Me.dgvHomeBatting.RowTemplate.Height = 29
        Me.dgvHomeBatting.Size = New System.Drawing.Size(525, 300)
        Me.dgvHomeBatting.TabIndex = 1
        '
        'rtbAwayBattingFieldingDetails
        '
        Me.rtbAwayBattingFieldingDetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.rtbAwayBattingFieldingDetails.Location = New System.Drawing.Point(16, 339)
        Me.rtbAwayBattingFieldingDetails.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.rtbAwayBattingFieldingDetails.Name = "rtbAwayBattingFieldingDetails"
        Me.rtbAwayBattingFieldingDetails.Size = New System.Drawing.Size(526, 91)
        Me.rtbAwayBattingFieldingDetails.TabIndex = 2
        Me.rtbAwayBattingFieldingDetails.Text = ""
        '
        'rtbHomeBattingFieldingDetails
        '
        Me.rtbHomeBattingFieldingDetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.rtbHomeBattingFieldingDetails.Location = New System.Drawing.Point(21, 335)
        Me.rtbHomeBattingFieldingDetails.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.rtbHomeBattingFieldingDetails.Name = "rtbHomeBattingFieldingDetails"
        Me.rtbHomeBattingFieldingDetails.Size = New System.Drawing.Size(526, 91)
        Me.rtbHomeBattingFieldingDetails.TabIndex = 3
        Me.rtbHomeBattingFieldingDetails.Text = ""
        '
        'dgvAwayPitchers
        '
        Me.dgvAwayPitchers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAwayPitchers.Location = New System.Drawing.Point(16, 471)
        Me.dgvAwayPitchers.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvAwayPitchers.Name = "dgvAwayPitchers"
        Me.dgvAwayPitchers.RowHeadersVisible = False
        Me.dgvAwayPitchers.RowHeadersWidth = 51
        Me.dgvAwayPitchers.RowTemplate.Height = 29
        Me.dgvAwayPitchers.Size = New System.Drawing.Size(525, 141)
        Me.dgvAwayPitchers.TabIndex = 4
        '
        'dgvHomePitchers
        '
        Me.dgvHomePitchers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvHomePitchers.Location = New System.Drawing.Point(21, 467)
        Me.dgvHomePitchers.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvHomePitchers.Name = "dgvHomePitchers"
        Me.dgvHomePitchers.RowHeadersVisible = False
        Me.dgvHomePitchers.RowHeadersWidth = 51
        Me.dgvHomePitchers.RowTemplate.Height = 29
        Me.dgvHomePitchers.Size = New System.Drawing.Size(525, 141)
        Me.dgvHomePitchers.TabIndex = 5
        '
        'rtbGameInfo
        '
        Me.rtbGameInfo.Location = New System.Drawing.Point(12, 736)
        Me.rtbGameInfo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.rtbGameInfo.Name = "rtbGameInfo"
        Me.rtbGameInfo.Size = New System.Drawing.Size(558, 91)
        Me.rtbGameInfo.TabIndex = 6
        Me.rtbGameInfo.Text = ""
        '
        'lblAwayBatters
        '
        Me.lblAwayBatters.AutoSize = True
        Me.lblAwayBatters.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblAwayBatters.Location = New System.Drawing.Point(16, 15)
        Me.lblAwayBatters.Name = "lblAwayBatters"
        Me.lblAwayBatters.Size = New System.Drawing.Size(93, 16)
        Me.lblAwayBatters.TabIndex = 7
        Me.lblAwayBatters.Text = "AwayBatters"
        '
        'lblHomeBatters
        '
        Me.lblHomeBatters.AutoSize = True
        Me.lblHomeBatters.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblHomeBatters.Location = New System.Drawing.Point(21, 11)
        Me.lblHomeBatters.Name = "lblHomeBatters"
        Me.lblHomeBatters.Size = New System.Drawing.Size(97, 16)
        Me.lblHomeBatters.TabIndex = 8
        Me.lblHomeBatters.Text = "HomeBatters"
        '
        'lblAwayPitchers
        '
        Me.lblAwayPitchers.AutoSize = True
        Me.lblAwayPitchers.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblAwayPitchers.Location = New System.Drawing.Point(16, 449)
        Me.lblAwayPitchers.Name = "lblAwayPitchers"
        Me.lblAwayPitchers.Size = New System.Drawing.Size(100, 16)
        Me.lblAwayPitchers.TabIndex = 9
        Me.lblAwayPitchers.Text = "AwayPitchers"
        '
        'lblHomePitchers
        '
        Me.lblHomePitchers.AutoSize = True
        Me.lblHomePitchers.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblHomePitchers.Location = New System.Drawing.Point(25, 445)
        Me.lblHomePitchers.Name = "lblHomePitchers"
        Me.lblHomePitchers.Size = New System.Drawing.Size(104, 16)
        Me.lblHomePitchers.TabIndex = 10
        Me.lblHomePitchers.Text = "HomePitchers"
        '
        'lblGameInfo
        '
        Me.lblGameInfo.AutoSize = True
        Me.lblGameInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblGameInfo.Location = New System.Drawing.Point(16, 716)
        Me.lblGameInfo.Name = "lblGameInfo"
        Me.lblGameInfo.Size = New System.Drawing.Size(77, 16)
        Me.lblGameInfo.TabIndex = 11
        Me.lblGameInfo.Text = "Game Info"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.Away)
        Me.TabControl1.Controls.Add(Me.Home)
        Me.TabControl1.Location = New System.Drawing.Point(12, 68)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(562, 645)
        Me.TabControl1.TabIndex = 12
        '
        'Away
        '
        Me.Away.Controls.Add(Me.dgvAwayPitchers)
        Me.Away.Controls.Add(Me.dgvAwayBatting)
        Me.Away.Controls.Add(Me.lblAwayBatters)
        Me.Away.Controls.Add(Me.lblAwayPitchers)
        Me.Away.Controls.Add(Me.rtbAwayBattingFieldingDetails)
        Me.Away.Location = New System.Drawing.Point(4, 24)
        Me.Away.Name = "Away"
        Me.Away.Padding = New System.Windows.Forms.Padding(3)
        Me.Away.Size = New System.Drawing.Size(554, 617)
        Me.Away.TabIndex = 0
        Me.Away.Text = "Away"
        Me.Away.UseVisualStyleBackColor = True
        '
        'Home
        '
        Me.Home.Controls.Add(Me.dgvHomeBatting)
        Me.Home.Controls.Add(Me.rtbHomeBattingFieldingDetails)
        Me.Home.Controls.Add(Me.lblHomePitchers)
        Me.Home.Controls.Add(Me.dgvHomePitchers)
        Me.Home.Controls.Add(Me.lblHomeBatters)
        Me.Home.Location = New System.Drawing.Point(4, 24)
        Me.Home.Name = "Home"
        Me.Home.Padding = New System.Windows.Forms.Padding(3)
        Me.Home.Size = New System.Drawing.Size(554, 621)
        Me.Home.TabIndex = 1
        Me.Home.Text = "Home"
        Me.Home.UseVisualStyleBackColor = True
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblTitle.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblTitle.Location = New System.Drawing.Point(14, 32)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(83, 16)
        Me.lblTitle.TabIndex = 13
        Me.lblTitle.Text = "Game Title"
        '
        'lblGamePk
        '
        Me.lblGamePk.AutoSize = True
        Me.lblGamePk.Location = New System.Drawing.Point(488, 24)
        Me.lblGamePk.Name = "lblGamePk"
        Me.lblGamePk.Size = New System.Drawing.Size(51, 15)
        Me.lblGamePk.TabIndex = 14
        Me.lblGamePk.Text = "GamePk"
        '
        'MlbBoxscore
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Green
        Me.ClientSize = New System.Drawing.Size(591, 875)
        Me.Controls.Add(Me.lblGamePk)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.lblGameInfo)
        Me.Controls.Add(Me.rtbGameInfo)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "MlbBoxscore"
        Me.Text = "Boxsore"
        CType(Me.dgvAwayBatting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvHomeBatting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvAwayPitchers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvHomePitchers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.Away.ResumeLayout(False)
        Me.Away.PerformLayout()
        Me.Home.ResumeLayout(False)
        Me.Home.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgvAwayBatting As DataGridView
    Friend WithEvents dgvHomeBatting As DataGridView
    Friend WithEvents rtbAwayBattingFieldingDetails As RichTextBox
    Friend WithEvents rtbHomeBattingFieldingDetails As RichTextBox
    Friend WithEvents dgvAwayPitchers As DataGridView
    Friend WithEvents dgvHomePitchers As DataGridView
    Friend WithEvents rtbGameInfo As RichTextBox
    Friend WithEvents lblAwayBatters As Label
    Friend WithEvents lblHomeBatters As Label
    Friend WithEvents lblAwayPitchers As Label
    Friend WithEvents lblHomePitchers As Label
    Friend WithEvents lblGameInfo As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents Away As TabPage
    Friend WithEvents Home As TabPage
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblGamePk As Label
End Class
