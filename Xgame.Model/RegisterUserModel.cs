using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Xgame.Model
{
    public class RegisterUserModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
    }
}
