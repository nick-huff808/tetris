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

namespace TetrisProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class Block
    {
        Color color;
        int[][] shape;
        int id;
        int pos;
        #region properties

        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }

        public int[][] Shape
        {
            get
            {
                return shape;
            }

            set
            {
                shape = value;
            }
        }

        public int Pos
        {
            get
            {
                return pos;
            }

            set
            {
                pos = value;
            }
        }
        #endregion

        public Block(int i)
        {
            id = i;
            pos = 0;
            if (i == 1)
            {
                color = Colors.Blue;
                shape = new int[][] { new int[] { 0,0,0,0},
                                      new int[] { 0,0,0,0},
                                      new int[] { 1,1,1,1},
                                      new int[] { 0,0,0,0}};
            }
            else if (i == 2)
            {
                color = Colors.Red;
                shape = new int[][] { new int[] { 2, 0, 0, 0 },
                                      new int[] { 2, 2, 2, 2 },
                                      new int[] { 0, 0, 0, 0},
                                      new int[] { 0, 0, 0, 0}};
            }
            else if (i == 3)
            {
                color = Colors.Green;
                shape = new int[][] { new int[] { 3, 3, 3, 3 },
                                      new int[] { 0, 0, 0, 3 },
                                      new int[] { 0, 0, 0, 0 },
                                      new int[] { 0, 0, 0, 0 }};
            }
            else if (i == 4)
            {
                color = Colors.Yellow;
                shape = new int[][] { new int[] { 0, 0, 4, 4 },
                                      new int[] { 4, 4, 0, 0 },
                                      new int[] { 0 ,0 ,0 ,0 },
                                      new int[] { 0 ,0 ,0 ,0 }};
            }
            else if (i == 5)
            {
                color = Colors.Purple;
                shape = new int[][] { new int[] { 5, 5, 0, 0 },
                                      new int[] { 0, 0, 5, 5 },
                                      new int[] { 0 ,0 ,0 ,0 },
                                      new int[] { 0 ,0 ,0 ,0 }};

            }
            else if (i == 6)
            {
                color = Colors.Black;
                shape = new int[][] { new int[] { 0, 0, 6, 6 },
                                      new int[] { 0, 0, 6, 6 },
                                      new int[] { 0 ,0 ,0 ,0 },
                                      new int[] { 0 ,0 ,0 ,0 }};
            }
            else if (i == 7)
            {
                color = Colors.DarkGreen;
                shape = new int[][] { new int[] { 7, 7, 7, 0 },
                                      new int[] { 0, 7, 0, 0 },
                                      new int[] { 0 ,0 ,0 ,0 },
                                      new int[] { 0 ,0 ,0 ,0 }};
            }


        }


        private void Rotate(int i)
        {
            if (i == 1)
            {
                pos++;
                pos += pos % 4;
            }
            else
            {
                if (pos == 0)
                    pos = 3;
                else
                    pos--;
            }

            //if a line
            if (id == 1)
            {
                if (pos == 0 || pos == 2)
                {
                    shape = new int[][] { new int[] { 1,0,0,0},
                                          new int[] { 1,0,0,0},
                                          new int[] { 1,0,0,0},
                                          new int[] { 1,0,0,0}};
                }
                else
                {
                    shape = new int[][] { new int[] { 0,0,0,0},
                                      new int[] { 0,0,0,0},
                                      new int[] { 0,0,0,0},
                                      new int[] { 1,1,1,1}};
                }
            }
            //if backwards L
            else if (id == 2)
            {
                if (pos == 0)
                {
                    shape = new int[][] { new int[] { 2, 0, 0, 0 },
                                          new int[] { 2, 2, 2, 2 },
                                          new int[] { 0, 0, 0, 0},
                                          new int[] { 0, 0, 0, 0}};
                }
                else if (pos == 1)
                {
                    shape = new int[][] { new int[] { 2, 2, 0, 0 },
                                          new int[] { 2, 0, 0, 0 },
                                          new int[] { 2, 0, 0, 0},
                                          new int[] { 2, 0, 0, 0}};
                }
                else if (pos == 2)
                {
                    shape = new int[][] { new int[] { 2, 2, 2, 2 },
                                          new int[] { 0, 0, 0, 2 },
                                          new int[] { 0, 0, 0, 0},
                                          new int[] { 0, 0, 0, 0}};

                }
                else
                {
                    shape = new int[][] { new int[] { 0, 0, 0, 2 },
                                          new int[] { 0, 0, 0, 2 },
                                          new int[] { 0, 0, 0, 2},
                                          new int[] { 0, 0, 2, 2}};
                }
            }
            //if regular L
            if(id ==3)
            {
                if (pos == 0 || pos == 2)
                {
                    shape = new int[][] { new int[] { 1,0,0,0},
                                          new int[] { 1,0,0,0},
                                          new int[] { 1,0,0,0},
                                          new int[] { 1,0,0,0}};
                }
                else
                {
                    shape = new int[][] { new int[] { 0,0,0,0},
                                      new int[] { 0,0,0,0},
                                      new int[] { 0,0,0,0},
                                      new int[] { 1,1,1,1}};
                }
            }
            //if backwards L
            else if (id == 2)
            {
                if (pos == 0)
                {
                    shape = new int[][] { new int[] { 3, 3, 3, 3 },
                                          new int[] { 0, 0, 0, 3},
                                          new int[] { 0, 0, 0, 0},
                                          new int[] { 0, 0, 0, 0}};
                }
                else if (pos == 1)
                {
                    shape = new int[][] { new int[] { 0, 0, 3, 3 },
                                          new int[] { 0, 0, 0, 3 },
                                          new int[] { 0, 0, 0, 3},
                                          new int[] { 0, 0, 0, 3}};
                }
                else if (pos == 2)
                {
                    shape = new int[][] { new int[] { 2, 2, 2, 2 },
                                          new int[] { 0, 0, 0, 2 },
                                          new int[] { 0, 0, 0, 0},
                                          new int[] { 0, 0, 0, 0}};

                }
                else
                {
                    shape = new int[][] { new int[] { 0, 0, 0, 2 },
                                          new int[] { 0, 0, 0, 2 },
                                          new int[] { 0, 0, 0, 2},
                                          new int[] { 0, 0, 2, 2}};
                }
            }


        }
    }



    public class GameBoard
    {
        int level;
        int[][] field;
        int score;
        int lines;
        Block next;

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

        public int[][] Field
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

        public Block Next
        {
            get
            {
                return next;
            }

            set
            {
                next = value;
            }
        }
        #endregion


    }
}
