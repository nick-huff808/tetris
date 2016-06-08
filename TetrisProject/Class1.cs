using System;
using System.Windows.Controls;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TetrisProject
{
    public class GameBoard
    {
            protected int level;
            protected int[,] field;
            protected int score;
            private int highScore;
            protected int lines;
            private Block nextBlock;
            protected Block currentBlock;
            private Canvas nextDisplay;

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

            public int HighScore
            {
                get
                {
                    return highScore;
                }

                set
                {
                    highScore = value;
                }
            }
            #endregion

            public GameBoard(Canvas c)
            {
                currentBlock = chooseBlock();
                Random rand = new Random();
                this.NextBlock = new Block(rand.Next(1, 8) + 1 % 7);
                displayNextPiece(nextDisplay = c);
                field = new int[18, 10];
                level = 1;
                score = lines = 0;

            }
            public Block chooseBlock()
            {
                Random rand = new Random();
                int num = rand.Next(1, 8);

                Block newBlock = new Block(num);
                int rotation = rand.Next(0, 4);
                for (int i = 0; i < rotation; i++)
                    newBlock.Rotate(1);
                int height = newBlock.checkHeight();
                newBlock.Y = -4 + height;
                //if (num == 1)
                //    newBlock.Y = -3;
                //else
                //    newBlock.Y = -2;

                newBlock.X = rand.Next(1, 10 - newBlock.checkWidth());

                return newBlock;
            }
            public static Color colorPick(int i)
            {
                int id = i % 10;

                if (id == 1)
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
                    return Colors.SandyBrown;
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
                play_area.Children.Clear();
                for (int y = 0; y < 18; y++)
                {
                    for (int x = 0; x < 10; x++)
                    {

                        Rectangle square = new Rectangle();
                        square.Width = 20;
                        square.Height = 20;

                        //Taken from a stack overflow forum
                        if (field[y, x] != 0)
                        {
                            square.Stroke = new SolidColorBrush(Colors.White);
                            square.StrokeThickness = 1;
                        }

                        square.Fill = new SolidColorBrush(colorPick(field[y, x]));

                        Canvas.SetLeft(square, x * 20);
                        Canvas.SetTop(square, y * 20);
                        play_area.Children.Add(square);

                    }
                }


            }

            public void moveBlock()
            {
                drawPieceOnCoords(CurrBlock.X, CurrBlock.Y);
                deleteTrailIfMovingDown();
            }

            private void deleteTrailIfMovingDown()
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

            }
            private void deleteTrailRotate()
            {

                int width = CurrBlock.checkWidth();
                //compeletely delete the peiece from the board
                for (int y = 0; y < 4; y++)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        if (CurrBlock.Y + y >= 0 && CurrBlock.X + x <= 9 && Field[CurrBlock.Y + y, CurrBlock.X + x] < 10)
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

                //get the heigth and width if i rotated
                CurrBlock.Rotate(direction);
                int newWidth = CurrBlock.checkWidth();
                int newHeight = CurrBlock.checkHeight();
                CurrBlock.Rotate(-direction);

                //if the newwidth is not ganna fit dont rotate
                if (newWidth > 10 - CurrBlock.X || (newHeight > CurrBlock.Y + 4))
                {
                    return false;
                }

                #region check to see if area i want to rotate to is avalible
                if (id == 1)
                {
                    if (newPostion % 2 == 0)
                    {
                        if (field[y + 0, x + 3] > 10 || field[y + 1, x + 3] > 10 || field[y + 2, x + 3] > 10 || field[y + 3, x + 3] > 10)
                            canRotate = false;
                    }
                    else
                    {
                        if (field[y + 0, x + 0] > 10 || field[y + 1, x + 0] > 10 || field[y + 2, x + 0] > 10 || field[y + 3, x + 0] > 10)
                            canRotate = false;
                    }
                }
                else if (id == 2)
                {
                    if (newPostion == 0)
                    {
                        if (field[y + 2, x + 0] > 10 || field[y + 3, x + 0] > 10 || field[y + 3, x + 1] > 10 || field[y + 3, x + 2] > 10)
                            canRotate = false;
                    }
                    else if (newPostion == 1)
                    {
                        if (field[y + 3, x + 0] > 10 || field[y + 2, x + 0] > 10 || field[y + 1, x + 0] > 10 || field[y + 1, x + 1] > 10)
                            canRotate = false;
                    }
                    else if (newPostion == 2)
                    {
                        if (field[y + 2, x + 0] > 10 || field[y + 2, x + 1] > 10 || field[y + 2, x + 2] > 10 || field[y + 3, x + 2] > 10)
                            canRotate = false;
                    }
                    else if (newPostion == 3)
                    {
                        if (field[y + 3, x + 0] > 10 || field[y + 1, x + 1] > 10 || field[y + 2, x + 1] > 10 || field[y + 3, x + 1] > 10)
                            canRotate = false;
                    }

                }
                else if (id == 3)
                {
                    if (newPostion == 0)
                    {
                        if (field[y + 2, x + 0] > 10 || field[y + 2, x + 1] > 10 || field[y + 2, x + 2] > 10 || field[y + 3, x + 0] > 10)
                            canRotate = false;
                    }
                    else if (newPostion == 1)
                    {
                        if (field[y + 1, x + 0] > 10 || field[y + 2, x + 0] > 10 || field[y + 3, x + 0] > 10 || field[y + 3, x + 1] > 10)
                            canRotate = false;
                    }
                    else if (newPostion == 2)
                    {
                        if (field[y + 3, x + 0] > 10 || field[y + 3, x + 1] > 10 || field[y + 3, x + 2] > 10 || field[y + 2, x + 2] > 10)
                            canRotate = false;
                    }
                    else if (newPostion == 3)
                    {
                        if (field[y + 1, x + 0] > 10 || field[y + 1, x + 1] > 10 || field[y + 2, x + 1] > 10 || field[y + 3, x + 1] > 10)
                            canRotate = false;
                    }


                }
                else if (id == 4)
                {
                    if (newPostion % 2 == 0)
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
                    if (newPostion % 2 == 0)
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
                    if (newPostion == 0)
                    {
                        if (field[y + 2, x + 0] > 10 || field[y + 2, x + 1] > 10 || field[y + 2, x + 2] > 10 || field[y + 3, x + 1] > 10)
                            canRotate = false;
                    }
                    else if (newPostion == 1)
                    {
                        if (field[y + 1, x + 0] > 10 || field[y + 2, x + 0] > 10 || field[y + 3, x + 0] > 10 || field[y + 2, x + 1] > 10)
                            canRotate = false;
                    }
                    else if (newPostion == 2)
                    {
                        if (field[y + 3, x + 0] > 10 || field[y + 3, x + 1] > 10 || field[y + 3, x + 2] > 10 || field[y + 2, x + 1] > 10)
                            canRotate = false;
                    }
                    else if (newPostion == 3)
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

                    for (int x = 0; x < CurrBlock.checkWidth(); x++)
                    {
                        if (CurrBlock.Shape[y][x] != 0)
                        {
                            field[y + boardY, x + boardX] = CurrBlock.Shape[y][x];
                        }

                    }

                }


            }

            public bool checkBlockCollisionDown()
            {
                bool collided = false;
                int width = CurrBlock.checkWidth();
                int height = CurrBlock.checkHeight();

                if (CurrBlock.Y < 14)
                {
                    for (int y = CurrBlock.Y + 4; y > CurrBlock.Y + 4 - y; y--)
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
                if (CurrBlock.Y == 14)
                {
                    collided = true;
                }
                if (collided)
                {
                    //set the board to stone so we know what squares were currently there for the next current block
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

                if (CurrBlock.X > 0 && CurrBlock.Y > -1)
                {
                    for (int x = CurrBlock.X - 1; x < CurrBlock.X + CurrBlock.checkWidth(); x++)
                    {
                        for (int y = CurrBlock.Y; y < CurrBlock.Y + 4; y++)
                        {
                            if (Field[y, x] != 0 && x > 0)
                            {
                                if (Field[y, x - 1] != 0 && Field[y, x] != Field[y, x - 1])
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
                    for (int x = CurrBlock.X + CurrBlock.checkWidth() - 1; x > CurrBlock.X - 1; x--)
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

                for (int y = CurrBlock.Y; y < CurrBlock.Y + 4; y++)
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

                        //move everything above that line down
                        for (int y2 = y; y2 > 0; y2--)
                        {
                            for (int x2 = 0; x2 < 10; x2++)
                            {
                                Field[y2, x2] = Field[y2 - 1, x2];
                            }
                        }
                    }
                }
                if (lineAmount > 0)
                {
                    score += ((100 * lineAmount) + (50 * (lineAmount - 1))) * level;
                    if (score > highScore)
                    {
                        highScore = score;
                    }
                }
            }

            public void displayNextPiece(Canvas next_block_display)
            {
                next_block_display.Children.Clear();
                for (int y = 0; y < 4; y++)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        Color color = colorPick(this.nextBlock.Shape[y][x]);

                        //Taken from a stack overflow forum
                        Rectangle square = new Rectangle();
                        if (this.nextBlock.Shape[y][x] != 0)
                        {
                            square.Stroke = new SolidColorBrush(Colors.White);
                            square.StrokeThickness = 1;
                        }

                        if (this.nextBlock.Shape[y][x] == 0)
                        {
                            square.Fill = new SolidColorBrush(Color.FromRgb(46, 50, 30));
                        }
                        else
                        {
                            square.Fill = new SolidColorBrush(color);
                        }
                        square.Width = 20;
                        square.Height = 20;
                        Canvas.SetLeft(square, (x + 2) * 20);
                        Canvas.SetTop(square, y * 20);
                        next_block_display.Children.Add(square);
                    }
                }
            }

            public void moveToNextPeice()
            {
                CurrBlock = this.nextBlock;
                this.nextBlock = chooseBlock();
                displayNextPiece(nextDisplay);
            }

            public void clearField()
            {
                for (int y = 0; y < 18; y++)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        field[y, x] = 0;
                    }
                }
            }

            public void levelUp()
            {

                clearField();
                drawPieceOnCoords(CurrBlock.X, CurrBlock.Y);
                Lines = 0;
                level++;

            }

        }
}
