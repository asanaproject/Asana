using Asana.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asana.Tools
{
    public class TaskLogMessages
    {
        public static string TitleChangeMessage(string message)
        {
            return String.Format($"");
        }

        public static string TaskCreatedMessage(Task task)
        {
            return String.Format($"Title: {task.Title}\nColumn: {task.Column.Title}\nProject: {task.Column.Project.Name}\nAssigned to: {task.AssignedTo}\nProject Manager: {task.Column.Project.ProjectManager}");
        }

        public static string ColumnChangedMessage(string initial,string last)
        {
            return String.Format($"Column: {initial} -> {last}");
        }

        public static string TaskChangedMessage(string initial, string last)
        {
            return String.Format($"Title: {initial} -> {last}");
        }

        public static string DeadlineIsAddedMessage(DateTime deadline)
        {
            return String.Format($"Deadline: {deadline}");
        }
    }
}
