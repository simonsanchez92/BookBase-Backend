﻿using System.ComponentModel.DataAnnotations;



namespace BookBase.DTOs
{

    public class UserCreateDto
    {
        public string Username { get;  set; }
        public string Email { get; set; }
        public string FirstName { get;  set; }
        public string LastName { get; set; }

        //Plain text password
        public string Password { get; set; }
    }


    //public class UserUpdateDto: UserCreateDto {
    
    //    public string Id { get; set; }
    
    //}

}
