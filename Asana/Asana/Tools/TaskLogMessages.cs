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
            return String.Format($"Title: {lastTitle} -> {newTitle}\n");
        }

        public static string TaskCreatedMessage(Task task)
        {
            return String.Format($"Title: {task.Title}\nColumn: {task.Column.Title}\nProject: {task.Column.Project.Name}\nAssigned to: {task.AssignedTo}\nProject Manager: {task.Column.Project.ProjectManager}\nKanban State: {task.CurrentKanbanState}\n");
        }

       
        public static string KanbanStateChangedMessage(string from, string to)
        {
            return String.Format($"Kanban state: {from} -> {to}\n");
        }

        public static string DeadlineChangedMessage(DateTime ?last,DateTime? newDate)
        {
            return String.Format($"Deadline: {last} -> {newDate}\n");
        }

        public static string DescriptionChangedMessage(string last, string newDesc)
        {
            return String.Format($"Description: {last} -> {newDesc}\n");
        }

        public static string ColumnTitleMessage(string lastTitle, string newTitle)
        {
            return String.Format($"Column title: {lastTitle} -> {newTitle}\n");
        }

        public static string ColumnChangedMessage(string initial,string last)
        {
            return String.Format($"Column: {initial} -> {last}\n");
        }

        public static string TaskStarChangedMessage(bool starred)
        {
            if (starred)
            {
                return String.Format($"Task starred\n");
            }
            return String.Format("Task unstarred\n");
        }

      
    }
}
