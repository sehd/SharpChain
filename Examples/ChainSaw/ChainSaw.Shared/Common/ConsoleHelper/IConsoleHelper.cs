using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ChainSaw.ConsoleHelper
{
    public interface IConsoleHelper
    {
        string CancellableReadLine(CancellationToken cancellationToken);
    }
}
