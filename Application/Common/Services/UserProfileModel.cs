using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Errands.Application.Common.Services
{
    public class UserProfileModel
    {
        public string Email { get; set; }
        [Required]
        [MaxLength(25)]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CompletedErrands { get; set; }
        public string Id { get; set; }
        public IFormFile Logo { get; set; }
    }
}
