using Asana.Objects;
using Asana.Services;
using Asana.Services.Interfaces;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Model
{
    public class CurrentProject : ViewModelBase
    {
        private readonly ITaskService taskService;
        private CurrentProject()
        {
            taskService = new TaskService();
            Project = new Project();
            KanbanStates = new ObservableCollection<KanbanState>(taskService.GetKanbanStatesOfTask());
        }
        private static CurrentProject instance;
        public static CurrentProject Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CurrentProject();
                }
                return instance;
            }
        }

        private ObservableCollection<KanbanState> kanbanStates;
        public ObservableCollection<KanbanState> KanbanStates
        {
            get { return kanbanStates; }
            set { Set(ref kanbanStates, value); }
        }

        private Project project;
        public Project Project
        {
            get { return project; }
            set { Set(ref project, value); }
        }



    }
}
