<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PlayerStats
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
        Me.lblPlayer = New System.Windows.Forms.Label()
        Me.lblPosition = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.dgvBattingStats = New System.Windows.Forms.DataGridView()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.dgvPitchingStats = New System.Windows.Forms.DataGridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.dgvFieldingStats = New System.Windows.Forms.DataGridView()
        Me.lblPlayerID = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.dgvBattingStats, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.dgvPitchingStats, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgvFieldingStats, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblPlayer
        '
        Me.lblPlayer.AutoSize = True
        Me.lblPlayer.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblPlayer.Location = New System.Drawing.Point(32, 19)
        Me.lblPlayer.Name = "lblPlayer"
        Me.lblPlayer.Size = New System.Drawing.Size(73, 25)
        Me.lblPlayer.TabIndex = 1
        Me.lblPlayer.Text = "Player"
        '
        'lblPosition
        '
        Me.lblPosition.AutoSize = True
        Me.lblPosition.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblPosition.Location = New System.Drawing.Point(32, 55)
        Me.lblPosition.Name = "lblPosition"
        Me.lblPosition.Size = New System.Drawing.Size(69, 20)
        Me.lblPosition.TabIndex = 2
        Me.lblPosition.Text = "Position"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(32, 101)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(519, 323)
        Me.TabControl1.TabIndex = 3
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dgvBattingStats)
        Me.TabPage1.Location = New System.Drawing.Point(4, 29)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(511, 290)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Batting"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'dgvBattingStats
        '
        Me.dgvBattingStats.AllowUserToAddRows = False
        Me.dgvBattingStats.AllowUserToDeleteRows = False
        Me.dgvBattingStats.AllowUserToOrderColumns = True
        Me.dgvBattingStats.AllowUserToResizeColumns = False
        Me.dgvBattingStats.AllowUserToResizeRows = False
        Me.dgvBattingStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBattingStats.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvBattingStats.Location = New System.Drawing.Point(6, 6)
        Me.dgvBattingStats.MultiSelect = False
        Me.dgvBattingStats.Name = "dgvBattingStats"
        Me.dgvBattingStats.ReadOnly = True
        Me.dgvBattingStats.RowHeadersVisible = False
        Me.dgvBattingStats.RowHeadersWidth = 51
        Me.dgvBattingStats.RowTemplate.Height = 29
        Me.dgvBattingStats.Size = New System.Drawing.Size(492, 278)
        Me.dgvBattingStats.TabIndex = 1
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.dgvPitchingStats)
        Me.TabPage3.Location = New System.Drawing.Point(4, 29)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(511, 290)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Pitching"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'dgvPitchingStats
        '
        Me.dgvPitchingStats.AllowUserToAddRows = False
        Me.dgvPitchingStats.AllowUserToDeleteRows = False
        Me.dgvPitchingStats.AllowUserToOrderColumns = True
        Me.dgvPitchingStats.AllowUserToResizeColumns = False
        Me.dgvPitchingStats.AllowUserToResizeRows = False
        Me.dgvPitchingStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPitchingStats.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvPitchingStats.Location = New System.Drawing.Point(9, 6)
        Me.dgvPitchingStats.MultiSelect = False
        Me.dgvPitchingStats.Name = "dgvPitchingStats"
        Me.dgvPitchingStats.ReadOnly = True
        Me.dgvPitchingStats.RowHeadersVisible = False
        Me.dgvPitchingStats.RowHeadersWidth = 51
        Me.dgvPitchingStats.RowTemplate.Height = 29
        Me.dgvPitchingStats.Size = New System.Drawing.Size(492, 278)
        Me.dgvPitchingStats.TabIndex = 2
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.dgvFieldingStats)
        Me.TabPage2.Location = New System.Drawing.Point(4, 29)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(511, 290)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Fielding"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'dgvFieldingStats
        '
        Me.dgvFieldingStats.AllowUserToAddRows = False
        Me.dgvFieldingStats.AllowUserToDeleteRows = False
        Me.dgvFieldingStats.AllowUserToOrderColumns = True
        Me.dgvFieldingStats.AllowUserToResizeColumns = False
        Me.dgvFieldingStats.AllowUserToResizeRows = False
        Me.dgvFieldingStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFieldingStats.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvFieldingStats.Location = New System.Drawing.Point(9, 6)
        Me.dgvFieldingStats.MultiSelect = False
        Me.dgvFieldingStats.Name = "dgvFieldingStats"
        Me.dgvFieldingStats.ReadOnly = True
        Me.dgvFieldingStats.RowHeadersVisible = False
        Me.dgvFieldingStats.RowHeadersWidth = 51
        Me.dgvFieldingStats.RowTemplate.Height = 29
        Me.dgvFieldingStats.Size = New System.Drawing.Size(492, 278)
        Me.dgvFieldingStats.TabIndex = 2
        '
        'lblPlayerID
        '
        Me.lblPlayerID.AutoSize = True
        Me.lblPlayerID.Font = New System.Drawing.Font("Segoe UI", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblPlayerID.Location = New System.Drawing.Point(498, 24)
        Me.lblPlayerID.Name = "lblPlayerID"
        Me.lblPlayerID.Size = New System.Drawing.Size(54, 17)
        Me.lblPlayerID.TabIndex = 4
        Me.lblPlayerID.Text = "PlayerId"
        Me.lblPlayerID.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'PlayerStats
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(579, 450)
        Me.Controls.Add(Me.lblPlayerID)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.lblPosition)
        Me.Controls.Add(Me.lblPlayer)
        Me.Name = "PlayerStats"
        Me.Text = "PlayerStats"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.dgvBattingStats, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.dgvPitchingStats, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.dgvFieldingStats, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblPlayer As Label
    Friend WithEvents lblPosition As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents dgvBattingStats As DataGridView
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents dgvFieldingStats As DataGridView
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents dgvPitchingStats As DataGridView
    Friend WithEvents lblPlayerID As Label
End Class
