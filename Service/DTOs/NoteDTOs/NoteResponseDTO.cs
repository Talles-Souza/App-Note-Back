﻿using Domain.Entities;

namespace Service.DTOs.NoteDTOs
{
    public class NoteResponseDTO
    {
        public int? Id { get; set; }
        public string Menssage { get; set; }
        public string Title { get; set; }
        public int IPerson { get; set; }
        
    }
}