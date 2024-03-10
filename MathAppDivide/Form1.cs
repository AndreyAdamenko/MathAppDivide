using System.Media;

namespace MathAppDivide
{
    public partial class Form1 : Form
    {
        double dividend = 0;
        double divisor = 0;

        int points = 0;
        int miss = 0;
        int sucsess = 0;

        int level = 1;

        int rounds = 0;

        class CheckResult
        {
            public int value { get; set; }
            public bool isCorrect { get; set; }
            public bool isSucces { get; set; }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            newSolution();

            UpdateLabels();

            button1.Click += (x, y) =>
            checkPress();

            textBox2.KeyDown += (x, y) =>
            {
                if (y.KeyCode == Keys.Enter)
                    checkPress();
            };

            FormClosing += Form1_FormClosing;

            void checkPress()
            {
                var result = checkResult();

                UpdateLabels();

                if (!result.isCorrect) return;

                textBox2.Text = "";


                if (result.isSucces)
                    textBox3.Text = $"{dividend}/{divisor}={result.value}\r\n" + textBox3.Text;
                else
                    textBox3.Text = $"{dividend}/{divisor}={result.value} (Не правильно. Ответ: {dividend/divisor})\r\n" + textBox3.Text;

                newSolution();
            }
        }

        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            var r = MessageBox.Show("Закрыть программу?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (r != DialogResult.Yes)
                e.Cancel = true;
        }

        CheckResult checkResult()
        {
            int result;

            if (int.TryParse(textBox2.Text.Trim(), out result))
            {
                var isSucces = false;

                if (dividend / divisor == result)
                {
                    points += 10 * level;
                    sucsess++;

                    Play(true);

                    isSucces = true;
                }
                else
                {
                    points -= level;
                    miss++;

                    Play(false);
                }

                rounds++;

                levelUpdate();

                var perc = Math.Round((double)sucsess / (sucsess + miss), 2) * 100;

                label9.Text = perc.ToString();

                return new CheckResult { value = result, isCorrect = true, isSucces = isSucces };
            }
            else
            {
                MessageBox.Show("Не могу понять ответ");

                return new CheckResult { value = -1, isCorrect = false, isSucces = false };
            }
        }

        void newSolution()
        {
            var t = GenerateDivisibleNumbers(2 + level, level);

            dividend = t.Item1;
            divisor = t.Item2;

            textBox1.Text = $"{dividend}/{divisor}";

            //textBox2.Text = (dividend / divisor).ToString();
        }

        static Tuple<double, double> GenerateDivisibleNumbers3(int numberOfDigitsDivisor, int numberOfDigitsMultip)
        {
            Random random = new Random();

            double divisor = 0;
            double multip = 0;
            double dividend = 0;

            while (!isCorrect())
            {
                divisor = Math.Round(random.NextDouble() * Math.Pow(10, numberOfDigitsDivisor));

                multip = Math.Round(random.NextDouble() * Math.Pow(10, numberOfDigitsMultip));

                dividend = divisor * multip;
            }

            return Tuple.Create(dividend, divisor);

            bool isCorrect()
            {
                if (dividend == 0 || divisor == 0) return false;
                if (dividend == divisor) return false;
                if (divisor == 1) return false;
                //if (dividend.ToString().Length != numberOfDigitsDividend) return false;
                if (divisor.ToString().Length != numberOfDigitsDivisor) return false;

                return true;
            }
        }

        static Tuple<int, int> GenerateDivisibleNumbers(int numberOfDigitsDividend, int numberOfDigitsDivisor)
        {
            Random random = new Random();

            int dividend = 0;
            int divisor = 0;

            while (!isCorrect())
            {
                // Генерация делимого
                int minDividend = (int)Math.Pow(10, numberOfDigitsDividend - 1);
                int maxDividend = (int)Math.Pow(10, numberOfDigitsDividend) - 1;
                dividend = random.Next(minDividend, maxDividend + 1);

                // Генерация делителя
                int minDivisor = (int)Math.Pow(10, numberOfDigitsDivisor - 1);
                int maxDivisor = (int)Math.Pow(10, numberOfDigitsDivisor) - 1;
                divisor = random.Next(minDivisor, maxDivisor + 1);

                // Убеждаемся, что делимое делится на делитель без остатка
                int quotient = dividend / divisor;
                dividend = quotient * divisor;
            }

            return Tuple.Create(dividend, divisor);

            bool isCorrect()
            {
                if (dividend == 0 || divisor == 0) return false;
                if (dividend == divisor) return false;
                if (divisor == 1) return false;
                if (dividend.ToString().Length != numberOfDigitsDividend) return false;
                if (divisor.ToString().Length != numberOfDigitsDivisor) return false;

                return true;
            }
        }

        static Tuple<double, double> GenerateDivisibleNumbers2(int numberOfDigitsDividend, int numberOfDigitsDivisor)
        {
            Random random = new Random();

            double dividend = 0;
            double divisor = 0;

            while (!isCorrect())
            {
                // Генерация делимого
                //double minDividend = (double)Math.Pow(10, numberOfDigitsDividend - 1);
                //double maxDividend = (double)Math.Pow(10, numberOfDigitsDividend) - 1;
                dividend = Math.Round(random.NextDouble() * Math.Pow(10, numberOfDigitsDividend));

                // Генерация делителя
                //double minDivisor = (double)Math.Pow(10, numberOfDigitsDivisor - 1);
                //double maxDivisor = (double)Math.Pow(10, numberOfDigitsDivisor) - 1;
                divisor = Math.Round(random.NextDouble() * Math.Pow(10, numberOfDigitsDivisor));

                // Убеждаемся, что делимое делится на делитель без остатка
                double quotient = dividend / divisor;
                dividend = quotient * divisor;
            }


            return Tuple.Create(dividend, divisor);

            bool isCorrect()
            {
                if (dividend == 0 || divisor == 0) return false;
                if (dividend == divisor) return false;
                if (divisor == 1) return false;
                if (dividend.ToString().Length != numberOfDigitsDividend) return false;
                if (divisor.ToString().Length != numberOfDigitsDivisor) return false;

                return true;
            }
        }

        void levelUpdate()
        {
            level = (sucsess / 10) + 1;

            if (level < 1) level = 1;

            if (level > 7) level = 7;
        }

        void UpdateLabels()
        {
            label3.Text = points.ToString();
            label5.Text = sucsess.ToString();
            label7.Text = miss.ToString();
            label2.Text = $"Уровень {level} по {10 * level} баллов";
        }

        void Play(bool succes)
        {
            Task.Run(() =>
            {
                var sound = succes ? @".\sound\success-1-6297.wav" : @".\sound\error-call-to-attention-129258.wav";

                SoundPlayer simpleSound = new SoundPlayer(sound);
                simpleSound.Play();
            });
        }
    }
}