using SnakesNLadders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace SnakesNLadders
{
    public partial class MainWindow : Window
    {
        Rectangle playerOne;
        Rectangle playerTwo;
        Rectangle targetRec; // the tile we landed on each turn
        ImageBrush playerOneImage = new ImageBrush(); // image brush to import the player GIF image and attach to the player rectangle
        ImageBrush playerTwoImage = new ImageBrush(); // image brush to import the opponent GIF image and attach to the opponent rectangle
        private readonly Random random = new Random();
        private readonly List<Rectangle> Tiles = new List<Rectangle>(); // list of rectangles to store the board tiles into
        private readonly List<Snake> snakes = new List<Snake>();
        private readonly List<Ladder> ladders = new List<Ladder>();
        int position, currentPosition;
        int playerTwoPosition, playerTwoCurrentPosition;
        int tempPos;// the current position of the player and the opponent to the GUI
        bool isPlayerOneRound, isPlayerTwoRound;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Function that sets up the game board after getting the number of snakes and ladders.
        /// </summary>
        private void Snakes_Ladders_Click(object sender, RoutedEventArgs e)
        {
            startPanel.Visibility = Visibility.Collapsed;
            dicePanel.Visibility = Visibility.Visible;
            dice1Image.Source = new BitmapImage(new Uri(@"images/6.png",UriKind.RelativeOrAbsolute));
            dice2Image.Source = new BitmapImage(new Uri(@"/images/6.png", UriKind.RelativeOrAbsolute));
            SetupGame(); // run the set up game function from inside this constructor
        }

        /// <summary>
        /// This function is called on each roll of dices. 
        /// We generate a random number of the dices, and check whose turn is to play.
        /// </summary>
        private void RollDice(object sender, RoutedEventArgs e)

        {
            int dice1, dice2;
            Random random = new Random();
            dice1 = random.Next(1, 7);
            dice2 = random.Next(1, 7);

            //show the dice images according to the rolls
            dice1Image.Source = new BitmapImage(new Uri(@$"/images/{dice1}.png", UriKind.RelativeOrAbsolute));
            dice2Image.Source = new BitmapImage(new Uri(@$"/images/{dice2}.png", UriKind.RelativeOrAbsolute));
            diceResult.Content = (dice1 + dice2).ToString();

            // check if it is the first round for both players
            if (isPlayerOneRound == false && isPlayerTwoRound == false)
            {
                position = dice1 + dice2;
                player1Text.Content = "You Rolled a " + position.ToString();
                currentPosition = 0;

                //check if i (=player1 current position) is in the game
                if ((currentPosition + position) <= 99)
                {
                    player1Turn();
                }
                else
                {
                    if (isPlayerTwoRound == false)
                    {
                        playerTwoPosition = dice1 + dice2;
                        player2Text.Content = "You Rolled a " + playerTwoPosition.ToString();
                        playerTwoCurrentPosition = 0;
                        player2Turn();
                    }
                    else
                    {
                        isPlayerOneRound = false;
                        isPlayerTwoRound = false;
                    }
                }
            }
            else if (isPlayerOneRound == true && isPlayerTwoRound == false)
            {
                position = currentPosition + dice1 + dice2;
                player1Text.Content = "You Rolled a " + (dice1 + dice2).ToString();
                player1Turn();
            }
            else if (isPlayerTwoRound == true && isPlayerOneRound == false)
            {
                playerTwoPosition = playerTwoCurrentPosition + dice1 + dice2;
                player2Text.Content = "You Rolled a " + (dice1 + dice2).ToString();
                player2Turn();
            }
        }

        /// <summary>
        /// Sets up the game board with 100 tiles, creates two players and draw snakes & ladders.
        /// </summary>
        private void SetupGame()
        {
            int leftPos = 10; // this will help us position the tiles from right to left 
            int topPos = 600; // this will help us position the tiles from bottom to top
            int a = 0; // integer to check if we have 10 tiles in a row
            playerOneImage.ImageSource = new BitmapImage(new Uri("C:/Users/גבי/Documents/SnakesNLadders/SnakesNLadders/images/player1.png"));
            playerTwoImage.ImageSource = new BitmapImage(new Uri("C:/Users/גבי/Documents/SnakesNLadders/SnakesNLadders/images/player2.png"));

            // for loop to make the game board
            for (int i = 0; i < 100; i++)
            {
                //creating a tile with 60X60 width & height
                Rectangle tile = new Rectangle
                {
                    Height = 60,
                    Width = 60,
                    Fill = i % 2 == 0 ? Brushes.Red : Brushes.AntiqueWhite,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1
                };

                //These tile numbers will be gold tiles
                if (i == 21 || i == 59)
                {
                    tile.Fill = Brushes.Gold;
                }

                //we will give each tile a name to indentify the rectangle
                tile.Name = "tile" + i.ToString();
                this.RegisterName(tile.Name, tile);
                Tiles.Add(tile); //add the tile to the tiles rectangles list

                /*This will be the TextBlock for number of the block*/
                var numberTextBlock = new TextBlock
                {
                    Text = (i + 1).ToString(), // Set the number you want to display
                    VerticalAlignment = VerticalAlignment.Bottom, // Align the text to the bottom
                    HorizontalAlignment = HorizontalAlignment.Center, // Center the text horizontally
                    Foreground = Brushes.Black // Set the text color
                };

                var grid = new Grid();
                grid.Children.Add(tile);
                grid.Children.Add(numberTextBlock);

                /*we will position 10 tiles in a row , and from left to right and move up when needed*/
                if (a == 10) //we positioned 10 tiles from left to right
                {
                    topPos -= 60; //reduce 60 from the top pos integer 
                    a = 30; // now we need tomove the tiles from right to left
                }
                if (a == 20)
                {
                    topPos -= 60; // reduce 60 from the top pos integer
                    a = 0; //set a back to 0
                }
                if (a > 20)
                {
                    // now we position the tiles from right to left
                    a--; // reduce 1 from a each loop
                    Canvas.SetLeft(FindRectangleInsideGrid(grid), leftPos); // set the tile inside the canvas using the value of leftpos
                    Canvas.SetLeft(grid, leftPos);
                    leftPos -= 60; // reduce 60 from the left pos each loop
                }
                // if a is less than 10
                if (a < 10)
                {
                    // this will happen when we want to position the tiles from left to right
                    a++; // add 1 to a integer each loop
                    Canvas.SetLeft(FindRectangleInsideGrid(grid), leftPos); // set the tile left position
                    Canvas.SetLeft(grid, leftPos); // set the grid (containing the tile and its number) left position
                    leftPos += 60; // add 60 to the left pos integer 
                    Canvas.SetLeft(FindRectangleInsideGrid(grid), leftPos); // set the tile left position
                    Canvas.SetLeft(grid, leftPos);
                }
                Canvas.SetTop(FindRectangleInsideGrid(grid), topPos); //set the tile top position 
                Canvas.SetTop(grid, topPos);

                // add the new tile to the canvas
                MyCanvas.Children.Add(grid);
            }

            /*create rectangles of player 1 and player 2 */
            playerOne = new Rectangle
            {
                Height = 30,
                Width = 30,
                Fill = playerOneImage,
                StrokeThickness = 2
            };
            playerTwo = new Rectangle
            {
                Height = 30,
                Width = 30,
                Fill = playerTwoImage,
                StrokeThickness = 2
            };
            // add the players to the canvas
            MyCanvas.Children.Add(playerOne);
            MyCanvas.Children.Add(playerTwo);
            // Place the players on the tile 0
            MovePiece(playerOne, "tile" + 0);
            MovePiece(playerTwo, "tile" + 0);

            /* Draw the snake between the two positions*/
            DrawSnakes();
            DrawLadders();
        }

        /// <summary>
        /// Generate two random tiles to draw a snake between them
        /// </summary>
        private void DrawSnakes()
        {
            for (int i = 0; i < int.Parse(snakesInput.Text); i++)
            {
                //generate 2 random tiles
                List<Rectangle> rectList = GetRandomRectangle(Tiles, "snake");
                Rectangle rectangle1 = rectList[0];
                Rectangle rectangle2 = rectList[1];
                //get the numbers of the tiles
                int rect1_number = int.Parse(rectangle1.Name.Remove(0, 4));
                int rect2_number = int.Parse(rectangle2.Name.Remove(0, 4));
                //get the center point of the tiles
                Point center1 = new Point(Canvas.GetLeft(rectangle1) + rectangle1.Width / 2, Canvas.GetTop(rectangle1) + rectangle1.Height / 2);
                Point center2 = new Point(Canvas.GetLeft(rectangle2) + rectangle2.Width / 2, Canvas.GetTop(rectangle2) + rectangle2.Height / 2);

                // Check what point needs to be the head and what point is the tail.
                // Draw a snake (line) between the centers of the rectangles and add the snake to the list.
                if (rect1_number > rect2_number)
                {
                    DrawSnake(MyCanvas, center1, center2);
                    snakes.Add(new Snake(rect1_number, rect2_number));
                }
                else
                {
                    DrawSnake(MyCanvas, center2, center1);
                    snakes.Add(new Snake(rect2_number, rect1_number));
                }
            }
        }

        /// <summary>
        /// Generate two random tiles to draw a ladder between them
        /// </summary>
        private void DrawLadders()
        {
            for (int i = 0; i < int.Parse(laddersInput.Text); i++)
            {
                //generate 2 random tiles
                List<Rectangle> rectList = GetRandomRectangle(Tiles, "ladder");
                Rectangle rectangle1 = rectList[0];
                Rectangle rectangle2 = rectList[1];
                //get the numbers of the tiles
                int rect1_number = int.Parse(rectangle1.Name.Remove(0, 4));
                int rect2_number = int.Parse(rectangle2.Name.Remove(0, 4));
                //get the center point of the tiles
                Point center1 = new Point(Canvas.GetLeft(rectangle1) + rectangle1.Width / 2, Canvas.GetTop(rectangle1) + rectangle1.Height / 2);
                Point center2 = new Point(Canvas.GetLeft(rectangle2) + rectangle2.Width / 2, Canvas.GetTop(rectangle2) + rectangle2.Height / 2);

                // Check what point needs to be the Top and what point is the Bottom.
                // Draw a ladder (line) between the centers of the rectangles and add the ladder to the list.
                if (rect1_number > rect2_number)
                {
                    DrawLadder(MyCanvas, center1, center2);
                    ladders.Add(new Ladder(rect1_number, rect2_number));
                }
                else
                {
                    DrawLadder(MyCanvas, center2, center1);
                    ladders.Add(new Ladder(rect2_number, rect1_number));
                }
            }
        }

        /// <summary>
        /// Draw a line that looks like a snake
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="start">the head point of the snake</param>
        /// <param name="end">the tail point of the snake</param>
        private void DrawSnake(Canvas canvas, Point start, Point end)
        {
            var headPoint = new Point(start.X, start.Y);
            var tailPoint = new Point(end.X, end.Y);

            var snakePath = new Path
            {
                Stroke = new LinearGradientBrush(
                   new GradientStopCollection
                   {
                        new GradientStop(Colors.Yellow, 0),
                        new GradientStop(Colors.Green, 0.5),
                        new GradientStop(Colors.Yellow, 1)
                   }),
                StrokeThickness = 3
            };

            var geometry = new PathGeometry();
            var figure = new PathFigure
            {
                StartPoint = headPoint // Set the start point to the head position
            };

            // Calculate control points for a Bezier curve
            var controlPoint1 = new Point(headPoint.X + (tailPoint.X - headPoint.X) / 5, headPoint.Y - 30);
            var controlPoint2 = new Point(headPoint.X + (tailPoint.X - headPoint.X) / 5 * 4, headPoint.Y - 30);


            var bezierSegment = new BezierSegment
            {
                Point1 = controlPoint1,
                Point2 = controlPoint2,
                Point3 = tailPoint // Set the end point to the tail position
            };

            figure.Segments.Add(bezierSegment);
            geometry.Figures.Add(figure);

            snakePath.Data = geometry;

            // Add the snake to the canvas
            canvas.Children.Add(snakePath);
        }

        /// <summary>
        /// Draw a line that looks like a ladder
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="start">the top point of the ladder</param>
        /// <param name="end">the bottom point of the ladder</param>
        private void DrawLadder(Canvas canvas, Point start, Point end)
        {
            // Create a line (ladder) between the start and end points
            var ladderLine = new Line
            {
                X1 = start.X,
                Y1 = start.Y,
                X2 = end.X,
                Y2 = end.Y,
                Stroke = Brushes.Gray,
                StrokeThickness = 6
            };

            // Add the ladder to the canvas
            canvas.Children.Add(ladderLine);

        }

        /// <summary>
        /// Generate two random tiles.These tiles need to be without a snake or a ladder on them, and from two differrent rows.
        /// </summary>
        /// <param name="rectangles"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private List<Rectangle> GetRandomRectangle(List<Rectangle> rectangles, string type)
        {
            int totalRows = 10;
            List<Rectangle> rectlist = new List<Rectangle>();
            int number1, number2;
            int row1, row2;

            /*generate 2 random numbers and check if they are from two different rows and there are no snakes or ladders on them*/
            do
            {
                //make sure a snake will not be on the last tile or on a gold tile
                do { number1 = type.Equals("snake") ? random.Next(0, 99) : random.Next(0, 100); } while (number1 == 21 || number1 == 59);
                do { number2 = type.Equals("snake") ? random.Next(0, 99) : random.Next(0, 100); } while (number2 == 21 || number2 == 59);
                CalculateRow(number1, out row1, totalRows);
                CalculateRow(number2, out row2, totalRows);
            } while (row1 == row2 || CheckSnakeExists(number1) || CheckLadderExists(number1) || CheckSnakeExists(number2) || CheckLadderExists(number2));

            // Return the two rectangles
            rectlist.Add(rectangles[number1]); rectlist.Add(rectangles[number2]);
            return rectlist;
        }

        private static void CalculateRow(int number, out int row, int totalRows)
        {
            row = (number - 1) / totalRows + 1;
        }

        /// <summary>
        /// Check if there is a snake on tile 'num' 
        /// </summary>
        /// <param name="num">number of tile</param>
        /// <returns>true if snake exists</returns>
        private bool CheckSnakeExists(int num)
        {
            if (snakes.Any(snake => snake.Head == num || snake.Tail == num))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if there is a ladder on tile 'num' 
        /// </summary>
        /// <param name="num">number of tile</param>
        /// <returns>true if ladder exists</returns>
        private bool CheckLadderExists(int num)
        {
            if (ladders.Any(ladder => ladder.Top == num || ladder.Bottom == num))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Iterate over grid's children and return the rectangle inside of that grid
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private Rectangle FindRectangleInsideGrid(Grid grid)
        {
            //Iterate over the grid's children
            foreach (UIElement child in grid.Children)
            {
                if (child is Rectangle rectangle)
                {
                    return rectangle;
                }
            }
            return null;
        }

        /// <summary>
        /// Checks player's current position and place him in the desired position. 
        /// The function checks if the position is a snake\ladder or a gold tile and place the player according to these conditions.
        /// If player has reached to the top of the board we show a message of the winner.
        /// </summary>
        private void player1Turn()
        {
            if (position < Tiles.Count)
            {
                // check if we did not reach the position that we generated with the dices
                if (currentPosition < position && (position < 100))
                {
                    currentPosition = position;
                    //i = position;// define i 
                    //move the player to the desired position
                    MovePiece(playerOne, "tile" + position);
                }
                // check if we landed on a snake\ladder
                position = CheckSnakesOrLadders(position);
                //if current position is a gold tile
                if (position == 21 || position == 59)
                {
                    int temp = currentPosition;
                    currentPosition = playerTwoCurrentPosition;
                    playerTwoCurrentPosition = temp;
                    MovePiece(playerTwo, "tile" + playerTwoCurrentPosition);
                }
                currentPosition = position;
                // update the players position
                MovePiece(playerOne, "tile" + position);

                tempPos = currentPosition;
                player1PositionText.Content = "Player 1 is on " + (tempPos + 1); // show the player current position
            }
            // check if the player has made to top of the board
            if (position == 99)
            {
                // if we have reached to the end - show a message on the screen
                MessageBox.Show("Player 1 is the winner!", "Game Over");
            }
            isPlayerOneRound = false;
            isPlayerTwoRound = true;
        }

        /// <summary>
        /// Checks player's current position and place him in the desired position. 
        /// The function checks if the position is a snake\ladder or a gold tile and place the player according to these conditions.
        /// If player has reached to the top of the board we show a message of the winner.
        /// </summary>
        private void player2Turn()
        {
            if (playerTwoPosition < Tiles.Count)
            {
                // check if we did not reach the position that we generated with the dices
                if (playerTwoCurrentPosition < playerTwoPosition && (playerTwoPosition < 100))
                {
                    playerTwoCurrentPosition = playerTwoPosition;
                    //j = playerTwoPosition; // increase the actual position of the opponent
                    //move the player to the desired position
                    MovePiece(playerTwo, "tile" + playerTwoPosition);
                }
                // check if we landed on a snake\ladder
                playerTwoPosition = CheckSnakesOrLadders(playerTwoPosition);
                //if current position is a gold tile
                if (playerTwoPosition == 21 || playerTwoPosition == 59)
                {
                    int temp = playerTwoPosition;
                    playerTwoPosition = currentPosition;
                    currentPosition = temp;
                    MovePiece(playerOne, "tile" + currentPosition);
                }

                playerTwoCurrentPosition = playerTwoPosition;
                // update the players position
                MovePiece(playerTwo, "tile" + playerTwoPosition);

                tempPos = playerTwoPosition;
                player2PositionText.Content = "Player 2 is on " + (tempPos + 1);
            }
            // check if the player has made to top of the board
            if (playerTwoPosition == 99)
            {
                // if we have reached to the end - show a message on the screen
                MessageBox.Show("Player 2 is the winner!", "Game Over");
            }
            isPlayerOneRound = true;
            isPlayerTwoRound = false;
        }

        /// <summary>
        /// Check if the number is a head of a snake or a bottom of a tail.
        /// </summary>
        /// <param name="num">number of a tile</param>
        /// <returns></returns>
        private int CheckSnakesOrLadders(int num)
        {
            // this is the check snakes or ladders function. The purpose of this function is to check if thep player has
            // landed on the bottom of a ladder or top of the snake
            if (snakes.Any(snake => snake.Head == num))
            {
                num = snakes.First(snake => snake.Head == num).Tail;
            }

            if (ladders.Any(ladder => ladder.Bottom == num))
            {
                num = ladders.First(ladder => ladder.Bottom == num).Top;
            }

            return num;
        }

        /// <summary>
        /// Iterate over the tiles list and find the rectangle of the given position.
        /// After finding the rectangle we set the player on it.
        /// </summary>
        /// <param name="player">a player to place on the rectangle</param>
        /// <param name="position">number of a rectangle</param>
        private void MovePiece(Rectangle player, string position)
        {
            //iterate over the tiles list and find the rectangle with the name of "position"
            foreach (Rectangle rectangle in Tiles)
            {
                if (rectangle.Name == position)
                {
                    targetRec = rectangle;
                }
            }

            // set the Canvas.ZIndex to make sure the new Rectangle is above the specific Grid
            Canvas.SetZIndex(player, 1);
            // sets the player position based on targetRec location            
            Canvas.SetLeft(player, Canvas.GetLeft(targetRec) + player.Width / 2);
            Canvas.SetTop(player, (Canvas.GetTop(targetRec)) + player.Height / 2);
        }
    }
}