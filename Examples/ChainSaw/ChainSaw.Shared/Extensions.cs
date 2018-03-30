using System;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;

namespace ChainSaw
{
    public static class Extensions
    {
        public static bool WaitHandled(this Task task, TimeSpan? timeout = null)
        {
            try
            {
                task.Wait(timeout ?? TimeSpan.FromSeconds(60));
                if (task.IsCompleted)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public static string HashPassword(this string password)
        {
            var passBytes = Encoding.UTF8.GetBytes(password);
            var resBytes = SHA256.Create().ComputeHash(passBytes);
            return Convert.ToBase64String(resBytes);
        }

        public static bool IsActiveKey(this ConsoleKey key)
        {
            return
                key == ConsoleKey.A || key == ConsoleKey.B || key == ConsoleKey.C || key == ConsoleKey.D ||
                key == ConsoleKey.E || key == ConsoleKey.F || key == ConsoleKey.G || key == ConsoleKey.H ||
                key == ConsoleKey.I || key == ConsoleKey.J || key == ConsoleKey.K || key == ConsoleKey.L ||
                key == ConsoleKey.M || key == ConsoleKey.N || key == ConsoleKey.O || key == ConsoleKey.P ||
                key == ConsoleKey.Q || key == ConsoleKey.R || key == ConsoleKey.S || key == ConsoleKey.T ||
                key == ConsoleKey.U || key == ConsoleKey.V || key == ConsoleKey.W || key == ConsoleKey.X ||
                key == ConsoleKey.Y || key == ConsoleKey.Z || key == ConsoleKey.Spacebar || key == ConsoleKey.Decimal ||
                key == ConsoleKey.Add || key == ConsoleKey.Subtract || key == ConsoleKey.Multiply || key == ConsoleKey.Divide ||
                key == ConsoleKey.D0 || key == ConsoleKey.D1 || key == ConsoleKey.D2 || key == ConsoleKey.D3 ||
                key == ConsoleKey.D4 || key == ConsoleKey.D5 || key == ConsoleKey.D6 || key == ConsoleKey.D7 ||
                key == ConsoleKey.D8 || key == ConsoleKey.D9 || key == ConsoleKey.NumPad0 || key == ConsoleKey.NumPad1 ||
                key == ConsoleKey.NumPad2 || key == ConsoleKey.NumPad3 || key == ConsoleKey.NumPad4 || key == ConsoleKey.NumPad5 ||
                key == ConsoleKey.NumPad6 || key == ConsoleKey.NumPad7 || key == ConsoleKey.NumPad8 || key == ConsoleKey.NumPad9 ||
                key == ConsoleKey.Oem1 || key == ConsoleKey.Oem102 || key == ConsoleKey.Oem2 || key == ConsoleKey.Oem3 ||
                key == ConsoleKey.Oem4 || key == ConsoleKey.Oem5 || key == ConsoleKey.Oem6 || key == ConsoleKey.Oem7 ||
                key == ConsoleKey.Oem8 || key == ConsoleKey.OemComma || key == ConsoleKey.OemMinus || key == ConsoleKey.OemPeriod ||
                key == ConsoleKey.OemPlus;
        }
    }
}
