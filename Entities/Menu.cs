using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdmin.Entities
{
    public class MenuMainFooter : BaseEntity
    {
        // For Company
        [StringLength(100, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [Display(Name = "UrlText", ResourceType = typeof(Resources.EntityValidation))]
        public string UrlText { get; set; }

        // For Company
        [StringLength(200, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [Display(Name = "UrlAddress", ResourceType = typeof(Resources.EntityValidation))]
        public string UrlAddress { get; set; }

        [Display(Name = "Display", ResourceType = typeof(Resources.EntityValidation))]
        public bool Status { get; set; }
    }

    public class MenuSubFooter : BaseEntity
    {
        // For Company
        [StringLength(100, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [Display(Name = "UrlText", ResourceType = typeof(Resources.EntityValidation))]
        public string UrlText { get; set; }

        // For Company
        [StringLength(200, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [Display(Name = "UrlAddress", ResourceType = typeof(Resources.EntityValidation))]
        public string UrlAddress { get; set; }

        [Display(Name = "Display", ResourceType = typeof(Resources.EntityValidation))]
        public bool Status { get; set; }
    }
}
