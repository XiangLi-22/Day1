using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 三子棋
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int input = 0;
            do
            {
                mune();
                Console.WriteLine("请选择是否开启游戏：");
                input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        game();
                        break;
                    case 0:
                        Console.WriteLine("退出游戏");
                        break;
                    default:
                        Console.WriteLine("选择非法，请重新输入");
                        break;
                }
            }
            while (input != 0);
        }
        #region 菜单
        static void mune()
        {
            Console.WriteLine("*****************************");
            Console.WriteLine("******* 1.play 0.exit *******");
            Console.WriteLine("*****************************");
        }
        #endregion
        #region 游戏整体
        static void game()
        {
            char ret = ' ';
            //初始化二维数组，并将二维数组中的所有元素都初始化成空格
            char[,] arr = new char[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    arr[i, j] = ' ';
                }
            }
            //初始化棋盘格，并将二维数组中的元素放到二维数组中
            borad(arr, 3, 3);
            //进行下棋，用户和电脑下棋在一个循环中，才不会使得用户和电脑只能下一次棋
            while (true)
            {
                //用户下棋
                UserMove(arr, 3, 3);
                ret = IsWin(arr, 3, 3);
                if (ret != 'C')
                {
                    break;
                }
                //打印下棋的内容
                borad(arr, 3, 3);
                //电脑下棋
                ComputerMove(arr, 3, 3);
                ret = IsWin(arr, 3, 3);
                if (ret != 'C')
                {
                    break;
                }
                //打印下棋的内容
                borad(arr, 3, 3);
            }
            if (ret == '*')
            {
                Console.WriteLine("用户赢了");
                borad(arr, 3, 3);
            }
            else if(ret == '#')
            {
                Console.WriteLine("电脑赢了");
                borad(arr, 3, 3);
            }
            else
            {
                Console.WriteLine("平局");
                borad(arr, 3, 3);
            }
        }
        #endregion
        #region 棋盘格
        static void borad(char[,] arr, int rew, int col)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(" " + arr[i, 0] + " | " + arr[i, 1] + " | " + arr[i, 2] + " ");
                if (i < 2)
                {
                    Console.WriteLine("---|---|---");
                }
            }
        }
        #endregion
        #region 用户下棋
        static void UserMove(char[,] arr, int row, int col)
        {
            Console.WriteLine("请输入坐标：");
            while (true)
            {
                int x = int.Parse(Console.ReadLine());
                int y = int.Parse(Console.ReadLine());
                if (x >= 1 && x <= 3 && y >= 1 && y <= 3)
                {
                    if (arr[x - 1, y - 1] == ' ')
                    {
                        arr[x - 1, y - 1] = '*';
                        break;
                    }
                    else
                    {
                        Console.WriteLine("坐标被占用，请重新输入");
                    }
                }
                else
                {
                    Console.WriteLine("坐标非法，请重新输入");
                }
            }

        }
        #endregion
        #region  电脑下棋判断用户是否有赢的趋势
        static bool IsCombine(char[,] arr, int x1, int y1, int x2, int y2, int x3, int y3)
        {
            if (x1 >= 0 && x1 < 3 && y1 >= 0 && y1 < 3 &&
                x2 >= 0 && x2 < 3 && y2 >= 0 && y2 < 3 &&
                x3 >= 0 && x3 < 3 && y3 >= 0 && y3 < 3)
            {
                if (arr[x1, y1] == '*' && arr[x2, y2] == '*' && arr[x3, y3] == ' ')
                {
                    arr[x3, y3] = '#';
                    return true;
                }
            }

            return false;
        }
        #endregion
        #region 电脑下棋
        static void ComputerMove(char[,] arr, int row, int col)
        {
            //我们想让电脑变聪明，当对手的棋子出现结合的情况下，我们进行拦截，反之在对手的棋子旁边下棋
            Console.WriteLine("电脑下棋：");
            if (arr[1, 1] == ' ')
            {
                arr[1, 1] = '#';
                return;
            }
            //判断行是否有结合趋势
            for (int i = 0; i < 3; i++)
            {
                if (IsCombine(arr, i, 0, i, 1, i, 2) || IsCombine(arr, i, 0, i, 2, i, 1) || IsCombine(arr, i, 2, i, 1, i, 0))
                {
                    return;
                }
            }
            //判断列是否有结合趋势
            for (int j = 0; j < 3; j++)
            {
                if (IsCombine(arr, 0, j, 1, j, 2, j) || IsCombine(arr, 0, j, 2, j, 1, j) || IsCombine(arr, 2, j, 1, j, 0, j))
                {
                    return;
                }
            }
            //判断对角线是否有结合趋势
            if (IsCombine(arr, 0, 0, 1, 1, 2, 2) || IsCombine(arr, 0, 0, 2, 2, 1, 1) || IsCombine(arr, 2, 2, 1, 1, 0, 0) || IsCombine(arr, 0, 3, 1, 1, 2, 0) || IsCombine(arr, 0, 3, 2, 0, 1, 1) || IsCombine(arr, 1, 1, 2, 2, 0, 3))
            {
                return;
            }
            //创建一个Random类型的random变量，Random类型用于生成0-2之间的随机数
            Random random = new Random();
            while (true)
            {
                int i = random.Next(3);
                int j = random.Next(3);

                //if (arr[i,j] == ' ')
                //{
                //    arr[i, j] = '#';
                //    break;
                //}


                if (arr[i, j] == ' ')
                {
                    bool hasOpponent = false;
                    if (j > 0 && arr[i, j - 1] == '*') hasOpponent = true;
                    if (j < 2 && arr[i, j + 1] == '*') hasOpponent = true;
                    if (i > 0 && arr[i - 1, j] == '*') hasOpponent = true;
                    if (i < 2 && arr[i + 1, j] == '*') hasOpponent = true;
                    if (i < 2 && j > 0 && arr[i + 1, j - 1] == '*') hasOpponent = true;
                    if (i > 0 && j < 2 && arr[i - 1, j + 1] == '*') hasOpponent = true;
                    if (i > 0 && j > 0 && arr[i - 1, j - 1] == '*') hasOpponent = true;
                    if (i < 2 && j < 2 && arr[i + 1, j + 1] == '*') hasOpponent = true;
                    if (hasOpponent == true && arr[i, j] == ' ')
                    {
                        arr[i, j] = '#';
                        break;
                    }
                }
            }
        }
        #endregion
        #region 判断输赢
        //用户赢了,返回'*';电脑赢了,返回'#';平局,返回'Q';都不满足,返回'C'
        static char IsWin(char[,] arr , int row,int col)
        {
            //行赢
            for (int i = 0; i < 3; i++)
            {
                if (arr[i,0] == arr[i,1] && arr[i,1] == arr[i,2] && arr[i,1] != ' ')
                {
                    return arr[i, 1];
                }
                //列赢
                if (arr[0, i] == arr[1, i] && arr[1, i] == arr[2, i] && arr[0, i] != ' ')
                {
                    return arr[0, i];
                }
            }
            //对角线赢
            if (arr[0, 0] == arr[1, 1] && arr[1, 1] == arr[2, 2] && arr[1, 1] != ' ' ||
                arr[0, 2] == arr[1, 1] && arr[1, 1] == arr[2, 0] && arr[1, 1] != ' ')
            {
                return arr[1, 1];
            }
            //是否平局
            if (IsDraw(arr, 3, 3))
            {
                return 'Q';
            }
            return 'C';

        }
        #endregion
        #region 判断是否平局
        static bool IsDraw(char [,]arr,int row ,int col)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (arr[i,j] == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion
    }

}