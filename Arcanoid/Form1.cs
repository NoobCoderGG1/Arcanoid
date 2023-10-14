namespace Arcanoid
{
    public partial class Form1 : Form
    {
        const int mapHeight = 60; //y
        const int mapWidth = 50; //x

        public DialogResult result;

        public int platformX, platformY;
        public int dirX = 0;
        public int dirY = 0;
        public int[,] map = new int[mapHeight, mapWidth];
        public int BallX, BallY;


        public Form1()
        {
            InitializeComponent();
            timer1.Tick += new EventHandler(update);
            Init();
        }


        private void update(object? sender, EventArgs e)
        {
            if (BallY + dirY > mapHeight - 25)
            {
                timer1.Stop();
                DialogResult result = MessageBox.Show("---Хотите начать игру заново?---", "Мяч утерян", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Init();
                }
                if (result == DialogResult.No)
                {
                    Close();
                }
            }

            map[BallY, BallX] = 0;

            if (!IsColide())
            {
                BallX += dirX;
                map[BallY, BallX + dirX] = 0;
                map[BallY, BallX - dirX - 1] = 0;
            }
            if (!IsColide())
            {
                BallY += dirY;
                map[BallY + dirY, BallX] = 0;
                map[BallY - dirY - 1, BallX] = 0;
            }

            map[BallY, BallX] = 8;


            Invalidate();
        }

        public void Area(Graphics g)
        {
            g.DrawRectangle(Pens.Black, new Rectangle(0, 0, (mapWidth * 7) + 35, mapHeight * 6));
        }

        public bool IsColide()
        {

            if (BallX + dirX > mapWidth - 13 || BallX + dirX < 1)
            {
                dirX = -dirX;
            }
            if (BallY + dirY < 1)
            {
                dirY = -dirY;
            }
            if (map[BallY, BallX + dirX] != 0)
            {
                dirX = -dirX;
            }
            if (map[BallY + dirY, BallX] != 0)
            {
                dirY = -dirY;

            }
            if (map[BallY, BallX + dirX] == 1 || map[BallY + dirY, BallX] == 1)
            {

                dirX = -dirX;
                dirY = -dirY;

            }
            return false;
        }

        public void Init()
        {
            timer1.Interval = 50;
            platformX = mapWidth / 2;
            platformY = mapHeight - 30;

            BallX = platformX + 1;
            BallY = platformY - 1;

            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    map[i, j] = 0;
                }
            }

            map[platformY, platformX] = 9;
            map[platformY, platformX + 2] = 99;
            map[platformY, platformX + 4] = 999;

            map[BallY, BallX] = 8;

            dirX = 1;
            dirY = -1;
            Blocks();

            timer1.Start();
        }
        public void Blocks()
        {
            for (int i = 0; i < mapHeight / 12; i++)
            {
                for (int j = 0; j < mapWidth; j += 2)
                {
                    if (j == 0 || j == 1)
                    {
                        continue;
                    }
                    map[i, j] = 1;
                }
            }
        }
        public void DrawMap(Graphics g)
        {
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    if (map[i, j] == 9)
                    {
                        pictureBox1.Location = new Point(j * 10, i * 10);
                    }
                    if (map[i, j] == 8)
                    {
                        pictureBox2.Location = new Point(j * 10, i * 10);
                    }
                    if (map[i, j] == 1)
                    {
                        g.DrawRectangle(Pens.Black, new Rectangle(j * 10, i * 10, 15, 5));
                    }
                }
            }
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Area(e.Graphics);
            DrawMap(e.Graphics);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            map[platformY, platformX] = 0;
            map[platformY, platformX + 2] = 0;
            map[platformY, platformX + 4] = 0;
            if (e.KeyCode == Keys.Right)
            {
                if (platformX < 31)
                {
                    platformX += 2;
                }
            }
            if (e.KeyCode == Keys.Left)
            {
                if (platformX > 2)
                {
                    platformX -= 2;
                }
            }
            map[platformY, platformX] = 9;
            map[platformY, platformX + 2] = 99;
            map[platformY, platformX + 4] = 999;
            if (e.KeyCode == Keys.F1)
            {
                //timer1.Stop();
                DialogResult result = MessageBox.Show("Разработал: Кучеров Николай\nГруппа: 183\nПреподаватель: Тупицын К.М.");
                if (result == DialogResult.OK) { //timer1.Start();
                                                 }
            }

        }

    }
}