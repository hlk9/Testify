using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.ViewModels
{
    public class AnswerInExcel
    {
        public int ConnectionId { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
    }
}
