<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MlbPlayerStats
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
        Me.lblPlayer = New System.Windows.Forms.Label()
        Me.lblPosition = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.Batting = New System.Windows.Forms.TabPage()
        Me.dgvBattingStats = New System.Windows.Forms.DataGridView()
        Me.Fielding = New System.Windows.Forms.TabPage()
        Me.dgvFieldingStats = New System.Windows.Forms.DataGridView()
        Me.Pitching = New System.Windows.Forms.TabPage()
        Me.dgvPitchingStats = New System.Windows.Forms.DataGridView()
        Me.lblPlayerID = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.Batting.SuspendLayout()
        CType(Me.dgvBattingStats, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Fielding.SuspendLayout()
        CType(Me.dgvFieldingStats, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Pitching.SuspendLayout()
        CType(Me.dgvPitchingStats, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblPlayer
        '
        Me.lblPlayer.AutoSize = True
        Me.lblPlayer.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblPlayer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPlayer.Location = New System.Drawing.Point(28, 14)
        Me.lblPlayer.Name = "lblPlayer"
        Me.lblPlayer.Size = New System.Drawing.Size(58, 20)
        Me.lblPlayer.TabIndex = 1
        Me.lblPlayer.Text = "Player"
        '
        'lblPosition
        '
        Me.lblPosition.AutoSize = True
        Me.lblPosition.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblPosition.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPosition.Location = New System.Drawing.Point(28, 41)
        Me.lblPosition.Name = "lblPosition"
        Me.lblPosition.Size = New System.Drawing.Size(65, 20)
        Me.lblPosition.TabIndex = 2
        Me.lblPosition.Text = "Position"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.Batting)
        Me.TabControl1.Controls.Add(Me.Fielding)
        Me.TabControl1.Controls.Add(Me.Pitching)
        Me.TabControl1.Location = New System.Drawing.Point(28, 76)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(746, 505)
        Me.TabControl1.TabIndex = 3
        '
        'Batting
        '
        Me.Batting.Controls.Add(Me.dgvBattingStats)
        Me.Batting.Location = New System.Drawing.Point(4, 24)
        Me.Batting.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Batting.Name = "Batting"
        Me.Batting.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Batting.Size = New System.Drawing.Size(738, 477)
        Me.Batting.TabIndex = 0
        Me.Batting.Text = "Batting"
        Me.Batting.UseVisualStyleBackColor = True
        '
        'dgvBattingStats
        '
        Me.dgvBattingStats.AllowUserToAddRows = False
        Me.dgvBattingStats.AllowUserToDeleteRows = False
        Me.dgvBattingStats.AllowUserToOrderColumns = True
        Me.dgvBattingStats.AllowUserToResizeColumns = False
        Me.dgvBattingStats.AllowUserToResizeRows = False
        Me.dgvBattingStats.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvBattingStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBattingStats.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvBattingStats.Location = New System.Drawing.Point(17, 47)
        Me.dgvBattingStats.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvBattingStats.MultiSelect = False
        Me.dgvBattingStats.Name = "dgvBattingStats"
        Me.dgvBattingStats.ReadOnly = True
        Me.dgvBattingStats.RowHeadersVisible = False
        Me.dgvBattingStats.RowHeadersWidth = 51
        Me.dgvBattingStats.RowTemplate.Height = 29
        Me.dgvBattingStats.Size = New System.Drawing.Size(699, 390)
        Me.dgvBattingStats.TabIndex = 1
        '
        'Fielding
        '
        Me.Fielding.Controls.Add(Me.dgvFieldingStats)
        Me.Fielding.Location = New System.Drawing.Point(4, 24)
        Me.Fielding.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Fielding.Name = "Fielding"
        Me.Fielding.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Fielding.Size = New System.Drawing.Size(446, 334)
        Me.Fielding.TabIndex = 1
        Me.Fielding.Text = "Fielding"
        Me.Fielding.UseVisualStyleBackColor = True
        '
        'dgvFieldingStats
        '
        Me.dgvFieldingStats.AllowUserToAddRows = False
        Me.dgvFieldingStats.AllowUserToDeleteRows = False
        Me.dgvFieldingStats.AllowUserToOrderColumns = True
        Me.dgvFieldingStats.AllowUserToResizeColumns = False
        Me.dgvFieldingStats.AllowUserToResizeRows = False
        Me.dgvFieldingStats.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvFieldingStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFieldingStats.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvFieldingStats.Location = New System.Drawing.Point(8, 4)
        Me.dgvFieldingStats.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvFieldingStats.MultiSelect = False
        Me.dgvFieldingStats.Name = "dgvFieldingStats"
        Me.dgvFieldingStats.ReadOnly = True
        Me.dgvFieldingStats.RowHeadersVisible = False
        Me.dgvFieldingStats.RowHeadersWidth = 51
        Me.dgvFieldingStats.RowTemplate.Height = 29
        Me.dgvFieldingStats.Size = New System.Drawing.Size(430, 326)
        Me.dgvFieldingStats.TabIndex = 2
        '
        'Pitching
        '
        Me.Pitching.Controls.Add(Me.dgvPitchingStats)
        Me.Pitching.Location = New System.Drawing.Point(4, 24)
        Me.Pitching.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Pitching.Name = "Pitching"
        Me.Pitching.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Pitching.Size = New System.Drawing.Size(446, 334)
        Me.Pitching.TabIndex = 2
        Me.Pitching.Text = "Pitching"
        Me.Pitching.UseVisualStyleBackColor = True
        '
        'dgvPitchingStats
        '
        Me.dgvPitchingStats.AllowUserToAddRows = False
        Me.dgvPitchingStats.AllowUserToDeleteRows = False
        Me.dgvPitchingStats.AllowUserToOrderColumns = True
        Me.dgvPitchingStats.AllowUserToResizeColumns = False
        Me.dgvPitchingStats.AllowUserToResizeRows = False
        Me.dgvPitchingStats.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvPitchingStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPitchingStats.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvPitchingStats.Location = New System.Drawing.Point(8, 4)
        Me.dgvPitchingStats.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvPitchingStats.MultiSelect = False
        Me.dgvPitchingStats.Name = "dgvPitchingStats"
        Me.dgvPitchingStats.ReadOnly = True
        Me.dgvPitchingStats.RowHeadersVisible = False
        Me.dgvPitchingStats.RowHeadersWidth = 51
        Me.dgvPitchingStats.RowTemplate.Height = 29
        Me.dgvPitchingStats.Size = New System.Drawing.Size(430, 326)
        Me.dgvPitchingStats.TabIndex = 2
        '
        'lblPlayerID
        '
        Me.lblPlayerID.AutoSize = True
        Me.lblPlayerID.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblPlayerID.Location = New System.Drawing.Point(697, 19)
        Me.lblPlayerID.Name = "lblPlayerID"
        Me.lblPlayerID.Size = New System.Drawing.Size(51, 15)
        Me.lblPlayerID.TabIndex = 4
        Me.lblPlayerID.Text = "PlayerId"
        Me.lblPlayerID.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'MlbPlayerStats
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(814, 600)
        Me.Controls.Add(Me.lblPlayerID)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.lblPosition)
        Me.Controls.Add(Me.lblPlayer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "MlbPlayerStats"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Player Stats"
        Me.TabControl1.ResumeLayout(False)
        Me.Batting.ResumeLayout(False)
        CType(Me.dgvBattingStats, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Fielding.ResumeLayout(False)
        CType(Me.dgvFieldingStats, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Pitching.ResumeLayout(False)
        CType(Me.dgvPitchingStats, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblPlayer As Label
    Friend WithEvents lblPosition As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents Batting As TabPage
    Friend WithEvents dgvBattingStats As DataGridView
    Friend WithEvents Fielding As TabPage
    Friend WithEvents dgvFieldingStats As DataGridView
    Friend WithEvents Pitching As TabPage
    Friend WithEvents dgvPitchingStats As DataGridView
    Friend WithEvents lblPlayerID As Label
End Class
