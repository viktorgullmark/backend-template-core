using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTrackerApi.Models
{
    public class AuthorizationInfoModel
    {
        [Required]
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [Required]
        [JsonProperty("authenticated")]
        public bool Authenticated { get; set; }
    }
}
