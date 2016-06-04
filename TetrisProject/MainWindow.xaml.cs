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
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private bool paused = true;
        GameBoard board = new GameBoard();

        public MainWindow()
        {
            InitializeComponent();          
            board.drawField(play_area);
            dispatcherTimer.Tick += new EventHandler(timerTick);
            
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dispatcherTimer.Start();
        }
        private void timerTick(object sender, EventArgs e)
        {
            if(board.CurrBlock.Y == 15)
            {
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
                dispatcherTimer.Start();
                pause_button.Content = "Pause";
                paused = false;
            }
            else
            {
                dispatcherTimer.Stop();
                pause_button.Content = "Resume";
                paused = true;
            }

        }

        private void play_area_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                if (board.CurrBlock.X != 0)
                {
                    board.CurrBlock.X--;
                }
                
                    
            }
            else if (e.Key == Key.Right)
            {

                if (board.CurrBlock.X != 9)
                {
                    board.CurrBlock.X++;
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
            currentBlock.Y = -1;

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
            currentBlock.Y = 0;
        }
        public Color colorPick(int i)
        {
            if(i == 1)
            {
                return Colors.Blue;
            }
            else if (i == 2)
            {
                return Colors.Red;
            }
            else if (i == 3)
            {
                return Colors.Green;
            }
            else if (i == 4)
            {
                return Colors.Yellow;
            }
            else if (i == 5)
            {
                return Colors.Purple;
            }
            else if (i == 6)
            {
                return Colors.Black;
            }
            else if (i == 7)
            {
                return Colors.DarkGreen;
            }
            else
                return Color.FromRgb(116, 61, 61);
        }
        public void drawField(Canvas play_area)
        {
            for(int y = 0; y < 18; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    Color color = colorPick(field[y, x]);

                    //Taken from a stack overflow forum
                    Rectangle square = new Rectangle();
                    if (field[y,x] != 0)
                    {
                        square.Stroke = new SolidColorBrush(Colors.White);
                        square.StrokeThickness = 1;
                    }
                    
                    square.Fill = new SolidColorBrush(color);
                    square.Width = 20;
                    square.Height = 20;
                    Canvas.SetLeft(square, x*20);
                    Canvas.SetTop(square, y*20);
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
                                    deleteTrailDown(currentBlock, field);

                                }
                                currentX++;    
                            }
                            currentY++;
                        }
                    
                    }
                }
            }
        }

        private void deleteTrailDown(Block currentBlock, int[,] field)
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

                    field[CurrBlock.Y + 2, x] = 0;
                }
            }
            else if (CurrBlock.Id == 7 && currentBlock.Pos == 0)
            {
                for (int x = CurrBlock.X; x < CurrBlock.X + 3; x++)
                {

                    field[CurrBlock.Y + 2, x] = 0;
                }
            }

        }

        
    }
}
