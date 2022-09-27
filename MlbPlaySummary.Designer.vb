<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MlbPlaySummary
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgvPlays = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblGameTitle = New System.Windows.Forms.Label()
        Me.lblGamePk = New System.Windows.Forms.Label()
        CType(Me.dgvPlays, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvPlays
        '
        Me.dgvPlays.AllowUserToAddRows = False
        Me.dgvPlays.AllowUserToDeleteRows = False
        Me.dgvPlays.AllowUserToOrderColumns = True
        Me.dgvPlays.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvPlays.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgvPlays.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPlays.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgvPlays.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvPlays.Location = New System.Drawing.Point(22, 69)
        Me.dgvPlays.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvPlays.MultiSelect = False
        Me.dgvPlays.Name = "dgvPlays"
        Me.dgvPlays.ReadOnly = True
        Me.dgvPlays.RowHeadersVisible = False
        Me.dgvPlays.RowHeadersWidth = 51
        Me.dgvPlays.RowTemplate.Height = 29
        Me.dgvPlays.Size = New System.Drawing.Size(660, 450)
        Me.dgvPlays.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS Reference Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Label1.Location = New System.Drawing.Point(22, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Play Summary"
        '
        'lblGameTitle
        '
        Me.lblGameTitle.AutoSize = True
        Me.lblGameTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblGameTitle.Location = New System.Drawing.Point(22, 41)
        Me.lblGameTitle.Name = "lblGameTitle"
        Me.lblGameTitle.Size = New System.Drawing.Size(67, 15)
        Me.lblGameTitle.TabIndex = 2
        Me.lblGameTitle.Text = "Game Title"
        '
        'lblGamePk
        '
        Me.lblGamePk.AutoSize = True
        Me.lblGamePk.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblGamePk.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblGamePk.Location = New System.Drawing.Point(558, 41)
        Me.lblGamePk.Name = "lblGamePk"
        Me.lblGamePk.Size = New System.Drawing.Size(55, 15)
        Me.lblGamePk.TabIndex = 3
        Me.lblGamePk.Text = "GamePk"
        Me.lblGamePk.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MlbPlaySummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(700, 530)
        Me.Controls.Add(Me.lblGamePk)
        Me.Controls.Add(Me.lblGameTitle)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgvPlays)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "MlbPlaySummary"
        Me.Text = "PlaySummary"
        CType(Me.dgvPlays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgvPlays As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents lblGameTitle As Label
    Friend WithEvents lblGamePk As Label
End Class
