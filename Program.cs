using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GDIDrawer;



namespace Single_Player_Pong
{
    internal class Program 
    {
        static void Main(string[] args)
        {
            bool quitting = false;  //quit button bool



            do
            {

                //Variables
                CDrawer Canvas = new CDrawer(800, 600);
                Canvas.Scale = 5;
                Canvas.ContinuousUpdate = false;
                int xBallVel = 1;
                int yBallVel = 1;
                int xBall = 7;              //x position for the ball
                int yBall = 3;              // y position for the ball
                int xPaddle = 0;            //x position for the paddle
                int yPaddle = 4;            //y position for the paddle
                int xPddleVel = 1;
                int yPddleVel = 1;
                Point paddlePnt = new Point();
                int xBorder = 0;
                int windowHeight = 600 / Canvas.Scale;
                int windowWidth = 800 / Canvas.Scale;
                Canvas.RedundaMouse = true;
                Point pointer = new Point();
                int score = 0;

                Point options = new Point();

                do
                {

                    //draw borders
                    Canvas.AddLine(0, 0, 800 / 5, 0, Color.Goldenrod, 10);
                    Canvas.AddLine(800 / 5, 0, 800 / 5, 600 / 5, Color.Goldenrod, 10);
                    Canvas.AddLine(0, 600 / 5, 800 / 5, 600 / 5, Color.Goldenrod, 10);

                    //ball borders
                    if (xBall > windowWidth - 1) //if the ball touches the side of the window 
                    {
                        xBallVel *= -1;        // * -1 will change direction
                    }
                    if (yBall <= 1 || yBall >= windowHeight - 1)
                    {
                        yBallVel *= -1;
                    }

                    //ball animation
                    Canvas.AddCenteredRectangle(xBall, yBall, 2, 2, Color.LawnGreen, 2);       //ball size and color

                    //clears previous position before continuing

                    xBall += xBallVel;                    //ball position is added to the ball velocity value to add one to the x position each time to keep it moving, so xposition and then plus one to the right (for example)       
                    yBall += yBallVel;


                    //pong animation
                    Canvas.GetLastMousePositionScaled(out pointer);

                    //draw the pong paddle
                    Canvas.AddCenteredRectangle(2, pointer.Y, 2, 10, Color.Magenta, 2);

                    //Canvas.AddCenteredRectangle(xPaddle, yPaddle, 2, 10, Color.Magenta, 2);

                    yPaddle += yPddleVel;   //paddle speed it added to the paddle position to keep it moving


                    //Ball touching Paddle (ball position should be less than the x position of the paddle | y position of the ball > bottom part paddle & < top of paddle)
                    if (yBall >= (pointer.Y - 4) && yBall <= (pointer.Y + 4) && xBall == 4)
                    {
                        //ball move in opposite direction when it touches the paddle
                        xBallVel *= -1;

                        //add one score each time the ball touches the paddle
                        score++;
                    }

                    //score counter
                    Canvas.AddText(score.ToString(), 50);


                    Canvas.Render();
                    System.Threading.Thread.Sleep(20);
                    Canvas.Clear();


                }
                //do the code while the ball is in front of the paddle 
                while (xBall > xPaddle);

                Rectangle quit = new Rectangle(75, 70, 10, 10);

                Rectangle retry = new Rectangle(105, 70, 10, 10);

                //final score page + quit/retry options
                //loop is executed when the ball goes outside the window
                do
                {



                    //display the final score
                    Canvas.AddText($"Final Score: {score}", 50, Color.OrangeRed);

                    //draw the rectangle
                    Canvas.AddRectangle(quit, Color.Black);

                    //add text inside the quit button
                    Canvas.AddText("quit", 15, quit, Color.IndianRed);

                    //draw the rectangle
                    Canvas.AddRectangle(retry, Color.Black);

                    //add text inside the retry button
                    Canvas.AddText("retry", 12, retry, Color.MediumTurquoise);

                    //sets options point to where the mouse is clicked
                    Canvas.GetLastMouseLeftClickScaled(out options);

                    //if mouse is clicked within the retry box
                    if (options.X >= retry.Left && options.X <= retry.Right && options.Y >= retry.Top && options.Y <= retry.Bottom)
                    {
                        Console.WriteLine("it works");
                        quitting = true;
                    }

                    //if mouse is clicked within the quit box
                    if (options.X >= quit.Left && options.X <= quit.Right && options.Y >= quit.Top && options.Y <= quit.Bottom)
                    {
                        Environment.Exit(0);
                    }

                    Canvas.Render();
                    System.Threading.Thread.Sleep(20);
                    Canvas.Clear();



                }
                //do the loop if the quit option is chosen
                while (!quitting);

                quitting = false;



            }
            //do the loop if the quit option is chosen
            while (!quitting);



        }
    }
}
