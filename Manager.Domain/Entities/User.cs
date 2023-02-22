using Manager.Core.Exceptions;
using Manager.Domain.Validators;

namespace Manager.Domain.Entities;

public class User : Base
{
    
    public string Name { get; private set; }

    public string Email { get; private set; }

    public string Password { get; private set; }
    
    public User(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
        _errors = new List<string>();
        Validate();
    }
    
    //EntityFramework
    protected User() {}

    public void ChangeName(string name)
    {
        Name = name;
        Validate();
    }

    public void ChangePassword(string password)
    {
        Password = password;
        Validate();
    }

    public void ChangeEmail(string email)
    {
        Email = email;
        Validate();
    }

    public override bool Validate()
    {
        var validator = new UserValidator();
        var validation = validator.Validate(this);
        
        if (!validation.IsValid)
        {
            validation.Errors.ForEach(error =>
            {
                _errors.Add(error.ErrorMessage);
                throw new DomainException("Alguns campos estão inválidos, por favor corrija-os!", _errors);
            });
            // foreach (var error in validation.Errors)
            // {
            //     _errors.Add(error.ErrorMessage);
            // }
        }
        return true;
    }
}