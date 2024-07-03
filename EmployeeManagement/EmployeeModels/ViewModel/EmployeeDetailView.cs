﻿using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeModels.ViewModel
{
    public class EmployeeDetailView
    {
       //public string Designation;

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Designation ID is required.")]
        public int DesignationId { get; set; }
        public string ProfilePicture { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        public decimal Salary { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
        //[Required(ErrorMessage = "Designation is required.")]
        public string Designation { get; set; }
    }
}
