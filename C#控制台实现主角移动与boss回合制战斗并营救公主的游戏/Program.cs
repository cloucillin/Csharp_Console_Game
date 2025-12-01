namespace 实践小项目
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1.需求分析
            //开始界面-控制台输出-控制台输入-控制台颜色变化
            //游戏界面-控制台输出-控制台输入-控制台颜色变化-while-switch
            //判断 - 条件运算符 - if语句
            //回合制战斗 - 随机数 - 循环 - if语句
            //结束界面 - 控制台输出 - 控制台输入 - 控制台颜色变化
            //界面之间的相关切换


            //2.控制台的基础设置


            //设置舞台（控制台）的大小
            //以后要改或者多处使用同样数值可以声明一个变量
            int xSize = 50;
            int ySize = 25;
            int DialogBox_y = (int)(0.8 * ySize);

            Console.SetWindowSize(xSize, ySize);
            Console.SetBufferSize(xSize, ySize);

            Random r = new Random();

            //3.多个场景
            //当前场景的编号：
            int nowSceneId = 1;

            //玩家战斗相关
            //申明战斗状态
            bool isFight = false;

            //作用是 从while 循环内部 的switch 改变标识 用来跳出外层的while循环
            bool isOver  = false;

            //结束场景显示的结束内容
            string gameOverInfo = " ";

            Console.SetCursorPosition(xSize / 2 - 12, 4);

            Console.WriteLine("请输入你的名字（三个字）");
            Console.SetCursorPosition(xSize / 2 - 4, 7);
            string userName = Console.ReadLine();

            //在玩家输入完信息后隐藏光标正式进入游戏
            Console.CursorVisible = false;

            while (true)
            {
                bool isQuitWhile = false;

                //根据不同的场景ID 进行不同的逻辑处理
                //如果看到一个变量经常和常量比较就自然的联想到switch语句
                //这个语句就是用来做这样的事情的
                switch (nowSceneId)
                {
                    #region 开始场景
                    case 1:
                        Console.Clear();
                        isFight = false;
                        //中文每个字符x占2y占1
                        //要使显示居中则光标居中后减去自身长度的一半再Write
                        Console.SetCursorPosition(xSize/2 - 7 , 8);
                        Console.Write(userName + "营救公主");

                        //当前选项的编号
                        int nowSelIndex = 0;

                        //输入：每次都可以输入：死循环
                        //我们可以构造一个开始界面自己的死循环
                        //专门用来处理开始场景相关的逻辑
                        while (true)
                        {
                            //显示 内容
                            //先设置光标位置 再显示内容


                            Console.SetCursorPosition(xSize / 2 - 4, 13);
                            //根据道歉选择的编号 来决定 是否变色
                            Console.ForegroundColor = nowSelIndex == 0 ? ConsoleColor.Red : ConsoleColor.White;
                            Console.Write("开始游戏");
                            Console.ForegroundColor = nowSelIndex == 1 ? ConsoleColor.Red : ConsoleColor.White;
                            Console.SetCursorPosition(xSize / 2 - 4, 17);
                            Console.Write("退出游戏");
                            //将开始游戏和退出游戏编号：通过改变编号来改变选中什么，红色显示什么

                            //检测 输入
                            //检测玩家 输入一个键内容 并且不会在控制台上 显示输入的内容
                            char input = Console.ReadKey(true).KeyChar;
                            switch(input)
                            {
                                case 'W':
                                case 'w':
                                    nowSelIndex --;
                                    if(nowSelIndex < 0)
                                    {
                                        nowSelIndex = 0;
                                    }
                                    break;
                                case 's':
                                case 'S':
                                    nowSelIndex ++;
                                    if (nowSelIndex > 1)
                                    {
                                        nowSelIndex = 1;
                                    }

                                    break;
                                case 'j':
                                case 'J':
                                    if(nowSelIndex == 0)
                                    {
                                        //1.改变当前选择的场景id
                                        //2.要退出 内层while循环
                                        nowSceneId = 2;
                                        //如果在swich里面要退出外层的while循环:在switch里面break只和和switch配对
                                        //只能在循环处声明一个标识通过改变标识在退出switch进入while后语句中判断该值以退出循环
                                        isQuitWhile = true;
                                    }
                                    else
                                    {
                                        Environment.Exit(0);//关闭控制台
                                    }
                                        break;
                            }

                            if(isQuitWhile == true)
                            {
                                break;//这样就可以在switch中改变isQuitWhile的值来退出while循环
                            }
                        }
                        break;
                    #endregion

                    //游戏场景
                    case 2:
                        Console.Clear();
                        isOver=false;//为了第二次进入游戏游戏的isOver为true必须重新赋值为false才能正常执行
                        //游戏场景的死循环 专门用来 检测 玩家输入相关循环
                        #region 不变的红墙
                        //不变的红墙
                        Console.ForegroundColor = ConsoleColor.Red;
                        //画墙
                        //先画横着的墙
                        //记得用循环理解法：第一次进入循环执行过程和结果最后一次进入循环过程和结果进行调整

                        for(int i = 0; i <= xSize - 2; i+=2)//由于一个方块占x轴两个位置+=2才行
                        {
                            Console.SetCursorPosition(i, 0);
                            Console.Write("■");
                            Console.SetCursorPosition(i, DialogBox_y);
                            Console.Write("■");
                            Console.SetCursorPosition(i, ySize -1 );
                            Console.Write("■");
                        }
                        //画竖着的墙
                        for (int i = 0; i <= ySize - 1; i += 1)//由于一个方块占x轴两个位置+=2才行
                        {
                            Console.SetCursorPosition(0, i);
                            Console.Write("■");
                            Console.SetCursorPosition(xSize - 2, i);
                            Console.Write("■");
                        }
                        #endregion

                     

                        #region 属性相关
                        //boss 属性相关
                        int bossX = 2*((int)(r.Next(2,xSize-3)/2));
                        int bossY = r.Next(2,DialogBox_y);
                        int bossAtkMin = 7;
                        int bossAtkMax = 13;
                        int bossHp = 100;
                        string bossIcon = "■";
                        //颜色和随机数一样是自定义颜色变量

                        //申明一个颜色变量
                        ConsoleColor bossColor = ConsoleColor.Green;

                        //玩家 属性相关
                        int playerX = 4;
                        int playerY = 5;
                        int playerAtkMin = 8;
                        int playerAtkMax = 12;
                        int playerHp = 110;
                        string playerIcon = "●";
                        ConsoleColor playerColor = ConsoleColor.Yellow;
                        #endregion

                        //公主 属性相关
                        int princessX = 2 * ((int)(r.Next(2, xSize - 3) / 2));
                        int princessY = r.Next(2, DialogBox_y);
                        string princessIcon = "◆";
                        ConsoleColor princessColor = ConsoleColor.Red;


                        //玩家输入的内容 外面申明 节约性能
                        char playerInput;
                        int playerMoveCount = 0;

                        while (true)
                        {
                            //boss活着的时候才会绘制他
                            if( bossHp > 0)
                            {
                            //绘制boss图标
                            Console.SetCursorPosition(bossX, bossY);
                            Console.ForegroundColor = bossColor;
                            Console.Write(bossIcon);
                            }
                            else
                            {
                                //赢下战斗
                                //绘制公主图标
                                Console.SetCursorPosition(princessX, princessY);
                                Console.ForegroundColor = princessColor;
                                Console.Write(princessIcon);

                            }

                            //绘制玩家和玩家移动相关
                            Console.SetCursorPosition(playerX, playerY);
                            Console.ForegroundColor = playerColor;
                            Console.Write(playerIcon);

                            //得到玩家输入
                            playerInput = Console.ReadKey(true).KeyChar;//玩家输入之前不会进行下面的代码，是每一帧的分界

                            //玩家键入之后就要检测攻击状态，如果不在攻击状态就执行移动否则不移动进入攻击状态

                            //战斗状态处理什么逻辑
                            if (isFight)
                            {
                                //如果是战斗状态 你做什么
                                //只会处理J键
                                if(playerInput == 'j' || playerInput == 'J')
                                {
                                    //在这判断 玩家或者怪物 是否死亡 如果死亡了 继续之后的流程
                                    if(playerHp <= 0)
                                    {
                                        //游戏结束
                                        //显示结束画面
                                        //输掉了应该直接显示 游戏结束画面
                                        nowSceneId = 3;
                                        gameOverInfo = "游戏失败";
                                        break;
                                    }
                                    else if (bossHp <= 0)
                                    {
                                        //去营救公主
                                        //boss擦除
                                        Console.SetCursorPosition(bossX, bossY);
                                        Console.Write("  ");
                                        isFight = false;
                                    }
                                    else
                                    {
                                        //去处理按J键打架

                                        //玩家打怪物
                                        int playerAtk = r.Next(playerAtkMin, playerAtkMax);
                                        int bossAtk = r.Next(bossAtkMin, bossAtkMax);

                                        //血量减对应的攻击力
                                        bossHp -= playerAtk;
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        //先擦除这一行 上次显示的内容
                                        Console.SetCursorPosition(2, DialogBox_y + 2);
                                        Console.Write("                                           ");
                                        //再打印这次作战的信息
                                        Console.SetCursorPosition(2, DialogBox_y + 2);
                                        Console.Write("你对boss造成了{0}点伤害，boss剩余血量为{1}", playerAtk, bossHp);

                                        //怪物打玩家
                                        //怪物血量大于0才能打玩家
                                        if (bossHp > 0)
                                        {
                                            playerHp -= bossAtk;
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.SetCursorPosition(2, DialogBox_y + 3);
                                            Console.Write("                                           ");

                                            //boss如果把玩家打死了做什么
                                            if (playerAtk <= 0)
                                            {
                                                Console.SetCursorPosition(2, DialogBox_y + 3);
                                                Console.Write("很遗憾，你未能通过boss的试炼，战败了");
                                            }
                                            else
                                            {
                                                Console.SetCursorPosition(2, DialogBox_y + 3);
                                                Console.Write("boss对你造成了{0}点伤害，你剩余血量为{1}", bossAtk, playerHp);
                                            }
                                        }
                                        else
                                        {
                                            //擦除之前的战斗信息
                                            Console.SetCursorPosition(2, DialogBox_y + 1);
                                            Console.Write("                                           ");
                                            Console.SetCursorPosition(2, DialogBox_y + 2);
                                            Console.Write("                                           ");
                                            Console.SetCursorPosition(2, DialogBox_y + 3);
                                            Console.Write("                                           ");
                                            //显示恭喜胜利的信息
                                            Console.SetCursorPosition(2, DialogBox_y + 1);
                                            Console.Write("你战胜了boss，快去营救公主");
                                            Console.SetCursorPosition(2, DialogBox_y + 2);
                                            Console.Write("前往公主身边按J键继续");
                                            //战斗结束了 不是战斗状态了
                                            isFight = false;

                                        }

                                    }

                                }

                            }
                            //非战斗状态处理什么逻辑
                            else
                            {
                                //擦除
                                Console.SetCursorPosition(playerX, playerY);
                                Console.Write("  ");
                                Console.SetCursorPosition(bossX, bossY);
                                Console.Write("  ");
                                //改位置
                                switch (playerInput)
                                {
                                    case 'w':
                                    case 'W':
                                        --playerY;
                                        if (playerY < 1)
                                        {
                                            playerY = 1;
                                        }
                                        else if (playerY == bossY && playerX == bossX && bossHp > 0)
                                        {
                                            playerY++;
                                        }
                                        //判断是否和公主重合
                                        //另外必须加bossHp<=0 的判断否则boss没死主角也不能走到公主的位置
                                        else if(playerX == princessX && playerY == princessY && bossHp <= 0)
                                        {
                                            playerY++;
                                        }
                                        break;
                                    case 's':
                                    case 'S':
                                        ++playerY;
                                        if (playerY >= DialogBox_y)
                                        {
                                            playerY--;
                                        }
                                        else if (playerY == bossY && playerX == bossX && bossHp > 0)
                                        {
                                            playerY--;
                                        }
                                        else if (playerX == princessX && playerY == princessY && bossHp <= 0)
                                        {
                                            playerY--;
                                        }
                                        break;
                                    case 'a':
                                    case 'A':
                                        playerX -= 2;
                                        if (playerX <= 1)
                                        {
                                            playerX += 2;
                                        }
                                        else if (playerY == bossY && playerX == bossX && bossHp > 0)
                                        {
                                            playerX += 2;
                                        }
                                        else if (playerX == princessX && playerY == princessY && bossHp <= 0)
                                        {
                                            playerX+=2;
                                        }

                                        break;
                                    case 'd':
                                    case 'D':
                                        playerX += 2;
                                        if (playerX >= xSize - 2)
                                        {
                                            playerX -= 2;
                                        }
                                        else if (playerY == bossY && playerX == bossX && bossHp > 0)
                                        {
                                            playerX -= 2;
                                        }
                                        else if (playerX == princessX && playerY == princessY && bossHp <= 0)
                                        {
                                            playerX-=2;
                                        }

                                        break;
                                    case 'J':
                                    case 'j':
                                        //开始战斗
                                        if (((playerX == bossX && (playerY == bossY - 1 || playerY == bossY + 1)) || (playerY == bossY && (playerX == bossX - 2 || playerX == bossX + 2))) && bossHp > 0)
                                        {
                                            //可以开始战斗
                                            Console.SetCursorPosition(2, DialogBox_y + 1);
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.Write("开始和boss战斗了，按J继续");
                                            Console.SetCursorPosition(2, DialogBox_y + 2);
                                            Console.Write("玩家当前的血量为{0}", playerHp);
                                            Console.SetCursorPosition(2, DialogBox_y + 3);
                                            Console.Write("boss当前的血量为{0}", bossHp);
                                            isFight = true;

                                        }
                                        //打完boss后营救公主
                                        //判断是否在公主身边按"J"键
                                        else if (((playerX == princessX && (playerY == princessY - 1 || playerY == princessY + 1)) || (playerY == princessY && (playerX == princessX - 2 || playerX == princessX + 2))) && bossHp <= 0)
                                        {
                                            //改变场景 ID
                                            nowSceneId = 3;
                                            gameOverInfo = "游戏通关";
                                            //跳出 游戏界面的while循环 回到主循环
                                            isOver = true;
                                            break;
                                        }


                                            //要让玩家不能再移动
                                            //下方能够显示信息

                                            break;
                                }
                                playerMoveCount++;

                                int bossMoveDirection;

                                //主角每移动两次boss随机朝一个方向移动一次

                                if (playerMoveCount >= 2 && isFight == false)
                                {
                                    playerMoveCount = 0;
                                    bossMoveDirection = r.Next(0, 4);
                                    switch (bossMoveDirection)
                                    {
                                        case 0:
                                            //w
                                            bossY--;
                                            if (bossY <= 0 || (bossY == playerY && bossX == playerX))
                                            {
                                                bossY += 2;//boss撞墙（与墙重合）或与主角重合往反方向行动
                                                if (bossY >= DialogBox_y || (bossY == playerY && bossX == playerX))
                                                {
                                                    bossY -= 1;//如果往反方向移动后仍会和墙或者主角重合则放弃这次移动
                                                               //此时一般为主角和墙夹住了boss的移动，boss要么由反方向与主角重合要么与墙重合，要住于更改判定与哪一面墙重合
                                                               //由于往反方向的移动所以第二次判定的撞墙为对面的那面墙
                                                }
                                            }
                                            break;
                                        case 1:
                                            //a
                                            bossX -= 2;
                                            if (bossX <= 1 || (bossY == playerY && bossX == playerX))
                                            {
                                                bossX += 4;
                                                if (bossX >= xSize - 2 || (bossY == playerY && bossX == playerX))
                                                {
                                                    bossX -= 2;
                                                }
                                            }
                                            break;

                                        case 2:
                                            //s
                                            bossY++;
                                            if (bossY >= DialogBox_y || (bossY == playerY && bossX == playerX))
                                            {
                                                bossY -= 2;
                                                if (bossY <= 0 || (bossY == playerY && bossX == playerX))
                                                {
                                                    bossY += 1;
                                                }
                                            }
                                            break;

                                        case 3:
                                            //d
                                            bossX += 2;
                                            if (bossX >= xSize - 2 || (bossY == playerY && bossX == playerX))
                                            {
                                                bossX -= 4;
                                                if (bossX <= 1 || (bossY == playerY && bossX == playerX))
                                                {
                                                    bossY += 2;
                                                }
                                            }
                                            break;

                                    }
                                }

                                if (isOver == true)
                                {
                                    //就是和while循环配对
                                    break;
                                }
                            }
                        }       
                        break;  

                    //结束场景

                    case 3:
                        nowSelIndex = 0;
                        Console.Clear();


                        //标题的显示
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(xSize / 2 - 4, 5);
                        Console.Write("GameOver");
                       
                        //可变内容的显示 根据失败或者 成功 显示的内容不一样
                        Console.SetCursorPosition(xSize / 2 - 4, 7);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(gameOverInfo);

                        bool isEndQuitWhile = false;

                        //结束场景死循环
                        while (true)
                        {
                            Console.SetCursorPosition(xSize / 2 - 6, 13);
                            //根据道歉选择的编号 来决定 是否变色
                            Console.ForegroundColor = nowSelIndex == 0 ? ConsoleColor.Red : ConsoleColor.White;
                            Console.Write("回到开始游戏");
                            Console.ForegroundColor = nowSelIndex == 1 ? ConsoleColor.Red : ConsoleColor.White;
                            Console.SetCursorPosition(xSize / 2 - 4, 17);
                            Console.Write("退出游戏");
                            //将开始游戏和退出游戏编号：通过改变编号来改变选中什么，红色显示什么

                            //检测 输入
                            //检测玩家 输入一个键内容 并且不会在控制台上 显示输入的内容
                            char input = Console.ReadKey(true).KeyChar;
                            switch (input)
                            {
                                case 'W':
                                case 'w':
                                    nowSelIndex--;
                                    if (nowSelIndex < 0)
                                    {
                                        nowSelIndex = 0;
                                    }
                                    break;
                                case 's':
                                case 'S':
                                    nowSelIndex++;
                                    if (nowSelIndex > 1)
                                    {
                                        nowSelIndex = 1;
                                    }

                                    break;
                                case 'j':
                                case 'J':
                                    if (nowSelIndex == 0)
                                    {
                                        //1.改变当前选择的场景id
                                        //2.要退出 内层while循环
                                        nowSceneId = 1;
                                        //如果在swich里面要退出外层的while循环:在switch里面break只和和switch配对
                                        //只能在循环处声明一个标识通过改变标识在退出switch进入while后语句中判断该值以退出循环
                                        isEndQuitWhile = true;
                                    }
                                    else
                                    {
                                        Environment.Exit(0);//关闭控制台
                                    }
                                    break;
                            }

                            if (isEndQuitWhile == true)
                            {
                                break;//这样就可以在switch中改变isQuitWhile的值来退出while循环
                            }

                        }
                        break;

                }

            }

        }
    }
}
//面向流程 逻辑思路要求强