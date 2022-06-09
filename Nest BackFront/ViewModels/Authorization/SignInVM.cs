using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nest_BackFront.ViewModels.Authorization
{
    public class SignInVM
    {
        [Requirded]
        public string UsernamOrEmail{ get; set; }
        [Requirded, DataType(DataType.Password)]
        public string Password{ get; set; }

        public bool RememberMe{ get; set; }
    }
}
