using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vroom.Models.ViewModel
{
    public class ModelViewModel
    {
        public Model? Model { get; set; }
        public IEnumerable<Make>? Makes { get; set; }
        

    }

}
