using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace main
{
	public class Gamek
	{
		private static List<Gamek> old = new List<Gamek>();
		private int[,] polegame;
		private int vozvrat;
		private int st;
		private Line liniawin;
		Random rand;
		public Gamek()
		{
			St = 0;
			polg = new int[3, 3];
			for (int i = 0; i < polg.GetLength(0); i++)
				for (int j = 0; j < polg.GetLength(1); j++)
					polg[i, j] = 0;
			rand = new Random();
			if (rand.Next(2) == 1)
				Turn = 1;
			else Turn = -1;
		}
		public bool Play(int player, int i, int j)
		{
			polg[i, j] = player;
			st++;
			Line Winner = Proverkawin();

			if (Winner != null)
			{
				Liniawin = Winner;
				throw new Exception("Конец игры, нажмите на новую игру");
			}
			nichia();
			noviTurn(); 
			return true;
		}
		private void noviTurn()
		{
			Turn *= -1;
		}
		public Gamek porazenie()
		{
			old.Add(this);
			return new Gamek();
		}
		private int[] freeCells(int xo)
		{
			for (int i = 0; i < polg.GetLength(0); i++)
			{
				if (polg[i, 0] == xo && polg[i, 1] == xo && polg[i, 2] == 0)
					return new int[] { i, 2 };
				else if (polg[i, 1] == xo && polg[i, 2] == xo && polg[i, 0] == 0)
					return new int[] { i, 0 };
				else if (polg[i, 0] == xo && polg[i, 2] == xo && polg[i, 1] == 0)
					return new int[] { i, 1 };
			}
			for (int i = 0; i < polg.GetLength(1); i++)
			{
				if (polg[0, i] == xo && polg[1, i] == xo && polg[2, i] == 0)
					return new int[] { 2, i };
				else if (polg[1, i] == xo && polg[2, i] == xo && polg[0, i] == 0)
					return new int[] { 0, i };
				else if (polg[0, i] == xo && polg[2, i] == xo && polg[1, i] == 0)
					return new int[] { 1, i };
			}
			if (polg[0, 0] == xo && polg[2, 2] == xo && polg[1, 1] == 0)
				return new int[] { 1, 1 };
			else if (polg[1, 1] == xo && polg[2, 2] == xo && polg[0, 0] == 0)
				return new int[] { 0, 0 };
			else if (polg[0, 0] == xo && polg[1, 1] == xo && polg[2, 2] == 0)
				return new int[] { 2, 2 };

			if (polg[2, 0] == xo && polg[0, 2] == xo && polg[1, 1] == 0)
				return new int[] { 1, 1 };
			else if (polg[1, 1] == xo && polg[0, 2] == xo && polg[2, 0] == 0)
				return new int[] { 2, 0 };
			else if (polg[2, 0] == xo && polg[1, 1] == xo && polg[0, 2] == 0)
				return new int[] { 0, 2 };
			return null;
		}
		public void xodrob()
		{
			int[] OptionToWin = freeCells(Turn);
			if (OptionToWin != null)
			{ Play(Turn, OptionToWin[0], OptionToWin[1]); return; }
			int[] OptionToBlock = freeCells(Turn * -1);
			if (OptionToBlock != null)
			{ Play(Turn, OptionToBlock[0], OptionToBlock[1]); return; }
			if (st == 0) 
			{
				RandomOX();

				return;
			}
			if (st == 1) 
			{
				if (polegame[1, 1] != 0)
				{
					RandomOX();
					return;
				}
				if (polegame[0, 0] != 0 || polegame[2, 2] != 0 || polegame[2, 0] != 0 || polegame[0, 2] != 0)
				{
					Play(vozvrat, 1, 1);
					return;
				}
			}
			Play(vozvrat, rand.Next(2), rand.Next(2));
		}
		private bool RandomOX()
		{
			int a = 0, b = 0;
			if (rand.Next(2) == 1)
				a = 2;
			if (rand.Next(2) == 1)
				b = 2;
			return Play(Turn, a, b);
		}
		 public void nichia()
        {
			if (polg[2, 0] != 0 && polg[1, 0] != 0 && polg[0, 0] != 0 && 
				polg[0, 2] != 0 && polg[1, 2] != 0 && polg[0, 2] != 0 &&
				polg[1, 1] != 0 && polg[0, 1] != 0 && polg[2, 1] != 0)
			{
				throw new Exception("Ничья");
			}
		}
		public int St
		{
			get { return st; }
			private set { st = value; }
		}
		public int Turn
		{
			get { return vozvrat; }
			private set { vozvrat = value; }
		}
		public Line Liniawin
		{
			get { return liniawin; }
			private set { liniawin = value; }
		}
		public int[,] polg
		{
			get { return polegame; }
			private set { polegame = value; }
		}
		private Line Proverkawin()
		{
			for (int i = 0; i < polg.GetLength(0); i++)
			{
				if (polg[i, 0] == Turn && polg[i, 1] == Turn && polg[i, 2] == Turn)
					return new Line()
					{
						X1 = 0,
						Y1 = 40 + 80 * i,
						X2 = 240,
						Y2 = 40 + 80 * i
					};
			}
			for (int i = 0; i < polg.GetLength(1); i++)
			{
				if (polg[0, i] == Turn && polg[1, i] == Turn && polg[2, i] == Turn)
					return new Line()
					{
						X1 = 40 + 80 * i,
						Y1 = 0,
						X2 = 40 + 80 * i,
						Y2 = 240
					};
			}
			if (polg[0, 0] == Turn && polg[2, 2] == Turn && polg[1, 1] == Turn)
				return new Line()
				{
					X1 = 0,
					Y1 = 0,
					X2 = 240,
					Y2 = 240
				};
			if (polg[2, 0] == Turn && polg[0, 2] == Turn && polg[1, 1] == Turn)
				return new Line()
				{
					X1 = 0,
					Y1 = 240,
					X2 = 240,
					Y2 = 0
				};
			return null;
		}
	}
}
