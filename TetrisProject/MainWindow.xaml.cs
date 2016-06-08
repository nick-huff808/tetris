using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TetrisProject
{
    /// <summary> 
    /// 
    /// Interaction logic for MainWindow.xaml
    /// the board is 10x18
    /// where each square is 20x 20 pixels 
    /// </summary>

    public partial class MainWindow : Window
    {
        private DispatcherTimer movementDownTimer;
        
        private bool paused = true;
        bool lost = false;

        GameBoard board;
        int TIMER_SPEED = 500;
        public MainWindow()
        {
            InitializeComponent();
            movementDownTimer = new DispatcherTimer();
            //board = new GameBoard(next_block_display);


            movementDownTimer.Tick += new EventHandler(downwardTick);
            movementDownTimer.Interval = new TimeSpan(0, 0, 0, 0, TIMER_SPEED);
            //movementDownTimer.Start();

        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }
        public void lose()
        {
            loseLabel.Visibility = Visibility.Visible;
            board.Lines = 0;
            board.Level = 0;
            TIMER_SPEED = 500;
            lost = true;
            pause.IsEnabled = false;
            play.IsEnabled = false;
            
            movementDownTimer.Stop();
            
            
        }
        private void downwardTick(object sender, EventArgs e)
        {

            
            if (board.checkBlockCollisionDown() && board.CurrBlock.Y < 0)
                lose();   
            
            else if(board.checkBlockCollisionDown())
            {               
                board.checkForLineDeleteAndMove();
                board.moveToNextPeice();
                
                //check to see if level up and if yes do it
                if (board.Lines >= 10)
                {
                    board.levelUp();
                    TIMER_SPEED = (int)(TIMER_SPEED * .75);
                    movementDownTimer.Interval = new TimeSpan(0, 0, 0, 0, TIMER_SPEED);
                    level_text.Text = board.Level + "";
                }

                lines_text.Text = board.Lines + "";
                score_text.Text = board.Score + "";
                high_score_text.Text = board.HighScore + "";

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
            paused = !paused;
            if (paused == true)
            {
                movementDownTimer.Stop();
                play.IsEnabled = true;
                pause.IsEnabled = false;
                Save.IsEnabled = true;
                
                load.IsEnabled = true;
                pauseBox.Text = "Game is Paused";
                
                

            }
            else
            {
                movementDownTimer.Start();
                play.IsEnabled = false;
                pause.IsEnabled = true;
                Save.IsEnabled = false;
                load.IsEnabled = false;
                pauseBox.Text = "Game is Running";
            }

        }

        private void play_area_KeyDown(object sender, KeyEventArgs e)
        {
            if (board != null)
            {
                int pWidth = board.CurrBlock.checkWidth();
                if (board.CurrBlock.Y == 14)
                {
                    return;
                }
                #region left and right movement
                else if (e.Key == Key.Left && board.checkBlockCollisionToLeft() == false && board.CurrBlock.X != 0 && !lost && !paused)
                {
                    board.CurrBlock.X--;
                    board.moveBlock();
                    deleteTrailIfMovingLeft();
                }
                else if (e.Key == Key.Right && board.checkBlockCollisionToRight() == false && board.CurrBlock.X + pWidth != 10 && !lost && !paused)
                {

                    board.CurrBlock.X++;
                    board.moveBlock();
                    deleteTrailIfMovingRight();
                }

                #endregion
                else if (e.Key == Key.Space && !paused)
                {
                    while (board.checkBlockCollisionDown() == false)
                    {
                        board.CurrBlock.Y++;
                        board.moveBlock();
                    }
                }
                else if (e.Key == Key.Home)
                {
                    board.levelUp();
                    TIMER_SPEED = (int)(TIMER_SPEED * .75);
                    movementDownTimer.Interval = new TimeSpan(0, 0, 0, 0, TIMER_SPEED);
                    level_text.Text = board.Level + "";
                }

                #region rotational movement
                else if (e.Key == Key.Up && !paused)
                {
                    //if (board.CurrBlock.Y >= 0)
                    //{
                        board.rotateBlock(1);
                    
                    //}
                }
                else if (e.Key == Key.Down && !paused)
                {
                    //if (board.CurrBlock.Y >= 0)
                    //{

                        board.rotateBlock(-1);
                    //}
                }
                #endregion

                board.drawField(play_area);

            }
            
            
        }

        private void deleteTrailIfMovingRight()
        {
                int id = board.CurrBlock.Id;
                int p = board.CurrBlock.Pos;
                int currX = board.CurrBlock.X;
                int currY = board.CurrBlock.Y;
                if (id == 1)
                {

                    if (p % 2 == 0)
                    {
                        board.Field[currY + 3, currX - 1] = 0;
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
                        board.Field[currY + 3, currX - 1] = 0;
                        board.Field[currY + 2, currX - 1] = 0;
                    }
                    else if (p == 1)
                    {
                        board.Field[currY + 1, currX - 1] = 0;
                        board.Field[currY + 2, currX - 1] = 0;
                        board.Field[currY + 3, currX - 1] = 0;
                    }
                    else if (p == 2)
                    {
                        board.Field[currY + 2, currX - 1] = 0;
                        board.Field[currY + 3, currX + 1] = 0;
                    }
                    else
                    {
                        board.Field[currY + 1, currX] = 0;
                        board.Field[currY + 2, currX] = 0;
                        board.Field[currY + 3, currX - 1] = 0;
                    }
                }
                else if (id == 3)
                {
                    if (p == 0)
                    {
                        board.Field[currY + 3, currX - 1] = 0;
                        board.Field[currY + 2, currX - 1] = 0;
                    }
                    else if (p == 1)
                    {
                        board.Field[currY + 1, currX - 1] = 0;
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
                        board.Field[currY + 2, currX] = 0;
                        board.Field[currY + 3, currX] = 0;
                    }
                }
                else if (id == 4)
                {
                    if (p % 2 == 0)
                    {
                        board.Field[currY + 2, currX] = 0;
                        board.Field[currY + 3, currX - 1] = 0;
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
                        board.Field[currY + 2, currX - 1] = 0;
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
                    board.Field[currY + 2, currX - 1] = 0;
                    board.Field[currY + 3, currX - 1] = 0;
                }
                else
                {
                    if (p == 0)
                    {
                        board.Field[currY + 2, currX - 1] = 0;
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
                        board.Field[currY + 3, currX - 1] = 0;
                    }
                    else
                    {
                        board.Field[currY + 1, currX] = 0;
                        board.Field[currY + 2, currX - 1] = 0;
                        board.Field[currY + 3, currX] = 0;
                    }
                }
        }

        private void deleteTrailIfMovingLeft()
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

        #region keyboard functions 
        private void About_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            pause_Executed(sender, e);
            Custom_Message_Box wi = new Custom_Message_Box("Adam Dodgen and Nick Huff" + Environment.NewLine + "CSCD 371" + Environment.NewLine + "CPU: any" + Environment.NewLine + "Target Framework: 4.5.2", "About");
            wi.Show();
        }
        private void resume_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (play.IsEnabled)
            {
                pause_button_Click(sender, e);                
            }
        }
        private void pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (pause.IsEnabled)
            {
                pause_button_Click(sender, e);
              
                
            }
        
        }
        private void control_Help_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            pause_Executed(sender, e);
            Custom_Message_Box w1 = new Custom_Message_Box(
                "Rules:" + Environment.NewLine +
                "You must select a new game or load an old one in order to begin laying." + Environment.NewLine +
                "You must try to position the pieces so they form a complete, unbroken line across the screen." + Environment.NewLine +
                "The pieces will fall at a constant speed, and you can move them left, right, or rotate them" + Environment.NewLine +
                "either clockwise or counter - clockwise.Each time a solid line is created the line will disappear" + Environment.NewLine +
                "and all of the rows above it will be moved down.The more lines you form at once the more points you get." + Environment.NewLine +
                "Once you have formed 10 lines, the board is cleared and you will level up, increasing the speed" + Environment.NewLine +
                "at which the pieces fall. The higher the level the more points possible can be achieved." + Environment.NewLine +
                "You can also save your game, as well as pause it at any time." + Environment.NewLine
                 + Environment.NewLine +
                "Controls: " + Environment.NewLine +
                "Left / Right keys - moves the piece left or right." + Environment.NewLine +
                "up / down keys - moves the piece clockwise or counter - clockwise." + Environment.NewLine +
                "cntrl + N - New Game." + Environment.NewLine +
                "cntrl + P - Pauses the game" + Environment.NewLine +
                "cntrl + G - Resumes the game" + Environment.NewLine +
                "Home button -Aa cheat to automatically level up as many times as desired.", "Help and Controls");
            w1.Show();
            w1 = null;
        }
        private void new_game_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (newGame.IsEnabled)
            {
                loseLabel.Visibility = Visibility.Hidden;
                board = new GameBoard(next_block_display);
                board.drawField(play_area);
                movementDownTimer.Interval = new TimeSpan(0, 0, 0, 0, TIMER_SPEED = 500);

                paused = false;
                pauseBox.Text = "Game is Running";
                lines_text.Text = "0";
                level_text.Text = "1";
                score_text.Text = "0";
                play.IsEnabled = false;
                pause.IsEnabled = true;
                Save.IsEnabled = false;
                load.IsEnabled = false;
                movementDownTimer.Start();
            }
        }
        //following 2 functions are wrtien with help from MSDN
        private void save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Custom_Message_Box w1 = new Custom_Message_Box("", "");
            if (Save.IsEnabled)
            {
                //check to see if file exists and if it doesnt create it
                try
                {
                    if (!File.Exists("Tetris.txt"))
                    {
                        File.CreateText("Tetris.txt");
                    }
                    StreamWriter file = new StreamWriter("Tetris.txt");

                    //write high score and game to the file
                    file.WriteLine(high_score_text.Text);
                    file.WriteLine(lines_text.Text);
                    file.WriteLine(score_text.Text);
                    file.WriteLine(level_text.Text);

                    for (int y = 0; y < 18; y++)
                    {
                        for (int x = 0; x < 10; x++)
                        {
                            if (board.Field[y, x] < 10)
                                file.Write(0);
                            else
                                file.Write(board.Field[y, x] % 10);
                        }
                        file.WriteLine();
                    }
                    file.Close();
                    w1 = new Custom_Message_Box("game saved", "Save");
                    w1.Show();
                }
                catch
                {
                    w1 = new Custom_Message_Box("failed to save game", "Save");
                    w1.Show();
                }
            }
        }
        private void load_game_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Custom_Message_Box w1;
            loseLabel.Visibility = Visibility.Hidden;
            if (load.IsEnabled)
            {
                try
                {
                    if (!File.Exists("Tetris.txt"))
                    {
                        w1 = new Custom_Message_Box("No game to load", "Load");
                        w1.Show();
                        return;
                    }
                    //open the file
                    StreamReader file = new StreamReader("Tetris.txt");
                    
                    //load old high score
                    high_score_text.Text = file.ReadLine();
                    board.HighScore = Int32.Parse(high_score_text.Text);

                    //load old completed lines
                    lines_text.Text = file.ReadLine();
                    board.Lines = Int32.Parse(lines_text.Text);

                    //load old score
                    score_text.Text = file.ReadLine();
                    board.Score = Int32.Parse(score_text.Text);

                    //load level
                    level_text.Text = file.ReadLine();
                    board.Level = Int32.Parse(level_text.Text);
                    TIMER_SPEED = (int)(500 * Math.Pow(.75, board.Level -1));
                    movementDownTimer.Interval = new TimeSpan(0, 0, 0, 0, TIMER_SPEED);

                    //fill in the board
                    for (int y = 0; y < 18; y++)
                    {
                        for (int x = 0; x < 10; x++)
                        {
                            char c = (char)file.Read();
                            int num = c - '0';
                            if (num != 0)
                            {

                                board.Field[y, x] = num + 10;
                            }
                            else
                                board.Field[y, x] = 0;
                        }
                        file.ReadLine();
                    }
                    file.Close();

                    //board.moveToNextPeice();
                    Random rand = new Random();
                    board.NextBlock = new Block(rand.Next(1, 7));
                    board.moveToNextPeice();
                    w1 = new Custom_Message_Box("game loaded", "Load");
                    w1.Show();

                    board.drawField(play_area);
                    board.displayNextPiece(next_block_display);
                }
                catch
                {
                    w1 = new Custom_Message_Box("no game to load", "Load");
                    w1.Show();
                }
            }
        }

        #endregion

        private void Exit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
    }
}
