using System.Security.Cryptography.X509Certificates;

namespace Tic_Tac_Toe
{
    class Game
    {
        private List<Player> players;
        private int currentPlayerIndex;
        private bool gameOver;
        private int moveCount;
        private bool exitGame = false;
        public Game(List<Player> players)
        {
            this.players = players;
            currentPlayerIndex = 0;
            gameOver = false;
            moveCount = 0;
        }
        
        public void MenuGame(bool isRestart = false)
        {
            do
            { 
                Console.Clear();
                Console.WriteLine("+=============================+");
                Console.WriteLine("| Welcome to Tic-Tac-Toe Game |");
                Console.WriteLine("+=============================+");
                if (!isRestart)
                    Console.WriteLine("1 => Start");
                else
                    Console.WriteLine("1 => Restart");
                Console.WriteLine("2 => Exit");
                Console.WriteLine("3 => See Score");

                int.TryParse(Console.ReadLine(), out var value);
                switch (value)
                {
                    case 1:
                        if (!isRestart)
                        {
                            StartGame();
                            Console.Clear();
                        }
                        else
                        {
                            RestartGame();
                            Console.Clear();
                        }

                        break;
                    case 2:
                        exitGame = true;
                        Console.WriteLine($"Exiting...");
                        break;
                    case 3:
                        SeeScore();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine($"Invalid Value: {value}!");
                        break;

                }
            } while (!exitGame);

        }

        
        public void SeeScore()
        {
            Console.WriteLine();
            foreach(var player in players)
            {
                Console.WriteLine($"{player.Name} has {player.Vitories} vitories !");
            }
            Console.WriteLine();
        }


        public void PlayGame()
        {
            char[,] board =
            {
                { '1', '2', '3' },
                { '4', '5', '6' },
                { '7', '8', '9' }
            };

            Console.WriteLine("Player 1 will be 'X' symbol.");
            Console.WriteLine("Player 2 will be 'O' symbol.");

            do
            {
                gameOver = PlayingGame(board);
            }
            while (!gameOver);

            Console.WriteLine();

            MenuGame(true);
        }

        public void StartGame()
        {
            PlayGame();
        }

        public void RestartGame()
        {
            PlayGame();
        }

        private bool PlayingGame(char[,] board)
        {
            var retornos = (false, board);


            Player currentPlayer = players[currentPlayerIndex];

            Console.WriteLine();

            Console.WriteLine($"\n{currentPlayer.Name}'s turn.");
            Console.WriteLine("Select the location where you want to mark down: ");
            PrintBoard(board);

            Console.WriteLine();
            int.TryParse(Console.ReadLine(), out var markedLocation);
            if (markedLocation > 0 && markedLocation < 10)
            {

                retornos = MarkingLocation(board, markedLocation, currentPlayer);
                board = retornos.Item2;
                PrintBoard(board);
            }
            else
            {
                Console.WriteLine("Invalid Value!");
            }



            return retornos.Item1;
        }

        private (bool, char[,]) MarkingLocation(char[,] board, int markedLocation, Player currentPlayer)
        {
            bool marked = false;
            var isWin= (false, board);
            char markedLocationConverted = (char)('0' + markedLocation);
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == markedLocationConverted)
                    {
                        board[i, j] = currentPlayer.Symbol;
                        moveCount++;

                        isWin = ConfirmationWin(board, i, j, currentPlayer.Symbol, currentPlayer);
                        marked = true;
                    }

                }
            }
            if (marked)
                NextPlayer();
            else
                Console.WriteLine("This placed is already marked!");

            return isWin;
        }

        private void PrintBoard(char[,] board)
        {
            Console.WriteLine();
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"{board[i, 0]} | {board[i, 1]} | {board[i, 2]}");

                if (i < 2)
                {
                    Console.WriteLine("\n---------");
                }
            }

        }

        private void NextPlayer()
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        }

        private (bool, char[,]) ConfirmationWin(char[,] board, int row, int col, char symbol, Player player)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                if (board[row, i] != symbol)
                {
                    break;
                }
                if (i == board.GetLength(0) - 1)
                {
                    Console.WriteLine($"{player.Name} win!");
                    player.Vitories++;
                    return (true, board);
                }
            }

            for (int i = 0; i < board.GetLength(0); i++)
            {
                if(board[i, col] != symbol)
                {
                    break;
                }
                if (i == board.GetLength(0) - 1)
                {
                    Console.WriteLine($"{player.Name} win!");
                    player.Vitories++;
                    return (true, board);
                }
            }

            if (row == col)
            {
                for(int i = 0; i < board.GetLength(0); i++)
                {
                    if (board[i, i] != symbol)
                    {
                        break;
                    }
                    if(i == board.GetLength(0) - 1)
                    {
                        Console.WriteLine($"{player.Name}  win!");
                        player.Vitories++;
                        return (true, board);
                    }
                }
            }

            if(row + col == board.GetLength(0) - 1)
            {
                for(int i = 0; i < board.GetLength(0); i++)
                {
                    if (board[i, (board.GetLength(0) - 1) - i] != symbol)
                    {
                        break;
                    }
                    if (i == board.GetLength(0) - 1)
                    {
                        Console.WriteLine($"{player.Name}  win!");
                        player.Vitories++;
                        return (true, board);
                    }
                }
            }


            if (moveCount == (int)(Math.Pow(board.GetLength(0), 2) - 1))
            {
               Console.WriteLine("It's a draw!");
               return (true, board);
            }
            return (false, board);
        }
    }
}
