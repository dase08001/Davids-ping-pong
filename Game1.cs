using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Davids_ping_pong;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Texture2D pixel;
    SpriteFont font;
    Rectangle leftPaddle = new Rectangle(30, 0, 20, 100);
    Rectangle rightPaddle = new Rectangle(800-50,0, 20, 100);
    Rectangle ball = new Rectangle(390,250, 20,20);

    int ballSpeedX = 4;
    int ballSpeedY = 4;
    int scoreLeft = 0;
    int scoreRight = 0;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        pixel = new Texture2D(GraphicsDevice, 1, 1);
        pixel.SetData([Color.White]);

        font = Content.Load<SpriteFont>("Font");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

        MovePaddles();
        MoveBall();
        Collision();
        CheckScore();
        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.DrawString(font,$"{scoreLeft}", new Vector2(50,20), Color.White);
        _spriteBatch.DrawString(font,$"{scoreRight}", new Vector2(700,20), Color.White);

        _spriteBatch.Draw(pixel, leftPaddle, Color.Red);
        _spriteBatch.Draw(pixel, rightPaddle, Color.Red);
        _spriteBatch.Draw(pixel, ball, Color.White);

        _spriteBatch.End();
        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }

    void MovePaddles()
    {
        KeyboardState kState = Keyboard.GetState();

        if(kState.IsKeyDown(Keys.W) && leftPaddle.Y > 0)
        {
            leftPaddle.Y = leftPaddle.Y - 3;
        }
        // bottom = leftPaddel.Y + leftPaddel.height
        else if(kState.IsKeyDown(Keys.S) && leftPaddle.Bottom < 480)
        {
            leftPaddle.Y = leftPaddle.Y + 3;
        }

        if(kState.IsKeyDown(Keys.Up) && rightPaddle.Y > 0)
        {
            rightPaddle.Y = rightPaddle.Y - 3;
        }
        else if(kState.IsKeyDown(Keys.Down) && rightPaddle.Bottom < 480)
        {
            rightPaddle.Y = rightPaddle.Y + 3;
        }
    }

    void MoveBall()
    {
        ball.Y += ballSpeedY;
        ball.X += ballSpeedX;
        if(ball.Bottom >= 480)
        {
            ball.Y = 480-ball.Height;
            ballSpeedY *= -1;
        }
        if(ball.Bottom <= 0)
        {
            ball.Y = 0-ball.Height;
            ballSpeedY *= -1;
        }
    }

    void Collision()
    {
        if(ball.Intersects(leftPaddle) || ball.Intersects(rightPaddle))
        {
            ballSpeedX *= -1;
        }
    }

    void ResetBall()
    {
        ball.X = 390;
        ball.Y = 230;
    }

    void CheckScore()
    {
        if(ball.Right <= 0)
        {
            ResetBall();
            scoreRight++;
        }

        if(ball.X >= 800)
        {
            ResetBall();
            scoreLeft++;

        }
    }

}
