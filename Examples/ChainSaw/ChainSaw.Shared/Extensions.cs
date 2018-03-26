using System;
using System.Collections.Generic;
using Con = System.Console;
using System.Threading;
using System.Threading.Tasks;

namespace ChainSaw
{
    public static class Extensions
    {
        public static string CancellableReadLine(CancellationToken cancellationToken)
        {
            var res = Task.Run(() =>
            {
                return Con.ReadLine();
            });
            try
            {
                res.Wait(cancellationToken);
                return res.Result;
            }
            catch
            {
                return "";
            }
        }

        public static bool WaitHandled(this Task task, TimeSpan? timeout = null)
        {
            try
            {
                task.Wait(timeout ?? TimeSpan.FromSeconds(5));
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
    }
}
