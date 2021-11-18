﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace ZeroHunger.Model
{
    [Table("DryFoodDonation")]
    public class DryFoodDonation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string DryFoodName { get; set; }
        [Required,]
        public int DryFoodQuantity { get; set; }
        [Required]
        public string DryFoodPickDate { get; set; }
        [Required]
        public string DryFoodRemark { get; set; }
        [Required]
        public string DonorId { get; set; }
        public string DelivererId { get; set; }
        /*[ForeignKey("DonorId")]
        public virtual Donor donor_Id { get; set; }
        [ForeignKey("DelivererId")]
        public virtual Deliverer deliverer_Id { get; set; }
        */



    }


}
