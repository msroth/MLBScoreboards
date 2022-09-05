<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Configure
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
        Me.numSBRefreshTime = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbxTeams = New System.Windows.Forms.ComboBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.chxWriteFiles = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.numGameRefreshTime = New System.Windows.Forms.NumericUpDown()
        CType(Me.numSBRefreshTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numGameRefreshTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'numSBRefreshTime
        '
        Me.numSBRefreshTime.Location = New System.Drawing.Point(246, 29)
        Me.numSBRefreshTime.Maximum = New Decimal(New Integer() {300, 0, 0, 0})
        Me.numSBRefreshTime.Minimum = New Decimal(New Integer() {15, 0, 0, 0})
        Me.numSBRefreshTime.Name = "numSBRefreshTime"
        Me.numSBRefreshTime.Size = New System.Drawing.Size(74, 27)
        Me.numSBRefreshTime.TabIndex = 0
        Me.numSBRefreshTime.Value = New Decimal(New Integer() {30, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(40, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(198, 20)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Scoreboard Refresh Seconds"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(40, 113)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(101, 20)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Favorite Team"
        '
        'cbxTeams
        '
        Me.cbxTeams.FormattingEnabled = True
        Me.cbxTeams.Location = New System.Drawing.Point(246, 110)
        Me.cbxTeams.Name = "cbxTeams"
        Me.cbxTeams.Size = New System.Drawing.Size(238, 28)
        Me.cbxTeams.TabIndex = 5
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(663, 197)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(94, 29)
        Me.btnSave.TabIndex = 6
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(554, 197)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(94, 29)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'chxWriteFiles
        '
        Me.chxWriteFiles.AutoSize = True
        Me.chxWriteFiles.Location = New System.Drawing.Point(246, 156)
        Me.chxWriteFiles.Name = "chxWriteFiles"
        Me.chxWriteFiles.Size = New System.Drawing.Size(143, 24)
        Me.chxWriteFiles.TabIndex = 9
        Me.chxWriteFiles.Text = "Write Data Files?"
        Me.chxWriteFiles.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(40, 73)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(160, 20)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Game Refresh Seconds"
        '
        'numGameRefreshTime
        '
        Me.numGameRefreshTime.Location = New System.Drawing.Point(246, 70)
        Me.numGameRefreshTime.Maximum = New Decimal(New Integer() {300, 0, 0, 0})
        Me.numGameRefreshTime.Minimum = New Decimal(New Integer() {15, 0, 0, 0})
        Me.numGameRefreshTime.Name = "numGameRefreshTime"
        Me.numGameRefreshTime.Size = New System.Drawing.Size(74, 27)
        Me.numGameRefreshTime.TabIndex = 10
        Me.numGameRefreshTime.Value = New Decimal(New Integer() {20, 0, 0, 0})
        '
        'Configure
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 258)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.numGameRefreshTime)
        Me.Controls.Add(Me.chxWriteFiles)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.cbxTeams)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.numSBRefreshTime)
        Me.Name = "Configure"
        Me.Text = "Configure"
        CType(Me.numSBRefreshTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numGameRefreshTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents numSBRefreshTime As NumericUpDown
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cbxTeams As ComboBox
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents chxWriteFiles As CheckBox
    Friend WithEvents Label4 As Label
    Friend WithEvents numGameRefreshTime As NumericUpDown
End Class
