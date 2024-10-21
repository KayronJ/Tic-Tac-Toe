

using Tic_Tac_Toe;



class TicTacToe
{
    static void Main(string[] args)
    {
        List<Player> players = new List<Player>
        {
            new Player("Player 1", 'X'),
            new Player("Player 2", 'O')
        };

        Game game = new Game(players);

        game.MenuGame(false);

    }

    
}