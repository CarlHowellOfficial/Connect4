/*Carl Ocsan
	395010
	SODV1202 (COOL)
	Connect4 Project
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct playerInfo
{
	public String playerName;
	public char playerID;
};

namespace Connect4
{
	class Program
	{
		static void Main(string[] args)
		{
			playerInfo playerOne = new playerInfo();
			playerInfo playerTwo = new playerInfo();
			char[,] board = new char[9, 10];
			int drop, win, max, again;

			//Get each players name and assign player1 as X and player2 as 0
			Console.WriteLine("GAME ON!");

			Console.WriteLine("Player1, enter your name: "); 
			playerOne.playerName = Console.ReadLine();
			playerOne.playerID = 'X';

			Console.WriteLine("Player2, enter your name: ");
			playerTwo.playerName = Console.ReadLine();
			playerTwo.playerID = 'O';

			max = 0; //identifies when the board is full 
			win = 0; 
			again = 0;

			//Using do and while loop
			DisplayBoard (board);
			do
			{
				drop = PlayerDrop (board, playerOne);
				CheckBellow (board, playerOne, drop);
				DisplayBoard (board);
				win = CheckFour (board, playerOne);
				if (win == 1)
				{
					PlayerWin(playerOne);
					again = restart(board);
					if (again == 2)
					{
						break;
					}
				}

				drop = PlayerDrop(board, playerTwo);
				CheckBellow(board, playerTwo, drop);
				DisplayBoard(board);
				win = CheckFour(board, playerTwo);
				if (win == 1)
				{
					PlayerWin(playerTwo);
					again = restart(board);
					if (again == 2)
					{
						break;
					}
				}
				max = FullBoard(board);
				if (max == 7)
				{
					Console.WriteLine("Draw!");
					again = restart(board);
				}
			} while (again != 2);
		}

		static int PlayerDrop(char[,] board, playerInfo activePlayer)
		{
			int drop;

			Console.WriteLine(activePlayer.playerName + "'s Turn ");
			do
			{
				Console.WriteLine("Please enter a number between 1 and 7: ");
				drop = Convert.ToInt32(Console.ReadLine());
			} while (drop < 1 || drop > 7);

			while (board[1, drop] == 'X' || board[1, drop] == 'O')
			{
				Console.WriteLine("That row is full, please enter a new row: ");
				drop = Convert.ToInt32(Console.ReadLine());
			}
			return drop;
		}

		static void CheckBellow(char[,] board, playerInfo activePlayer, int drop)
		{
			int length, turn;

			length = 6;
			turn = 0;

			do
			{
				if (board[length, drop] != 'X' && board[length, drop] != 'O')
				{
					board[length, drop] = activePlayer.playerID;
					turn = 1;
				}
				else
					--length;
			} while (turn != 1);
		}

		static void DisplayBoard(char[,] board)
		{
			int rows = 6, columns = 7, 
				i, x;

			for (i = 1; i <= rows; i++)
			{
				Console.Write("|");
				for (x = 1; x <= columns; x++)
				{
					if (board[i, x] != 'X' && board[i, x] != 'O')
						board[i, x] = '*';

					Console.Write(board[i, x]);

				}
				Console.Write("| \n");
			}
		}

		//Loop for possible winning patters of array
		static int CheckFour(char[,] board, playerInfo activePlayer)
		{
			char playersXO;
			int win;

			playersXO = activePlayer.playerID;
			win = 0;

			for (int i = 8; i >= 1; --i)
			{
				for (int x = 9; x >= 1; --x)
				{
					if (board[i, x] == playersXO &&
						board[i - 1, x - 1] == playersXO &&
						board[i - 2, x - 2] == playersXO &&
						board[i - 3, x - 3] == playersXO)
					{
						win = 1;
					}

					if (board[i, x] == playersXO &&
						board[i, x - 1] == playersXO &&
						board[i, x - 2] == playersXO &&
						board[i, x - 3] == playersXO)
					{
						win = 1;
					}

					if (board[i, x] == playersXO &&
						board[i - 1, x] == playersXO &&
						board[i - 2, x] == playersXO &&
						board[i - 3, x] == playersXO)
					{
						win = 1;
					}

					if (board[i, x] == playersXO &&
						board[i - 1, x + 1] == playersXO &&
						board[i - 2, x + 2] == playersXO &&
						board[i - 3, x + 3] == playersXO)
					{
						win = 1;
					}

					if (board[i, x] == playersXO &&
						 board[i, x + 1] == playersXO &&
						 board[i, x + 2] == playersXO &&
						 board[i, x + 3] == playersXO)
					{
						win = 1;
					}
				}
			}
			return win;
		}

		static int FullBoard(char[,] board)
		{
			int full;

			full = 0;

			for (int i = 1; i <= 7; ++i)
			{
				if (board[1, i] != '*')
					++full;
			}
			return full;
		}

		static void PlayerWin(playerInfo activePlayer)
		{
			Console.WriteLine(activePlayer.playerName + "Congratualions, You Win!");
		}

		//Loop for either restarting 
		static int restart(char[,] board)
		{
			int re;

			Console.WriteLine("Play Again? Yes(1) No(2): ");

			re = Convert.ToInt32(Console.ReadLine());
			if (re == 1)
			{
				for (int i = 1; i <= 6; i++)
				{
					for (int x = 1; x <= 7; x++)
					{
						board[i, x] = '*';
					}
				}
			}
			else
            {
				Console.WriteLine("Goodbye!");
			}
			return re;
		}
	}
}