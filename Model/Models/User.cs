using System;
using System.ComponentModel.DataAnnotations;

public class User

{
    [Key]
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
}