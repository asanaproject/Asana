using Asana.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asana.Tools
{
    public class TaskLogMessages
    {
        public static string TitleChangeMessage(string lastTitle, string newTitle)
        {
            return String.Format($"Title: {lastTitle} => {newTitle}");
        }

        public static string TaskCreatedMessage(Task task)
        {
            return String.Format($"Title: {task.Title}\nColumn: {task.Column.Title}\nProject: {task.Column.Project.Name}\nAssigned to: {task.AssignedTo}\nProject Manager: {task.Column.Project.ProjectManager}");
        }

        public static string DeadlineChangedMessage(DateTime ?last,DateTime? newDate)
        {
            return String.Format($"Deadline: {last} => {newDate}");
        }

        public static string DescriptionChangedMessage(string last, string newDesc)
        {
            return String.Format($"Description: {last} => {newDesc}");
        }

        public static string ColumnTitleMessage(string lastTitle, string newTitle)
        {
            return String.Format($"Column title: {lastTitle} => {newTitle}");
        }

        public static string ColumnChangedMessage(string initial,string last)
        {
            return String.Format($"Column: {initial} -> {last}");
        }

      
    }
}
