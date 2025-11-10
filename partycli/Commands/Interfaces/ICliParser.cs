
using System.CommandLine;

namespace partycli.Commands.Interfaces
{
    internal interface ICliParser
    {
        RootCommand BuildRootCommand();
    }
}
