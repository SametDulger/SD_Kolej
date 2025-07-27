using System;
using System.ComponentModel.DataAnnotations;

namespace SDKolej.Business.DTOs
{
    public class MessageDto
    {
        public int Id { get; set; }
        
        [Display(Name = "Gönderen")]
        public int SenderId { get; set; }
        
        [Display(Name = "Gönderen Rolü")]
        public string? SenderRole { get; set; }
        
        [Display(Name = "Alıcı")]
        public int ReceiverId { get; set; }
        
        [Display(Name = "Alıcı Rolü")]
        public string? ReceiverRole { get; set; }
        
        [Display(Name = "İçerik")]
        public string? Content { get; set; }
        
        [Display(Name = "Gönderilme Tarihi")]
        public DateTime SentAt { get; set; }
        
        [Display(Name = "Okundu")]
        public bool IsRead { get; set; }
        
        [Display(Name = "Konu")]
        public string? Subject { get; set; }
        
        [Display(Name = "Gönderilme Tarihi")]
        public DateTime SentDate => SentAt;
        
        // Navigation properties
        public StudentDto? SenderStudent { get; set; }
        public TeacherDto? SenderTeacher { get; set; }
        public ParentDto? SenderParent { get; set; }
        public StudentDto? ReceiverStudent { get; set; }
        public TeacherDto? ReceiverTeacher { get; set; }
        public ParentDto? ReceiverParent { get; set; }
    }
} 