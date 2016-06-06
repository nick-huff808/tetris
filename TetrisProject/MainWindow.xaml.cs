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
            drawNextPiece(next_block_display, board.NextBlock);

            if(board.CurrBlock.Y == 14 || (board.CurrBlock.Y != 14 && board.checkBlockCollision() == true))
            {

                board.checkBlockCollision();
                board.checkForLineDeleteAndMove();

                lines_text.Text = board.Lines + "";
                score_text.Text = board.Score + "";

                board.CurrBlock = board.NextBlock;
                board.NextBlock = board.chooseBlock();
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
            if (board.CurrBlock.Y == 14)
            {
                return;
            }
            #region left and right movement
            else if (e.Key == Key.Left && board.checkBlockCollisionToLeft() == false)
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
            else if (e.Key == Key.Right && board.checkBlockCollisionToRight() == false)
            {

                if (board.CurrBlock.X >= 10 - pWidth)
                {
                    board.CurrBlock.X = 10 - pWidth;
                }
                else
                {
                    board.CurrBlock.X++;
                }
                board.moveBlock();
                deleteTrailRight();
            }

            #endregion

            #region rotational movement
            else if (e.Key == Key.Up)
            {
                if (board.CurrBlock.Y >= 0)
                {
                    board.rotateBlock(1);
                    
                }
            }
            else if (e.Key == Key.Down)
            {
                if (board.CurrBlock.Y >= 0)
                {

                    board.rotateBlock(-1);
                }
            }
            #endregion

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
                    board.Field[currY + 1, currX -1] = 0;
                    board.Field[currY + 2, currX - 1] = 0;
                    board.Field[currY + 3, currX - 1] = 0;
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
                    board.Field[currY + 2, currX + 3] = 0;
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
                    board.Field[currY + 2, currX + 3] = 0;
                    board.Field[currY + 3, currX + 3] = 0;
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

        public void drawNextPiece(Canvas next_block_display, Block nextPiece)
        {
            for(int y = 0; y < 4; y++)
            {
                for(int x = 0; x < 4; x++)
                {
                    Color color = GameBoard.colorPick(nextPiece.Shape[y][x]);

                    //Taken from a stack overflow forum
                    Rectangle square = new Rectangle();
                    if (nextPiece.Shape[y][x] != 0)
                    {
                        square.Stroke = new SolidColorBrush(Colors.White);
                        square.StrokeThickness = 1;
                    }

                    if(nextPiece.Shape[y][x] == 0)
                    {
                        square.Fill = new SolidColorBrush(Color.FromRgb(46, 50, 30));
                    }
                    else
                    {
                        square.Fill = new SolidColorBrush(color);
                    }
                    square.Width = 20;
                    square.Height = 20;
                    Canvas.SetLeft(square, x * 19);
                    Canvas.SetTop(square, y * 19);
                    next_block_display.Children.Add(square);
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
        private Block nextBlock;
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

        public Block NextBlock
        {
            get
            {
                return nextBlock;
            }

            set
            {
                nextBlock = value;
            }
        }
        #endregion

        public GameBoard()
        {
            currentBlock = chooseBlock();
            NextBlock = chooseBlock();

            currentBlock.Y = -2;

            field = new int[18,10];
            level = 1;
            score = lines = 0;

        }
        public Block chooseBlock()
        {
            Random rand = new Random();
            int num = rand.Next(1, 8);

            Block newBlock = new Block(num);
            newBlock.X = 4;
            newBlock.Y = -2;
            return newBlock;
        }
        public static Color colorPick(int i)
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
            drawPieceOnCoords(CurrBlock.X, CurrBlock.Y);
            deleteTrailDown();
            /*
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
                                    //deleteTrailDown();

                                }
                                currentX++;    
                            }
                            currentY++;
                        }
                    
                    }
                }
            } */
        }

        private void deleteTrailDown()
        {
            if (CurrBlock.Pos % 2 == 1 && CurrBlock.Id == 1)
            {
                if (CurrBlock.Y > 0)
                {
                    field[CurrBlock.Y - 1, CurrBlock.X] = 0;
                }
            }
            else
            {
                deleteTrailRotate();
            }

            #region old
            /*
            if (CurrBlock.Id == 1)
            {

                if (CurrBlock.Pos % 2 == 0)
                {

                    for (int x = CurrBlock.X; x < CurrBlock.X + 4; x++)
                    {
                        if (field[CurrBlock.Y + 2, x] < 10)
                            field[CurrBlock.Y + 2, x] = 0;
                    }
                }
                else
                {
                    if (field[CurrBlock.Y - 1, 0] < 10)
                        field[CurrBlock.Y - 1, 0] = 0;
                }
            }

        
            else if (CurrBlock.Id == 2 && currentBlock.Pos == 0)
            {
                if (field[CurrBlock.Y + 1, CurrBlock.X] < 10)
                    field[CurrBlock.Y + 1, CurrBlock.X] = 0;
                for (int x = CurrBlock.X + 1; x < CurrBlock.X + 3; x++)
                {
                    if (field[CurrBlock.Y + 2, x] < 10)
                        field[CurrBlock.Y + 2, x] = 0;
                }
            }
            else if (CurrBlock.Id == 3 && currentBlock.Pos == 0)
            {
                for (int x = CurrBlock.X; x < CurrBlock.X + 3; x++)
                {
                    if (field[CurrBlock.Y + 1, x] < 10)
                        field[CurrBlock.Y + 1, x] = 0;
                }
            }
            else if (CurrBlock.Id == 4 && currentBlock.Pos == 0)
            {
                if (field[CurrBlock.Y + 2, CurrBlock.X] < 10)
                    field[CurrBlock.Y + 2, CurrBlock.X] = 0;
                for (int x = CurrBlock.X; x < CurrBlock.X + 3; x++)
                {
                    if (field[CurrBlock.Y + 1, x] < 10)
                        field[CurrBlock.Y + 1, x] = 0;
                }
            }
            else if (CurrBlock.Id == 5 && currentBlock.Pos == 0)
            {
                if (field[CurrBlock.Y + 2, CurrBlock.X + 2] < 10)
                    field[CurrBlock.Y + 2, CurrBlock.X + 2] = 0;
                for (int x = CurrBlock.X; x < CurrBlock.X + 3; x++)
                {
                    if (field[CurrBlock.Y + 1, x] < 10)
                        field[CurrBlock.Y + 1, x] = 0;
                }
            }
            else if (CurrBlock.Id == 6 && currentBlock.Pos == 0)
            {
                for (int x = CurrBlock.X; x < CurrBlock.X + 2; x++)
                {
                    if (field[CurrBlock.Y + 1, x] < 10)
                        field[CurrBlock.Y + 1, x] = 0;
                }
            }
            else if (CurrBlock.Id == 7 && currentBlock.Pos == 0)
            {
                for (int x = CurrBlock.X; x < CurrBlock.X + 3; x++)
                {
                    if (field[CurrBlock.Y + 1, x] < 10)
                        field[CurrBlock.Y + 1, x] = 0;
                }
            }*/
            #endregion
        }
        private void deleteTrailRotate()
        {

            int width = CurrBlock.checkWidth();
            //compeletely delete the peiece from the board
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x <4; x++)
                {
                    if (CurrBlock.Y + y >=0 && CurrBlock.X +x <= 9 && Field[CurrBlock.Y + y, CurrBlock.X + x] < 10)
                        Field[CurrBlock.Y + y, CurrBlock.X + x] = 0;
                }
            }
            //now redraw it with the oriantation
            drawPieceOnCoords(CurrBlock.X, CurrBlock.Y);
         
        }
        

        public bool rotateBlock(int direction)
        {
            int id = CurrBlock.Id, pos = CurrBlock.Pos;
            int newPostion = incrementPos(pos, direction);
            bool canRotate = true;
            int y = CurrBlock.Y, x = CurrBlock.X;

            //get the width if i rotated
            CurrBlock.Rotate(direction);
            int newWidth = CurrBlock.checkWidth();
            CurrBlock.Rotate(-direction);

            //if the newwidth is not ganna fit dont rotate
            if (newWidth > 10 - CurrBlock.X || y <-1)
            {
                return false;
            }

            #region check to see if area i want to rotate to is avalible
            if (id == 1)
            {
                if (newPostion % 2 == 0)
                {
                    for (int i = 1; i < 4; i++)
                    {
                        if (Field[3 + y, i + x] != 0)
                        {
                            canRotate = false;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (Field[i + 3, 0 + x] != 0)
                        {
                            canRotate = false;
                        }
                    }
                }
            }
            else if (id == 2)
            {
                if (pos == 0)
                {
                    if (field[y + 2, x + 0] > 10 || field[y + 3, x + 0] > 10 || field[y + 3, x + 1] > 10 || field[y + 3, x + 2] > 10)
                        canRotate = false;
                }
                else if (pos == 1)
                {
                    if (field[y + 3, x + 0] > 10 || field[y + 2, x + 0] > 10 || field[y + 1, x + 0] > 10 || field[y + 1, x + 1] > 10)
                        canRotate = false;
                }
                else if (pos == 2)
                {
                    if (field[y + 2, x + 0] > 10 || field[y + 2, x + 1] > 10 || field[y + 2, x + 2] > 10 || field[y + 3, x + 2] > 10)
                        canRotate = false;
                }
                else if (pos == 3)
                {
                    if (field[y + 3, x + 0] > 10 || field[y + 1, x + 1] > 10 || field[y + 2, x + 1] > 10 || field[y + 3, x + 1] > 10)
                        canRotate = false;
                }

            }
            else if (id == 3)
            {
                if (pos == 0)
                {
                    if (field[y + 2, x + 0] > 10 || field[y + 2, x + 1] > 10 || field[y + 2, x + 2] > 10 || field[y + 3, x + 0] > 10)
                        canRotate = false;
                }
                else if (pos == 1)
                {
                    if (field[y + 1, x + 0] > 10 || field[y + 2, x + 0] > 10 || field[y + 3, x + 0] > 10 || field[y + 3, x + 1] > 10)
                        canRotate = false;
                }
                else if (pos == 2)
                {
                    if (field[y + 3, x + 0] > 10 || field[y + 3, x + 1] > 10 || field[y + 3, x + 2] > 10 || field[y + 2, x + 2] > 10)
                        canRotate = false;
                }
                else if (pos == 3)
                {
                    if (field[y + 1, x + 0] > 10 || field[y + 1, x + 1] > 10 || field[y + 2, x + 1] > 10 || field[y + 3, x + 1] > 10)
                        canRotate = false;
                }


            }
            else if (id == 4)
            {
                if (pos % 2 == 0)
                {
                    if (field[y + 3, x + 0] > 10 || field[y + 3, x + 1] > 10 || field[y + 2, x + 1] > 10 || field[y + 2, x + 2] > 10)
                        canRotate = false;
                }
                else
                {
                    if (field[y + 1, x + 0] > 10 || field[y + 2, x + 0] > 10 || field[y + 2, x + 1] > 10 || field[y + 3, x + 1] > 10)
                        canRotate = false;
                }
            }
            else if (id == 5)
            {
                if (pos % 2 == 0)
                {
                    if (field[y + 2, x + 0] > 10 || field[y + 2, x + 1] > 10 || field[y + 3, x + 1] > 10 || field[y + 3, x + 2] > 10)
                        canRotate = false;
                }
                else
                {
                    if (field[y + 2, x + 0] > 10 || field[y + 3, x + 0] > 10 || field[y + 2, x + 1] > 10 || field[y + 1, x + 1] > 10)
                        canRotate = false;
                }
            }
            else if (id == 6)
                return false;
            else if (id == 7)
            {
                if (pos == 0)
                {
                    if (field[y + 2, x + 0] > 10 || field[y + 2, x + 1] > 10 || field[y + 2, x + 2] > 10 || field[y + 3, x + 1] > 10)
                        canRotate = false;
                }
                else if (pos == 1)
                {
                    if (field[y + 1, x + 0] > 10 || field[y + 2, x + 0] > 10 || field[y + 3, x + 0] > 10 || field[y + 2, x + 1] > 10)
                        canRotate = false;
                }
                else if (pos == 2)
                {
                    if (field[y + 3, x + 0] > 10 || field[y + 3, x + 1] > 10 || field[y + 3, x + 2] > 10 || field[y + 2, x + 1] > 10)
                        canRotate = false;
                }
                else if (pos == 3)
                {
                    if (field[y + 2, x + 0] > 10 || field[y + 1, x + 1] > 10 || field[y + 2, x + 1] > 10 || field[y + 3, x + 1] > 10)
                        canRotate = false;
                }
            }
            #endregion

            if (canRotate)
            {
                CurrBlock.Rotate(direction);
                deleteTrailRotate();
                
            }
            return canRotate;

        }
        public static int incrementPos(int pos, int direction)
        {
            int newPos = pos + direction;
            if (newPos < 0)
            {
                newPos = 3;
            }
            else if (newPos == 4)
                newPos = 0;
            return newPos;
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
                //deleteTrailDown();
                for (int i = 0; i < 10; i++)
                {
                    field[0, i] = 0;
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
                            if (Field[y - 1, x] != 0 && Field[y, x] > Field[y - 1, x])
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
        public bool checkBlockCollisionToLeft()
        {
            bool collided = false;

            if (CurrBlock.X > 0  && CurrBlock.Y > -1)
            {
                for (int x = CurrBlock.X - 1; x < CurrBlock.X + 2; x++)
                {
                    for (int y = CurrBlock.Y; y < CurrBlock.Y + 4; y++)
                    {                       
                        if (Field[y,x] != 0 && x > 0)
                        {
                            if (Field[y, x-1] != 0 && Field[y,x] != Field[y, x-1])
                                collided = true;
                        }
                    }
                }
            }
            return collided;
        }
        public bool checkBlockCollisionToRight()
        {
            bool collided = false;

            if (CurrBlock.X < 10 - CurrBlock.checkWidth() && CurrBlock.Y < 14 && CurrBlock.Y > -1)
            {
                for (int x = CurrBlock.X + CurrBlock.checkWidth()-1; x > CurrBlock.X-1; x--)
                {
                    for (int y = CurrBlock.Y; y < CurrBlock.Y + 4; y++)
                    {
                        if (Field[y, x] != 0)
                        {
                            if (Field[y, x + 1] != 0 && Field[y, x] != Field[y, x + 1])
                                collided = true;
                        }
                    }
                }
            }
            return collided;
        }
        
        public void checkForLineDeleteAndMove()
        {
            bool isLine = true;
            int lineAmount = 0;

            for(int y = CurrBlock.Y; y < CurrBlock.Y + 4; y++)
            {
                isLine = true;
                for (int x = 0; x < 10; x++)
                {
                    if (Field[y, x] == 0)
                        isLine = false;
                }
                if (isLine == true)
                {
                    lineAmount++;
                    this.lines++;

                    for (int y2 = y; y2 > 0; y2--)
                    {
                        for(int x2 = 0; x2 < 10; x2++)
                        {
                            Field[y2, x2] = Field[y2 - 1, x2];
                        }
                    }
                }
            }
            if(lineAmount > 0)
                score += ((100 * lineAmount) + (50 * (lineAmount - 1))) * level;
        }
    }
}
