using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Objects
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProjectId { get; set; }
        public int Deadline_day { get; set; }
        public int Deadline_month { get; set; }
        public int Deadline_year { get; set; }
        public int ColumnId { get; set; }
        public int ExtraInfoId { get; set; }
    }
}
