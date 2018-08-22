Public Class Form1

    Dim timer As Stopwatch
    Dim backBuffer As Image
    Dim graphics As Graphics
    Dim clientWidth As Integer
    Dim clientHeight As Integer
    Dim interval As Long
    Dim startTick As Long
    Dim direction As Point
    Shared queue As New List(Of Rectangle)
    Dim currentScore As Integer
    Dim highScore As Integer = 0
    'Dim counter As Integer = 0
    Dim gnam As New Rectangle


    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        GameLoop()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        Me.MaximizeBox = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
        timer = New Stopwatch()
        clientWidth = 300
        clientHeight = 300
        interval = 80
        Me.ClientSize = New Size(clientWidth, clientHeight)
        backBuffer = New Bitmap(clientWidth, clientHeight)
        graphics = graphics.FromImage(backBuffer)

        Dim init As New Rectangle(50, 100, 10, 10)
        Dim tmp As New Rectangle(init.X - 10, init.Y, 10, 10)
        Dim tmp2 As New Rectangle(init.X - 20, init.Y, 10, 10)
        Dim tmp3 As New Rectangle(init.X - 30, init.Y, 10, 10)
        Dim tmp4 As New Rectangle(init.X - 40, init.Y, 10, 10)
        queue.Add(init)
        queue.Add(tmp)
        queue.Add(tmp2)
        queue.Add(tmp3)
        queue.Add(tmp4)

        direction.X = 10
        direction.Y = 0

        gnam.X = 50
        gnam.Y = 50
        gnam.Height = 10
        gnam.Width = 10

    End Sub

    Private Sub GameLoop()

        timer.Start()
        Do While (Me.Created)
            startTick = timer.ElapsedMilliseconds
            GameLogic()
            RenderScene()
            Application.DoEvents()
            Do While timer.ElapsedMilliseconds - startTick < interval

            Loop

            'counter += 1
        Loop
    End Sub

    Private Sub GameLogic()
        Dim tmp As New Rectangle(queue(0).X, queue(0).Y, 10, 10)

        tmp.X += direction.X
        tmp.Y += direction.Y

        If tmp.X < 0 Then
            tmp.X = clientWidth - 10
        End If

        If tmp.Y < 0 Then
            tmp.Y = clientHeight - 10
        End If

        If tmp.X >= clientWidth Then
            tmp.X = 0
        End If

        If tmp.Y >= clientHeight Then
            tmp.Y = 0
        End If

        queue(0) = tmp

        For i = queue.Count - 1 To 1 Step -1
            Dim tmpp As New Rectangle(queue(i - 1).X, queue(i - 1).Y, 10, 10)
            queue(i) = tmpp
        Next

        For i = queue.Count - 1 To 2 Step -1
            If (queue(0).X = queue(i).X And queue(0).Y = queue(i).Y And queue.Count > 2) Then
                System.Threading.Thread.Sleep(500)
                For j = queue.Count - 1 To i Step -1
                    queue.RemoveAt(j)
                Next
            End If
        Next

        'If counter = 2 Then
        '    Dim tmppp As New Rectangle(queue(queue.Count - 1).X, queue(queue.Count - 1).Y, 10, 10)
        '    queue.Add(tmppp)
        '    counter = 0
        'End If

        If (queue(0).X = gnam.X And queue(0).Y = gnam.Y) Then
            Dim tmppp As New Rectangle(queue(queue.Count - 1).X, queue(queue.Count - 1).Y, 10, 10)
            queue.Add(tmppp)
            get_num()
        End If

    End Sub

    Private Sub RenderScene()
        backBuffer = New Bitmap(clientWidth, clientHeight)
        graphics = graphics.FromImage(backBuffer)
        pbSurface.Image = Nothing
        For i = 0 To queue.Count - 1
            graphics.FillRectangle(Brushes.Blue(), queue(i))
        Next

        currentScore = (queue.Count - 1)
        If highScore < currentScore Then
            highScore = currentScore
        End If

        Dim myFont As Font = New Font("Arial", 10, GraphicsUnit.Point)
        Dim score1 As String = "Current score: " & currentScore.ToString
        Dim score2 As String = "High score: " & highScore.ToString
        graphics.DrawString(score1, myFont, Brushes.Red, 170, 0)
        graphics.DrawString(score2, myFont, Brushes.Yellow, 170, 13)

        pbSurface.Image = backBuffer
        graphics.FillRectangle(Brushes.White, gnam)



    End Sub

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Select Case e.KeyCode
            Case Keys.Up
                If direction.Y <> 10 Then
                    direction.X = 0
                    direction.Y = -10
                End If

            Case Keys.Down
                If direction.Y <> -10 Then
                    direction.X = 0
                    direction.Y = 10
                End If

            Case Keys.Left
                If direction.X <> 10 Then
                    direction.X = -10
                    direction.Y = 0
                End If


            Case Keys.Right
                If direction.X <> -10 Then
                    direction.X = 10
                    direction.Y = 0
                End If


            Case Keys.X
                If interval > 20 Then
                    interval -= 20
                End If

            Case Keys.Y
                interval += 20

            Case Keys.C
                Dim tmp As New Rectangle(queue(queue.Count - 1).X, queue(queue.Count - 1).Y , 10, 10)
                queue.Add(tmp)

            Case Keys.V
                If queue.Count > 1 Then
                    queue.RemoveAt(queue.Count - 1)
                End If

            Case Keys.Space
                Dim tmp As New Rectangle(queue(0).X, queue(0).Y, 10, 10)
                queue.Clear()
                queue.Add(tmp)

            Case Keys.R
                timer.Restart()

        End Select

    End Sub

    Private Sub get_num()
        Randomize()
        gnam.X = CInt(Math.Ceiling(Rnd() * 29)) * 10 + 10
        gnam.Y = CInt(Math.Ceiling(Rnd() * 29)) * 10 + 10

        For i = 0 To queue.Count - 1
            If (gnam.X < 10 Or gnam.X > 290 Or gnam.Y < 10 Or gnam.Y > 290 Or (gnam.X = queue(i).X And gnam.Y = queue(i).Y)) Then
                get_num()
            End If
        Next

    End Sub

End Class
