using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TetrisProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// the board is 10x18
    /// where each square is 20x 20 pixels 
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer movementDownTimer = new DispatcherTimer();
        private DispatcherTimer movementSideTimer = new DispatcherTimer();
        private bool paused = true;
        GameBoard board = new GameBoard();

        public MainWindow()
        {
            InitializeComponent();          
            board.drawField(play_area);

            movementDownTimer.Tick += new EventHandler(downwardTick);
            movementDownTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            movementDownTimer.Start();

            
        }


        private void downwardTick(object sender, EventArgs e)
        {
            if(board.CurrBlock.Y == 14 || (board.CurrBlock.Y != 14 && board.checkBlockCollision() == true))
            {
                board.checkBlockCollision();
                board.chooseBlock();
            }
            else
            {
                board.CurrBlock.Y++;
                board.moveBlock();
                

            }
            board.drawField(play_area);
        }

        private void pause_button_Click(object sender, RoutedEventArgs e)
        {
            if(paused == true)
            {
                movementDownTimer.Start();
                pause_button.Content = "Pause";
                paused = false;
            }
            else
            {
                movementDownTimer.Stop();
                pause_button.Content = "Resume";
                paused = true;
            }

        }

        private void play_area_KeyDown(object sender, KeyEventArgs e)
        {
            
            int pWidth = board.CurrBlock.checkWidth();
            if(board.CurrBlock.Y == 14)
            {
                return;
            }
            else if (e.Key == Key.Left)
            {
                if (board.CurrBlock.X <= 0)
                {
                    board.CurrBlock.X = 0;

                }
                else
                {
                    board.CurrBlock.X--;
                }
                board.moveBlock();
                deleteTrailLeft();
                    
                    
            }
            else if (e.Key == Key.Right)
            {

                if (board.CurrBlock.X >= 10 -pWidth)
                {
                    board.CurrBlock.X = 10-pWidth;
                }
                else
                {
                    board.CurrBlock.X++;
                }
                board.moveBlock();
                deleteTrailRight();
            }
            board.drawField(play_area);
        }

        private void deleteTrailRight()
        {
            int id = board.CurrBlock.Id;
            int p = board.CurrBlock.Pos;
            int currX = board.CurrBlock.X;
            int currY = board.CurrBlock.Y;
            if (id == 1)
            {

                if (p % 2 == 0)
                {
                    board.Field[currY +3, currX-1] = 0;
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                        board.Field[currY + i, currX - 1] = 0;
                }

            }
            else if (id == 2)
            {
                if (p == 0)
                {
                    board.Field[currY + 3, currX -1] = 0;
                    board.Field[currY + 2, currX - 1] = 0;
                }
                else if (p == 1)
                {
                    board.Field[currY +1, currX - 1] = 0;
                    board.Field[currY + 2, currX - 1] = 0;
                    board.Field[currY + 3, currX - 1] = 0;
                }
                else if (p == 2)
                {
                    board.Field[currY + 2, currX -1] = 0;
                    board.Field[currY + 3, currX + 1] = 0;
                }
                else
                {
                    board.Field[currY + 1, currX ] = 0;
                    board.Field[currY + 2, currX ] = 0;
                    board.Field[currY + 3, currX - 1] = 0;
                }
            }
            else if (id == 3)
            {
                if (p == 0)
                {
                    board.Field[currY + 3, currX -1] = 0;
                    board.Field[currY + 2, currX -1] = 0;
                }
                else if (p == 1)
                {
                    board.Field[currY + 3, currX - 2] = 0;
                    board.Field[currY + 1, currX - 1] = 0;
                    board.Field[currY + 2, currX - 1] = 0;
                }
                else if (p == 2)
                {
                    board.Field[currY + 2, currX + 1] = 0;
                    board.Field[currY + 3, currX - 1] = 0;
                }
                else
                {
                    board.Field[currY + 1, currX - 1] = 0;
                    board.Field[currY + 2, currX ] = 0;
                    board.Field[currY + 3, currX ] = 0;
                }
            }
            else if (id == 4)
            {
                if (p % 2 == 0)
                {
                    board.Field[currY + 2, currX] = 0;
                    board.Field[currY + 3, currX -1] = 0;
                }
                else
                {

                    board.Field[currY + 1, currX - 1] = 0;
                    board.Field[currY + 2, currX - 1] = 0;
                    board.Field[currY + 3, currX] = 0;
                }

            }
            else if (id == 5)
            {
                if (p % 2 == 0)
                {
                    board.Field[currY + 2, currX -1 ] = 0;
                    board.Field[currY + 3, currX] = 0;
                }
                else
                {

                    board.Field[currY + 1, currX] = 0;
                    board.Field[currY + 2, currX - 1] = 0;
                    board.Field[currY + 3, currX - 1] = 0;
                }

            }
            else if (id == 6)
            {
                board.Field[currY + 2, currX -1] = 0;
                board.Field[currY + 3, currX -1] = 0;
            }
            else
            {
                if (p == 0)
                {
                    board.Field[currY + 2, currX -1] = 0;
                    board.Field[currY + 3, currX] = 0;
                }
                else if (p == 1)
                {
                    board.Field[currY + 1, currX - 1] = 0;
                    board.Field[currY + 2, currX - 1] = 0;
                    board.Field[currY + 3, currX - 1] = 0;
                }
                else if (p == 2)
                {
                    board.Field[currY + 2, currX] = 0;
                    board.Field[currY + 3, currX -1] = 0;
                }
                else
                {
                    board.Field[currY + 1, currX] = 0;
                    board.Field[currY + 2, currX - 1] = 0;
                    board.Field[currY + 3, currX] = 0;
                }
            }
        }

        private void deleteTrailLeft()
        {
            int id = board.CurrBlock.Id;
            int p = board.CurrBlock.Pos;
            int currX = board.CurrBlock.X;
            int currY = board.CurrBlock.Y;
            if (id == 1)
            {

                if (p % 2 == 0)
                {
                    board.Field[currY + 3, currX + 4] = 0;
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                        board.Field[currY + i, currX + 1] = 0;
                }

            }
            else if (id == 2)
            {
                if (p == 0)
                {
                    board.Field[currY + 3, currX + 3] = 0;
                    board.Field[currY + 2, currX + 1] = 0;
                }
                else if (p == 1)
                {
                    
                    board.Field[currY + 1, currX + 2] = 0;
                    board.Field[currY + 2, currX + 1] = 0;
                    board.Field[currY + 3, currX + 1] = 0;
                }
                else if (p == 2)
                {
                    board.Field[currY + 2, currX + 3] = 0;
                    board.Field[currY + 3, currX + 3] = 0;
                }
                else
                {
                    for (int i = 1; i < 4; i++)
                        board.Field[currY + i, currX + 2] = 0;
                }
            }
            else if (id == 3)
            {
                if (p == 0)
                {
                    board.Field[currY + 3, currX + 1] = 0;
                    board.Field[currY + 2, currX + 4] = 0;
                }
                else if (p == 1)
                {
                    board.Field[currY + 3, currX + 2] = 0;
                    board.Field[currY + 1, currX + 1] = 0;
                    board.Field[currY + 2, currX + 1] = 0;
                    board.Field[currY + 0, currX + 1] = 0;
                }
                else if (p == 2)
                {
                    board.Field[currY + 2, currX + 4] = 0;
                    board.Field[currY + 3, currX + 4] = 0;
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                        board.Field[currY + i, currX + 2] = 0;
                }
            }
            else if (id == 4)
            {
                if (p % 2 == 0)
                {
                    board.Field[currY + 2, currX + 3] = 0;
                    board.Field[currY + 3, currX + 2] = 0;
                }
                else
                {

                    board.Field[currY + 1, currX + 1] = 0;
                    board.Field[currY + 2, currX + 2] = 0;
                    board.Field[currY + 3, currX + 2] = 0;
                }

            }
            else if (id == 5)
            {
                if (p % 2 == 0)
                {
                    board.Field[currY + 2, currX + 2] = 0;
                    board.Field[currY + 3, currX + 3] = 0;
                }
                else
                {

                    board.Field[currY + 1, currX + 2] = 0;
                    board.Field[currY + 2, currX + 2] = 0;
                    board.Field[currY + 3, currX + 1] = 0;
                }

            }
            else if (id == 6)
            {
                board.Field[currY + 2, currX + 2] = 0;
                board.Field[currY + 3, currX + 2] = 0;
            }
            else
            {
                if (p == 0)
                {
                    board.Field[currY + 2, currX + 3] = 0;
                    board.Field[currY + 3, currX + 2] = 0;
                }
                else if (p == 1)
                {
                    board.Field[currY + 1, currX + 1] = 0;
                    board.Field[currY + 2, currX + 2] = 0;
                    board.Field[currY + 3, currX + 1] = 0;
                }
                else if (p == 2)
                {
                    board.Field[currY + 2, currX + 2] = 0;
                    board.Field[currY + 3, currX + 3] = 0;
                }
                else
                {
                    for (int i = 1; i < 4; i++)
                        board.Field[currY + i, currX + 2] = 0;
                }
            }
        }
        
    }


    public class GameBoard
    {
        protected int level;
        protected int[,] field;
        protected int score;
        protected int lines;
        protected Block currentBlock;

        #region properties
        public int Level
        {
            get
            {
                return level;
            }

            set
            {
                level = value;
            }
        }

        public int[,] Field
        {
            get
            {
                return field;
            }

            set
            {
                field = value;
            }
        }

        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
            }
        }

        public int Lines
        {
            get
            {
                return lines;
            }

            set
            {
                lines = value;
            }
        }

        public Block CurrBlock
        {
            get
            {
                return currentBlock;
            }

            set
            {
                currentBlock = value;
            }
        }
        #endregion
    
        public GameBoard()
        {
            chooseBlock();
            currentBlock.Y = -2;

            field = new int[18,10];
            level = 1;
            score = lines = 0;

        }
        public void chooseBlock()
        {
            Random rand = new Random();
            int num = rand.Next(1, 8);

            currentBlock = new Block(5);
            currentBlock.X = 4;
            currentBlock.Y = -2;
        }
        public Color colorPick(int i)
        {
            int id = i % 10;

            if(id == 1)
            {
                return Colors.Blue;
            }
            else if (id == 2)
            {
                return Colors.Red;
            }
            else if (id == 3)
            {
                return Colors.Green;
            }
            else if (id == 4)
            {
                return Colors.Yellow;
            }
            else if (id == 5)
            {
                return Colors.Purple;
            }
            else if (id == 6)
            {
                return Colors.Black;
            }
            else if (id == 7)
            {
                return Colors.DarkGreen;
            }
            else
                return Color.FromRgb(116, 61, 61);
        }

        public void drawField(Canvas play_area)
        {
            if (CurrBlock.Y < 0)
            {
                drawPieceOnCoords(CurrBlock.X, CurrBlock.Y);
            }
            
                for (int y = 0; y < 18; y++)
                {
                    for (int x = 0; x < 10; x++)
                    {


                        Color color = colorPick(field[y, x]);

                        //Taken from a stack overflow forum
                        Rectangle square = new Rectangle();
                        if (field[y, x] != 0)
                        {
                            square.Stroke = new SolidColorBrush(Colors.White);
                            square.StrokeThickness = 1;
                        }

                        square.Fill = new SolidColorBrush(color);
                        square.Width = 20;
                        square.Height = 20;
                        Canvas.SetLeft(square, x * 20);
                        Canvas.SetTop(square, y * 20);
                        play_area.Children.Add(square);

                    }
                }
            
            
        }

        
        public void moveBlock()
        {
            //we have a 10x18 field of zeros
            for(int y = 0; y < 15; y++)
            {
                
                for (int x = 0; x < 10; x++)
                {
                    
                    if (x == CurrBlock.X && y == CurrBlock.Y)
                    {
                        int currentY = y;
                        for (int by = 0; by < 4; by++)
                        {
                            
                            int currentX = x;
                            for (int bx = 0; bx < 4; bx++)
                            {
                                
                                if (currentBlock.Shape[by][bx] != 0)
                                {
                                    field[currentY, currentX] = currentBlock.Shape[by][bx];
                                    deleteTrailDown();

                                }
                                currentX++;    
                            }
                            currentY++;
                        }
                    
                    }
                }
            }
        }

        private void deleteTrailDown()
        {
            
            if (CurrBlock.Id == 1 && currentBlock.Pos == 0)
            {

                for (int x = CurrBlock.X; x < CurrBlock.X + 4; x++)
                {
                    
                    field[CurrBlock.Y+2, x] = 0;
                }
                
            }
            else if (CurrBlock.Id == 2 && currentBlock.Pos == 0)
            {
                field[CurrBlock.Y + 1, CurrBlock.X] = 0;
                for (int x = CurrBlock.X + 1; x < CurrBlock.X + 4; x++)
                {
                    
                    field[CurrBlock.Y + 2, x] = 0;
                }
            }
            else if (CurrBlock.Id == 3 && currentBlock.Pos == 0)
            {
                for (int x = CurrBlock.X; x < CurrBlock.X + 4; x++)
                {
                    field[CurrBlock.Y + 1, x] = 0;
                }
            }
            else if (CurrBlock.Id == 4 && currentBlock.Pos == 0)
            {
                field[CurrBlock.Y + 2, CurrBlock.X] = 0;
                for (int x = CurrBlock.X; x < CurrBlock.X + 3; x++)
                {
                    field[CurrBlock.Y + 1, x] = 0;
                }
            }
            else if (CurrBlock.Id == 5 && currentBlock.Pos == 0)
            {
                field[CurrBlock.Y + 2, CurrBlock.X + 2] = 0;
                for (int x = CurrBlock.X; x < CurrBlock.X + 3; x++)
                {
                    field[CurrBlock.Y + 1, x] = 0;
                }
            }
            else if (CurrBlock.Id == 6 && currentBlock.Pos == 0)
            {
                for (int x = CurrBlock.X; x < CurrBlock.X + 2; x++)
                {

                    field[CurrBlock.Y + 1, x] = 0;
                }
            }
            else if (CurrBlock.Id == 7 && currentBlock.Pos == 0)
            {
                for (int x = CurrBlock.X; x < CurrBlock.X + 3; x++)
                {
                    field[CurrBlock.Y + 1, x] = 0;
                }
            }

        }

        public bool checkBlockCollision()
        {
            bool collided = false;
            int width = CurrBlock.checkWidth();

            if(CurrBlock.Y > 0 && CurrBlock.Y < 14)
            {
                for (int y = CurrBlock.Y + 4; y > CurrBlock.Y+4 - y; y--)
                {
                    for (int x = CurrBlock.X; x < CurrBlock.X + width; x++)
                    {
                        if (Field[y, x] != 0)
                        {
                            if (Field[y - 1, x] != 0 && Field[y, x] != Field[y - 1, x])
                                collided = true;
                        }
                    }
                }
            }
            if(CurrBlock.Y == 14)
            {
                collided = true;
            }
            if(collided)
            {
                for (int y = 0; y < 18; y++)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        if (Field[y, x] > 0)
                            Field[y, x] += 10;
                    }
                }
            }
            return collided;
        }
        private void drawPieceOnCoords(int boardX, int boardY)
        {
            
            for (int y = 0; y <= 3; y++)
            {
                
                for (int x = 0; x < 4; x++)
                {
                    if (CurrBlock.Shape[y][x] != 0)
                    {
                        field[y + boardY, x + boardX] = CurrBlock.Shape[y][x];
                    }
                    
                }
                
            }

            if(boardY == -1)
            {
                deleteTrailDown();
            for (int i = 0; i < 10; i++)
                        {
                            field[0, i] = 0;
                        }
            }
            
            
        }
        
    }
}
