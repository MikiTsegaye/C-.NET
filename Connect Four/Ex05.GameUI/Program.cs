using Ex05;
using System;
using System.Windows.Forms;

namespace Ex05_Eylon_203235478_Michael_208233890
{
    public static class Program
    {
        public static void Main()
        {
            runGame();
        }

        private static void runGame()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FormSettings settingsForm = new FormSettings();

            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                FormGame gameForm = new FormGame(
                    settingsForm.Rows,
                    settingsForm.Cols,
                    settingsForm.Player1Name,
                    settingsForm.Player2Name,
                    settingsForm.IsAgainstComputer);

                Application.Run(gameForm);
            }
        }
    }
}