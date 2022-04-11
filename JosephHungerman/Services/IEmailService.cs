﻿using JosephHungerman.Models.Dtos;
using JosephHungerman.Models.Dtos.Contact;

namespace JosephHungerman.Services
{
    public interface IEmailService
    {
        Task<ResponseDto> SendEmailAsync(MessageDto message);
    }
}
