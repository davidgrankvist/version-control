﻿using VersionControl.Lib.Execution;

namespace VersionControl.App;

internal class Program
{
    public static void Main(string[] args)
    {
        var cli = CliCreator.Create();
        cli.Run(args);
    }
}