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
        dgvHomeBatting = New DataGridView()
        rtbHomeBattingFieldingDetails = New RichTextBox()
        dgvHomePitchers = New DataGridView()
        rtbGameInfo = New RichTextBox()
        lblHomeBatters = New Label()
        lblHomePitchers = New Label()
        lblGameInfo = New Label()
        lblTitle = New Label()
        Label1 = New Label()
        rtbAwayBattingFieldingDetails = New RichTextBox()
        lblAwayPitchers = New Label()
        lblAwayBatters = New Label()
        dgvAwayBatting = New DataGridView()
        dgvAwayPitchers = New DataGridView()
        CType(dgvHomeBatting, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvHomePitchers, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvAwayBatting, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvAwayPitchers, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' dgvHomeBatting
        ' 
        dgvHomeBatting.AllowUserToAddRows = False
        dgvHomeBatting.AllowUserToDeleteRows = False
        dgvHomeBatting.AllowUserToResizeColumns = False
        dgvHomeBatting.AllowUserToResizeRows = False
        dgvHomeBatting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvHomeBatting.BackgroundColor = SystemColors.Control
        dgvHomeBatting.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvHomeBatting.Location = New Point(703, 121)
        dgvHomeBatting.Name = "dgvHomeBatting"
        dgvHomeBatting.RowHeadersVisible = False
        dgvHomeBatting.RowHeadersWidth = 51
        dgvHomeBatting.RowTemplate.Height = 29
        dgvHomeBatting.Size = New Size(600, 380)
        dgvHomeBatting.TabIndex = 1
        ' 
        ' rtbHomeBattingFieldingDetails
        ' 
        rtbHomeBattingFieldingDetails.BorderStyle = BorderStyle.FixedSingle
        rtbHomeBattingFieldingDetails.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        rtbHomeBattingFieldingDetails.Location = New Point(701, 506)
        rtbHomeBattingFieldingDetails.Name = "rtbHomeBattingFieldingDetails"
        rtbHomeBattingFieldingDetails.Size = New Size(601, 114)
        rtbHomeBattingFieldingDetails.TabIndex = 3
        rtbHomeBattingFieldingDetails.Text = ""
        ' 
        ' dgvHomePitchers
        ' 
        dgvHomePitchers.AllowUserToAddRows = False
        dgvHomePitchers.AllowUserToDeleteRows = False
        dgvHomePitchers.AllowUserToResizeColumns = False
        dgvHomePitchers.AllowUserToResizeRows = False
        dgvHomePitchers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvHomePitchers.BackgroundColor = SystemColors.Control
        dgvHomePitchers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvHomePitchers.Location = New Point(701, 672)
        dgvHomePitchers.Name = "dgvHomePitchers"
        dgvHomePitchers.RowHeadersVisible = False
        dgvHomePitchers.RowHeadersWidth = 51
        dgvHomePitchers.RowTemplate.Height = 29
        dgvHomePitchers.Size = New Size(600, 179)
        dgvHomePitchers.TabIndex = 5
        ' 
        ' rtbGameInfo
        ' 
        rtbGameInfo.Location = New Point(8, 914)
        rtbGameInfo.Name = "rtbGameInfo"
        rtbGameInfo.Size = New Size(1293, 114)
        rtbGameInfo.TabIndex = 6
        rtbGameInfo.Text = ""
        ' 
        ' lblHomeBatters
        ' 
        lblHomeBatters.AutoSize = True
        lblHomeBatters.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        lblHomeBatters.Location = New Point(701, 87)
        lblHomeBatters.Name = "lblHomeBatters"
        lblHomeBatters.Size = New Size(97, 16)
        lblHomeBatters.TabIndex = 8
        lblHomeBatters.Text = "HomeBatters"
        ' 
        ' lblHomePitchers
        ' 
        lblHomePitchers.AutoSize = True
        lblHomePitchers.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        lblHomePitchers.Location = New Point(701, 639)
        lblHomePitchers.Name = "lblHomePitchers"
        lblHomePitchers.Size = New Size(104, 16)
        lblHomePitchers.TabIndex = 10
        lblHomePitchers.Text = "HomePitchers"
        ' 
        ' lblGameInfo
        ' 
        lblGameInfo.AutoSize = True
        lblGameInfo.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        lblGameInfo.Location = New Point(12, 883)
        lblGameInfo.Name = "lblGameInfo"
        lblGameInfo.Size = New Size(77, 16)
        lblGameInfo.TabIndex = 11
        lblGameInfo.Text = "Game Info"
        ' 
        ' lblTitle
        ' 
        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point)
        lblTitle.ForeColor = SystemColors.ControlText
        lblTitle.Location = New Point(12, 45)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(86, 20)
        lblTitle.TabIndex = 13
        lblTitle.Text = "Game Title"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point)
        Label1.Location = New Point(12, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(83, 20)
        Label1.TabIndex = 15
        Label1.Text = "Boxscore"
        ' 
        ' rtbAwayBattingFieldingDetails
        ' 
        rtbAwayBattingFieldingDetails.BorderStyle = BorderStyle.FixedSingle
        rtbAwayBattingFieldingDetails.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        rtbAwayBattingFieldingDetails.Location = New Point(12, 506)
        rtbAwayBattingFieldingDetails.Name = "rtbAwayBattingFieldingDetails"
        rtbAwayBattingFieldingDetails.Size = New Size(650, 114)
        rtbAwayBattingFieldingDetails.TabIndex = 2
        rtbAwayBattingFieldingDetails.Text = ""
        ' 
        ' lblAwayPitchers
        ' 
        lblAwayPitchers.AutoSize = True
        lblAwayPitchers.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        lblAwayPitchers.Location = New Point(12, 639)
        lblAwayPitchers.Name = "lblAwayPitchers"
        lblAwayPitchers.Size = New Size(100, 16)
        lblAwayPitchers.TabIndex = 9
        lblAwayPitchers.Text = "AwayPitchers"
        ' 
        ' lblAwayBatters
        ' 
        lblAwayBatters.AutoSize = True
        lblAwayBatters.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        lblAwayBatters.Location = New Point(12, 87)
        lblAwayBatters.Name = "lblAwayBatters"
        lblAwayBatters.Size = New Size(93, 16)
        lblAwayBatters.TabIndex = 7
        lblAwayBatters.Text = "AwayBatters"
        ' 
        ' dgvAwayBatting
        ' 
        dgvAwayBatting.AllowUserToAddRows = False
        dgvAwayBatting.AllowUserToDeleteRows = False
        dgvAwayBatting.AllowUserToOrderColumns = True
        dgvAwayBatting.AllowUserToResizeColumns = False
        dgvAwayBatting.AllowUserToResizeRows = False
        dgvAwayBatting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvAwayBatting.BackgroundColor = SystemColors.Control
        dgvAwayBatting.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvAwayBatting.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvAwayBatting.EditMode = DataGridViewEditMode.EditProgrammatically
        dgvAwayBatting.Location = New Point(12, 119)
        dgvAwayBatting.MultiSelect = False
        dgvAwayBatting.Name = "dgvAwayBatting"
        dgvAwayBatting.ReadOnly = True
        dgvAwayBatting.RowHeadersVisible = False
        dgvAwayBatting.RowHeadersWidth = 51
        dgvAwayBatting.RowTemplate.Height = 29
        dgvAwayBatting.ShowCellErrors = False
        dgvAwayBatting.ShowCellToolTips = False
        dgvAwayBatting.ShowEditingIcon = False
        dgvAwayBatting.ShowRowErrors = False
        dgvAwayBatting.Size = New Size(650, 383)
        dgvAwayBatting.TabIndex = 0
        ' 
        ' dgvAwayPitchers
        ' 
        dgvAwayPitchers.AllowUserToAddRows = False
        dgvAwayPitchers.AllowUserToDeleteRows = False
        dgvAwayPitchers.AllowUserToResizeColumns = False
        dgvAwayPitchers.AllowUserToResizeRows = False
        dgvAwayPitchers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvAwayPitchers.BackgroundColor = SystemColors.Control
        dgvAwayPitchers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvAwayPitchers.Location = New Point(12, 672)
        dgvAwayPitchers.Name = "dgvAwayPitchers"
        dgvAwayPitchers.RowHeadersVisible = False
        dgvAwayPitchers.RowHeadersWidth = 51
        dgvAwayPitchers.RowTemplate.Height = 29
        dgvAwayPitchers.Size = New Size(650, 179)
        dgvAwayPitchers.TabIndex = 4
        ' 
        ' MlbBoxscore
        ' 
        AutoScaleDimensions = New SizeF(8F, 19F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.Control
        ClientSize = New Size(1334, 1036)
        Controls.Add(dgvAwayPitchers)
        Controls.Add(lblAwayPitchers)
        Controls.Add(rtbHomeBattingFieldingDetails)
        Controls.Add(dgvAwayBatting)
        Controls.Add(rtbAwayBattingFieldingDetails)
        Controls.Add(lblAwayBatters)
        Controls.Add(lblHomePitchers)
        Controls.Add(dgvHomeBatting)
        Controls.Add(Label1)
        Controls.Add(dgvHomePitchers)
        Controls.Add(lblTitle)
        Controls.Add(lblHomeBatters)
        Controls.Add(lblGameInfo)
        Controls.Add(rtbGameInfo)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Name = "MlbBoxscore"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Boxscore"
        CType(dgvHomeBatting, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvHomePitchers, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvAwayBatting, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvAwayPitchers, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

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
