using System;
using System.Linq;

class Program
{

    //Checks for correct turns taken and then checks for invalid input
    //Returns true if checks passed
    //Returns false if either check not passed
    private static bool IsGameLegal(int gameTurns, int noughtTurns, int crossTurns, char[] board)
    {

        bool result = false;
        if (gameTurns <= 9 && (crossTurns == noughtTurns || crossTurns == noughtTurns + 1))
        {
            result = true;
        }
        foreach (char character in board)
        {
            if(character != 'X' && character != 'O' && character != '_' )
            {
                result = false;
            }
        }
        return result;
    }

    //Cretaes a multidimensional array of win patterns
    //Populates win patterns with characters from board
    //Returns win patterns
    private static char[][] CreateWinPatterns(char[] board)
    {

        char[] win_pattern_123;
        char[] win_pattern_456;
        char[] win_pattern_789;
        char[] win_pattern_147;
        char[] win_pattern_258;
        char[] win_pattern_369;
        char[] win_pattern_159;
        char[] win_pattern_357;

        char[][] win_patterns = {
            win_pattern_123 = new char[3],
            win_pattern_456 = new char[3],
            win_pattern_789 = new char[3],
            win_pattern_147 = new char[3],
            win_pattern_258 = new char[3],
            win_pattern_369 = new char[3],
            win_pattern_159 = new char[3],
            win_pattern_357 = new char[3]
        };



        if (board[0] == 'X' || board[0] == 'O')
        {
            win_pattern_123[0] = board[0];
            win_pattern_147[0] = board[0];
            win_pattern_159[0] = board[0];
        }

        if (board[1] == 'X' || board[1] == 'O')
        {
            win_pattern_123[1] = board[1];
            win_pattern_258[0] = board[1];
        }
        if (board[2] == 'X' || board[2] == 'O')
        {
            win_pattern_123[2] = board[2];
            win_pattern_369[0] = board[2];
            win_pattern_357[0] = board[2];
        }
        if (board[3] == 'X' || board[3] == 'O')
        {
            win_pattern_456[0] = board[3];
            win_pattern_147[1] = board[3];
        }
        if (board[4] == 'X' || board[4] == 'O')
        {
            win_pattern_456[1] = board[4];
            win_pattern_258[1] = board[4];
            win_pattern_159[1] = board[4];
            win_pattern_357[1] = board[4];
        }
        if (board[5] == 'X' || board[5] == 'O')
        {
            win_pattern_456[2] = board[5];
            win_pattern_369[1] = board[5];
        }
        if (board[6] == 'X' || board[6] == 'O')
        {
            win_pattern_147[2] = board[6];
            win_pattern_789[0] = board[6];
            win_pattern_357[2] = board[6];
        }
        if (board[7] == 'X' || board[7] == 'O')
        {
            win_pattern_789[1] = board[7];
            win_pattern_258[2] = board[7];
        }
        if (board[8] == 'X' || board[8] == 'O')
        {
            win_pattern_789[2] = board[8];
            win_pattern_369[2] = board[8];
            win_pattern_159[2] = board[8];
        }
        return win_patterns;
    }

    private enum BoardState
    {
        // complete enum with all the possible states of a noughts and crosses board (there "s more than 3)
        NOUGHTS_WIN, CROSSES_WIN, DRAW, NOUGHT_CANNOT_WIN, CROSS_CANNOT_WIN, OPEN_GAME, ILLEGAL_GAME
    }

    //Checks the total number of turns taken in each game
    //Returns number of turns
    private static int GetTurnNumber(char[] board, char XorO)
    {
        int turnNumber = 0;
        foreach (char character in board)
        {
            if (character == XorO)
            {
                turnNumber++;
            }
        }
        return turnNumber;
    }

    //Checks for a winner
    //Loops over each win pattern
    //Compares to either an array of XXX or OOO
    //If any patterns match, returns true
    //If no patterns match, returns false
    private static bool CheckWin(char[][] winPatterns, char[] winArray)
    {
        bool isAWinner = false;
        foreach (char[] pattern in winPatterns)
        {

            if (pattern.SequenceEqual(winArray))
            {

                isAWinner = true;
                return isAWinner;
            }
        }
        return isAWinner;
    }

    //Checks if game is a draw
    //If game is finished and neither Nought or Cross have won, returns true
    //Else returns false
    private static bool IsDraw(int turnTotal, bool checkCrossWin, bool checkNoughtWin)
    {
        bool result = false;
        if (turnTotal == 9 && checkCrossWin == false && checkNoughtWin == false)
        {
            result = true;
        }
        return result;
    }


    //Checks if game outcome is win or draw for noughts or crosses
    //Loops through win patterns to see if character is present in each pattern
    //Returns true if character is present in all win patterns
    private static bool CantWinCheck(char[][] winPatterns, char noughtOrCross)
    {
        int count = 0;
        bool result = false;
        foreach (char[] pattern in winPatterns)
        {
            if (Array.Exists(pattern, element => element == noughtOrCross))
            {
                count++;
            }
        }

        if (count == 8)
        {
            result = true;
        }
        return result;
    }



    private static BoardState GetStateOfBoard(string board)
    {
        //Arrays to match with Win Patterns
        char[] noughtsWinPattern = { 'O', 'O', 'O' };
        char[] cossesWinPattern = { 'X', 'X', 'X' };

        //Set board to array of characters
        char[] newBoard = board.ToCharArray();

        //Nought turns taken
        int noughtTurnNumber = GetTurnNumber(newBoard, 'O');

        //Cross turns taken
        int crossTurnNumber = GetTurnNumber(newBoard, 'X');

        //Total turns taken
        int totalTurns = noughtTurnNumber + crossTurnNumber;

        //Bool for deciding if game is legal
        bool legalGame = IsGameLegal(totalTurns, noughtTurnNumber, crossTurnNumber, newBoard);

        //Create win patterns for board
        char[][] winPatterns = CreateWinPatterns(newBoard);

        //Bool for checking if Noughts have won
        bool checkNoughtsWin = CheckWin(winPatterns, noughtsWinPattern);

        //Bool for checking if Crosses have won
        bool checkCrossesWin = CheckWin(winPatterns, cossesWinPattern);

        //Bool to check if finished game is a draw
        bool checkDraw = IsDraw(totalTurns, checkCrossesWin, checkNoughtsWin);

        //Bool to check if it is not possible for Noughts to win
        bool noughtCantWin = CantWinCheck(winPatterns, 'X');

        //Bool to check if it is not possible for Crosses to win
        bool crossCantWin = CantWinCheck(winPatterns, 'O');


        if (legalGame)
        {
            if (checkNoughtsWin)
            {
                return BoardState.NOUGHTS_WIN;
            }
            if (checkCrossesWin)
            {
                return BoardState.CROSSES_WIN;
            }
            if (checkDraw)
            {
                return BoardState.DRAW;
            }
            if (noughtCantWin)
            {
                return BoardState.NOUGHT_CANNOT_WIN;
            }
            if (crossCantWin)
            {
                return BoardState.CROSS_CANNOT_WIN;
            }
            if (totalTurns <= 6 || (totalTurns <= 6 && !checkCrossesWin && !checkNoughtsWin))
            {
                return BoardState.OPEN_GAME;
            }
        }
            return BoardState.ILLEGAL_GAME;
    }

    static void Main(string[] args)
    {
        // leave this main method unchanged
        for (int i = 0; i < args.Length; i++)
        {
            System.Console.WriteLine(GetStateOfBoard(args[i]));
        }
    }
}
