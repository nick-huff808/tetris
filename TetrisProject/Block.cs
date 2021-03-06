﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TetrisProject
{
    public class Block
    {
        protected Color color;
        protected int[][] shape;
        private int id;
        protected int pos;
        protected int x;
        protected int y;
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

        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
        #endregion
        /*
         * 1 == l
         * 2 == backwards L
         * 3 ==  L
         * 4 == S
         * 5 == Z
         * 6 == square
         * 7 == t 
         */
        public Block(int id)
        {

            this.id = id;
            pos = 0;
            X = 4;
            Y = -1;
            #region setting id's
            if (id == 1)
            {
                color = Colors.Blue;
                shape = new int[][] { new int[] { 0,0,0,0},
                                      new int[] { 0,0,0,0},
                                      new int[] { 0,0,0,0},
                                      new int[] { 1,1,1,1} };
            }
            else if (id == 2)
            {
                color = Colors.Red;
                shape = new int[][] { new int[] { 0, 0, 0, 0},
                                      new int[] { 0, 0, 0, 0},
                                      new int[] { 2, 0, 0, 0 },
                                      new int[] { 2, 2, 2, 0 },
                                      };
            }
            else if (id == 3)
            {
                color = Colors.Green;
                shape = new int[][] { new int[] { 0, 0, 0, 0 },
                                      new int[] { 0, 0, 0, 0 },
                                      new int[] { 3, 3, 3, 0 },
                                      new int[] { 3, 0, 0, 0 }
                                      };
            }
            else if (id == 4)
            {
                color = Colors.Yellow;
                shape = new int[][] { new int[] { 0 ,0 ,0 ,0 },
                                      new int[] { 0 ,0 ,0 ,0 },
                                      new int[] { 0, 4, 4, 0 },
                                      new int[] { 4, 4, 0, 0 }
                                      };
            }
            else if (id == 5)
            {
                color = Colors.Purple;
                shape = new int[][] { new int[] { 0 ,0 ,0 ,0 },
                                      new int[] { 0 ,0 ,0 ,0 },
                                      new int[] { 5, 5, 0, 0 },
                                      new int[] { 0, 5, 5, 0 },
                                      };

            }
            else if (id == 6)
            {
                color = Colors.Black;
                shape = new int[][] { new int[] { 0, 0, 0, 0 },
                                      new int[] { 0, 0, 0, 0 },
                                      new int[] { 6 ,6 ,0 ,0 },
                                      new int[] { 6 ,6 ,0 ,0 }};
            }
            else
            {
                color = Colors.SandyBrown;
                shape = new int[][] { new int[] { 0 ,0 ,0 ,0 },
                                      new int[] { 0 ,0 ,0 ,0 },
                                      new int[] { 7, 7, 7, 0 },
                                      new int[] { 0, 7, 0, 0 },
                                      };
            }
            #endregion

        }


        public void Rotate(int rotation_direction)
        {
            if (rotation_direction == 1)
            {
                pos++;
                pos = pos % 4;
            }
            else
            {
                if (pos == 0)
                    pos = 3;
                else
                    pos--;
            }

            #region differnt postions
            //if a line
            if (this.id == 1)
            {
                if (pos == 1 || pos == 3)
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
                                          new int[] { 1,1,1,1}}
                                          ;
                }
            }
            //if backwards L
            else if (this.id == 2)
            {
                if (pos == 0)
                {
                    shape = new int[][] { new int[] { 0, 0, 0, 0},
                                          new int[] { 0, 0, 0, 0},
                                          new int[] { 2, 0, 0, 0 },
                                          new int[] { 2, 2, 2, 0 },};
                }
                else if (pos == 1)
                {
                    shape = new int[][] { new int[] { 0, 0, 0, 0 },
                                          new int[] { 2, 2, 0, 0 },
                                          new int[] { 2, 0, 0, 0},
                                          new int[] { 2, 0, 0, 0}};
                }
                else if (pos == 2)
                {
                    shape = new int[][] { new int[] { 0, 0, 0, 0},
                                          new int[] { 0, 0, 0, 0},
                                          new int[] { 2, 2, 2, 0 },
                                          new int[] { 0, 0, 2, 0 },
                                          };

                }
                else
                {
                    shape = new int[][] { new int[] { 0, 0, 0, 0 },
                                          new int[] { 0, 2, 0, 0 },
                                          new int[] { 0, 2, 0, 0},
                                          new int[] { 2, 2, 0, 0}};
                }
            }
            //if regular L
            else if (this.id == 3)
            {
                if (pos == 0)
                {
                    shape = new int[][] { new int[] { 0, 0, 0, 0 },
                                          new int[] { 0, 0, 0, 0 },
                                          new int[] { 3, 3, 3, 0 },
                                          new int[] { 3, 0, 0, 0 }};
                }
                else if (pos == 1)
                {
                    shape = new int[][] { new int[] { 0, 0, 0, 0 },
                                          new int[] { 3, 0, 0, 0 },
                                          new int[] { 3, 0, 0, 0},
                                          new int[] { 3, 3, 0, 0}};
                }
                else if (pos == 2)
                {
                    shape = new int[][] { new int[] { 0, 0, 0, 0},
                                          new int[] { 0, 0, 0, 0},
                                          new int[] { 0, 0, 3, 0},
                                          new int[] { 3, 3, 3, 0},
                                          };

                }
                else
                {
                    shape = new int[][] { new int[] { 0, 0, 0, 0 },
                                          new int[] { 3, 3, 0, 0 },
                                          new int[] { 0, 3, 0, 0},
                                          new int[] { 0, 3, 0, 0}};
                }
            }
            //S
            else if (this.id == 4)
            {
                if (pos == 0 || pos == 2)
                {
                    shape = new int[][] {new int[] { 0 ,0 ,0 ,0 },
                                         new int[] { 0 ,0 ,0 ,0 },
                                         new int[] { 0, 4, 4, 0 },
                                         new int[] { 4, 4, 0, 0 }
                                      };
                
                }
                else
                {
                    shape = new int[][] { new int[] { 0, 0, 0, 0 },
                                          new int[] { 4, 0, 0, 0 },
                                          new int[] { 4, 4, 0, 0 },
                                          new int[] { 0, 4, 0, 0 },
                                          };
                }

            }
            //Z
            else if (this.id == 5)
            {
                if (pos == 0 || pos == 2)
                {
                    shape = new int[][] { new int[] { 0 ,0 ,0 ,0 },
                                          new int[] { 0 ,0 ,0 ,0 },
                                          new int[] { 5, 5, 0, 0 },
                                          new int[] { 0, 5, 5, 0 },};
                }
                else
                {
                    shape = new int[][] { new int[] { 0, 0, 0, 0 },
                                          new int[] { 0, 5, 0, 0 },
                                          new int[] { 5, 5, 0, 0 },
                                          new int[] { 5, 0, 0, 0 },
                                          };
                }

            }
            //Square
            else if (this.id == 6)
                return;
            //T
            else if (this.id == 7)
            {
                if (pos == 0)
                {
                    shape = new int[][] { new int[] { 0, 0, 0, 0},
                                          new int[] { 0, 0, 0, 0},
                                          new int[] { 7, 7, 7, 0 },
                                          new int[] { 0, 7, 0, 0 },
                                          };
                }
                else if (pos == 1)
                {
                    shape = new int[][] { new int[] { 0, 0, 0, 0},
                                          new int[] { 7, 0, 0, 0 },
                                          new int[] { 7, 7, 0, 0 },
                                          new int[] { 7, 0, 0, 0},
                                          };
                }
                else if (pos == 2)
                {
                    shape = new int[][] { new int[] { 0, 0, 0, 0},
                                          new int[] { 0, 0, 0, 0},
                                          new int[] { 0, 7, 0, 0 },
                                          new int[] { 7, 7, 7, 0},
                                          };

                }
                else
                {
                    shape = new int[][] { new int[] { 0, 0, 0, 0 },
                                          new int[] { 0, 7, 0, 0 },
                                          new int[] { 7, 7, 0, 0 },
                                          new int[] { 0, 7, 0, 0 },
                                          };
                }
            }
            #endregion

        }

        public int checkWidth()
        {
            int id = Id;
            int position = Pos;

            if (id == 1)
            {
                if (position == 1 || position == 3)
                {
                    return 1;
                }
                else
                    return 4;
            }
            else if (id == 2)
            {
                if (position % 2 == 0)
                    return 3;
                else
                    return 2;
            }
            else if (id == 3)
            {
                if (position % 2 == 0)
                    return 3;
                else
                    return 2;
            }
            else if (id == 4)
            {
                if (position % 2 == 0)
                    return 3;
                else
                    return 2;
            }
            else if (id == 5)
            {
                if (position % 2 == 0)
                    return 3;
                else
                    return 2;
            }
            else if (id == 6)
            {
                return 2;
            }
            else if (id == 7)
            {
                if (position % 2 == 0)
                    return 3;
                else
                    return 2;
            }
            return 0;
        }

        public int checkHeight()
        {
            int id = Id;
            int position = Pos;

            if (id == 1)
            {
                if (position == 1 || position == 3)
                {
                    return 4;
                }
                else
                    return 1;
            }
            else if (id == 2)
            {
                if (position % 2 == 0)
                    return 2;
                else
                    return 3;
            }
            else if (id == 3)
            {
                if (position % 2 == 0)
                    return 2;
                else
                    return 3;
            }
            else if (id == 4)
            {
                if (position % 2 == 0)
                    return 2;
                else
                    return 3;
            }
            else if (id == 5)
            {
                if (position % 2 == 0)
                    return 2;
                else
                    return 3;
            }
            else if (id == 6)
            {
                return 2;
            }
            else if (id == 7)
            {
                if (position % 2 == 0)
                    return 2;
                else
                    return 3;
            }
            return 0;
        }
    }
}
