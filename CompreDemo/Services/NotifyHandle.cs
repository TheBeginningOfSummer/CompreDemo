using CSharpKit.FileManagement;

namespace CompreDemo.Services
{
    public class NotifyHandle
    {
        public static Action<string>? Notify;

        public NotifyHandle() { }

        public static void Record(string log, LogType logType)
        {
            switch (logType)
            {
                case LogType.Error:
                    FileManager.AppendLog("Log\\Error", "错误记录", log);
                    Notify?.Invoke(log);
                    break;
                case LogType.Warning:
                    FileManager.AppendLog("Log\\Warning", "报警记录", log);
                    Notify?.Invoke(log);
                    break;
                case LogType.Modification:
                    FileManager.AppendLog("Log\\Modification", "更改记录", log);
                    Notify?.Invoke(log);
                    break;
            }
        }

    }

    public enum LogType
    {
        Error, Warning, Modification, Clue
    }
}
