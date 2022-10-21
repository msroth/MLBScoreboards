<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MlbBroadcasters
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
        Me.dgvBroadcasters = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblGameTitle = New System.Windows.Forms.Label()
        CType(Me.dgvBroadcasters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvBroadcasters
        '
        Me.dgvBroadcasters.AllowUserToAddRows = False
        Me.dgvBroadcasters.AllowUserToDeleteRows = False
        Me.dgvBroadcasters.AllowUserToResizeColumns = False
        Me.dgvBroadcasters.AllowUserToResizeRows = False
        Me.dgvBroadcasters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvBroadcasters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBroadcasters.Location = New System.Drawing.Point(12, 89)
        Me.dgvBroadcasters.MultiSelect = False
        Me.dgvBroadcasters.Name = "dgvBroadcasters"
        Me.dgvBroadcasters.ReadOnly = True
        Me.dgvBroadcasters.RowHeadersVisible = False
        Me.dgvBroadcasters.RowTemplate.Height = 25
        Me.dgvBroadcasters.Size = New System.Drawing.Size(517, 244)
        Me.dgvBroadcasters.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Label1.Location = New System.Drawing.Point(12, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(116, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Broadcasters"
        '
        'lblGameTitle
        '
        Me.lblGameTitle.AutoSize = True
        Me.lblGameTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblGameTitle.Location = New System.Drawing.Point(12, 54)
        Me.lblGameTitle.Name = "lblGameTitle"
        Me.lblGameTitle.Size = New System.Drawing.Size(86, 20)
        Me.lblGameTitle.TabIndex = 2
        Me.lblGameTitle.Text = "Game Title"
        '
        'MlbBroadcasters
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(541, 360)
        Me.Controls.Add(Me.lblGameTitle)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgvBroadcasters)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "MlbBroadcasters"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Broadcasters"
        CType(Me.dgvBroadcasters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgvBroadcasters As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents lblGameTitle As Label
End Class
