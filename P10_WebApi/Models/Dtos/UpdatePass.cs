using System;

namespace P10_WebApi.Models.Dtos;

public class UpdatePass
{

    public required string Email { get; set; }

    public required string Password { get; set; }

    public required string ConfirmPassword { get; set; }


    public required int OTP { get; set; } 



}
