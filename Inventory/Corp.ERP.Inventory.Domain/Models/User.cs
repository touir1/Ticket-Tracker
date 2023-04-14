﻿namespace Corp.ERP.Inventory.Domain.Models;

public class User
{
    public virtual int Id { get; set; }
    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }
    public string FullName
    {
        get { return FirstName + " " + LastName; }
    }
    public virtual string Email { get; set; }
}