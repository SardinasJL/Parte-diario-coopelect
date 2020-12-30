Public Class frmInicio
    Private Sub ActividadesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActividadesToolStripMenuItem.Click
        Dim frmAct As New frmActividades
        frmAct.MdiParent = Me
        frmAct.Show()
    End Sub
    Private Sub frmInicio_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        End
    End Sub
    Private Sub ProgramaciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProgramaciónToolStripMenuItem.Click
        Dim frmPro As New frmProgramacion
        frmPro.MdiParent = Me
        frmPro.Show()
    End Sub
    Private Sub EjecuciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EjecuciónToolStripMenuItem.Click
        Dim frmEje As New frmEjecucion
        frmEje.MdiParent = Me
        frmEje.Show()
    End Sub

    Private Sub SalirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalirToolStripMenuItem.Click
        End
    End Sub

End Class