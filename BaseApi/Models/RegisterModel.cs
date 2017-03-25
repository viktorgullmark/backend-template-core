using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BaseApi.Models
{
    public class RegisterModel
    {
        [Required]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }

        [Required]
        [JsonProperty("confirmPassword")]
        public string ConfirmPassword { get; set; }
    }
}
