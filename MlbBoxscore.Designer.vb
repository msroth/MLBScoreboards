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
        Me.dgvHomeBatting = New System.Windows.Forms.DataGridView()
        Me.rtbHomeBattingFieldingDetails = New System.Windows.Forms.RichTextBox()
        Me.dgvHomePitchers = New System.Windows.Forms.DataGridView()
        Me.rtbGameInfo = New System.Windows.Forms.RichTextBox()
        Me.lblHomeBatters = New System.Windows.Forms.Label()
        Me.lblHomePitchers = New System.Windows.Forms.Label()
        Me.lblGameInfo = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.rtbAwayBattingFieldingDetails = New System.Windows.Forms.RichTextBox()
        Me.lblAwayPitchers = New System.Windows.Forms.Label()
        Me.lblAwayBatters = New System.Windows.Forms.Label()
        Me.dgvAwayBatting = New System.Windows.Forms.DataGridView()
        Me.dgvAwayPitchers = New System.Windows.Forms.DataGridView()
        CType(Me.dgvHomeBatting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvHomePitchers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvAwayBatting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvAwayPitchers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvHomeBatting
        '
        Me.dgvHomeBatting.AllowUserToAddRows = False
        Me.dgvHomeBatting.AllowUserToDeleteRows = False
        Me.dgvHomeBatting.AllowUserToResizeColumns = False
        Me.dgvHomeBatting.AllowUserToResizeRows = False
        Me.dgvHomeBatting.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvHomeBatting.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dgvHomeBatting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvHomeBatting.Location = New System.Drawing.Point(620, 117)
        Me.dgvHomeBatting.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvHomeBatting.Name = "dgvHomeBatting"
        Me.dgvHomeBatting.RowHeadersVisible = False
        Me.dgvHomeBatting.RowHeadersWidth = 51
        Me.dgvHomeBatting.RowTemplate.Height = 29
        Me.dgvHomeBatting.Size = New System.Drawing.Size(525, 300)
        Me.dgvHomeBatting.TabIndex = 1
        '
        'rtbHomeBattingFieldingDetails
        '
        Me.rtbHomeBattingFieldingDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.rtbHomeBattingFieldingDetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.rtbHomeBattingFieldingDetails.Location = New System.Drawing.Point(619, 421)
        Me.rtbHomeBattingFieldingDetails.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.rtbHomeBattingFieldingDetails.Name = "rtbHomeBattingFieldingDetails"
        Me.rtbHomeBattingFieldingDetails.Size = New System.Drawing.Size(526, 91)
        Me.rtbHomeBattingFieldingDetails.TabIndex = 3
        Me.rtbHomeBattingFieldingDetails.Text = ""
        '
        'dgvHomePitchers
        '
        Me.dgvHomePitchers.AllowUserToAddRows = False
        Me.dgvHomePitchers.AllowUserToDeleteRows = False
        Me.dgvHomePitchers.AllowUserToResizeColumns = False
        Me.dgvHomePitchers.AllowUserToResizeRows = False
        Me.dgvHomePitchers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvHomePitchers.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dgvHomePitchers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvHomePitchers.Location = New System.Drawing.Point(619, 568)
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
        Me.rtbGameInfo.Location = New System.Drawing.Point(12, 759)
        Me.rtbGameInfo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.rtbGameInfo.Name = "rtbGameInfo"
        Me.rtbGameInfo.Size = New System.Drawing.Size(1132, 91)
        Me.rtbGameInfo.TabIndex = 6
        Me.rtbGameInfo.Text = ""
        '
        'lblHomeBatters
        '
        Me.lblHomeBatters.AutoSize = True
        Me.lblHomeBatters.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblHomeBatters.Location = New System.Drawing.Point(619, 90)
        Me.lblHomeBatters.Name = "lblHomeBatters"
        Me.lblHomeBatters.Size = New System.Drawing.Size(97, 16)
        Me.lblHomeBatters.TabIndex = 8
        Me.lblHomeBatters.Text = "HomeBatters"
        '
        'lblHomePitchers
        '
        Me.lblHomePitchers.AutoSize = True
        Me.lblHomePitchers.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblHomePitchers.Location = New System.Drawing.Point(619, 536)
        Me.lblHomePitchers.Name = "lblHomePitchers"
        Me.lblHomePitchers.Size = New System.Drawing.Size(104, 16)
        Me.lblHomePitchers.TabIndex = 10
        Me.lblHomePitchers.Text = "HomePitchers"
        '
        'lblGameInfo
        '
        Me.lblGameInfo.AutoSize = True
        Me.lblGameInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblGameInfo.Location = New System.Drawing.Point(16, 734)
        Me.lblGameInfo.Name = "lblGameInfo"
        Me.lblGameInfo.Size = New System.Drawing.Size(77, 16)
        Me.lblGameInfo.TabIndex = 11
        Me.lblGameInfo.Text = "Game Info"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblTitle.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTitle.Location = New System.Drawing.Point(16, 50)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(86, 20)
        Me.lblTitle.TabIndex = 13
        Me.lblTitle.Text = "Game Title"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Label1.Location = New System.Drawing.Point(16, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 20)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Boxscore"
        '
        'rtbAwayBattingFieldingDetails
        '
        Me.rtbAwayBattingFieldingDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.rtbAwayBattingFieldingDetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.rtbAwayBattingFieldingDetails.Location = New System.Drawing.Point(16, 421)
        Me.rtbAwayBattingFieldingDetails.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.rtbAwayBattingFieldingDetails.Name = "rtbAwayBattingFieldingDetails"
        Me.rtbAwayBattingFieldingDetails.Size = New System.Drawing.Size(569, 91)
        Me.rtbAwayBattingFieldingDetails.TabIndex = 2
        Me.rtbAwayBattingFieldingDetails.Text = ""
        '
        'lblAwayPitchers
        '
        Me.lblAwayPitchers.AutoSize = True
        Me.lblAwayPitchers.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblAwayPitchers.Location = New System.Drawing.Point(16, 536)
        Me.lblAwayPitchers.Name = "lblAwayPitchers"
        Me.lblAwayPitchers.Size = New System.Drawing.Size(100, 16)
        Me.lblAwayPitchers.TabIndex = 9
        Me.lblAwayPitchers.Text = "AwayPitchers"
        '
        'lblAwayBatters
        '
        Me.lblAwayBatters.AutoSize = True
        Me.lblAwayBatters.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblAwayBatters.Location = New System.Drawing.Point(16, 90)
        Me.lblAwayBatters.Name = "lblAwayBatters"
        Me.lblAwayBatters.Size = New System.Drawing.Size(93, 16)
        Me.lblAwayBatters.TabIndex = 7
        Me.lblAwayBatters.Text = "AwayBatters"
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
        Me.dgvAwayBatting.Location = New System.Drawing.Point(16, 115)
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
        Me.dgvAwayBatting.Size = New System.Drawing.Size(569, 302)
        Me.dgvAwayBatting.TabIndex = 0
        '
        'dgvAwayPitchers
        '
        Me.dgvAwayPitchers.AllowUserToAddRows = False
        Me.dgvAwayPitchers.AllowUserToDeleteRows = False
        Me.dgvAwayPitchers.AllowUserToResizeColumns = False
        Me.dgvAwayPitchers.AllowUserToResizeRows = False
        Me.dgvAwayPitchers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvAwayPitchers.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dgvAwayPitchers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAwayPitchers.Location = New System.Drawing.Point(16, 568)
        Me.dgvAwayPitchers.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvAwayPitchers.Name = "dgvAwayPitchers"
        Me.dgvAwayPitchers.RowHeadersVisible = False
        Me.dgvAwayPitchers.RowHeadersWidth = 51
        Me.dgvAwayPitchers.RowTemplate.Height = 29
        Me.dgvAwayPitchers.Size = New System.Drawing.Size(569, 141)
        Me.dgvAwayPitchers.TabIndex = 4
        '
        'MlbBoxscore
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1167, 879)
        Me.Controls.Add(Me.dgvAwayPitchers)
        Me.Controls.Add(Me.lblAwayPitchers)
        Me.Controls.Add(Me.rtbHomeBattingFieldingDetails)
        Me.Controls.Add(Me.dgvAwayBatting)
        Me.Controls.Add(Me.rtbAwayBattingFieldingDetails)
        Me.Controls.Add(Me.lblAwayBatters)
        Me.Controls.Add(Me.lblHomePitchers)
        Me.Controls.Add(Me.dgvHomeBatting)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgvHomePitchers)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.lblHomeBatters)
        Me.Controls.Add(Me.lblGameInfo)
        Me.Controls.Add(Me.rtbGameInfo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "MlbBoxscore"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Boxscore"
        CType(Me.dgvHomeBatting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvHomePitchers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvAwayBatting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvAwayPitchers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvHomeBatting As DataGridView
    Friend WithEvents rtbHomeBattingFieldingDetails As RichTextBox
    Friend WithEvents dgvHomePitchers As DataGridView
    Friend WithEvents rtbGameInfo As RichTextBox
    Friend WithEvents lblHomeBatters As Label
    Friend WithEvents lblHomePitchers As Label
    Friend WithEvents lblGameInfo As Label
    Friend WithEvents lblTitle As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents rtbAwayBattingFieldingDetails As RichTextBox
    Friend WithEvents lblAwayPitchers As Label
    Friend WithEvents lblAwayBatters As Label
    Friend WithEvents dgvAwayBatting As DataGridView
    Friend WithEvents dgvAwayPitchers As DataGridView
End Class
