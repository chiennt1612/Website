﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Web.Entities
{
    public class Product : MetaEntity
    {
        [StringLength(30, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [Display(Name = "ProductCode", ResourceType = typeof(Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        public string Code { get; set; }

        [StringLength(200, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [Display(Name = "ProductName", ResourceType = typeof(Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        public string Name { get; set; }
        
        [ForeignKey("Categories")]
        [Display(Name = "CateMain", ResourceType = typeof(Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        public long CategoryMain { get; set; }
        public Categories Categories { get; set; }

        [StringLength(1000, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [Display(Name = "CateRefer", ResourceType = typeof(Resources.EntityValidation))]
        public string CategoryReference { get; set; }

        //[StringLength(200, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [Display(Name = "ProductImage", ResourceType = typeof(Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        public string Img { get; set; }

        //[StringLength(200, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [Display(Name = "ProductSlide1", ResourceType = typeof(Resources.EntityValidation))]
        public string ImgSlide1 { get; set; }

        //[StringLength(200, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [Display(Name = "ProductSlide2", ResourceType = typeof(Resources.EntityValidation))]
        public string ImgSlide2 { get; set; }

        //[StringLength(200, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [Display(Name = "ProductSlide3", ResourceType = typeof(Resources.EntityValidation))]
        public string ImgSlide3 { get; set; }

        //[StringLength(200, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [Display(Name = "ProductSlide4", ResourceType = typeof(Resources.EntityValidation))]
        public string ImgSlide4 { get; set; }

        //[StringLength(200, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [Display(Name = "ProductSlide5", ResourceType = typeof(Resources.EntityValidation))]
        public string ImgSlide5 { get; set; }

        [StringLength(2000, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [Display(Name = "Summary", ResourceType = typeof(Resources.EntityValidation))]
        public string Summary { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        public string Description { get; set; }

        [Display(Name = "IsNews", ResourceType = typeof(Resources.EntityValidation))]
        public bool IsNews { get; set; }

        [Display(Name = "Status", ResourceType = typeof(Resources.EntityValidation))]
        public bool Status { get; set; }

        [Display(Name = "Price", ResourceType = typeof(Resources.EntityValidation))]
        public double Price { get; set; }

        [Display(Name = "Discount", ResourceType = typeof(Resources.EntityValidation))]
        public double Discount { get; set; }

        [Display(Name = "Quota", ResourceType = typeof(Resources.EntityValidation))]
        public int Quota { get; set; }

        [Display(Name = "IsSale", ResourceType = typeof(Resources.EntityValidation))]
        public bool IsSale { get; set; }

        [Display(Name = "IsHot", ResourceType = typeof(Resources.EntityValidation))]
        public bool IsHot { get; set; }
    }
}
