﻿namespace VersionControl.Lib.Changes.Services
{
    public interface IChangeService
    {
        void Save(IReadOnlyCollection<string> filePaths, string message);

        IReadOnlyCollection<ChangeWrapper> GetHistory(HistoryQuery query);
    }
}