using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPetProjectC_.TasksEntity
{
    public class TimeEntity
    {
        public Guid Id { get; set; }
        [Required] public string Time { get; set; }
        [Required] public string Task { get; set; }
        [Required] public bool Checked { get; set; } = false;
    }
}
