using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace NewPetProjectC_
{
    public class TaskEntity
    {
        public Guid Id {  get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Theme { get; set; }
        [Required] public bool IsCompleted { get; set; } = false;
        public string PathOfReadyProject { get; set; }
        public string Description { get; set; }
    }
}
