using System.Collections.Generic;

namespace Menu.BLL.Models
{
    abstract public class BaseViewModel
    {
        public IEnumerable<string> Messages { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public BaseViewModel()
        {
            Messages = new List<string>();
            Errors = new List<string>();
        }
    }
}
