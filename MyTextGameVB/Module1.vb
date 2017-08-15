Module Module1
    Class Character
        Public hp As Integer
        Public mp As Integer
        Public name As String
        Public minAtk As Integer = 1
        Public maxAtk As Integer
        Public dmg As Integer
        Public armor As Integer
    End Class

    Class Monster
        Inherits Character
    End Class
    Class Paladin
        Inherits Character
        Public Sub New()
            hp = 70
            maxAtk = 7
            armor = 5
            mp = 10
        End Sub
    End Class

    Class Mage
        Inherits Character
        Public Sub New()
            hp = 30
            minAtk = 5
            maxAtk = 11
            armor = 0
            mp = 20
        End Sub
    End Class

    Class Warrior
        Inherits Character
        Public Sub New()
            hp = 50
            minAtk = 3
            maxAtk = 9
            armor = 3
            mp = 0
        End Sub
    End Class

    Class Battle
        Public p1 As Character
        Public m1 As Monster

        Public Function doBattle() As Boolean
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine("A " & m1.name & " appears before you with " & m1.hp & " health and " & m1.maxAtk & " attack")
            Console.ForegroundColor = ConsoleColor.Gray
            System.Threading.Thread.Sleep(1000)
            Console.WriteLine("What do you do? Attack or run?")
            System.Threading.Thread.Sleep(1000)

            While p1.hp > 0 AndAlso m1.hp > 0

                'secondary setup for
                'randomizing both attacks
                Dim r As New Random()
                p1.dmg = r.[Next](p1.minAtk, p1.maxAtk)
                m1.dmg = r.[Next](m1.minAtk, m1.maxAtk)

                'taking commands
                Dim input As String = Console.ReadLine().ToUpper()
                Select Case input
                    Case "ATTACK", "ATK"
                        Console.WriteLine("You attack " & m1.name & " for " & p1.dmg & " damage")
                        m1.hp -= p1.dmg
                        Console.ForegroundColor = ConsoleColor.Green
                        'Erase comment mark to show monster health
                        'Console.WriteLine("It now has " + m1.hp + " health left");
                        Console.ForegroundColor = ConsoleColor.Gray
                        Exit Select

                    Case "RUN"
                        Console.WriteLine("You attempt to flee but you are behind bars, so you run in a circle")
                        Exit Select

                    Case Else
                        Console.WriteLine("Unknown command, the monster gets a free hit! Try again.")
                        Exit Select

                End Select
                'checking if the monster is still alive, without this the monster will
                'deal a final blow with negative health
                If m1.hp > 0 Then
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.WriteLine("The " & m1.name & " attacks you for " & m1.dmg & " damage")
                    'double nested if statement checks if you're a mage to do armor calculations
                    'partially no longer works with battle class system
                    'but still can calculate armor no matter what
                    'original line as follows
                    'if (classInput!= "MAGE")
                    If p1.name <> "MAGE" Then
                        Console.WriteLine("Your worn armor saves you from " & p1.armor & " damage")
                        'this triple nested if statement prevents an enemy from dealing negative
                        'damage because of armor and healing the character (HOPEFULLY)
                        If m1.dmg > p1.armor Then
                            p1.hp -= (m1.dmg - p1.armor)
                        Else
                            Console.ForegroundColor = ConsoleColor.Yellow
                            Console.WriteLine("The attack bounces off your armor")
                        End If
                    Else
                        p1.hp -= m1.dmg
                    End If

                    Console.ForegroundColor = ConsoleColor.Green
                    Console.WriteLine("You now have " & p1.hp & " health left " & vbLf)
                    'Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Gray
                Else
                    Exit While

                End If
            End While

            If m1.hp <= 0 And p1.hp > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

    End Class

    Sub Main()
        Console.Title = "The Bane Of Klothor"
        Dim c As New Character()
        'making first monster, a large rat
        'set some stats
        Dim m As New Monster()
        m.hp = 30
        m.minAtk = 1
        m.maxAtk = 10
        m.name = "Large Rat"

        'appearance of first NPC and player name entry

        Console.WriteLine("You awake in a strange room in the dark." & vbLf & "You hear a rough voice ask, 'Stranger, what is it that you do?'")
        System.Threading.Thread.Sleep(2000)
        Console.WriteLine("You look all around you but see no one, only bars of iron" & vbLf)
        System.Threading.Thread.Sleep(2000)

        Dim validClass As Boolean = False
        Dim classInput As String
        Dim classInOriginal As String
        'class select loop
        'cannot be skipped
        Do
            Console.WriteLine("Pick a class: paladin, warrior, mage")

            classInOriginal = Console.ReadLine()
            classInput = classInOriginal.ToUpper()
            Select Case classInput
                Case "PALADIN"
                    c = New Paladin()
                    validClass = True
                    Exit Select
                Case "WARRIOR"
                    c = New Warrior()
                    validClass = True
                    Exit Select
                Case "MAGE"
                    c = New Mage()
                    validClass = True
                    Exit Select
                Case Else
                    c = New Character()
                    validClass = False
                    Console.WriteLine("'I'm sorry, you were a what now?', queries the voice, bewildered.")
                    Console.WriteLine("(Invalid Class. Try Again!)")
                    Exit Select

            End Select
        Loop While validClass = False

        'figured out a clever way to utilize the to upper function on chosen class, thought the solution was funny
        'but also figured out how to keep and use the original case, but kept the to upper plot utilization for the development of the npc character

        Console.WriteLine("'BUT OF COURSE... YOU WERE A {0}!', exclaims the voice.", classInput)
        'epic pauses to harness the true awkward silence
        System.Threading.Thread.Sleep(1000)
        Console.WriteLine("'SHUT UP! OR I'LL BEAT YOU TO SLEEP!', you hear a voice shout in the distance")
        System.Threading.Thread.Sleep(1000)
        Console.WriteLine("'...'")
        System.Threading.Thread.Sleep(1000)
        Console.WriteLine("'I apologize, I... ")
        System.Threading.Thread.Sleep(1000)
        Console.WriteLine("'I have random outbursts when I get excited'")
        System.Threading.Thread.Sleep(1000)
        Console.WriteLine("'Now what did you say your name was, young {0}?'", classInOriginal)

        c.name = Console.ReadLine()
        Console.WriteLine("{0}? What a terrible name!", c.name)
        System.Threading.Thread.Sleep(1000)
        Console.WriteLine("{0} the {1}? No that doesn't have a good ring to it... How did you end up in this prison?", c.name, classInOriginal)
        Console.ReadLine()
        Console.WriteLine("'Oh wow really? That's interesting...'")
        System.Threading.Thread.Sleep(1000)
        Console.WriteLine("'Just kidding I don't care.'")
        System.Threading.Thread.Sleep(1000)
        Console.WriteLine("'Shhhh be quiet {0}!', whispers the voice, 'Is that a rat?! Kill it before you catch a disease!'", c.name)

        'making the first battle, setting characters and monsters for the fight
        Dim b As New Battle()
        b.p1 = c
        b.m1 = m
        Dim did_p1_win As Boolean = b.doBattle()

        If did_p1_win = True Then
            Console.WriteLine("YOU WIN")
            Console.WriteLine("'Very good me lad, {0}, you were named poorly but be quite a scrappy {1}!'", c.name, classInOriginal)
            'making the second monster
            Dim g As New Monster()
            Dim b1 As New Battle()
            'setting new monsters stats
            b1.p1 = c
            b1.m1 = g
            g.hp = 50
            g.minAtk = 2
            g.maxAtk = 21
            g.name = "Sleepy Guard"
            'introducing sleepy guard
            Console.WriteLine("You see a light in the distance grow, and you watch an angry guard with a torch approach you")
            Console.WriteLine("'Who keeps shouting while I'm trying to sleep?!'")

            did_p1_win = b1.doBattle()
            If did_p1_win = True Then
                Console.WriteLine("GOOD JOB YOU KILLED THAT GUARD DUDE!" & vbLf & "'Now let's escape'" & vbLf & "TO BE CONTINUED...")
            Else
                Console.WriteLine("You died..." & vbLf & "'I'll spit on your grave', says the guard")
            End If
        Else
            Console.WriteLine("YOU LOSE")
        End If

        Console.ReadKey()
    End Sub

End Module