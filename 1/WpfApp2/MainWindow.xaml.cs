using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
namespace main
{
	public partial class MainWindow : Window
	{
		
	    Gamek BoardData;
		Button[,] BoardView;
		int chek = 1;
		public MainWindow()
		{
			InitializeComponent();
			StatusAndErrors.Text = "Ваш ход";
			BoardData = new Gamek();
			CreateMainNable();
			updtab();

			if (BoardData.Turn == 1)
				RobotPlay();
		}
		private void CreateMainNable()
		{
			BoardView = new Button[3, 3];
			BoardView[0, 0] = Place00;
			BoardView[0, 1] = Place01;
			BoardView[0, 2] = Place02;
			BoardView[1, 0] = Place10;
			BoardView[1, 1] = Place11;
			BoardView[1, 2] = Place12;
			BoardView[2, 0] = Place20;
			BoardView[2, 1] = Place21;
			BoardView[2, 2] = Place22;
		}
		private void updtab()
		{
			if (BoardData.Turn == 1 && chek == 1)
			{
				WhoIsNow.Text = "Вы - O";
			}
			else if (chek == 1) { WhoIsNow.Text = "Вы - X"; }
			for (int i = 0; i < BoardView.GetLength(0); i++)
				for (int j = 0; j < BoardView.GetLength(1); j++)
				{
					if (BoardData.polg[i, j] == 1)
					{
						BoardView[i, j].Content = "O";
					}
					else if (BoardData.polg[i, j] == -1)
					{
						BoardView[i, j].Content = "X";
					}
					else
					{
						BoardView[i, j].Content = "";
						BoardView[i, j].IsEnabled = true;
					}
				}
			chek = 0;
		}
		private void firstclick(object sender, RoutedEventArgs e)
		{
			try
			{
				StatusAndErrors.Text = "Играем";
				Button Selected = sender as Button;
				int i = -1, j = -1;
				bool found = false;
				for (i = 0; i < BoardView.GetLength(0) && !found; i++)
					for (j = 0; j < BoardView.GetLength(1) && !found; j++)
						if (Selected.Name == BoardView[i, j].Name) found = true;
				i--; j--;
				BoardData.Play(BoardData.Turn, i, j);
				updtab();

				RobotPlay();
			}
			catch (Exception ex)
			{
				if (ex.Message == "Конец игры, нажмите на новую игру" && BoardData.Turn == 1 && WhoIsNow.Text == "Вы - O")
				{ StatusAndErrors.Text = "Вы выйграли"; linwin(); }
				else if (ex.Message == "Конец игры, нажмите на новую игру") { StatusAndErrors.Text = "Вы проиграли"; linwin(); }
				if (ex.Message == "Ничья")
				{
					StatusAndErrors.Text = "Ничья";
				}
			}
		}
		private void RobotPlay()
		{
			try
			{
				BoardData.xodrob();
				updtab();
			}
			catch (Exception ex)
			{
				if (ex.Message == "Конец игры, нажмите на новую игру" && BoardData.Turn == 1 && WhoIsNow.Text == "Вы - O")
				{ StatusAndErrors.Text = "Вы выйграли"; linwin(); return; } 
				else if (ex.Message == "Конец игры, нажмите на новую игру") { StatusAndErrors.Text = "Вы проиграли"; linwin(); return; }
				if (ex.Message == "Ничья" )
				{
					StatusAndErrors.Text = "Ничья";
				}
				if (BoardData.St < 9)
					RobotPlay();
			}
		}
		private void linwin()
		{
			chek = 0;
			updtab();
			WinnerLineView.X1 = BoardData.Liniawin.X1;
			WinnerLineView.X2 = BoardData.Liniawin.X2;
			WinnerLineView.Y1 = BoardData.Liniawin.Y1;
			WinnerLineView.Y2 = BoardData.Liniawin.Y2;
			WinnerLineView.Visibility = Visibility.Visible;
		}
		private void newgame(object sender, RoutedEventArgs e)
		{
			this.BoardData = BoardData.porazenie();
			WinnerLineView.Visibility = Visibility.Collapsed;
			chek = 1;
			StatusAndErrors.Text = "Ваш ход";
			updtab();
		}
	}
}

