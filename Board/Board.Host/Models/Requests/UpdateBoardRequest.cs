﻿using Board.Host.Models.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Board.Host.Models.Requests
{
    public class UpdateBoardRequest
    {
        [Required]
        public int BoardId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
