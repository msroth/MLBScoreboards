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
        lblPlayer = New Label()
        lblPosition = New Label()
        TabControl1 = New TabControl()
        Batting = New TabPage()
        dgvBattingStats = New DataGridView()
        Fielding = New TabPage()
        dgvFieldingStats = New DataGridView()
        Pitching = New TabPage()
        dgvPitchingStats = New DataGridView()
        lblPlayerID = New Label()
        LinkLabel1 = New LinkLabel()
        picHeadshot = New PictureBox()
        TabControl1.SuspendLayout()
        Batting.SuspendLayout()
        CType(dgvBattingStats, ComponentModel.ISupportInitialize).BeginInit()
        Fielding.SuspendLayout()
        CType(dgvFieldingStats, ComponentModel.ISupportInitialize).BeginInit()
        Pitching.SuspendLayout()
        CType(dgvPitchingStats, ComponentModel.ISupportInitialize).BeginInit()
        CType(picHeadshot, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lblPlayer
        ' 
        lblPlayer.AutoSize = True
        lblPlayer.Font = New Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point)
        lblPlayer.ForeColor = SystemColors.ControlText
        lblPlayer.Location = New Point(32, 18)
        lblPlayer.Name = "lblPlayer"
        lblPlayer.Size = New Size(58, 20)
        lblPlayer.TabIndex = 1
        lblPlayer.Text = "Player"
        ' 
        ' lblPosition
        ' 
        lblPosition.AutoSize = True
        lblPosition.Font = New Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point)
        lblPosition.ForeColor = SystemColors.ControlText
        lblPosition.Location = New Point(32, 52)
        lblPosition.Name = "lblPosition"
        lblPosition.Size = New Size(65, 20)
        lblPosition.TabIndex = 2
        lblPosition.Text = "Position"
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(Batting)
        TabControl1.Controls.Add(Fielding)
        TabControl1.Controls.Add(Pitching)
        TabControl1.Location = New Point(32, 96)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(853, 640)
        TabControl1.TabIndex = 3
        ' 
        ' Batting
        ' 
        Batting.Controls.Add(dgvBattingStats)
        Batting.Location = New Point(4, 28)
        Batting.Name = "Batting"
        Batting.Padding = New Padding(3)
        Batting.Size = New Size(845, 608)
        Batting.TabIndex = 0
        Batting.Text = "Batting"
        Batting.UseVisualStyleBackColor = True
        ' 
        ' dgvBattingStats
        ' 
        dgvBattingStats.AllowUserToAddRows = False
        dgvBattingStats.AllowUserToDeleteRows = False
        dgvBattingStats.AllowUserToOrderColumns = True
        dgvBattingStats.AllowUserToResizeColumns = False
        dgvBattingStats.AllowUserToResizeRows = False
        dgvBattingStats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvBattingStats.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvBattingStats.EditMode = DataGridViewEditMode.EditProgrammatically
        dgvBattingStats.Location = New Point(19, 29)
        dgvBattingStats.MultiSelect = False
        dgvBattingStats.Name = "dgvBattingStats"
        dgvBattingStats.ReadOnly = True
        dgvBattingStats.RowHeadersVisible = False
        dgvBattingStats.RowHeadersWidth = 51
        dgvBattingStats.RowTemplate.Height = 29
        dgvBattingStats.Size = New Size(799, 546)
        dgvBattingStats.TabIndex = 1
        ' 
        ' Fielding
        ' 
        Fielding.Controls.Add(dgvFieldingStats)
        Fielding.Location = New Point(4, 28)
        Fielding.Name = "Fielding"
        Fielding.Padding = New Padding(3)
        Fielding.Size = New Size(845, 608)
        Fielding.TabIndex = 1
        Fielding.Text = "Fielding"
        Fielding.UseVisualStyleBackColor = True
        ' 
        ' dgvFieldingStats
        ' 
        dgvFieldingStats.AllowUserToAddRows = False
        dgvFieldingStats.AllowUserToDeleteRows = False
        dgvFieldingStats.AllowUserToOrderColumns = True
        dgvFieldingStats.AllowUserToResizeColumns = False
        dgvFieldingStats.AllowUserToResizeRows = False
        dgvFieldingStats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvFieldingStats.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvFieldingStats.EditMode = DataGridViewEditMode.EditProgrammatically
        dgvFieldingStats.Location = New Point(19, 29)
        dgvFieldingStats.MultiSelect = False
        dgvFieldingStats.Name = "dgvFieldingStats"
        dgvFieldingStats.ReadOnly = True
        dgvFieldingStats.RowHeadersVisible = False
        dgvFieldingStats.RowHeadersWidth = 51
        dgvFieldingStats.RowTemplate.Height = 29
        dgvFieldingStats.Size = New Size(799, 546)
        dgvFieldingStats.TabIndex = 2
        ' 
        ' Pitching
        ' 
        Pitching.Controls.Add(dgvPitchingStats)
        Pitching.Location = New Point(4, 28)
        Pitching.Name = "Pitching"
        Pitching.Padding = New Padding(3)
        Pitching.Size = New Size(845, 608)
        Pitching.TabIndex = 2
        Pitching.Text = "Pitching"
        Pitching.UseVisualStyleBackColor = True
        ' 
        ' dgvPitchingStats
        ' 
        dgvPitchingStats.AllowUserToAddRows = False
        dgvPitchingStats.AllowUserToDeleteRows = False
        dgvPitchingStats.AllowUserToOrderColumns = True
        dgvPitchingStats.AllowUserToResizeColumns = False
        dgvPitchingStats.AllowUserToResizeRows = False
        dgvPitchingStats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvPitchingStats.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvPitchingStats.EditMode = DataGridViewEditMode.EditProgrammatically
        dgvPitchingStats.Location = New Point(19, 29)
        dgvPitchingStats.MultiSelect = False
        dgvPitchingStats.Name = "dgvPitchingStats"
        dgvPitchingStats.ReadOnly = True
        dgvPitchingStats.RowHeadersVisible = False
        dgvPitchingStats.RowHeadersWidth = 51
        dgvPitchingStats.RowTemplate.Height = 29
        dgvPitchingStats.Size = New Size(799, 546)
        dgvPitchingStats.TabIndex = 2
        ' 
        ' lblPlayerID
        ' 
        lblPlayerID.AutoSize = True
        lblPlayerID.Font = New Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point)
        lblPlayerID.Location = New Point(693, 18)
        lblPlayerID.Name = "lblPlayerID"
        lblPlayerID.Size = New Size(51, 15)
        lblPlayerID.TabIndex = 4
        lblPlayerID.Text = "PlayerId"
        lblPlayerID.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' LinkLabel1
        ' 
        LinkLabel1.AutoSize = True
        LinkLabel1.Location = New Point(693, 36)
        LinkLabel1.Name = "LinkLabel1"
        LinkLabel1.Size = New Size(181, 19)
        LinkLabel1.TabIndex = 5
        LinkLabel1.TabStop = True
        LinkLabel1.Text = "https://www.mlb.com/player"
        LinkLabel1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' picHeadshot
        ' 
        picHeadshot.BorderStyle = BorderStyle.FixedSingle
        picHeadshot.Location = New Point(604, 18)
        picHeadshot.Name = "picHeadshot"
        picHeadshot.Size = New Size(70, 87)
        picHeadshot.SizeMode = PictureBoxSizeMode.Zoom
        picHeadshot.TabIndex = 6
        picHeadshot.TabStop = False
        ' 
        ' MlbPlayerStats
        ' 
        AutoScaleDimensions = New SizeF(8F, 19F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.Control
        ClientSize = New Size(930, 760)
        Controls.Add(picHeadshot)
        Controls.Add(LinkLabel1)
        Controls.Add(lblPlayerID)
        Controls.Add(TabControl1)
        Controls.Add(lblPosition)
        Controls.Add(lblPlayer)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Name = "MlbPlayerStats"
        StartPosition = FormStartPosition.CenterParent
        Text = "Player Stats"
        TopMost = True
        TabControl1.ResumeLayout(False)
        Batting.ResumeLayout(False)
        CType(dgvBattingStats, ComponentModel.ISupportInitialize).EndInit()
        Fielding.ResumeLayout(False)
        CType(dgvFieldingStats, ComponentModel.ISupportInitialize).EndInit()
        Pitching.ResumeLayout(False)
        CType(dgvPitchingStats, ComponentModel.ISupportInitialize).EndInit()
        CType(picHeadshot, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

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
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents picHeadshot As PictureBox
End Class
